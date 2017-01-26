<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="DirectPurchaseConfirmationList.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseConfirmationList" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnProcess" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Proses")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#<%=btnProcess.ClientID %>').click(function () {
                onCustomButtonClick('process');
            });
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onRefreshGridView() {
            cbpView.PerformCallback('refresh');
        }

        function onAfterCustomClickSuccess(type) {
            cbpView.PerformCallback('refresh');
        }

        function onAfterSaveEditRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
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

        $('.lnkDirectPurchase a').live('click', function () {
            $tr = $(this).closest('tr');
            var param = $tr.find('.keyField').html();
            var transactionStatus = $tr.find('.hdnGCTransactionStatus').val();
            var url = "";
            if (transactionStatus == '<%=OnGetTransactionStatusApproved() %>')
                url = ResolveUrl("~/Program/Procurement/DirectPurchase/Confirmation/DirectPurchaseConfirmationEditDtCtl.ascx");
            else
                url = ResolveUrl("~/Program/Procurement/DirectPurchase/Confirmation/DirectPurchaseConfirmationDtCtl.ascx");
            openUserControlPopup(url, param, 'Konfirmasi Pembelian Tunai', 1000, 600);
        });
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnIsVATAppliedToAveragePrice" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <table class="tblEntryContent">
        <colgroup>
            <col style="width:150px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tampilan")%></label></td>
            <td>
                <dxe:ASPxComboBox ID="cboViewType" ClientInstanceName="cboViewType" Width="150px" runat="server">
                    <ClientSideEvents ValueChanged="function(s,e) { onRefreshGridView(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
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
                                <asp:BoundField DataField="DirectPurchaseID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderText="No. Pembelian" ItemStyle-CssClass="lnkDirectPurchase" HeaderStyle-Width="150px" >
                                    <ItemTemplate>
                                        <input type="hidden" class="hdnGCTransactionStatus" value='<%#Eval("GCTransactionStatus") %>' />
                                        <a><%#Eval("DirectPurchaseNo")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PurchaseDateInString" HeaderText="Tanggal Pembelian" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="BusinessPartnerName" HeaderText="Nama Supplier" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="ReferenceNo" HeaderText="No Referensi" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="ServiceUnitName" HeaderText="Dari Bagian" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="ToServiceUnitName" HeaderText="Ke Bagian" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="LocationName" HeaderText="Lokasi" HeaderStyle-Width="220px" />
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
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>