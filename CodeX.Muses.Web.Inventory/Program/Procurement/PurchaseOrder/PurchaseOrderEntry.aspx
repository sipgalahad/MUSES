<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    AutoEventWireup="true" CodeBehind="PurchaseOrderEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseOrderEntry" %>

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

            setDatePicker('<%=txtItemOrderDate.ClientID %>');
            $('#<%=txtItemOrderDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtItemOrderDeliveryDate.ClientID %>');
            $('#<%=txtItemOrderDeliveryDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtItemOrderExpiredDate.ClientID %>');
            $('#<%=txtItemOrderExpiredDate.ClientID %>').datepicker('option', 'maxDate', '0');

            //#region Purchase Order No
            $('#lblOrderNo.lblLink').click(function () {
                openSearchDialog('purchaseorderhd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtOrderNo.ClientID %>').val(value);
                    onTxtOrderNoChanged(value);
                });
            });

            $('#<%=txtOrderNo.ClientID %>').change(function () {
                onTxtOrderNoChanged($(this).val());
            });

            function onTxtOrderNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Service Unit
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
                        
                        var filterExpression = "SiteServiceUnitID = " + result.SiteServiceUnitID + " AND <%=OnGetFilterExpressionItemGroup() %>";
                        Methods.getListObject('GetvServiceUnitItemGroupList', filterExpression, function (result) {
                            var filterLocationItemGroup = '';
                            for (var i = 0; i < result.length; ++i) {
                                if (filterLocationItemGroup != '')
                                    filterLocationItemGroup += ' OR ';
                                filterLocationItemGroup += "DisplayPath LIKE '%/" + result[i].ItemGroupID + "/%'";
                            }
                            if (filterLocationItemGroup != '')
                                $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                            else
                                $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("");
                        });
                    }
                    else {
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtServiceUnitName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region To Service Unit
            function onGetToLocationFilterExpression() {
                if ($('#<%=hdnToSiteServiceUnitID.ClientID %>').val() != "") {
                    var filterExpression = "<%=OnGetFilterExpressionToLocation() %>LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = " + $('#<%=hdnToSiteServiceUnitID.ClientID %>').val() + " AND IsHeader = 0)";
                    return filterExpression;
                }
                return "<%=OnGetFilterExpressionToLocation()%>1 = 0";
            }

            function getToServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionToServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblToSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', getToServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtToServiceUnitCode.ClientID %>').val(value);
                    ontxtToServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtToServiceUnitCode.ClientID %>').live('change', function () {
                ontxtToServiceUnitCodeChanged($(this).val());
            });

            function ontxtToServiceUnitCodeChanged(value) {
                var filterExpression = getToServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnToSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtToServiceUnitName.ClientID %>').val(result.ServiceUnitName);

                        var filterExpression = onGetToLocationFilterExpression();
                        Methods.getListObject('GetLocationUserAccessList', filterExpression, function (result) {
                            var lstLocationID = '';
                            for (var i = 0; i < result.length; ++i) {
                                if (lstLocationID != '')
                                    lstLocationID += ',';
                                lstLocationID += result[i].LocationID;
                            }
                            var filterExpression = "LocationID IN (" + lstLocationID + ")";
                            Methods.getListObject('GetLocationItemGroupList', filterExpression, function (result) {
                                var filterLocationItemGroup = '';
                                for (var i = 0; i < result.length; ++i) {
                                    if (filterLocationItemGroup != '')
                                        filterLocationItemGroup += ' OR ';
                                    filterLocationItemGroup += "DisplayPath LIKE '%/" + result[i].ItemGroupID + "/%'";
                                }
                                if (filterLocationItemGroup != '')
                                    $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                                else
                                    $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val("");

                                cbpLocation.PerformCallback();
                            });
                        });
                    }
                    else {
                        $('#<%=hdnToSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtToServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtToServiceUnitName.ClientID %>').val('');
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
                        cboTerm.SetValue(result.TermID);
                    }
                    else {
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        cboTerm.SetSelectedIndex(0);
                    }
                });
            }
            //#endregion

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemGroup() %>";
                if ($('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() + ")";
                if ($('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() + ")";
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
                var orderID = $('#<%=hdnOrderID.ClientID %>').val();
                if ($('#<%=txtItemGroupCode.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                else {
                    if ($('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() + ")";
                    if ($('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() + ")";
                }
                if (orderID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM PurchaseOrderDt WHERE PurchaseOrderID = " + orderID + " AND IsDeleted = 0)";
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
                        $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val(result.ItemUnit);
                        $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val(result.ItemUnit);
                        Methods.getItemMasterPurchase(result.ItemID, $('#<%=hdnSupplierID.ClientID %>').val(), function (result2) {
                            if (result2 != null) {
                                $('#<%=hdnItemGroupID.ClientID %>').val(result2.ItemGroupID);
                                $('#<%=txtItemGroupCode.ClientID %>').val(result2.ItemGroupCode);
                                $('#<%=txtItemGroupName.ClientID %>').val(result2.ItemGroupName1);
                                $('#<%=txtSupplierItemCode.ClientID %>').val(result2.SupplierItemCode);
                                $('#<%=txtSupplierItemName.ClientID %>').val(result2.SupplierItemName);
                                $('#<%=txtDiscountPercentage1.ClientID %>').val(result2.Discount);
                                $('#<%=hdnUnitPrice.ClientID %>').val(result2.Price);
                                $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                                $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);
                                $('#<%=hdnConversionFactor.ClientID %>').val(result2.ConversionFactor);

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
                                $('#<%=hdnUnitPrice.ClientID %>').val('0');
                            }
                        });
                        var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
                        filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnSiteServiceUnitID.ClientID %>').val());
                        filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
                        Methods.getValue('GetvPurchaseOrderDtSumQtyOnOrder', filterExpression, function (result3) {
                            if (result3 != null)
                                $('#<%=txtQtyOnOrder.ClientID %>').val(result3);
                            else
                                $('#<%=txtQtyOnOrder.ClientID %>').val("0");
                            GetItemQtyFromServiceUnit();
                        });
                        cboItemUnit.PerformCallback();
                    }
                    else {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                        $('#<%=hdnItemID.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
                        $('#<%=txtStockServiceUnit.ClientID %>').val('');
                    }
                });

            }
            //#endregion

            $('#btnQtyOnOrderDetail').click(function () {
                var qtyOnOrder = $('#<%=txtQtyOnOrder.ClientID %>').val();
                if (qtyOnOrder != '' && qtyOnOrder != '0') {
                    var itemID = $('#<%=hdnItemID.ClientID %>').val();
                    var siteServiceUnitID = $('#<%=hdnSiteServiceUnitID.ClientID %>').val();
                    if (itemID != '' && siteServiceUnitID != '') {
                        var param = siteServiceUnitID + '|' + itemID;
                        var url = ResolveUrl("~/Program/Procurement/PurchaseOrder/PurchaseOrderQtyOnOrderCtl.ascx");
                        openUserControlPopup(url, param, 'Qty On Order', 1200, 500);
                    }
                }
            });

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=txtQuantity.ClientID %>').val('1');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnItemID.ClientID %>').val('');
                    $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val('');
                    $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val('');
                    $('#<%=hdnGCItemUnit.ClientID %>').val('');
                    $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=txtNonMasterItemName.ClientID %>').val('');
                    $('#<%=hdnUnitPrice.ClientID %>').val('0');
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtDiscountPercentage1.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount1.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtDiscountPercentage2.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount2.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtSupplierItemCode.ClientID %>').val('');
                    $('#<%=txtSupplierItemName.ClientID %>').val('');
                    $('#<%=txtLineAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=lblSupplier.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSupplierCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=lblSiteServiceUnit.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtServiceUnitCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtStockServiceUnit.ClientID %>').val('');
                    lastTransactionAmount = $('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal');
                    editedLineAmount = 0;
                    cboItemUnit.SetValue('');
                    $('#<%=txtConversion.ClientID %>').val('');

                    $('#<%=chkIsFromMasterItem.ClientID %>').prop("checked", true);
                    $('#<%=chkIsFromMasterItem.ClientID %>').change();

                    $('#entryDetailContainer').show();
                }
            });

            $('#<%=chkIsFromMasterItem.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    $('#<%=tblItemMaster.ClientID %>').show();
                    $('#<%=txtNonMasterItemName.ClientID %>').hide();
                    $('#lblItem').attr('class', 'lblLink lblMandatory');

                    cboNonMasterItemUnit.SetVisible(false);
                    cboItemUnit.SetVisible(true);
                }
                else {
                    $('#<%=tblItemMaster.ClientID %>').hide();
                    $('#<%=txtNonMasterItemName.ClientID %>').show();
                    $('#lblItem').attr('class', 'lblMandatory');

                    $('#<%=hdnItemID.ClientID %>').val($('#<%=hdnNonMasterItemID.ClientID %>').val());
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');

                    cboNonMasterItemUnit.SetVisible(true);
                    cboItemUnit.SetVisible(false);
                }
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
                if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
                    cbpProcess.PerformCallback('save');
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
                $('#<%=txtDiscountPercentage1.ClientID %>').change();
            });

            $('#<%=chkPPN.ClientID %>').change(function () {
                calculateTotal();
            });

            $('#<%=txtDP.ClientID %>').change(function () {
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

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
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

            setDdeLocationText();
        }

        function GetItemQtyFromServiceUnit() {
            if ($('#<%=hdnLstLocationID.ClientID %>').val() != "") {
                var filterExpression = "LocationID IN (" + $('#<%=hdnLstLocationID.ClientID %>').val() + ") AND ItemID = " + $('#<%=hdnItemID.ClientID %>').val() + " AND IsDeleted = 0";
                Methods.getValue('GetItemBalanceSumQuantityEND', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=txtStockServiceUnit.ClientID %>').val(result);
                    else
                        $('#<%=txtStockServiceUnit.ClientID %>').val('');
                });
            }
            else
                $('#<%=txtStockServiceUnit.ClientID %>').val('');
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
            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCPurchaseUnit);
            $('#<%=hdnConversionFactor.ClientID %>').val(entity.ConversionFactor);
            $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val(entity.BaseUnit);
            $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val(entity.BaseUnit);
            $('#<%=txtSupplierItemCode.ClientID %>').val(entity.SupplierItemCode);
            $('#<%=txtSupplierItemName.ClientID %>').val(entity.SupplierItemName);
            $('#<%=hdnUnitPrice.ClientID %>').val(parseFloat(entity.UnitPrice) / parseFloat(entity.ConversionFactor));
            $('#<%=txtDiscountPercentage1.ClientID %>').val(entity.DiscountPercentage1);
            $('#<%=txtDiscountAmount1.ClientID %>').val(entity.DiscountAmount1).trigger('changeValue');
            $('#<%=txtDiscountPercentage2.ClientID %>').val(entity.DiscountPercentage2);
            $('#<%=txtDiscountAmount2.ClientID %>').val(entity.DiscountAmount2).trigger('changeValue');
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);

            var isNonMasterItem = entity.ItemID == $('#<%=hdnNonMasterItemID.ClientID %>').val();
            $('#<%=chkIsFromMasterItem.ClientID %>').prop("checked", !isNonMasterItem);
            $('#<%=chkIsFromMasterItem.ClientID %>').change();
            if (isNonMasterItem) {
                $('#<%=txtNonMasterItemName.ClientID %>').val(entity.ItemName1);
                cboNonMasterItemUnit.SetValue(entity.GCPurchaseUnit);
                onCboNonMasterItemUnitChanged();
                $('#<%=txtPrice.ClientID %>').val(entity.UnitPrice).trigger('changeValue');
                calculateSubTotal();
            }
            else {
                $('#<%=txtItemCode.ClientID %>').val(entity.ItemCode);
                $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
                $('#<%=hdnItemGroupID.ClientID %>').val(entity.ItemGroupID);
                $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
                $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);

                var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
                filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnSiteServiceUnitID.ClientID %>').val());
                filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
                Methods.getValue('GetvPurchaseOrderDtSumQtyOnOrder', filterExpression, function (result3) {
                    if (result3 != null)
                        $('#<%=txtQtyOnOrder.ClientID %>').val(result3 - entity.CustomTotal);
                    else
                        $('#<%=txtQtyOnOrder.ClientID %>').val("0");
                    GetItemQtyFromServiceUnit();
                });
                lastTransactionAmount = $('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal');
                editedLineAmount = parseFloat(entity.LineAmount);
                cboItemUnit.PerformCallback();
            }
            $('#entryDetailContainer').show();
        });
        //#endregion

        var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');
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
            $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga - discountAmount - DP).trigger('changeValue');
        }

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

        //#region cboItemUnit
        function onCboItemUnitEndCallBack() {
            if ($('#<%=hdnGCItemUnit.ClientID %>').val() == '')
                cboItemUnit.SetValue($('#<%=hdnGCBaseUnit.ClientID %>').val() + '|1');
            else
                cboItemUnit.SetValue($('#<%=hdnGCItemUnit.ClientID %>').val() + '|' + $('#<%=hdnConversionFactor.ClientID %>').val());
            onCboItemUnitChanged();
        }

        function onCboItemUnitChanged() {
            var baseValue = $('#<%=hdnGCBaseUnit.ClientID %>').val();
            var temp = cboItemUnit.GetValue().split('|');
            var toUnitItem = temp[0];
            var conversion = temp[1];
            var baseText = getItemUnitName(baseValue);
            var toConversion = cboItemUnit.GetText().split(' (')[0];
            $('#<%=txtBaseUnit.ClientID %>').val("Per " + toConversion);
            if (baseValue == toUnitItem) {
                $('#<%=hdnConversionFactor.ClientID %>').val('1');
                var conversion = "1 " + baseText + " = 1 " + baseText;
                $('#<%=txtConversion.ClientID %>').val(conversion);
            }
            else {
                var itemID = $('#<%=hdnItemID.ClientID %>').val();
                $('#<%=hdnConversionFactor.ClientID %>').val(conversion);
                var conversion = "1 " + toConversion + " = " + conversion + " " + baseText;
                $('#<%=txtConversion.ClientID %>').val(conversion);
            }
            var conversion = parseFloat($('#<%=hdnConversionFactor.ClientID %>').val());
            var priceperitemunit = parseFloat(($('#<%=hdnUnitPrice.ClientID %>').val()));
            var pricePerPurchaseUnit = conversion * priceperitemunit;
            $('#<%=txtPrice.ClientID %>').val(pricePerPurchaseUnit).trigger('changeValue');
            $('#<%=txtDiscountPercentage1.ClientID %>').change();
        }

        function onCboNonMasterItemUnitChanged() {
            $('#<%=txtBaseUnit.ClientID %>').val("per " + cboNonMasterItemUnit.GetText());
        }

        function getItemUnitName(baseValue) {
            var value = cboItemUnit.GetValue();
            cboItemUnit.SetValue(baseValue + '|1');
            var text = cboItemUnit.GetText().split(' (')[0];
            cboItemUnit.SetValue(value);
            return text;
        }
        //#endregion

        function onAfterSaveRecordDtSuccess(OrderID) {
            if ($('#<%=hdnOrderID.ClientID %>').val() == '0') {
                $('#<%=hdnOrderID.ClientID %>').val(OrderID);
                var filterExpression = 'PurchaseOrderID = ' + OrderID;
                Methods.getObject('GetPurchaseOrderHdList', filterExpression, function (result) {
                    $('#<%=txtOrderNo.ClientID %>').val(result.PurchaseOrderNo);
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
                    var OrderID = s.cpOrderID;
                    onAfterSaveRecordDtSuccess(OrderID);
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

        $('.chkLocation input').live('change', function () {
            setDdeLocationText();
        });

        $(function () {
            $('#btnItemBalanceDt').click(function () {
                var itemID = $('#<%=hdnItemID.ClientID %>').val();
                var locationID = $('#<%=hdnLstLocationID.ClientID %>').val();
                if (itemID != '' && locationID != '') {
                    var param = itemID + '|' + locationID;
                    var url = ResolveUrl("~/Program/Information/ItemBalanceDtCtl.ascx");
                    openUserControlPopup(url, param, 'Item Per Lokasi', 700, 500);
                }
            });

            setDdeLocationText();
        });

        function setDdeLocationText() {
            var lstLocationID = '';
            var lstLocationName = '';
            $('.chkLocation input:checked').each(function () {
                if (lstLocationName != '') {
                    lstLocationName += ', ';
                    lstLocationID += ',';
                }
                lstLocationID += $(this).parent().attr('locationid');
                lstLocationName += $(this).parent().attr('locationname');
            });
            $('#<%=hdnLstLocationID.ClientID %>').val(lstLocationID);
            ddeLocation.SetText(lstLocationName);
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var purchaseOrderID = $('#<%=hdnOrderID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (purchaseOrderID == '' || purchaseOrderID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "PurchaseOrderID = " + purchaseOrderID;
                    return true;
                }
            }
            else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

        $('.lblItemName').live("click", function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var param = entity.ID;
            var url = ResolveUrl("~/Program/Procurement/PurchaseOrder/PurchaseOrderPRDtCtl.ascx");
            openUserControlPopup(url, param, 'Purchase Request Detail', 650, 500);
        });

        function onCbpLocationEndCallback(s) {
            hideLoadingPanel();
            setDdeLocationText();
        }
    </script>

    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnDefaultSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnNonMasterItemID" runat="server" />
    <input type="hidden" value="" id="hdnIsAllowReopenOustandingPO" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="" id="hdnOrderID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnListSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnListToSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterLocationItemGroup" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterToLocationItemGroup" runat="server" />
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
                            <td class="tdLabel"><label id="lblOrderNo" class="lblLink"><%=GetLabel("No. Pemesanan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Pesan") %></td>
                            <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Pengiriman") %></td>
                            <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDeliveryDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Expired") %></td>
                            <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderExpiredDate" Width="120px" CssClass="datepicker" runat="server" /></td>
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
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblSiteServiceUnit"><%=GetLabel("Dari Bagian")%></label></td>
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
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblToSiteServiceUnit"><%=GetLabel("Ke Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnToSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtToServiceUnitCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtToServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Lokasi")%></label></td>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="cbpLocation" runat="server" Width="100%" ClientInstanceName="cbpLocation"
                                    ShowLoadingPanel="false" OnCallback="cbpLocation_Callback">
                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpLocationEndCallback(s); }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                            <dxe:ASPxDropDownEdit ClientInstanceName="ddeLocation" ID="ddeLocation"
                                                Width="300px" runat="server" EnableAnimation="False">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <asp:Repeater ID="rptLocation" runat="server" OnItemDataBound="rptLocation_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkLocation" CssClass="chkLocation" runat="server"  /> <%#Eval("LocationName") %><br />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </DropDownWindowTemplate>
                                            </dxe:ASPxDropDownEdit>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Persediaan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboPurchaseOrderType" ClientInstanceName="cboPurchaseOrderType" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTerm" ClientInstanceName="cboTerm" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Franco")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboFrancoRegion" ClientInstanceName="cboFrancoRegion" Width="100%" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboCurrency" ClientInstanceName="cboCurrency" Width="100%" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                            <td><asp:TextBox ID="txtKurs" Width="120px" runat="server" /></td>
                        </tr>
                        <tr id="trReferenceNo" runat="server" style="display:none">
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Referensi") %></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><asp:TextBox ID="txtReferencePurchaseOrderNo" Width="150px" runat="server" /></td>
                                        <td style="width:5px"></td>
                                        <td><asp:CheckBox ID="chkIsFinalPO" runat="server" /><%=GetLabel("PO Final") %></td>
                                    </tr>
                                </table>                                
                            </td>
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
                                                    <col style="width: 120px" />
                                                    <col style="width: 380px"/>
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
                                                    <td>
                                                        <input type="hidden" value="" id="hdnItemID" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnConversionFactor" runat="server" />
                                                        <input type="hidden" value="" id="hdnUnitPrice" runat="server" />
                                                        <table cellpadding="0" cellspacing="0" id="tblItemMaster" runat="server">
                                                            <tr>
                                                                <td style="width: 120px"><asp:TextBox ID="txtItemCode" Width="100%" runat="server" /></td>
                                                                <td style="width: 3px">&nbsp;</td>
                                                                <td style="width: 250px"><asp:TextBox ID="txtItemName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                        <asp:TextBox ID="txtNonMasterItemName" Width="100%" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsFromMasterItem" runat="server" Checked="true" /><%=GetLabel("Dari Master") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label><%=GetLabel("Stok")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 120px"><asp:TextBox ID="txtStockServiceUnit" ReadOnly="true" CssClass="number" Width="100%" runat="server"/></td>
                                                                <td style="width: 3px">&nbsp;</td>
                                                                <td style="width: 250px"><asp:TextBox ID="txtStockServiceUnitItemUnit" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td><input type="button" id="btnItemBalanceDt" class="btnMore" value="..."/></td>
                                                </tr>        
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
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Jumlah")%></label></td>
                                                    <td><asp:TextBox ID="txtQuantity" Width="120px" CssClass="number" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Satuan Item")%></label></td>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                            Width="300px" OnCallback="cboItemUnit_Callback">
                                                            <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
                                                        </dxe:ASPxComboBox>
                                                        <dxe:ASPxComboBox runat="server" ID="cboNonMasterItemUnit" ClientInstanceName="cboNonMasterItemUnit" Width="300px">
                                                            <ClientSideEvents ValueChanged="function(s,e){ onCboNonMasterItemUnitChanged(); }" />
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Konversi")%></label></td>
                                                    <td><asp:TextBox ID="txtConversion" Width="180px" runat="server" ReadOnly="true" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 150px" />
                                                    <col style="width: 380px"/>
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
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Qty On Order")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 120px"><asp:TextBox ID="txtQtyOnOrder" ReadOnly="true" CssClass="number" Width="100%" runat="server"/></td>
                                                                <td style="width: 3px">&nbsp;</td>
                                                                <td style="width: 250px"><asp:TextBox ID="txtQtyOnOrderItemUnit" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td><input type="button" id="btnQtyOnOrderDetail" class="btnMore" value="..."/></td>
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
                                                                <td>&nbsp;[%]&nbsp;</td>
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
                                                                <td>&nbsp;[%]&nbsp;</td>
                                                                <td><asp:TextBox ID="txtDiscountAmount2" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align:top; padding-top:5px"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                                    <td colspan="2"><asp:TextBox ID="txtNotesDt" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderText="Nama Barang">
                                                <ItemTemplate>
                                                    <label class="lblLink lblItemName"><%#Eval("ItemName1")%></label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jumlah Pembelian" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:75px" align="right"><%#Eval("Quantity", "{0:N}")%></td>
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
                                            <asp:BoundField DataField="DiscountAmount1" HeaderStyle-CssClass="thRight" HeaderText="Diskon 1" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="DiscountAmount2" HeaderStyle-CssClass="thRight" HeaderText="Diskon 2" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="LineAmount" HeaderStyle-CssClass="thRight" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="150px" DataFormatString="{0:N}" />
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
                                                    <input type="hidden" value="<%#Eval("SupplierItemCode") %>" bindingfield="SupplierItemCode" />
                                                    <input type="hidden" value="<%#Eval("SupplierItemName") %>" bindingfield="SupplierItemName" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                    <input type="hidden" value="<%#Eval("GCPurchaseUnit") %>" bindingfield="GCPurchaseUnit" />
                                                    <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                    <input type="hidden" value="<%#Eval("PurchaseUnit") %>" bindingfield="PurchaseUnit" />
                                                    <input type="hidden" value="<%#Eval("BaseUnit") %>" bindingfield="BaseUnit" />
                                                    <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage1") %>" bindingfield="DiscountPercentage1" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount1") %>" bindingfield="DiscountAmount1" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage2") %>" bindingfield="DiscountPercentage2" />
                                                    <input type="hidden" value="<%#Eval("DiscountAmount2") %>" bindingfield="DiscountAmount2" />
                                                    <input type="hidden" value="<%#Eval("ConversionFactor", "{0:G29}") %>" bindingfield="ConversionFactor" />
                                                    <input type="hidden" value="<%#Eval("GCItemDetailStatus") %>" bindingfield="GCItemDetailStatus" />
                                                    <input type="hidden" value="<%#Eval("LineAmount") %>" bindingfield="LineAmount" />
                                                    <input type="hidden" value="<%#Eval("CustomTotal") %>" bindingfield="CustomTotal" />
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
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;"><label class="lblNormal" id="lblPaymentRemarks"><%=GetLabel("Syarat Pembayaran")%></label></td>
                                                <td><asp:TextBox ID="txtPaymentRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                                <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Uang Muka")%></label></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtDP" CssClass="txtCurrency" Width="180px" runat="server" hiddenVal="0"/></td>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
