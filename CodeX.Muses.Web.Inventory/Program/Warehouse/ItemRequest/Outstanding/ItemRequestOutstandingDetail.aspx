<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="ItemRequestOutstandingDetail.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.ItemRequestOutstandingDetail" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnItemReqHdProcess" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/list.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Process")%></div></li>
    <li id="btnItemReqHdDecline" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Decline")%></div></li>
    <li id="btnOrderListBack" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><div><%=GetLabel("Back")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            $('#<%=btnItemReqHdDecline.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                    showToast('Warning', 'Please Select Item First');
                }
                else {
                    onCustomButtonClick('decline');
                }
            });

            $('#<%=btnItemReqHdProcess.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    getCheckedMember();
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                        showToast('Warning', 'Please Select Item First');
                    else {
                        if ($('#<%=hdnIsItemDistributionExist.ClientID %>').val() == '1' && $('#<%=hdnToLocationID.ClientID %>').val() == '')
                            showToast('Warning', 'Lokasi Distribusi Wajib Diisi');
                        else if ($('#<%=hdnIsItemConsumptionExist.ClientID %>').val() == '1' && $('#<%=hdnToLocationID.ClientID %>').val() == '')
                            showToast('Warning', 'Lokasi Pemakaian Wajib Diisi');
                        onCustomButtonClick('approve');
                    }
                }
            });

            $('#<%=btnOrderListBack.ClientID %>').click(function () {
                showLoadingPanel();
                document.location = ResolveUrl('~/Program/Warehouse/ItemRequest/Outstanding/ItemRequestOutstandingList.aspx');
            });

            //#region Location To
            function getLocationFilterExpressionTo() {
                if ($('#<%=hdnToSiteServiceUnitID.ClientID %>').val() != "") {
                    var filterExpression = "<%=OnGetFilterExpressionToLocation() %>LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = " + $('#<%=hdnToSiteServiceUnitID.ClientID %>').val() + " AND IsHeader = 0)";
                    return filterExpression;
                }
                return "<%=OnGetFilterExpressionToLocation()%>1 = 0";
            }

            $('#<%=lblToLocation.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('locationroleuser', getLocationFilterExpressionTo(), function (value) {
                    $('#<%=txtToLocationCode.ClientID %>').val(value);
                    onTxtLocationToCodeChanged(value);
                });
            });

            $('#<%=txtToLocationCode.ClientID %>').live('change', function () {
                onTxtLocationToCodeChanged($(this).val());
            });

            function onTxtLocationToCodeChanged(value) {
                var filterExpression = getLocationFilterExpressionTo() + " AND LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnToLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtToLocationName.ClientID %>').val(result.LocationName);
                        $('#<%=hdnRestrictionID.ClientID %>').val(result.RestrictionID);
                        filterExpression = "LocationID = " + result.LocationID;
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

                            var filterExpression = "<%=OnGetRestrictionTransactionCodeFilterExpression() %>";
                            filterExpression = filterExpression.replace('[RestrictionID]', $('#<%=hdnRestrictionID.ClientID %>').val());
                            var isAllowPurchaseRequest = false;
                            var isAllowItemDistribution = false;
                            var isAllowItemConsumption = false;
                            Methods.getListObject('GetRestrictionDtList', filterExpression, function (result) {
                                for (var i = 0; i < result.length; ++i) {
                                    if (result[i].TransactionCode == "<%=OnGetTransactionCodeItemDistribution() %>")
                                        isAllowItemDistribution = true;
                                    else if (result[i].TransactionCode == "<%=OnGetTransactionCodeItemConsumption() %>")
                                        isAllowItemConsumption = true;
                                }

                                $('#<%=hdnIsAllowItemDistribution.ClientID %>').val(isAllowItemDistribution ? "1" : "0");
                                $('#<%=hdnIsAllowItemConsumption.ClientID %>').val(isAllowItemConsumption ? "1" : "0");
                                
                                getCheckedMember();
                                cbpView.PerformCallback('refresh');
                            });
                        });
                    }
                    else {
                        $('#<%=hdnToLocationID.ClientID %>').val('');
                        $('#<%=txtToLocationCode.ClientID %>').val('');
                        $('#<%=txtToLocationName.ClientID %>').val('');
                        $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val("");

                        getCheckedMember();
                        cbpView.PerformCallback('refresh');
                    }
                });
            }
            //#endregion
        }

        $('.txtDistribution').live('change', function () {
            if ($(this).val() != '' && $(this).val() != '0') {
                $(this).closest('tr').parent().closest('tr').find('.txtConsumption').val('0');
            }
        });

        $('.txtConsumption').live('change', function () {
            if ($(this).val() != '' && $(this).val() != '0') {
                $(this).closest('tr').parent().closest('tr').find('.txtDistribution').val('0');
            }
        });

        $('.lblEndingBalance').live('click', function () {
            var itemID = $(this).closest('tr').parent().closest('tr').find('.keyField').html();
            var locationID = $('#<%=hdnLstLocationID.ClientID %>').val();
            if (itemID != '' && locationID != '') {
                var param = itemID + '|' + locationID;
                var url = ResolveUrl("~/Program/Information/ItemBalanceDtCtl.ascx");
                openUserControlPopup(url, param, 'Item Per Lokasi', 700, 500);
            }
        });

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var itemRequestID = $('#<%=hdnOrderID.ClientID %>').val();
            filterExpression.text = "ItemRequestID = " + itemRequestID;
            return true;
        }

        $('.chkIsSelected input').live('change', function () {
            $tr = $(this).closest('tr');
            if ($(this).is(':checked')) {
                if ($('#<%=hdnIsAllowItemDistribution.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnDistributionQty').val()) == 0) {
                        $tr.find('.txtDistribution').removeAttr('readonly');
                        $tr.find('.lblDistributionItemUnit').attr('class', 'lblDistributionItemUnit lblLink');
                    }
                }
                if ($('#<%=hdnIsAllowItemConsumption.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnConsumptionQty').val()) == 0) {
                        $tr.find('.txtConsumption').removeAttr('readonly');
                        $tr.find('.lblConsumptionItemUnit').attr('class', 'lblConsumptionItemUnit lblLink');
                    }
                }
                if ($('#<%=hdnIsAllowPurchaseRequest.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnPurchaseRequestQty').val()) == 0) {
                        $tr.find('.txtPurchaseRequest').removeAttr('readonly');
                        $tr.find('.lblPurchaseRequestItemUnit').attr('class', 'lblPurchaseRequestItemUnit lblLink');
                    }
                }
            }
            else {
                $tr.find('.txtDistribution').attr('readonly', 'readonly');
                $tr.find('.lblDistributionItemUnit').attr('class', 'lblDistributionItemUnit lblDisabled');
                $tr.find('.txtConsumption').attr('readonly', 'readonly');
                $tr.find('.lblConsumptionItemUnit').attr('class', 'lblConsumptionItemUnit lblDisabled');
                $tr.find('.txtPurchaseRequest').attr('readonly', 'readonly');
                $tr.find('.lblPurchaseRequestItemUnit').attr('class', 'lblPurchaseRequestItemUnit lblDisabled');
            }
        });

        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        function onAfterCustomClickSuccess(type, retval) {
            var param = retval.split('|');
            var messageText = '';
            if (param[1] != '')
                messageText += 'Permintaan Pembelian Berhasil Dibuat Dengan No Transaksi <b>' + param[1] + '</b>';
            if (param[2] != '') {
                if (messageText != '')
                    messageText += '<br />';
                messageText += 'Distribusi Berhasil Dibuat Dengan No Transaksi <b>' + param[2] + '</b>';
            }
            if (param[3] != '') {
                if (messageText != '')
                    messageText += '<br />';
                messageText += 'Pemakaian Berhasil Dibuat Dengan No Transaksi <b>' + param[3] + '</b>';
            }
            showToast('Save Success', messageText, function () {
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                $('#<%=hdnParamDistribution.ClientID %>').val('');
                $('#<%=hdnParamConsumption.ClientID %>').val('');
                $('#<%=hdnParamPurchaseRequest.ClientID %>').val('');
                if (param[0] == '0')
                    $('#<%=btnOrderListBack.ClientID %>').click();
                cbpView.PerformCallback('refresh');
            });
        }

        function getCheckedMember() {
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
            var lstDistribution = $('#<%=hdnParamDistribution.ClientID %>').val().split(',');
            var lstConsumption = $('#<%=hdnParamConsumption.ClientID %>').val().split(',');
            var lstPR = $('#<%=hdnParamPurchaseRequest.ClientID %>').val().split(',');
            var lstDistributionGCItemUnit = $('#<%=hdnParamDistributionGCItemUnit.ClientID %>').val().split(',');
            var lstConsumptionGCItemUnit = $('#<%=hdnParamConsumptionGCItemUnit.ClientID %>').val().split(',');
            var lstPurchaseRequestGCItemUnit = $('#<%=hdnParamPurchaseRequestGCItemUnit.ClientID %>').val().split(',');
            var lstDistributionItemUnit = $('#<%=hdnParamDistributionItemUnit.ClientID %>').val().split(',');
            var lstConsumptionItemUnit = $('#<%=hdnParamConsumptionItemUnit.ClientID %>').val().split(',');
            var lstPurchaseRequestItemUnit = $('#<%=hdnParamPurchaseRequestItemUnit.ClientID %>').val().split(',');
            var lstDistributionConversionFactor = $('#<%=hdnParamDistributionConversionFactor.ClientID %>').val().split(',');
            var lstConsumptionConversionFactor = $('#<%=hdnParamConsumptionConversionFactor.ClientID %>').val().split(',');
            var lstPurchaseRequestConversionFactor = $('#<%=hdnParamPurchaseRequestConversionFactor.ClientID %>').val().split(',');

            var result = '';
            $('.grdItemRequest .chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').html();
                    var itemRequestDtDistribution = $tr.find('.txtDistribution').val();
                    var itemRequestDtConsumption = $tr.find('.txtConsumption').val();
                    var itemRequestDtPR = $tr.find('.txtPurchaseRequest').val();
                    var itemRequestDtDistributionGCItemUnit = $tr.find('.hdnGCDistributionItemUnit').val();
                    var itemRequestDtConsumptionGCItemUnit = $tr.find('.hdnGCConsumptionItemUnit').val();
                    var itemRequestDtPurchaseRequestGCItemUnit = $tr.find('.hdnPurchaseRequestItemUnit').val();
                    var itemRequestDtDistributionItemUnit = $tr.find('.hdnDistributionItemUnit').val();
                    var itemRequestDtConsumptionItemUnit = $tr.find('.hdnConsumptionItemUnit').val();
                    var itemRequestDtPurchaseRequestItemUnit = $tr.find('.hdnPurchaseRequestItemUnit').val();
                    var itemRequestDtDistributionConversionFactor = $tr.find('.hdnDistributionConversionFactor').val();
                    var itemRequestDtConsumptionConversionFactor = $tr.find('.hdnConsumptionConversionFactor').val();
                    var itemRequestDtPurchaseRequestConversionFactor = $tr.find('.hdnPurchaseRequestConversionFactor').val();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx < 0) {
                        lstSelectedMember.push(key);
                        lstDistribution.push(itemRequestDtDistribution);
                        lstConsumption.push(itemRequestDtConsumption);
                        lstPR.push(itemRequestDtPR);
                        lstDistributionGCItemUnit.push(itemRequestDtDistributionGCItemUnit);
                        lstConsumptionGCItemUnit.push(itemRequestDtConsumptionGCItemUnit);
                        lstPurchaseRequestGCItemUnit.push(itemRequestDtPurchaseRequestGCItemUnit);
                        lstDistributionItemUnit.push(itemRequestDtDistributionItemUnit);
                        lstConsumptionItemUnit.push(itemRequestDtConsumptionItemUnit);
                        lstPurchaseRequestItemUnit.push(itemRequestDtPurchaseRequestItemUnit);
                        lstDistributionConversionFactor.push(itemRequestDtDistributionConversionFactor);
                        lstConsumptionConversionFactor.push(itemRequestDtConsumptionConversionFactor);
                        lstPurchaseRequestConversionFactor.push(itemRequestDtPurchaseRequestConversionFactor);
                    }
                    else {
                        lstDistribution[idx] = itemRequestDtDistribution;
                        lstConsumption[idx] = itemRequestDtConsumption;
                        lstPR[idx] = itemRequestDtPR;
                        lstDistributionGCItemUnit[idx] = itemRequestDtDistributionGCItemUnit;
                        lstConsumptionGCItemUnit[idx] = itemRequestDtConsumptionGCItemUnit;
                        lstPurchaseRequestGCItemUnit[idx] = itemRequestDtPurchaseRequestGCItemUnit;
                        lstDistributionItemUnit[idx] = itemRequestDtDistributionItemUnit;
                        lstConsumptionItemUnit[idx] = itemRequestDtConsumptionItemUnit;
                        lstPurchaseRequestItemUnit[idx] = itemRequestDtPurchaseRequestItemUnit;
                        lstDistributionConversionFactor[idx] = itemRequestDtDistributionConversionFactor;
                        lstConsumptionConversionFactor[idx] = itemRequestDtConsumptionConversionFactor;
                        lstPurchaseRequestConversionFactor[idx] = itemRequestDtPurchaseRequestConversionFactor;
                    }
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').html();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx > -1) {
                        lstSelectedMember.splice(idx, 1);
                        lstDistribution.splice(idx, 1);
                        lstConsumption.splice(idx, 1);
                        lstPR.splice(idx, 1);
                        lstDistributionGCItemUnit.splice(idx, 1);
                        lstConsumptionGCItemUnit.splice(idx, 1);
                        lstPurchaseRequestGCItemUnit.splice(idx, 1);
                        lstDistributionItemUnit.splice(idx, 1);
                        lstConsumptionItemUnit.splice(idx, 1);
                        lstPurchaseRequestItemUnit.splice(idx, 1);
                        lstDistributionConversionFactor.splice(idx, 1);
                        lstConsumptionConversionFactor.splice(idx, 1);
                        lstPurchaseRequestConversionFactor.splice(idx, 1);
                    }
                }
            });

            var isItemDistributionExists = false;
            for (var i = 1; i < lstDistribution.length; ++i) {
                if (lstDistribution[i] != '0') {
                    isItemDistributionExists = true;
                    break;
                }
            }
            var isItemConsumptionExists = false;
            for (var i = 1; i < lstConsumption.length; ++i) {
                if (lstConsumption[i] != '0') {
                    isItemConsumptionExists = true;
                    break;
                }
            }
            var isPurchaseRequestExists = false;
            for (var i = 1; i < lstPR.length; ++i) {
                if (lstPR[i] != '0') {
                    isPurchaseRequestExists = true;
                    break;
                }
            }

            $('#<%=hdnIsItemDistributionExist.ClientID %>').val(isItemDistributionExists ? "1" : "0");
            $('#<%=hdnIsItemConsumptionExist.ClientID %>').val(isItemConsumptionExists ? "1" : "0");
            $('#<%=hdnIsPurchaseRequestExist.ClientID %>').val(isPurchaseRequestExists ? "1" : "0"); 

            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            $('#<%=hdnParamDistribution.ClientID %>').val(lstDistribution.join(','));
            $('#<%=hdnParamConsumption.ClientID %>').val(lstConsumption.join(','));
            $('#<%=hdnParamPurchaseRequest.ClientID %>').val(lstPR.join(','));
            $('#<%=hdnParamDistributionGCItemUnit.ClientID %>').val(lstDistributionGCItemUnit.join(','));
            $('#<%=hdnParamConsumptionGCItemUnit.ClientID %>').val(lstConsumptionGCItemUnit.join(','));
            $('#<%=hdnParamPurchaseRequestGCItemUnit.ClientID %>').val(lstPurchaseRequestGCItemUnit.join(','));
            $('#<%=hdnParamDistributionItemUnit.ClientID %>').val(lstDistributionItemUnit.join(','));
            $('#<%=hdnParamConsumptionItemUnit.ClientID %>').val(lstConsumptionItemUnit.join(','));
            $('#<%=hdnParamPurchaseRequestItemUnit.ClientID %>').val(lstPurchaseRequestItemUnit.join(','));
            $('#<%=hdnParamDistributionConversionFactor.ClientID %>').val(lstDistributionConversionFactor.join(','));
            $('#<%=hdnParamConsumptionConversionFactor.ClientID %>').val(lstConsumptionConversionFactor.join(','));
            $('#<%=hdnParamPurchaseRequestConversionFactor.ClientID %>').val(lstPurchaseRequestConversionFactor.join(','));
        }

        //#region Item Unit
        function getItemUnitFilterExpression() {
            var filterExpression = "ItemID = " + itemID;
            return filterExpression;
        }

        var itemID = 0;
        $('.lblDistributionItemUnit.lblLink').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            itemID = $tr.find('.hdnItemID').val();
            openSearchDialog('itemalternateunit', getItemUnitFilterExpression(), function (value) {
                onTxtItemUnitChanged(value, 'hdnGCDistributionItemUnit', 'hdnDistributionItemUnit', 'lblDistributionItemUnit', 'hdnDistributionConversionFactor');
            });
        });
        $('.lblConsumptionItemUnit.lblLink').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            itemID = $tr.find('.hdnItemID').val();
            openSearchDialog('itemalternateunit', getItemUnitFilterExpression(), function (value) {
                onTxtItemUnitChanged(value, 'hdnGCConsumptionItemUnit', 'hdnConsumptionItemUnit', 'lblConsumptionItemUnit', 'hdnConsumptionConversionFactor');
            });
        });
        $('.lblPurchaseRequestItemUnit.lblLink').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            itemID = $tr.find('.hdnItemID').val();
            openSearchDialog('itemalternateunit', getItemUnitFilterExpression(), function (value) {
                onTxtItemUnitChanged(value, 'hdnGCPurchaseRequestItemUnit', 'hdnPurchaseRequestItemUnit', 'lblPurchaseRequestItemUnit', 'hdnPurchaseRequestConversionFactor');
            });
        });

        function onTxtItemUnitChanged(value, hdnGCItemUnit, hdnItemUnit, lblItemUnit, hdnConversionFactor) {
            var temp = value.split('|');
            var filterExpression = getItemUnitFilterExpression() + " AND GCAlternateUnit = '" + temp[0] + "' AND ConversionFactor = " + temp[1];
            Methods.getObject('GetvItemAlternateUnitCustomList', filterExpression, function (result) {
                if (result != null) {
                    $tr.find('.' + hdnGCItemUnit).val(result.GCAlternateUnit);
                    $tr.find('.' + hdnItemUnit).val(result.AlternateUnit);
                    $tr.find('.' + lblItemUnit).html(result.cfAlternateUnit);
                    $tr.find('.' + hdnConversionFactor).val(result.ConversionFactor);
                }
                else {
                    $tr.find('.' + hdnGCItemUnit).val('');
                    $tr.find('.' + hdnItemUnit).val('');
                    $tr.find('.' + lblItemUnit).html('');
                    $tr.find('.' + hdnConversionFactor).val('');
                }
            });
        }
        //#endregion

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        $('.lblAvailableStock.lblLink').live("click", function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            var itemID = $tr.find('.hdnItemID').val();
            var stock = $tr.find('.hdnQuantityEND').val();
            var itemUnit = $tr.find('.hdnItemUnit').val();
            var orderID = $('#<%=hdnOrderID.ClientID %>').val();
            var param = orderID + '|' + itemID + '|' + stock + '|' + itemUnit;
            var url = ResolveUrl("~/Program/Warehouse/ItemRequest/Outstanding/ItemRequestProcessedDtCtl.ascx");
            openUserControlPopup(url, param, 'Detail Penggunaan Item', 800, 500);
        });
    </script>
    <input type="hidden" value="" id="hdnParamID" runat="server" />
    <input type="hidden" value="" id="hdnParamDistribution" runat="server" />
    <input type="hidden" value="" id="hdnParamDistributionGCItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamDistributionItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamDistributionConversionFactor" runat="server" />
    <input type="hidden" value="" id="hdnParamConsumption" runat="server" />
    <input type="hidden" value="" id="hdnParamConsumptionGCItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamConsumptionItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamConsumptionConversionFactor" runat="server" />
    <input type="hidden" value="" id="hdnParamPurchaseRequest" runat="server" />
    <input type="hidden" value="" id="hdnParamPurchaseRequestGCItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamPurchaseRequestItemUnit" runat="server" />
    <input type="hidden" value="" id="hdnParamPurchaseRequestConversionFactor" runat="server" />
    <input type="hidden" value="" id="hdnOrderID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultGCConsumptionType" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" value="" id="hdnIsAllowPurchaseRequest" runat="server" />
    <input type="hidden" value="" id="hdnIsAllowItemConsumption" runat="server" />
    <input type="hidden" value="" id="hdnIsAllowItemDistribution" runat="server" />
    <input type="hidden" value="" id="hdnIsPurchaseRequestExist" runat="server" />
    <input type="hidden" value="" id="hdnIsItemDistributionExist" runat="server" />
    <input type="hidden" value="" id="hdnIsItemConsumptionExist" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
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
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" id="lblOrderNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dari Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnFromSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFromServiceUnitCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtFromServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" runat="server" id="lblLocation"><%=GetLabel("Dari Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnFromLocationID" value="" runat="server" />
                                <input type="hidden" id="hdnLstFilterFromLocationItemGroup" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFromLocationCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtFromLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" ReadOnly="true" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemOrderTime" Width="100px" CssClass="time" runat="server" ReadOnly="true" Style="text-align: center" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Ke Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnToSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtToServiceUnitCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtToServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" runat="server" id="lblToLocation"><%=GetLabel("Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnRestrictionID" value="" runat="server" />
                                <input type="hidden" id="hdnToLocationID" value="" runat="server" />
                                <input type="hidden" id="hdnLstFilterToLocationItemGroup" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtToLocationCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtToLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdItemRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">
                                                        
                                                    </th>
                                                    <th rowspan="2" style="width: 20px; text-align: center">
                                                        <input id="chkSelectAll" type="checkbox" />
                                                    </th>
                                                    <th rowspan="2"><%=GetLabel("NAMA BARANG")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("SEDANG DIPROSES")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH PROSES")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Total Diminta")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Bisa Digunakan")%></th>

                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diterima")%></th>

                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Distribusi")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Pemakaian")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="10">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdItemRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">
                                                        
                                                    </th>
                                                    <th rowspan="2" style="width: 20px; text-align: center">
                                                        <input id="chkSelectAll" type="checkbox" />
                                                    </th>
                                                    <th rowspan="2"><%=GetLabel("NAMA BARANG")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("SEDANG DIPROSES")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH PROSES")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Total Diminta")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Bisa Digunakan")%></th>

                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diterima")%></th>

                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Distribusi")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Pemakaian")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%# Eval("ID")%></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                </td>
                                                <td>
                                                    <input type="hidden" value='<%#Eval("ItemID") %>' class="hdnItemID" />
                                                    <input type="hidden" value='<%#Eval("ItemUnit") %>' class="hdnItemUnit" />
                                                    <input type="hidden" value='<%#Eval("EndingBalance") %>' class="hdnQuantityEND" /> 
                                                    <%# Eval("ItemName1")%>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("Quantity")%></td>
                                                            <td>&nbsp;<%# Eval("cfItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("CustomTotal", "{0:G29}")%></td>
                                                            <td>&nbsp;<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right">
                                                                <label id="lblEndingBalance" runat="server" class="lblEndingBalance lblLink"></label>
                                                            </td>    
                                                            <td>&nbsp;<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right">
                                                                <label id="lblAvailableStock" runat="server" class="lblAvailableStock lblLink"></label>
                                                            </td>    
                                                            <td>&nbsp<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("PurchaseRequestQty")%></td>
                                                            <td>&nbsp;<%# Eval("cfPurchaseRequestItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("PurchaseRequestReceivedQty")%></td>
                                                            <td>&nbsp;<%# Eval("cfPurchaseRequestReceivedItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>

                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtDistribution" Width="65px" runat="server" value="0" CssClass="number max txtDistribution" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp<label runat="server" id="lblDistributionItemUnit" class="lblDistributionItemUnit"></label>
                                                                <input type="hidden" class="hdnDistributionQty" value='<%# Eval("DistributionQty")%>' />
                                                                <input type="hidden" class="hdnGCDistributionItemUnit" id="hdnGCDistributionItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnDistributionItemUnit" id="hdnDistributionItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnDistributionConversionFactor" id="hdnDistributionConversionFactor" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                        
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtConsumption" Width="65px" runat="server" value="0" CssClass="number max txtConsumption" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp<label runat="server" id="lblConsumptionItemUnit" class="lblConsumptionItemUnit"></label>
                                                                <input type="hidden" class="hdnConsumptionQty" value='<%# Eval("ConsumptionQty")%>' />
                                                                <input type="hidden" class="hdnGCConsumptionItemUnit" id="hdnGCConsumptionItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnConsumptionItemUnit" id="hdnConsumptionItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnConsumptionConversionFactor" id="hdnConsumptionConversionFactor" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtPurchaseRequest" Width="65px" runat="server" value="0" CssClass="number txtPurchaseRequest" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp<label runat="server" id="lblPurchaseRequestItemUnit" class="lblPurchaseRequestItemUnit"></label>
                                                                <input type="hidden" class="hdnPurchaseRequestQty" value='<%# Eval("PurchaseRequestQty")%>' />
                                                                <input type="hidden" class="hdnGCPurchaseRequestItemUnit" id="hdnGCPurchaseRequestItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnPurchaseRequestItemUnit" id="hdnPurchaseRequestItemUnit" runat="server" />
                                                                <input type="hidden" class="hdnPurchaseRequestConversionFactor" id="hdnPurchaseRequestConversionFactor" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
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
</asp:Content>
