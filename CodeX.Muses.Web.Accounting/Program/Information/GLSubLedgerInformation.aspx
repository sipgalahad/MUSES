<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="GLSubLedgerInformation.aspx.cs" Inherits="CodeX.Web.Accounting.Program.GLSubLedgerInformation" %>

<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnProcess').click(function () {
                cbpView.PerformCallback('refresh');
            });

            //#region GLAccount
            function onGetGLAccountFilterExpression() {
                var filterExpression = "IsHeader = 0 AND IsDeleted = 0 AND SubLedgerID IS NOT NULL";
                return filterExpression;
            }

            $('#lblGLAccount.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtGLAccountNo.ClientID %>').val(value);
                    onTxtGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtGLAccountNo.ClientID %>').change(function () {
                onTxtGLAccountCodeChanged($(this).val());
            });

            function onTxtGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnGLAccountID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtGLAccountName.ClientID %>').val(result.GLAccountName);
                        $('#<%=hdnGLAccountSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnGLAccountSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnGLAccountIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnGLAccountCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnGLAccountDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnGLAccountMethodName.ClientID %>').val(result.MethodName);
                        $('#<%=hdnGLAccountFilterExpression.ClientID %>').val(result.FilterExpression);
                    }
                    else {
                        $('#<%=hdnGLAccountID.ClientID %>').val('');
                        $('#<%=txtGLAccountName.ClientID %>').val('');
                        $('#<%=hdnGLAccountSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnGLAccountSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnGLAccountIDFieldName.ClientID %>').val('');
                        $('#<%=hdnGLAccountCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnGLAccountDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnGLAccountMethodName.ClientID %>').val('');
                        $('#<%=hdnGLAccountFilterExpression.ClientID %>').val('');
                    }
                });
            }
            //#endregion
        });

        $('.lblAccountNo').live('click', function () {
            $tr = $(this).closest('tr');
            var subLedgerDtID = $tr.find('.keyField').html();
            var glAccountID = $('#<%=hdnGLAccountID.ClientID %>').val();
            var url = ResolveUrl('~/Program/Information/GLSubLedgerInformationCtl.ascx');
            var code = $(this).html();
            var name = $tr.find('.lblGLAccountName').html();
            var period = cboYear.GetValue() + '|' + cboMonth.GetValue();
            var param = glAccountID + '|' + subLedgerDtID + '|' + period + '|' + code + '|' + name;
            openUserControlPopup(url, param, 'Detail', 900, 600);
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion
    </script>
    <div>
        <input type="hidden" runat="server" id="hdnID" />
        <table width="100%">
            <colgroup>
                <col width="120px" />
                <col />
            </colgroup>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="tdLabel" style="width: 100px;"><label class="lblLink" id="lblGLAccount"><%=GetLabel("Perkiraan")%></label></td>
                            <td colspan="2">
                                <input type="hidden" id="hdnGLAccountID" runat="server" />
                                <input type="hidden" id="hdnGLAccountSubLedgerID" runat="server" />
                                <input type="hidden" id="hdnGLAccountSearchDialogTypeName" runat="server" />
                                <input type="hidden" id="hdnGLAccountIDFieldName" runat="server" />
                                <input type="hidden" id="hdnGLAccountCodeFieldName" runat="server" />
                                <input type="hidden" id="hdnGLAccountDisplayFieldName" runat="server" />
                                <input type="hidden" id="hdnGLAccountMethodName" runat="server" />
                                <input type="hidden" id="hdnGLAccountFilterExpression" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox runat="server" ID="txtGLAccountNo" Width="100%" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtGLAccountName" Width="100%" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trPeriode" runat="server">
                            <td class="tdLabel">
                                <label class="tdLabel">
                                <%=GetLabel("Periode")%></label>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboYear" Width="120px" ClientInstanceName="cboYear" runat="server" />
                                <input type="hidden" id="hdnSelectedYear" runat="server" />
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" />
                                <input type="hidden" value="" id="hdnSelectedMonth" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"><input type="button" id="btnProcess" value="Process" /></td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView" ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" >
                                        <Columns>
                                            <asp:BoundField DataField="SubLedgerDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderStyle-Width="120px" >
                                                <HeaderTemplate>
                                                    <div style="padding-left:3px">
                                                        <%=GetLabel("Kode")%>
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <label class="lblLink lblAccountNo" ><%# Eval("SubLedgerDtCode")%></label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SubLedgerDtName" ItemStyle-HorizontalAlign="Left" HeaderText="Name" ItemStyle-CssClass="lblGLAccountName" />
                                            <asp:BoundField DataField="BalanceBEGIN" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="Saldo Awal" />
                                            <asp:BoundField DataField="BalanceDEBIT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="Debit" />
                                            <asp:BoundField DataField="BalanceCREDIT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="Kredit" />
                                            <asp:BoundField DataField="BalanceEND" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="Saldo Akhir" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                    <div class="imgLoadingGrdView" id="containerImgLoadingView">
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                    <div class="containerPaging">
                        <div class="divInformationNumEntries" id="informationNumEntries"></div>
                        <div class="wrapperPaging">
                            <div id="paging">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
