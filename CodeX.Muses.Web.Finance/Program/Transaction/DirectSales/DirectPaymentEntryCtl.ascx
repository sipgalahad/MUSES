<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPaymentEntryCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Finance.Program.DirectPaymentEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    //#region Inline Editing
    var grdPayment = new InlineEditing();
    var numFinishLoad = 0;
    function init() {
        var listParam = new Array();
        var cboPaymentMethodID = '<%=cboPaymentMethod.ClientID%>';
        var cboEDCMachineID = '<%=cboEDCMachine.ClientID%>';
        var cboBankID = '<%=cboBank.ClientID%>';

        listParam[0] = { "type": "cbo", "className": "cboPaymentMethod", "cboID": cboPaymentMethodID, "isUnique": false, "isEnabled": true };
        listParam[1] = { "type": "cbo", "className": "cboEDCMachine", "cboID": cboEDCMachineID, "isRequired": true, "isUnique": false, "isEnabled": false };
        listParam[2] = { "type": "bte", "className": "bteCardInformation", "isEnabled": false, "isRequired": true, "isButtonEnabled": false };
        listParam[3] = { "type": "cbo", "className": "cboBank", "cboID": cboBankID, "isUnique": false, "isRequired": true, "isEnabled": false };
        listParam[4] = { "type": "txt", "className": "txtReferenceNo", "isRequired": true, "isEnabled": false };
        listParam[5] = { "type": "txt", "className": "txtPayment", "isRequired": true, "isEnabled": true, "dataType": "money" };
        listParam[6] = { "type": "txt", "className": "txtFee", "isEnabled": false, "dataType": "money" };
        listParam[7] = { "type": "txt", "className": "txtLineTotal", "isEnabled": false, "dataType": "money" };
        listParam[8] = { "type": "hdn", "className": "hdnCardType" };
        listParam[9] = { "type": "hdn", "className": "hdnCardNo" };
        listParam[10] = { "type": "hdn", "className": "hdnHolderName" };
        listParam[11] = { "type": "hdn", "className": "hdnExpiredDateMonth" };
        listParam[12] = { "type": "hdn", "className": "hdnExpiredDateYear" };
        listParam[13] = { "type": "hdn", "className": "hdnCardFee", "value": "0" };
        listParam[14] = { "type": "hdn", "className": "hdnCardProvider" };

        grdPayment.setOnBteButtonClickHandler(function ($row, bteClass) {
            $currEditedRow = $row;
            if (bteClass == 'bteCardInformation') {
                var cardtype = grdPayment.getCellHiddenValue($row, 'hdnCardType');
                var cardNo = grdPayment.getCellHiddenValue($row, 'hdnCardNo');
                var holderName = grdPayment.getCellHiddenValue($row, 'hdnHolderName');
                var expiredDateMonth = grdPayment.getCellHiddenValue($row, 'hdnExpiredDateMonth');
                var expiredDateYear = grdPayment.getCellHiddenValue($row, 'hdnExpiredDateYear');
                var cardProvider = grdPayment.getCellHiddenValue($row, 'hdnCardProvider');

                cboCardType.SetValue(cardtype);
                $('#<%=txtCardNumber4.ClientID %>').val(cardNo);
                $('#<%=txtHolderName.ClientID %>').val(holderName);
                cboCardDateMonth.SetValue(expiredDateMonth);
                cboCardDateYear.SetValue(expiredDateYear);
                cboCardProvider.SetValue(cardProvider);

                cboCardType.SetFocus();

                pcCardInformation.Show();
            }
        });

        grdPayment.setOnCboValueChangedHandler(function ($row, cboClass, oldValue, newValue) {
            if (cboClass == 'cboPaymentMethod') {
                grdPayment.setCellHiddenValue($row, 'hdnCardFee ', '0');
                grdPayment.setTextBoxProperties($row, 'txtFee', { "value": 0 });

                var isCreditOrDebit = (newValue == 'X035^002' || newValue == 'X035^003');
                grdPayment.setComboBoxProperties($row, 'cboEDCMachine', { "isEnabled": isCreditOrDebit, "value": "" });
                grdPayment.setButtonEditProperties($row, 'bteCardInformation', { "isButtonEnabled": isCreditOrDebit, "value": "" });

                var isBankTransfer = (newValue == 'X035^004');
                grdPayment.setTextBoxProperties($row, 'txtReferenceNo', { "isEnabled": isBankTransfer, "value": "" });

                grdPayment.setComboBoxProperties($row, 'cboBank', { "isEnabled": isBankTransfer, "value": "" });
                //var isBankTransferOrCreditOrDebit = (isCreditOrDebit || isBankTransfer);
                //grdPayment.setComboBoxProperties($row, 'cboBank', { "isEnabled": isBankTransferOrCreditOrDebit, "value": "" });

                if (isCreditOrDebit) {
                    var amount = parseInt($('#<%=hdnCashbackAmount.ClientID %>').val()) * -1;
                    if (amount < 0)
                        amount = 0;
                    grdPayment.setTextBoxProperties($row, 'txtPayment', { "value": amount });
                }
                else {
                    if (grdPayment.getRowEnabled($row)) {
                        grdPayment.setTextBoxProperties($row, 'txtPayment', { "value": 0 });
                        grdPayment.setTextBoxProperties($row, 'txtLineTotal', { "value": 0 });
                    }
                }
                calculateCardFeeAndLineTotal($row);
            }
            else if (cboClass == 'cboEDCMachine') {
                getCardFee($row);
            }
        });

        grdPayment.setOnTxtValueChangedHandler(function ($row, txtClass, oldValue, newValue) {
            if (txtClass == 'txtPayment') {
                calculateCardFeeAndLineTotal($row);
            }
        });

        grdPayment.setOnRowDeletedHandler(function (objDeleted) {
            calculatePaymentDtTotal();
        });

        grdPayment.init('tblPaymentDt', listParam);
        grdPayment.addRow(true);
        calculatePaymentDtTotal();
    }

    function calculateCardFeeAndLineTotal($row) {
        var payment = parseFloat(grdPayment.getTextBoxValue($row, 'txtPayment'));
        var cardFeeInPercentage = parseFloat(grdPayment.getCellHiddenValue($row, 'hdnCardFee'));
        var cardFee = payment * cardFeeInPercentage / 100;
        var lineTotal = payment + cardFee;
        grdPayment.setTextBoxProperties($row, 'txtFee', { "value": cardFee });
        grdPayment.setTextBoxProperties($row, 'txtLineTotal', { "value": lineTotal });
        calculatePaymentDtTotal();
    }

    function getCardFee($row) {
        var cardProvider = grdPayment.getCellHiddenValue($row, 'hdnCardProvider');
        var cardtype = grdPayment.getCellHiddenValue($row, 'hdnCardType');
        var edcMachine = grdPayment.getComboBoxValue($row, 'cboEDCMachine');
        if (edcMachine != '' && cardtype != '' && cardProvider != '') {
            var filterExpression = $('#<%=hdnCreditCardFeeFilterExpression.ClientID %>').val().replace('[GCCardType]', cardtype).replace('[GCCardProvider]', cardProvider).replace('[EDCMachineID]', edcMachine);
            Methods.getObjectValue('GetCreditCardList', filterExpression, 'CreditCardFee', function (result) {
                if (result == '')
                    result = '0';
                grdPayment.setCellHiddenValue($row, 'hdnCardFee ', result);
                calculateCardFeeAndLineTotal($row);
            });
        }
        else {
            grdPayment.setCellHiddenValue($row, 'hdnCardFee ', '0');
            calculateCardFeeAndLineTotal($row);
        }
    }

    function calculatePaymentDtTotal() {
        var totalPayment = grdPayment.getColumnTotal('txtPayment');
        var totalCardFee = grdPayment.getColumnTotal('txtFee');
        var totalLineTotal = grdPayment.getColumnTotal('txtLineTotal');

        $('#tdTotalPatient').html(totalPayment.formatMoney(2, '.', ','));
        $('#tdTotalCardFee').html(totalCardFee.formatMoney(2, '.', ','));
        $('#tdTotalLineTotal').html(totalLineTotal.formatMoney(2, '.', ','));

        $('#<%=hdnTotalPaymentAmount.ClientID %>').val(totalPayment);
        $('#<%=hdnTotalFeeAmount.ClientID %>').val(totalCardFee);

        calculateCashbackAmount();
    }

    function calculateCashbackAmount() {
        var totalBilling = parseFloat($('#<%=txtInvoiceTotal.ClientID %>').attr('hiddenVal'));
        var totalPayment = parseFloat($('#<%=hdnTotalPaymentAmount.ClientID %>').val());
        var cashBackAmount = totalPayment - totalBilling;

        $('#<%=hdnCashbackAmount.ClientID %>').val(cashBackAmount);
        $('#<%=txtCashReturnAmount.ClientID %>').val(cashBackAmount).trigger('changeValue');
    }

    function closePcCardInformation(action) {
        if (action == 'save') {
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnCardType', cboCardType.GetValue());
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnCardNo', $('#<%=txtCardNumber4.ClientID %>').val());
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnHolderName', $('#<%=txtHolderName.ClientID %>').val());
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnExpiredDateMonth', cboCardDateMonth.GetValue());
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnExpiredDateYear', cboCardDateYear.GetValue());
            grdPayment.setCellHiddenValue($currEditedRow, 'hdnCardProvider', cboCardProvider.GetValue());

            var cardInformation = 'XXXX-XXXX-XXXX-' + $('#<%=txtCardNumber4.ClientID %>').val();
            grdPayment.setButtonEditProperties($currEditedRow, 'bteCardInformation', { value: cardInformation });

            grdPayment.setButtonEditFocus($currEditedRow, 'bteCardInformation');
            pcCardInformation.Hide();

            getCardFee($currEditedRow);
        }
        else {
            grdPayment.setButtonEditFocus($currEditedRow, 'bteCardInformation');
            pcCardInformation.Hide();
        }

    }
    //#endregion

    var ctr = 0;
    function onInit() {
        ctr++;

        if (ctr == 3) {
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            $('#btnPaymentCardInformationAdd').click(function (evt) {
                if (IsValid(evt, 'fsCardInformation', 'vgCardInformation'))
                    closePcCardInformation('save');
            });

            function getIsAdd() {
                return true;
            }

            if (getIsAdd()) {
                setDatePicker('<%=txtPaymentDate.ClientID %>');

                if (cboPaymentMethod != null && cboEDCMachine != null && cboBank != null)
                    init();

                $('#divContainerGrdDetailAdd').show();
                $('#divContainerGrdDetailEdit').hide();
                $('#divContainerGrdDetailAR').hide();
                $('#divDdeBillingNo').show();
                $('#divTxtBillingNo').hide();
                $('#btnPaymentCardInformationAdd').show();
                $('#btnPaymentCardInformationCancel').show();
                $('#btnPaymentCardInformationClose').hide();

                showLoadingPanel();
                setTimeout(function () {
                    showLoadingPanel();
                    setTimeout(function () {
                        onCboPaymentTypeValueChanged();
                        hideLoadingPanel();
                    }, 1500);
                }, 100);
            }
            else {
                $('#divContainerGrdDetailAdd').hide();
                $('#divContainerGrdDetailEdit').show();
                $('#divContainerGrdDetailAR').hide();

                $('#divDdeBillingNo').hide();
                $('#divTxtBillingNo').show();

                $('#btnPaymentCardInformationAdd').hide();
                $('#btnPaymentCardInformationCancel').hide();
                $('#btnPaymentCardInformationClose').show();

                $('.lnkCardNumber').click(function () {
                    $td = $(this).parent();
                    cboCardType.SetValue($td.find('.hdnGCCardType').val());
                    $('#<%=txtCardNumber4.ClientID %>').val($td.find('.hdnCardNumber4').val());
                    $('#<%=txtHolderName.ClientID %>').val($td.find('.hdnCardHolderName').val());
                    var cardValidThru = $td.find('.hdnCardValidThru').val().split('/');
                    var expiredDateMonth = parseInt(cardValidThru[0]);
                    var expiredDateYear = 2000 + parseInt(cardValidThru[1]);
                    cboCardDateMonth.SetValue(expiredDateMonth);
                    cboCardDateYear.SetValue(expiredDateYear);
                    cboCardProvider.SetValue($td.find('.hdnGCCardProvider').val());

                    pcCardInformation.Show();
                });
            }
        }
    }

    function onCboPaymentTypeValueChanged() {
        grdPayment.clearTable();
        grdPayment.addRow();
        var paymentType = cboPaymentType.GetValue();
        $('#tblCashback').show();

        $('#divContainerGrdDetailAdd').show();
        $('#divContainerGrdDetailAR').hide();

    }

    function onBeforeSaveRecord(errMessage) {
        var isAllowSave = true;
        var paymentType = cboPaymentType.GetValue();
        var cashBackAmount = parseFloat($('#<%=hdnCashbackAmount.ClientID %>').val());
        if (cashBackAmount < 0) {
            errMessage.text = 'Pembayaran Harus Lebih Besar Dari Tagihan';
            isAllowSave = false;
        }
        if (isAllowSave) {
            var isValid = grdPayment.validate();
            if (isValid) {
                $('#<%=hdnInlineEditingData.ClientID %>').val(grdPayment.getTableData());
                return true;
            }
            errMessage.text = 'Informasi Kartu Harus Diisi';
            return false;
        }
        return false;
    }
</script>
<input type="hidden" value="" id="hdnInvoiceID" runat="server" />
<input type="hidden" value="" id="hdnInlineEditingData" runat="server" />
<input type="hidden" value="" id="hdnCreditCardFeeFilterExpression" runat="server" />  
<input type="hidden" value="" id="hdnTotalPaymentAmount" runat="server" />  
<input type="hidden" value="" id="hdnTotalFeeAmount" runat="server" />  
<input type="hidden" value="" id="hdnPaymentHdID" runat="server" />  
<input type="hidden" value="" id="hdnCashbackAmount" runat="server" />  
<input type="hidden" value="" id="hdnBillingTotal" runat="server" />  
<div style="height:442px;overflow-y:auto;">
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Informasi Pembayaran") %></h4>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><div style="position: relative;"><label><%=GetLabel("No Pembayaran")%></label></div></td>
                        <td><asp:TextBox ID="txtPaymentNo" Width="150px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal / Jam")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-right: 1px;width:145px"><asp:TextBox ID="txtPaymentDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    <td style="width:5px">&nbsp;</td>
                                    <td><asp:TextBox ID="txtPaymentTime" Width="80px" CssClass="time" runat="server" Style="text-align:center" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Pembayaran")%></label></td>
                        <td>
                            <dxe:ASPxComboBox ID="cboPaymentType" ClientInstanceName="cboPaymentType" Width="100%" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboPaymentTypeValueChanged(); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Informasi Tagihan") %></h4>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Faktur")%></label></td>
                        <td><asp:TextBox ID="txtInvoiceNo" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Tagihan")%></label></td>
                        <td><asp:TextBox ID="txtInvoiceTotal" CssClass="txtCurrency" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Pembayaran")%></label></td>
                        <td><asp:TextBox ID="txtPayment" CssClass="txtCurrency" Width="150px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h4 style="text-align:left"><%=GetLabel("Detil Pembayaran")%></h4>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="divContainerGrdDetailAdd">
                    <table class="grdNormal grdBorder" id="tblPaymentDt" style="width:100%;font-size:0.9em" cellpadding="0" cellspacing="0">
                        <tr>  
                            <th rowspan="2" align="center" style="width:30px">
                                <div style="padding:3px;">#</div>
                            </th>
                            <th rowspan="2" align="left">
                                <div><%= GetLabel("Metode Pembayaran")%></div>
                            </th>
                            <th colspan="2" class="thCenter"><%=GetLabel("Kartu Kredit")%></th>
                            <th colspan="2" class="thCenter"><%=GetLabel("Informasi Bank")%></th>
                            <th colspan="3" class="thCenter"><%=GetLabel("Jumlah")%></th>
                        </tr>
                        <tr>
                            <th style="width:120px" class="thCenter"><%=GetLabel("EDC")%></th>
                            <th style="width:180px" class="thCenter"><%=GetLabel("Informasi Kartu")%></th>
                            <th style="width:150px" class="thCenter"><%=GetLabel("Bank")%></th>
                            <th style="width:150px" class="thCenter"><%=GetLabel("No Referensi")%></th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Pembayaran")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Fee")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Line Total")%>
                                </div>
                            </th>
                        </tr>
                        <tr class="trFooter">  
                            <td colspan="6">
                                <div style="text-align:right;padding:3px">
                                    <%=GetLabel("Total")%>
                                </div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdTotalPatient">0</div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdTotalCardFee">0</div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdTotalLineTotal">0</div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContainerGrdDetailAR" style="display:none">
                    <table class="grdNormal" id="Table1" style="width:100%;font-size:0.9em" cellpadding="0" cellspacing="0">
                        <tr>  
                            <th rowspan="2" align="left">
                                <div style="padding:3px;float:left;">
                                    <div><%= GetLabel("Metode Pembayaran")%></div>
                                </div>
                            </th>
                            <th colspan="2"><%=GetLabel("Kartu Kredit")%></th>
                            <th colspan="2"><%=GetLabel("Informasi Bank")%></th>
                            <th colspan="3"><%=GetLabel("Jumlah")%></th>
                        </tr>
                        <tr>
                            <th style="width:120px">
                                <div style="padding-left:3px">
                                    <%=GetLabel("EDC")%>
                                </div>
                            </th>
                            <th style="width:180px">
                                <div style="padding-left:3px">
                                    <%=GetLabel("Informasi Kartu")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="padding-left:3px">
                                    <%=GetLabel("Bank")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="padding-left:3px">
                                    <%=GetLabel("No Referensi")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Pembayaran")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Fee")%>
                                </div>
                            </th>
                            <th style="width:150px">
                                <div style="text-align:right;padding-right:3px">
                                    <%=GetLabel("Line Total")%>
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td id="tdARPaymentMethod" runat="server"></td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right" id="tdPaymentDtAR">0</td>
                            <td align="right">0</td>
                            <td align="right" id="tdLineAmountAR">0</td>
                        </tr>
                        <tr class="trFooter">  
                            <td colspan="5">
                                <div style="text-align:right;padding:3px">
                                    <%=GetLabel("Total")%>
                                </div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdTotalAR">0</div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdTotalCardFeeAR">0</div>
                            </td>
                            <td>
                                <div style="text-align:right;padding:3px" id="tdLineTotalAR">0</div>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width:100%" id="tblCashback">
                    <tr>
                        <td align="right" style="padding-right:5px"><%=GetLabel("Uang Kembalian") %></td>
                        <td style="width:150px"><asp:TextBox ID="txtCashReturnAmount" runat="server" CssClass="txtCurrency min" Width="150px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>  
    <dxe:ASPxComboBox ID="cboPaymentMethod" ClientInstanceName="cboPaymentMethod" runat="server" Width="100%" 
        EnableSynchronization="False" ClientVisible="false" IncrementalFilteringMode="Contains" >
        <ClientSideEvents Init="function(s,e) { onInit(); }"
            LostFocus="function(s,e){ grdPayment.hideAspxComboBox(s); }" 
            KeyDown="grdPayment.onCboKeyDown" />
    </dxe:ASPxComboBox>
    <dxe:ASPxComboBox ID="cboBank" ClientInstanceName="cboBank" runat="server" Width="100%" 
        EnableSynchronization="False" ClientVisible="false" IncrementalFilteringMode="Contains" >
        <ClientSideEvents Init="function(s,e) { onInit(); }"
            LostFocus="function(s,e){ grdPayment.hideAspxComboBox(s); }" 
            KeyDown="grdPayment.onCboKeyDown" />
    </dxe:ASPxComboBox>
    <dxe:ASPxComboBox ID="cboEDCMachine" ClientInstanceName="cboEDCMachine" runat="server" Width="100%" 
        EnableSynchronization="False" ClientVisible="false" IncrementalFilteringMode="Contains" >
        <ClientSideEvents Init="function(s,e) { onInit(); }"
            LostFocus="function(s,e){ grdPayment.hideAspxComboBox(s); }" 
            KeyDown="grdPayment.onCboKeyDown" />
    </dxe:ASPxComboBox>
    <div id="containerCbo" style="display:none"></div>

    <!-- Popup Entry Notes -->
    <dxpc:ASPxPopupControl ID="pcCardInformation" runat="server" ClientInstanceName="pcCardInformation" CloseAction="CloseButton"
        Height="180px" HeaderText="Informasi Kartu" Width="400px" Modal="True" PopupAction="None"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" ID="pccc1">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <fieldset id="fsCardInformation" style="margin:0"> 
                                <div style="text-align: left; width: 100%;">
                                    <table>
                                        <colgroup>
                                            <col style="width: 500px"/>
                                        </colgroup>
                                        <tr>
                                            <td valign="top">
                                                <table>
                                                    <colgroup>
                                                        <col style="width:150px"/>
                                                        <col style="width:200px"/>
                                                    </colgroup>
                                                    <tr>
                                                        <td><%=GetLabel("Tipe Kartu")%></td>
                                                        <td><dxe:ASPxComboBox ID="cboCardType" ClientInstanceName="cboCardType" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Bank Penerbit")%></td>
                                                        <td><dxe:ASPxComboBox ID="cboCardProvider" ClientInstanceName="cboCardProvider" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("No Kartu")%></td>
                                                        <td>
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><asp:TextBox ID="txtCardNumber1" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber2" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber3" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber4" Width="100%" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Pemegang Kartu")%></td>
                                                        <td><asp:TextBox ID="txtHolderName" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Masa Berlaku")%></td>
                                                        <td>
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><dxe:ASPxComboBox ID="cboCardDateMonth" ClientInstanceName="cboCardDateMonth" Width="100px" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><dxe:ASPxComboBox ID="cboCardDateYear" ClientInstanceName="cboCardDateYear" Width="80px" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                     
                                                </table>  
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
                                        <tr>
                                            <td>
                                                <input type="button" id="btnPaymentCardInformationAdd" value='<%= GetLabel("Ok")%>' />
                                            </td>
                                            <td>
                                                <input type="button" id="btnPaymentCardInformationCancel" value='<%= GetLabel("Batal")%>' onclick="closePcCardInformation('cancel');" />
                                            </td>
                                            <td>
                                                <input type="button" id="btnPaymentCardInformationClose" value='<%= GetLabel("Tutup")%>' onclick="pcCardInformation.Hide();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>                                
                            </fieldset>                                
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</div>
