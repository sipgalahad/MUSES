<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="PurchaseOrderOutstandingList.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseOrderOutstandingList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnClosePO" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><div><%=GetLabel("Close")%></div></li>
    <li id="btnCloseNewPO" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><div><%=GetLabel("Close & New")%></div></li>
    <li id="btnCopyPO" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Copy")%></div></li>
    <li id="btnGeneratePO" runat="server" CRUDMode="R" title="Generate Dari Penerimaan Pembelian"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Generate")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#<%=btnClosePO.ClientID %>').click(function () {
                onCustomButtonClick('close');
            });

            $('#<%=btnGeneratePO.ClientID %>').click(function () {
                showToastConfirmation('PO Akan Ditutup. Akan Dibuat 1 nomor PO untuk barang yang sudah diterima dan 1 nomor PO untuk barang outstanding. Apakah anda Yakin?', function (result) {
                    if (result)
                        onCustomButtonClick('generate');
                });
            });

            $('#<%=btnCopyPO.ClientID %>').click(function () {
                var id = $('#<%=hdnID.ClientID %>').val();
                var url = ResolveUrl("~/Program/Procurement/PurchaseOrder/Outstanding/PurchaseOrderCopyCtl.ascx");
                openUserControlPopup(url, id, 'Copy PO', 600, 400);
            });

            $('#<%=btnCloseNewPO.ClientID %>').click(function () {
                var id = $('#<%=hdnID.ClientID %>').val();
                var url = ResolveUrl("~/Program/Procurement/PurchaseOrder/Outstanding/PurchaseOrderCloseNewCtl.ascx");
                openUserControlPopup(url, id, 'Close & New PO', 600, 400);
            });
        });

        function onAfterSaveAddRecordEntryPopup(param) {
            var tempText = "Pemesanan Barang Berhasil Dibuat Dengan Nomor <b>" + param + "</b>";
            showToast('Save Success', tempText, function () {
                cbpView.PerformCallback('refresh');
            });               
        }

        function onAfterCustomClickSuccess(type, param) {
            if (type == 'generate') {
                var temp = param.split('|');
                var tempText = "Pemesanan Barang Final Berhasil Dibuat Dengan Nomor <b>" + temp[0] + "</b><br/>Pemesanan Barang Outstanding Berhasil Dibuat Dengan Nomor <b>" + temp[1] + "</b>";
                showToast('Generate Success', tempText, function () {
                    cbpView.PerformCallback('refresh');
                });
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        $('.lnkPurchaseOrder a').live('click', function () {
            $tr = $(this).closest('tr');
            var param = $tr.find('.keyField').html();
            var url = ResolveUrl("~/Program/Procurement/PurchaseOrder/Outstanding/PurchaseOrderOutstandingDtCtl.ascx");
            openUserControlPopup(url, param, 'Detil Pemesanan Barang', 1200, 600);
        });
    </script>
    <input type="hidden" value="" id="hdnParam" runat="server" />
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnListSiteServiceUnitID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PurchaseOrderID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:HyperLinkField HeaderText="No. Pemesanan" DataTextField="PurchaseOrderNo" ItemStyle-CssClass="lnkPurchaseOrder" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="ServiceUnitName" HeaderText="Dari Bagian" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="ToServiceUnitName" HeaderText="Ke Bagian" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="BusinessPartnerName" HeaderText="Supplier" HeaderStyle-Width="220px" />
                                <asp:BoundField DataField="OrderDateInString" HeaderText="Tanggal Pemesanan" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="110px" />
                                <asp:BoundField DataField="DeliveryDateInString" HeaderText="Tanggal Pengiriman" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="110px" />
                                <asp:BoundField DataField="ExpiredDateInString" HeaderText="Tanggal Expired" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="110px" />
                                <asp:BoundField DataField="ReferencePurchaseOrderNo" HeaderText="No Referensi" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="TotalDtAmount" HeaderText="Jumlah Transaksi Outstanding" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
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
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>