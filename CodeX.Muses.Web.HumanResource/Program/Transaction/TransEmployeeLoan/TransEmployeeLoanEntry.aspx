<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="TransEmployeeLoanEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.TransEmployeeLoanEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            var tempTransAmount = 0;
            var tempInterest = 0;
            var tempTotalAmount = 0;

            $('#<%=txtTotalAmount.ClientID %>').attr('readonly', 'readonly');

            $('#<%=txtTransactionAmount.ClientID %>').change(function () {
                $(this).blur();
                calculateTotalAmount();
            });

            $('#<%=txtInterestPercentage.ClientID %>').change(function () {
                calculateTotalAmount();
            });

            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divQuickPicks').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divQuickPicks').hide();
            }

            setDatePicker('<%=txtStartPaymentDate.ClientID %>');
            $('#<%=txtStartPaymentDate.ClientID %>').datepicker('option', 'minDate', '0');
            setDatePicker('<%=txtTransactionDate.ClientID %>');
            $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'minDate', '0');



            $('#btnGenerate').click(function () {
                var id = $('#<%=hdnTransactionID.ClientID %>').val();
                if (id != '0') {
                    var url = ResolveUrl("~/Program/Transaction/TransEmployeeLoan/TransEmployeeLoanDtCtl.ascx");
                    openUserControlPopup(url, id, 'Details Formula', 600, 500);
                }
            });

            function calculateTotalAmount() {
                var interestPercentage = parseFloat($('#<%=txtInterestPercentage.ClientID %>').val());
                var transactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
                var total = transactionAmount * (100 + interestPercentage) / 100;
                $('#<%=txtTotalAmount.ClientID %>').val(total).trigger('changeValue');
            }


            //#region Transaction No
            function onGetFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblTransactionNo.lblLink').click(function () {
                openSearchDialog('transemployeeloanhd', onGetFilterExpression(), function (value) {
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
        }

        //#endregion


        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        function onAfterSaveRecordDtSuccess(TransactionID) {
            if ($('#<%=hdnTransactionID.ClientID %>').val() == '0') {
                $('#<%=hdnTransactionID.ClientID %>').val(TransactionID);
                var filterExpression = 'TransactionID = ' + TransactionID;
                Methods.getObject('GetTransRenumerationCompFormulaHdList', filterExpression, function (result) {
                    $('#<%=txtTransactionNo.ClientID %>').val(result.TransactionNo);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail') 
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    onAfterSaveRecordDtSuccess(s.cpTransactionID);
                    $('#divTransactionAdd').click();
                    
                    $('#tblDetails').hide();
                    $('#divEntryDtAdd').hide();
                    $('.trHourDt').each(function (){
                       $tr = $(this).closest('tr');
                       $tr.remove();
                    });
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

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (TransactionID == '' || TransactionID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "TransactionID = " + TransactionID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

        //#region Comp Renumeration
        function onGetRenumerationCompFilterExpression() {
//            var TransactionID = $('#</%=hdnTransactionID.ClientID %>').val();
            var filterExpression = "IsDeleted = 0 ";
            //alert(filterExpression);
            return filterExpression;
        }

        function onTacRenumerationCompIDSearchClick() {
            openSearchDialog('renumerationcomp', onGetRenumerationCompFilterExpression(), function (value) {
                var filterExpression = onGetRenumerationCompFilterExpression() + " AND RenumerationCompCode = '" + value + "'";
                Methods.getObject('GetvRenumerationCompList', filterExpression, function (result) {
                    if (result != null) {
                        tacRenumerationCompID.setValue(result.RenumerationCompID);
                        tacRenumerationCompID.setText(result.RenumerationCompName);
                    }
                    else {
                        tacRenumerationCompID.setValue('');
                        tacRenumerationCompID.setText('');
                    }
                });
            });

        }

        function onTacRenumerationCompIDValueChanged() {
        }
        //#endregion

        //#region Employee
        function onGetEmployeeFilterExpression() {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var filterExpression = "1 = 0";
            if (TransactionID != '') {
                filterExpression = "<%=OnGetEmployeeFilterExpression() %>"
                filterExpression += " AND IsDeleted = 0 AND EmployeeID NOT IN (SELECT EmployeeID FROM TransEmployeePositionDt WHERE TransactionID = " + TransactionID + ")";
            }
            return filterExpression;
        }

        function onTacEmployeeIDSearchClick() {
            openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
                var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
                Methods.getObject('GetEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        tacEmployeeID.setValue(result.EmployeeID);
                        tacEmployeeID.setText(result.FullName);
                    }
                    else {
                        tacEmployeeID.setValue('');
                        tacEmployeeID.setText('');
                    }
                });
            });
        }

        function onTacEmployeeIDValueChanged() {
        }
        //#endregion

        

    </script>  
    
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnTransactionDtID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnDtHourSave" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    

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
                            <td class="tdLabel"><label class="lblLink" id="lblTransactionNo" ><%=GetLabel("No. Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionNo" Width="150px"  runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Dimasukkan")%></td>
                            <td><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory" id="Label1"><%=GetLabel("Karyawan")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacEmployeeID" ClientInstanceName="tacEmployeeID" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                    SearchFields="EmployeeName,EmployeeID" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeIDSearchClick(); }"
                                        ValueChanged="function(){ onTacEmployeeIDValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory" id="lblEmployee"><%=GetLabel("Komp. Renumerasi")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacRenumerationCompID" ClientInstanceName="tacRenumerationCompID" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetRenumerationCompFilterExpression"
                                    SearchFields="RenumerationCompName,RenumerationCompID" TextField="RenumerationCompName" ValueField="RenumerationCompID" SearchText="${RenumerationCompName} (<b>${RenumerationCompCode}</b>)" OrderByExpression="RenumerationCompName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacRenumerationCompIDSearchClick(); }"
                                        ValueChanged="function(){ onTacRenumerationCompIDValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Besar Peminjaman")%></label></td>
                            <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bunga")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><asp:TextBox ID="txtInterestPercentage" CssClass="number" Width="80px" runat="server" /></td>
                                        <td>%</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Total Peminjaman")%></label></td>
                            <td><asp:TextBox ID="txtTotalAmount" CssClass="txtCurrency" Width="120px" runat="server" readonly="true"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Start Pembayaran")%></td>
                            <td><asp:TextBox ID="txtStartPaymentDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Frekuensi Pengembalian")%></label></td>
                            <td><asp:TextBox ID="txtNoOfPayment" CssClass="number" Width="80px" runat="server" /></td>
                        </tr>
                       <tr>
                            <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><input type="button" id="btnGenerate" class="btnWhite" value='<%=GetLabel("Detil Pembayaran") %>'/></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
