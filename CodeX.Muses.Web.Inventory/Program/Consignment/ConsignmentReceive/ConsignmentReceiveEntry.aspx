<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    AutoEventWireup="true" CodeBehind="ConsignmentReceiveEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.ConsignmentReceiveEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
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

            setDatePicker('<%=txtPurchaseReceiveDate.ClientID %>');
            $('#<%=txtPurchaseReceiveDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtDateReferrence.ClientID %>');

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            calculateTotal();

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
                        cboTerm.SetValue(result.TermID);
                    }
                    else {
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        cboTerm.SetValue('');
                    }
                });
            }
            //#endregion

            //#region Location
            function getLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionLocation() %>1 = 1";
                return filterExpression;
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

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
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
                var receiveID = $('#<%=hdnPRID.ClientID %>').val();
                if ($('#<%=txtItemGroupCode.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath like '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                if (receiveID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM PurchaseReceiveDt WHERE PurchaseReceiveID = " + receiveID + ")";
                return filterExpression;
            }

            $('#lblItem.lblLink').live('click', function () {
                openSearchDialog('item', getItemFilterExpression(), function (value) {
                    $('#<%=txtItemCode.ClientID %>').val(value);
                    onTxtItemCodeChanged(value);
                });
            });

            $('#<%=txtItemCode.ClientID %>').live('change', function () {
                onTxtItemCodeChanged($(this).val());
            });

            function onTxtItemCodeChanged(value) {
                var filterExpression = getItemFilterExpression() + " AND ItemCode = '" + value + "'";
                var filterExpressionItemGroup = "ItemCode = '" + value + "'";
                Methods.getObject('GetvItemMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                        $('#<%=txtItemName.ClientID %>').val(result.ItemName1);
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result.GCItemUnit);
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupCode.ClientID %>').val(result.ItemGroupCode);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                        Methods.getItemMasterPurchase(result.ItemID, $('#<%=hdnSupplierID.ClientID %>').val(), function (result2) {
                            if (result2 != null) {
                                $('#<%=hdnItemGroupID.ClientID %>').val(result2.ItemGroupID);
                                $('#<%=txtItemGroupCode.ClientID %>').val(result2.ItemGroupCode);
                                $('#<%=txtItemGroupName.ClientID %>').val(result2.ItemGroupName1);
                                $('#<%=txtSupplierItemCode.ClientID %>').val(result2.SupplierItemCode);
                                $('#<%=txtSupplierItemName.ClientID %>').val(result2.SupplierItemName);
                                $('#<%=txtDiscountPercentage1.ClientID %>').val(result2.Discount);
                                $('#<%=hdnUnitPrice.ClientID %>').val(result2.Price).trigger('changeValue');
                                $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                                $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);

                                var qty = parseFloat($('#<%=txtQuantity.ClientID %>').val());
                                var discountAmount = qty * result2.Price * result2.Discount / 100;
                                $('#<%=txtDiscountAmount1.ClientID %>').val(discountAmount).trigger('changeValue');
                                $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                                $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                            }
                            else {
                                $('#<%=txtSupplierItemCode.ClientID %>').val('');
                                $('#<%=txtSupplierItemName.ClientID %>').val('');
                                $('#<%=txtDiscountPercentage1.ClientID %>').val('0');
                                $('#<%=txtDiscountAmount1.ClientID %>').val('0').trigger('changeValue');
                                $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                                $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                                $('#<%=hdnUnitPrice.ClientID %>').val('0').trigger('changeValue');
                            }
                        });
                        cboItemUnit.PerformCallback();
                    }
                    else {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                        $('#<%=hdnItemID.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Purchase Receive No
            function onGetPurchaseReceiveNoFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblPurchaseReceiveNo.lblLink').click(function () {
                openSearchDialog('purchasereceivehd', onGetPurchaseReceiveNoFilterExpression(), function (value) {
                    $('#<%=txtPurchaseReceiveNo.ClientID %>').val(value);
                    onTxtPurchaseReceiveNoChanged(value);
                });
            });

            $('#<%=txtPurchaseReceiveNo.ClientID %>').change(function () {
                onTxtPurchaseReceiveNoChanged($(this).val());
            });

            function onTxtPurchaseReceiveNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=txtQuantity.ClientID %>').val('1.00');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnItemID.ClientID %>').val('');
                    $('#<%=hdnGCItemUnit.ClientID %>').val('');
                    $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=hdnItemGroupID.ClientID %>').val('');
                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');
                    $('#<%=txtOrderNo.ClientID %>').val('');
                    $('#<%=txtOrderQty.ClientID %>').val('');
                    $('#<%=txtOrderUnit.ClientID %>').val('');
                    $('#<%=lblSupplier.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSupplierCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=chkIsBonus.ClientID %>').prop('checked', true);
                    $('#<%=hdnUnitPrice.ClientID %>').val('0');
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtPrice.ClientID %>').val('0');
                    $('#<%=txtDiscountPercentage1.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount1.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtSupplierItemCode.ClientID %>').val('');
                    $('#<%=txtSupplierItemName.ClientID %>').val('');
                    $('#<%=txtLineAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtItemCode.ClientID %>').removeAttr('readonly');
                    $('#<%=txtItemGroupCode.ClientID %>').removeAttr('readonly');
                    $('#lblItemGroup').attr('class', 'lblLink');
                    $('#lblItem').attr('class', 'lblLink lblMandatory');
                    lastTransactionAmount = $('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal');
                    editedLineAmount = 0;
                    cboItemUnit.SetValue('');
                    //cboCurrency.SetEnabled(false);
                    cboTerm.SetEnabled(false);
                    $('#<%=txtConversion.ClientID %>').val('');
                    $('#entryDetailContainer').show();
                }
            });

            $('#<%=txtQuantity.ClientID %>').change(function () {
                calculateSubTotal();
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

            $('#<%=txtDP.ClientID %>').change(function () {
                $(this).trigger('changeValue');
                calculateTotal();
            });

            $('#<%=txtCharges.ClientID %>').change(function () {
                $(this).trigger('changeValue');
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

            calculateTotal();

            $('.lblExpiredDate').click(function () {
                if ($('#<%=hdnIsEditable.ClientID %>').val() == '1'){
                    $tr = $(this).closest('tr');
                    var param = $tr.find('.keyField').html();
                    var url = ResolveUrl("~/Program/WareHouse/PurchaseReceive/ExpiredDatePerItemCtl.ascx");
                    openUserControlPopup(url, param, 'Expired Date Per Item', 550, 450);
                }
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });

            if (isShowWatermark())
                $('#btnPurchaseReceive').attr('enabled', false);
            else {
                $('#btnPurchaseReceive').click(function () {
                    if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                        var param = $('#<%=hdnSupplierID.ClientID %>').val();
                        var url = ResolveUrl("~/Program/Consignment/ConsignmentReceive/ConsignmentReceiveDetailCtl.ascx");
                        openUserControlPopup(url, param, 'Penerimaan Pembelian Detail', 1200, 550);
                    }
                });
            }
        }

        //#region edit and delete
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
            $('#<%=hdnItemGroupID.ClientID %>').val('');
            $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
            $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);

            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            $('#<%=txtOrderNo.ClientID %>').val(entity.PurchaseOrderNo);
            $('#<%=chkIsBonus.ClientID %>').prop('checked', (entity.IsBonusItem == 'True'));
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtItemCode.ClientID %>').val(entity.ItemCode);
            $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
            $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
            $('#<%=txtOrderQty.ClientID %>').val(entity.OrderQuantity);
            $('#<%=txtOrderUnit.ClientID %>').val(entity.OrderPurchaseUnit);
            $('#<%=txtSupplierItemCode.ClientID %>').val(entity.SupplierItemCode);
            $('#<%=txtSupplierItemName.ClientID %>').val(entity.SupplierItemName);
            $('#<%=hdnUnitPrice.ClientID %>').val(parseFloat(entity.UnitPrice) / parseFloat(entity.ConversionFactor)).trigger('changeValue');
            $('#<%=txtDiscountPercentage1.ClientID %>').val(entity.DiscountPercentage1);
            $('#<%=txtDiscountAmount1.ClientID %>').val(entity.DiscountAmount1).trigger('changeValue');
            $('#<%=txtDiscountPercentage2.ClientID %>').val(entity.DiscountPercentage2);
            $('#<%=txtDiscountAmount2.ClientID %>').val(entity.DiscountAmount2).trigger('changeValue');
            if (entity.IsBonusItem == 'True') {
                $('#<%=txtItemCode.ClientID %>').removeAttr('readonly');
                $('#<%=txtItemGroupCode.ClientID %>').removeAttr('readonly');
                $('#lblItemGroup').attr('class', 'lblLink');
                $('#lblItem').attr('class', 'lblLink lblMandatory');
            }
            else {
                $('#<%=txtItemCode.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtItemGroupCode.ClientID %>').attr('readonly', 'readonly');
                $('#lblItemGroup').attr('class', 'lblNormal');
                $('#lblItem').attr('class', 'lblNormal');
            }
            lastTransactionAmount = $('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal');
            editedLineAmount = entity.LineAmount;
            cboItemUnit.PerformCallback();
            $('#entryDetailContainer').show();
        });
        //#endregion

        var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');
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

        function calculateTotal() {
            var totalKotor = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            if ($('#<%=chkPPN.ClientID %>').is(':checked')) {
                var temp = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                var PPN = VATPercentage / 100 * parseFloat(temp);
                $('#<%=txtPPN.ClientID %>').val(PPN).trigger('changeValue');
            }
            else
                $('#<%=txtPPN.ClientID %>').val('0').trigger('changeValue');
            var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
            var totalHarga = totalKotor + PPN;
            var discountAmount = parseFloat($('#<%=txtFinalDiscountAmount.ClientID %>').attr('hiddenVal'));
            if (totalHarga == 0)
                $('#<%=txtFinalDiscountPercentage.ClientID %>').val(0);
            else {
                var discountPercentage = discountAmount * 100 / totalHarga;
                $('#<%=txtFinalDiscountPercentage.ClientID %>').val(discountPercentage);
            }
            var DP = parseFloat($('#<%=txtDP.ClientID %>').attr('hiddenVal'));
            var Charge = parseFloat($('#<%=txtCharges.ClientID %>').attr('hiddenVal'));
            totalHarga = totalHarga - discountAmount - DP + Charge;
            $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

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
            onAfterCustomSaveSuccess();
            onLoadObject(param);
        }

        function onAfterSaveRecordDtSuccess(PRID) {
            if ($('#<%=hdnPRID.ClientID %>').val() == '0') {
                $('#<%=hdnPRID.ClientID %>').val(PRID);
                var filterExpression = 'PurchaseReceiveID = ' + PRID;
                Methods.getObject('GetPurchaseReceiveHdList', filterExpression, function (result) {
                    $('#<%=txtPurchaseReceiveNo.ClientID %>').val(result.PurchaseReceiveNo);
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

        //#region cbo Item Unit
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
            }
            else {
                var itemID = $('#<%=hdnItemID.ClientID %>').val();
                var filterExpression = "ItemID = " + itemID + " AND GCAlternateUnit = '" + toUnitItem + "'";
                Methods.getObjectValue('GetvItemAlternateUnitList', filterExpression, 'ConversionFactor', function (result) {
                    var toConversion = getItemUnitName(toUnitItem);
                    $('#<%=hdnConversionFactor.ClientID %>').val(result);
                    var conversion = "1 " + toConversion + " = " + result + " " + baseText;
                    $('#<%=txtConversion.ClientID %>').val(conversion);
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
        //#endregion

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var purchaseReceiveID = $('#<%=hdnPRID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (purchaseReceiveID == '' || purchaseReceiveID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "PurchaseReceiveID = " + purchaseReceiveID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }
    </script>
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="" id="hdnPRID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="0" id="hdnNeedConfirmation" runat="server" />
    <input type="hidden" value="0" id="hdnIsDiscountAppliedToAveragePrice" runat="server" />
    <input type="hidden" value="0" id="hdnIsDiscountAppliedToUnitPrice" runat="server" />
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
                            <col style="width: 150px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblPurchaseReceiveNo"><%=GetLabel("No. BPB")%></label></td>
                            <td><asp:TextBox ID="txtPurchaseReceiveNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu Penerimaan") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtPurchaseReceiveDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtPurchaseReceiveTime" Width="100px" CssClass="time" runat="server" Style="text-align: center"/></td>
                                    </tr>
                                </table>
                            </td>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No.Faktur/Kirim")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col style="width: 250px" />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFacturNo" CssClass="required" ValidationGroup="mpEntry" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><input type="button" id="btnPurchaseReceive" value="Salin Pesanan Barang" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal di Faktur") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtDateReferrence" Width="120px" CssClass="datepicker" runat="server" /></td>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTerm" ClientInstanceName="cboTerm" Width="300px" runat="server" /></td>
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
                        <tr style="display: none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                            <td><dxe:ASPxComboBox Visible="false" ID="cboCurrency" ClientInstanceName="cboCurrency" Width="100%" runat="server" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                            <td><asp:TextBox ID="txtKurs" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Barang")%></span><br />
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
                                                    <col style="width: 130px" />
                                                </colgroup>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td><asp:CheckBox ID="chkIsBonus" Width="100%" runat="server" Checked="true" />&nbsp;<%=GetLabel("Bonus")%></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label><%=GetLabel("No. Pemesanan")%></label></td>
                                                    <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                                                </tr>
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
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;">
                                                        <label class="lblMandatory"><%=GetLabel("Jumlah Dipesan")%></label>
                                                    </td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtOrderQty" ReadOnly="true" Width="120px" CssClass="number" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtOrderUnit" ReadOnly="true" Width="150px" runat="server" /></td>
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
                                                                <td><asp:TextBox ID="txtQuantity" CssClass="number" Width="120px" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                                        Width="300px" OnCallback="cboItemUnit_Callback">
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
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Batch")%></label></td>
                                                    <td><asp:TextBox ID="txtBatchNo" runat="server" /></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Expired")%></label></td>
                                                    <td><asp:TextBox ID="txtExpired" value="0.00" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Acc")%></label></td>
                                                    <td><asp:CheckBox ID="chkAcc" Width="100%" runat="server" Checked="true" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal" id="lblSupplierItem"><%=GetLabel("Supplier Item")%></label></td>
                                                    <td colspan="2">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtSupplierItemCode" ReadOnly="true" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtSupplierItemName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Harga")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 200px" />
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
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon 1")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 50px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 200px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtDiscountPercentage1" value="0" CssClass="number" Width="100%" runat="server" /></td>
                                                                <td>[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount1" CssClass="txtCurrency" Width="100%" runat="server" /></td>
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
                                                                <td><asp:TextBox ID="txtDiscountPercentage2" value="0" CssClass="number" Width="100%" runat="server" /></td>
                                                                <td>[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount2" CssClass="txtCurrency" Width="100%" runat="server" /></td>
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
                                            <input type="button" id="btnSave" class="btnWhite" value='<%=GetLabel("Commit") %>'/>
                                            <input type="button" id="btnCancel" class="btnWhite" value='<%=GetLabel("Cancel") %>'/>
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
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                        OnRowDataBound="grdView_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />                                            
                                            <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderText="Bonus"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsBonus" Enabled="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" />
                                            <asp:TemplateField HeaderText="Dipesan" HeaderStyle-Width="125px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("OrderQuantity")%></td>
                                                            <td style="width:50px; color: Red;"><%#Eval("OrderPurchaseUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diterima" HeaderStyle-Width="125px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("Quantity", "{0:N}")%></td>
                                                            <td style="width:50px; color: Red;"><%#Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Harga / Satuan" HeaderStyle-Width="125px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
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
                                            <asp:BoundField DataField="CustomConversion" HeaderStyle-CssClass="thCenter" HeaderText="Konversi" HeaderStyle-Width="150px" />
                                            <asp:TemplateField HeaderStyle-Width="10px" />
                                            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="No. Pemesanan" HeaderStyle-Width="140px" />
                                            <asp:BoundField DataField="DiscountAmount" HeaderStyle-CssClass="thRight" HeaderText="Diskon" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="LineAmount" HeaderStyle-CssClass="thRight" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:TemplateField HeaderStyle-Width="90px">
                                                <ItemTemplate>
                                                    <label <%# IsEditable() == "1" ? "class='lblExpiredDate lblLink'":"class='lblExpiredDate lblLink lblDisabled'" %> ><%=GetLabel("Expired Date")%></label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="Penerima" HeaderStyle-Width="40px"/>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src='<%# ResolveUrl("~/Libs/Images/Button/verify.png") %>' 
                                                    <%#Eval("isConfirmed").ToString() == "True" ? "" : "Style ='display:none'" %> 
                                                    title='<%=GetLabel("Confirmed") %>' alt="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                                    <input type="hidden" value="<%#Eval("IsBonusItem") %>" bindingfield="IsBonusItem" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                    <input type="hidden" value="<%#Eval("PurchaseOrderNo") %>" bindingfield="PurchaseOrderNo" />
                                                    <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                                    <input type="hidden" value="<%#Eval("ItemCode") %>" bindingfield="ItemCode" />
                                                    <input type="hidden" value="<%#Eval("ItemName1") %>" bindingfield="ItemName1" />
                                                    <input type="hidden" value="<%#Eval("ItemGroupCode") %>" bindingfield="ItemGroupCode" />
                                                    <input type="hidden" value="<%#Eval("ItemGroupName1") %>" bindingfield="ItemGroupName1" />
                                                    <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                    <input type="hidden" value="<%#Eval("GCItemUnit") %>" bindingfield="GCItemUnit" />
                                                    <input type="hidden" value="<%#Eval("OrderQuantity") %>" bindingfield="OrderQuantity" />
                                                    <input type="hidden" value="<%#Eval("OrderPurchaseUnit") %>" bindingfield="OrderPurchaseUnit" />
                                                    <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                    <input type="hidden" value="<%#Eval("ConversionFactor") %>" bindingfield="ConversionFactor" />
                                                    <input type="hidden" value="<%#Eval("SupplierItemCode") %>" bindingfield="SupplierItemCode" />
                                                    <input type="hidden" value="<%#Eval("SupplierItemName") %>" bindingfield="SupplierItemName" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage1") %>" bindingfield="DiscountPercentage1" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount1") %>" bindingfield="DiscountAmount1" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage2") %>" bindingfield="DiscountPercentage2" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount2") %>" bindingfield="DiscountAmount2" />
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
                            <div id="paging">
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
                                                <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
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
                                                <td align="right"><asp:CheckBox ID="chkPPN" runat="server" /></td>
                                                <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server"/></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                                <td><asp:TextBox ID="txtFinalDiscountPercentage" CssClass="number" Width="50px" runat="server" /></td>
                                                <td>[%]</td>
                                                <td><asp:TextBox ID="txtFinalDiscountAmount" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Reff Uang Muka")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtDPReferrenceNo" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Uang Muka")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtDP" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pembiayaan")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><dxe:ASPxComboBox ID="cboChargesType" ClientInstanceName="cboChargesType" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Biaya")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtCharges" CssClass="txtCurrency" Width="180px" runat="server" /></td>
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
