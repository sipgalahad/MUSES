<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    AutoEventWireup="true" CodeBehind="PurchaseRequestEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseRequestEntry" %>

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
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divQuickPicks').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divQuickPicks').hide();
            }
            
            setDatePicker('<%=txtItemOrderDate.ClientID %>');
            $('#<%=txtItemOrderDate.ClientID %>').datepicker('option', 'maxDate', '0');

            //#region Order No
            $('#lblOrderNo.lblLink').click(function () {
                openSearchDialog('purchaserequesthd', "<%=GetFilterExpression() %>", function (value) {
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

                        var filterExpression = getLocationFilterExpression() + "LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = '" + result.SiteServiceUnitID + "')";
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
                                    $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                                else
                                    $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("");

                                cbpLocation.PerformCallback();
                            });
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

                        var filterExpression = "SiteServiceUnitID = " + result.SiteServiceUnitID + " AND <%=OnGetFilterExpressionItemGroup() %>";
                        Methods.getListObject('GetvServiceUnitItemGroupList', filterExpression, function (result) {
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
                var requestID = $('#<%=hdnRequestID.ClientID %>').val();
                if ($('#<%=txtItemGroupCode.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                else {
                    if ($('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() + ")";
                    if ($('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() + ")";
                }
                if (requestID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM PurchaseRequestDt WHERE PurchaseRequestID = " + requestID + " AND IsDeleted = 0)";
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
                Methods.getObject('GetvItemMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                        $('#<%=txtItemName.ClientID %>').val(result.ItemName1);
                        $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val(result.ItemUnit);
                        $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val(result.ItemUnit);
                        Methods.getItemMasterPurchase(result.ItemID, 0, function (result2) {
                            if (result2 != null) {
                                $('#<%=hdnItemGroupID.ClientID %>').val(result2.ItemGroupID);
                                $('#<%=txtItemGroupCode.ClientID %>').val(result2.ItemGroupCode);
                                $('#<%=txtItemGroupName.ClientID %>').val(result2.ItemGroupName1);
                                $('#<%=hdnSupplierID.ClientID %>').val(result2.BusinessPartnerID);
                                $('#<%=txtSupplierCode.ClientID %>').val(result2.BusinessPartnerCode);
                                $('#<%=txtSupplierName.ClientID %>').val(result2.BusinessPartnerName);
                                $('#<%=txtDiscount.ClientID %>').val(result2.Discount);
                                $('#<%=hdnPrice.ClientID %>').val(result2.Price);
                                $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                                $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);
                                $('#<%=hdnConversionFactor.ClientID %>').val(result2.ConversionFactor);
                            }
                            else {
                                $('#<%=txtDiscount.ClientID %>').val('0');
                                $('#<%=txtPrice.ClientID %>').val('0.00');
                                $('#<%=hdnPrice.ClientID %>').val('0.00');
                            }
                        });
                        var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
                        filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnSiteServiceUnitID.ClientID %>').val());
                        filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
                        Methods.getValue('GetvPurchaseRequestDtQtyOnOrderSumQtyOnOrder', filterExpression, function (result3) {
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
                        $('#<%=txtItemCode.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
                        $('#<%=txtQtyOnOrder.ClientID %>').val('');
                        $('#<%=txtStockServiceUnit.ClientID %>').val('');
                        $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val('');
                        $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val('');
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
                        var url = ResolveUrl("~/Program/Procurement/PurchaseRequest/PurchaseRequestQtyOnOrderCtl.ascx");
                        openUserControlPopup(url, param, 'Qty On Order', 1100, 500);
                    }
                }
            });

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

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=lblSiteServiceUnit.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtServiceUnitCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=lblToSiteServiceUnit.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtToServiceUnitCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtQuantity.ClientID %>').val('1');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val('');
                    $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val('');
                    $('#<%=hdnItemGroupID.ClientID %>').val('');
                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');
                    $('#<%=hdnItemID.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=txtNonMasterItemName.ClientID %>').val('');
                    $('#<%=txtSupplierCode.ClientID %>').val('');
                    $('#<%=txtSupplierName.ClientID %>').val('');
                    $('#<%=txtPrice.ClientID %>').val('0.00');
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtDiscount.ClientID %>').val('0');
                    $('#<%=txtNotesDt.ClientID %>').val('');
                    $('#<%=txtStockServiceUnit.ClientID %>').val('');
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

            $('#divQuickPicks').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Procurement/PurchaseRequest/PurchaseRequestQuickPicksCtl.ascx');
                    var transactionID = $('#<%=hdnRequestID.ClientID %>').val();
                    var lstLocationID = $('#<%=hdnLstLocationID.ClientID %>').val();
                    var filterLocationItemGroupID = $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val();
                    var id = transactionID + '|' + lstLocationID + '|' + filterLocationItemGroupID;
                    openUserControlPopup(url, id, 'Quick Picks', 1200, 600);
                }
            });

            $('#btnCancel').click(function () {
                $('#<%=lblSiteServiceUnit.ClientID %>').removeClass('lblDisabled');
                $('#<%=txtServiceUnitCode.ClientID %>').removeClass('lblDisabled');
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
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

        //#region delete and edit
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
            var pricePerPurchaseUnit = parseFloat(entity.UnitPrice);
            var conversion = parseFloat(entity.ConversionFactor);
            var price = pricePerPurchaseUnit / conversion;
            $('#<%=hdnPrice.ClientID %>').val(price);
            $('#<%=txtDiscount.ClientID %>').val(entity.DiscountPercentage);
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);
            $('#<%=hdnSupplierID.ClientID %>').val(entity.BusinessPartnerID);
            $('#<%=txtSupplierCode.ClientID %>').val(entity.BusinessPartnerCode);
            $('#<%=txtSupplierName.ClientID %>').val(entity.BusinessPartnerName);

            var isNonMasterItem = entity.ItemID == $('#<%=hdnNonMasterItemID.ClientID %>').val();
            $('#<%=chkIsFromMasterItem.ClientID %>').prop("checked", !isNonMasterItem);
            $('#<%=chkIsFromMasterItem.ClientID %>').change();
            if (isNonMasterItem) {
                $('#<%=txtNonMasterItemName.ClientID %>').val(entity.ItemName1);
                cboNonMasterItemUnit.SetValue(entity.GCPurchaseUnit);
                onCboNonMasterItemUnitChanged();
                $('#<%=txtPrice.ClientID %>').val(entity.UnitPrice).trigger('changeValue');
            }
            else {
                $('#<%=txtItemCode.ClientID %>').val(entity.ItemCode);
                $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
                $('#<%=hdnItemGroupID.ClientID %>').val(entity.ItemGroupID);
                $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
                $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);
                cboItemUnit.PerformCallback();

                var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
                filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnSiteServiceUnitID.ClientID %>').val());
                filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
                Methods.getValue('GetvPurchaseRequestDtQtyOnOrderSumQtyOnOrder', filterExpression, function (result3) {
                    if (result3 != null)
                        $('#<%=txtQtyOnOrder.ClientID %>').val(result3 - entity.CustomTotal);
                    else
                        $('#<%=txtQtyOnOrder.ClientID %>').val("0");
                    GetItemQtyFromServiceUnit();
                });
            }

            $('#entryDetailContainer').show();
        });
        //#endregion

        function getSupplierFilterExpression() {
            var filterExpression = "<%=OnGetFilterExpressionSupplier() %>";
            return filterExpression;
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
            var priceperitemunit = parseFloat(($('#<%=hdnPrice.ClientID %>').val()));
            var pricePerPurchaseUnit = conversion * priceperitemunit;
            $('#<%=txtPrice.ClientID %>').val(pricePerPurchaseUnit).trigger('changeValue');
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
            if ($('#<%=hdnRequestID.ClientID %>').val() == '0') {
                $('#<%=hdnRequestID.ClientID %>').val(OrderID);
                var filterExpression = 'PurchaseRequestID = ' + OrderID;
                Methods.getObject('GetPurchaseRequestHdList', filterExpression, function (result) {
                    $('#<%=txtOrderNo.ClientID %>').val(result.PurchaseRequestNo);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
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
            var purchaseRequestID = $('#<%=hdnRequestID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (purchaseRequestID == '' || purchaseRequestID == '0') {
                    errMessage.text = 'Please Save Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "PurchaseRequestID = " + purchaseRequestID;
                    return true;
                }
            }
            else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

        function onCbpLocationEndCallback(s) {
            hideLoadingPanel();
            setDdeLocationText();
        }
    </script>
    <input type="hidden" value="false" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnRequestID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnNonMasterItemID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnListSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterLocationItemGroup" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterToLocationItemGroup" runat="server" />
    
    <div style="height: 495px; overflow-y: auto; overflow-x: hidden;">
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
                            <td class="tdLabel"><label class="lblLink" id="lblOrderNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
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
                            <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemOrderTime" Width="100px" CssClass="time" runat="server" Style="text-align: center" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Persediaan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboPurchaseOrderType" ClientInstanceName="cboPurchaseOrderType" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Barang")%></span>
                        <span id="divQuickPicks" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Quick Picks")%></span>
                        <br />
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
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblItem"><%=GetLabel("Item")%></label></td>
                                                    <td>
                                                        <input type="hidden" value="" id="hdnItemID" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
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
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah")%></label></td>
                                                    <td><asp:TextBox ID="txtQuantity" value="1" CssClass="number" Width="120px" runat="server" /></td>
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
                                                    <td>
                                                        <input type="hidden" value="" id="hdnConversionFactor" runat="server" />
                                                        <asp:TextBox ID="txtConversion" Width="180px" runat="server" ReadOnly="true" />
                                                    </td>
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
                                                    <td class="tdLabel"><label class="lblLink" id="lblSupplier" runat="server"><%=GetLabel("Supplier/Penyedia")%></label></td>
                                                    <td>
                                                        <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtSupplierCode" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
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
                                                                <td>
                                                                    <input type="hidden" value="0" id="hdnPrice" runat="server" />
                                                                    <asp:TextBox ID="txtPrice" Width="100%" value="0.00" runat="server" CssClass="txtCurrency" />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtBaseUnit" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
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
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon")%></label></td>
                                                    <td><asp:TextBox ID="txtDiscount" Width="100px" runat="server" value="0" CssClass="number" /> %</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align:top; padding"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                                    <td colspan="2"><asp:TextBox ID="txtNotesDt" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                            <asp:BoundField DataField="ItemName1" HeaderText="Item Name" HeaderStyle-Width="300px" />
                                            <asp:BoundField DataField="BusinessPartnerName" HeaderText="Supplier" HeaderStyle-Width="200px" />
                                            <asp:BoundField DataField="CustomPurchaseUnit" HeaderStyle-CssClass="thRight" HeaderText="Diminta" HeaderStyle-Width="150px"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CustomUnitPrice" HeaderStyle-CssClass="thRight" HeaderText="Harga / Satuan" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="CustomConversion" HeaderStyle-CssClass="thCenter" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="CustomPurchaseRequest" HeaderStyle-CssClass="thRight" HeaderText="Total Diminta" HeaderStyle-Width="150px"
                                                ItemStyle-HorizontalAlign="Right" />
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
                                                    <input type="hidden" value="<%#Eval("BusinessPartnerCode") %>" bindingfield="BusinessPartnerCode" />
                                                    <input type="hidden" value="<%#Eval("BusinessPartnerName") %>" bindingfield="BusinessPartnerName" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                    <input type="hidden" value="<%#Eval("GCPurchaseUnit") %>" bindingfield="GCPurchaseUnit" />
                                                    <input type="hidden" value="<%#Eval("PurchaseUnit") %>" bindingfield="PurchaseUnit" />
                                                    <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                    <input type="hidden" value="<%#Eval("BaseUnit") %>" bindingfield="BaseUnit" />
                                                    <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage") %>" bindingfield="DiscountPercentage" />
                                                    <input type="hidden" value="<%#Eval("ConversionFactor", "{0:G29}") %>" bindingfield="ConversionFactor" />
                                                    <input type="hidden" value="<%#Eval("GCItemDetailStatus") %>" bindingfield="GCItemDetailStatus" />
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
        </table>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
