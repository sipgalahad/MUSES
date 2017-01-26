<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseRequestQtyOnOrderCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseRequestQtyOnOrderCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_purchaserequestqtyonorderctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            cbpEntryPopupView.PerformCallback('changepage|' + page);
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
                cbpEntryPopupView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto;overflow-x: hidden">
    <input type="hidden" id="hdnItemID" runat="server" />
    <input type="hidden" id="hdnSiteServiceUnitID" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:160px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Bagian")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtServiceUnit" ReadOnly="true" Width="400px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Item")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtItem" ReadOnly="true" Width="400px" runat="server" /></td>
                    </tr>  
                </table>

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="PurchaseRequestNo" HeaderText="No Permintaan" HeaderStyle-Width="180px" />
                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="No Pemesanan" HeaderStyle-Width="180px" />
                                        <asp:BoundField DataField="TransactionStatus" HeaderText="Status PO" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="CustomPurchaseUnit" HeaderText="Diminta" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="CustomReceived" HeaderText="Diterima" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" />
                                        <asp:BoundField DataField="CustomPurchaseRequest" HeaderText="Total Diminta" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
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
                        <div id="pagingPopup"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>
</div>