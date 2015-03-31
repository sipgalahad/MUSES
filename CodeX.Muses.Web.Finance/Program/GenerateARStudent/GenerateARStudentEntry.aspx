<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="GenerateARStudentEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.GenerateARStudentEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnGenerate" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Generate")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            setStudentImage();

            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnGenerate.ClientID %>').click(function () {
                var param = "";
                $('.chkIsSelected input:checked').each(function () {
                    var id = $(this).closest('tr').find('.keyField').html();
                    if (param != '')
                        param += ',';
                    param += id;
                });
                if (param == "")
                    showToast('Warning', 'Silakan Pilih Siswa Terlebih Dahulu');
                else {
                    $('#<%=hdnSelectedValue.ClientID %>').val(param);
                    onCustomButtonClick('save');
                }

            });
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

        function onAfterCustomClickSuccess() {
            cbpView.PerformCallback('refresh');            
        }

        $('.lblDetail').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + '|' + cboMonth.GetValue() + '|' + cboYear.GetValue();
            var url = ResolveUrl("~/Program/GenerateARStudent/GenerateARStudentDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Transaksi', 800, 550);
        });

        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
        }

        //#region Period Section
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND <%=OnGetPeriodSectionFilterExpression() %>";
            return filterExpression;
        }

        function onTacPeriodSectionButtonSearchClick() {
            openSearchDialog('periodsection', onGetPeriodSectionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodSectionFilterExpression() + " AND PeriodSectionCode = '" + value + "'";
                Methods.getObject('GetPeriodSectionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodSection.setValue(result.PeriodSectionID);
                        tacPeriodSection.setText(result.PeriodSectionName);
                    }
                    else {
                        tacPeriodSection.setValue('');
                        tacPeriodSection.setText('');
                    }
                    onTacPeriodSectionValueChanged();
                });
            });

        }

        function onTacPeriodSectionValueChanged() {
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
        }
        //#endregion

        $('.chkIsSelected input').live('change', function () {
            $('.chkSelectAll input').prop('checked', false);
        });

        $('.chkSelectAll input').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
        });
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
    </style>
    <input type="hidden" id="hdnSelectedValue" runat="server" />
    <table>
        <colgroup>
            <col style="width:150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                    SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Bulan") %></td>
            <td style="padding-right: 1px;">
                <table cellpadding="0" cellspacing="0" >
                    <colgroup>
                        <col width="120px" />
                        <col width="70px" />
                        <col width="120px" />
                    </colgroup>
                    <tr>
                        <td class="tdMonth"><dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" Width="120px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tahun")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboYear" runat="server" ClientInstanceName="cboYear" Width="120px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
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
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="StudentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="40px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsSelected" CssClass="chkIsSelected" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                        <div class="gridCircle divStudentImage"></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StudentCode" HeaderText="NIS" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="StudentName" HeaderText="Nama Siswa" />
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" HeaderText="Total Transaksi" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <label runat="server" id="lblStudentAmount" class="lblLink lblDetail"></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("Data Tidak Tersedia")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
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
