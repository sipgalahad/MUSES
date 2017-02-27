<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="TreasuryEntry.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.TreasuryEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

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

            setDatePicker('<%=txtReferenceDate.ClientID %>');
            setDatePicker('<%=txtTransactionDate.ClientID %>');
            if (getIsAdd()) {
                $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'maxDate', '0');
                var minDate = parseInt('<%=minDate %>');
                if (minDate > -1)
                    $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'minDate', '-' + minDate);
            }

            $('#divQuickPicks').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Treasury/TreasuryQuickPicksCtl.ascx');
                    var id = '';
                    openUserControlPopup(url, id, 'Quick Picks', 1000, 600);
                }
            });

            //#region Journal No
            $('#lblTransactionNo.lblLink').click(function () {
                openSearchDialog('treasuryhd', '', function (value) {
                    $('#<%=txtTransactionNo.ClientID %>').val(value);
                    onTxtTransactionNoChanged(value);
                });
            });

            $('#<%=txtTransactionNo.ClientID %>').change(function () {
                onTxtTransactionNoChanged($(this).val());
            });

            function onTxtTransactionNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Load
            if (!isShowWatermark()) {
                $('#tblJournalEntry').show();
                $('#tblJournalView').hide();
                var TransactionID = $('#<%=hdnID.ClientID %>').val();
                if (TransactionID != '0') {
                    var filterExpression = "TransactionID = " + TransactionID + " AND GCItemDetailStatus != 'X121^999' ORDER BY DisplayOrder ASC";
                    Methods.getListObject('GetvTreasuryDtList', filterExpression, function (result) {
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
                            $newTr.find('.txtTotal').val(entity.TotalAmount).trigger('changeValue');
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
                        calculateTotalAmount();
                        addEntityRow();
                    });
                }
                else {
                    addEntityRow();
                    calculateTotalAmount();
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

        //#region Service Unit
        function onGetServiceUnitFilterExpression() {
            var filterExpression = "<%=GetServiceUnitFilterExpression() %>";
            return filterExpression;
        }

        function onTacServiceUnitButtonSearchClick() {
            openSearchDialog('serviceunitpersite', onGetServiceUnitFilterExpression(), function (value) {
                var filterExpression = onGetServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        tacServiceUnit.setValue(result.SiteServiceUnitID);
                        tacServiceUnit.setText(result.ServiceUnitName);
                    }
                    else {
                        tacServiceUnit.setValue('');
                        tacServiceUnit.setText('');
                    }
                });
            });

        }

        function onTacServiceUnitValueChanged() {
        }
        //#endregion

        //#region Book
        function onGetTreasuryBookFilterExpression() {
            var filterExpression = "<%=GetTreasuryBookFilterExpression() %>";
            return filterExpression;
        }

        function onTacBookButtonSearchClick() {
            openSearchDialog('treasurybook', onGetTreasuryBookFilterExpression(), function (value) {
                var filterExpression = onGetTreasuryBookFilterExpression() + " AND BookCode = '" + value + "'";
                Methods.getObject('GetvTreasuryBookList', filterExpression, function (result) {
                    if (result != null) {
                        tacBook.setValue(result.BookID);
                        tacBook.setText(result.BookName);
                    }
                    else {
                        tacBook.setValue('');
                        tacBook.setText('');
                    }
                    entityToControlBook(result);
                });
            });

        }

        function onTacBookValueChanged() {
            var ID = tacBook.getValue();
            if (ID != '') {
                var filterExpression = "BookID = " + ID;
                Methods.getObject('GetvTreasuryBookList', filterExpression, function (result) {
                    entityToControlBook(result);
                });
            }
        }

        function entityToControlBook(entity) {
            if (entity != null) {
                $('#<%=txtGLAccountName.ClientID %>').val(entity.GLAccountName + ' (' + entity.GLAccountNo + ')');
                if (entity.SubLedgerCode != '')
                    $('#<%=txtSubLedgerName.ClientID %>').val(entity.SubLedgerName + ' (' + entity.SubLedgerCode + ')');
                else
                    $('#<%=txtSubLedgerName.ClientID %>').val('');
            }
            else {
                $('#<%=txtGLAccountName.ClientID %>').val('');
                $('#<%=txtSubLedgerName.ClientID %>').val('');
            }
        }
        //#endregion

        function fillTransactionDt(GLAccountID, sLstSubCOAID, sLstSubCOAName, sLstAmount, remarks) {
            var filterExpression = "GLAccountID = " + GLAccountID;
            Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {

                var lstSubCOAID = sLstSubCOAID.split(',');
                var lstSubCOAName = sLstSubCOAName.split(',');
                var lstSubCOAAmount = sLstAmount.split(',');
                $('.trJournalEntry:last').remove();
                for (var i = 0; i < lstSubCOAID.length; ++i) {
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
                    tempHelper.setValue(result.GLAccountID);
                    tempHelper.setText(result.GLAccountName);

                    $newTr.find('.txtTotal').val(lstSubCOAAmount[i]).trigger('changeValue');

                    $newTr.find('.btnDocumentDetail').attr('enabled', false);

                    $newTr.find('.hdnSubLedgerID').val(result.SubLedgerID);
                    $newTr.find('.hdnSearchDialogTypeName').val(result.SearchDialogTypeName);
                    $newTr.find('.hdnFilterExpression').val(result.FilterExpression.replace('@SubLedgerID', result.SubLedgerID));
                    $newTr.find('.hdnIDFieldName').val(result.IDFieldName);
                    $newTr.find('.hdnCodeFieldName').val(result.CodeFieldName);
                    $newTr.find('.hdnDisplayFieldName').val(result.DisplayFieldName);
                    $newTr.find('.hdnMethodName').val(result.MethodName);
                    $newTr.find('.txtRemarks').val(remarks);

                    var template = "<script class='tmpltAutoComplete' type='text/x-jquery-tmpl'><div>";
                    template += "${" + result.DisplayFieldName + "} (<b>${" + result.CodeFieldName + "}</b>";
                    template += "<input type='hidden' value='${" + result.DisplayFieldName + "}' class='hdnAutoCompleteRowText'/>";
                    template += "<input type='hidden' value='${" + result.IDFieldName + "}' class='hdnAutoCompleteRowValue'/>";
                    template += "<\/div><\/script>";

                    $newTr.find('.divSubLedgerTemplate').html(template);

                    var tempHelper = new CodeXClientAutoCompleteHelper();
                    tempHelper.init("SubCOA" + idx, result.CodeFieldName + "," + result.DisplayFieldName, result.MethodName, result.FilterExpression, "", result.CodeFieldName);
                    tempHelper.setClientSideEvents(onSubLedgerIDValueChanged);
                    tempHelper.initializeControl();
                    tempHelper.setValue(lstSubCOAID[i]);
                    tempHelper.setText(lstSubCOAName[i]);

                    $newTr.find('.tacSubCOA').find('.txtAutoComplete').removeAttr('readonly');
                    $newTr.find('.tacSubCOA').find('.btnAutoCompleteSearchMore').removeAttr('enabled');

                    idx++;
                }
                calculateTotalAmount();
                addEntityRow();
            });
        }

        var popupType = '';

        $tacTr = null;
        //#region COA
        window.onGetCOAFilterExpression = function() {
            if (tacBook.getValue() == '')
                return "1 = 0";
            var filterExpression = "GLAccountID IN (SELECT GLAccount FROM TreasuryBookCOA WHERE BookID = " + tacBook.getValue() + ") AND IsHeader = 0 AND IsDeleted = 0";
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

        //#region calculateTotalAmount
        function calculateTotalAmount() {
            var total = 0;
            $('#tblJournalEntry .txtTotal').each(function () {
                total += parseFloat($(this).attr('hiddenVal'));
            });
            $('#txtTotalAmount').val(total).trigger('changeValue');
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

                        calculateTotalAmount();
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

        //#region Total
        $('.txtTotal').live('blur', function () {
            $(this).trigger('changeValue');
            calculateTotalAmount();
        });
        //#endregion

        //#region Btn Detail Saldo Information 
        $('.btnCOADetail').live('click', function () {
            if ($(this).attr('enabled') == null) {
                $tr = $(this).closest('tr');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var url = ResolveUrl('~/Program/Journal/GLBalanceInformationCtl.ascx');
                var id = glAccountID;
                var date = $('#<%=txtTransactionDate.ClientID %>').val().split('-');
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
                var date = $('#<%=txtTransactionDate.ClientID %>').val().split('-');
                var period = date[2] + '|' + date[1];
                var param = id + '|' + period;
                openUserControlPopup(url, param, 'Detail', 900, 600);


                $tacSubCOA = $tr.find('.tacSubCOA');
                var glAccountID = $tr.find('.tacCOA').find('.hdnAutoCompleteValue').val();
                var subLedgerDtID = $tacSubCOA.find('.hdnAutoCompleteValue').val();
                var url = ResolveUrl('~/Program/Journal/GLSubLedgerInformationCtl.ascx');

                var code = $tacSubCOA.find('.hdnAutoCompleteCode').val();
                var name = $tacSubCOA.find('.txtAutoComplete').val();

                var date = $('#<%=txtTransactionDate.ClientID %>').val().split('-');
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

        function onAfterSaveAddRecordEntryPopup(param) {
        }

        function onBeforeSaveRecord() {
            var objList = new Array();
            var lstTransactionDtID = '';
            $('.trJournalEntry').each(function () {
                var glAccountID = $(this).find('.tacCOA').find('.hdnAutoCompleteValue').val();
                if (glAccountID != '') {
                    var subLedgerID = $(this).find('.tacSubCOA').find('.hdnAutoCompleteValue').val();
                    var remarks = $(this).find('.txtRemarks').val();
                    var total = $(this).find('.txtTotal').attr('hiddenVal');
                    var documentNo = $(this).find('.txtDocumentNo').val();
                    var transactionDtID = $(this).find('.hdnTransactionDtID').val();

                    if (transactionDtID != '0') {
                        if (lstTransactionDtID != '')
                            lstTransactionDtID += ',';
                        lstTransactionDtID += transactionDtID;
                    }

                    objList.push(new Array(transactionDtID, glAccountID, subLedgerID, remarks, total, documentNo));
                }
            });

            $('#<%=hdnSaveParam.ClientID %>').val(JSON.stringify(objList));
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
                    filterExpression.text = 'TransactionID = ' + transactionID;
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
    <input type="hidden" id="hdnLstBookID" runat="server" />
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
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtCurrency txtTotal" value="0" style="width:99%" /></td>
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
            <col style="width:55%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblTransactionNo"><%=GetLabel("No Voucher")%></label></td>
                        <td ><asp:TextBox runat="server" ID="txtTransactionNo" Width="220px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtTransactionDate" CssClass="datepicker" Width="120px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Buku")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacBook" ClientInstanceName="tacBook" MethodName="GetvTreasuryBookList" GetFilterExpressionFunction="onGetTreasuryBookFilterExpression"
                                SearchFields="BookName,BookCode" TextField="BookName" ValueField="BookID" SearchText="${BookName} (<b>${BookCode}</b>)" OrderByExpression="BookName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacBookButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacBookValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Perkiraan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtGLAccountName" Width="220px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Sub")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtSubLedgerName" Width="220px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Voucher") %></label></td>
                        <td colspan="4"><dxe:ASPxComboBox ID="cboVoucherGroup" ClientInstanceName="cboVoucherGroup" Width="220px" runat="server" /></td>
                    </tr>
                    <tr id="trJournalNo" runat="server" style="display:none">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Jurnal")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtJournalNo" Width="220px" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Referensi") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtReferenceNo" Width="150px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtReferenceDate" CssClass="datepicker" Width="120px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Bagian")%></label></td>
                        <td colspan="4">
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacServiceUnit" ClientInstanceName="tacServiceUnit" MethodName="GetvSiteServiceUnitList" GetFilterExpressionFunction="onGetServiceUnitFilterExpression"
                                SearchFields="ServiceUnitName,ServiceUnitCode" TextField="ServiceUnitName" ValueField="SiteServiceUnitID" SearchText="${ServiceUnitName} (<b>${ServiceUnitCode}</b>)" OrderByExpression="ServiceUnitName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacServiceUnitButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacServiceUnitValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="width: 150px; vertical-align:top; padding-top:5px; "><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td colspan="4"><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divTransactionEntry">
                    <span id="divQuickPicks" class="divAdd"><%=GetLabel("Quick Picks")%></span>
                    <table id="tblJournalEntry" style="display:none" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                        <tr id="trHeader2">
                            <th style="width:30px;"></th>
                            <th style="width:20px;"></th>
                            <th style="width:250px"><%=GetLabel("Perkiraan")%></th> 
                            <th style="width:250px"><%=GetLabel("Sub Perkiraan")%></th> 
                            <th><%=GetLabel("Keterangan")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("Total")%></th> 
                            <th style="width:200px"><%=GetLabel("No. Dokumen")%></th> 
                        </tr>
                        <tr id="trFooter">
                            <td colspan="5" align="right"><%=GetLabel("Total") %> : </td>
                            <td align="center"><input id="txtTotalAmount" type="text" validationgroup="mpTrx" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table id="tblJournalView" style="display:none" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                        <tr>
                            <th style="width:250px"><%=GetLabel("Perkiraan")%></th> 
                            <th style="width:250px"><%=GetLabel("Sub Perkiraan")%></th> 
                            <th><%=GetLabel("Keterangan")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("Total")%></th> 
                            <th style="width:150px"><%=GetLabel("No. Dokumen")%></th> 
                        </tr>
                        <asp:Repeater ID="rptJournalViewDt" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("GLAccountName") %></td>
                                    <td><%#Eval("SubLedgerName")%></td>
                                    <td><%#Eval("Remarks") %></td>
                                    <td align="right"><%#Eval("TotalAmount", "{0:N2}") %></td>
                                    <td><%#Eval("ReferenceNo") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr id="tr2">
                            <td colspan="3" align="right"><%=GetLabel("Total") %> : </td>
                            <td align="center"><input id="txtTotalView" runat="server" type="text" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
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