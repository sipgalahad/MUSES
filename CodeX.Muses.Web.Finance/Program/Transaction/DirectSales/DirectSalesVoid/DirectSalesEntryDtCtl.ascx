<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSalesEntryDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Finance.Program.DirectSalesEntryDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    setDatePicker('<%=txtSalesUnitDate.ClientID %>');

    //#region Paging
    function onCbpPopupViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpPopupView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion

    $('.txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });
</script>
<input type="hidden" id="hdnSalesInvoiceID" runat="server" value="" />
<input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
<div style="max-height: 500px; overflow-y: auto" id="containerPopup">
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 150px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal" id="lblSalesInvoiceNo"><%=GetLabel("Nomor Faktur")%></label></td>
                        <td><asp:TextBox ID="txtSalesInvoiceNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                        <td><asp:TextBox ID="txtSalesUnitDate" Width="120px" ReadOnly="true" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Siswa")%></label></td>
                        <td colspan="3">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 120px" />
                                    <col style="width: 3px" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStudentCode" ReadOnly="true" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtStudentName" Width="100%" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:CheckBox ID="chkPPN" Enabled="false" Width="100%" runat="server" Text="PPN" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 25%" />
                        <col style="width: 25%" />
                        <col style="width: 25%" />
                        <col style="width: 25%" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal" runat="server" id="lblLocation"><%=GetLabel("Lokasi")%></label></td>
                        <td colspan="3">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 120px" />
                                    <col style="width: 3px" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtLocationCode" ReadOnly="true" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Waktu Pembayaran")%></label></td>
                        <td><asp:TextBox ID="txtTerm" Width="100%" ReadOnly="true" runat="server" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Franco")%></label></td>
                        <td><asp:TextBox ID="txtFrancoRegion" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Uang")%></label></td>
                        <td><asp:TextBox ID="txtCurrency" ReadOnly="true" Width="100%" runat="server" /></td>
                        <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                        <td><asp:TextBox ID="txtKurs" ReadOnly="true"  Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                position: relative;">
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Qty" HeaderStyle-Width="50px" DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="ItemUnit" HeaderText="Satuan" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="UnitPrice" HeaderText="Harga" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="110px" DataFormatString="{0:N}"/>
                                        <asp:BoundField DataField="UnitPriceAfterVAT" HeaderText="Harga + PPN" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="110px" DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="CustomTotalDiscount" HeaderText="Total Disc" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                        <asp:TemplateField HeaderStyle-Width="5px" />
                                        <asp:BoundField DataField="CustomSubTotal" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                        <asp:TemplateField HeaderStyle-Width="5px" />
                                        <asp:BoundField DataField="CreatedByName" HeaderText="Petugas" HeaderStyle-Width="80px"/>
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
                        <div id="paging"></div>
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
                                            <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;">
                                                <label class="lblNormal"><%=GetLabel("Catatan")%></label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNotes" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="5" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <table style="width: 100%;">
                                        <colgroup>
                                            <col style="width: 220px" />
                                        </colgroup>
                                        <tr>
                                            <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Penjualan (Sebelum PPN)")%></label></td>
                                            <td></td>
                                            <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%></label></td>
                                            <td><asp:TextBox runat="server" ID="txtPPNPercentage" Width="35px" CssClass="number" ReadOnly="true"></asp:TextBox>&nbsp;<%=GetLabel("%")%></td>
                                            <td></td>
                                            <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Penjualan (Sesudah PPN)")%></label></td>
                                            <td></td>
                                            <td><asp:TextBox ID="txtTransactionAmountAfterVAT" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                            <td><asp:TextBox runat="server" ID="txtFinalDiscountInPercentage" Width="35px" CssClass="number" ReadOnly="true"></asp:TextBox>&nbsp;<%=GetLabel("%")%></td>
                                            <td></td>
                                            <td><asp:TextBox ID="txtFinalDiscount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Total Nilai Penjualan")%></label></td>
                                            <td></td>
                                            <td><asp:TextBox ID="txtTransactionAmountSaldo" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
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