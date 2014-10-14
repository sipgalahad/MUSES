<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
CodeBehind="JournalEntry.aspx.cs" Inherits="Codex.Muses.Web.Accounting.Program.JournalEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divTemplatePick').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divTemplatePick').hide();
            }

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');

                    $('#<%=hdnGLAccount1ID.ClientID %>').val('');
                    $('#<%=txtGLAccount1Code.ClientID %>').val('');
                    $('#<%=txtGLAccount1Name.ClientID %>').val('');
                    $('#<%=hdnSubLedgerID1.ClientID %>').val('');
                    $('#<%=hdnSearchDialogTypeName1.ClientID %>').val('');
                    $('#<%=hdnIDFieldName1.ClientID %>').val('');
                    $('#<%=hdnCodeFieldName1.ClientID %>').val('');
                    $('#<%=hdnDisplayFieldName1.ClientID %>').val('');
                    $('#<%=hdnMethodName1.ClientID %>').val('');
                    $('#<%=hdnFilterExpression1.ClientID %>').val('');
                    $('#<%=txtReferenceNo.ClientID %>').val('');
                    $('#<%=txtSaldoReference.ClientID %>').val('0').trigger('changeValue');
                    onSubLedgerID1Changed();
                    $('#<%=hdnSubLedgerDt1ID.ClientID %>').val('');
                    $('#<%=txtSubLedgerDt1Code.ClientID %>').val('');
                    $('#<%=txtSubLedgerDt1Name.ClientID %>').val('');
                    $('#<%=txtAmountD.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtAmountK.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtDisplayOrder.ClientID %>').val($('#<%=hdnDisplayCount.ClientID %>').val());
                    //$('#<%=txtRemarksDt.ClientID %>').val('');

                    $('#btnGLAccount').attr('enabled', 'false');
                    $('#btnSubLedger').attr('enabled', 'false');

                    $('#entryDetailContainer').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            $('#<%=txtAmountD.ClientID %>').change(function () {
                $('#<%=txtAmountK.ClientID %>').val(0).trigger('changeValue');
                $(this).trigger('changeValue');
            });

            $('#<%=txtAmountK.ClientID %>').change(function () {
                $('#<%=txtAmountD.ClientID %>').val(0).trigger('changeValue');
                $(this).trigger('changeValue');
            });

            if (getIsAdd()) {
                setDatePicker('<%=txtJournalDate.ClientID %>');
                $('#<%=txtJournalDate.ClientID %>').datepicker('option', 'maxDate', '0');
                var minDate = parseInt('<%=minDate %>');
                if (minDate > -1) 
                    $('#<%=txtJournalDate.ClientID %>').datepicker('option', 'minDate', '-' + minDate);
            }

            $('#divTemplatePick').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Journal/JournalTemplateCtl.ascx');
                    var glTransactionID = $('#<%=hdnID.ClientID %>').val();
                    var id = glTransactionID;
                    openUserControlPopup(url, id, 'Template', 600, 300);
                }
            });
        }

        //#region Document
        function onGetDocumentFilterExpression() {
            var filterExpression = "ReferenceNo IS NOT NULL AND GLAccount = " + $('#<%=hdnGLAccount1ID.ClientID %>').val();
            if ($('#<%=hdnSubLedgerDt1ID.ClientID %>').val() != "0" && $('#<%=hdnSubLedgerDt1ID.ClientID %>').val() != "") filterExpression += " AND SubLedger = " + $('#<%=hdnSubLedgerDt1ID.ClientID %>').val();
            else filterExpression += " AND SubLedger IS NULL";
            return filterExpression;
        }

        $('#lblDocument').live('click', function () {
            if ($('#<%=hdnGLAccount1ID.ClientID %>').val() != '' && $('#<%=hdnGLAccount1ID.ClientID %>').val() != '0') {
                openSearchDialog('glbalancedtdocument', onGetDocumentFilterExpression(), function (value) {
                    $('#<%=txtReferenceNo.ClientID %>').val(value);
                    onTxtReferenceNoChanged(value);
                });
            }
        });

        $('#<%=txtReferenceNo.ClientID %>').live('change', function () {
            onTxtReferenceNoChanged($(this).val());
        });

        function onTxtReferenceNoChanged(value) {
            var filterExpression = onGetDocumentFilterExpression() + " AND ReferenceNo = '" + value + "'";
            Methods.getObject('GetGLBalanceDtDocumentList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=txtSaldoReference.ClientID %>').val(result.BalanceEND).trigger('changeValue');
                }
                else {
                    $('#<%=txtSaldoReference.ClientID %>').val('');
                }
            });
        }

        //#endregion
        
        //#region GL Account 1
        function onGetGLAccountFilterExpression() {
            var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
            return filterExpression;
        }

        $('#lblGLAccount1.lblLink').live('click', function () {
            openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                $('#<%=txtGLAccount1Code.ClientID %>').val(value);
                onTxtGLAccount1CodeChanged(value);
                $('#<%=txtRemarksDt.ClientID %>').focus();
            });
        });

        $('#<%=txtGLAccount1Code.ClientID %>').live('change', function () {
            onTxtGLAccount1CodeChanged($(this).val());
        });

        function onTxtGLAccount1CodeChanged(value) {
            var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
            Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnGLAccount1ID.ClientID %>').val(result.GLAccountID);
                    $('#<%=txtGLAccount1Name.ClientID %>').val(result.GLAccountName);

                    $('#<%=hdnSubLedgerID1.ClientID %>').val(result.SubLedgerID);
                    $('#<%=hdnSearchDialogTypeName1.ClientID %>').val(result.SearchDialogTypeName);
                    $('#<%=hdnFilterExpression1.ClientID %>').val(result.FilterExpression);
                    $('#<%=hdnIDFieldName1.ClientID %>').val(result.IDFieldName);
                    $('#<%=hdnCodeFieldName1.ClientID %>').val(result.CodeFieldName);
                    $('#<%=hdnDisplayFieldName1.ClientID %>').val(result.DisplayFieldName);
                    $('#<%=hdnMethodName1.ClientID %>').val(result.MethodName);

                    $('#btnGLAccount').removeAttr('enabled');
                }
                else {
                    $('#<%=hdnGLAccount1ID.ClientID %>').val('');
                    $('#<%=txtGLAccount1Code.ClientID %>').val('');
                    $('#<%=txtGLAccount1Name.ClientID %>').val('');

                    $('#<%=hdnSubLedgerID1.ClientID %>').val('');
                    $('#<%=hdnSearchDialogTypeName1.ClientID %>').val('');
                    $('#<%=hdnFilterExpression1.ClientID %>').val('');
                    $('#<%=hdnIDFieldName1.ClientID %>').val('');
                    $('#<%=hdnCodeFieldName1.ClientID %>').val('');
                    $('#<%=hdnDisplayFieldName1.ClientID %>').val('');
                    $('#<%=hdnMethodName1.ClientID %>').val('');

                    $('#btnGLAccount').attr('enabled', 'false');
                }
                onSubLedgerID1Changed();
                $('#<%=hdnSubLedgerDt1ID.ClientID %>').val('');
                $('#<%=txtSubLedgerDt1Code.ClientID %>').val('');
                $('#<%=txtSubLedgerDt1Name.ClientID %>').val('');
            });
        }

        function onSubLedgerID1Changed() {
            if ($('#<%=hdnSubLedgerID1.ClientID %>').val() == '0' || $('#<%=hdnSubLedgerID1.ClientID %>').val() == '') {
                $('#<%=lblSubLedgerDt1.ClientID %>').attr('class', 'lblDisabled');
                $('#btnSubLedger').attr('enabled', 'false');
                $('#<%=txtSubLedgerDt1Code.ClientID %>').attr('readonly', 'readonly');
            }
            else {
                $('#<%=lblSubLedgerDt1.ClientID %>').attr('class', 'lblLink lblMandatory');
                $('#<%=txtSubLedgerDt1Code.ClientID %>').removeAttr('readonly');
                $('#btnSubLedger').removeAttr('enabled');
            }
        }
        //#endregion

        //#region Sub Ledger 1
        function onGetSubLedgerDt1FilterExpression() {
            var filterExpression = $('#<%=hdnFilterExpression1.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnSubLedgerID1.ClientID %>').val());
            return filterExpression;
        }

        $('#<%=lblSubLedgerDt1.ClientID %>.lblLink').live('click', function () {
            if ($('#<%=hdnSearchDialogTypeName1.ClientID %>').val() != '') {
                openSearchDialog($('#<%=hdnSearchDialogTypeName1.ClientID %>').val(), onGetSubLedgerDt1FilterExpression(), function (value) {
                    $('#<%=txtSubLedgerDt1Code.ClientID %>').val(value);
                    onTxtSubLedgerDt1CodeChanged(value);
                });
            }
        });

        $('#<%=txtSubLedgerDt1Code.ClientID %>').live('change', function () {
            onTxtSubLedgerDt1CodeChanged($(this).val());
        });

        function onTxtSubLedgerDt1CodeChanged(value) {
            if ($('#<%=hdnSearchDialogTypeName1.ClientID %>').val() != '') {
                var filterExpression = onGetSubLedgerDt1FilterExpression() + " AND " + $('#<%=hdnCodeFieldName1.ClientID %>').val() + " = '" + value + "'";
                Methods.getObject($('#<%=hdnMethodName1.ClientID %>').val(), filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSubLedgerDt1ID.ClientID %>').val(result[$('#<%=hdnIDFieldName1.ClientID %>').val()]);
                        $('#<%=txtSubLedgerDt1Name.ClientID %>').val(result[$('#<%=hdnDisplayFieldName1.ClientID %>').val()]);
                    }
                    else {
                        $('#<%=hdnSubLedgerDt1ID.ClientID %>').val('');
                        $('#<%=txtSubLedgerDt1Code.ClientID %>').val('');
                        $('#<%=txtSubLedgerDt1Name.ClientID %>').val('');
                    }
                });
            }
        }
        //#endregion

        //#region Journal No
        $('#lblJournalNo.lblLink').live('click', function () {
            openSearchDialog('gltransactionhd', '', function (value) {
                $('#<%=txtJournalNo.ClientID %>').val(value);
                onTxtJournalNoChanged(value);
            });
        });

        $('#<%=txtJournalNo.ClientID %>').live('change', function () {
            onTxtJournalNoChanged($(this).val());
        });

        function onTxtJournalNoChanged(value) {
            onLoadObject(value);
        }
        //#endregion

        function onLoadCurrentRecord() {
            onLoadObject($('#<%=txtJournalNo.ClientID %>').val());
        }

        function onAfterSaveRecordDtSuccess(GLTransactionID) {
            if ($('#<%=hdnID.ClientID %>').val() == '' || $('#<%=hdnID.ClientID %>').val() == '0') {
                $('#<%=hdnID.ClientID %>').val(GLTransactionID);
                var filterExpression = 'GLTransactionID = ' + GLTransactionID;
                Methods.getObject('GetGLTransactionHdList', filterExpression, function (result) {
                    $('#<%=tdTransactionNoAdd.ClientID %>').attr('style', 'display:none');
                    $('#<%=tdTransactionNoEdit.ClientID %>').removeAttr('style');
                    $('#<%=txtJournalNo.ClientID %>').val(result.JournalNo);
                    cboTransactionCode.SetEnabled(false);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            } else {
                cbpView.PerformCallback('refresh');
            }
        }

        var isAfterAdd = false;
        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    isAfterAdd = true;
                    var GLTransactionID = s.cpGLTransactionID;
                    onAfterSaveRecordDtSuccess(GLTransactionID);
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else {
                    isAfterAdd = false;
                    cbpView.PerformCallback('refresh');
                }
            }
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                $('#<%=txtTotalDebet.ClientID %>').val(param[1]).trigger('changeValue');
                $('#<%=txtTotalKredit.ClientID %>').val(param[2]).trigger('changeValue');
                $('#<%=txtTotalSelisih.ClientID %>').val(param[3]).trigger('changeValue');

                if (isAfterAdd)
                    $('#divTransactionAdd').click();
            }
        }
        //#endregion

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
            $('#<%=hdnEntryID.ClientID %>').val(entity.TransactionDtID);

            $('#<%=hdnGLAccount1ID.ClientID %>').val(entity.GLAccount);
            $('#<%=txtGLAccount1Code.ClientID %>').val(entity.GLAccountNo);
            $('#<%=txtGLAccount1Name.ClientID %>').val(entity.GLAccountName);
            $('#<%=hdnSubLedgerID1.ClientID %>').val(entity.SubLedgerID);
            $('#<%=hdnSearchDialogTypeName1.ClientID %>').val(entity.SearchDialogTypeName);
            $('#<%=hdnIDFieldName1.ClientID %>').val(entity.IDFieldName);
            $('#<%=hdnCodeFieldName1.ClientID %>').val(entity.CodeFieldName);
            $('#<%=hdnDisplayFieldName1.ClientID %>').val(entity.DisplayFieldName);
            $('#<%=hdnMethodName1.ClientID %>').val(entity.MethodName);
            $('#<%=hdnFilterExpression1.ClientID %>').val(entity.FilterExpression);

            $('#btnGLAccount').removeAttr('enabled');
            onSubLedgerID1Changed();
            $('#<%=hdnSubLedgerDt1ID.ClientID %>').val(entity.SubLedger);
            $('#<%=txtSubLedgerDt1Code.ClientID %>').val(entity.SubLedgerCode);
            $('#<%=txtSubLedgerDt1Name.ClientID %>').val(entity.SubLedgerName);
            $('#<%=txtAmountD.ClientID %>').val(entity.DebitAmount).trigger('changeValue');
            $('#<%=txtAmountK.ClientID %>').val(entity.CreditAmount).trigger('changeValue');
            $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
            $('#<%=txtRemarksDt.ClientID %>').val(entity.Remarks);
            $('#<%=txtReferenceNo.ClientID %>').val(entity.ReferenceNo);
            $('#<%=txtSaldoReference.ClientID %>').val(entity.BalanceEND).trigger('changeValue');
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCboTransactionCodeValueChanged(s) {
            var value = s.GetValue();
            var filterExpression = "TransactionCode = '" + value + "'";
            Methods.getObject('GetTransactionTypeList', filterExpression, function (result) {
                if (result != null)
                    $('#<%=txtJournalPrefix.ClientID %>').val(result.TransactionInitial);
                else
                    $('#<%=txtJournalPrefix.ClientID %>').val('');
            });
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
        }

        $('#btnGLAccount').live('click', function () {
            if ($(this).attr('enabled') == null) {
                var accountID = $('#<%=hdnGLAccount1ID.ClientID %>').val();
                var url = ResolveUrl('~/Program/Information/GLBalanceInformationCtl.ascx');
                var id = accountID;
                var date = $('#<%=txtJournalDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = id + '|' + period;
                openUserControlPopup(url, param, 'Detail', 900, 600);
            }
        });

        $('#btnSubLedger').live('click', function () {
            if ($(this).attr('enabled') == null) {
                var subLedgerDtID = $('#<%=hdnSubLedgerDt1ID.ClientID %>').val();
                var glAccountID = $('#<%=hdnGLAccount1ID.ClientID %>').val();
                var url = ResolveUrl('~/Program/Information/GLSubLedgerInformationCtl.ascx');
                var code = $('#<%=txtSubLedgerDt1Code.ClientID %>').val();
                var name = $('#<%=txtSubLedgerDt1Name.ClientID %>').val();
                var date = $('#<%=txtJournalDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = glAccountID + '|' + subLedgerDtID + '|' + period + '|' + code + '|' + name;
                openUserControlPopup(url, param, 'Detail', 900, 600);
            }
        });

        $('#btnReference').live('click', function () {
            if ($('#<%=txtReferenceNo.ClientID %>').val() != '') {
                var url = ResolveUrl('~/Program/Journal/JournalDocumentCtl.ascx');
                var referenceNo = $('#<%=txtReferenceNo.ClientID %>').val();
                var glAccount = $('#<%=hdnGLAccount1ID.ClientID %>').val();
                var subLedgerDt = "0";
                if ($('#<%=hdnSubLedgerDt1ID.ClientID %>').val() != "" && $('#<%=hdnSubLedgerDt1ID.ClientID %>').val() != "0")
                    subLedgerDt = $('#<%=hdnSubLedgerDt1ID.ClientID %>').val();
                var param = glAccount + '|' + subLedgerDt + '|' + referenceNo;
                openUserControlPopup(url, param, 'Document Detail', 1000, 600);
            }
        });

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var transactionID = $('#<%=hdnID.ClientID %>').val();

            if (transactionID == '' || transactionID == '0') {
                errMessage.text = 'Pilih Jurnal Terlebih Dahulu!';
                return false;
            }
            else {
                var status = $('#<%=hdnGCTransactionStatus.ClientID %>').val();
                if (status == "<%=GetGCTransactionStatusOpen() %>") {
                    errMessage.text = 'Jurnal Belum di Approve';
                    return false;
                } else {
                    filterExpression.text = 'GLTransactionID = ' + transactionID;
                    return true;
                }
            }
        }
    </script>
    <style type="text/css">
        .rblJournalGroup input[type="radio"]            { margin-left: 40px; margin-right: 1px; }
    </style>
    <input type="hidden" id="hdnGCTransactionStatus" runat="server" value="" />
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnLastPostingDate" runat="server" value="" />
    <input type="hidden" id="hdnIsEditable" runat="server" value="" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber Data") %></label></td>
                        <td>
                            <dxe:ASPxComboBox ID="cboTransactionCode" ClientInstanceName="cboTransactionCode" Width="100%" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboTransactionCodeValueChanged(s); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblJournalNo"><%=GetLabel("Nomor Jurnal") %></label></td>
                        <td id="tdTransactionNoAdd" runat="server">
                            <table  cellpadding="0" cellspacing="0" width="100%">
                                <colgroup>
                                    <col style="width: 50px" />
                                    <col style="width: 3px" />
                                    <col style="width: 160px"/>
                                    <col style="width: 100px" />
                                    <col style="width: 140px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtJournalPrefix" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtJournalNo1" Width="100%" runat="server" ReadOnly="true" /></td>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtJournalDate" CssClass="datepicker" Width="120px" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="display:none;" id="tdTransactionNoEdit" runat="server"><asp:TextBox runat="server" ID="txtJournalNo" Width="220px" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="width: 150px; vertical-align:top; padding-top:5px; "><label class="lblNormal"><%=GetLabel("Keterangan Jurnal")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divTransactionEntry">
                    <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Barang")%></span>
                    <span id="divTemplatePick" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Template")%></span>
                    <br />
                    <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrx" style="margin: 0">
                            <input type="hidden" value="" id="hdnEntryID" runat="server" />
                            <table style="width: 50%">
                                <colgroup>
                                    <col style="width: 150px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblGLAccount1"><%=GetLabel("Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnGLAccount1ID" runat="server" />
                                        <input type="hidden" id="hdnSubLedgerID1" runat="server" />
                                        <input type="hidden" id="hdnSearchDialogTypeName1" runat="server" />
                                        <input type="hidden" id="hdnIDFieldName1" runat="server" />
                                        <input type="hidden" id="hdnCodeFieldName1" runat="server" />
                                        <input type="hidden" id="hdnDisplayFieldName1" runat="server" />
                                        <input type="hidden" id="hdnMethodName1" runat="server" />
                                        <input type="hidden" id="hdnFilterExpression1" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtGLAccount1Code" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtGLAccount1Name" ReadOnly="true" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><input type="button" id="btnGLAccount" class="btnMore" value="..." /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblSubLedgerDt1"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnSubLedgerDt1ID" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtSubLedgerDt1Code" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtSubLedgerDt1Name" ReadOnly="true" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><input type="button" id="btnSubLedger" class="btnMore" value="..." /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="width: 120px; vertical-align:top; padding-top:5px; "><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                    <td><asp:TextBox ID="txtRemarksDt" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="text-align:right; vertical-align:bottom;"><label class="lblNormal"><%=GetLabel("Jumlah") %></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td><div class="lblComponent" style="text-align:right;padding-right:5px;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("Debit") %></div></td>
                                                <td style="width:3px"></td>
                                                <td><div class="lblComponent" style="text-align:right;padding-right:5px;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("Kredit") %></div></td>
                                            </tr>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtAmountD" CssClass="txtCurrency" Width="99%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtAmountK" CssClass="txtCurrency" Width="99%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal lblLink" id="lblDocument"><%=GetLabel("Dokumen") %></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtReferenceNo" runat="server" Width="120px"/></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtSaldoReference" runat="server" ReadOnly="true" CssClass="txtCurrency" Width="220px" /></td>
                                                <td>&nbsp;</td>
                                                <td><input type="button" id="btnReference" class="btnMore" value="..." /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Display Order") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtDisplayOrder" Width="120px" CssClass="txtNumeric" /></td>
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
                                position: relative;">
                                <input type="hidden" value="0" id="hdnDisplayCount" runat="server" />
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate><%=GetLabel("Perkiraan")%></HeaderTemplate>
                                            <ItemTemplate>
                                                <div><%#Eval("GLAccountNo")%></div>
                                                <div><%#Eval("SubLedgerCode")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="300px" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate><%=GetLabel("Nama Perkiraan")%></HeaderTemplate>
                                            <ItemTemplate>
                                                <div><%#Eval("GLAccountName")%></div>
                                                <div><%#Eval("SubLedgerName")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Keterangan Transaksi" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" HeaderStyle-CssClass="thRight">
                                            <HeaderTemplate>
                                                <div style="text-align:right; padding-right:5px;">DEBET</div>    
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Position").ToString() == "D" ? Eval("DebitAmount", "{0:N}") : "0"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" HeaderStyle-CssClass="thRight">
                                            <HeaderTemplate>
                                                <div style="text-align:right; padding-right:5px;">KREDIT</div>    
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Position").ToString() == "K" ? Eval("CreditAmount", "{0:N}") : "0"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10px" />
                                        <asp:BoundField DataField="ReferenceNo" HeaderText="No. Dokumen" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="130px" />
                                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                <input type="hidden" value="<%#Eval("TransactionDtID") %>" bindingfield="TransactionDtID" />
                                                <input type="hidden" value="<%#Eval("GLAccount") %>" bindingfield="GLAccount" />
                                                <input type="hidden" value="<%#Eval("GLAccountNo") %>" bindingfield="GLAccountNo" />
                                                <input type="hidden" value="<%#Eval("GLAccountName") %>" bindingfield="GLAccountName" />
                                                <input type="hidden" value="<%#Eval("SubLedgerID") %>" bindingfield="SubLedgerID" />
                                                <input type="hidden" value="<%#Eval("SearchDialogTypeName") %>" bindingfield="SearchDialogTypeName" />
                                                <input type="hidden" value="<%#Eval("IDFieldName") %>" bindingfield="IDFieldName" />
                                                <input type="hidden" value="<%#Eval("CodeFieldName") %>" bindingfield="CodeFieldName" />
                                                <input type="hidden" value="<%#Eval("DisplayFieldName") %>" bindingfield="DisplayFieldName" />
                                                <input type="hidden" value="<%#Eval("MethodName") %>" bindingfield="MethodName" />
                                                <input type="hidden" value="<%#Eval("FilterExpression") %>" bindingfield="FilterExpression" />
                                                <input type="hidden" value="<%#Eval("SubLedger") %>" bindingfield="SubLedger" />
                                                <input type="hidden" value="<%#Eval("SubLedgerCode") %>" bindingfield="SubLedgerCode" />
                                                <input type="hidden" value="<%#Eval("SubLedgerName") %>" bindingfield="SubLedgerName" />
                                                
                                                <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                                <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                                <input type="hidden" value="<%#Eval("DebitAmount") %>" bindingfield="DebitAmount" />
                                                <input type="hidden" value="<%#Eval("CreditAmount") %>" bindingfield="CreditAmount" />
                                                <input type="hidden" value="<%#Eval("ReferenceNo") %>" bindingfield="ReferenceNo" />
                                                <input type="hidden" value="<%#Eval("BalanceEND") %>" bindingfield="BalanceEND" />
                                                <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
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
                <div>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align:top">
                                <div style="width: 550px;">
                                    <div class="lblComponent" style="text-align:left; padding-left:5px;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("Informasi Jurnal") %></div>
                                    <div style="background-color: #EAEAEA;">
                                        <table width="450px" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col width="200px" />
                                                <col width="20px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td align="right"><%=GetLabel("Dibuat Oleh / Tanggal") %></td>
                                                <td align="center">:</td>
                                                <td><div runat="server" id="divCreatedBy" style="color:Maroon"></div></td>
                                            </tr>
                                            <tr>
                                                <td align="right"><%=GetLabel("Diubah Oleh / Tanggal") %></td>
                                                <td align="center">:</td>
                                                <td><div runat="server" id="divLastUpdatedBy" style="color:Maroon"></div></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp</td>
                                                <td>&nbsp</td>
                                                <td>&nbsp</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                            <td style="float:right;">
                                <table width="300px">
                                    <colgroup>
                                        <col width="120px" />
                                    </colgroup>
                                    <tr>
                                        <td><div class="lblComponent" style="text-align:right;padding-right:5px;padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL DEBET") %></div></td>
                                        <td><asp:TextBox ID="txtTotalDebet" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                                    </tr>
                                    <tr>
                                        <td><div class="lblComponent" style="text-align:right;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL KREDIT") %></div></td>
                                        <td><asp:TextBox ID="txtTotalKredit" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                                    </tr>
                                    <tr>
                                        <td><div class="lblComponent" style="text-align:right;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL SELISIH") %></div></td>
                                        <td><asp:TextBox ID="txtTotalSelisih" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                                    </tr>
                                </table>                                
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
