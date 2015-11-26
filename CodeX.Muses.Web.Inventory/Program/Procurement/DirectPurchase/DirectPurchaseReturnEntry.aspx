<%@ Page Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" CodeBehind="DirectPurchaseReturnEntry.aspx.cs" 
    Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseReturnEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        var lastTransactionAmount = 0;
        var editedLineAmount = 0;
        function onLoad() {
            setButtonEnabled();

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            setDatePicker('<%=txtPurchaseReturnDate.ClientID %>');
            setDatePicker('<%=txtReferenceDate.ClientID %>');
            $('#<%=txtPurchaseReturnDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#btnSalinItem').click(function () {
                if ($(this).attr('enabled') == null) {
                    if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                        var param = $('#<%=hdnDirectPurchaseID.ClientID %>').val() + '|' + $('#<%=hdnDirectPurchaseReturnID.ClientID %>').val();
                        var url = ResolveUrl("~/Program/Procurement/DirectPurchase/DirectPurchaseReturnEntryPicksCtl.ascx");
                        openUserControlPopup(url, param, 'Salin Pesanan Barang', 950, 500);
                    }
                }
            });

            //#region Purchase Return No
            $('#lblReturnNo.lblLink').click(function () {
                openSearchDialog('directpurchasereturnhd', '', function (value) {
                    $('#<%=txtDirectPurchaseReturnNo.ClientID %>').val(value);
                    onTxtPurchaseReturnNoChanged(value);
                });
            });

            $('#<%=txtDirectPurchaseReturnNo.ClientID %>').change(function () {
                onTxtPurchaseReturnNoChanged($(this).val());
            });

            function onTxtPurchaseReturnNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion
            
            //#region Direct Purchase No
            function getDirectPurchaseFilterExpression() {
                var filterExpression = ""; ;
                if ($('#<%=hdnSupplierID.ClientID %>').val() != "")
                    filterExpression += "BusinessPartnerID = " + $('#<%=hdnSupplierID.ClientID %>').val() + " AND ";
                filterExpression += "GCTransactionStatus = 'X121^003'";
                return filterExpression;
            }

            $('#<%=lblDirectPurchaseNo.ClientID %>.lblLink').click(function () {
                openSearchDialog('directpurchase', getDirectPurchaseFilterExpression(), function (value) {
                    $('#<%=txtDirectPurchaseNo.ClientID %>').val(value);
                    ontxtDirectPurchaseNoChanged(value);
                });
            });

            $('#<%=txtDirectPurchaseNo.ClientID %>').change(function () {
                ontxtDirectPurchaseNoChanged($(this).val());
            });

            function ontxtDirectPurchaseNoChanged(value) {
                var filterExpression = "DirectPurchaseNo = '" + value + "'";
                Methods.getObject('GetvDirectPurchaseHdList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnDirectPurchaseID.ClientID %>').val(result.DirectPurchaseID);
                        $('#<%=txtDirectPurchaseNo.ClientID %>').val(result.DirectPurchaseNo);
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtServiceUnitCode.ClientID %>').val(result.ServiceUnitCode);
                        $('#<%=txtServiceUnitName.ClientID %>').val(result.ServiceUnitName);
                        $('#<%=hdnLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationCode.ClientID %>').val(result.LocationCode);
                        $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                        $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                        $('#<%=chkPPN.ClientID %>').prop('checked', result.IsIncludeVAT);
                        $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                        $('#<%=txtReferenceNo.ClientID %>').val(result.ReferenceNo);
                        if (Methods.getJSONDateValue(result.ReferenceDate) != "01-01-1900")
                            $('#<%=txtReferenceDate.ClientID %>').val(Methods.getJSONDateValue(result.ReferenceDate));
                        else
                            $('#<%=txtReferenceDate.ClientID %>').val('');
                    }
                    else {
                        $('#<%=hdnDirectPurchaseID.ClientID %>').val('0');
                        $('#<%=txtDirectPurchaseNo.ClientID %>').val('');
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtServiceUnitName.ClientID %>').val('');
                        $('#<%=hdnLocationID.ClientID %>').val('');
                        $('#<%=txtLocationCode.ClientID %>').val('');
                        $('#<%=txtLocationName.ClientID %>').val('');
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        $('#<%=txtReferenceNo.ClientID %>').val('');
                        $('#<%=chkPPN.ClientID %>').prop('checked', false);
                    }
                });
                setButtonEnabled();
            }
            //#endregion

            $('#<%=txtQuantity.ClientID %>').change(function () {
                $(this).blur();
                calculateSubTotal();
            });

            $('#<%=txtPrice.ClientID %>').change(function () {
                $(this).blur();
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var totalBeforeDisc = price * qty;
                var discountPercentage = parseFloat($('#<%=txtDiscountPercentage.ClientID %>').val());
                var discountAmount = totalBeforeDisc * discountPercentage / 100;
                $('#<%=txtDiscountAmount.ClientID %>').val(discountAmount).trigger('changeValue');
                calculateSubTotal();
            });

            $('#<%=txtDiscountPercentage.ClientID %>').change(function () {
                $(this).blur();
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var totalBeforeDisc = price * qty;
                var discountPercentage = parseFloat($('#<%=txtDiscountPercentage.ClientID %>').val());
                var discountAmount = totalBeforeDisc * discountPercentage / 100;
                $('#<%=txtDiscountAmount.ClientID %>').val(discountAmount).trigger('changeValue');

                calculateSubTotal();
            });

            $('#<%=txtDiscountAmount.ClientID %>').change(function () {
                $(this).blur();
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var totalBeforeDisc = price * qty;
                var discountAmount = parseFloat($('#<%=txtDiscountAmount.ClientID %>').attr('hiddenVal'));
                var discountPercentage = discountAmount * 100 / totalBeforeDisc;
                $('#<%=txtDiscountPercentage.ClientID %>').val(discountPercentage);

                calculateSubTotal();
            });

            $('#<%=chkPPN.ClientID %>').change(function () {
                calculateTotal();
            });

            $('#<%=txtFinalDiscountPercentage.ClientID %>').change(function () {
                var transactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
                var totalHarga = transactionAmount + PPN;
                var discountPercentage = parseFloat($(this).val());
                var discountAmount = totalHarga * discountPercentage / 100;
                $('#<%=txtFinalDiscountAmount.ClientID %>').val(discountAmount).trigger('changeValue');
                calculateTotal();
            });

            $('#<%=txtFinalDiscountAmount.ClientID %>').change(function () {
                $(this).blur();
                calculateTotal();
            });           

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });

            calculateTotal();
        }

        //#region edit dan delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);
            $('#<%=txtDiscountPercentage.ClientID %>').val(entity.DiscountPercentage);
            $('#<%=txtDiscountAmount.ClientID %>').val(entity.DiscountAmount).trigger('changeValue');
            $('#<%=txtReceivedQty.ClientID %>').val(entity.ReceivedQuantity);
            $('#<%=txtReceivedUnit.ClientID %>').val(entity.ReceivedItemUnit);
            var receiveQtyBaseUnit = parseFloat(entity.ReceivedQuantity) * parseFloat(entity.ConversionFactor);
            $('#<%=hdnReceiveQtyBaseUnit.ClientID %>').val(receiveQtyBaseUnit);

            var pricePerBaseUnit = parseFloat(entity.UnitPrice) / parseFloat(entity.ConversionFactor);
            $('#<%=hdnUnitPrice.ClientID %>').val(pricePerBaseUnit);
            cboReason.SetValue(entity.GCPurchaseReturnReason);
            $('#<%=txtReason.ClientID %>').val(entity.PurchaseReturnReason);

            var isNonMasterItem = entity.ItemID == $('#<%=hdnNonMasterItemID.ClientID %>').val();
            if (isNonMasterItem) {
                $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
                cboNonMasterItemUnit.SetVisible(true);
                cboItemUnit.SetVisible(false);
                cboNonMasterItemUnit.SetValue(entity.GCItemUnit);
                onCboNonMasterItemUnitChanged();
                $('#<%=txtPrice.ClientID %>').val(entity.UnitPrice).trigger('changeValue');
                calculateSubTotal();
            }
            else {
                cboNonMasterItemUnit.SetVisible(false);
                cboItemUnit.SetVisible(true);
                $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
                $('#<%=hdnItemGroupID.ClientID %>').val(entity.ItemGroupID);
                $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
                $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);
                cboItemUnit.PerformCallback();
            }

            lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            editedLineAmount = entity.LineAmount;
            $('#entryDetailContainer').show();
        });
        //#endregion

        var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');
        function calculateTotal() {
            var totalKotor = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            if ($('#<%=chkPPN.ClientID %>').is(':checked')) {
                var PPN = VATPercentage / 100 * totalKotor;
                $('#<%=txtPPN.ClientID %>').val(PPN).trigger('changeValue');
            }
            else
                $('#<%=txtPPN.ClientID %>').val('0').trigger('changeValue');
            var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
            var totalHarga = totalKotor + PPN;
            $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
        }

        function calculateSubTotal() {
            var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
            var qty = $('#<%=txtQuantity.ClientID %>').val();
            var totalBeforeDisc = price * qty;
            var discount = parseFloat($('#<%=txtDiscountAmount.ClientID %>').attr('hiddenVal'));
            var subTotal = totalBeforeDisc - discount;
            $('#<%=txtLineAmount.ClientID %>').val(subTotal).trigger('changeValue');

            var totalPurchase = lastTransactionAmount - editedLineAmount + subTotal;
            $('#<%=txtTransactionAmount.ClientID %>').val(totalPurchase).trigger('changeValue');
            calculateTotal();
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                var totalPurchase = parseInt(param[3]);
                $('#<%=txtTransactionAmount.ClientID %>').val(totalPurchase).trigger('changeValue');
                calculateTotal();

                var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        function onAfterSaveAddRecordEntryPopup(param) {
            onLoadObject(param);
        }

        function onAfterSaveRecordDtSuccess(PRID) {
            if ($('#<%=hdnDirectPurchaseReturnID.ClientID %>').val() == '0') {
                $('#<%=hdnDirectPurchaseReturnID.ClientID %>').val(PRID);
                var filterExpression = 'DirectPurchaseReturnID = ' + PRID;
                Methods.getObject('GetDirectPurchaseReturnHdList', filterExpression, function (result) {
                    $('#<%=txtDirectPurchaseReturnNo.ClientID %>').val(result.DirectPurchaseReturnNo);
                });
                onAfterCustomSaveSuccess();
            }
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var PRID = s.cpPurchaseReturnID;
                    onAfterSaveRecordDtSuccess(PRID);
                    $('#btnCancel').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        function onCboReasonValueChanged() {
            if (cboReason.GetValue() != "X162^999") {
                $('#trReason').attr('style', 'display:none');
            }
            else $('#trReason').removeAttr('style');
        }

        function onCboReturnTypeValueChanged() {
            setButtonEnabled();
        }

        function setButtonEnabled() {
            var isEnabled = false;
            if (cboReturnType.GetValue() != null
                && $('#<%=txtDirectPurchaseNo.ClientID %>').val() != ''
                && ($('#<%=hdnGCTransactionStatus.ClientID %>').val() != 'X121^003' 
                && $('#<%=hdnGCTransactionStatus.ClientID %>').val() != 'X121^999'))
                isEnabled = true;

            if (!isEnabled) 
                $('#btnSalinItem').attr('enabled', 'false');
            else 
                $('#btnSalinItem').removeAttr('enabled');
        }

        //#region cboItemUnit
        function onCboItemUnitEndCallBack() {
            if ($('#<%=hdnGCItemUnit.ClientID %>').val() == '') 
                cboItemUnit.SetValue($('#<%=hdnGCBaseUnit.ClientID %>').val());
            else 
                cboItemUnit.SetValue($('#<%=hdnGCItemUnit.ClientID %>').val());
            onCboItemUnitChanged();
        }

        function onCboItemUnitChanged() {
            var baseValue = $('#<%=hdnGCBaseUnit.ClientID %>').val();
            var toUnitItem = cboItemUnit.GetValue();
            var baseText = getItemUnitName(baseValue);
            $('#<%=txtBaseUnit.ClientID %>').val("per " + cboItemUnit.GetText());
            if (baseValue == toUnitItem) {
                $('#<%=hdnConversionFactor.ClientID %>').val('1');
                var conversion = "1 " + baseText + " = 1 " + baseText;
                $('#<%=txtConversion.ClientID %>').val(conversion);
                $('#<%=txtQuantity.ClientID %>').attr('max', $('#<%=hdnReceiveQtyBaseUnit.ClientID %>').val());
            }
            else {
                var itemID = $('#<%=hdnItemID.ClientID %>').val();
                var filterExpression = "ItemID = " + itemID + " AND GCAlternateUnit = '" + toUnitItem + "'";
                Methods.getObjectValue('GetvItemAlternateUnitList', filterExpression, 'ConversionFactor', function (result) {
                    var toConversion = getItemUnitName(toUnitItem);
                    $('#<%=hdnConversionFactor.ClientID %>').val(result);
                    var conversion = "1 " + toConversion + " = " + result + " " + baseText;
                    $('#<%=txtConversion.ClientID %>').val(conversion);

                    var qty = parseFloat($('#<%=hdnReceiveQtyBaseUnit.ClientID %>').val()) / result;
                    $('#<%=txtQuantity.ClientID %>').attr('max', qty);
                });
            }
            var conversion = parseFloat($('#<%=hdnConversionFactor.ClientID %>').val());
            var priceperitemunit = parseFloat(($('#<%=hdnUnitPrice.ClientID %>').val()));
            var pricePerPurchaseUnit = conversion * priceperitemunit;
            $('#<%=txtPrice.ClientID %>').val(pricePerPurchaseUnit).trigger('changeValue');
            calculateSubTotal();
        }

        function onCboNonMasterItemUnitChanged() {
            $('#<%=txtBaseUnit.ClientID %>').val("per " + cboNonMasterItemUnit.GetText());
        }

        function getItemUnitName(baseValue) {
            var value = cboItemUnit.GetValue();
            cboItemUnit.SetValue(baseValue);
            var text = cboItemUnit.GetText();
            cboItemUnit.SetValue(value);
            return text;
        }
        //#endregion
    </script>
    <input type="hidden" value="0" id="hdnDirectPurchaseReturnID" runat="server" />
    <input type="hidden" value="0" id="hdnDirectPurchaseID" runat="server" />
    <input type="hidden" value="" id="hdnNonMasterSupplierID" runat="server" />
    <input type="hidden" value="" id="hdnNonMasterItemID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="" id="hdnGCTransactionStatus" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
    <div style="overflow-y: auto; overflow-x: hidden;">
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
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblReturnNo"><%=GetLabel("No. Retur")%></label></td>
                            <td><asp:TextBox ID="txtDirectPurchaseReturnNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td><asp:TextBox ID="txtPurchaseReturnDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblDirectPurchaseNo" runat="server"><%=GetLabel("No. Pembelian Tunai")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col style="width: 250px" />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtDirectPurchaseNo" Width="100%" ReadOnly="true" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><input type="button" id="btnSalinItem" value='<%=GetLabel("Salin Barang") %>' /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Supplier/Penyedia")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                                <asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory" runat="server"><%=GetLabel("Bagian")%></label>
                            </td>
                            <td>
                                <input type="hidden" id="hdnSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtServiceUnitCode" Width="100%" ReadOnly="true" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory" runat="server" id="lblLocation"><%=GetLabel("Lokasi")%></label>
                            </td>
                            <td>
                                <input type="hidden" id="hdnLocationID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCode" Width="100%" ReadOnly="true" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Penggantian")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboReturnType" ClientInstanceName="cboReturnType" Width="300px"
                                    runat="server">
                                    <ClientSideEvents ValueChanged="function(s,e){ onCboReturnTypeValueChanged(); }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("No Referensi") %></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="120px" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Referensi")%></td>
                            <td><asp:TextBox ID="txtReferenceDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrx" style="margin: 0">
                                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                                <table style="width: 100%">
                                    <colgroup>
                                        <col style="width: 50%" />
                                    </colgroup>
                                    <tr>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelompok Item")%></label></td>
                                                    <td>
                                                        <input type="hidden" value="" id="hdnItemGroupID" runat="server" />
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtItemGroupCode" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtItemGroupName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                                                    <td colspan="2">
                                                        <input type="hidden" value="" id="hdnItemID" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnConversionFactor" runat="server" />
                                                        <input type="hidden" value="" id="hdnReceiveQtyBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnUnitPrice" runat="server" />
                                                        <asp:TextBox ID="txtItemName" ReadOnly="true" Width="100%" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Jumlah Diterima")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtReceivedQty" ReadOnly="true" Width="120px" CssClass="number" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtReceivedUnit" ReadOnly="true" Width="150px" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Jumlah Diretur")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtQuantity" Width="120px" CssClass="number max" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                                        Width="150px" OnCallback="cboItemUnit_Callback">
                                                                        <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
                                                                    </dxe:ASPxComboBox>
                                                                    <dxe:ASPxComboBox runat="server" ID="cboNonMasterItemUnit" ClientEnabled="false" ClientInstanceName="cboNonMasterItemUnit" Width="300px">
                                                                        <ClientSideEvents ValueChanged="function(s,e){ onCboNonMasterItemUnitChanged(); }" />
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Konversi")%></label></td>
                                                    <td><asp:TextBox ID="txtConversion" Width="180px" runat="server" ReadOnly="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Alasan Retur")%></label></td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cboReason" ClientInstanceName="cboReason" Width="300px" runat="server">
                                                            <ClientSideEvents ValueChanged="function(s,e){ onCboReasonValueChanged(); }" />
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr id="trReason" style="display: none">
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Alasan")%></label></td>
                                                    <td colspan="2"><asp:TextBox ID="txtReason" runat="server" Width="300px" TextMode="MultiLine" Rows="2" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Harga")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtPrice" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtBaseUnit" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 50px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 200px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtDiscountPercentage" value="0" CssClass="number" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Harga")%></label></td>
                                                    <td><asp:TextBox ID="txtLineAmount" Width="180px" ReadOnly="true" runat="server" CssClass="txtCurrency" /></td>
                                                </tr>
                                            </table>                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> 
                                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                        position: relative; font-size: 0.95em;">
                                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" HeaderStyle-Width="100px" />
                                                <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" HeaderStyle-Width="280px" />
                                                <asp:TemplateField HeaderText="Jumlah Retur" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width:75px" align="right"><%#Eval("Quantity", "{0:N}")%></td>
                                                                <td style="width:50px; color: Red;"><%#Eval("ItemUnit") %></td>
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
                                                                <td style="width:50px; color: Red;"><%#Eval("ItemUnit") %></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" />
                                                <asp:BoundField DataField="DiscountAmount" HeaderText="Total Discount" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" />
                                                <asp:BoundField DataField="LineAmount" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" />
                                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                        <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                                        <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                                        <input type="hidden" value="<%#Eval("ItemCode") %>" bindingfield="ItemCode" />
                                                        <input type="hidden" value="<%#Eval("ItemName1") %>" bindingfield="ItemName1" />
                                                        <input type="hidden" value="<%#Eval("ItemGroupID") %>" bindingfield="ItemGroupID" />
                                                        <input type="hidden" value="<%#Eval("ItemGroupCode") %>" bindingfield="ItemGroupCode" />
                                                        <input type="hidden" value="<%#Eval("ItemGroupName1") %>" bindingfield="ItemGroupName1" />
                                                        <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                        <input type="hidden" value="<%#Eval("GCItemUnit") %>" bindingfield="GCItemUnit" />
                                                        <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                        <input type="hidden" value="<%#Eval("ItemUnit") %>" bindingfield="ItemUnit" />
                                                        <input type="hidden" value="<%#Eval("BaseUnit") %>" bindingfield="BaseUnit" />
                                                        <input type="hidden" value="<%#Eval("ConversionFactor") %>" bindingfield="ConversionFactor" />
                                                        <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                        <input type="hidden" value="<%#Eval("DiscountPercentage") %>" bindingfield="DiscountPercentage" />
                                                        <input type="hidden" value="<%#Eval("DiscountAmount") %>" bindingfield="DiscountAmount" />
                                                        <input type="hidden" value="<%#Eval("LineAmount") %>" bindingfield="LineAmount" />
                                                        <input type="hidden" value="<%#Eval("GCPurchaseReturnReason") %>" bindingfield="GCPurchaseReturnReason" />
                                                        <input type="hidden" value="<%#Eval("PurchaseReturnReason") %>" bindingfield="PurchaseReturnReason" />
                                                        <input type="hidden" value="<%#Eval("ReceivedQuantity") %>" bindingfield="ReceivedQuantity" />
                                                        <input type="hidden" value="<%#Eval("ReceivedItemUnit") %>" bindingfield="ReceivedItemUnit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                                <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="3" /></td>
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
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Retur")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%> (<%=GetVATPercentageLabel()%>%)</label></td>
                                                <td>&nbsp;</td>
                                                <td><asp:CheckBox ID="chkPPN" runat="server" /></td>
                                                <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                                <td><asp:TextBox ID="txtFinalDiscountPercentage" CssClass="number" Width="50px" runat="server" /></td>
                                                <td>[%]</td>
                                                <td><asp:TextBox ID="txtFinalDiscountAmount" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Nilai Retur")%></label></td>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>