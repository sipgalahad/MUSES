<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    CodeBehind="DirectPurchaseEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseEntry" %>

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

            setDatePicker('<%=txtDirectPurchaseDate.ClientID %>');
            setDatePicker('<%=txtReferenceDate.ClientID %>');
            $('#<%=txtDirectPurchaseDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#trCustomSupplier').hide();

            //#region Direct Purchase No
            $('#lblDirectPurchaseNo.lblLink').click(function () {
                openSearchDialog('directpurchase', '', function (value) {
                    $('#<%=txtDirectPurchaseNo.ClientID %>').val(value);
                    ontxtDirectPurchaseNoChanged(value);
                });
            });

            $('#<%=txtDirectPurchaseNo.ClientID %>').change(function () {
                ontxtDirectPurchaseNoChanged($(this).val());
            });

            function ontxtDirectPurchaseNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Location
            function getLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionLocation() %>";
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
                var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
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
                        if (result.BusinessPartnerID == $("#<%=hdnCustomSupplierID.ClientID %>").val()) {
                            $('#trCustomSupplier').show();
                        } else {
                            $('#trCustomSupplier').hide();
                        }
                        $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                        $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                    }
                    else {
                        $('#<%=hdnSupplierID.ClientID %>').val('');
                        $('#<%=txtSupplierCode.ClientID %>').val('');
                        $('#<%=txtSupplierName.ClientID %>').val('');
                        $('#trCustomSupplier').hide();
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
                var purchaseID = $('#<%=hdnDirectPurchaseID.ClientID %>').val();
                if ($('#<%=txtItemGroupCode.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath like '%/" + $('#<%=hdnItemGroupID.ClientID %>').val() + "/%')";
                if (purchaseID != '')
                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM DirectPurchaseDt WHERE DirectPurchaseID = " + purchaseID + "AND GCItemDetailStatus != 'X121^999')";
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
                        Methods.getItemMasterPurchase(result.ItemID, $('#<%=hdnSupplierID.ClientID %>').val(), function (result2) {
                            if (result2 != null) {
                                $('#<%=hdnItemGroupID.ClientID %>').val(result2.ItemGroupID);
                                $('#<%=txtItemGroupCode.ClientID %>').val(result2.ItemGroupCode);
                                $('#<%=txtItemGroupName.ClientID %>').val(result2.ItemGroupName1);
                                $('#<%=txtDiscountPercentage.ClientID %>').val(result2.Discount);
                                $('#<%=hdnUnitPrice.ClientID %>').val(result2.Price);
                                $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                                $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);

                                var qty = parseFloat($('#<%=txtQuantity.ClientID %>').val());
                                var discountAmount = qty * result2.Price * result2.Discount / 100;
                                $('#<%=txtDiscountAmount.ClientID %>').val(discountAmount).trigger('changeValue');
                            }
                            else {
                                $('#<%=txtDiscountPercentage.ClientID %>').val('0');
                                $('#<%=txtDiscountAmount.ClientID %>').val('0');
                                $('#<%=hdnUnitPrice.ClientID %>').val('0');
                            }
                        });
                        cboItemUnit.PerformCallback();
                    }
                    else {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                        $('#<%=hdnItemID.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
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
                    $('#<%=txtItemGroupCode.ClientID %>').val('');
                    $('#<%=txtItemGroupName.ClientID %>').val('');
                    $('#<%=txtItemCode.ClientID %>').val('');
                    $('#<%=txtItemName.ClientID %>').val('');
                    $('#<%=hdnUnitPrice.ClientID %>').val('0');
                    $('#<%=txtPrice.ClientID %>').val('').trigger('changeValue'); ;
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtDiscountPercentage.ClientID %>').val('0');
                    $('#<%=txtDiscountAmount.ClientID %>').val('0');
                    $('#<%=txtLineAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=lblSupplier.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSupplierCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtLocationCode.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=lblLocation.ClientID %>').attr('class', 'lblDisabled');
                    lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                    editedLineAmount = 0;
                    cboItemUnit.SetValue('');
                    cboDirectPurchaseType.SetEnabled(false);
                    $('#<%=txtConversion.ClientID %>').val('');

                    $('#entryDetailContainer').show();
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
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

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
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
            $('#<%=hdnUnitPrice.ClientID %>').val(parseFloat(entity.UnitPrice) / parseFloat(entity.ConversionFactor));
            $('#<%=txtDiscountPercentage.ClientID %>').val(entity.DiscountPercentage).trigger('changeValue');
            $('#<%=txtDiscountAmount.ClientID %>').val(entity.DiscountAmount).trigger('changeValue');
            $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
            $('#<%=txtItemCode.ClientID %>').val(entity.ItemCode);
            $('#<%=txtItemName.ClientID %>').val(entity.ItemName1);
            $('#<%=hdnItemGroupID.ClientID %>').val(entity.ItemGroupID);
            $('#<%=txtItemGroupCode.ClientID %>').val(entity.ItemGroupCode);
            $('#<%=txtItemGroupName.ClientID %>').val(entity.ItemGroupName1);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);

            lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            editedLineAmount = entity.LineAmount;

            cboItemUnit.PerformCallback();
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onAfterCustomClickSuccess(type, retval) {
            onLoadObject(retval);
        }

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
            $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga - discountAmount).trigger('changeValue');
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

        function onAfterSaveRecordDtSuccess(PurchaseID) {
            if ($('#<%=hdnDirectPurchaseID.ClientID %>').val() == '0') {
                $('#<%=hdnDirectPurchaseID.ClientID %>').val(PurchaseID);
                var filterExpression = 'DirectPurchaseID = ' + PurchaseID;
                Methods.getObject('GetDirectPurchaseHdList', filterExpression, function (result) {
                    $('#<%=txtDirectPurchaseNo.ClientID %>').val(result.DirectPurchaseNo);
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
                    var PurchaseID = s.cpPurchaseID;
                    onAfterSaveRecordDtSuccess(PurchaseID);
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

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var ID = $('#<%=hdnDirectPurchaseID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (ID == '' || ID == '0') {
                    errMessage.text = 'Please Save Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "DirectPurchaseID = " + ID;
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
            var ID = $('#<%=hdnDirectPurchaseID.ClientID %>').val();
            var param = ID + '|' + entity.ItemID;
            var url = ResolveUrl("~/Program/Procurement/DirectPurchase/DirectPurchasePRDtCtl.ascx");
            openUserControlPopup(url, param, 'Purchase Request Detail', 650, 500);
        });
    </script>
    <input type="hidden" value="" id="hdnCustomSupplierID" runat="server" />
    <input type="hidden" value="false" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnDirectPurchaseID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="" id="hdnGCTransactionStatus" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
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
                            <td class="tdLabel"><label class="lblLink" id="lblDirectPurchaseNo"><%=GetLabel("No. Pembelian")%></label></td>
                            <td><asp:TextBox ID="txtDirectPurchaseNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Pembelian") %></td>
                            <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtDirectPurchaseDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblSupplier" runat="server"><%=GetLabel("Supplier/Penyedia")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 5px" />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtSupplierCode" CssClass="required" ValidationGroup="mpEntry" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trCustomSupplier">
                            <td class="tdLabel"><label class="lbNormal" id="lblSupplierName"><%=GetLabel("Nama Supplier")%></label></td>
                            <td><asp:TextBox ID="txtCustomSupplierName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr id="hdnLocation" runat="server">
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocation"><%=GetLabel("Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Pembelian")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboDirectPurchaseType" ClientInstanceName="cboDirectPurchaseType" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Referensi")%></label></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Referensi")%></label></td>
                            <td><asp:TextBox ID="txtReferenceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                            <table>
                                                <colgroup>
                                                    <col style="width: 120px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblLink" id="lblItemGroup"><%=GetLabel("Kelompok Item")%></label></td>
                                                    <td>
                                                        <input type="hidden" value="" id="hdnItemGroupID" runat="server" />
                                                        <table style="width:70%" cellpadding="0" cellspacing="0">
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
                                                        <table style="width:70%" cellpadding="0" cellspacing="0">
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
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah")%></label></td>
                                                    <td><asp:TextBox ID="txtQuantity" Width="120px" CssClass="number" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Satuan Item")%></label></td>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                            Width="300px" OnCallback="cboItemUnit_Callback">
                                                            <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" 
                                                                ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
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
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Harga")%></label></td>
                                                    <td>
                                                        <table style="width:70%" cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 30%" />
                                                                <col style="width: 3px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtPrice" CssClass="txtCurrency" Width="180px" runat="server" /></td>
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
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
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
                                                <asp:BoundField DataField="CustomConversion" HeaderText="Konversi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="200px" />
                                                <asp:BoundField DataField="DiscountAmount" HeaderText="Diskon" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="150px" DataFormatString="{0:N}" />
                                                <asp:BoundField DataField="LineAmount" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="150px" DataFormatString="{0:N}" />
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
                                                        <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                        <input type="hidden" value="<%#Eval("DiscountPercentage") %>" bindingfield="DiscountPercentage" />
                                                        <input type="hidden" value="<%#Eval("DiscountAmount") %>" bindingfield="DiscountAmount" />
                                                        <input type="hidden" value="<%#Eval("ConversionFactor") %>" bindingfield="ConversionFactor" />
                                                        <input type="hidden" value="<%#Eval("GCItemDetailStatus") %>" bindingfield="GCItemDetailStatus" />
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
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="containerTotalPembelian" style="margin-top: 20px;">
                        <fieldset id="fsTotalOrder" style="margin: 0">
                            <table style="width: 100%;">
                                <colgroup>
                                    <col style="width: 50%" />
                                    <col style="width: 40px" />
                                </colgroup>
                                <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td align="right">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col style="width: 180px" />
                                                <col style="width: 50px" />
                                                <col style="width: 10px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Pembelian Tunai")%></label></td>
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
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Pembelian Tunai")%></label></td>
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
