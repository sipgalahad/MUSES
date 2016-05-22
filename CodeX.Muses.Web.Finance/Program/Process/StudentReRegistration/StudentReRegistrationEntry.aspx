<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="StudentReRegistrationEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.StudentReRegistrationEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnProcess" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Process")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnProcess.ClientID %>').click(function () {
                var param = "";
                var result = '';
                var lstStudentID = '';
                $('.chkIsSelected input:checked').each(function () {
                    $tr = $(this).closest('tr');
                    var id = $tr.find('.keyField').html();
                    var idx = $tr.find('.hdnItemIndex').val();

                    if (param != '') {
                        param += '|';
                        result += '|';
                        lstStudentID += ',';
                    }
                    var scholarshipID = 0;
                    param += id + ',' + scholarshipID;

                    var temp = '';
                    $tr.find('.txtNewCompValue').each(function () {
                        if (temp != '')
                            temp += ';';
                        temp += $(this).attr('studentfeecomptypeid') + '^' + $(this).attr('hiddenVal');
                    });
                    result += id + ',' + temp;
                    lstStudentID += id;
                });
                $('#<%=hdnSelectedValue.ClientID %>').val(param);
                $('#<%=hdnSaveValue.ClientID %>').val(result);
                $('#<%=hdnLstStudentID.ClientID %>').val(lstStudentID);

                onCustomButtonClick('promote');
            })

            setStudentImage();
        });
        
        $('.chkSelectAll input').live('click',function () {
            var value = $(this).is(':checked');
            $('.tblView .chkIsSelected input').each(function () {
                $(this).prop("checked", value);
            });
        });

        function onAfterCustomClickSuccess() {
            cbpView.PerformCallback('refresh');
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        function onCbpViewEndCallback(s) {
            addTableHeader();
            setStudentImage();
            hideLoadingPanel();
            $('.tblView .txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }

        function setStudentImage() {
            setTimeout(function () {
                var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
                var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");

                $('.imgStudentImage').each(function () {
                    $divStudentImage = $(this).parent().find('.divStudentImage');
                    $divStudentImage.attr('style', "background-image:url('" + this.src + "')");
                    $(this).error(function () {
                        var gender = $(this).parent().find('.hdnStudentGender').val();
                        if (gender == '0003^F')
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlF + "')");
                        else
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlM + "')");
                    }).attr('src', this.src);
                });
            }, 0);
        }

        $('.lblFinalMark').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + '|' + tacSchoolClass.getValue() + '|' + tacPeriodSection.getValue();
            var url = ResolveUrl("~/Program/StudentMark/StudentFinalMarkDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Nilai', 800, 550);       
        });

        function onCboStudentTypeValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

        function onCboSiteValueChanged() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNowFilterExpression() %>";
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                if (result != null) {
                    tacSchoolPeriod.setValue(result.SchoolPeriodID);
                    tacSchoolPeriod.setText(result.SchoolPeriodName);
                }
                else {
                    tacSchoolPeriod.setValue('');
                    tacSchoolPeriod.setText('');
                }
                var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNextFilterExpression() %>";
                Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacNextSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacNextSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacNextSchoolPeriod.setValue('');
                        tacNextSchoolPeriod.setText('');
                    }
                    onTacNextSchoolPeriodValueChanged();
                });
            });
        }

        //#region School Period
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "'";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                    }
                    onTacSchoolPeriodValueChanged();
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Period Class Type
        function onGetPeriodClassTypeFilterExpression() {
            if (tacSchoolPeriod.getValue() == "")
                return "1 = 0";
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue();
            return filterExpression;
        }

        function onTacPeriodClassTypeButtonSearchClick() {
            openSearchDialog('periodclasstype', onGetPeriodClassTypeFilterExpression(), function (value) {
                var filterExpression = onGetPeriodClassTypeFilterExpression() + " AND CurriculumClassTypeCode = '" + value + "'";
                Methods.getObject('GetvPeriodClassTypeList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodClassType.setValue(result.PeriodClassTypeID);
                        tacPeriodClassType.setText(result.CurriculumClassTypeName);
                    }
                    else {
                        tacPeriodClassType.setValue('');
                        tacPeriodClassType.setText('');
                    }
                    onTacPeriodClassTypeValueChanged();
                });
            });

        }

        function onTacPeriodClassTypeValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Next School Period
        function onTacNextSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacNextSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacNextSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacNextSchoolPeriod.setValue('');
                        tacNextSchoolPeriod.setText('');
                    }
                    onTacNextSchoolPeriodValueChanged();
                });
            });

        }

        function onTacNextSchoolPeriodValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        $(function () {
            addTableHeader();
        });

        function addTableHeader() {
            $('#tblView thead').html($('#tblView1 thead').html());
        }

        $('#tblView .txtNewValue').live('change', function () {
            $(this).trigger('changeValue');
            var value = $(this).attr('hiddenVal');
            var idx = $('#tblView .txtNewValue').index($(this));
            $('.trDt').each(function () {
                $(this).find('.txtNewCompValue:eq(' + idx + ')').val(value).trigger('changeValue');
            });
        });

        $('#tblView .txtMarkupValue').live('change', function () {
            var value = parseFloat($(this).val());
            var idx = $('#tblView .txtMarkupValue').index($(this));
            $('.trDt').each(function () {
                var oldValue = parseFloat($(this).find('.txtOldCompValue:eq(' + idx + ')').attr('hiddenVal'));
                var newValue = oldValue + value;
                $(this).find('.txtNewCompValue:eq(' + idx + ')').val(newValue).trigger('changeValue');
            });
        });

        $('#tblView .txtMarkupPercentageValue').live('change', function () {
            var value = parseFloat($(this).val());
            var idx = $('#tblView .txtMarkupValue').index($(this));
            $('.trDt').each(function () {
                var oldValue = parseFloat($(this).find('.txtOldCompValue:eq(' + idx + ')').attr('hiddenVal'));
                var newValue = oldValue * (value + 100) / 100;
                $(this).find('.txtNewCompValue:eq(' + idx + ')').val(newValue).trigger('changeValue');
            });
        });
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
    </style>
    <input type="hidden" runat="server" id="hdnLstStudentID" />
    <input type="hidden" runat="server" id="hdnSelectedValue" />
    <input type="hidden" runat="server" id="hdnSaveValue" />
    <table>
        <tr>
            <td class="tdLabel" style="width:200px;"><%=GetLabel("Site") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodClassType" ClientInstanceName="tacPeriodClassType" MethodName="GetvPeriodClassTypeList" GetFilterExpressionFunction="onGetPeriodClassTypeFilterExpression"
                    SearchFields="CurriculumClassTypeName,CurriculumClassTypeCode" TextField="CurriculumClassTypeName" ValueField="PeriodClassTypeID" SearchText="${CurriculumClassTypeName} (<b>${CurriculumClassTypeCode}</b>)" OrderByExpression="CurriculumClassTypeName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodClassTypeButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodClassTypeValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran Berikutnya") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacNextSchoolPeriod" ClientInstanceName="tacNextSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacNextSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacNextSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tipe Siswa") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboStudentType" ClientInstanceName="cboStudentType" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboStudentTypeValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />

    <table id="tblView1" rules="all" class="grdSelected grdBorder" style="display:none">
        <thead>
            <tr>
                <th style="width:40px" class="thCenter" rowspan="2"><asp:CheckBox runat="server" ID="chkSelectAll" CssClass="chkSelectAll" /></th>
                <th rowspan="2" style="width:40px">&nbsp;</th>
                <th style="width:100px" rowspan="2"><%=GetLabel("NIS")%></th>        
                <th rowspan="2"><%=GetLabel("Nama Siswa")%></th>        
                <asp:Repeater ID="rptStudentFeeCompTypeView" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" colspan="2"><%#Eval("StudentFeeCompTypeName")%></th>
                    </ItemTemplate>
                </asp:Repeater>      
                <th style="width:120px" rowspan="2"><%=GetLabel("Status")%></th>
            </tr>
            <tr> 
                <asp:Repeater ID="rptStudentFeeCompTypeView2" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Lama") %></th>
                        <th class="thCenter" style="width:80px">
                            <%=GetLabel("Baru") %><br />
                            <input type="text" class="txtCurrency txtNewValue" value="0" style="width:80px" /><br />
                            ↑ <input type="text" class="number txtMarkupPercentageValue" value="0" style="width:40px" /> [%]<br />
                            ↑ <input type="text" class="txtCurrency txtMarkupValue" value="0" style="width:60px" /><br />
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </thead>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblView" rules="all" class="tblView grdSelected grdBorder">
                                    <thead>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="trDt">
                                    <td class="keyField"><%#Eval("StudentID") %></td>
                                    <td align="center">    
                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                        <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                    </td>
                                    <td align="center">
                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                        <div class="gridCircle divStudentImage"></div>
                                    </td>
                                    <td><%#Eval("StudentCode") %></td>
                                    <td><%#Eval("StudentName") %></td>                                    
                                    <asp:Repeater ID="rptViewDt" runat="server" OnItemDataBound="rptViewDt_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="center">
                                                <asp:TextBox ID="txtOldCompValue" CssClass="txtOldCompValue txtCurrency" ReadOnly="true" runat="server" Width="100%" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtNewCompValue" CssClass="txtNewCompValue txtCurrency" runat="server" Width="100%" />
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>  
                                    <td><%#Eval("ClassStudentStatus")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>                                
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>