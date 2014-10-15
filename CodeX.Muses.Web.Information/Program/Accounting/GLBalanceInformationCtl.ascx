<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GLBalanceInformationCtl.ascx.cs" 
    Inherits="CodeX.Web.Accounting.Program.GLBalanceInformationCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_drugslogisticsquickpicksctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            cbpViewPopup.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
        });
    });

    function onCbpEntryPopupViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpViewPopup.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion
</script>
<div style="padding:10px;">
    <input type="hidden" id="hdnGLAccountID" runat="server" />
    <input type="hidden" id="hdnYear" runat="server" />
    <input type="hidden" id="hdnMonth" runat="server" />
    <table width="100%">
        <colgroup>
            <col width="120px" />
            <col width="150px" />
            <col />
        </colgroup>
        <tr>
            <td class="tdlabel"><label><%=GetLabel("Perkiraan") %></label></td>
            <td><asp:TextBox runat="server" ReadOnly="true" ID="txtGLAccountNo" Width="100%" /></td>
            <td><asp:TextBox runat="server" ReadOnly="true" ID="txtGLAccountName" Width="100%" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup" ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                position: relative; max-height: 420px; overflow-y: scroll">
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" >
                                    <Columns>
                                        <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="JournalNo" ItemStyle-HorizontalAlign="Left" HeaderText="Nomor Voucher" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="JournalDateInString" ItemStyle-HorizontalAlign="Center" HeaderText="Tanggal" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="100px"  />
                                        <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderText="Catatan" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="DEBITAmount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="DEBIT" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="CREDITAmount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="KREDIT" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="BalanceEND" ItemStyle-HorizontalAlign="Right" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}" HeaderStyle-Width="100px" HeaderText="SALDO" HeaderStyle-CssClass="thRight"/>
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
                    <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
                    <div class="wrapperPaging">
                        <div id="pagingPopup">
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>