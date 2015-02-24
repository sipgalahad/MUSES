<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="CreditNoteEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.CreditNoteEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=txtCreditNoteDate.ClientID %>').attr('readonly') == null) {
                setDatePicker('<%=txtCreditNoteDate.ClientID %>');
                $('#<%=txtCreditNoteDate.ClientID %>').datepicker('option', 'maxDate', '0');
            }

            //#region Credit Note No
            $('#lblCreditNoteNo.lblLink').click(function () {
                openSearchDialog('suppliercreditnote', '', function (value) {
                    $('#<%=txtCreditNoteNo.ClientID %>').val(value);
                    onTxtCreditNoteNoChanged(value);
                });
            });

            $('#<%=txtCreditNoteNo.ClientID %>').change(function () {
                onTxtCreditNoteNoChanged($(this).val());
            });

            function onTxtCreditNoteNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Supplier
            function getSupplierFilterExpression() {
                var filterExpression = "<%=GetSupplierFilterExpression() %>";
                return filterExpression;
            }

            $('#<%=lblSupplier.ClientID %>.lblLink').click(function () {
                openSearchDialog('businesspartners', getSupplierFilterExpression(), function (value) {
                    $('#<%=txtSupplierCode.ClientID %>').val(value);
                    onTxtSupplierChanged(value);
                });
            });

            $('#<%=txtSupplierCode.ClientID %>').change(function () {
                onTxtSupplierChanged($(this).val());
            });

            function onTxtSupplierChanged(value) {
                var filterExpression = getSupplierFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
                Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                        $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                    }
                    else {
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        $('#<%=hdnPurchaseReturnID.ClientID %>').val('');
                        $('#<%=txtPurchaseReturnNo.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Purchase Return
            function getPurchaseReturnFilterExpression() {
                var filterExpression = "<%=GetPurchaseReturnFilterExpression() %>";
                var supplierID = $('#<%=hdnSupplierID.ClientID %>').val();
                if (supplierID != '')
                    filterExpression += " AND BusinessPartnerID = " + $('#<%=hdnSupplierID.ClientID %>').val();
                return filterExpression;
            }

            $('#<%=lblPurchaseReturn.ClientID %>.lblLink').click(function () {
                openSearchDialog('purchasereturnhd', getPurchaseReturnFilterExpression(), function (value) {
                    $('#<%=txtPurchaseReturnNo.ClientID %>').val(value);
                    onTxtPurchaseReturnChanged(value);
                });
            });

            $('#<%=txtPurchaseReturnNo.ClientID %>').change(function () {
                onTxtPurchaseReturnChanged($(this).val());
            });

            function onTxtPurchaseReturnChanged(value) {
                var filterExpression = getPurchaseReturnFilterExpression() + " AND PurchaseReturnNo = '" + value + "'";
                Methods.getObject('GetvPurchaseReturnHdList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnPurchaseReturnID.ClientID %>').val(result.PurchaseReturnID);
                        $('#<%=txtCNAmount.ClientID %>').val(result.TotalNetTransactionAmount).trigger('changeValue');
                        if ($('#<%=hdnSupplierID.ClientID %>').val() == '') {
                            $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                            $('#<%=txtSupplierCode.ClientID %>').val(result.BusinessPartnerCode);
                            $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                        }
                        $('#<%=chkPPN.ClientID %>').prop('checked', result.IsIncludeVAT);
                    }
                    else {
                        $('#<%=hdnPurchaseReturnID.ClientID %>').val('');
                        $('#<%=txtPurchaseReturnNo.ClientID %>').val('');
                        $('#<%=txtCNAmount.ClientID %>').val('0').trigger('changeValue');
                        $('#<%=chkPPN.ClientID %>').prop('checked', false);
                    }
                });
            }
            //#endregion

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var creditNoteID = $('#<%=hdnCreditNoteID.ClientID %>').val();
            if (creditNoteID == '' || creditNoteID == '0') {
                errMessage.text = 'Please Set Transaction First!';
                return false;
            }
            else {
                filterExpression.text = "CreditNoteID = " + creditNoteID;
                return true;
            }
        }

    </script>
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <input type="hidden" id="hdnCreditNoteID" value="0" runat="server" />
                <input type="hidden" id="hdnVATPercentage" value="0" runat="server" />
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 30%" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblCreditNoteNo"><%=GetLabel("No Nota Kredit")%></label></td>
                        <td><asp:TextBox ID="txtCreditNoteNo" Width="150px" runat="server" TabIndex="1" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
                        <td><asp:TextBox ID="txtCreditNoteDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblSupplier" runat="server"><%=GetLabel("Supplier/Penyedia")%></label></td>
                        <td>
                            <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 30%" />
                                    <col style="width: 3px" />
                                    <col style="width: 250px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtSupplierCode" CssClass="required" ValidationGroup="mpEntry" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblPurchaseReturn"><%=GetLabel("No. Pengembalian")%></label></td>
                        <td>
                            <input type="hidden" runat="server" id="hdnPurchaseReturnID" value="" />
                            <asp:TextBox ID="txtPurchaseReturnNo" Width="150px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Nota Kredit")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboGCCreditNoteType" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox ID="chkPPN" Width="100%" runat="server" />&nbsp;<%=GetLabel("PPN")%></td>
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
</asp:Content>