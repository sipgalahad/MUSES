<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="ItemRequestEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.ItemRequestEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
    <input type="hidden" id="hdnTransactionCode" runat="server" />
    <input type="hidden" id="hdnTransactionCodeItemDistribution" runat="server" />
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
                openSearchDialog('itemrequesthd', "<%=GetFilterExpression() %>", function (value) {
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

            //#region From Service Unit
            function onGetFromServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionFromServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblFromSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', onGetFromServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtFromServiceUnitCode.ClientID %>').val(value);
                    onTxtFromServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtFromServiceUnitCode.ClientID %>').live('change', function () {
                onTxtFromServiceUnitCodeChanged($(this).val());
            });

            function onTxtFromServiceUnitCodeChanged(value) {
                var filterExpression = onGetFromServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtFromServiceUnitName.ClientID %>').val(result.ServiceUnitName);
                    }
                    else {
                        $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtFromServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtFromServiceUnitName.ClientID %>').val('');
                    }
                    $('#<%=hdnFromLocationID.ClientID %>').val('');
                    $('#<%=txtFromLocationCode.ClientID %>').val('');
                    $('#<%=txtFromLocationName.ClientID %>').val('');
                    $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val("");
                });
            }
            //#endregion

            //#region Location From
            function onGetLocationFilterExpression() {
                if ($('#<%=hdnFromSiteServiceUnitID.ClientID %>').val() != "") {
                    var filterExpression = "<%=OnGetFilterExpressionFromLocation() %>LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = " + $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val() + " AND IsHeader = 0)";
                    return filterExpression;
                }
                return "<%=OnGetFilterExpressionFromLocation()%>1 = 0";
            }

            $('#<%=lblFromLocation.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('locationroleuser', onGetLocationFilterExpression(), function (value) {
                    $('#<%=txtFromLocationCode.ClientID %>').val(value);
                    onTxtLocationCodeChanged(value);
                });
            });

            $('#<%=txtFromLocationCode.ClientID %>').live('change', function () {
                onTxtLocationCodeChanged($(this).val());
            });

            function onTxtLocationCodeChanged(value) {
                var filterExpression = onGetLocationFilterExpression() + " AND LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnFromLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtFromLocationName.ClientID %>').val(result.LocationName);

                        filterExpression = "LocationID = " + result.LocationID;
                        Methods.getListObject('GetLocationItemGroupList', filterExpression, function (result) {
                            var filterLocationItemGroup = '';
                            for (var i = 0; i < result.length; ++i) {
                                if (filterLocationItemGroup != '')
                                    filterLocationItemGroup += ' OR ';
                                filterLocationItemGroup += "DisplayPath LIKE '%/" + result[i].ItemGroupID + "/%'";
                            }
                            if (filterLocationItemGroup != '')
                                $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                            else
                                $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val("");
                        });
                    }
                    else {
                        $('#<%=hdnFromLocationID.ClientID %>').val('');
                        $('#<%=txtFromLocationCode.ClientID %>').val('');
                        $('#<%=txtFromLocationName.ClientID %>').val('');
                        $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val("");
                    }
                });
            }
            //#endregion

            //#region To Service Unit
            function onGetToLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionToLocation() %>";
                return filterExpression;
            }

            function onGetToServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionToServiceUnit() %>";
                if ($('#<%=hdnFromSiteServiceUnitID.ClientID %>').val() != '')
                    filterExpression += " AND SiteServiceUnitID != " + $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val();
                return filterExpression;
            }

            $('#<%=lblToSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', onGetToServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtToServiceUnitCode.ClientID %>').val(value);
                    onTxtToServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtToServiceUnitCode.ClientID %>').live('change', function () {
                onTxtToServiceUnitCodeChanged($(this).val());
            });

            function onTxtToServiceUnitCodeChanged(value) {
                var filterExpression = onGetToServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnToSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtToServiceUnitName.ClientID %>').val(result.ServiceUnitName);

                        var filterExpression = onGetToLocationFilterExpression() + "LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = '" + result.SiteServiceUnitID + "')";
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

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
                if ($('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val() + ")";
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
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath like '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                else {
                    if ($('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val() + ")";
                    if ($('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() != '')
                        filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() + ")";
                }
                if (orderID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM ItemRequestDt WHERE ItemRequestID = " + orderID + " AND IsDeleted = 0)";
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
                        if ($('#<%=txtItemGroupCode.ClientID %>').val() == '') {
                            $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                            $('#<%=txtItemGroupCode.ClientID %>').val(result.ItemGroupCode);
                            $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                        }
                        $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                        $('#<%=txtItemName.ClientID %>').val(result.ItemName1);
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result.GCItemUnit);
                        $('#<%=hdnGCItemUnit.ClientID %>').val('');
                        $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val(result.ItemUnit);
                        $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val(result.ItemUnit);
                        var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
                        filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val());
                        filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
                        Methods.getValue('GetvItemRequestDtSumQtyOnOrder', filterExpression, function (result3) {
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
                    var siteServiceUnitID = $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val();
                    if (itemID != '' && siteServiceUnitID != '') {
                        var param = siteServiceUnitID + '|' + itemID;
                        var url = ResolveUrl("~/Program/Warehouse/ItemRequest/ItemRequestQtyOnOrderCtl.ascx");
                        openUserControlPopup(url, param, 'Qty On Order', 1000, 500);
                    }
                }
            });

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=lblFromLocation.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=lblFromSiteServiceUnit.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=lblToSiteServiceUnit.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtFromLocationCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtFromServiceUnitCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtToServiceUnitCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtQuantity.ClientID %>').val('1');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnItemID.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=txtQtyOnOrder.ClientID %>').val('');
                    $('#<%=txtStockServiceUnit.ClientID %>').val('');
                    $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val('');
                    $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val('');
                    cboItemUnit.SetValue('');
                    $('#<%=txtConversion.ClientID %>').val('');

                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');

                    $('#entryDetailContainer').show();
                }
            });

            $('#divQuickPicks').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Warehouse/ItemRequest/ItemRequestQuickPicksCtl.ascx');
                    var transactionID = $('#<%=hdnOrderID.ClientID %>').val();
                    var lstLocationID = $('#<%=hdnLstLocationID.ClientID %>').val();
                    var fromLocationItemGroupID = $('#<%=hdnLstFilterFromLocationItemGroup.ClientID %>').val();
                    var toLocationItemGroupID = $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val();
                    var id = transactionID + '|' + lstLocationID + '|' + fromLocationItemGroupID + '|' + toLocationItemGroupID;
                    openUserControlPopup(url, id, 'Quick Picks', 1000, 600);
                }
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
            cboItemUnit.PerformCallback();
            $('#<%=txtStockServiceUnitItemUnit.ClientID %>').val(entity.ItemUnit);
            $('#<%=txtQtyOnOrderItemUnit.ClientID %>').val(entity.ItemUnit);
            var filterExpression = "<%=OnGetItemQtyOnOrderFilterExpression() %>";
            filterExpression = filterExpression.replace('[SiteServiceUnitID]', $('#<%=hdnFromSiteServiceUnitID.ClientID %>').val());
            filterExpression = filterExpression.replace('[ItemID]', $('#<%=hdnItemID.ClientID %>').val());
            Methods.getValue('GetvItemRequestDtSumQtyOnOrder', filterExpression, function (result3) {
                if (result3 != null)
                    $('#<%=txtQtyOnOrder.ClientID %>').val(result3);
                else
                    $('#<%=txtQtyOnOrder.ClientID %>').val("0");
                GetItemQtyFromServiceUnit();
            });
            $('#entryDetailContainer').show();
        });
        //#endregion

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
        }

        function getItemUnitName(baseValue) {
            var value = cboItemUnit.GetValue();
            cboItemUnit.SetValue(baseValue);
            var text = cboItemUnit.GetText();
            cboItemUnit.SetValue(value);
            return text;
        }
        //#endregion

        function onAfterSaveRecordDtSuccess(OrderID) {
            if ($('#<%=hdnOrderID.ClientID %>').val() == '0') {
                $('#<%=hdnOrderID.ClientID %>').val(OrderID);
                var filterExpression = 'ItemRequestID = ' + OrderID;
                Methods.getObject('GetItemRequestHdList', filterExpression, function (result) {
                    $('#<%=txtOrderNo.ClientID %>').val(result.ItemRequestNo);
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

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var itemRequestID = $('#<%=hdnOrderID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (itemRequestID == '' || itemRequestID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "ItemRequestID = " + itemRequestID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

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

        function onCbpLocationEndCallback(s) {
            hideLoadingPanel();
            setDdeLocationText();
        }
    </script>
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnOrderID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnListFromSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnListToSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnDefaultLocationID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultLocationCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultLocationName" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
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
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblFromSiteServiceUnit"><%=GetLabel("Dari Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnFromSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFromServiceUnitCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtFromServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblFromLocation"><%=GetLabel("Dari Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnFromLocationID" value="" runat="server" />
                                <input type="hidden" value="" id="hdnLstFilterFromLocationItemGroup" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFromLocationCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtFromLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
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
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblToSiteServiceUnit"><%=GetLabel("Ke Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnToSiteServiceUnitID" value="" runat="server" />
                                <input type="hidden" value="" id="hdnLstFilterToLocationItemGroup" runat="server" />
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
                                            <table style="width: 50%">
                                                <colgroup>
                                                    <col style="width: 150px" />
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
                                                    <td colspan="2">
                                                        <input type="hidden" value="" id="hdnItemID" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
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
                                                    <td><asp:TextBox ID="txtQuantity" CssClass="number" Width="120px" runat="server"/></td>
                                                </tr>
                                                 <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Satuan Item")%></label></td>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                            Width="300px" OnCallback="cboItemUnit_Callback">
                                                            <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
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
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" />
                                            <asp:BoundField DataField="CustomItemUnit" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderText="Diminta" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="BaseUnit" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderText="Satuan Dasar" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="CustomConversion" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderText="Konversi" />
                                            <asp:BoundField DataField="CustomItemRequest" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderText="Total Diminta" HeaderStyle-Width="150px" />
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
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
