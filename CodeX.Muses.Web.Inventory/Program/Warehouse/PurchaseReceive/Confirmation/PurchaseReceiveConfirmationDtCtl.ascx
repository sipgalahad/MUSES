<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseReceiveConfirmationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseReceiveConfirmationDtCtl" %>
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

    var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');

    calculateTotal();
    function calculateTotal() {
        var totalKotor = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
        var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
        var totalHarga = totalKotor + PPN;
        var discountAmount = parseFloat($('#<%=txtFinalDiscountAmount.ClientID %>').attr('hiddenVal'));
        var DP = parseFloat($('#<%=txtDP.ClientID %>').attr('hiddenVal'));
        var Charge = parseFloat($('#<%=txtCharges.ClientID %>').attr('hiddenVal'));
        totalHarga = totalHarga - discountAmount - DP + Charge;
        $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
    }
</script>
<input type="hidden" id="hdnPurchaseReceiveID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />

<div style="max-height: 500px; overflow-y: auto" id="containerPopup">
    <table style="width:100%">
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:200px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No BPB")%></label></td>
                        <td><asp:TextBox ID="txtPurchaseReceiveNo" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu Penerimaan") %></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtPurchaseReceiveDate" ReadOnly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    <td style="width: 5px">&nbsp;</td>
                                    <td><asp:TextBox ID="txtPurchaseReceiveTime" Width="60px" ReadOnly="true" CssClass="time" runat="server" Style="text-align: center"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No.Faktur/Kirim")%></label></td>
                        <td><asp:TextBox ID="txtReferenceNo" ReadOnly="true" CssClass="required" ValidationGroup="mpEntry" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal di Faktur") %></td>
                        <td><asp:TextBox ID="txtDateReferrence" ReadOnly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 150px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboTerm" ClientEnabled="false" ClientInstanceName="cboTerm" Width="200px" runat="server" /></td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                        <td><dxe:ASPxComboBox Visible="false" ID="cboCurrency" ClientInstanceName="cboCurrency" Width="100%" runat="server" /></td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                        <td><asp:TextBox ID="txtKurs" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtNotes" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div style="position: relative;">
                    <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                        ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                        <ClientSideEvents EndCallback="function(s,e){onCbpPopupViewEndCallback()}" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th rowspan="2" style="width:30px" align="center"><%=GetLabel("Bonus")%></th>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diterima")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 1")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 2")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Penerima")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>

                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="16">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdPurchaseReceive grdBorder grdView notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th rowspan="2" style="width:30px" align="center"><%=GetLabel("Bonus")%></th>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diterima")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 1")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 2")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total (Sebelum Retur)")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Penerima")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>

                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" ></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center"><asp:CheckBox ID="chkIsBonus" Enabled="false" runat="server" Checked='false' /></td>
                                                <td><%# Eval("ItemCode")%></td>
                                                <td><%# Eval("ItemName1")%></td>
                                                <td align="center">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><%#Eval("UnitPrice","{0:N}")%></td>
                                                            <td>&nbsp/&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td>
                                                    <input type="hidden" class="hdnID" value='<%# Eval("ID")%>' />
                                                    <input type="hidden" class="hdnQuantity" value='<%# Eval("Quantity")%>' />
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("Quantity")%></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountPercentage1")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountAmount1","{0:N}")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountPercentage2")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountAmount2","{0:N}")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("LineAmount","{0:N}")%></td>
                                                <td><%# Eval("Username")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>    
                    <div class="imgLoadingGrdView" id="containerImgLoadingView" >
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                </div>
                <table style="width: 100%;">
                    <colgroup>
                        <col style="width: 50%" />
                        <col style="width: 40px" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <h4><%=GetLabel("Informasi Penerimaan Barang") %></h4>
                            <table style="width: 100%;">
                                <colgroup>
                                    <col style="width: 180px" />
                                    <col style="width: 50px" />
                                    <col style="width: 10px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Pembelian")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
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
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Reff Uang Muka")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtDPReferrenceNo" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Uang Muka")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtDP" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pembiayaan")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><dxe:ASPxComboBox ID="cboChargesType" ClientInstanceName="cboChargesType" ClientEnabled="false" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Biaya")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtCharges" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Nilai Penerimaan")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTotalNetTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>