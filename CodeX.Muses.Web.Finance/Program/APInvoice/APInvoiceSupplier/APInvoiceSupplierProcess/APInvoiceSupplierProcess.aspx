<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPSupplierPageTrx.master" AutoEventWireup="true"
    CodeBehind="APInvoiceSupplierProcess.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierProcess" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divCopyPurchaseReceive').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divCopyPurchaseReceive').hide();
            }

            setDatePicker('<%=txtPurchaseInvoiceDate.ClientID %>');
            setDatePicker('<%=txtSupplierInvoiceDate.ClientID %>');
            setDatePicker('<%=txtTaxInvoiceDate.ClientID %>');
            setDatePicker('<%=txtDueDate.ClientID %>');
            setDatePicker('<%=txtInvoiceDate.ClientID %>');

            $('#<%=txtPurchaseInvoiceDate.ClientID %>').datepicker('option', 'maxDate', '0');
            $('#<%=txtSupplierInvoiceDate.ClientID %>').datepicker('option', 'maxDate', '0');
            $('#<%=txtTaxInvoiceDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#<%=txtFinalDiscountPIPctg.ClientID %>').val($('#<%=hdnFinalDiscountPI.ClientID %>').val()).trigger('changeValue');
            $('#<%=txtStampPI.ClientID %>').val($('#<%=hdnStampPI.ClientID %>').val()).trigger('changeValue');
            $('#<%=txtChargesPI.ClientID %>').val($('#<%=hdnChargesPI.ClientID %>').val()).trigger('changeValue');
            $('#<%=txtPPHPIPctg.ClientID %>').val($('#<%=hdnPPHPctg.ClientID %>').val()).trigger('changeValue');
            $('#<%=txtFinalDiscountPIPctg.ClientID %>').change();
            $('#<%=txtStampPI.ClientID %>').change();
            $('#<%=txtChargesPI.ClientID %>').change();
            $('#<%=txtPPHPIPctg.ClientID %>').change();

            //#region Add
            $('#divTransactionAdd').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=txtTransactionAmount.ClientID %>').removeAttr('readonly');
                    $('#<%=txtDiscountAmount.ClientID %>').removeAttr('readonly');

                    cboGLAPOther.SetValue('');
                    $('#<%=txtTransactionAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=txtInvoiceNo.ClientID %>').val('');
                    $('#<%=txtDiscTransAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtDiscountAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtVAT.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtPPh23.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtPPh25.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtStampAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtChargesAmount.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtDownPayment.ClientID %>').val('').trigger('changeValue');
                    $('#<%=txtCreditNote.ClientID %>').val('').trigger('changeValue');

                    $('#<%=hdnSiteServiceUnitID.ClientID %>').val('');
                    $('#<%=txtServiceUnitCode.ClientID %>').val('');
                    $('#<%=txtServiceUnitName.ClientID %>').val('');
                    $('#<%=txtRemarksDt.ClientID %>').val('');

                    $('.chkSite input:checked').each(function () {
                        $(this).prop('checked', false);
                    });
                    setDdeSiteText();

                    $('#entryDetailContainer').show();
                }
            });
            //#endregion

            $('#divCopyPurchaseReceive').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var id = $('#<%=hdnPurchaseInvoiceID.ClientID %>').val() + '|' + cboItemType.GetValue() + '|' + cboPurchaseType.GetValue();
                    var url = ResolveUrl('~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessCtl.ascx');
                    openUserControlPopup(url, id, 'Pilih Penerimaan Pembelian', 1000, 600);
                }
            });

            $('#divCopyPurchaseReturn').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var id = $('#<%=hdnPurchaseInvoiceID.ClientID %>').val() + '|' + cboItemType.GetValue() + '|' + cboPurchaseType.GetValue();
                    var url = ResolveUrl('~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessPurchaseReturnCtl.ascx');
                    openUserControlPopup(url, id, 'Pilih Retur Pembelian', 1000, 600);
                }
            });

            //#region Perhitungan
            $('#<%=chkPPN.ClientID %>').change(function () {
                calculateTotal();
            });

            $('#<%=txtFinalDIscountPI.ClientID %>').change(function () {
                $(this).trigger('changeValue');
                calculateFinalDiscount("fromTxt");
                calculatePPH("fromTxt");
                calculateTotal();
            });

            $('#<%=txtFinalDiscountPIPctg.ClientID %>').change(function () {
                $(this).blur();
                calculateFinalDiscount("fromPctg");
                calculatePPH("fromTxt");
                calculateTotal();
            });

            $('#<%=txtPPHPI.ClientID %>').change(function () {
                $(this).trigger('changeValue');
                calculatePPH("fromTxt");
                calculateTotal();
            });

            $('#<%=txtPPHPIPctg.ClientID %>').change(function () {
                $(this).blur();
                calculatePPH("fromPctg");
                calculateTotal();
            });

            $('#<%=txtChargesPI.ClientID %>').change(function () {
                $(this).blur();
                calculateTotal();
            });

            $('#<%=txtStampPI.ClientID %>').change(function () {
                $(this).blur();
                calculateTotal();
            });
            //#endregion

            //#region Purchase Invoice No
            $('#lblPurchaseInvoiceNo.lblLink').click(function () {
                openSearchDialog('purchaseinvoicehd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtPurchaseInvoiceNo.ClientID %>').val(value);
                    onTxtPurchaseInvoiceNoChanged(value);
                });
            });

            $('#<%=txtPurchaseInvoiceNo.ClientID %>').change(function () {
                onTxtPurchaseInvoiceNoChanged($(this).val());
            });

            function onTxtPurchaseInvoiceNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Site Service Unit
            function getSiteServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', getSiteServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtServiceUnitCode.ClientID %>').val(value);
                    onTxtServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtServiceUnitCode.ClientID %>').live('change', function () {
                onTxtServiceUnitCodeChanged($(this).val());
            });

            function onTxtServiceUnitCodeChanged(value) {
                var filterExpression = getSiteServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
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
                });
            }
            //#endregion

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            calculateTotal();

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    cbpProcess.PerformCallback('save');
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });
        }

        function onAfterSaveEditRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }

        //#region edit and delete
        $('.grdPurchaseInvoice .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('.grdPurchaseInvoice .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            if (entity.PurchaseReceiveNo != "") {
                var id = entity.ID + '|' + entity.PurchaseReceiveID;
                var url = '';
                if ($(this).attr('isalloweditpurchasereceive') == '1')
                    url = ResolveUrl("~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessEditCtl.ascx");
                else
                    url = ResolveUrl("~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessEdit2Ctl.ascx");
                openUserControlPopup(url, id, 'Ubah Penerimaan Pembelian', 1250, 600);
            }
            else {
                $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                cboGLAPOther.SetValue(entity.GLAPOtherID);
                $('#<%=txtPurchaseRcvNo.ClientID %>').val(entity.PurchaseReceiveNo);
                $('#<%=txtInvoiceNo.ClientID %>').val(entity.ReferenceNo);
                $('#<%=txtTransactionAmount.ClientID %>').val(entity.TransactionAmount).trigger('changeValue');
                $('#<%=txtDiscTransAmount.ClientID %>').val(entity.DiscountAmount).trigger('changeValue');
                $('#<%=txtDiscountAmount.ClientID %>').val(entity.FinalDiscountAmount).trigger('changeValue');
                $('#<%=txtVAT.ClientID %>').val(entity.VATAmount).trigger('changeValue');
                $('#<%=txtPPh23.ClientID %>').val(entity.PPH23).trigger('changeValue');
                $('#<%=txtPPh25.ClientID %>').val(entity.PPH25).trigger('changeValue');
                $('#<%=txtStampAmount.ClientID %>').val(entity.StampAmount).trigger('changeValue');
                $('#<%=txtChargesAmount.ClientID %>').val(entity.ChargesAmount).trigger('changeValue');
                $('#<%=txtDownPayment.ClientID %>').val(entity.DownPaymentAmount).trigger('changeValue');
                $('#<%=txtCreditNote.ClientID %>').val(entity.CreditNoteAmount).trigger('changeValue');
                $('#<%=hdnSiteServiceUnitID.ClientID %>').val(entity.SiteServiceUnitID);
                $('#<%=txtServiceUnitCode.ClientID %>').val(entity.ServiceUnitCode);
                $('#<%=txtServiceUnitName.ClientID %>').val(entity.ServiceUnitName);
                $('#<%=txtRemarksDt.ClientID %>').val(entity.Remarks);

                $('.chkSite input:checked').each(function () {
                    $(this).prop('checked', false);
                });

                var lstSiteID = entity.ListSiteID.split(',');
                for (var i = 0; i < lstSiteID.length; ++i) {
                    $('.chkSite').each(function () {
                        if ($(this).attr('siteid') == lstSiteID[i])
                            $(this).find('input').prop('checked', true);
                    });
                }
                setDdeSiteText();

                $('#entryDetailContainer').show();
            }
        });
        //#endregion

        function calculateFinalDiscount(kode) {
            var totalTrans = parseFloat($('#<%=hdnTotalAmountBeforeDP.ClientID %>').val());
            if (kode == "fromPctg") {
                var disc = parseFloat($('#<%=txtFinalDiscountPIPctg.ClientID %>').attr('hiddenVal'));
                var totalDisc = totalTrans * (disc / 100);
                $('#<%=txtFinalDIscountPI.ClientID %>').val(totalDisc).trigger('changeValue');
            }
            else if (kode == "fromTxt") {
                var disc = parseFloat($('#<%=txtFinalDIscountPI.ClientID %>').attr('hiddenVal'));
                var pctg = disc / (totalTrans / 100);
                $('#<%=txtFinalDiscountPIPctg.ClientID %>').val(pctg).trigger('changeValue');
            }
        }

        function calculatePPH(kode) {
            var totalTrans = parseFloat($('#<%=hdnTotalAmount.ClientID %>').val()) - parseFloat($('#<%=txtFinalDIscountPI.ClientID %>').attr("hiddenVal"));
            if (kode == "fromPctg") {
                var pctg = parseFloat($('#<%=txtPPHPIPctg.ClientID %>').attr('hiddenVal'));
                var totalPPH = totalTrans * (pctg / 100);
                $('#<%=txtPPHPI.ClientID %>').val(totalPPH).trigger('changeValue');
            }
            else if (kode == "fromTxt") {
                var pph = parseFloat($('#<%=txtPPHPI.ClientID %>').attr('hiddenVal'));
                var pctg = pph / (totalTrans / 100);
                $('#<%=txtPPHPIPctg.ClientID %>').val(pctg).trigger('changeValue');
            }
        }

        function calculateTotal() {
            calculateFinalDiscount("fromPctg");
            calculatePPH("fromPctg");
            var totalTrans = parseFloat($('#<%=hdnTotalAmount.ClientID %>').val());
            $('#<%=txtTotalAmount.ClientID %>').val(totalTrans).trigger('changeValue');
            if ($('#<%=chkPPN.ClientID %>').is(':checked')) {
                var temp = parseFloat($('#<%=hdnTotalAmount.ClientID %>').val()) - parseFloat($('#<%=txtFinalDIscountPI.ClientID %>').attr("hiddenVal"));
                var PPN = parseFloat($('#<%=hdnPPNPctg.ClientID %>').val()) / 100 * parseFloat(temp);
                $('#<%=txtPPNPI.ClientID %>').val(PPN).trigger('changeValue');
            }
            else {
                $('#<%=txtPPNPI.ClientID %>').val('0').trigger('changeValue');
            }
            var PPN = parseFloat($('#<%=txtPPNPI.ClientID %>').attr('hiddenVal'));
            var Discount = parseFloat($('#<%=txtFinalDIscountPI.ClientID %>').attr('hiddenVal'));
            var PPH = parseFloat($('#<%=txtPPHPI.ClientID %>').attr('hiddenVal'));
            var materai = parseFloat($('#<%=txtStampPI.ClientID %>').attr('hiddenVal'));
            var ongkos = parseFloat($('#<%=txtChargesPI.ClientID %>').attr('hiddenVal'));
            var totalHarga = totalTrans - Discount + PPN + PPH + materai + ongkos;
            $('#<%=txtGrandTotalPI.ClientID %>').val(totalHarga).trigger('changeValue');
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            if ($('#<%=hdnPurchaseInvoiceID.ClientID %>').val() == '0') {
                $('#<%=hdnPurchaseInvoiceID.ClientID %>').val(param);
                var filterExpression = 'PurchaseInvoiceID = ' + param;
                Methods.getObject('GetPurchaseInvoiceHdList', filterExpression, function (result) {
                    $('#<%=txtPurchaseInvoiceNo.ClientID %>').val(result.PurchaseInvoiceNo);
                    onLoadObject(result.PurchaseInvoiceNo);
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterSaveRecordDtSuccess(PurchaseInvoiceID) {
            if ($('#<%=hdnPurchaseInvoiceID.ClientID %>').val() == '0') {
                $('#<%=hdnPurchaseInvoiceID.ClientID %>').val(PurchaseInvoiceID);
                var filterExpression = 'PurchaseInvoiceID = ' + PurchaseInvoiceID;
                Methods.getObject('GetPurchaseInvoiceHdList', filterExpression, function (result) {
                    $('#<%=txtPurchaseInvoiceNo.ClientID %>').val(result.PurchaseInvoiceNo);
                    onLoadObject(result.PurchaseInvoiceNo);
                });
                onAfterCustomSaveSuccess();
            }
        }

        function cbpViewEndCallback(s) {
            hideLoadingPanel();
            calculateTotal();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var PurchaseInvoiceID = s.cpPurchaseInvoiceID;
                    onAfterSaveRecordDtSuccess(PurchaseInvoiceID);
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
        var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
        setPaging($("#paging"), pageCount, function (page) {
            cbpView.PerformCallback('changepage|' + page);
        });
        //#endregion

        $('.lblPurchaseReceiveNo').die('click');
        $('.lblPurchaseReceiveNo').live('click', function () {
            $tr = $(this).closest('tr');
            var entity = rowToObject($tr);
            var id = entity.ID + '|' + entity.PurchaseReceiveID;

            var url = ResolveUrl("~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessDtCtl.ascx");
            openUserControlPopup(url, id, 'Detail Penerimaan Pembelian', 1250, 600);
        });

        $('.lblCreditNote.lblLink').die('click');
        $('.lblCreditNote.lblLink').live('click', function () {
            $tr = $(this).closest('tr');
            var entity = rowToObject($tr);
            var id = entity.ID;

            if (entity.IsCreditNoteOnly == "True") {
                var url = ResolveUrl("~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessCreditNoteCtl2.ascx");
                openUserControlPopup(url, id, 'Nota Kredit', 700, 500);
            }
            else {
                var url = ResolveUrl("~/Program/APInvoice/APInvoiceSupplier/APInvoiceSupplierProcess/APInvoiceSupplierProcessCreditNoteCtl.ascx");
                openUserControlPopup(url, id, 'Nota Kredit', 1000, 500);
            }
        });

        $('.chkSite input').live('change', function () {
            setDdeSiteText();
        });

        function setDdeSiteText() {
            var lstSiteID = '';
            var lstSiteName = '';
            $('.chkSite input:checked').each(function () {
                if (lstSiteName != '') {
                    lstSiteName += ', ';
                    lstSiteID += ',';
                }
                lstSiteID += $(this).parent().attr('siteid');
                lstSiteName += $(this).parent().attr('sitename');
            });
            $('#<%=hdnLstSiteID.ClientID %>').val(lstSiteID);
            ddeSite.SetText(lstSiteName);
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var purchaseInvoiceID = $('#<%=hdnPurchaseInvoiceID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (purchaseInvoiceID == '' || purchaseInvoiceID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "PurchaseInvoiceID = " + purchaseInvoiceID;
                    return true;
                }
            }
            else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }
    </script>
    <input type="hidden" value="" id="hdnIsDiscountAppliedToAveragePrice" runat="server" />
    <input type="hidden" value="" id="hdnIsDiscountAppliedToUnitPrice" runat="server" />
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" id="hdnPurchaseInvoiceID" runat="server" value="0" />
    <input type="hidden" value="" id="hdnBusinessPartnerID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnPPNPctg" runat="server" />
    <input type="hidden" value="" id="hdnStampPI" runat="server" />
    <input type="hidden" value="" id="hdnFinalDiscountPI" runat="server" />
    <input type="hidden" value="" id="hdnPPHPctg" runat="server" />
    <input type="hidden" value="" id="hdnChargesPI" runat="server" />
    <input type="hidden" value="" id="hdnTransactionStatus" runat="server" />
    <div style="overflow-y: auto; overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 40%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 175px" />
                            <col style="width: 150px" />
                            <col style="width: 135px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label id="lblPurchaseInvoiceNo" class="lblLink"><%=GetLabel("No. Tanda Terima Faktur")%></label></td>
                            <td><asp:TextBox ID="txtPurchaseInvoiceNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Pembelian")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboPurchaseType" ClientInstanceName="cboPurchaseType" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Item")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboItemType" ClientInstanceName="cboItemType" Width="120px" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboCurrency" ClientInstanceName="cboCurrency" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Proses") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtPurchaseInvoiceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                            <td><asp:TextBox ID="txtKurs" Width="150px" CssClass="number" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Jatuh Tempo") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtDueDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 135px" />
                            <col style="width: 150px" />
                            <col style="width: 135px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label id="Label1"><%=GetLabel("No. Faktur Supplier")%></label></td>
                            <td><asp:TextBox ID="txtSupplierInvoiceNo" Width="150px" runat="server" /></td>
                            <td class="tdLabel"><%=GetLabel("Tgl. Faktur Supplier") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtSupplierInvoiceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("No. Faktur Pajak")%></td>
                            <td><asp:TextBox ID="txtTaxInvoiceNo" Width="150px" runat="server" /></td>
                            <td class="tdLabel"><%=GetLabel("Tgl. Faktur Pajak") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtTaxInvoiceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top:5px;"><%=GetLabel("Catatan") %></td>
                            <td colspan="3"><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Faktur Tanpa No. BPB")%></span>
                        <span id="divCopyPurchaseReceive" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Salin Penerimaan Pembelian")%></span>
                        <span id="divCopyPurchaseReturn" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Salin Retur Pembelian")%></span>
                        <br />
                        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrx" style="margin: 0">
                                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                                <table style="width:100%">
                                    <colgroup>
                                        <col style="width: 50%" />
                                    </colgroup>
                                    <tr>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width:150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe") %></label></td>
                                                    <td><dxe:ASPxComboBox id="cboGLAPOther" ClientInstanceName="cboGLAPOther" runat="server" Width="150px" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel">
                                                        <label class="lblMandatory lblLink" runat="server" id="lblSiteServiceUnit"><%=GetLabel("Bagian")%></label>
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
                                                                <td><asp:TextBox ID="txtServiceUnitCode" Width="100%" runat="server" /></td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Unit")%></label></td>
                                                    <td>
                                                        <input type="hidden" id="hdnLstSiteID" value="" runat="server" />
                                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeSite" ID="ddeSite"
                                                            Width="300px" runat="server" EnableAnimation="False">
                                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                                            <DropDownWindowTemplate>
                                                                <asp:Repeater ID="rptSite" runat="server" OnItemDataBound="rptSite_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSite" CssClass="chkSite" runat="server"  /> <%#Eval("SiteName") %><br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </DropDownWindowTemplate>
                                                        </dxe:ASPxDropDownEdit>
                                                    </td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No. BPB") %></label></td>
                                                    <td><asp:TextBox id="txtPurchaseRcvNo" runat="server" Width="150px" ReadOnly="true"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No. Faktur/Kirim") %></label></td>
                                                    <td><asp:TextBox id="txtInvoiceNo" runat="server" Width="150px" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
                                                    <td><asp:TextBox ID="txtInvoiceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah") %></label></td>
                                                    <td><asp:TextBox id="txtTransactionAmount" runat="server" Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Diskon Per Item") %></label></td>
                                                    <td><asp:TextBox id="txtDiscTransAmount" runat="server"  Width="150px" ReadOnly="true" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Diskon Final") %></label></td>
                                                    <td><asp:TextBox id="txtDiscountAmount" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width:150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("PPN") %></label></td>
                                                    <td><asp:TextBox id="txtVAT" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("PPh23") %></label></td>
                                                    <td><asp:TextBox id="txtPPh23" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("PPh25") %></label></td>
                                                    <td><asp:TextBox id="txtPPh25" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Materai") %></label></td>
                                                    <td><asp:TextBox id="txtStampAmount" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Ongkos Kirim") %></label></td>
                                                    <td><asp:TextBox id="txtChargesAmount" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Uang Muka") %></label></td>
                                                    <td><asp:TextBox id="txtDownPayment" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nota Kredit") %></label></td>
                                                    <td><asp:TextBox id="txtCreditNote" runat="server"  Width="150px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align:top; padding-top:5px;"><%=GetLabel("Catatan") %></td>
                                                    <td colspan="3"><asp:TextBox ID="txtRemarksDt" Width="300px" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ cbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <input type="hidden" value="0" id="hdnTotalAmount" runat="server" />
                                    <input type="hidden" value="0" id="hdnTotalAmountBeforeDP" runat="server" />
                                    <table class="grdPurchaseInvoice tblTransactionEntryResult" cellspacing="0" width="100%" rules="all">
                                        <tr>
                                            <th class="keyField"></th>
                                            <th><%=GetLabel("No BPB") %></th>
                                            <th style="width:160px"><%=GetLabel("No Faktur") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("Jumlah") %></th>
                                            <th style="width:90px" class="thRight"><%=GetLabel("Diskon Final") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("PPN") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("Materai") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("Biaya") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("DP") %></th>
                                            <th style="width:80px" class="thRight"><%=GetLabel("Nota Kredit") %></th>
                                            <th style="width:100px" class="thRight"><%=GetLabel("Total") %></th>
                                            <th style="width:80px;"> </th>
                                        </tr>
                                        <asp:ListView runat="server" ID="lvwView">
                                            <EmptyDataTemplate>
                                                <tr class="trEmpty">
                                                    <td colspan="14"><%=GetLabel("Data Tidak Tersedia") %></td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left"><label class="lblLink lblPurchaseReceiveNo"><%#Eval("PurchaseReceiveNo") %></label></td>
                                                    <td align="left"><%#Eval("ReferenceNo")%></td>
                                                    <td align="right"><%#Eval("TransactionAmount","{0:N}")%></td>
                                                    <td align="right"><%#Eval("FinalDiscountAmount","{0:N}")%></td>
                                                    <td align="right"><%#Eval("VATAmount","{0:N}") %></td>
                                                    <td align="right"><%#Eval("StampAmount", "{0:N}")%></td>
                                                    <td align="right"><%#Eval("ChargesAmount", "{0:N}")%></td>
                                                    <td align="right"><%#Eval("DownPaymentAmount", "{0:N}")%></td>
                                                    <td align="right"><img height="14" title="<%=GetLabel("Ada Nota Kredit Yang Belum Diproses") %>" src='<%= ResolveUrl("~/Libs/Images/Button/warning.png")%>' alt='' <%#Eval("IsHasCreditNote").ToString() == "True" && Convert.ToDecimal(Eval("CreditNoteAmount")) == 0 ? "" : "style='display:none'" %> />
                                                        <label class="<%# Convert.ToDecimal(Eval("CreditNoteAmount").ToString()) != 0 ? "lblLink lblCreditNote" : ""%>"><%#Eval("CreditNoteAmount", "{0:N}")%></label>
                                                    </td>
                                                    <td align="right"><%#Eval("LineAmount", "{0:N}")%></td>
                                                    <td>
                                                        <div style='float:right;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                        <div style='float:right;margin-right:10px;<%#IsEditable().ToString() == "0" || Eval("IsCreditNoteOnly").ToString() == "True" ? "display:none" : "" %>' <%=IsAllowEditPurchaseReceive() ? "isalloweditpurchasereceive='1'" : "isalloweditpurchasereceive='0'" %> class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" bindingfield="PurchaseInvoiceID" value='<%# Eval("PurchaseInvoiceID")%>' />
                                                        <input type="hidden" bindingfield="PurchaseReceiveID" value='<%# Eval("PurchaseReceiveID")%>' />
                                                        <input type="hidden" bindingfield="PurchaseReceiveNo" value='<%# Eval("PurchaseReceiveNo")%>' />
                                                        <input type="hidden" bindingfield="GLAPOtherID" value='<%# Eval("GLAPOtherID")%>' />
                                                        <input type="hidden" bindingfield="ReferenceNo" value='<%# Eval("ReferenceNo")%>' />
                                                        <input type="hidden" bindingfield="ReferenceDate" value='<%# Eval("ReferenceDateInString")%>' />
                                                        <input type="hidden" bindingfield="VATAmount" value='<%# Eval("VATAmount")%>' />
                                                        <input type="hidden" bindingfield="PPH23Amount" value='<%# Eval("PPH23Amount")%>' />
                                                        <input type="hidden" bindingfield="PPH25Amount" value='<%# Eval("PPH25Amount")%>' />
                                                        <input type="hidden" bindingfield="TransactionAmount" value='<%# Eval("TransactionAmount")%>' />
                                                        <input type="hidden" bindingfield="DownPaymentAmount" value='<%# Eval("DownPaymentAmount")%>' />
                                                        <input type="hidden" bindingfield="CreditNoteAmount" value='<%# Eval("CreditNoteAmount")%>' />
                                                        <input type="hidden" bindingfield="DiscountAmount" value='<%# Eval("DiscountAmount")%>' />
                                                        <input type="hidden" bindingfield="FinalDiscountAmount" value='<%# Eval("FinalDiscountAmount")%>' />
                                                        <input type="hidden" bindingfield="StampAmount" value='<%# Eval("StampAmount")%>' />
                                                        <input type="hidden" bindingfield="ChargesAmount" value='<%# Eval("ChargesAmount")%>' />
                                                        <input type="hidden" bindingfield="CustomSubTotal" value='<%# Eval("LineAmount")%>' />
                                                        <input type="hidden" bindingfield="IsCreditNoteOnly" value='<%# Eval("IsCreditNoteOnly")%>' />
                                                        <input type="hidden" bindingfield="ID" value='<%# Eval("ID")%>' />
                                                        <input type="hidden" bindingfield="ListSiteID" value='<%# Eval("ListSiteID")%>' />
                                                        <input type="hidden" bindingfield="SiteServiceUnitID" value='<%# Eval("SiteServiceUnitID")%>' />
                                                        <input type="hidden" bindingfield="ServiceUnitCode" value='<%# Eval("ServiceUnitCode")%>' />
                                                        <input type="hidden" bindingfield="ServiceUnitName" value='<%# Eval("ServiceUnitName")%>' />
                                                        <input type="hidden" bindingfield="Remarks" value='<%# Eval("Remarks")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="containerTotal" style="margin-top: 5px;float:right">
                        <table class="tblContentArea" style="width: 100%;">
                            <colgroup>
                                <col style="width: 50%" />
                            </colgroup>
                                <tr>
                                <td valign="top" colspan="2">
                                    <div id="containerTotalFaktur" style="margin-top: 5px;">
                                        <fieldset id="fsTotalFaktur" style="margin: 0">
                                            <table style="width: 100%;" border="0" >
                                                <colgroup>
                                                    <col />
                                                </colgroup>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <table style="width: 100%;">
                                                            <colgroup>
                                                                <col style="width: 200px" />
                                                                <col />
                                                            </colgroup>
                                                            <tr>
                                                                <td class="tdLabel"  style="text-align:right"><label class="lblNormal"><%=GetLabel("Total Penerimaan")%></label></td>
                                                                <td><asp:TextBox ID="txtTotalAmount" CssClass="txtCurrency" ReadOnly="true" Width="100%" runat="server" hiddenVal="0" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:right;">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>                                                                    
                                                                            <td><asp:TextBox class= "txtFinalDiscountPIPctg txtCurrency" ID="txtFinalDiscountPIPctg" Width="60px" runat="server" hiddenVal="0"/> %</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td><asp:TextBox ID="txtFinalDIscountPI" CssClass="txtCurrency" Width="100%" runat="server" hiddenVal="0"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdLabel" style="text-align:right;"><asp:CheckBox ID="chkPPN" runat="server" />&nbsp;<%=GetLabel("PPN")%></td>
                                                                <td><asp:TextBox ID="txtPPNPI" CssClass="txtCurrency" Width="100%" ReadOnly="true" runat="server" hiddenVal="0"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:right;">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("PPH")%></label></td>
                                                                            <td><asp:TextBox class= "txtPPHPIPctg txtCurrency" ID="txtPPHPIPctg" Width="60px" runat="server" hiddenVal="0"/> %</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td><asp:TextBox ID="txtPPHPI" CssClass="txtCurrency" Width="100%" runat="server" hiddenVal="0"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Ongkos Kirim")%></label></td>
                                                                <td><asp:TextBox ID="txtChargesPI" CssClass="txtCurrency" Width="100%" runat="server" hiddenVal="0"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Materai")%></label></td>
                                                                <td><asp:TextBox ID="txtStampPI" CssClass="txtCurrency" Width="100%" runat="server" hiddenVal="0"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdLabel" style="text-align:right"><label class="lblNormal"><%=GetLabel("Total Faktur")%></label></td>
                                                                <td><asp:TextBox ID="txtGrandTotalPI" CssClass="txtCurrency" ReadOnly="true" Width="100%" runat="server" hiddenVal="0" /></td>
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
                        <div class="imgLoadingGrdView" id="containerImgLoadingView">
                            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
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