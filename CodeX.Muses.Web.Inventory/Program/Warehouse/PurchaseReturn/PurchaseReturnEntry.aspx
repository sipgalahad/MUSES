<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    AutoEventWireup="true" CodeBehind="PurchaseReturnEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseReturnEntry" %>

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
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1')
                $('#divTransactionAdd').show();
            else
                $('#divTransactionAdd').hide();

            setDatePicker('<%=txtPurchaseReturnDate.ClientID %>');
            $('#<%=txtPurchaseReturnDate.ClientID %>').datepicker('option', 'maxDate', '0');

            //#region Service Unit
            function getLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionLocation() %>";
                return filterExpression;
            }

            function getServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', getServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtServiceUnitCode.ClientID %>').val(value);
                    onTxtServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtServiceUnitCode.ClientID %>').live('change', function () {
                onTxtServiceUnitCodeChanged($(this).val());
            });

            function onTxtServiceUnitCodeChanged(value) {
                var filterExpression = getServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtServiceUnitName.ClientID %>').val(result.ServiceUnitName);
                    }
                    else {
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtServiceUnitName.ClientID %>').val('');
                    }
                    $('#<%=hdnLocationID.ClientID %>').val('');
                    $('#<%=txtLocationCode.ClientID %>').val('');
                    $('#<%=txtLocationName.ClientID %>').val('');
                });
            }
            //#endregion

            //#region Location
            function getLocationFilterExpression() {
                if ($('#<%=hdnSiteServiceUnitID.ClientID %>').val() != "") {
                    var filterExpression = "<%=OnGetFilterExpressionLocation() %>LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = " + $('#<%=hdnSiteServiceUnitID.ClientID %>').val() + " AND IsHeader = 0)";
                    return filterExpression;
                }
                return "<%=OnGetFilterExpressionLocation() %>1 = 0";
            }

            $('#<%=lblLocation.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('locationroleuser', getLocationFilterExpression(), function (value) {
                    $('#<%=txtLocationCode.ClientID %>').val(value);
                    onTxtLocationCodeChanged(value);
                });
            });

            $('#<%=txtLocationCode.ClientID %>').live('change', function () {
                onTxtLocationCodeChanged($(this).val());
            });

            function onTxtLocationCodeChanged(value) {
                var filterExpression = getLocationFilterExpression() + " AND LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                    }
                    else {
                        $('#<%=hdnLocationID.ClientID %>').val('');
                        $('#<%=txtLocationCode.ClientID %>').val('');
                        $('#<%=txtLocationName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Supplier
            function getSupplierFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionSupplier() %>";
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
                        $('#<%=hdnPurchaseReceiveID.ClientID %>').val('0');
                        $('#<%=txtPurchaseReceiveNo.ClientID %>').val('');
                    }
                    else {
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        $('#<%=hdnPurchaseReceiveID.ClientID %>').val('0');
                        $('#<%=txtPurchaseReceiveNo.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Purchase Return No
            $('#lblReturnNo.lblLink').click(function () {
                openSearchDialog('purchasereturnhd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtReturnNo.ClientID %>').val(value);
                    onTxtPurchaseReturnNoChanged(value);
                });
            });

            $('#<%=txtReturnNo.ClientID %>').change(function () {
                onTxtPurchaseReturnNoChanged($(this).val());
            });

            function onTxtPurchaseReturnNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemGroup() %>";
                return filterExpression;
            }

            $('#lblItemGroup.lblLink').live('click', function () {
                openSearchDialog('itemgroup', onGetItemGroupFilterExpression(), function (value) {
                    $('#<%=txtItemGroupCode.ClientID %>').val(value);
                    onTxtItemGroupCodeChanged(value);
                });
            });

            $('#<%=txtItemGroupCode.ClientID %>').live('change', function () {
                onTxtItemGroupCodeChanged($(this).val());
            });

            function onTxtItemGroupCodeChanged(value) {
                var filterExpression = onGetItemGroupFilterExpression() + " AND ItemGroupCode = '" + value + "'";
                $('#<%=txtItemCode.ClientID %>').val('');
                $('#<%=txtItemName.ClientID %>').val('');
                Methods.getObject('GetItemGroupMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                    }
                    else {
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Item
            function getItemFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
                var returnID = $('#<%=hdnPRID.ClientID %>').val();
                var receiveID = $('#<%=hdnPurchaseReceiveID.ClientID %>').val();
                if ($('#<%=txtItemGroupCode.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                if (receiveID != '') {
                    filterExpression += " AND ItemID IN (SELECT ItemID FROM PurchaseReceiveDt WHERE PurchaseReceiveID = " + receiveID + ")";
                }
                if (returnID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM PurchaseReturnDt WHERE PurchaseReturnID = " + returnID + ")";
                return filterExpression;
            }

            $('#lblItem.lblLink').click(function () {
                openSearchDialog('item', getItemFilterExpression(), function (value) {
                    $('#<%=txtItemCode.ClientID %>').val(value);
                    onTxtItemCodeChanged(value);
                });
            });

            $('#<%=txtItemCode.ClientID %>').change(function () {
                onTxtItemCodeChanged($(this).val());
            });

            function onTxtItemCodeChanged(value) {
                var filterExpression = "PurchaseReceiveID = " + $('#<%=hdnPurchaseReceiveID.ClientID %>').val() + " AND ItemCode = '" + value + "' AND GCItemDetailStatus != '<%=GetTransactionStatusVoid() %>'";
                Methods.getObject('GetvPurchaseReceiveDtList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                        $('#<%=txtItemName.ClientID %>').val(result.ItemName1);
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result.GCBaseUnit);
                        $('#<%=hdnGCItemUnit.ClientID %>').val(result.GCItemUnit);
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupCode.ClientID %>').val(result.ItemGroupCode);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);

                        var receiveQtyBaseUnit = parseFloat(result.Quantity) * parseFloat(result.ConversionFactor);
                        $('#<%=hdnReceiveQtyBaseUnit.ClientID %>').val(receiveQtyBaseUnit);

                        var pricePerBaseUnit = parseFloat(result.UnitPrice) / parseFloat(result.ConversionFactor);
                        $('#<%=hdnUnitPrice.ClientID %>').val(pricePerBaseUnit);
                        $('#<%=txtPrice.ClientID %>').val(result.UnitPrice).trigger('changeValue');
                        $('#<%=txtDiscountPercentage1.ClientID %>').val(result.DiscountPercentage1);
                        $('#<%=txtDiscountPercentage2.ClientID %>').val(result.DiscountPercentage2);
                        $('#<%=txtDiscountPercentage1.ClientID %>').change();
                        
                        $('#<%=txtReceivedQty.ClientID %>').val(result.Quantity);
                        $('#<%=txtReceivedUnit.ClientID %>').val(result.ItemUnit);

                        $('#<%=txtQuantity.ClientID %>').attr('max', result.Quantity);
                        cboItemUnit.PerformCallback();
                    }
                    else {
                        $('#<%=hdnItemID.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                        $('#<%=txtDiscountPercentage1.ClientID %>').val('0');
                        $('#<%=txtDiscountAmount1.ClientID %>').val('0').trigger('changeValue');
                        $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                        $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                        $('#<%=hdnUnitPrice.ClientID %>').val('0');
                        $('#<%=txtPrice.ClientID %>').val('0').trigger('changeValue');
                        $('#<%=txtReceivedQty.ClientID %>').val('0');
                        $('#<%=txtReceivedUnit.ClientID %>').val('');
                        $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                        $('#<%=hdnGCItemUnit.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Purchase Receive No
            function onGetPurchaseReceiveFilterExpression() {
                if ($('#<%=hdnSiteServiceUnitID.ClientID %>').val() != "" && $('#<%=hdnSiteServiceUnitID.ClientID %>').val() != "0") {
                    var filterExpression = "<%=OnGetPurchaseReceiveFilterExpression() %>";
                    filterExpression += " AND SiteServiceUnitID = " + $('#<%=hdnSiteServiceUnitID.ClientID %>').val();
                    if ($('#<%=hdnSupplierID.ClientID %>').val() != "" && $('#<%=hdnSupplierID.ClientID %>').val() != "0")
                        filterExpression += " AND SupplierID = " + $('#<%=hdnSupplierID.ClientID %>').val();
                    if ($('#<%=hdnLocationID.ClientID %>').val() != "" && $('#<%=hdnLocationID.ClientID %>').val() != "0")
                        filterExpression += " AND LocationID = " + $('#<%=hdnLocationID.ClientID %>').val();
                    return filterExpression;
                }
                return "1 = 0";
            }

            $('#<%=lblPurchaseReceiveNo.ClientID %>.lblLink').click(function () {
                openSearchDialog('purchasereceivehd', onGetPurchaseReceiveFilterExpression(), function (value) {
                    $('#<%=txtPurchaseReceiveNo.ClientID %>').val(value);
                    onTxtPurchaseReceiveNoChanged(value);
                });
            });

            $('#<%=txtPurchaseReceiveNo.ClientID %>').change(function () {
                onTxtPurchaseReceiveNoChanged($(this).val());
            });

            function onTxtPurchaseReceiveNoChanged(value) {
                var filterExpression = onGetPurchaseReceiveFilterExpression() + " AND PurchaseReceiveNo = '" + value + "'";
                Methods.getObject('GetvPurchaseReceiveHdList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnPurchaseReceiveID.ClientID %>').val(result.PurchaseReceiveID);
                        $('#<%=txtPurchaseReceiveNo.ClientID %>').val(result.PurchaseReceiveNo);
                        $('#<%=hdnLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationCode.ClientID %>').val(result.LocationCode);
                        $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                        $('#<%=hdnSupplierID.ClientID %>').val(result.SupplierID);
                        $('#<%=txtSupplierCode.ClientID %>').val(result.SupplierCode);
                        $('#<%=txtSupplierName.ClientID %>').val(result.SupplierName);
                        $('#<%=txtReferenceNo.ClientID %>').val(result.ReferenceNo);
                        $('#<%=chkPPN.ClientID %>').prop('checked', result.IsIncludeVAT);
                        $('#<%=txtReferenceDate.ClientID %>').val(Methods.getJSONDateValue(result.ReferenceDate));
                        $('#<%=chkPPN.ClientID %>').change();
                    }
                    else {
                        $('#<%=hdnPurchaseReceiveID.ClientID %>').val('0');
                        $('#<%=txtPurchaseReceiveNo.ClientID %>').val('');
                        $('#<%=hdnLocationID.ClientID %>').val('');
                        $('#<%=txtLocationCode.ClientID %>').val('');
                        $('#<%=txtLocationName.ClientID %>').val('');
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        $('#<%=txtReferenceNo.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=txtQuantity.ClientID %>').val('1');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnItemID.ClientID %>').val('');
                    $('#<%=hdnGCItemUnit.ClientID %>').val('');
                    $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=hdnItemGroupID.ClientID %>').val('');
                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');
                    $('#<%=txtPrice.ClientID %>').val('');
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtReceivedQty.ClientID %>').val('');
                    $('#<%=txtReceivedUnit.ClientID %>').val('');
                    $('#<%=txtDiscountPercentage1.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount1.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtLineAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=lblSupplier.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=lblPurchaseReceiveNo.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSupplierCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtPurchaseReceiveNo.ClientID %>').attr('readonly', 'readonly');
                    cboReturnType.SetEnabled(false);
                    $('#<%=txtQuantity.ClientID %>').removeAttr('max');
                    lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                    editedLineAmount = 0;
                    cboItemUnit.SetValue('');
                    cboReason.SetValue('');
                    $('#<%=txtConversion.ClientID %>').val('');
                    $('#entryDetailContainer').show();
                }
            });

            $('#<%=txtQuantity.ClientID %>').change(function () {
                $('#<%=txtDiscountPercentage1.ClientID %>').change();
            });

            $('#<%=txtDiscountPercentage1.ClientID %>').change(function () {
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var totalBeforeDisc = price * qty;
                var discountPercentage = parseFloat($('#<%=txtDiscountPercentage1.ClientID %>').val());
                var discountAmount = totalBeforeDisc * discountPercentage / 100;
                $('#<%=txtDiscountAmount1.ClientID %>').val(discountAmount).trigger('changeValue');

                $('#<%=txtDiscountPercentage2.ClientID %>').change();
                calculateSubTotal();
            });

            $('#<%=txtDiscountAmount1.ClientID %>').change(function () {
                $(this).blur();
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var totalBeforeDisc = price * qty;
                var discountAmount = parseFloat($('#<%=txtDiscountAmount1.ClientID %>').attr('hiddenVal'));
                var discountPercentage = discountAmount * 100 / totalBeforeDisc;
                $('#<%=txtDiscountPercentage1.ClientID %>').val(discountPercentage);

                $('#<%=txtDiscountPercentage2.ClientID %>').change();
                calculateSubTotal();
            });

            $('#<%=txtDiscountPercentage2.ClientID %>').change(function () {
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var discountAmount1 = $('#<%=txtDiscountAmount1.ClientID %>').attr('hiddenVal');
                var totalBeforeDisc = (price * qty) - discountAmount1;
                var discountPercentage = parseFloat($('#<%=txtDiscountPercentage2.ClientID %>').val());
                var discountAmount = totalBeforeDisc * discountPercentage / 100;
                $('#<%=txtDiscountAmount2.ClientID %>').val(discountAmount).trigger('changeValue');

                calculateSubTotal();
            });

            $('#<%=txtDiscountAmount2.ClientID %>').change(function () {
                $(this).blur();
                var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
                var qty = $('#<%=txtQuantity.ClientID %>').val();
                var discountAmount1 = $('#<%=txtDiscountAmount1.ClientID %>').attr('hiddenVal');
                var totalBeforeDisc = (price * qty) - discountAmount1;
                var discountAmount = parseFloat($('#<%=txtDiscountAmount2.ClientID %>').attr('hiddenVal'));
                var discountPercentage = discountAmount * 100 / totalBeforeDisc;
                $('#<%=txtDiscountPercentage2.ClientID %>').val(discountPercentage);

                calculateSubTotal();
            });

            $('#<%=txtPrice.ClientID %>').change(function () {
                $(this).blur();
                calculateSubTotal();
            });

            $('#<%=chkPPN.ClientID %>').change(function () {
                calculateTotal();
            });

            $('#btnCancel').click(function () {
                var lineAmount = parseFloat($('#<%=txtLineAmount.ClientID %>').attr('hiddenVal'));
                var transactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                transactionAmount = transactionAmount - lineAmount + editedLineAmount;
                $('#<%=txtTransactionAmount.ClientID %>').val(transactionAmount).trigger('changeValue');
                $('#entryDetailContainer').hide();
                calculateTotal();
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

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
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
            $('#<%=hdnItemGroupID.ClientID %>').val(entity.ItemGroupID);
            $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
            $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
            $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtItemCode.ClientID %>').val(entity.ItemCode);
            $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);
            $('#<%=txtReceivedQty.ClientID %>').val(entity.ReceivedQuantity);
            $('#<%=txtReceivedUnit.ClientID %>').val(entity.ReceivedItemUnit);
            $('#<%=txtDiscountPercentage1.ClientID %>').val(entity.DiscountPercentage1);
            $('#<%=txtDiscountAmount1.ClientID %>').val(entity.DiscountAmount1).trigger('changeValue');
            $('#<%=txtDiscountPercentage2.ClientID %>').val(entity.DiscountPercentage2);
            $('#<%=txtDiscountAmount2.ClientID %>').val(entity.DiscountAmount2).trigger('changeValue');

            var receiveQtyBaseUnit = parseFloat(entity.ReceivedQuantity) * parseFloat(entity.ReceivedConversionFactor);
            $('#<%=hdnReceiveQtyBaseUnit.ClientID %>').val(receiveQtyBaseUnit);

            var pricePerBaseUnit = parseFloat(entity.UnitPrice) / parseFloat(entity.ConversionFactor);
            $('#<%=hdnUnitPrice.ClientID %>').val(pricePerBaseUnit);
            cboReason.SetValue(entity.GCPurchaseReturnReason);
            $('#<%=txtReason.ClientID %>').val(entity.PurchaseReturnReason);

            lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            editedLineAmount = parseFloat(entity.LineAmount);
            cboItemUnit.PerformCallback();
            $('#entryDetailContainer').show();
        });
        //#endregion

        function calculateSubTotal() {
            var price = $('#<%=txtPrice.ClientID %>').attr('hiddenVal');
            var qty = $('#<%=txtQuantity.ClientID %>').val();
            var totalBeforeDisc = price * qty;
            var discount1 = parseFloat($('#<%=txtDiscountAmount1.ClientID %>').attr('hiddenVal'));
            var discount2 = parseFloat($('#<%=txtDiscountAmount2.ClientID %>').attr('hiddenVal'));
            var subTotal = totalBeforeDisc - discount1 - discount2;
            $('#<%=txtLineAmount.ClientID %>').val(subTotal).trigger('changeValue');

            var totalPurchase = lastTransactionAmount - editedLineAmount + subTotal;
            $('#<%=txtTransactionAmount.ClientID %>').val(totalPurchase).trigger('changeValue');
            calculateTotal();
        }

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
            if ($('#<%=hdnPRID.ClientID %>').val() == '0') {
                $('#<%=hdnPRID.ClientID %>').val(PRID);
                var filterExpression = 'PurchaseReturnID = ' + PRID;
                Methods.getObject('GetPurchaseReturnHdList', filterExpression, function (result) {
                    $('#<%=txtReturnNo.ClientID %>').val(result.PurchaseReturnNo);
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
                    var PRID = s.cpOrderID;
                    onAfterSaveRecordDtSuccess(PRID);
                    $('#divTransactionAdd').click();
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

        //#region cboItemUnit
        function onCboItemUnitEndCallBack() {
            if ($('#<%=hdnGCItemUnit.ClientID %>').val() == '') {
                cboItemUnit.SetValue($('#<%=hdnGCBaseUnit.ClientID %>').val());
            }
            else cboItemUnit.SetValue($('#<%=hdnGCItemUnit.ClientID %>').val());
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

        function getItemUnitName(baseValue) {
            var value = cboItemUnit.GetValue();
            cboItemUnit.SetValue(baseValue);
            var text = cboItemUnit.GetText();
            cboItemUnit.SetValue(value);
            return text;
        }

        function onCboReturnTypeValueChanged(s) {
            if (s.GetValue() == '<%=GetPurchaseReturnCreditNote() %>') {
                $('#<%=chkIsAutoUpdateStock.ClientID %>').prop('checked', false);
                $('#<%=chkIsAutoUpdateStock.ClientID %>').removeAttr("disabled");
            }
            else {
                $('#<%=chkIsAutoUpdateStock.ClientID %>').prop('checked', true);
                $('#<%=chkIsAutoUpdateStock.ClientID %>').attr("disabled", true);
            }
        }

        //#endregion
        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var purchaseReturnID = $('#<%=hdnPRID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (purchaseReturnID == '' || purchaseReturnID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "PurchaseReturnID = " + purchaseReturnID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }
    </script>
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="0" id="hdnPRID" runat="server" />
    <input type="hidden" value="0" id="hdnCNType" runat="server" />
    <input type="hidden" value="0" id="hdnPurchaseReceiveID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="0" id="hdnConfirm" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="" id="hdnDefaultSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnListSiteServiceUnitID" runat="server" />
    <div style="height: 520px; overflow-y: auto; overflow-x: hidden;">
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
                            <td class="tdLabel"><label class="lblLink" id="lblReturnNo"><%=GetLabel("No. Retur")%></label></td>
                            <td><asp:TextBox ID="txtReturnNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td><asp:TextBox ID="txtPurchaseReturnDate" Width="120px" CssClass="datepicker" runat="server" /></td>
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
                                        <td><asp:TextBox ID="txtSupplierCode" ReadOnly="true" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblPurchaseReceiveNo" runat="server"><%=GetLabel("No. BPB")%></label></td>
                            <td><asp:TextBox ID="txtPurchaseReceiveNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:CheckBox ID="chkIsAutoUpdateStock" Width="100%" runat="server" Text="Otomatis Mengurangi Stok" /></td>
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
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblSiteServiceUnit"><%=GetLabel("Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtServiceUnitCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocation"><%=GetLabel("Ke Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Penggantian")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboReturnType" ClientInstanceName="cboReturnType" Width="100%" runat="server">
                                    <ClientSideEvents ValueChanged="function(s,e){ onCboReturnTypeValueChanged(s); }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("No Referensi") %></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="120px" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal di Faktur") %></td>
                            <td><asp:TextBox ID="txtReferenceDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Add Item")%></span><br />
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
                                                    <col style="width: 140px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblLink" id="lblItemGroup"><%=GetLabel("Kelompok Item")%></label></td>
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
                                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblItem"><%=GetLabel("Item")%></label></td>
                                                    <td colspan="2">
                                                        <input type="hidden" value="" id="hdnItemID" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnReceiveQtyBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnConversionFactor" runat="server" />
                                                        <input type="hidden" value="" id="hdnUnitPrice" runat="server" />
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtItemCode" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtItemName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
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
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah Diretur")%></label></td>
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
                                                                <td><asp:TextBox ID="txtPrice" ReadOnly="true" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtBaseUnit" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon 1")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 50px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 200px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtDiscountPercentage1" ReadOnly="true" value="0" CssClass="number" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount1" ReadOnly="true" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon 2")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 50px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 200px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtDiscountPercentage2" ReadOnly="true" value="0" CssClass="number" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount2" ReadOnly="true" CssClass="txtCurrency" Width="100%" runat="server" /></td>
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
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" />
                                            <asp:TemplateField HeaderText="Jumlah Retur" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("Quantity", "{0:N}")%></td>
                                                            <td style="width:50px; color: Red;"><%#Eval("ItemUnit")%></td>
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
                                                            <td style="width:50px; color: Red;"><%#Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DiscountAmount" HeaderText="Total Discount" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="LineAmount" HeaderText="SubTotal" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
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
                                                    <input type="hidden" value="<%#Eval("ReceivedQuantity") %>" bindingfield="ReceivedQuantity" />
                                                    <input type="hidden" value="<%#Eval("ReceivedItemUnit") %>" bindingfield="ReceivedItemUnit" />
                                                    <input type="hidden" value="<%#Eval("ReceivedConversionFactor") %>" bindingfield="ReceivedConversionFactor" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                    <input type="hidden" value="<%#Eval("GCItemUnit") %>" bindingfield="GCItemUnit" />
                                                    <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                    <input type="hidden" value="<%#Eval("ItemUnit") %>" bindingfield="ItemUnit" />
                                                    <input type="hidden" value="<%#Eval("BaseUnit") %>" bindingfield="BaseUnit" />
                                                    <input type="hidden" value="<%#Eval("ConversionFactor") %>" bindingfield="ConversionFactor" />
                                                    <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage1") %>" bindingfield="DiscountPercentage1" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount1") %>" bindingfield="DiscountAmount1" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage2") %>" bindingfield="DiscountPercentage2" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount2") %>" bindingfield="DiscountAmount2" />
                                                    <input type="hidden" value="<%#Eval("GCPurchaseReturnReason") %>" bindingfield="GCPurchaseReturnReason" />
                                                    <input type="hidden" value="<%#Eval("PurchaseReturnReason") %>" bindingfield="PurchaseReturnReason" />
                                                    <input type="hidden" value="<%#Eval("LineAmount") %>" bindingfield="LineAmount" />
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
                                                <col style="width: 10px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Pemesanan")%></label></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%> (<%=GetVATPercentageLabel()%>%)</label></td>
                                                <td align="right"><asp:CheckBox Enabled="false" ID="chkPPN" runat="server" /></td>
                                                <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Nilai Penerimaan")%></label></td>
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
