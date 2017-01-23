<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderOutstandingDtCtl.ascx.cs"
    Inherits="CodeX.Ottimo.Web.Inventory.Program.PurchaseOrderOutstandingDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#containerPopup .txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            cbpView.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
        });
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCountPopup = parseInt(param[1]);
            var rowCountPopup = parseInt(param[2]);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
            setPaging($("#pagingPopup"), pageCountPopup, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
            });
        }
    }
    //#endregion
</script>
<input type="hidden" id="hdnPurchaseReceiveID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />

<div style="max-height: 500px; overflow-y: auto" id="containerPopup">
    <input type="hidden" value="" id="hdnParam" runat="server" />
    <input type="hidden" value="" id="hdnOrderID" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />

    <style type="text/css">
        .trOutstanding td       { background-color: #EB6A7D; }
    </style>
    <div style="overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 50%" />
                <col style="width: 50%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" id="lblOrderNo"><%=GetLabel("No. Pemesanan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Order") %></td>
                            <td><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Pengiriman") %></td>
                            <td><asp:TextBox ID="txtDeliveryDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Expired") %></td>
                            <td><asp:TextBox ID="txtExpiredDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Dari Bagian") %></td>
                            <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Ke Bagian") %></td>
                            <td><asp:TextBox ID="txtToServiceUnitName" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Jenis Persediaan") %></td>
                            <td><asp:TextBox ID="txtPurchaseOrderType" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Supplier") %></td>
                            <td><asp:TextBox ID="txtSupplierName" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Waktu Pembayaran") %></td>
                            <td><asp:TextBox ID="txtTermCondition" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tipe Franco") %></td>
                            <td><asp:TextBox ID="txtFrancoRegion" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdLabel"><%=GetLabel("Mata Uang") %></td>
                            <td><asp:TextBox ID="txtCurrencyCode" Width="100%" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs") %></td>
                            <td><asp:TextBox ID="txtCurrencyRate" CssClass="number" Width="80px" runat="server" ReadOnly="true"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdViewPopup" runat="server" CssClass="tblTransactionEntryResult" OnRowDataBound="grdViewPopup_RowDataBound"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src='<%# ResolveUrl("~/Libs/Images/Button/verify.png") %>' <%#Eval("IsReceived").ToString() == "True" ? "" : "Style ='display:none'" %> title='<%=GetLabel("Diterima") %>' alt="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName1" HeaderText="Item Name" HeaderStyle-Width="300px" />
                                            <asp:TemplateField HeaderText="Pesan" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("Quantity", "{0:N}")%></td>
                                                            <td style="width:50px; color: Red;"><%#Eval("PurchaseUnit") %></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terima" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("ReceivedQuantity", "{0:N}")%></td>
                                                            <td style="width:50px; color: Red;"><%#Eval("PurchaseUnit") %></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Harga / Satuan" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:90px" align="right"><%#Eval("UnitPrice", "{0:N}")%></td>
                                                            <td>/</td>
                                                            <td style="width:50px; color: Red;"><%#Eval("PurchaseUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="200px" />
                                            <asp:BoundField DataField="DiscountPercentage1" HeaderStyle-CssClass="thRight" HeaderText="Diskon 1 [%]" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="DiscountPercentage2" HeaderStyle-CssClass="thRight" HeaderText="Diskon 2 [%]" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="LineAmount" HeaderStyle-CssClass="thRight" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="130px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="OutstandingAmount" HeaderStyle-CssClass="thRight" HeaderText="Outstanding" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="130px" DataFormatString="{0:N}" />
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
            <tr>
                <td colspan="2">
                    <div id="containerTotalOrder" style="margin-top: 20px;">
                        <fieldset id="fsTotalOrder" style="margin: 0">
                            <table style="width: 100%;">
                                <colgroup>
                                    <col style="width: 50%" />
                                    <col style="width: 40px" />
                                </colgroup>
                                <tr>
                                    <td valign="top">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col style="width: 100px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;"><label class="lblNormal" id="lblPaymentRemarks"><%=GetLabel("Syarat Pembayaran")%></label></td>
                                                <td><asp:TextBox ID="txtPaymentRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" ReadOnly="true" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                                <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" ReadOnly="true" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td valign="top">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col style="width: 180px" />
                                                <col style="width: 50px" />
                                                <col style="width: 10px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Pemesanan")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtTransactionAmount" ReadOnly="true" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%> (<%=GetVATPercentageLabel()%>%)</label></td>
                                                <td>&nbsp;</td>
                                                <td align="right"><asp:CheckBox ID="chkPPN" Enabled="false" runat="server" /></td>
                                                <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server"/></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                                <td><asp:TextBox ID="txtFinalDiscountPercentage" ReadOnly="true" CssClass="number" Width="50px" runat="server" /></td>
                                                <td>[%]</td>
                                                <td><asp:TextBox ID="txtFinalDiscountAmount" ReadOnly="true" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Uang Muka")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtDP" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" hiddenVal="0"/></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Saldo Nilai Pemesanan")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtTotalNetTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>