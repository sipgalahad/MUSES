<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="DirectSalesEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.DirectSalesEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomQuickMenu" runat="server">
    <li id="btnPaymentEntry" runat="server" CRUDMode="C"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><br style="clear:both"/> <div><%=GetLabel("Payment")%></div></li>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/inlineEditing-1.0.js")%>'></script>
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1')
                $('#divTransactionAdd').show();
            else 
                $('#divTransactionAdd').hide();

            if (!getIsAdd())
                $('#entryDetailContainer').hide();

            setDatePicker('<%=txtSalesUnitDate.ClientID %>');
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            $('#<%=txtSalesUnitDate.ClientID %>').datepicker('option', 'maxDate', '0');

            calculateTotal();

            $('#<%=btnPaymentEntry.ClientID %>').click(function () {
                var id = $('#<%=hdnPRID.ClientID %>').val();
                if (id == '' || id == '0')
                    showToast('Warning', 'Silakan Pilih No Faktur Terlebih Dahulu');
                else {
                    var url = '';
                    if ($('#<%=hdnIsClosed.ClientID %>').val() == '0')
                        url = ResolveUrl("~/Program/Transaction/DirectSales/DirectPaymentEntryCtl.ascx");
                    else
                        url = ResolveUrl("~/Program/Transaction/DirectSales/DirectPaymentViewCtl.ascx");
                    openUserControlPopup(url, id, 'Payment', 1200, 550);
                }
            });

            //#region Student
            function getStudentFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionStudent() %>";
                return filterExpression;
            }

            $('#<%=lblStudent.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('student', getStudentFilterExpression(), function (value) {
                    $('#<%=txtStudentCode.ClientID %>').val(value);
                    onTxtStudentCodeChanged(value);
                });
            });

            $('#<%=txtLocationCode.ClientID %>').live('change', function () {
                onTxtStudentCodeChanged($(this).val());
            });

            function onTxtStudentCodeChanged(value) {
                var filterExpression = getStudentFilterExpression() + " AND StudentCode = '" + value + "'";
                Methods.getObject('GetStudentList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnStudentID.ClientID %>').val(result.StudentID);
                        $('#<%=txtStudentName.ClientID %>').val(result.StudentName);
                    }
                    else {
                        $('#<%=hdnStudentID.ClientID %>').val('');
                        $('#<%=txtStudentCode.ClientID %>').val('');
                        $('#<%=txtStudentName.ClientID %>').val('');
                    }
                });
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

            //#region Sales Unit No
            $('#lblSalesInvoiceNo.lblLink').click(function () {
                openSearchDialog('salesinvoicehd', "<%= OnGetFilterExpression() %>", function (value) {
                    $('#<%=txtSalesInvoiceNo.ClientID %>').val(value);
                    ontxtSalesInvoiceNoChanged(value);
                });
            });

            $('#<%=txtSalesInvoiceNo.ClientID %>').change(function () {
                ontxtSalesInvoiceNoChanged($(this).val());
            });

            function ontxtSalesInvoiceNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    editedLineAmount = 0;
                    lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                    $('#<%=txtQuantity.ClientID %>').val('1.00');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnGCItemUnit.ClientID %>').val('');
                    $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                    if (tacItem != null) {
                        tacItem.setValue('');
                        tacItem.setEnabled(true);
                        tacItem.setText('');
                    }
                    $('#<%=hdnBasePrice.ClientID %>').val(0);
                    $('#<%=txtDiscount1.ClientID %>').val('0');
                    $('#<%=txtDiscount2.ClientID %>').val('0');
                    $('#<%=txtPrice.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtPriceAfterVAT.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtBaseUnit.ClientID %>').val('');
                    $('#<%=txtBaseUnitAfterVAT.ClientID %>').val('');

                    $('#<%=hdnLineAmount.ClientID %>').val('0')
                    $('#<%=txtLineAmount.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtNotesDt.ClientID %>').val('');
                    cboItemUnit.SetValue('');
                    cboCurrency.SetEnabled(false);
                    cboFrancoRegion.SetEnabled(false);
                    cboTerm.SetEnabled(false);
                    $('#entryDetailContainer').show();
                }
            });

            $('#<%=chkPPN.ClientID %>').change(function () {
                $('#<%=txtPrice.ClientID %>').change();
            });

            $('#<%=txtFinalDiscountInPercentage.ClientID %>').change(function () {
                calculateFinalDiscount();
                calculateTotal();
            });

            $('#<%=txtFinalDiscount.ClientID %>').change(function () {
                var finalDiscountInPercentage = (parseFloat($(this).val()) / parseFloat($('#<%=txtTransactionAmountAfterVAT.ClientID %>').attr('hiddenVal'))) * 100;
                $('#<%=txtFinalDiscountInPercentage.ClientID %>').val(finalDiscountInPercentage).trigger('changeValue');
                calculateTotal();
            });

            $('#btnCancel').click(function () {
                var lineAmount = parseFloat($('#<%=hdnLineAmount.ClientID %>').val());
                var transactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                transactionAmount = transactionAmount - lineAmount + editedLineAmount;

                var vatPercentage = 0;
                if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                    vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());

                var vatAmount = transactionAmount * vatPercentage / 100;
                $('#<%=txtTransactionAmount.ClientID %>').val(transactionAmount).trigger('changeValue');
                $('#<%=txtPPN.ClientID %>').val(vatAmount).trigger('changeValue');
                $('#<%=txtTransactionAmountAfterVAT.ClientID %>').val(Math.round(transactionAmount + vatAmount)).trigger('changeValue');
                $('#entryDetailContainer').hide();
                calculateTotal();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            $('#<%=txtQuantity.ClientID %>').change(function () {
                calculateSubTotal();
            });

            $('#<%=txtPrice.ClientID %>').change(function () {
                $(this).blur();
                var vatPercentage = 0;
                if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                    vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());
                var unitPrice = parseFloat($(this).attr('hiddenVal'));
                $('#<%=txtPriceAfterVAT.ClientID %>').val(Math.round(unitPrice * (100 + vatPercentage) / 100)).trigger('changeValue');
                calculateSubTotal();
            });

            $('#<%=txtPriceAfterVAT.ClientID %>').change(function () {
                $(this).blur();
                var vatPercentage = 0;
                if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                    vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());
                var unitPriceAfterVAT = parseFloat($(this).attr('hiddenVal'));
                $('#<%=txtPrice.ClientID %>').val(unitPriceAfterVAT * 100 / (100 + vatPercentage)).trigger('changeValue');
                calculateSubTotal();
            });

            $('#<%=txtDiscount1.ClientID %>').change(function () {
                $(this).blur();
                calculateSubTotal();
            });

            $('#<%=txtDiscount2.ClientID %>').change(function () {
                $(this).blur();
                calculateSubTotal();
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });

            $('#<%=txtBarcodeEntryItem.ClientID %>').keypress(function (e) {
                var keyCode = e.keyCode || e.which;
                if (keyCode == 38 || keyCode == 40) {
                    var qty = parseFloat($('#<%=txtBarcodeEntryQty.ClientID %>').val());
                    if (keyCode == 40 && qty > 1)
                        qty--;
                    else if (keyCode == 38)
                        qty++;
                    $('#<%=txtBarcodeEntryQty.ClientID %>').val(qty);
                }
                else if (keyCode == 9 || keyCode == 13) {
                    cbpProcess.PerformCallback('barcodeentry');
                }
            });

            $('#<%=txtBarcodeEntryQty.ClientID %>').val('1');
            $('#<%=txtBarcodeEntryItem.ClientID %>').focus();
        }

        function calculateFinalDiscount() {
            var finalDiscount = (parseFloat($('#<%=txtFinalDiscountInPercentage.ClientID %>').val()) / 100) * parseFloat($('#<%=txtTransactionAmountAfterVAT.ClientID %>').attr('hiddenVal'));
            $('#<%=txtFinalDiscount.ClientID %>').val(finalDiscount).trigger('changeValue');
        }

        function onAfterSaveEditRecord() {
            cbpView.PerformCallback('refresh');
        }

        //#region Edit & Delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.TransactionDtID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            lastTransactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            editedLineAmount = parseFloat(entity.LineAmount);

            $('#<%=hdnEntryID.ClientID %>').val(entity.TransactionDtID);
            tacItem.setValue(entity.ItemID);
            tacItem.setEnabled(false);
            tacItem.setText(entity.ItemName1);
            $('#<%=txtQuantity.ClientID %>').val(entity.Quantity);
            $('#<%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
            $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
            $('#<%=txtDiscount1.ClientID %>').val(entity.DiscountPercentage1);
            $('#<%=txtDiscount2.ClientID %>').val(entity.DiscountPercentage2);

            var vatPercentage = 0;
            if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());
            $('#<%=txtPrice.ClientID %>').val(entity.UnitPrice).trigger('changeValue');
            $('#<%=txtPriceAfterVAT.ClientID %>').val(Math.round(entity.UnitPrice * (100 + vatPercentage) / 100)).trigger('changeValue');
            $('#<%=hdnBasePrice.ClientID %>').val(entity.UnitPrice);
            $('#<%=hdnLineAmount.ClientID %>').val(entity.LineAmount);
            $('#<%=txtLineAmount.ClientID %>').val(Math.round(entity.LineAmount * (100 + vatPercentage) / 100)).trigger('changeValue');
            cboItemUnit.PerformCallback();
            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Item
        function onGetItemFilterExpression() {
            var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
            var receiveID = $('#<%=hdnPRID.ClientID %>').val();
            if (receiveID != '')
                filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM SalesInvoiceDt WHERE SalesInvoiceID = " + receiveID + ")";
            return filterExpression;
        }

        function onTacItemButtonSearchClick() {
            openSearchDialog('item', onGetItemFilterExpression(), function (value) {
                var filterExpression = onGetItemFilterExpression() + " AND ItemCode = '" + value + "'";
                Methods.getObject('GetItemMasterList', filterExpression, function (result) {
                    if (result != null) {
                        tacItem.setValue(result.ItemID);
                        tacItem.setText(result.ItemName1);
                        entityToControlItem(result);
                    }
                    else {
                        tacItem.setValue('');
                        tacItem.setText('');
                    }
                });
            });

        }

        function onTacItemValueChanged() {
            var itemID = tacItem.getValue();
            if (itemID != '') {
                var filterExpression = onGetItemFilterExpression() + " AND ItemID = " + itemID;
                Methods.getObject('GetItemMasterList', filterExpression, function (result) {
                    entityToControlItem(result);
                });
            }
        }

        function getTrxDate() {
            var date = Methods.getDatePickerDate($('#<%=txtSalesUnitDate.ClientID %>').val());
            var dateInYMD = Methods.dateToYMD(date);
            return dateInYMD;
        }

        function entityToControlItem(result) {
            if (result != null) {
                var locationID = $('#<%=hdnLocationID.ClientID %>').val();
                var trxDate = getTrxDate();
                Methods.getItemMasterSales(result.ItemCode, $('#<%=hdnStudentID.ClientID %>').val(), locationID, 1, trxDate, onGetItemFilterExpression(), function (result) {
                    if (result != null) {
                        tacItem.setValue(result.ItemID);
                        tacItem.setText(result.ItemName1);
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result.GCItemUnit);
                        $('#<%=hdnBasePrice.ClientID %>').val(result.Price);
                        $('#<%=txtPrice.ClientID %>').val(result.Price).trigger('changeValue');
                        var vatPercentage = 0;
                        if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                            vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());
                        $('#<%=txtPriceAfterVAT.ClientID %>').val(Math.round(result.Price * (100 + vatPercentage) / 100)).trigger('changeValue');
                        cboItemUnit.PerformCallback();
                    }
                    else {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val('');
                        $('#<%=txtPrice.ClientID %>').val(0).trigger('changeValue');
                        $('#<%=txtPriceAfterVAT.ClientID %>').val(0).trigger('changeValue');
                        tacItem.setValue('');
                        tacItem.setText('');
                    }
                });
            }
        }
        //#endregion

        var lastTransactionAmount = 0;
        var editedLineAmount = 0;
        function calculateSubTotal() {
            var price = parseFloat($('#<%=txtPrice.ClientID %>').attr('hiddenVal'));
            var qty = parseFloat($('#<%=txtQuantity.ClientID %>').val());

            var lineAmount = price * qty;
            var discount1 = parseFloat($('#<%=txtDiscount1.ClientID %>').val());
            var discount2 = parseFloat($('#<%=txtDiscount2.ClientID %>').val());
            lineAmount = lineAmount * (100 - discount1) / 100;
            lineAmount = lineAmount * (100 - discount2) / 100;
            $('#<%=hdnLineAmount.ClientID %>').val(lineAmount);
            var transactionAmount = lastTransactionAmount - editedLineAmount + lineAmount;
            var vatPercentage = 0;
            if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());
            var lineAmount = Math.round(lineAmount * (100 + vatPercentage) / 100);
            $('#<%=txtLineAmount.ClientID %>').val(lineAmount).trigger('changeValue');
            $('#<%=txtTransactionAmount.ClientID %>').val(transactionAmount).trigger('changeValue');

            var vatAmount = transactionAmount * vatPercentage / 100;
            $('#<%=txtPPN.ClientID %>').val(vatAmount).trigger('changeValue');
            $('#<%=txtTransactionAmountAfterVAT.ClientID %>').val(Math.round(transactionAmount + vatAmount)).trigger('changeValue');
            calculateFinalDiscount();
            calculateTotal();
        }

        function calculateTotal() {
            var totalAfterVAT = parseFloat($('#<%=txtTransactionAmountAfterVAT.ClientID %>').attr('hiddenVal'));
            var Discount = parseFloat($('#<%=txtFinalDiscount.ClientID %>').attr('hiddenVal'));
            var totalHarga = totalAfterVAT - Discount;
            $('#<%=txtTransactionAmountSaldo.ClientID %>').val(totalHarga).trigger('changeValue');
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                var transactionAmount = parseFloat(param[3]);
                var vatPercentage = 0;
                if ($('#<%=chkPPN.ClientID %>').is(":checked"))
                    vatPercentage = parseFloat($('#<%=txtPPNPercentage.ClientID %>').val());

                var vatAmount = transactionAmount * vatPercentage / 100; 
                $('#<%=txtTransactionAmount.ClientID %>').val(transactionAmount).trigger('changeValue');
                $('#<%=txtPPN.ClientID %>').val(vatAmount).trigger('changeValue');
                $('#<%=txtTransactionAmountAfterVAT.ClientID %>').val(Math.round(transactionAmount + vatAmount)).trigger('changeValue');
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
            $('#divTransactionAdd').hide();
            $('#entryDetailContainer').hide();
            $('#<%=trBarcodeEntry.ClientID %>').hide();
            showWatermark('CLOSED');

            setTimeout(function () {
                openPopupPrintControl();
            }, 500);
        }

        function onAfterSaveRecordDtSuccess(PRID) {
            if ($('#<%=hdnPRID.ClientID %>').val() == '0' || $('#<%=hdnPRID.ClientID %>').val() == '') {
                $('#<%=hdnPRID.ClientID %>').val(PRID);
                $('#<%=lblLocation.ClientID %>').attr('class', 'lblDisabled');
                $('#<%=txtLocationCode.ClientID %>').attr('readonly', 'readonly');

                var filterExpression = 'SalesInvoiceID = ' + PRID;
                Methods.getObject('GetSalesInvoiceHdList', filterExpression, function (result) {
                    $('#<%=txtSalesInvoiceNo.ClientID %>').val(result.SalesInvoiceNo);
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
            else if (param[0] == 'barcodeentry') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var PRID = s.cpOrderID;
                    onAfterSaveRecordDtSuccess(PRID);
                    $('#<%=txtBarcodeEntryQty.ClientID %>').val('1');
                    $('#<%=txtBarcodeEntryItem.ClientID %>').val('');
                    $('#<%=txtBarcodeEntryItem.ClientID %>').focus();
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

        //#region cboItemUnit
        function onCboItemUnitEndCallBack() {
            if ($('#<%=hdnGCItemUnit.ClientID %>').val() == '') {
                cboItemUnit.SetValue($('#<%=hdnGCBaseUnit.ClientID %>').val());
            }
            else cboItemUnit.SetValue($('#<%=hdnGCItemUnit.ClientID %>').val());
            onCboItemUnitChanged();
        }

        function onCboItemUnitChanged() {
            var baseUnit = cboItemUnit.GetText();
            $('#<%=txtBaseUnit.ClientID %>').val('Per ' + baseUnit);
            $('#<%=txtBaseUnitAfterVAT.ClientID %>').val('Per ' + cboItemUnit.GetText());
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
            var transactionID = $('#<%=hdnPRID.ClientID %>').val();
            if (transactionID == '' || transactionID == '0') {
                errMessage.text = 'Harap memilih Nomor Faktur terlebih dahulu!';
                return false;
            }
            else {
                filterExpression.text = "site=<%= SiteID %>&type=struk&id=" + transactionID;
                return true;
            }
        }
    </script>
    <input type="hidden" value="" id="hdnPRID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnVATPercentage" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="0" id="hdnIsClosed" runat="server" />
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
                            <col style="width: 150px" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblSalesInvoiceNo"><%=GetLabel("Nomor Faktur")%></label></td>
                            <td><asp:TextBox ID="txtSalesInvoiceNo" Width="150px" ReadOnly="true" runat="server" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td><asp:TextBox ID="txtSalesUnitDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblStudent"><%=GetLabel("Siswa")%></label></td>
                            <td>
                                <input type="hidden" id="hdnStudentID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 120px" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtStudentCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtStudentName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:CheckBox ID="chkPPN" Width="100%" runat="server" Text="PPN" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocation"><%=GetLabel("Lokasi")%></label></td>
                            <td colspan="3">
                                <input type="hidden" id="hdnDefaultLocationID" value="" runat="server" />
                                <input type="hidden" id="hdnDefaultLocationCode" value="" runat="server" />
                                <input type="hidden" id="hdnDefaultLocationName" value="" runat="server" />
                                <input type="hidden" id="hdnLocationID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 120px" />
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTerm" ClientInstanceName="cboTerm" Width="100%" runat="server" /></td>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Franco")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboFrancoRegion" ClientInstanceName="cboFrancoRegion" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboCurrency" ClientInstanceName="cboCurrency" Width="100%" runat="server" /></td>
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                            <td><asp:TextBox ID="txtKurs" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trBarcodeEntry" runat="server">
                <td colspan="2">
                    <h4><%=GetLabel("Barcode Entry")%></h4>
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 150px" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                            <td>
                                <asp:TextBox ID="txtBarcodeEntryItem" Width="200px" runat="server" />
                                <%=GetLabel("Jumlah")%>
                                <asp:TextBox ID="txtBarcodeEntryQty" Width="50px" CssClass="number" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                        <div id="entryDetailContainer" class="entryDetailContainer"">
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
                                                    <col style="width: 200px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                                                    <td colspan="2">
                                                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
                                                        <input type="hidden" value="" id="hdnBasePrice" runat="server" />
                                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacItem" ClientInstanceName="tacItem" MethodName="GetItemMasterList" GetFilterExpressionFunction="onGetItemFilterExpression"
                                                            SearchFields="ItemName1,ItemCode" TextField="ItemName1" ValueField="ItemID" SearchText="${ItemName1} / ${PreferredName} (<b>${ItemCode}</b>)" OrderByExpression="ItemName1">
                                                            <ClientSideEvents ButtonSearchClick="function(){ onTacItemButtonSearchClick(); }"
                                                                ValueChanged="function(){ onTacItemValueChanged(); }" />
                                                        </cdx:CodeXAutoCompleteTextBox>  
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Jumlah Dijual")%></label></td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtQuantity" CssClass="number" Width="120px" runat="server" Text="1" /></td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox runat="server" ClientEnabled="false" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                                                                        Width="300px" OnCallback="cboItemUnit_Callback">
                                                                        <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;">
                                                        <label class="lblNormal"><%=GetLabel("Harga (Sebelum PPN)")%></label>
                                                    </td>
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
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;">
                                                        <label class="lblNormal"><%=GetLabel("Harga (Sesudah PPN)")%></label>
                                                    </td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <colgroup>
                                                                <col style="width: 120px" />
                                                                <col style="width: 3px" />
                                                                <col style="width: 250px" />
                                                            </colgroup>
                                                            <tr>
                                                                <td><asp:TextBox ID="txtPriceAfterVAT" CssClass="txtCurrency" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtBaseUnitAfterVAT" ReadOnly="true" Width="100%" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon 1")%></label></td>
                                                    <td><asp:TextBox ID="txtDiscount1" Text="0" CssClass="number" Width="80px" runat="server" /> %</td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon 2")%></label></td>
                                                    <td><asp:TextBox ID="txtDiscount2" Text="0" CssClass="number" Width="80px" runat="server" /> %</td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Harga")%></label></td>
                                                    <td>
                                                        <input type="hidden" id="hdnLineAmount" runat="server" />
                                                        <asp:TextBox ID="txtLineAmount" Width="180px" ReadOnly="true" runat="server" CssClass="txtCurrency" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel" style="padding-top: 5px; vertical-align: top"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                                    <td><asp:TextBox ID="txtNotesDt" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                    position: relative;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" HeaderStyle-Width="100px" />
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Qty" HeaderStyle-Width="50px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="ItemUnit" HeaderText="Satuan" HeaderStyle-Width="100px" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="Harga" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="110px" DataFormatString="{0:N}"/>
                                            <asp:BoundField DataField="UnitPriceAfterVAT" HeaderText="Harga + PPN" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="110px" DataFormatString="{0:N}" />
                                            <asp:BoundField DataField="CustomTotalDiscount" HeaderText="Total Disc" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                            <asp:TemplateField HeaderStyle-Width="5px" />
                                            <asp:BoundField DataField="CustomSubTotal" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                                            <asp:TemplateField HeaderStyle-Width="5px" />
                                            <asp:BoundField DataField="CreatedByName" HeaderText="Petugas" HeaderStyle-Width="80px"/>
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("TransactionDtID") %>" bindingfield="TransactionDtID" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                                    <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                                    <input type="hidden" value="<%#Eval("ItemName1") %>" bindingfield="ItemName1" />
                                                    <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                                    <input type="hidden" value="<%#Eval("GCItemUnit") %>" bindingfield="GCItemUnit" />
                                                    <input type="hidden" value="<%#Eval("UnitPrice") %>" bindingfield="UnitPrice" />
                                                    <input type="hidden" value="<%#Eval("LineAmount") %>" bindingfield="LineAmount" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage1") %>" bindingfield="DiscountPercentage1" />
                                                    <input type="hidden" value="<%#Eval("DiscountPercentage2") %>" bindingfield="DiscountPercentage2" />
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
                                                <td class="tdLabel" style="width: 120px; vertical-align: top; padding-top: 5px;">
                                                    <label class="lblNormal">
                                                        <%=GetLabel("Catatan")%></label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="5" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col style="width: 220px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Penjualan (Sebelum PPN)")%></label></td>
                                                <td></td>
                                                <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%></label></td>
                                                <td><asp:TextBox runat="server" ID="txtPPNPercentage" Width="35px" CssClass="number" ReadOnly="true"></asp:TextBox>&nbsp;<%=GetLabel("%")%></td>
                                                <td></td>
                                                <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Penjualan (Sesudah PPN)")%></label></td>
                                                <td></td>
                                                <td><asp:TextBox ID="txtTransactionAmountAfterVAT" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                                <td><asp:TextBox runat="server" ID="txtFinalDiscountInPercentage" Width="35px" CssClass="number" ReadOnly="true"></asp:TextBox>&nbsp;<%=GetLabel("%")%></td>
                                                <td></td>
                                                <td><asp:TextBox ID="txtFinalDiscount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Total Nilai Penjualan")%></label></td>
                                                <td></td>
                                                <td><asp:TextBox ID="txtTransactionAmountSaldo" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
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
