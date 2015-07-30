<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="BudgetRealizationEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.BudgetRealizationEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
            }
            else {
                $('#divTransactionAdd').hide();
            }

            setDatePicker('<%=txtRealizationDate.ClientID %>');

            //#region Realization No
            $('#lblBudgetRealizationNo.lblLink').click(function () {
                openSearchDialog('budgetrealizationhd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtBudgetRealizationNo.ClientID %>').val(value);
                    onTxtBudgetRequestNoChanged(value);
                });
            });

            $('#<%=txtBudgetRealizationNo.ClientID %>').change(function () {
                onTxtBudgetRequestNoChanged($(this).val());
            });

            function onTxtBudgetRequestNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    tacProjectBudget.setValue('');
                    tacProjectBudget.setText('');
                    $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                    $('#<%=txtRequestAmount.ClientID %>').val(0).trigger('changeValue');
                    $('#<%=txtRealizationAmount.ClientID %>').val(0).trigger('changeValue');
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

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }

        //#region ProjectBudget
        function onGetProjectBudgetFilterExpression() {
            var filterExpression = "<%=OnGetProjectBudgetFilterExpression() %>" + " AND ProjectID = " + cboProject.GetValue();
            filterExpression = "BudgetRequestID IN ( " + filterExpression + " ) AND IsDeleted = 0  AND RealizationAmount = 0";
            return filterExpression;
        }

        function onTacProjectBudgetButtonSearchClick() {
            var filterExpression = onGetProjectBudgetFilterExpression();
            openSearchDialog('budgetrequestdt', filterExpression, function (value) {
                var filterExpression = "BudgetRequestDtID = '" + value + "'";
                Methods.getObject('GetvBudgetRequestDtList', filterExpression, function (result) {
                    if (result != null) {
                        tacProjectBudget.setValue(result.BudgetRequestDtID);
                        tacProjectBudget.setText(result.BudgetName);
                        $('#<%=txtProposedBudget.ClientID %>').val(result.ProposedAmount - result.RealizationAmount).trigger('changeValue');
                        $('#<%=txtRequestAmount.ClientID %>').val(result.RequestAmount).trigger('changeValue');
                        entityToControlProjectBudget(result);
                    }
                    else {
                        tacProjectBudget.setValue('');
                        tacProjectBudget.setText('');
                        $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                        $('#<%=txtRequestAmount.ClientID %>').val(0).trigger('changeValue');
                        entityToControlProjectBudget(null);
                    }
                });
            });
        }

        function onTacProjectBudgetValueChanged() {
            var id = tacProjectBudget.getValue();
            if (id != '') {
                var filterExpression = "BudgetRequestDtID = " + id;
                Methods.getObject('GetvBudgetRequestDtList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=txtProposedBudget.ClientID %>').val(result.ProposedAmount - result.RealizationAmount).trigger('changeValue');
                        $('#<%=txtRequestAmount.ClientID %>').val(result.RequestAmount).trigger('changeValue');
                        entityToControlProjectBudget(result);
                    }
                    else {
                        $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                        $('#<%=txtRequestAmount.ClientID %>').val(0).trigger('changeValue');
                        entityToControlProjectBudget(null);
                    }
                });
            } else {
                $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                $('#<%=txtRealizationAmount.ClientID %>').val(0).trigger('changeValue');
                entityToControlProjectBudget(null);
            }
        }

        function entityToControlProjectBudget(result) {
            if (result != null)
                $('#<%=hdnBudgetID.ClientID %>').val(result.BudgetRequestDtID);
            else
                $('#<%=hdnBudgetID.ClientID %>').val(null);
        }
        //#endregion

        $('.divDetailEdit').die('click');
        $('.divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetRealizationDtID);
            tacProjectBudget.setValue(entity.BudgetRequestDtID);
            tacProjectBudget.setText(entity.BudgetName);
            $('#<%=hdnBudgetID.ClientID %>').val(entity.BudgetRequestDtID);

            var filterExpression = "BudgetID = " + entity.BudgetID;
            var realizationAmount = 0;
            Methods.getObject('GetvProjectBudgetList', filterExpression, function (result) {
                if (result != null) {
                    realizationAmount = parseFloat(result.RealizationAmount);
                }
                else {
                    realizationAmount = 0;
                }
            });

            $('#<%=txtProposedBudget.ClientID %>').val(entity.ProposedAmount - realizationAmount).trigger('changeValue');
            $('#<%=txtRequestAmount.ClientID %>').val(entity.RequestAmount).trigger('changeValue');
            $('#<%=txtRealizationAmount.ClientID %>').val(0).trigger('changeValue');

            $('#entryDetailContainer').show();
        });

        $('.divDetailDelete').die('click');
        $('.divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetRealizationDtID);
            cbpProcess.PerformCallback('delete');
        });

        function onAfterSaveRecordDtSuccess(OrderID) {
            if ($('#<%=hdnID.ClientID %>').val() == '0') {
                $('#<%=hdnID.ClientID %>').val(OrderID);
                var filterExpression = 'BudgetRealizationID = ' + OrderID;
                Methods.getObject('GetBudgetRealizationHdList', filterExpression, function (result) {
                    $('#<%=txtBudgetRealizationNo.ClientID %>').val(result.BudgetRequestNo);
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

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var OrderID = s.cpOrderID;
                    onAfterSaveRecordDtSuccess(OrderID);
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
    </script>
    <div>
        <input type="hidden" value="0" id="hdnID" runat="server" />
        <input type="hidden" value="" id="hdnPageCount" runat="server" />
        <input type="hidden" value="" id="hdnRowCount" runat="server" />
        <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
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
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblBudgetRealizationNo"><%=GetLabel("No. Realisasi")%></label></td>
                            <td><asp:TextBox ID="txtBudgetRealizationNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Project") %></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboProject" ClientInstanceName="cboProject" Width="200px"/>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr><td style="padding-right: 1px; width: 140px;"><asp:TextBox ID="txtRealizationDate" Width="110px" CssClass="datepicker" runat="server" /></td></tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top: 5px"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                <table style="width:100%">
                                    <colgroup>
                                        <col style="width: 50%" />
                                    </colgroup>
                                    <tr>
                                        <td valign="top">
                                            <table style="width: 100%">
                                                <colgroup>
                                                    <col style="width: 140px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Anggaran")%></label></td>
                                                    <td>
                                                        <input type="hidden" value="" id="hdnBudgetID" runat="server" />
                                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacProjectBudget" ClientInstanceName="tacProjectBudget" MethodName="GetvBudgetRequestDtList" GetFilterExpressionFunction="onGetProjectBudgetFilterExpression"
                                                            SearchFields="BudgetName,BudgetCode" TextField="BudgetName" ValueField="BudgetRequestDtID" SearchText="${BudgetName} (<b>${BudgetCode}</b>)" OrderByExpression="BudgetName">
                                                            <ClientSideEvents ButtonSearchClick="function(){ onTacProjectBudgetButtonSearchClick(); }"
                                                                ValueChanged="function(){ onTacProjectBudgetValueChanged(); }" />
                                                        </cdx:CodeXAutoCompleteTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jmlh. Anggaran")%></label></td>
                                                    <td><asp:TextBox runat="server" ID="txtProposedBudget" ReadOnly="true" Width="120px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jmlh. Diminta")%></label></td>
                                                    <td><asp:TextBox runat="server" ID="txtRequestAmount" ReadOnly="true" Width="120px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jmlh. Dicairkan")%></label></td>
                                                    <td><asp:TextBox runat="server" ID="txtRealizationAmount" Width="120px" CssClass="txtCurrency" /></td>
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
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="BudgetRealizationDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="BudgetCode" HeaderText="Kode" HeaderStyle-Width="150px"/>
                                            <asp:BoundField DataField="BudgetName" HeaderText="Anggaran"  />
                                            <asp:BoundField DataField="ProposedAmount" HeaderText="Dianggarkan" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="RealizationAmount" HeaderText="Direalisasikan" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("BudgetRealizationDtID") %>" bindingfield="BudgetRealizationDtID" />
                                                    <input type="hidden" value="<%#Eval("BudgetRequestDtID") %>" bindingfield="BudgetRequestDtID" />
                                                    <input type="hidden" value="<%#Eval("BudgetID") %>" bindingfield="BudgetID" />
                                                    <input type="hidden" value="<%#Eval("BudgetName") %>" bindingfield="BudgetName" />
                                                    <input type="hidden" value="<%#Eval("ProposedAmount") %>" bindingfield="ProposedAmount" />
                                                    <input type="hidden" value="<%#Eval("RealizationAmount") %>" bindingfield="RealizationAmount" />
                                                    <input type="hidden" value="<%#Eval("RequestAmount") %>" bindingfield="RequestAmount" />
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
                            <div id="paging"></div>
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
