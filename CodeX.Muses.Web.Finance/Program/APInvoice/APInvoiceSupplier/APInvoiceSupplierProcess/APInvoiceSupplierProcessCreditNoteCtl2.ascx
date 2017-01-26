<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APInvoiceSupplierProcessCreditNoteCtl2.ascx.cs" 
    Inherits="CodeX.Ottimo.Web.Finance.Program.APInvoiceSupplierProcessCreditNoteCtl2" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    if ($('#<%=txtCreditNoteDate.ClientID %>').attr('readonly') == null) {
        setDatePicker('<%=txtCreditNoteDate.ClientID %>');
    }

    $('#containerPopup .txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });
</script>
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnItemID" runat="server" />
<input type="hidden" id="hdnLocationID" runat="server" />
<input type="hidden" id="hdnDateFrom" runat="server" />
<input type="hidden" id="hdnDateTo" runat="server" />
<input type="hidden" id="hdnPurchaseReceiveID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />
<input type="hidden" id="hdnPurchaseReturnAmount" runat="server" />

<div style="max-height: 440px; overflow-y: auto" id="containerPopup">
    <table style="width:100%">
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <input type="hidden" id="hdnCreditNoteID" value="0" runat="server" />
                <input type="hidden" id="Hidden1" value="0" runat="server" />
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 30%" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Nota Kredit")%></label></td>
                        <td><asp:TextBox ID="txtCreditNoteNo" Width="150px" ReadOnly="true" runat="server" TabIndex="1" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
                        <td><asp:TextBox ID="txtCreditNoteDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Supplier/Penyedia")%></label></td>
                        <td>
                            <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 30%" />
                                    <col style="width: 3px" />
                                    <col style="width: 250px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtSupplierCode" ReadOnly="true" CssClass="required" ValidationGroup="mpEntry" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Pengembalian")%></label></td>
                        <td>
                            <input type="hidden" runat="server" id="hdnPurchaseReturnID" value="" />
                            <asp:TextBox ID="txtPurchaseReturnNo" ReadOnly="true" Width="150px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Nota Kredit")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboGCCreditNoteType" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox ID="chkPPN" Enabled="false" Width="100%" runat="server" />&nbsp;<%=GetLabel("PPN")%></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Retur")%></label></td>
                        <td><asp:TextBox ID="txtReturnAmount" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Total (Setelah PPN)")%></label></td>
                        <td><asp:TextBox ID="txtCNAmount" Width="150px" CssClass="txtCurrency" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>            
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>