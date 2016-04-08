<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="StudentFeePenaltyEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.StudentFeePenaltyEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Simpan")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#btnRefresh').click(function () {
                onRefreshGridView();
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                getCheckedMember();
                onCustomButtonClick('save');
            });

            getInitCheckedData();
        });

        function getCheckedMember() {
            var param = "";
            var lstStudentFeeID = '';
            var lstStudentID = '';
            $('.chkIsSelected input:checked').each(function () {
                $tr = $(this).closest('tr');
                var id = $tr.find('.keyField').html();
                var penaltyPercentage = $tr.find('.hdnPenaltyPercentage').val();
                var studentID = $tr.find('.hdnStudentID').val();
                if (param != '') {
                    param += '|';
                    lstStudentFeeID += ',';
                    lstStudentID += ',';
                }
                param += id + ';' + penaltyPercentage;
                lstStudentFeeID += id;
                lstStudentID += studentID;
            });
            $('#<%=hdnListStudentFeeID.ClientID %>').val(lstStudentFeeID);
            $('#<%=hdnListSaveValue.ClientID %>').val(param);
            $('#<%=hdnListStudentID.ClientID %>').val(lstStudentID);
        }

        function getInitCheckedData() {
            var lstStudentFeeID = '';
            $('.chkIsSelected input:checked').each(function () {
                $tr = $(this).closest('tr');
                var id = $tr.find('.keyField').html();
                if (lstStudentFeeID != '')
                    lstStudentFeeID += ',';
                lstStudentFeeID += id;
            });
            $('#<%=hdnOldListStudentFeeID.ClientID %>').val(lstStudentFeeID);
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            getInitCheckedData();
        }
        //#endregion

        $('.chkSelectAll input').live('click', function () {
            var value = $(this).is(':checked');
            $('#<%=grdView.ClientID %> .chkIsSelected input').each(function () {
                $(this).prop("checked", value);
                $(this).change();
            });
        });

        $('.chkIsSelected input').live('change', function () {
            var isChecked = $(this).is(':checked');
            $tr = $(this).closest('tr');
            var studentAmount = parseFloat($tr.find('.hdnStudentAmount').val());
            var penaltyPercentage = parseFloat($tr.find('.hdnPenaltyPercentage').val());
            var totalPenaltyAmount = 0;
            if (isChecked)
                totalPenaltyAmount = studentAmount * penaltyPercentage / 100;
            var totalStudentAmount = studentAmount + totalPenaltyAmount;

            $tr.find('.hdnTotalStudentPenaltyAmount').val(totalPenaltyAmount);
            $tr.find('.tdTotalStudentPenaltyAmount').html(totalPenaltyAmount.formatMoney(2, '.', ','));
            $tr.find('.divTotalStudentAmount').html(totalStudentAmount.formatMoney(2, '.', ','));
            $tr.find('.hdnTotalStudentAmount').val(totalStudentAmount);
        });

        function onCboSchoolPeriodValueChanged(s) {
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
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
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
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
            });
        }

        function onRefreshGridView() {
            $('#<%=hdnFilterExpressionQuickSearch.ClientID %>').val(txtSearchView.GenerateFilterExpression());
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
                setTimeout(function () {
                    s.SetFocus();
                }, 0);
            }, 0);
        }

    </script>
    <div>
        <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
        <input type="hidden" id="hdnOldListStudentFeeID" runat="server" />
        <input type="hidden" id="hdnListStudentFeeID" runat="server" />
        <input type="hidden" id="hdnListSaveValue" runat="server" />
        <input type="hidden" id="hdnListStudentID" runat="server" />
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 120px" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
                <td>
                    <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                        <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetSchoolPeriodList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                        SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                            ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
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
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Search Filter")%></label></td>
                <td>
                    <cdx:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView"
	                    Width="300px" Watermark="Search">
	                    <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
	                    <IntellisenseHints>
		                    <cdx:QISIntellisenseHint Text="Name" FieldName="StudentName" />
		                    <cdx:QISIntellisenseHint Text="NBS" FieldName="VirtualAccountNo" />
	                    </IntellisenseHints>
                    </cdx:QISIntellisenseTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Bulan") %></td>
                <td style="padding-right: 1px; width: 140px">
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
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlGridView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:scroll;">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="StudentFeeID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />                              
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <div style="text-align:center">
                                        <asp:CheckBox runat="server" ID="chkSelectAll" CssClass="chkSelectAll" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsSelected" CssClass="chkIsSelected" />
                                </ItemTemplate>
                            </asp:TemplateField>                                          
                            <asp:BoundField DataField="VirtualAccountNo" HeaderText="NBS" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="StudentName" HeaderText="Nama Siswa" />
                            <asp:BoundField DataField="StudentAmount" DataFormatString="{0:N}" HeaderText="Uang Sekolah" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalStudentPenaltyAmount" DataFormatString="{0:N}" HeaderText="Denda" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="tdTotalStudentPenaltyAmount" />
                            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight">
                                <HeaderTemplate>
                                    <%=GetLabel("Total")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="hidden" class="hdnStudentID" value='<%#Eval("StudentID") %>' />
                                    <input type="hidden" id="hdnPenaltyPercentage" class="hdnPenaltyPercentage" runat="server" />
                                    <input type="hidden" class="hdnTotalStudentPenaltyAmount" value='<%#Eval("TotalStudentPenaltyAmount") %>' />
                                    <input type="hidden" class="hdnStudentAmount" value='<%#Eval("StudentAmount") %>' />
                                    <input type="hidden" class="hdnTotalStudentAmount" value='<%#Eval("TotalStudentAmount") %>' />
                                    <div class="divTotalStudentAmount"><%#Eval("TotalStudentAmount", "{0:N}") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <div class="containerPaging">
        <div class="wrapperPaging">
            <div id="paging"></div>
        </div>
    </div> 
</asp:Content>
