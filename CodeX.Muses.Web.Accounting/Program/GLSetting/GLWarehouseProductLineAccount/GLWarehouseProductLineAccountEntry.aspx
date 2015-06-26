<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="GLWarehouseProductLineAccountEntry.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.GLWarehouseProductLineAccountEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {

            function onGetGLAccountFilterExpression() {
                var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
                return filterExpression;
            }
            
            //#region Inventory
            $('#lblInventory.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtInventoryGLAccountNo.ClientID %>').val(value);
                    onTxtInventoryGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtInventoryGLAccountNo.ClientID %>').change(function () {
                onTxtInventoryGLAccountCodeChanged($(this).val());
            });

            function onTxtInventoryGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnInventoryID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtInventoryGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnInventorySubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnInventorySearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnInventoryFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnInventoryIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnInventoryCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnInventoryDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnInventoryMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnInventoryID.ClientID %>').val('');
                        $('#<%=txtInventoryGLAccountNo.ClientID %>').val('');
                        $('#<%=txtInventoryGLAccountName.ClientID %>').val('');

                        $('#<%=hdnInventorySubLedgerID.ClientID %>').val('');
                        $('#<%=hdnInventorySearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnInventoryFilterExpression.ClientID %>').val('');
                        $('#<%=hdnInventoryIDFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryMethodName.ClientID %>').val('');
                    }
                    onInventorySubLedgerIDChanged();
                    $('#<%=hdnInventorySubLedger.ClientID %>').val('');
                    $('#<%=txtInventorySubLedgerCode.ClientID %>').val('');
                    $('#<%=txtInventorySubLedgerName.ClientID %>').val('');
                });
            }

            function onInventorySubLedgerIDChanged() {
                if ($('#<%=hdnInventorySubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnInventorySubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblInventorySubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtInventorySubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblInventorySubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtInventorySubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region Inventory Sub Ledger
            function onGetInventorySubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnInventoryFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnInventorySubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblInventorySubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnInventorySearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnInventorySearchDialogTypeName.ClientID %>').val(), onGetInventorySubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtInventorySubLedgerCode.ClientID %>').val(value);
                        onTxtInventorySubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtInventorySubLedgerCode.ClientID %>').change(function () {
                onTxtInventorySubLedgerDtCodeChanged($(this).val());
            });

            function onTxtInventorySubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnInventorySearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetInventorySubLedgerDtFilterExpression() + " AND " + $('#<%=hdnInventoryCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnInventoryMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnInventorySubLedger.ClientID %>').val(result[$('#<%=hdnInventoryIDFieldName.ClientID %>').val()]);
                            $('#<%=txtInventorySubLedgerName.ClientID %>').val(result[$('#<%=hdnInventoryDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnInventorySubLedger.ClientID %>').val('');
                            $('#<%=txtInventorySubLedgerCode.ClientID %>').val('');
                            $('#<%=txtInventorySubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region COGS
            $('#lblCOGS.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtCOGSGLAccountNo.ClientID %>').val(value);
                    onTxtCOGSGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtCOGSGLAccountNo.ClientID %>').change(function () {
                onTxtCOGSGLAccountCodeChanged($(this).val());
            });

            function onTxtCOGSGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnCOGSID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtCOGSGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnCOGSSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnCOGSSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnCOGSFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnCOGSIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnCOGSCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnCOGSDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnCOGSMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnCOGSID.ClientID %>').val('');
                        $('#<%=txtCOGSGLAccountNo.ClientID %>').val('');
                        $('#<%=txtCOGSGLAccountName.ClientID %>').val('');

                        $('#<%=hdnCOGSSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnCOGSSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnCOGSFilterExpression.ClientID %>').val('');
                        $('#<%=hdnCOGSIDFieldName.ClientID %>').val('');
                        $('#<%=hdnCOGSCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnCOGSDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnCOGSMethodName.ClientID %>').val('');
                    }
                    onCOGSSubLedgerIDChanged();
                    $('#<%=hdnCOGSSubLedger.ClientID %>').val('');
                    $('#<%=txtCOGSSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtCOGSSubLedgerName.ClientID %>').val('');
                });
            }

            function onCOGSSubLedgerIDChanged() {
                if ($('#<%=hdnCOGSSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnCOGSSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblCOGSSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtCOGSSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblCOGSSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtCOGSSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region COGS Sub Ledger
            function onGetCOGSSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnCOGSFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnCOGSSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblCOGSSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnCOGSSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnCOGSSearchDialogTypeName.ClientID %>').val(), onGetCOGSSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtCOGSSubLedgerCode.ClientID %>').val(value);
                        onTxtCOGSSubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtCOGSSubLedgerCode.ClientID %>').change(function () {
                onTxtCOGSSubLedgerDtCodeChanged($(this).val());
            });

            function onTxtCOGSSubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnCOGSSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetCOGSSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnCOGSCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnCOGSMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnCOGSSubLedger.ClientID %>').val(result[$('#<%=hdnCOGSIDFieldName.ClientID %>').val()]);
                            $('#<%=txtCOGSSubLedgerName.ClientID %>').val(result[$('#<%=hdnCOGSDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnCOGSSubLedger.ClientID %>').val('');
                            $('#<%=txtCOGSSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtCOGSSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region Consumption
            $('#lblConsumption.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtConsumptionGLAccountNo.ClientID %>').val(value);
                    onTxtConsumptionGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtConsumptionGLAccountNo.ClientID %>').change(function () {
                onTxtConsumptionGLAccountCodeChanged($(this).val());
            });

            function onTxtConsumptionGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnConsumption.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtConsumptionGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnConsumptionSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnConsumptionSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnConsumptionFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnConsumptionIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnConsumptionCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnConsumptionDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnConsumptionMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnConsumption.ClientID %>').val('');
                        $('#<%=txtConsumptionGLAccountNo.ClientID %>').val('');
                        $('#<%=txtConsumptionGLAccountName.ClientID %>').val('');

                        $('#<%=hdnConsumptionSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnConsumptionSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnConsumptionFilterExpression.ClientID %>').val('');
                        $('#<%=hdnConsumptionIDFieldName.ClientID %>').val('');
                        $('#<%=hdnConsumptionCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnConsumptionDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnConsumptionMethodName.ClientID %>').val('');
                    }
                    onConsumptionSubLedgerIDChanged();
                    $('#<%=hdnConsumptionSubLedger.ClientID %>').val('');
                    $('#<%=txtConsumptionSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtConsumptionSubLedgerName.ClientID %>').val('');
                });
            }

            function onConsumptionSubLedgerIDChanged() {
                if ($('#<%=hdnConsumptionSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnConsumptionSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblConsumptionSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtConsumptionSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblConsumptionSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtConsumptionSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region Consumption Sub Ledger
            function onGetConsumptionSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnConsumptionFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnConsumptionSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblConsumptionSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnConsumptionSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnConsumptionSearchDialogTypeName.ClientID %>').val(), onGetConsumptionSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtConsumptionSubLedgerCode.ClientID %>').val(value);
                        onTxtConsumptionSubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtConsumptionSubLedgerCode.ClientID %>').change(function () {
                onTxtConsumptionSubLedgerDtCodeChanged($(this).val());
            });

            function onTxtConsumptionSubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnConsumptionSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetConsumptionSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnConsumptionCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnConsumptionMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnConsumptionSubLedger.ClientID %>').val(result[$('#<%=hdnConsumptionIDFieldName.ClientID %>').val()]);
                            $('#<%=txtConsumptionSubLedgerName.ClientID %>').val(result[$('#<%=hdnConsumptionDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnConsumptionSubLedger.ClientID %>').val('');
                            $('#<%=txtConsumptionSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtConsumptionSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region AdjustmentIN
            $('#lblAdjustmentIN.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtAdjustmentINGLAccountNo.ClientID %>').val(value);
                    onTxtAdjustmentINGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtAdjustmentINGLAccountNo.ClientID %>').change(function () {
                onTxtAdjustmentINGLAccountCodeChanged($(this).val());
            });

            function onTxtAdjustmentINGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnAdjustmentIN.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtAdjustmentINGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnAdjustmentINSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnAdjustmentINSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnAdjustmentINFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnAdjustmentINIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnAdjustmentINCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnAdjustmentINDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnAdjustmentINMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnAdjustmentIN.ClientID %>').val('');
                        $('#<%=txtAdjustmentINGLAccountNo.ClientID %>').val('');
                        $('#<%=txtAdjustmentINGLAccountName.ClientID %>').val('');

                        $('#<%=hdnAdjustmentINSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINFilterExpression.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINIDFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentINMethodName.ClientID %>').val('');
                    }
                    onAdjustmentINSubLedgerIDChanged();
                    $('#<%=hdnAdjustmentINSubLedger.ClientID %>').val('');
                    $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtAdjustmentINSubLedgerName.ClientID %>').val('');
                });
            }

            function onAdjustmentINSubLedgerIDChanged() {
                if ($('#<%=hdnAdjustmentINSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnAdjustmentINSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblAdjustmentINSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblAdjustmentINSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region AdjustmentIN Sub Ledger
            function onGetAdjustmentINSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnAdjustmentINFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnAdjustmentINSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblAdjustmentINSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnAdjustmentINSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnAdjustmentINSearchDialogTypeName.ClientID %>').val(), onGetAdjustmentINSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').val(value);
                        onTxtAdjustmentINSubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').change(function () {
                onTxtAdjustmentINSubLedgerDtCodeChanged($(this).val());
            });

            function onTxtAdjustmentINSubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnAdjustmentINSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetAdjustmentINSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnAdjustmentINCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnAdjustmentINMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnAdjustmentINSubLedger.ClientID %>').val(result[$('#<%=hdnAdjustmentINIDFieldName.ClientID %>').val()]);
                            $('#<%=txtAdjustmentINSubLedgerName.ClientID %>').val(result[$('#<%=hdnAdjustmentINDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnAdjustmentINSubLedger.ClientID %>').val('');
                            $('#<%=txtAdjustmentINSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtAdjustmentINSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region AdjustmentOUT
            $('#lblAdjustmentOUT.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtAdjustmentOUTGLAccountNo.ClientID %>').val(value);
                    onTxtAdjustmentOUTGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtAdjustmentOUTGLAccountNo.ClientID %>').change(function () {
                onTxtAdjustmentOUTGLAccountCodeChanged($(this).val());
            });

            function onTxtAdjustmentOUTGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnAdjustmentOUT.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtAdjustmentOUTGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnAdjustmentOUTSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnAdjustmentOUTSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnAdjustmentOUTFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnAdjustmentOUTIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnAdjustmentOUTCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnAdjustmentOUTDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnAdjustmentOUTMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnAdjustmentOUT.ClientID %>').val('');
                        $('#<%=txtAdjustmentOUTGLAccountNo.ClientID %>').val('');
                        $('#<%=txtAdjustmentOUTGLAccountName.ClientID %>').val('');

                        $('#<%=hdnAdjustmentOUTSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTFilterExpression.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTIDFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnAdjustmentOUTMethodName.ClientID %>').val('');
                    }
                    onAdjustmentOUTSubLedgerIDChanged();
                    $('#<%=hdnAdjustmentOUTSubLedger.ClientID %>').val('');
                    $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtAdjustmentOUTSubLedgerName.ClientID %>').val('');
                });
            }

            function onAdjustmentOUTSubLedgerIDChanged() {
                if ($('#<%=hdnAdjustmentOUTSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnAdjustmentOUTSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblAdjustmentOUTSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblAdjustmentOUTSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region AdjustmentOUT Sub Ledger
            function onGetAdjustmentOUTSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnAdjustmentOUTFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnAdjustmentOUTSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblAdjustmentOUTSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnAdjustmentOUTSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnAdjustmentOUTSearchDialogTypeName.ClientID %>').val(), onGetAdjustmentOUTSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').val(value);
                        onTxtAdjustmentOUTSubLedgerDtCodeChanged(value);
                    });
                }
            });

            $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').change(function () {
                onTxtAdjustmentOUTSubLedgerDtCodeChanged($(this).val());
            });

            function onTxtAdjustmentOUTSubLedgerDtCodeChanged(value) {
                if ($('#<%=hdnAdjustmentOUTSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetAdjustmentOUTSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnAdjustmentOUTCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnAdjustmentOUTMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnAdjustmentOUTSubLedger.ClientID %>').val(result[$('#<%=hdnAdjustmentOUTIDFieldName.ClientID %>').val()]);
                            $('#<%=txtAdjustmentOUTSubLedgerName.ClientID %>').val(result[$('#<%=hdnAdjustmentOUTDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnAdjustmentOUTSubLedger.ClientID %>').val('');
                            $('#<%=txtAdjustmentOUTSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtAdjustmentOUTSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region InventoryVAT
            $('#lblInventoryVAT.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtInventoryVATGLAccountNo.ClientID %>').val(value);
                    onTxtInventoryVATGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtInventoryVATGLAccountNo.ClientID %>').change(function () {
                onTxtInventoryVATGLAccountCodeChanged($(this).val());
            });

            function onTxtInventoryVATGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnInventoryVATID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtInventoryVATGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnInventoryVATSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnInventoryVATSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnInventoryVATFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnInventoryVATIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnInventoryVATCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnInventoryVATDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnInventoryVATMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnInventoryVATID.ClientID %>').val('');
                        $('#<%=txtInventoryVATGLAccountNo.ClientID %>').val('');
                        $('#<%=txtInventoryVATGLAccountName.ClientID %>').val('');

                        $('#<%=hdnInventoryVATSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnInventoryVATSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnInventoryVATFilterExpression.ClientID %>').val('');
                        $('#<%=hdnInventoryVATIDFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryVATCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryVATDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryVATMethodName.ClientID %>').val('');
                    }
                    onInventoryVATSubLedgerIDChanged();
                    $('#<%=hdnInventoryVATSubLedger.ClientID %>').val('');
                    $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtInventoryVATSubLedgerName.ClientID %>').val('');
                });
            }

            function onInventoryVATSubLedgerIDChanged() {
                if ($('#<%=hdnInventoryVATSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnInventoryVATSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblInventoryVATSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblInventoryVATSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region InventoryVAT Sub Ledger
            function onGetInventoryVATSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnInventoryVATFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnInventoryVATSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblInventoryVATSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnInventoryVATSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnInventoryVATSearchDialogTypeName.ClientID %>').val(), onGetInventoryVATSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').val(value);
                        onTxtSubLedgerDt4CodeChanged(value);
                    });
                }
            });

            $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').change(function () {
                onTxtSubLedgerDt4CodeChanged($(this).val());
            });

            function onTxtSubLedgerDt4CodeChanged(value) {
                if ($('#<%=hdnInventoryVATSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetInventoryVATSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnInventoryVATCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnInventoryVATMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnInventoryVATSubLedger.ClientID %>').val(result[$('#<%=hdnInventoryVATIDFieldName.ClientID %>').val()]);
                            $('#<%=txtInventoryVATSubLedgerName.ClientID %>').val(result[$('#<%=hdnInventoryVATDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnInventoryVATSubLedger.ClientID %>').val('');
                            $('#<%=txtInventoryVATSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtInventoryVATSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region InventoryDiscount
            $('#lblInventoryDiscount.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtInventoryDiscountGLAccountNo.ClientID %>').val(value);
                    onTxtInventoryDiscountGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtInventoryDiscountGLAccountNo.ClientID %>').change(function () {
                onTxtInventoryDiscountGLAccountCodeChanged($(this).val());
            });

            function onTxtInventoryDiscountGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnInventoryDiscountID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtInventoryDiscountGLAccountName.ClientID %>').val(result.GLAccountName);

                        $('#<%=hdnInventoryDiscountSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnInventoryDiscountSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnInventoryDiscountFilterExpression.ClientID %>').val(result.FilterExpression);
                        $('#<%=hdnInventoryDiscountIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnInventoryDiscountCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnInventoryDiscountDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnInventoryDiscountMethodName.ClientID %>').val(result.MethodName);
                    }
                    else {
                        $('#<%=hdnInventoryDiscountID.ClientID %>').val('');
                        $('#<%=txtInventoryDiscountGLAccountNo.ClientID %>').val('');
                        $('#<%=txtInventoryDiscountGLAccountName.ClientID %>').val('');

                        $('#<%=hdnInventoryDiscountSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountFilterExpression.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountIDFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnInventoryDiscountMethodName.ClientID %>').val('');
                    }
                    onInventoryDiscountSubLedgerIDChanged();
                    $('#<%=hdnInventoryDiscountSubLedger.ClientID %>').val('');
                    $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtInventoryDiscountSubLedgerName.ClientID %>').val('');
                });
            }

            function onInventoryDiscountSubLedgerIDChanged() {
                if ($('#<%=hdnInventoryDiscountSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnInventoryDiscountSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblInventoryDiscountSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblInventoryDiscountSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region InventoryDiscount Sub Ledger
            function onGetInventoryDiscountSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnInventoryDiscountFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnInventoryDiscountSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblInventoryDiscountSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnInventoryDiscountSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnInventoryDiscountSearchDialogTypeName.ClientID %>').val(), onGetInventoryDiscountSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').val(value);
                        onTxtSubLedgerDt5CodeChanged(value);
                    });
                }
            });

            $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').change(function () {
                onTxtSubLedgerDt5CodeChanged($(this).val());
            });

            function onTxtSubLedgerDt5CodeChanged(value) {
                if ($('#<%=hdnInventoryDiscountSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetInventoryDiscountSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnInventoryDiscountCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnInventoryDiscountMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnInventoryDiscountSubLedger.ClientID %>').val(result[$('#<%=hdnInventoryDiscountIDFieldName.ClientID %>').val()]);
                            $('#<%=txtInventoryDiscountSubLedgerName.ClientID %>').val(result[$('#<%=hdnInventoryDiscountDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnInventoryDiscountSubLedger.ClientID %>').val('');
                            $('#<%=txtInventoryDiscountSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtInventoryDiscountSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region PurchasePriceVariant
            $('#lblPurchasePriceVariant.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtPurchasePriceVariantGLAccountNo.ClientID %>').val(value);
                    onTxtPurchasePriceVariantGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtPurchasePriceVariantGLAccountNo.ClientID %>').change(function () {
                onTxtPurchasePriceVariantGLAccountCodeChanged($(this).val());
            });

            function onTxtPurchasePriceVariantGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnPurchasePriceVariantID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtPurchasePriceVariantGLAccountName.ClientID %>').val(result.GLAccountName);
                        $('#<%=hdnPurchasePriceVariantSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnPurchasePriceVariantSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnPurchasePriceVariantIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnPurchasePriceVariantCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnPurchasePriceVariantDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnPurchasePriceVariantMethodName.ClientID %>').val(result.MethodName);
                        $('#<%=hdnPurchasePriceVariantFilterExpression.ClientID %>').val(result.FilterExpression);
                    }
                    else {
                        $('#<%=hdnPurchasePriceVariantID.ClientID %>').val('');
                        $('#<%=txtPurchasePriceVariantGLAccountName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantIDFieldName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantMethodName.ClientID %>').val('');
                        $('#<%=hdnPurchasePriceVariantFilterExpression.ClientID %>').val('');
                    }
                    onPurchasePriceVariantSubLedgerIDChanged();
                    $('#<%=hdnPurchasePriceVariantSubLedger.ClientID %>').val('');
                    $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtPurchasePriceVariantSubLedgerName.ClientID %>').val('');
                });
            }

            function onPurchasePriceVariantSubLedgerIDChanged() {
                if ($('#<%=hdnPurchasePriceVariantSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnPurchasePriceVariantSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblPurchasePriceVariantSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblPurchasePriceVariantSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region PurchasePriceVariant Sub Ledger
            function onGetPurchasePriceVariantSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnPurchasePriceVariantFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnPurchasePriceVariantSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblPurchasePriceVariantSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnPurchasePriceVariantSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnPurchasePriceVariantSearchDialogTypeName.ClientID %>').val(), onGetPurchasePriceVariantSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').val(value);
                        onTxtPurchasePriceVariantSubLedgerCodeChanged(value);
                    });
                }
            });

            $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').change(function () {
                onTxtPurchasePriceVariantSubLedgerCodeChanged($(this).val());
            });

            function onTxtPurchasePriceVariantSubLedgerCodeChanged(value) {
                if ($('#<%=hdnPurchasePriceVariantSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetPurchasePriceVariantSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnPurchasePriceVariantCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnPurchasePriceVariantMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnPurchasePriceVariantSubLedger.ClientID %>').val(result[$('#<%=hdnPurchasePriceVariantIDFieldName.ClientID %>').val()]);
                            $('#<%=txtPurchasePriceVariantSubLedgerName.ClientID %>').val(result[$('#<%=hdnPurchasePriceVariantDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnPurchasePriceVariantSubLedger.ClientID %>').val('');
                            $('#<%=txtPurchasePriceVariantSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtPurchasePriceVariantSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            //#region Product Line
            function onGetProductLineFilterExpression() {
                var filterExpression = "IsDeleted = 0";
                return filterExpression;
            }

            $('#lblProductLine.lblLink').click(function () {
                openSearchDialog('productline', onGetProductLineFilterExpression(), function (value) {
                    $('#<%=txtProductLineCode.ClientID %>').val(value);
                    onTxtProductLineCodeChanged(value);
                });
            });

            $('#<%=txtProductLineCode.ClientID %>').change(function () {
                onTxtProductLineCodeChanged($(this).val());
            });

            function onTxtProductLineCodeChanged(value) {
                var filterExpression = onGetProductLineFilterExpression() + " AND ProductLineCode = '" + value + "'";
                Methods.getObject('GetProductLineList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnProductLineID.ClientID %>').val(result.ProductLineID);
                        $('#<%=txtProductLineName.ClientID %>').val(result.ProductLineName);
                    }
                    else {
                        $('#<%=hdnProductLineID.ClientID %>').val('');
                        $('#<%=txtProductLineCode.ClientID %>').val('');
                        $('#<%=txtProductLineName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            onInventorySubLedgerIDChanged();
            onCOGSSubLedgerIDChanged();
            onConsumptionSubLedgerIDChanged();
            onAdjustmentINSubLedgerIDChanged();
            onAdjustmentOUTSubLedgerIDChanged();
            onInventoryVATSubLedgerIDChanged();
            onInventoryDiscountSubLedgerIDChanged();
            onPurchasePriceVariantSubLedgerIDChanged();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSite" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:240px"/>
                    </colgroup>
                    <tr runat="server" id="trSiteServiceUnit">
                        <td class="tdLabel" valign="top" style="padding-top:5px"><label class="lblMandatory lblLink" id="lblProductLine"><%=GetLabel("Product Line")%></label></td>
                        <td>
                            <input type="hidden" id="hdnProductLineID" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtProductLineCode" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtProductLineName" Width="100%" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Item")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboGCItemType" runat="server" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top" style="padding-top:5px"><label><%=GetLabel("Notes")%></label></td>
                        <td><asp:TextBox ID="txtNotes" Width="300px" runat="server" TextMode="MultiLine" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <colgroup>
                        <col style="width: 50%"/>
                    </colgroup>
                    <tr>
                        <td>
                            <table width="100%">
                                <colgroup>
                                    <col width="240px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblInventory"><%=GetLabel("COA Persediaan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventoryID" runat="server" />
                                        <input type="hidden" id="hdnInventorySubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnInventorySearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnInventoryIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryMethodName" runat="server" />
                                        <input type="hidden" id="hdnInventoryFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventoryGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventoryGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblInventoryVAT"><%=GetLabel("COA Persediaan (PPN)")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventoryVATID" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATMethodName" runat="server" />
                                        <input type="hidden" id="hdnInventoryVATFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventoryVATGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventoryVATGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblInventoryDiscount"><%=GetLabel("COA Persediaan (Diskon)")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventoryDiscountID" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountMethodName" runat="server" />
                                        <input type="hidden" id="hdnInventoryDiscountFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventoryDiscountGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventoryDiscountGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblCOGS"><%=GetLabel("COA HPP")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnCOGSID" runat="server" />
                                        <input type="hidden" id="hdnCOGSSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnCOGSSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnCOGSIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnCOGSCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnCOGSDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnCOGSMethodName" runat="server" />
                                        <input type="hidden" id="hdnCOGSFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtCOGSGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtCOGSGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblConsumption"><%=GetLabel("COA Pemakaian")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnConsumption" runat="server" />
                                        <input type="hidden" id="hdnConsumptionSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnConsumptionSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnConsumptionIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnConsumptionCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnConsumptionDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnConsumptionMethodName" runat="server" />
                                        <input type="hidden" id="hdnConsumptionFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtConsumptionGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtConsumptionGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblAdjustmentIN"><%=GetLabel("COA Selisih Persediaan (IN)")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnAdjustmentIN" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINMethodName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentINFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentINGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentINGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblAdjustmentOUT"><%=GetLabel("COA Selisih Persediaan (OUT)")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnAdjustmentOUT" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTMethodName" runat="server" />
                                        <input type="hidden" id="hdnAdjustmentOUTFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentOUTGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentOUTGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink lblMandatory" id="lblPurchasePriceVariant"><%=GetLabel("COA Perubahan Harga")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnPurchasePriceVariantID" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantSubLedgerID" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantSearchDialogTypeName" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantIDFieldName" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantCodeFieldName" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantDisplayFieldName" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantMethodName" runat="server" />
                                        <input type="hidden" id="hdnPurchasePriceVariantFilterExpression" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtPurchasePriceVariantGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtPurchasePriceVariantGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblInventorySubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventorySubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventorySubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventorySubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblInventoryVATSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventoryVATSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventoryVATSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventoryVATSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblInventoryDiscountSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInventoryDiscountSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtInventoryDiscountSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtInventoryDiscountSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblCOGSSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnCOGSSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtCOGSSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtCOGSSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblConsumptionSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnConsumptionSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtConsumptionSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtConsumptionSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblAdjustmentINSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnAdjustmentINSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentINSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentINSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblAdjustmentOUTSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnAdjustmentOUTSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentOUTSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtAdjustmentOUTSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblPurchasePriceVariantSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnPurchasePriceVariantSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtPurchasePriceVariantSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtPurchasePriceVariantSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>