<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PODPQtyOnOrderCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Inventory.Program.PODPQtyOnOrderCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_purchaserequestqtyonorderctl">
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
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <h4><%=GetLabel("Pembelian Kredit") %></h4>
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="No Pembelian" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="TransactionStatus" HeaderText="Status" HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier" HeaderStyle-Width="110px" />
                                        <asp:BoundField DataField="CustomPurchaseUnit" HeaderText="Dipesan" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="CustomUnitPrice" HeaderText="Harga / Satuan" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" />
                                        <asp:BoundField DataField="CustomTotalPurchaseUnit" HeaderText="Total Pesan" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField="LineAmount" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="90px" DataFormatString="{0:N}" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br />
                                <h4><%=GetLabel("Pembelian Tunai") %></h4>
                                <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="DirectPurchaseNo" HeaderText="No Pembelian" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="TransactionStatus" HeaderText="Status" HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField="BusinessPartnerName" HeaderText="Supplier" HeaderStyle-Width="110px" />
                                        <asp:BoundField DataField="CustomItemUnit" HeaderText="Dipesan" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
                                        <asp:BoundField DataField="CustomUnitPrice" HeaderText="Harga / Satuan" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" />
                                        <asp:BoundField DataField="CustomTotalPurchaseUnit" HeaderText="Total Pesan" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField="LineAmount" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="90px" DataFormatString="{0:N}" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</div>