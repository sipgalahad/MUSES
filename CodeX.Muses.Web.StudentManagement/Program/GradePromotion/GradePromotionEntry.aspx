<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="GradePromotionEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.GradePromotionEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnPromote" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Naik")%></div></li>
    <li id="btnReject" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Tidak Naik")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnPromote.ClientID %>').click(function () {
                var param = "";
                var lstStudentID = '';
                $('.chkIsSelected input:checked').each(function () {
                    $tr = $(this).closest('tr');
                    var id = $tr.find('.keyField').html();
                    if (param != '') {
                        param += '|';
                        lstStudentID += ',';
                    }
                    var idx = $tr.find('.hdnItemIndex').val();

                    var GCMajor = '';
                    var cboGCMajor = eval('cboGCMajor' + idx);
                    if (cboGCMajor.GetValue() != null && cboGCMajor.GetValue() != '')
                        GCMajor = cboGCMajor.GetValue();

                    param += id + ';' + GCMajor;
                    lstStudentID += id;
                });
                $('#<%=hdnSelectedValue.ClientID %>').val(param);
                $('#<%=hdnLstStudentID.ClientID %>').val(lstStudentID);
                cbpProcess.PerformCallback('promote');
            })

            $('#<%=btnReject.ClientID %>').click(function () {
                var param = "";
                $('.chkIsSelected input:checked').each(function () {
                    var id = $(this).closest('tr').find('.keyField').html();

                    if (param != '') {
                        param += ',';
                    }
                    param += id;
                });
                $('#<%=hdnLstStudentID.ClientID %>').val(param);
                
                cbpProcess.PerformCallback('reject');
            })

            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            })

            setStudentImage();
        });

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
            $('.grdStudent').width(parseInt(s.cpTableWidth));
            setStudentImage();
            hideLoadingPanel();
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
                onTacSchoolPeriodValueChanged();
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
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpSubject.PerformCallback('refresh');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            if (tacSchoolPeriod.getValue() != "") {
                var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
                return filterExpression;
            }
            return "1 = 0";
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                        entityToControlClass(result);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                        entityToControlClass(null);
                    }
                });
            });

        }

        function onTacClassValueChanged() {
            var schoolClassID = tacSchoolClass.getValue();
            if (schoolClassID != '') {
                var filterExpression = "SchoolClassID = " + schoolClassID;
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    entityToControlClass(result);
                });
            }
        }

        function entityToControlClass(entity) {
            if (entity != null) {
                $('#<%=hdnGCMajor.ClientID %>').val(entity.GCMajor);
                $('#<%=hdnGCGrade.ClientID %>').val(entity.GCGrade);
                $('#<%=hdnNextGCGrade.ClientID %>').val(entity.NextGCGrade);
                $('#<%=hdnNextGrade.ClientID %>').val(entity.NextGrade);
                $('#<%=hdnGradePromotionFormulaID.ClientID %>').val(entity.GradePromotionFormulaID); 
            }
            else {
                $('#<%=hdnGCMajor.ClientID %>').val('');
                $('#<%=hdnGCGrade.ClientID %>').val('');
                $('#<%=hdnNextGCGrade.ClientID %>').val('');
                $('#<%=hdnNextGrade.ClientID %>').val('');
                $('#<%=hdnGradePromotionFormulaID.ClientID %>').val(''); 
            }
            setTimeout(function () {
                cbpSubject.PerformCallback('refresh');
            }, 100);
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
        }
        //#endregion

        $('.chkSelectAll input').live('change', function () {
            var isChecked = $(this).is(':checked');
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
            });
        });

        $('.chkSelectAllSubject input').live('change', function () {
            var isChecked = $(this).is(':checked');
            $('.chkSubject input').each(function () {
                $(this).prop('checked', isChecked);
            });
            setDdeSubjectText();
        });

        $('.chkSubject input').live('change', function () {
            setDdeSubjectText();
        });

        function setDdeSubjectText() {
            var lstID = '';
            var lstName = '';
            $('.chkSubject input:checked').each(function () {
                if (lstName != '') {
                    lstName += ', ';
                    lstID += ',';
                }
                lstID += $(this).parent().attr('id');
                lstName += $(this).parent().attr('name');
            });
            $('#<%=hdnLstSubjectID.ClientID %>').val(lstID);
            ddeSubject.SetText(lstName);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
        .belowpassinggrade                  { color: Red !important; }
    </style>
    <input type="hidden" runat="server" id="hdnSelectedValue" />
    <input type="hidden" runat="server" id="hdnLstStudentID" />
    <input type="hidden" runat="server" id="hdnLstSubjectID" />
    <table>
        <tr>
            <td class="tdLabel" style="width:150px;"><%=GetLabel("Site") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <input type="hidden" id="hdnGCGrade" runat="server" />
                <input type="hidden" id="hdnNextGCGrade" runat="server" />
                <input type="hidden" id="hdnNextGrade" runat="server" />
                <input type="hidden" id="hdnGCMajor" runat="server" />
                <input type="hidden" id="hdnGradePromotionFormulaID" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td>
                <dxe:ASPxDropDownEdit ClientInstanceName="ddeSubject" ID="ddeSubject"
                    Width="300px" runat="server" EnableAnimation="False">
                    <DropDownWindowStyle BackColor="#EDEDED" />
                    <DropDownWindowTemplate>
                        <dxcp:ASPxCallbackPanel ID="cbpSubject" runat="server" Width="100%" ClientInstanceName="cbpSubject"
                            ShowLoadingPanel="false" OnCallback="cbpSubject_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ hideLoadingPanel(); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="max-height:200px">
                                        <asp:GridView ID="grdSubject" runat="server" CssClass="grdBorder grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdSubject_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="30px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAllSubject" CssClass="chkSelectAllSubject" runat="server"  />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSubject" CssClass="chkSubject" runat="server"  /> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ClassSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" />
                                                <asp:BoundField DataField="CurriculumSubjectGroupName" HeaderText="Tipe Pelajaran" HeaderStyle-Width="120px" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <%=GetLabel("No Data To Display")%>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </DropDownWindowTemplate>
                </dxe:ASPxDropDownEdit>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"></td>
            <td><asp:CheckBox ID="chkIsOnlyFinalMark" runat="server" /> <%=GetLabel("Tampilkan Hanya NPK") %></td>
        </tr>
        <tr>
            <td class="tdLabel"></td>
            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
        </tr>
    </table>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <div style="width:1320px; height:300px; overflow: auto;" id="divContainerTable" runat="server">
                            <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent" id="tblView">
                                <tr>
                                    <th rowspan="3" style="width:40px" class="thCenter">
                                        <asp:CheckBox ID="chkSelectAll" CssClass="chkSelectAll" runat="server"  />
                                    </th>
                                    <th rowspan="3" colspan="2"><%=GetLabel("Siswa") %></th>
                                    <asp:Repeater ID="rptColHeaderLevel1" runat="server" OnItemDataBound="rptColHeaderLevel1_ItemDataBound">
                                        <ItemTemplate>
                                            <th class="thCenter" style="width:90px" id="tdSubjectName" runat="server"><%#Eval("SubjectName") %></th>    
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th rowspan="3" style="width:100px"><%=GetLabel("Naik Ke Kelas") %></th>
                                    <th rowspan="3" style="width:120px"><%=GetLabel("Jurusan") %></th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel2" runat="server" OnItemDataBound="rptColHeaderLevel2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptColHeaderLevel2Dt" runat="server" OnItemDataBound="rptColHeaderLevel2Dt_ItemDataBound">
                                                <ItemTemplate>
                                                    <th class="thCenter" id="tdPeriodSection" runat="server"><%#Eval("PeriodSection") %></th>  
                                                </ItemTemplate>
                                            </asp:Repeater>        
                                            <th class="thCenter" rowspan="2" style="width:60px" id="tdFinalMark" runat="server"><%=GetLabel("NPK") %></th>  
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel3" runat="server" OnItemDataBound="rptColHeaderLevel3_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptColHeaderLevel3Dt" runat="server" OnItemDataBound="rptColHeaderLevel3Dt_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Repeater ID="rptColHeaderLevel3Dt2" runat="server">
                                                        <ItemTemplate>
                                                            <th class="thCenter" style="width:90px"><%#Eval("CurriculumMarkTypeName") %></th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%#Eval("StudentID")%></td>
                                            <td align="center"><asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" /></td>
                                            <td style="width:40px" align="center">
                                                <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                <div class="gridCircle divStudentImage"></div>
                                            </td>
                                            <td><%#Eval("StudentName") %></td>
                                            <asp:Repeater ID="rptStudentSubject" runat="server" OnItemDataBound="rptStudentSubject_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Repeater ID="rptStudentSubjectPeriodSection" runat="server" OnItemDataBound="rptStudentSubjectPeriodSection_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:Repeater ID="rptStudentSubjectMarkType" runat="server" OnItemDataBound="rptStudentSubjectMarkType_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td id="tdStudentMark" runat="server" style="width:60px" align="center"></td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td align="center" id="tdFinalMark" runat="server"></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td><div runat="server" id="divNextGrade"></div></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                <dxe:ASPxComboBox ID="cboGCMajor" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
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
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>