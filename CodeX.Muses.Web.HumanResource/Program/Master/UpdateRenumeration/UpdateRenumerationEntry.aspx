<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="UpdateRenumerationEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.UpdateRenumerationEntry" %>

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



            $('#<%=chkIsUseFormula.ClientID %>').change(function () {
                if (this.checked) {
                    $('#<%=txtAmount.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtAmount.ClientID %>').attr('readonly', 'readonly');
                }
                else
                    $('#<%=txtAmount.ClientID %>').removeAttr('readonly');
            });

            //#region Transaction No
            function onGetRenumerationPositionFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblTransactionNo.lblLink').click(function () { 
                openSearchDialog('transrenumerationhd', onGetRenumerationPositionFilterExpression(), function (value) {
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

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    editedLineAmount = 0;

                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=txtAmount.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=chkIsAllowChange.ClientID %>').prop('checked', false);
                    $('#<%=chkIsUseFormula.ClientID %>').prop('checked', false);
                    $('#<%=txtRenumerationCompType.ClientID %>').val('');
                    tacRenumerationCompID.setValue('');
                    tacRenumerationCompID.setText('');
                    $('#<%=chkIsUseFormula.ClientID %>').change();
                    $('#entryDetailContainer').show();
                }
            });

            $('#btnRenumerationID').click(function () {
                var renumerationID = cboRenumerationID.GetValue();
                if (renumerationID != null && renumerationID != '') {
                    var id = renumerationID;
                    var url = ResolveUrl("~/Program/Master/UpdateRenumeration/RenumerationDtCtl.ascx");
                    openUserControlPopup(url, id, 'Details Renumeration', 600, 500);
                }
            });

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
            $('#<%=txtRenumerationCompType.ClientID %>').val(entity.RenumerationCompType);
            $('#<%=txtAmount.ClientID %>').val(entity.Amount).trigger('changeValue');
            $('#<%=chkIsAllowChange.ClientID %>').prop('checked', entity.IsAllowChange == 'True');
            
            $('#<%=chkIsUseFormula.ClientID %>').prop('checked', entity.IsUseFormula == 'True');
            tacRenumerationCompID.setValue(entity.RenumerationCompID);
            tacRenumerationCompID.setText(entity.RenumerationCompName);
            $('#<%=chkIsUseFormula.ClientID %>').change();
            $('#entryDetailContainer').show();
        });

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
                    showToast('Save Failed', 'Error Message : ' + param[2]);
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

        $('.lnkDetail a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/UpdateRenumeration/UpdateRenumerationEntryCtl.ascx");
            openUserControlPopup(url, id, 'Renumeration Formula', 600, 500);
        });

        function onGetRenumerationCompFilterExpression() {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var filterExpression = "IsDeleted = 0 AND RenumerationCompID NOT IN (SELECT RenumerationCompID FROM TransRenumerationDt where TransactionID = " + TransactionID + ")";
            return filterExpression;
        }

        function ontacRenumerationCompIDSearchClick() {
            openSearchDialog('renumerationcomp', onGetRenumerationCompFilterExpression(), function (value) {
                var filterExpression = onGetRenumerationCompFilterExpression() + " AND RenumerationCompCode = '" + value + "'";
                Methods.getObject('GetvRenumerationCompList', filterExpression, function (result) {
                    if (result != null) {
                        tacRenumerationCompID.setValue(result.RenumerationCompID);
                        tacRenumerationCompID.setText(result.RenumerationCompName);
                        $('#<%=txtRenumerationCompType.ClientID %>').val(result.RenumerationCompType);
                    }
                    else {
                        tacRenumerationCompID.setValue('');
                        tacRenumerationCompID.setText('');
                        $('#<%=txtRenumerationCompType.ClientID %>').val('');
                    }
                });
            });
        }

        function ontacRenumerationCompIDValueChanged() {
        }
    </script>    
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnTransactionDtID" runat="server" />
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
                            <td class="tdLabel"><label class="lblLink" id="lblTransactionNo" ><%=GetLabel("No. Transaksi")%></label></td>
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
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><dxe:ASPxComboBox ID="cboRenumerationID" ClientInstanceName="cboRenumerationID" Width="200px" runat="server" /></td>
                                        <td style="width:5px;"></td>
                                        <td><input type="button" id="btnRenumerationID" class="btnMore" value="..." /></td>
                                    </tr>
                                </table>                                
                            </td>
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
                                                <td class="tdLabel"><label class="lblMandatory" id="lblPosition"><%=GetLabel("Komp. Renumerasi")%></label></td>
                                                    <td>
                                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacRenumerationCompID" ClientInstanceName="tacRenumerationCompID" MethodName="GetvRenumerationCompList" GetFilterExpressionFunction="onGetRenumerationCompFilterExpression"
                                                            SearchFields="RenumerationCompName,RenumerationCompID" TextField="RenumerationCompName" ValueField="RenumerationCompID" SearchText="${RenumerationCompName} (<b>${RenumerationCompType}</b>)" OrderByExpression="RenumerationCompName">
                                                            <ClientSideEvents ButtonSearchClick="function(){ ontacRenumerationCompIDSearchClick(); }"
                                                                ValueChanged="function(){ ontacRenumerationCompIDValueChanged(); }" />
                                                        </cdx:CodeXAutoCompleteTextBox>   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Pembayaran")%></label></td>
                                                    <td><asp:TextBox ID="txtRenumerationCompType" ReadOnly="true" Width="200px" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Amount")%></label></td>
                                                    <td><asp:TextBox ID="txtAmount" CssClass="txtCurrency" Width="120px" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"></td>
                                                    <td><asp:Checkbox runat="server" ID="chkIsAllowChange" Text="Is Allow Changed"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"></td>
                                                    <td><asp:CheckBox runat="server" ID="chkIsUseFormula" Text="Is Use Formula" /></td>
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
                                            <asp:BoundField DataField="RenumerationCompName" HeaderText="Komp Renumerasi" />
                                            <asp:BoundField DataField="RenumerationCompType" HeaderText="Tipe Pembayaran" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="Amount" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" HeaderText="Amount" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" ItemStyle-CssClass="lnkDetail" HeaderText="Formula" HeaderStyle-Width="80px">
                                                <ItemTemplate>
                                                    <a <%# Eval("IsUseFormula").ToString() == "False" ? "style='display:none'" : ""%>>Detil</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%#IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("TransactionDtID") %>" bindingfield="TransactionDtID" />
                                                    <input type="hidden" value="<%#Eval("RenumerationCompID") %>" bindingfield="RenumerationCompID" />
                                                    <input type="hidden" value="<%#Eval("RenumerationCompName") %>" bindingfield="RenumerationCompName" />
                                                    <input type="hidden" value="<%#Eval("RenumerationCompType") %>" bindingfield="RenumerationCompType" />
                                                    <input type="hidden" value="<%#Eval("Amount") %>" bindingfield="Amount" />
                                                    <input type="hidden" value="<%#Eval("IsAllowChange") %>" bindingfield="IsAllowChange" />
                                                    <input type="hidden" value="<%#Eval("IsUseFormula") %>" bindingfield="IsUseFormula" />
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
