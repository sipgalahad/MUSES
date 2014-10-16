<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="GLBalanceInformationPerAccount.aspx.cs" Inherits="CodeX.Web.Accounting.Program.GLBalanceInformationPerAccount" %>

<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnProcess').click(function () {
                cbpView.PerformCallback('refresh');
            });

            //#region GL Account 
            $('#lblGLAccount.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtGLAccountCode.ClientID %>').val(value);
                    onTxtGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtGLAccountCode.ClientID %>').change(function () {
                onTxtGLAccountCodeChanged($(this).val());
            });

            function onTxtGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnGLAccountID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnMethodName.ClientID %>').val(result.MethodName);
                        onSubLedgerIDChanged();
                    }
                    else {
                        $('#<%=hdnGLAccountID.ClientID %>').val('');
                        $('#<%=txtGLAccountCode.ClientID %>').val('');
                        $('#<%=txtGLAccountName.ClientID %>').val('');

                        $('#<%=hdnSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnFilterExpression.ClientID %>').val('');
                        $('#<%=hdnIDFieldName.ClientID %>').val('');
                        $('#<%=hdnCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnMethodName.ClientID %>').val('');
                    }

                    $('#<%=hdnSubLedgerDtID.ClientID %>').val('');
                    $('#<%=txtSubLedgerDtCode.ClientID %>').val('');
                    $('#<%=txtSubLedgerDtName.ClientID %>').val('');
                });
            }

            function onSubLedgerIDChanged() {
                if ($('#<%=hdnSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblSubLedgerDt.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSubLedgerDtCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblSubLedgerDt.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtSubLedgerDtCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region Sub Ledger 
            function onGetSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblSubLedgerDt.ClientID %>').click(function () {
                if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnSearchDialogTypeName.ClientID %>').val(), onGetSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtSubLedgerDtCode.ClientID %>').val(value);
                        onTxtSubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtSubLedgerDtCode.ClientID %>').change(function () {
                onTxtSubLedgerDtCodeChanged($(this).val());
            });

            function onTxtSubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnSubLedgerDtID.ClientID %>').val(result[$('#<%=hdnIDFieldName.ClientID %>').val()]);
                            $('#<%=txtSubLedgerDtName.ClientID %>').val(result[$('#<%=hdnDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnSubLedgerDtID.ClientID %>').val('');
                            $('#<%=txtSubLedgerDtCode.ClientID %>').val('');
                            $('#<%=txtSubLedgerDtName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion
        });

        function onGetGLAccountFilterExpression() {
            var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
            return filterExpression;
        }

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
                <td class="tdLabel"><label class="lblLink" id="lblGLAccount"><%=GetLabel("Perkiraan")%></label></td>
                <td>
                    <input type="hidden" id="hdnGLAccountID" runat="server" />
                    <input type="hidden" id="hdnSubLedgerID" runat="server" />
                    <input type="hidden" id="hdnSearchDialogTypeName" runat="server" />
                    <input type="hidden" id="hdnIDFieldName" runat="server" />
                    <input type="hidden" id="hdnCodeFieldName" runat="server" />
                    <input type="hidden" id="hdnDisplayFieldName" runat="server" />
                    <input type="hidden" id="hdnMethodName" runat="server" />
                    <input type="hidden" id="hdnFilterExpression" runat="server" />
                    <table cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col style="width:30%"/>
                            <col style="width:3px"/>
                            <col/>
                        </colgroup>
                        <tr>
                            <td><asp:TextBox runat="server" ID="txtGLAccountCode" Width="120px" /></td>
                            <td>&nbsp;</td>
                            <td><asp:TextBox runat="server" ID="txtGLAccountName" Width="220px" ReadOnly="true" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblSubLedgerDt"><%=GetLabel("Sub Perkiraan")%></label></td>
                    <td>
                        <input type="hidden" id="hdnSubLedgerDtID" runat="server" />
                        <table cellpadding="0" cellspacing="0">
                            <colgroup>
                                <col style="width:30%"/>
                                <col style="width:3px"/>
                                <col/>
                            </colgroup>
                            <tr>
                                <td><asp:TextBox runat="server" ID="txtSubLedgerDtCode" Width="120px" /></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox runat="server" ID="txtSubLedgerDtName" Width="220px" ReadOnly="true"  /></td>
                            </tr>
                        </table>
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <colgroup>
                            <col width="120px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="tdLabel"><%=GetLabel("Periode")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboYear" Width="120px" ClientInstanceName="cboYear" runat="server" /></td>
                            <td><dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"><input type="button" id="btnProcess" value="Process" /></td>
            </tr>
            <tr>
                <td colspan="2">
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
                                            <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="JournalNo" ItemStyle-HorizontalAlign="Left" HeaderText="No. Voucher" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="JournalDateInString" ItemStyle-HorizontalAlign="Center" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" />
                                            <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderText="Catatan" />
                                            <asp:BoundField DataField="DEBITAmount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="DEBIT" HeaderStyle-CssClass="thRight"  />
                                            <asp:BoundField DataField="CREDITAmount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="KREDIT" HeaderStyle-CssClass="thRight" />
                                            <asp:BoundField DataField="BalanceEND" ItemStyle-HorizontalAlign="Right" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}" HeaderStyle-Width="100px" HeaderText="SALDO" HeaderStyle-CssClass="thRight" />
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
