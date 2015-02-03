<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
CodeBehind="JournalEntry.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.JournalEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if (!isShowWatermark()) {
                $('#divTemplatePick').show();
                $('#divSaveTemplate').show();
            }
            else {
                $('#divTemplatePick').hide();
                $('#divSaveTemplate').hide();
            }

            if (getIsAdd()) {
                setDatePicker('<%=txtJournalDate.ClientID %>');
                $('#<%=txtJournalDate.ClientID %>').datepicker('option', 'maxDate', '0');
                var minDate = parseInt('<%=minDate %>');
                if (minDate > -1)
                    $('#<%=txtJournalDate.ClientID %>').datepicker('option', 'minDate', '-' + minDate);
            }

            $('#divSaveTemplate').click(function () {
                popupType = 'templateSave';
                onBeforeSaveRecord();
                showLoadingPanel();
                var url = ResolveUrl('~/Program/Journal/JournalTemplateSaveCtl.ascx');
                var id = $('#<%=hdnSaveParam.ClientID %>').val();
                openUserControlPopup(url, id, 'Save As Template', 600, 300);
            });

            $('#divTemplatePick').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    popupType = 'templatePick';
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Journal/JournalTemplateCtl.ascx');
                    var glTransactionID = $('#<%=hdnID.ClientID %>').val();
                    var id = glTransactionID;
                    openUserControlPopup(url, id, 'Template', 600, 300);
                }
            });

            //#region Journal No
            $('#lblJournalNo.lblLink').click(function () {
                openSearchDialog('gltransactionhd', '', function (value) {
                    $('#<%=txtJournalNo.ClientID %>').val(value);
                    onTxtJournalNoChanged(value);
                });
            });

            $('#<%=txtJournalNo.ClientID %>').change(function () {
                onTxtJournalNoChanged($(this).val());
            });

            function onTxtJournalNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Load
            if (!isShowWatermark()) {
                $('#tblJournalEntry').show();
                $('#tblJournalView').hide();
                var GLTransactionID = $('#<%=hdnID.ClientID %>').val();
                if (GLTransactionID != '0') {
                    var filterExpression = "GLTransactionID = " + GLTransactionID + " AND GCItemDetailStatus != 'X121^999' ORDER BY DisplayOrder ASC";
                    Methods.getListObject('GetvGLTransactionDtList', filterExpression, function (result) {
                        for (var i = 0; i < result.length; ++i) {
                            var entity = result[i];
                            $newTr = $('#tmplEntity').html().replace('script1', 'script').replace('script1', 'script');
                            $newTr = $newTr.replace(/\$\{idx}/g, idx);
                            $newTr = $($newTr);

                            $newTr.insertBefore($('#trFooter'));

                            $newTr.find('.txtCurrency').each(function () {
                                $(this).trigger('changeValue');
                            });

                            var tempHelper = new CodeXClientAutoCompleteHelper();
                            tempHelper.init("COA" + idx, "GLAccountNo,GLAccountName", "GetChartOfAccountList", "", "onGetCOAFilterExpression", "GLAccountNo");
                            tempHelper.setClientSideEvents(onGLAccountIDValueChanged);
                            tempHelper.initializeControl();
                            tempHelper.setValue(entity.GLAccount);
                            tempHelper.setText(entity.GLAccountName);

                            $newTr.find('.txtRemarks').val(entity.Remarks);
                            $newTr.find('.txtDebit').val(entity.DebitAmount).trigger('changeValue');
                            $newTr.find('.txtKredit').val(entity.CreditAmount).trigger('changeValue');
                            $newTr.find('.txtDocumentNo').val(entity.ReferenceNo);
                            $newTr.find('.hdnTransactionDtID').val(entity.TransactionDtID);

                            if (entity.ReferenceNo == '')
                                $newTr.find('.btnDocumentDetail').attr('enabled', false);
                            else
                                $newTr.find('.btnDocumentDetail').removeAttr('enabled');

                            $newTr.find('.hdnSubLedgerID').val(entity.SubLedgerID);
                            $newTr.find('.hdnSearchDialogTypeName').val(entity.SearchDialogTypeName);
                            $newTr.find('.hdnFilterExpression').val(entity.FilterExpression.replace('@SubLedgerID', entity.SubLedgerID));
                            $newTr.find('.hdnIDFieldName').val(entity.IDFieldName);
                            $newTr.find('.hdnCodeFieldName').val(entity.CodeFieldName);
                            $newTr.find('.hdnDisplayFieldName').val(entity.DisplayFieldName);
                            $newTr.find('.hdnMethodName').val(entity.MethodName);

                            if (entity.SubLedgerID != '0') {
                                var template = "<script class='tmpltAutoComplete' type='text/x-jquery-tmpl'><div>";
                                template += "${" + entity.DisplayFieldName + "} (<b>${" + entity.CodeFieldName + "}</b>";
                                template += "<input type='hidden' value='${" + entity.DisplayFieldName + "}' class='hdnAutoCompleteRowText'/>";
                                template += "<input type='hidden' value='${" + entity.IDFieldName + "}' class='hdnAutoCompleteRowValue'/>";
                                template += "<\/div><\/script>";

                                $newTr.find('.divSubLedgerTemplate').html(template);

                                var tempHelper = new CodeXClientAutoCompleteHelper();
                                tempHelper.init("SubCOA" + idx, entity.CodeFieldName + "," + entity.DisplayFieldName, entity.MethodName, entity.FilterExpression, "", entity.CodeFieldName);
                                tempHelper.setClientSideEvents(onSubLedgerIDValueChanged);
                                tempHelper.initializeControl();
                                tempHelper.setValue(entity.SubLedger);
                                tempHelper.setText(entity.SubLedgerName);

                                $newTr.find('.tacSubCOA').find('.txtAutoComplete').removeAttr('readonly');
                                $newTr.find('.tacSubCOA').find('.btnAutoCompleteSearchMore').removeAttr('enabled');
                            }

                            idx++;
                        }
                        calculateTotalDebitKredit();
                        addEntityRow();
                    });
                }
                else {
                    addEntityRow();
                    calculateTotalDebitKredit();
                }
            }
            else {
                $('#tblJournalEntry').hide();
                $('#tblJournalView').show();

                $('#tblJournalView').find('.txtCurrency').each(function () {
                    $(this).trigger('changeValue');
                });
            }
            //#endregion
        }

        var popupType = '';

        $tacTr = null;
        //#region COA
        function onGetCOAFilterExpression() {
            var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
            return filterExpression;
        }

        $('.tacCOA .btnAutoCompleteSearchMore').die('click');
        $('.tacCOA .btnAutoCompleteSearchMore').live('click', function () {
            $tacTr = $(this).closest('tr');
            openSearchDialog('chartofaccount', onGetCOAFilterExpression(), function (value) {
                var filterExpression = onGetCOAFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    $tacCOA = $tacTr.find('.tacCOA');
                    if (result != null) {
                        $tacCOA.find('.hdnAutoCompleteValue').val(result.GLAccountID);
                        $tacCOA.find('.hdnAutoCompleteText').val(result.GLAccountName);
                        $tacCOA.find('.txtAutoComplete').val(result.GLAccountName);
                    }
                    else {
                        $tacCOA.find('.hdnAutoCompleteValue').val('');
                        $tacCOA.find('.hdnAutoCompleteText').val('');
                        $tacCOA.find('.txtAutoComplete').val('');
                    }
                    entityToControlCOA(result);
                });
                var trIdx = $('.trJournalEntry').index($tacTr);
                if (trIdx == $('.trJournalEntry').length - 1)
                    addEntityRow();
                $tacTr = null;
            });
        });

        function entityToControlCOA(entity) {
            if (entity != null) {
                $tacTr.find('.hdnSubLedgerID').val(entity.SubLedgerID);
                $tacTr.find('.hdnSearchDialogTypeName').val(entity.SearchDialogTypeName);
                $tacTr.find('.hdnFilterExpression').val(entity.FilterExpression.replace('@SubLedgerID', entity.SubLedgerID));
                $tacTr.find('.hdnIDFieldName').val(entity.IDFieldName);
                $tacTr.find('.hdnCodeFieldName').val(entity.CodeFieldName);
                $tacTr.find('.hdnDisplayFieldName').val(entity.DisplayFieldName);
                $tacTr.find('.hdnMethodName').val(entity.MethodName);
                $tacTr.find('.btnCOADetail').removeAttr('enabled');

                var template = "<script class='tmpltAutoComplete' type='text/x-jquery-tmpl'><div>";
                template += "${" + entity.DisplayFieldName + "} (<b>${" + entity.CodeFieldName + "}</b>";
                template += "<input type='hidden' value='${" + entity.DisplayFieldName + "}' class='hdnAutoCompleteRowText'/>";
                template += "<input type='hidden' value='${" + entity.IDFieldName + "}' class='hdnAutoCompleteRowValue'/>";
                template += "<\/div><\/script>";

                $tacTr.find('.divSubLedgerTemplate').html(template);

                var id = $tacTr.find('.tacSubCOA').attr('id');
                var tempHelper = new CodeXClientAutoCompleteHelper();
                tempHelper.init(id, entity.CodeFieldName + "," + entity.DisplayFieldName, entity.MethodName, entity.FilterExpression, "", entity.CodeFieldName);
                tempHelper.setClientSideEvents(onSubLedgerIDValueChanged);
                tempHelper.initializeControl();
            }
            else {
                $tacTr.find('.hdnSubLedgerID').val('');
                $tacTr.find('.hdnSearchDialogTypeName').val('');
                $tacTr.find('.hdnFilterExpression').val('');
                $tacTr.find('.hdnIDFieldName').val('');
                $tacTr.find('.hdnCodeFieldName').val('');
                $tacTr.find('.hdnDisplayFieldName').val('');
                $tacTr.find('.hdnMethodName').val('');
                $tacTr.find('.btnCOADetail').attr('enabled', false);
            }
            $tacTr.find('.tacSubCOA').find('.hdnAutoCompleteValue').val('');
            $tacTr.find('.tacSubCOA').find('.hdnAutoCompleteText').val('');
            $tacTr.find('.tacSubCOA').find('.txtAutoComplete').val('');
            var subLedgerID = $tacTr.find('.hdnSubLedgerID').val();
            if (subLedgerID == '0' || subLedgerID == '') {
                $tacTr.find('.tacSubCOA').find('.btnAutoCompleteSearchMore').attr('enabled', false);
                $tacTr.find('.tacSubCOA').find('.txtAutoComplete').attr('readonly', 'readonly');
            }
            else {
                $tacTr.find('.tacSubCOA').find('.txtAutoComplete').removeAttr('readonly');
                $tacTr.find('.tacSubCOA').find('.btnAutoCompleteSearchMore').removeAttr('enabled');
            }
        }

        function onGLAccountIDValueChanged($s) {
            $tacTr = $s.closest('tr');
            if ($s.val() != '') {
                var glAccountID = $tacTr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                $tacTr.find('.btnCOADetail').removeAttr('enabled');

                var trIdx = $('.trJournalEntry').index($tacTr);
                if (trIdx == $('.trJournalEntry').length - 1)
                    addEntityRow();

                var filterExpression = onGetCOAFilterExpression() + " AND GLAccountID = " + glAccountID + "";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    entityToControlCOA(result);
                    $tacTr = null;
                });
            }
            else
                $tacTr.find('.btnCOADetail').attr('enabled', false);
        }
        //#endregion

        //#region Sub Ledger
        $('.tacSubCOA .btnAutoCompleteSearchMore').die('click');
        $('.tacSubCOA .btnAutoCompleteSearchMore').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tacTr = $(this).closest('tr');

                var subLedgerID = $tacTr.find('.hdnSubLedgerID').val();
                var searchDialogTypeName = $tacTr.find('.hdnSearchDialogTypeName').val();
                var baseFilterExpression = $tacTr.find('.hdnFilterExpression').val();
                var IDFieldName = $tacTr.find('.hdnIDFieldName').val();
                var codeFieldName = $tacTr.find('.hdnCodeFieldName').val();
                var displayFieldName = $tacTr.find('.hdnDisplayFieldName').val();
                var methodName = $tacTr.find('.hdnMethodName').val();

                if (subLedgerID != '') {
                    openSearchDialog(searchDialogTypeName, baseFilterExpression, function (value) {
                        var filterExpression = baseFilterExpression + " AND " + codeFieldName + " = '" + value + "'";
                        Methods.getObject(methodName, filterExpression, function (result) {
                            $tacSubCOA = $tacTr.find('.tacSubCOA');
                            if (result != null) {
                                $tacSubCOA.find('.hdnAutoCompleteValue').val(result[IDFieldName]);
                                $tacSubCOA.find('.hdnAutoCompleteText').val(result[displayFieldName]);
                                $tacSubCOA.find('.hdnAutoCompleteCode').val(result[codeFieldName]);
                                $tacSubCOA.find('.txtAutoComplete').val(result[displayFieldName]);
                                $tacSubCOA.find('.btnSubCOADetail').removeAttr('enabled');
                            }
                            else {
                                $tacSubCOA.find('.hdnAutoCompleteValue').val('');
                                $tacSubCOA.find('.hdnAutoCompleteText').val('');
                                $tacSubCOA.find('.hdnAutoCompleteCode').val('');
                                $tacSubCOA.find('.txtAutoComplete').val('');
                                $tacSubCOA.find('.btnSubCOADetail').attr('enabled', false);
                            }
                        });
                        $tacTr = null;
                    });
                }
            }
        });

        function onSubLedgerIDValueChanged($s) {
            if ($s.val() != '') {
                $tacTr = $s.closest('tr');
                var subLedgerDtID = $tacTr.find('.tacSubCOA').find('.hdnAutoCompleteValue').val();
                if (subLedgerDtID != '')
                    $tacTr.find('.btnSubCOADetail').removeAttr('enabled');
                else
                    $tacTr.find('.btnSubCOADetail').attr('enabled', false);
            }
            else
                $tacTr.find('.btnSubCOADetail').attr('enabled', false);
        }
        //#endregion

        //#region Add Row
        var idx = 1;
        function addEntityRow() {
            $newTr = $('#tmplEntity').html().replace('script1', 'script').replace('script1', 'script');
            $newTr = $newTr.replace(/\$\{idx}/g, idx);
            $newTr = $($newTr);
            $newTr.insertBefore($('#trFooter'));

            $newTr.find('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            var tempHelper = new CodeXClientAutoCompleteHelper();
            tempHelper.init("COA" + idx, "GLAccountNo,GLAccountName", "GetChartOfAccountList", "", "onGetCOAFilterExpression", "GLAccountNo");
            tempHelper.setClientSideEvents(onGLAccountIDValueChanged);
            tempHelper.initializeControl();

            idx++;
        }
        //#endregion

        //#region calculateTotalDebitKredit
        function calculateTotalDebit() {
            var totalDebit = 0;
            $('#tblJournalEntry .txtDebit').each(function () {
                totalDebit += parseFloat($(this).attr('hiddenVal'));
            });
            $('#txtTotalDebit').val(totalDebit).trigger('changeValue');
        }

        function calculateTotalKredit() {
            var totalKredit = 0;
            $('#tblJournalEntry .txtKredit').each(function () {
                totalKredit += parseFloat($(this).attr('hiddenVal'));
            });
            $('#txtTotalKredit').val(totalKredit).trigger('changeValue');
        }

        function calculateTotalDebitKredit() {
            calculateTotalDebit();
            calculateTotalKredit();
        }
        //#endregion

        //#region Delete Move Up Down
        $('.imgDelete.imgLink').live('click', function () {
            $tr = $(this).closest('tr');
            var trIdx = $('.trJournalEntry').index($tr);
            if (trIdx < $('.trJournalEntry').length - 1) {
                showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                    if (result) {
                        $tr.remove();

                        calculateTotalDebitKredit();
                    }
                });

            }
        });

        $('.imgUp.imgLink').live('click', function () {
            $tr = $(this).closest('tr');
            var glAccount = $tr.find('.tacCOA').find('.txtAutoComplete').val();
            if (glAccount != '') {
                var trIdx = $('.trJournalEntry').index($tr);
                if (trIdx > 0)
                    $tr.insertBefore($('.trJournalEntry:eq(' + (trIdx - 1) + ')'));
            }
        });

        $('.imgDown.imgLink').live('click', function () {
            $tr = $(this).closest('tr');
            var glAccount = $tr.find('.tacCOA').find('.txtAutoComplete').val();
            if (glAccount != '') {
                var trIdx = $('.trJournalEntry').index($tr);
                if (trIdx < $('.trJournalEntry').length - 2)
                    $tr.insertAfter($('.trJournalEntry:eq(' + (trIdx + 1) + ')'));
            }
        });
        //#endregion

        //#region Debit Kredit
        $('.txtDebit').live('blur', function () {
            $(this).trigger('changeValue');
            var value = parseFloat($(this).attr('hiddenVal'));
            if (value != 0)
                $(this).closest('tr').find('.txtKredit').val('0').trigger('changeValue');
            calculateTotalDebitKredit();
        });

        $('.txtKredit').live('blur', function () {
            $(this).trigger('changeValue');
            var value = parseFloat($(this).attr('hiddenVal'));
            if (value != 0)
                $(this).closest('tr').find('.txtDebit').val('0').trigger('changeValue');
            calculateTotalDebitKredit();
        });

        $('.txtKredit').live('focus', function () {
            var value = parseFloat($(this).attr('hiddenVal'));
            if (value == 0) {
                var debit = parseFloat($(this).closest('tr').find('.txtDebit').attr('hiddenVal'));
                if (debit == 0) {
                    var totalDebit = 0;
                    $('#tblJournalEntry .txtDebit').each(function () {
                        totalDebit += parseFloat($(this).attr('hiddenVal'));
                    });
                    var totalKredit = 0;
                    $('#tblJournalEntry .txtKredit').each(function () {
                        totalKredit += parseFloat($(this).attr('hiddenVal'));
                    });
                    $(this).val(totalDebit - totalKredit).trigger('changeValue');
                }
            }
        });
        //#endregion

        //#region Btn Detail Saldo Information 
        $('.btnCOADetail').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tr = $(this).closest('tr');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var url = ResolveUrl('~/Program/Journal/GLBalanceInformationCtl.ascx');
                var id = glAccountID;
                var date = $('#<%=txtJournalDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = id + '|' + period;
                openUserControlPopup(url, param, 'Detail', 900, 600);
            }
        });

        $('.btnSubCOADetail').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tr = $(this).closest('tr');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var url = ResolveUrl('~/Program/Journal/GLBalanceInformationCtl.ascx');
                var id = glAccountID;
                var date = $('#<%=txtJournalDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = id + '|' + period;
                openUserControlPopup(url, param, 'Detail', 900, 600);


                $tacSubCOA = $tr.find('.tacSubCOA');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var subLedgerDtID = $tacSubCOA.find('.hdnAutoCompleteValue').val();
                var url = ResolveUrl('~/Program/Journal/GLSubLedgerInformationCtl.ascx');

                var code = $tacSubCOA.find('.hdnAutoCompleteCode').val();
                var name = $tacSubCOA.find('.txtAutoComplete').val();

                var date = $('#<%=txtJournalDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = glAccountID + '|' + subLedgerDtID + '|' + period + '|' + code + '|' + name;
                openUserControlPopup(url, param, 'Detail', 900, 600);
            }
        });

        $('.btnDocumentDetail').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tr = $(this).closest('tr');
                $tacSubCOA = $tr.find('.tacSubCOA');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var subLedgerDtID = $tacSubCOA.find('.hdnAutoCompleteValue').val();
                if (subLedgerDtID == '')
                    subLedgerDtID = '0';
                var referenceNo = $tr.find('.txtDocumentNo').val();
                var url = ResolveUrl('~/Program/Journal/JournalDocumentCtl.ascx');
                var param = glAccountID + '|' + subLedgerDtID + '|' + referenceNo;
                openUserControlPopup(url, param, 'Document Detail', 1000, 600);
            }
        });
        //#endregion

        //#region Document No
        $('.txtDocumentNo').live('change', function () {
            if ($(this).val() == '')
                $(this).closest('tr').find('.btnDocumentDetail').attr('enabled', false);
            else
                $(this).closest('tr').find('.btnDocumentDetail').removeAttr('enabled');
        });

        $('.btnSearchDocument').die('click');
        $('.btnSearchDocument').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tr = $(this).closest('tr');

                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var subLedgerDtID = $tr.find('.tacSubCOA').find('.hdnAutoCompleteValue').val();
                if (subLedgerDtID == '')
                    subLedgerDtID = '0';

                if (glAccountID != '') {
                    var filterExpression = "ReferenceNo IS NOT NULL AND GLAccount = " + glAccountID;
                    if (subLedgerDtID != '0')
                        filterExpression += " AND SubLedger = " + subLedgerDtID;
                    else
                        filterExpression += " AND SubLedger IS NULL";
                    openSearchDialog('glbalancedtdocument', filterExpression, function (value) {
                        $tr.find('.txtDocumentNo').val(value);
                    });
                }
            }
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
            if (popupType == 'templatePick') {
                var temp = param.split('|');
                var templateID = temp[0];
                var amount = parseFloat(temp[1]);
                var filterExpression = "TemplateID = " + templateID + " AND IsDeleted = 0 ORDER BY DisplayOrder";
                Methods.getListObject('GetvJournalTemplateDtList', filterExpression, function (result) {
                    if (result != null) {
                        $('.trJournalEntry:last').remove();
                        for (var i = 0; i < result.length; ++i) {
                            var entity = result[i];
                            $newTr = $('#tmplEntity').html().replace('script1', 'script').replace('script1', 'script');
                            $newTr = $newTr.replace(/\$\{idx}/g, idx);
                            $newTr = $($newTr);

                            $newTr.insertBefore($('#trFooter'));

                            $newTr.find('.txtCurrency').each(function () {
                                $(this).trigger('changeValue');
                            });

                            var tempHelper = new CodeXClientAutoCompleteHelper();
                            tempHelper.init("COA" + idx, "GLAccountNo,GLAccountName", "GetChartOfAccountList", "", "onGetCOAFilterExpression", "GLAccountNo");
                            tempHelper.setClientSideEvents(onGLAccountIDValueChanged);
                            tempHelper.initializeControl();
                            tempHelper.setValue(entity.GLAccount);
                            tempHelper.setText(entity.GLAccountName);

                            var debitAmount = 0;
                            var creditAmount = 0;
                            if (entity.Position == 'D')
                                debitAmount = amount * entity.AmountPercentage / 100;
                            else
                                creditAmount = amount * entity.AmountPercentage / 100;

                            $newTr.find('.txtDebit').val(debitAmount).trigger('changeValue');
                            $newTr.find('.txtKredit').val(creditAmount).trigger('changeValue');

                            if (entity.ReferenceNo == '')
                                $newTr.find('.btnDocumentDetail').attr('enabled', false);
                            else
                                $newTr.find('.btnDocumentDetail').removeAttr('enabled');

                            $newTr.find('.hdnSubLedgerID').val(entity.SubLedgerID);
                            $newTr.find('.hdnSearchDialogTypeName').val(entity.SearchDialogTypeName);
                            $newTr.find('.hdnFilterExpression').val(entity.FilterExpression.replace('@SubLedgerID', entity.SubLedgerID));
                            $newTr.find('.hdnIDFieldName').val(entity.IDFieldName);
                            $newTr.find('.hdnCodeFieldName').val(entity.CodeFieldName);
                            $newTr.find('.hdnDisplayFieldName').val(entity.DisplayFieldName);
                            $newTr.find('.hdnMethodName').val(entity.MethodName);

                            if (entity.SubLedgerID != '0') {
                                var template = "<script class='tmpltAutoComplete' type='text/x-jquery-tmpl'><div>";
                                template += "${" + entity.DisplayFieldName + "} (<b>${" + entity.CodeFieldName + "}</b>";
                                template += "<input type='hidden' value='${" + entity.DisplayFieldName + "}' class='hdnAutoCompleteRowText'/>";
                                template += "<input type='hidden' value='${" + entity.IDFieldName + "}' class='hdnAutoCompleteRowValue'/>";
                                template += "<\/div><\/script>";

                                $newTr.find('.divSubLedgerTemplate').html(template);

                                var tempHelper = new CodeXClientAutoCompleteHelper();
                                tempHelper.init("SubCOA" + idx, entity.CodeFieldName + "," + entity.DisplayFieldName, entity.MethodName, entity.FilterExpression, "", entity.CodeFieldName);
                                tempHelper.setClientSideEvents(onSubLedgerIDValueChanged);
                                tempHelper.initializeControl();
                                tempHelper.setValue(entity.SubLedger);
                                tempHelper.setText(entity.SubLedgerName);

                                $newTr.find('.tacSubCOA').find('.txtAutoComplete').removeAttr('readonly');
                                $newTr.find('.tacSubCOA').find('.btnAutoCompleteSearchMore').removeAttr('enabled');
                            }

                            idx++;
                        }
                        calculateTotalDebitKredit();
                        addEntityRow();
                    }
                });
            }
        }

        function onBeforeSaveRecord() {
            var saveParam = '';
            var lstTransactionDtID = '';
            $('.trJournalEntry').each(function () {
                var glAccountID = $(this).find('.tacCOA').find('.hdnAutoCompleteValue').val();
                if (glAccountID != '') {
                    var subLedgerID = $(this).find('.tacSubCOA').find('.hdnAutoCompleteValue').val();
                    var remarks = $(this).find('.txtRemarks').val();
                    var debit = $(this).find('.txtDebit').attr('hiddenVal');
                    var kredit = $(this).find('.txtKredit').attr('hiddenVal');
                    var documentNo = $(this).find('.txtDocumentNo').val();
                    var transactionDtID = $(this).find('.hdnTransactionDtID').val();

                    if (transactionDtID != '0') {
                        if (lstTransactionDtID != '')
                            lstTransactionDtID += ',';
                        lstTransactionDtID += transactionDtID;
                    }

                    if (saveParam != '')
                        saveParam += '|';
                    saveParam += transactionDtID + ',' + glAccountID + ',' + subLedgerID + ',' + remarks + ',' + debit + ',' + kredit + ',' + documentNo;
                }
            });

            $('#<%=hdnSaveParam.ClientID %>').val(saveParam);
            $('#<%=hdnListTransactionDtID.ClientID %>').val(lstTransactionDtID);
            return true;
        }

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
    <input type="hidden" id="hdnSaveParam" runat="server" />
    <input type="hidden" id="hdnListTransactionDtID" runat="server" />
    <script id="tmplEntity" type="text/x-jquery-tmpl">
        <tr class="trJournalEntry">
            <td align="center">
                <input type="hidden" class="hdnTransactionDtID" value="0">
                <img src='<%=ResolveUrl("~/Libs/Images/Button/delete.png") %>' class="imgLink imgDelete" title="Hapus"/>
            </td>
            <td align="center">
                <img src='<%=ResolveUrl("~/Libs/Images/up-arrow.png") %>' class="imgLink imgUp" title="Move Up"/><br/>
                <img src='<%=ResolveUrl("~/Libs/Images/down-arrow.png") %>' class="imgLink imgDown" title="Move Down"/>
            </td>
            <td align="center">
                <input type="hidden" class="hdnSubLedgerID" />
                <input type="hidden" class="hdnSearchDialogTypeName" />
                <input type="hidden" class="hdnIDFieldName" />
                <input type="hidden" class="hdnCodeFieldName" />
                <input type="hidden" class="hdnDisplayFieldName" />
                <input type="hidden" class="hdnMethodName" />
                <input type="hidden" class="hdnFilterExpression" />
                <div id="COA${idx}" class="tacCOA">
                    <div>
                        <div class="containerAutoComplete">
                            <input type="hidden" class="hdnAutoCompleteValue" value="">
                            <input type="hidden" class="hdnAutoCompleteText">
                            <input type="hidden" class="hdnIsRequired" value="1">
                            <input type="hidden" class="hdnValidationGroup" value="mpTrx">
                            <input type="text" class="required txtAutoComplete" validationgroup="mpTrx" style="width:175px"/>
                            <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                            <input type="button" class="btnCOADetail btnMore" value="..." enabled="false"/>
                            <div class="divListAutoCompleteResultBox">
                                <div class="divListAutoCompleteResult">
                                </div>
                            </div>
                        </div>
                        <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                            <div>
                                ${GLAccountName} (<b>${GLAccountNo}</b>)
                                <input type='hidden' value='${GLAccountName}' class='hdnAutoCompleteRowText'/>
                                <input type='hidden' value='${GLAccountID}' class='hdnAutoCompleteRowValue'/>
                            </div>
                        </script1>
                    </div>
                </div>
            </td>
            <td align="center">
                <div id="SubCOA${idx}" class="tacSubCOA">
                    <div>
                        <div class="containerAutoComplete">
                            <input type="hidden" class="hdnAutoCompleteValue">
                            <input type="hidden" class="hdnAutoCompleteCode">
                            <input type="hidden" class="hdnAutoCompleteText">
                            <input type="hidden" class="hdnIsRequired" value="1">
                            <input type="hidden" class="hdnValidationGroup" value="mpTrx">
                            <input type="text" readonly="readonly" class="required txtAutoComplete" validationgroup="mpTrx" style="width:175px"/>
                            <input type="button" enabled="false" class="btnAutoCompleteSearchMore btnSearch"/>
                            <input type="button" class="btnSubCOADetail btnMore" value="..." enabled="false"/>
                            <div class="divListAutoCompleteResultBox">
                                <div class="divListAutoCompleteResult">
                                </div>
                            </div>
                        </div>
                        <div class="divSubLedgerTemplate">
                            <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                <div>
                                    ${GLAccountName} (<b>${GLAccountNo}</b>)
                                    <input type='hidden' value='${GLAccountName}' class='hdnAutoCompleteRowText'/>
                                    <input type='hidden' value='${GLAccountID}' class='hdnAutoCompleteRowValue'/>
                                </div>
                            </script1>
                        </div>
                    </div>
                </div>
            </td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtRemarks" value="" style="width:99%" /></td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtCurrency txtDebit" value="0" style="width:99%" /></td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtCurrency txtKredit" value="0" style="width:99%" /></td>
            <td align="center">
                <input type="text" validationgroup="mpTrx" class="txtDocumentNo" value="" style="width:125px" />
                <input type="button" class="btnSearchDocument btnSearch"/>
                <input type="button" class="btnDocumentDetail btnMore" value="..." enabled="false"/>
            </td>
        </tr>
    </script>

    <input type="hidden" id="hdnGCTransactionStatus" runat="server" value="" />
    <input type="hidden" id="hdnID" runat="server" value="0" />
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
                        <td colspan="4">
                            <dxe:ASPxComboBox ID="cboTransactionCode" ClientInstanceName="cboTransactionCode" Width="100%" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboTransactionCodeValueChanged(s); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblJournalNo"><%=GetLabel("Nomor Jurnal") %></label></td>
                        <td id="tdTransactionNoAdd" runat="server">
                            <table  cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 50px" />
                                    <col style="width: 3px" />
                                    <col style="width: 170px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtJournalPrefix" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtJournalNo1" Width="100%" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="display:none;" id="tdTransactionNoEdit" runat="server"><asp:TextBox runat="server" ID="txtJournalNo" Width="220px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtJournalDate" CssClass="datepicker" Width="120px" /></td>
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
                    <span id="divTemplatePick" class="divAdd"><%=GetLabel("Template")%></span>
                    <span id="divSaveTemplate" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Save As Template")%></span>
                    <table id="tblJournalEntry" style="display:none" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                        <tr id="trHeader2">
                            <th style="width:30px;"></th>
                            <th style="width:20px;"></th>
                            <th style="width:250px"><%=GetLabel("Perkiraan")%></th> 
                            <th style="width:250px"><%=GetLabel("Sub Perkiraan")%></th> 
                            <th><%=GetLabel("Keterangan")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("DEBET")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("KREDIT")%></th> 
                            <th style="width:200px"><%=GetLabel("No. Dokumen")%></th> 
                        </tr>
                        <tr id="trFooter">
                            <td colspan="5" align="right"><%=GetLabel("Total") %> : </td>
                            <td align="center"><input id="txtTotalDebit" type="text" validationgroup="mpTrx" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td align="center"><input id="txtTotalKredit" type="text" validationgroup="mpTrx" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table id="tblJournalView" style="display:none" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                        <tr>
                            <th style="width:250px"><%=GetLabel("Perkiraan")%></th> 
                            <th style="width:250px"><%=GetLabel("Sub Perkiraan")%></th> 
                            <th><%=GetLabel("Keterangan")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("DEBET")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("KREDIT")%></th> 
                            <th style="width:150px"><%=GetLabel("No. Dokumen")%></th> 
                        </tr>
                        <asp:Repeater ID="rptJournalViewDt" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("GLAccountName") %></td>
                                    <td><%#Eval("SubLedgerName")%></td>
                                    <td><%#Eval("Remarks") %></td>
                                    <td align="right"><%#Eval("DebitAmount", "{0:N2}") %></td>
                                    <td align="right"><%#Eval("CreditAmount", "{0:N2}")%></td>
                                    <td><%#Eval("ReferenceNo") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr id="tr2">
                            <td colspan="3" align="right"><%=GetLabel("Total") %> : </td>
                            <td align="center"><input id="txtTotalDebitView" runat="server" type="text" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td align="center"><input id="txtTotalKreditView" runat="server" type="text" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
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
            </td>
        </tr>
    </table>
</asp:Content>