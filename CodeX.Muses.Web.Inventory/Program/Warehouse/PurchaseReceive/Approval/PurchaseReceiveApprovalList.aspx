<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="PurchaseReceiveApprovalList.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseReceiveApprovalList" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnPurchaseReceiveHdApprove" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Approve")%></div></li>
    <li id="btnPurchaseReceiveHdDecline" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/redo.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Decline")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#<%=btnPurchaseReceiveHdApprove.ClientID %>').click(function () {
                if ($('#<%=hdnID.ClientID %>').val() == ''')
                    showToast('Warning', 'Silakan Pilih No Penerimaan Terlebih Dahulu');
                else 
                    onCustomButtonClick('approve');
            });

            $('#<%=btnPurchaseReceiveHdDecline.ClientID %>').click(function () {
                if ($('#<%=hdnID.ClientID %>').val() == ''')
                    showToast('Warning', 'Silakan Pilih No Penerimaan Terlebih Dahulu');
                else 
                    onCustomButtonClick('decline');
            });
        });

        function onAfterCustomClickSuccess(type) {
            cbpView.PerformCallback('refresh');
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });
        });

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
        }
        //#endregion

        $('.lnkPurchaseReceive a').live('click', function () {
            $tr = $(this).closest('tr');
            var param = $tr.find('.keyField').html();
            var transactionStatus = $tr.find('.hdnGCTransactionStatus').val();
            var url = ResolveUrl("~/Program/Warehouse/PurchaseReceive/Confirmation/PurchaseReceiveConfirmationDtCtl.ascx");
            openUserControlPopup(url, param, 'Detil Penerimaan Pembelian', 1000, 600);
        });
    </script>
    <input type="hidden" value="" id="hdnIsDiscountAppliedToUnitPrice" runat="server" />
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
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PurchaseReceiveID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:HyperLinkField HeaderText="No. Penerimaan" DataTextField="PurchaseReceiveNo" ItemStyle-CssClass="lnkPurchaseReceive" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="ReceivedDateInString" HeaderText="Tanggal Penerimaan" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="SupplierName" HeaderText="Nama Supplier" HeaderStyle-Width="250px" />
                                <asp:BoundField DataField="ReferenceNo" HeaderText="No Faktur" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="PaymentDueDateInString" HeaderText="Tanggal Jatuh Tempo" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="TotalNetTransactionAmount" HeaderText="Jumlah Transaksi" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("No Data To Display")%>
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
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>