<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="PurchaseRequestOutstandingDetail.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseRequestOutstandingDetail" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnProcessPurchaseRequest" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/list.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Process")%></div></li>
    <li id="btnAddToPOPurchaseRequest" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/list.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Add To PO")%></div></li>
    <li id="btnOrderListBack" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><div><%=GetLabel("Back")%></div></li>
    <li id="btnPurchaseRequestHdDecline" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Decline")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        var flag = true;
        function onLoad() {
            $('.txtNonMasterSupplierName').hide();

            $('#<%=btnProcessPurchaseRequest.ClientID %>').click(function () {
                var errMessage = { text: '' };
                getCheckedMember(errMessage);
                if (errMessage.text == '') {
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                        showToast('Warning', 'Please Select Item First');
                    else
                        onCustomButtonClick('approve');
                }
                else
                    showToast('Warning', errMessage.text);
            });

            $('#<%=btnAddToPOPurchaseRequest.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    showToast('Warning', 'Please Select Item First');
                else {
                    var url = ResolveUrl("~/Program/Procurement/PurchaseRequest/Outstanding/PurchaseRequestAddToPOCtl.ascx");
                    openUserControlPopup(url, $('#<%=hdnPurchaseRequestID.ClientID %>').val(), 'Add To PO', 900, 500);
                }
            });

            $('#<%=btnPurchaseRequestHdDecline.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    showToast('Warning', 'Please Select Item First');
                else
                    onCustomButtonClick('decline');
            });

            $('#<%=btnOrderListBack.ClientID %>').click(function () {
                showLoadingPanel();
                document.location = ResolveUrl('~/Program/Procurement/PurchaseRequest/Outstanding/PurchaseRequestOutstandingList.aspx');
            });

            //#region Location
            function getLocationFilterExpression() {
                if ($('#<%=hdnSiteServiceUnitID.ClientID %>').val() != "") {
                    var filterExpression = "<%=OnGetFilterExpressionLocation() %>LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = " + $('#<%=hdnSiteServiceUnitID.ClientID %>').val() + " AND IsHeader = 0)";
                    return filterExpression;
                }
                return "1 = 0";
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
        }

        function getCheckedMember(errMessage) {
            var lstGCPurchaseUnit = $('#<%=hdnListGCPurchaseUnit.ClientID %>').val().split('|');
            var lstPurchaseUnit = $('#<%=hdnListPurchaseUnit.ClientID %>').val().split('|');
            var lstSupplierID = $('#<%=hdnListSupplierID.ClientID %>').val().split('|');
            var lstSupplierName = $('#<%=hdnListSupplierName.ClientID %>').val().split('|');
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split('|');
            var lstPurchaseOrderQty = $('#<%=hdnPurchaseOrderQty.ClientID %>').val().split('|');
            var lstPrice = $('#<%=hdnPrice.ClientID %>').val().split('|');
            var lstDiscount1 = $('#<%=hdnDiscount1.ClientID %>').val().split('|');
            var lstDiscount2 = $('#<%=hdnDiscount2.ClientID %>').val().split('|');
            var lstConversionFactor = $('#<%=hdnListConversionFactor.ClientID %>').val().split('|');
            var lstTermID = $('#<%=hdnListTermID.ClientID %>').val().split('|');
            var lstSupplierItemName = $('#<%=hdnListSupplierItemName.ClientID %>').val().split('|');
            var lstGCPurchaseMethod = $('#<%=hdnListGCPurchaseMethod.ClientID %>').val().split('|');
            var lstNonMasterSupplierName = $('#<%=hdnListNonMasterSupplierName.ClientID %>').val().split('|');
            var lstIsFromMasterSupplier = $('#<%=hdnListIsFromMasterSupplier.ClientID %>').val().split('|');

            var result = '';
            var itemEmptySupplier = '';
            var itemEmptyQty = '';

            $('.grdPurchaseRequest .chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').html();
                    var supplierID = $tr.find('.hdnSupplierID').val();
                    var itemName = $tr.find('.tdItemName').html();
                    var supplierName = $tr.find('.lblSupplier').html();
                    var GCPurchaseUnit = $tr.find('.hdnGCPurchaseUnit').val();
                    var purchaseUnit = $tr.find('.lblPurchaseUnit').html();
                    var purchaseQty = $tr.find('.txtPurchaseQty').val();
                    var price = $tr.find('.txtPrice').val();
                    var discount1 = $tr.find('.txtDiscount1').val();
                    var discount2 = $tr.find('.txtDiscount2').val();
                    var conversionFactor = $tr.find('.hdnConversionFactor').val();
                    var termID = $tr.find('.hdnTermID').val();
                    var supplierItemName = $tr.find('.tdSupplierItemName').html();
                    var idx = $tr.find('.hdnItemIndex').val();
                    var cboPurchaseMethod = eval('cboPurchaseMethod' + idx);
                    var purchaseMethod = cboPurchaseMethod.GetValue();
                    var nonMasterSupplierName = $tr.find('.txtNonMasterSupplierName').val();
                    var isFromMasterSupplier = $tr.find('.chkIsFromMasterSupplier input').is(':checked') ? '1' : '0';
                    
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx < 0) {
                        lstSelectedMember.push(key);
                        lstSupplierID.push(supplierID);
                        lstSupplierName.push(supplierName);
                        lstGCPurchaseUnit.push(GCPurchaseUnit);
                        lstPurchaseUnit.push(purchaseUnit);
                        lstPurchaseOrderQty.push(purchaseQty);
                        lstPrice.push(price);
                        lstDiscount1.push(discount1);
                        lstDiscount2.push(discount2);
                        lstConversionFactor.push(conversionFactor);
                        lstTermID.push(termID);
                        lstSupplierItemName.push(supplierItemName);
                        lstGCPurchaseMethod.push(purchaseMethod);
                        lstNonMasterSupplierName.push(nonMasterSupplierName);
                        lstIsFromMasterSupplier.push(isFromMasterSupplier);
                    }
                    else {
                        lstDiscount1[idx] = discount1;
                        lstDiscount2[idx] = discount2;
                        lstPrice[idx] = price;
                        lstPurchaseOrderQty[idx] = purchaseQty;
                        lstSupplierName[idx] = supplierName;
                        lstSupplierID[idx] = supplierID;
                        lstGCPurchaseUnit[idx] = GCPurchaseUnit;
                        lstPurchaseUnit[idx] = purchaseUnit;
                        lstConversionFactor[idx] = conversionFactor;
                        lstTermID[idx] = termID;
                        lstSupplierItemName[idx] = supplierItemName;
                        lstGCPurchaseMethod[idx] = purchaseMethod;
                        lstNonMasterSupplierName[idx] = nonMasterSupplierName;
                        lstIsFromMasterSupplier[idx] = isFromMasterSupplier;
                    }
                    if (supplierID == '0') {
                        if (itemEmptySupplier != '')
                            itemEmptySupplier += ', ';
                        itemEmptySupplier += '<b>' + itemName + '</b>';
                    }
                    if (purchaseQty == '0') {
                        if (itemEmptyQty != '')
                            itemEmptyQty += ', ';
                        itemEmptyQty += '<b>' + itemName + '</b>';
                    }
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').html();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx > -1) {
                        lstSelectedMember.splice(idx, 1);
                        lstPurchaseOrderQty.splice(idx, 1);
                        lstPrice.splice(idx, 1);
                        lstSupplierID.splice(idx, 1);
                        lstSupplierName.splice(idx, 1);
                        lstDiscount1.splice(idx, 1);
                        lstDiscount2.splice(idx, 1);
                        lstGCPurchaseUnit.splice(idx, 1);
                        lstPurchaseUnit.splice(idx, 1);
                        lstConversionFactor.splice(idx, 1);
                        lstTermID.splice(idx, 1);
                        lstSupplierItemName.splice(idx, 1);
                        lstGCPurchaseMethod.splice(idx, 1);
                        lstNonMasterSupplierName.splice(idx, 1);
                        lstIsFromMasterSupplier.splice(idx, 1);
                    }
                }
            });
            if (errMessage != null) {
                if (itemEmptySupplier != '')
                    errMessage.text = 'Silakan Pilih Supplier Untuk Item ' + itemEmptySupplier + ' Terlebih Dahulu';
                if (itemEmptyQty != '') {
                    if (errMessage.text != '')
                        errMessage.text += '<br>';
                    errMessage.text += 'Silakan Isi Qty Untuk Item ' + itemEmptySupplier + ' Terlebih Dahulu';
                }

                var isDirectPurchaseExists = false;
                for (var i = 1; i < lstGCPurchaseMethod.length; ++i) {
                    if (lstGCPurchaseMethod[i] == "<%=OnGetPurchaseMethodDirectPurchase() %>") {
                        isDirectPurchaseExists = true;
                        break;
                    }
                }
                if (isDirectPurchaseExists && $('#<%=hdnLocationID.ClientID %>').val() == '')
                    errMessage.text += 'Lokasi Pembelian Tunai Wajib Diisi';
            }
            $('#<%=hdnListGCPurchaseUnit.ClientID %>').val(lstGCPurchaseUnit.join('|'));
            $('#<%=hdnListPurchaseUnit.ClientID %>').val(lstPurchaseUnit.join('|'));
            $('#<%=hdnListSupplierID.ClientID %>').val(lstSupplierID.join('|'));
            $('#<%=hdnListConversionFactor.ClientID %>').val(lstConversionFactor.join('|'));
            $('#<%=hdnListSupplierName.ClientID %>').val(lstSupplierName.join('|'));
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join('|'));
            $('#<%=hdnPurchaseOrderQty.ClientID %>').val(lstPurchaseOrderQty.join('|'));
            $('#<%=hdnPrice.ClientID %>').val(lstPrice.join('|'));
            $('#<%=hdnDiscount1.ClientID %>').val(lstDiscount1.join('|'));
            $('#<%=hdnDiscount2.ClientID %>').val(lstDiscount2.join('|'));
            $('#<%=hdnListTermID.ClientID %>').val(lstTermID.join('|'));
            $('#<%=hdnListSupplierItemName.ClientID %>').val(lstSupplierItemName.join('|'));
            $('#<%=hdnListGCPurchaseMethod.ClientID %>').val(lstGCPurchaseMethod.join('|'));
            $('#<%=hdnListNonMasterSupplierName.ClientID %>').val(lstNonMasterSupplierName.join('|'));
            $('#<%=hdnListIsFromMasterSupplier.ClientID %>').val(lstIsFromMasterSupplier.join('|'));
        }

        function onAfterCustomClickSuccess(type,retval) {
            var param = retval.split('|');
            var orderPerSupplier = param[0].split(';');
            var tempText = "";
            for (var a = 0; a < orderPerSupplier.length - 1; a++) {
                var paramDetail = orderPerSupplier[a].split('^');
                if (tempText != '')
                    tempText += "<br />";
                if (paramDetail[0] == '1')
                    tempText += "Pemesanan Barang Untuk Supplier <b>" + paramDetail[2] + "</b> Dengan Nomor Pemesanan <b>" + paramDetail[1] + "</b>";
                else
                    tempText += "Pembelian Tunai Untuk Supplier <b>" + paramDetail[2] + "</b> Dengan Nomor Pembelian <b>" + paramDetail[1] + "</b>";
            }
            showToast('Save Success', tempText, function () {
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                $('#<%=hdnListSupplierID.ClientID %>').val('');
                $('#<%=hdnListConversionFactor.ClientID %>').val('');
                $('#<%=hdnListSupplierName.ClientID %>').val('');
                $('#<%=hdnListGCPurchaseUnit.ClientID %>').val('');
                $('#<%=hdnListPurchaseUnit.ClientID %>').val('');
                $('#<%=hdnPurchaseOrderQty.ClientID %>').val('');
                $('#<%=hdnDiscount1.ClientID %>').val('');
                $('#<%=hdnDiscount2.ClientID %>').val('');
                $('#<%=hdnPrice.ClientID %>').val('');
                $('#<%=hdnListTermID.ClientID %>').val('');
                $('#<%=hdnListSupplierItemName.ClientID %>').val('');
                $('#<%=hdnListGCPurchaseMethod.ClientID %>').val('');
                $('#<%=hdnListNonMasterSupplierName.ClientID %>').val('');
                $('#<%=hdnListIsFromMasterSupplier.ClientID %>').val('');
                if (param[1] == '0') $('#<%=btnOrderListBack.ClientID %>').click();
                cbpView.PerformCallback('refresh');
            });
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            $('#<%=hdnListSupplierID.ClientID %>').val('');
            $('#<%=hdnListConversionFactor.ClientID %>').val('');
            $('#<%=hdnListSupplierName.ClientID %>').val('');
            $('#<%=hdnListGCPurchaseUnit.ClientID %>').val('');
            $('#<%=hdnListPurchaseUnit.ClientID %>').val('');
            $('#<%=hdnPurchaseOrderQty.ClientID %>').val('');
            $('#<%=hdnDiscount1.ClientID %>').val('');
            $('#<%=hdnDiscount2.ClientID %>').val('');
            $('#<%=hdnPrice.ClientID %>').val('');
            $('#<%=hdnListTermID.ClientID %>').val('');
            $('#<%=hdnListSupplierItemName.ClientID %>').val('');
            $('#<%=hdnListGCPurchaseMethod.ClientID %>').val('');
            $('#<%=hdnListNonMasterSupplierName.ClientID %>').val('');
            $('#<%=hdnListIsFromMasterSupplier.ClientID %>').val('');
            if (param == '0') $('#<%=btnOrderListBack.ClientID %>').click();
            else cbpView.PerformCallback('refresh');
        }

        $('.chkIsFromMasterSupplier').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboPurchaseMethod = eval('cboPurchaseMethod' + idx);
            if ($(this).find('input').is(':checked')) {
                $tr.find('.lblSupplier').show();
                $tr.find('.txtNonMasterSupplierName').hide();
                cboPurchaseMethod.SetEnabled(true);
            }
            else {
                $tr.find('.hdnSupplierID').val($("#<%=hdnNonMasterSupplierID.ClientID %>").val());
                $tr.find('.lblSupplier').hide();
                $tr.find('.txtNonMasterSupplierName').show();
                cboPurchaseMethod.SetEnabled(false);
                cboPurchaseMethod.SetValue('<%=OnGetPurchaseMethodDirectPurchase() %>');
            }
        });

        $('.chkIsSelected').live('change', function () {
            $tr = $(this).closest('tr');
            $lblSupplier = $tr.find('.lblSupplier');
            $lblPurchaseUnit = $tr.find('.lblPurchaseUnit');
            if ($(this).find('input').is(':checked')) {
                $tr.find('.txtPrice').removeAttr('readonly');
                $tr.find('.txtDiscount1').removeAttr('readonly');
                $tr.find('.txtDiscount2').removeAttr('readonly');
                $tr.find('.txtPurchaseQty').removeAttr('readonly');
                $tr.find('.txtNonMasterSupplierName').removeAttr('readonly');
                $tr.find('.chkIsFromMasterSupplier input').removeAttr("disabled");
                $lblSupplier.removeClass('lblDisabled');
                $lblSupplier.addClass('lblLink');
                $lblPurchaseUnit.removeClass('lblDisabled');
                $lblPurchaseUnit.addClass('lblLink');
            }
            else {
                $tr.find('.txtPrice').attr('readonly', 'readonly');
                $tr.find('.txtDiscount1').attr('readonly', 'readonly');
                $tr.find('.txtDiscount2').attr('readonly', 'readonly');
                $tr.find('.txtPurchaseQty').attr('readonly', 'readonly');
                $tr.find('.txtNonMasterSupplierName').attr('readonly', 'readonly');
                $tr.find('.chkIsFromMasterSupplier input').attr("disabled", true);
                $lblSupplier.removeClass('lblLink');
                $lblSupplier.addClass('lblDisabled');
                $lblPurchaseUnit.removeClass('lblLink');
                $lblPurchaseUnit.addClass('lblDisabled');
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

        $('.lblStock.lblLink').live('click', function () {
            var itemID = $(this).closest('tr').parent().closest('tr').find('.hdnItemID').val();
            var locationID = $('#<%=hdnLstLocationID.ClientID %>').val();
            if (itemID != '' && locationID != '') {
                var param = itemID + '|' + locationID;
                var url = ResolveUrl("~/Program/Information/ItemBalanceDtCtl.ascx");
                openUserControlPopup(url, param, 'Item Per Lokasi', 700, 500);
            }
        });

        $('.lblQtyOnOrder.lblLink').live('click', function () {
            var itemID = $(this).closest('tr').parent().closest('tr').find('.hdnItemID').val();
            var siteServiceUnitID = $('#<%=hdnSiteServiceUnitID.ClientID %>').val();
            if (itemID != '' && siteServiceUnitID != '') {
                var param = siteServiceUnitID + '|' + itemID;
                var url = ResolveUrl("~/Program/Procurement/PurchaseRequest/Outstanding/PODPQtyOnOrderCtl.ascx");
                openUserControlPopup(url, param, 'Qty On Order', 1200, 500);
            }
        }); 

        //#region Supplier
        function getSupplierFilterExpression() {
            var filterExpression = "<%=filterExpressionSupplier %>";
            return filterExpression;
        }

        $td = null;
        $('.lblSupplier.lblLink').live('click', function () {
            $td = $(this).parent();
            openSearchDialog('businesspartners', getSupplierFilterExpression(), function (value) {
                onTxtSupplierChanged(value);
            });
        });

        function onTxtSupplierChanged(value) {
            var filterExpression = getSupplierFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
            Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
                if (result != null) {
                    $td.find('.hdnSupplierID').val(result.BusinessPartnerID);
                    $td.find('.hdnTermID').val(result.TermID);
                    $td.find('.lblSupplier').html(result.BusinessPartnerName);

                    var itemID = $td.parent().find('.hdnItemID').val();
                    filterExpression = 'BusinessPartnerID = ' + result.BusinessPartnerID + ' AND ItemID = ' + itemID + ' AND IsDeleted = 0';
                    Methods.getObject('GetSupplierItemList', filterExpression, function (result) {
                        if (result != null)
                            $td.parent().find('.tdSupplierItemName').html(result.cfSupplierItem);
                        else
                            $td.parent().find('.tdSupplierItemName').html('');
                    });
                }
                else {
                    $td.find('.hdnSupplierID').val('0');
                    $td.find('.hdnTermID').val('0');
                    $td.find('.lblSupplier').html('');
                    $td.parent().find('.tdSupplierItemName').html('');
                }
            });
        }
        //#endregion

        //#region Purchase Unit
        function getPurchaseUnitFilterExpression() {
            var filterExpression = "ItemID = " + itemID;
            return filterExpression;
        }

        var itemID = 0;
        $('.lblPurchaseUnit.lblLink').live('click', function () {
            $td = $(this).parent();
            itemID = $td.closest('tr').parent().closest('tr').find('.hdnItemID').val();
            openSearchDialog('itemalternateunit', getPurchaseUnitFilterExpression(), function (value) {
                onTxtPurchaseUnitChanged(value);
            });
        });

        function onTxtPurchaseUnitChanged(value) {
            var temp = value.split('|');
            var filterExpression = getPurchaseUnitFilterExpression() + " AND GCAlternateUnit = '" + temp[0] + "' AND ConversionFactor = " + temp[1];
            Methods.getObject('GetvItemAlternateUnitCustomList', filterExpression, function (result) {
                $lblPurchaseUnitPrice = $td.parent().find('.lblPurchaseUnitPrice');
                if (result != null) {
                    $td.find('.hdnGCPurchaseUnit').val(result.GCAlternateUnit);
                    $td.find('.lblPurchaseUnit').html(result.cfAlternateUnit);
                    $td.find('.hdnConversionFactor').val(result.ConversionFactor);
                    $lblPurchaseUnitPrice.html(result.AlternateUnit);
                }
                else {
                    $td.find('.hdnGCPurchaseUnit').val('');
                    $td.find('.lblPurchaseUnit').html('');
                    $td.find('.hdnConversionFactor').val('');
                    $lblPurchaseUnitPrice.html('');
                }
            });
        }
        //#endregion

        $(function () {
            $('#btnItemBalanceDt').click(function () {
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
    <input type="hidden" value="" id="hdnNonMasterSupplierID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultDirectPurchaseType" runat="server" />
    <input type="hidden" value="" id="hdnDefaultPurchaseOrderType" runat="server" />
    <input type="hidden" value="" id="hdnDefaultFrancoRegion" runat="server" />
    <input type="hidden" value="" id="hdnDefaultCurrencyCode" runat="server" />
    
    <input type="hidden" value="" id="hdnListConversionFactor" runat="server" />
    <input type="hidden" value="" id="hdnPurchaseOrderQty" runat="server" />
    <input type="hidden" value="" id="hdnDiscount1" runat="server" />
    <input type="hidden" value="" id="hdnDiscount2" runat="server" />
    <input type="hidden" value="" id="hdnPrice" runat="server" />
    <input type="hidden" value="" id="hdnListSupplierID" runat="server" />
    <input type="hidden" value="" id="hdnListSupplierName" runat="server" />
    <input type="hidden" value="" id="hdnListIsFromMasterSupplier" runat="server" />
    <input type="hidden" value="" id="hdnListNonMasterSupplierName" runat="server" />
    <input type="hidden" value="" id="hdnListGCPurchaseUnit" runat="server" />
    <input type="hidden" value="" id="hdnListPurchaseUnit" runat="server" />
    <input type="hidden" value="" id="hdnPurchaseRequestID" runat="server" />
    <input type="hidden" value="" id="hdnListTermID" runat="server" />
    <input type="hidden" value="" id="hdnListSupplierItemName" runat="server" />
    <input type="hidden" value="" id="hdnListGCPurchaseMethod" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterLocationItemGroup" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <div style="height: 550px; overflow-y: auto; overflow-x: hidden;">
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
                            <td class="tdLabel"><label class="lblNormal" runat="server"><%=GetLabel("Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtServiceUnitCode" Width="100%" runat="server" ReadOnly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
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
                            <td class="tdLabel"><label class="lblLink" runat="server" id="lblLocation"><%=GetLabel("Lokasi Beli Tunai")%></label></td>
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
                            <td>&nbsp;</td>
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
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" runat="server" ReadOnly="true"/></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemOrderTime" Width="100px" CssClass="time" runat="server" Style="text-align: center" ReadOnly="true"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" ReadOnly="true" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdPurchaseRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">&nbsp;</th>
                                                    <th rowspan="2" style="width: 20px" align="center"><input id="chkSelectAll" type="checkbox" /></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Barang")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("SUPPLIER")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("PROSES PEMESANAN")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 150px" class="thCenter"><%=GetLabel("Konversi")%></th>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 90px" class="thCenter"><%=GetLabel("Qty On Order")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Name")%></th>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Kode Item")%></th>
                                                    <th style="width: 60px" class="thCenter"><%=GetLabel("Cara")%></th>
                                                    <th style="width: 100px;" class="thCenter"><%=GetLabel("Pesan")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Harga Satuan")%></th>
                                                    <th style="width: 65px" class="thCenter"><%=GetLabel("Disc. 1")%></th>
                                                    <th style="width: 65px" class="thCenter"><%=GetLabel("Disc. 2")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="20">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdPurchaseRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">&nbsp;</th>
                                                    <th rowspan="2" style="width: 20px" align="center"><input id="chkSelectAll" type="checkbox" /></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Barang")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("SUPPLIER")%></th>
                                                    <th colspan="4" class="thCenter"><%=GetLabel("PROSES PEMESANAN")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 150px" class="thCenter"><%=GetLabel("Konversi")%></th>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 90px" class="thCenter"><%=GetLabel("Qty On Order")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Name")%></th>
                                                    <th style="width: 80px" class="thCenter"><%=GetLabel("Kode Item")%></th>
                                                    <th style="width: 60px" class="thCenter"><%=GetLabel("Cara")%></th>
                                                    <th style="width: 100px;" class="thCenter"><%=GetLabel("Pesan")%></th>
                                                    <th style="width: 130px" class="thCenter"><%=GetLabel("Harga Satuan")%></th>
                                                    <th style="width: 65px" class="thCenter"><%=GetLabel("Disc. 1")%></th>
                                                    <th style="width: 65px" class="thCenter"><%=GetLabel("Disc. 2")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%# Eval("ID")%></td>
                                                <td align="center"><asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" /></td>
                                                <td class="tdItemName"><%# Eval("ItemName1")%></td>
                                                <td align="right" style="padding-top:10px">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("Quantity")%></td>
                                                            <td>&nbsp<%# Eval("PurchaseUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="left" style="padding-top:10px"><%# Eval("CustomConversion")%></td>
                                                <td align="center" style="padding-top:10px">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right"><label id="lblStock" runat="server" class="lblStock lblLink"></label></td>
                                                            <td>&nbsp<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="center" style="padding-top:10px">  
                                                    <input type="hidden" class="hdnItemID" value='<%#Eval("ItemID") %>' />
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right"><label id="lblQtyOnOrder" runat="server" class="lblQtyOnOrder lblLink"></label></td>
                                                            <td>&nbsp<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkIsFromMasterSupplier" CssClass="chkIsFromMasterSupplier" runat="server" Text="Dari Master" /><br />
                                                    <input type="hidden" value="0" class="hdnSupplierID" id="hdnSupplierID" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnTermID" id="hdnTermID" runat="server"/>
                                                    <label runat="server" id="lblSupplier" class="lblSupplier"></label>
                                                    <asp:TextBox runat="server" CssClass="txtNonMasterSupplierName" ID="txtNonMasterSupplierName" Width="100%" />
                                                </td>
                                                <td id="tdSupplierItemName" runat="server" class="tdSupplierItemName"><%# Eval("cfSupplierItem")%></td>
                                                <td>
                                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                                    <dxe:ASPxComboBox ID="cboPurchaseMethod" runat="server" Width="100%" />
                                                </td>
                                                <td align="right">
                                                    <input type="hidden" value="0" class="hdnGCPurchaseUnit" id="hdnGCPurchaseUnit" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnConversionFactor" id="hdnConversionFactor" runat="server"/>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtPurchaseQty" Width="50px" runat="server" value="0" CssClass="number txtPurchaseQty" ReadOnly="true"/></td>
                                                            <td>&nbsp;<label runat="server" id="lblPurchaseUnit" class="lblPurchaseUnit"></label> </td>
                                                        </tr>
                                                    </table>                                                    
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtPrice" Width="75px" runat="server" value="0" CssClass="txtCurrency txtPrice" ReadOnly="true"/> / </td>
                                                            <td><label runat="server" id="lblPurchaseUnitPrice" class="lblPurchaseUnitPrice"></label></td>
                                                        </tr>
                                                    </table>                                                                                                        
                                                </td>
                                                <td align="right"><asp:TextBox ID="txtDiscount1" Width="65%" runat="server" value="0" CssClass="txtCurrency txtDiscount1" ReadOnly="true"/>&nbsp; %</td>
                                                <td align="right"><asp:TextBox ID="txtDiscount2" Width="65%" runat="server" value="0.00" CssClass="txtCurrency txtDiscount2" ReadOnly="true"/>&nbsp; %</td>
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
