<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="UpdateRenumerationEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.UpdateRenumerationEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divQuickPicks').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divQuickPicks').hide();
            }

            setDatePicker('<%=txtStartEffectiveDate.ClientID %>');
            $('#<%=txtStartEffectiveDate.ClientID %>').datepicker('option', 'minDate', '0');
            setDatePicker('<%=txtTransactionDate.ClientID %>');
            $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'minDate', '0');

            //#region Location
//            function getLocationFilterExpression() {
//                var filterExpression = "<%=OnGetFilterExpressionLocation() %>";
//                return filterExpression;
//            }

//            $('#<//%=lblLocation.ClientID %>.lblLink').live('click', function () {
//                openSearchDialog('locationroleuser', getLocationFilterExpression(), function (value) {
//                    $('#<//%=txtLocationCode.ClientID %>').val(value);
//                    onTxtLocationCodeChanged(value);
//                });
//            });

//            $('#<//%=txtLocationCode.ClientID %>').live('change', function () {
//                onTxtLocationCodeChanged($(this).val());
//            });

//            function onTxtLocationCodeChanged(value) {
//                var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
//                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
//                    if (result != null) {
//                        $('#<//%=hdnLocationID.ClientID %>').val(result.LocationID);
//                        $('#<//%=txtLocationName.ClientID %>').val(result.LocationName);
//                        filterExpression = "LocationID = " + result.LocationID;
//                        Methods.getObject('GetLocationList', filterExpression, function (result) {
//                            $('#<//%=hdnLocationItemGroupID.ClientID %>').val(result.ItemGroupID);
//                        });
//                    }
//                    else {
//                        $('#<//%=hdnLocationID.ClientID %>').val('');
//                        $('#<//%=txtLocationCode.ClientID %>').val('');
//                        $('#<//%=txtLocationName.ClientID %>').val('');
//                        $('#<//%=hdnLocationItemGroupID.ClientID %>').val('');
//                    }
//                });
//            }
            //#endregion

            //#region Site Service Unit
//            function getSiteServiceUnitFilterExpression() {
//                var filterExpression = "<//%=OnGetFilterExpressionServiceUnit() %>";
//                return filterExpression;
//            }

//            $('#<//%=lblSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
//                openSearchDialog('serviceunitpersite', getSiteServiceUnitFilterExpression(), function (value) {
//                    $('#<//%=txtServiceUnitCode.ClientID %>').val(value);
//                    onTxtServiceUnitCodeChanged(value);
//                });
//            });

//            $('#<//%=txtServiceUnitCode.ClientID %>').live('change', function () {
//                onTxtServiceUnitCodeChanged($(this).val());
//            });

//            function onTxtServiceUnitCodeChanged(value) {
//                var filterExpression = getSiteServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
//                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
//                    if (result != null) {
//                        $('#<//%=hdnSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
//                        $('#<//%=txtServiceUnitName.ClientID %>').val(result.ServiceUnitName);
//                    }
//                    else {
//                        $('#<//%=hdnSiteServiceUnitID.ClientID %>').val('');
//                        $('#<//%=txtServiceUnitCode.ClientID %>').val('');
//                        $('#<//%=txtServiceUnitName.ClientID %>').val('');
//                    }
//                });
//            }
            //#endregion

            //#region Consumption No
//            function onGetItemConsumptionFilterExpression() {
//                var filterExpression = "<//%=GetFilterExpression() %>";
//                return filterExpression;
//            }

//            $('#lblConsumptionNo.lblLink').click(function () { 
//                openSearchDialog('itemtransactionhd', onGetItemConsumptionFilterExpression(), function (value) {
//                    $('#<//%=txtConsumptionNo.ClientID %>').val(value);
//                    onTxtConsumptionNoChanged(value);
//                });
//            });

//            $('#<//%=txtConsumptionNo.ClientID %>').change(function () {
//                onTxtConsumptionNoChanged($(this).val());
//            });

//            function onTxtConsumptionNoChanged(value) {
//                onLoadObject(value);
//            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    editedLineAmount = 0;
//                    $('#</%=txtQuantity.ClientID %>').val('1');
//                    $('#</%=hdnEntryID.ClientID %>').val('');
//                    $('#</%=hdnItemID.ClientID %>').val('');
//                    $('#</%=hdnGCItemUnit.ClientID %>').val('');
//                    $('#</%=hdnGCBaseUnit.ClientID %>').val('');
//                    $('#</%=txtItemCode.ClientID %>').val('');
//                    $('#</%=txtItemName.ClientID %>').val('');
//                    $('#</%=txtNotesDt.ClientID %>').val('');
//                    $('#</%=txtStockLocation.ClientID %>').val('');
//                    cboItemUnit.SetValue('');
//                    $('#</%=txtConversion.ClientID %>').val('');

                    $('#entryDetailContainer').show();
                }
            });

//            $('#divQuickPicks').click(function () {
//                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
//                    showLoadingPanel();
//                    var url = ResolveUrl('~/Program/Warehouse/StockConsumption/StockConsumptionQuickPicksCtl.ascx');
//                    var transactionID = $('#</%=hdnTransactionID.ClientID %>').val();
//                    var locationID = $('#</%=hdnLocationID.ClientID %>').val();
//                    var locationItemGroupID = $('#</%=hdnLocationItemGroupID.ClientID %>').val();
//                    var id = transactionID + '|' + locationID + '|' + locationItemGroupID;
//                    openUserControlPopup(url, id, 'Quick Picks', 1000, 600);
//                }
//            });

            //#region Item Group
//            $('#lblItemGroup.lblLink').live('click', function () {
//                openSearchDialog('itemgroup', onGetItemGroupFilterExpression(), function (value) {
//                    $('#</%=txtItemGroupCode.ClientID %>').val(value);
//                    onTxtItemGroupCodeChanged(value);
//                });
//            });

//            $('#</%=txtItemGroupCode.ClientID %>').live('change', function () {
//                onTxtItemGroupCodeChanged($(this).val());
//            });

//            function onTxtItemGroupCodeChanged(value) {
//                var filterExpression = onGetItemGroupFilterExpression() + " AND ItemGroupCode = '" + value + "'";
//                $('#</%=txtItemCode.ClientID %>').val('');
//                $('#</%=txtItemName.ClientID %>').val('');
//                Methods.getObject('GetItemGroupMasterList', filterExpression, function (result) {
//                    if (result != null) {
//                        $('#</%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
//                        $('#</%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
//                    }
//                    else {
//                        $('#</%=hdnItemGroupID.ClientID %>').val('');
//                        $('#</%=txtItemGroupCode.ClientID %>').val('');
//                        $('#</%=txtItemGroupName.ClientID %>').val('');
//                    }
//                });
//            }
            //#endregion

            //#region Item
//            function getItemFilterExpression() {
//                var filterExpression = "</%=OnGetFilterExpressionItemProduct() %>";
//                var adjustmentID = $('#</%=hdnTransactionID.ClientID %>').val();
//                var locationID = $('#</%=hdnLocationID.ClientID %>').val();
//                if ($('#</%=txtItemGroupCode.ClientID %>').val() != '')
//                    filterExpression += " AND ItemGroupID = '" + $('#</%=hdnItemGroupID.ClientID %>').val() + "'";
//                if (adjustmentID != '')
//                    filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM ItemTransactionDt WHERE TransactionID = " + adjustmentID + " AND IsDeleted = 0)";
//                filterExpression += " AND ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID = " + locationID + ') AND IsDeleted = 0';
//                return filterExpression;
//            }

//            $('#lblItem.lblLink').live('click', function () {
//                openSearchDialog('item', getItemFilterExpression(), function (value) {
//                    $('#</%=txtItemCode.ClientID %>').val(value);
//                    onTxtItemCodeChanged(value);
//                });
//            });

//            $('#</%=txtItemCode.ClientID %>').live('change', function () {
//                onTxtItemCodeChanged($(this).val());
//            });

//            function onTxtItemCodeChanged(value) {
//                var filterExpression = getItemFilterExpression() + " AND ItemCode = '" + value + "'";
//                Methods.getObject('GetItemMasterList', filterExpression, function (result) {
//                    if (result != null) {
//                        $('#</%=hdnItemID.ClientID %>').val(result.ItemID);
//                        $('#</%=txtItemName.ClientID %>').val(result.ItemName1);
//                        $('#</%=hdnGCItemUnit.ClientID %>').val('');
//                        $('#</%=hdnGCBaseUnit.ClientID %>').val(result.GCItemUnit);

//                        $('#</%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
//                        $('#</%=txtItemGroupCode.ClientID %>').val(result.ItemGroupCode);
//                        $('#</%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
//                        GetItemQtyFromLocation();
//                        cboItemUnit.PerformCallback();
//                    }
//                    else {
//                        $('#</%=hdnGCBaseUnit.ClientID %>').val('');
//                        $('#</%=hdnItemID.ClientID %>').val('');
//                        $('#</%=txtItemName.ClientID %>').val('');
//                        $('#</%=txtStockLocation.ClientID %>').val('');
//                    }
//                });
//            }
            //#endregion

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
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

//        function onGetItemGroupFilterExpression() {
//            var filterExpression = "</%=OnGetFilterExpressionItemProduct() %>";
//            if ($('#</%=hdnLocationItemGroupID.ClientID %>').val() != '')
//                filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath like '%/" + $('#</%=hdnLocationItemGroupID.ClientID %>').val() + "/%')";
//            return filterExpression;
//        }

//        function GetItemQtyFromLocation() {
//            var filterExpression = "LocationID = " + $('#</%=hdnLocationID.ClientID %>').val() + " AND ItemID = " + $('#</%=hdnItemID.ClientID %>').val() + " AND IsDeleted = 0";
//            Methods.getObject('GetvItemBalanceList', filterExpression, function (result) {
//                $('#</%=txtQuantity.ClientID %>').attr('max', result.QuantityEND);
//                $('#</%=txtStockLocation.ClientID %>').val(result.QuantityEND + ' ' + result.ItemUnit);
//            });
//        }

        //#region Edit & Delete
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
//            $('#</%=hdnEntryID.ClientID %>').val(entity.ID);
//            $('#</%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
//            $('#</%=hdnGCItemUnit.ClientID %>').val(entity.GCItemUnit);
//            $('#</%=hdnConversionFactor.ClientID %>').val(entity.ConversionFactor);
//            $('#</%=hdnItemID.ClientID %>').val(entity.ItemID);
//            $('#</%=txtItemCode.ClientID %>').val(entity.ItemCode);
//            $('#</%=txtItemName.ClientID %>').val(entity.ItemName1);
//            $('#</%=txtQuantity.ClientID %>').val(entity.Quantity);
//            $('#</%=txtNotesDt.ClientID %>').val(entity.Remarks);
//            GetItemQtyFromLocation();
//            cboItemUnit.PerformCallback();
            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Cbo Item Unit
        function onCboItemUnitEndCallBack() {
//            if ($('#</%=hdnGCItemUnit.ClientID %>').val() == '')
//                cboItemUnit.SetValue($('#</%=hdnGCBaseUnit.ClientID %>').val() + '|1');
//            else
//                cboItemUnit.SetValue($('#</%=hdnGCItemUnit.ClientID %>').val() + '|' + $('#</%=hdnConversionFactor.ClientID %>').val());
//            onCboItemUnitChanged();
        }

        function onCboItemUnitChanged() {
//            var baseValue = $('#</%=hdnGCBaseUnit.ClientID %>').val();
//            var temp = cboItemUnit.GetValue().split('|');
//            var toUnitItem = temp[0];
//            var conversion = temp[1];
//            var baseText = getItemUnitName(baseValue);
//            var toConversion = cboItemUnit.GetText().split(' (')[0];
//            if (baseValue == toUnitItem) {
//                $('#</%=hdnConversionFactor.ClientID %>').val('1');
//                var conversion = "1 " + baseText + " = 1 " + baseText;
//                $('#</%=txtConversion.ClientID %>').val(conversion);
//            }
//            else {
//                var itemID = $('#</%=hdnItemID.ClientID %>').val();
//                $('#</%=hdnConversionFactor.ClientID %>').val(conversion);
//                var conversion = "1 " + toConversion + " = " + conversion + " " + baseText;
//                $('#</%=txtConversion.ClientID %>').val(conversion);
//            }
        }

        function getItemUnitName(baseValue) {
            var value = cboItemUnit.GetValue();
            cboItemUnit.SetValue(baseValue + '|1');
            var text = cboItemUnit.GetText().split(' (')[0];
            cboItemUnit.SetValue(value);
            return text;
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
                Methods.getObject('GetTransRenumerationHdList', filterExpression, function (result) {
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
                    //showToast('Save Failed', 'Error Message : ' + param[2]);
                    $('#divTransactionAdd').click();
                else {
                    onAfterSaveRecordDtSuccess(s.cpTransactionID);
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
    </script>    
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
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
                            <td class="tdLabel"><label id="lblTransactionNo"><%=GetLabel("No. Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionNo" Width="150px"  runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Dimasukkan")%></td>
                            <td><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Dimulai")%></td>
                            <td><asp:TextBox ID="txtStartEffectiveDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Renumerasi")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboRenumerationID" ClientInstanceName="cboRenumerationID" Width="50%" runat="server" /></td>
                        </tr>
                       <tr>
                            <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                        <span id="divQuickPicks" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Quick Picks")%></span>
                        <br />
                        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
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
                                                    <col style="width: 150px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel">
                                                        <label class="lblMandatory"><%=GetLabel("Komp. Renumerasi")%></label>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ID="cboRenumerationCompID" ClientInstanceName="cboRenumerationCompID" Width="300px"></dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;">
                                                        <label class="lblMandatory"><%=GetLabel("Amount")%></label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmount" CssClass="txtCurrency" min="0" Width="120px" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"></td>
                                                    <td><asp:Checkbox runat="server" ID="chkIsAllowChange" Text="Is Allow Changed"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"></td>
                                                    <td><asp:CheckBox runat="server" ID="chkIsUseFormula" Text="Is Use Formula"/></td>
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
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="RenumerationCompName" HeaderText="Nama" />
                                            <asp:BoundField DataField="Amount" HeaderStyle-CssClass="thRight" HeaderText="Amount" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("TransactionDtID") %>" bindingfield="TransactionDtID" />
                                                    <input type="hidden" value="<%#Eval("RenumerationCompID") %>" bindingfield="RenumerationCompID" />
                                                    <input type="hidden" value="<%#Eval("RenumerationCompName") %>" bindingfield="RenumerationCompName" />
                                                    <input type="hidden" value="<%#Eval("Amount") %>" bindingfield="Amount" />
                                                    <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
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
                            <div id="paging">
                            </div>
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
