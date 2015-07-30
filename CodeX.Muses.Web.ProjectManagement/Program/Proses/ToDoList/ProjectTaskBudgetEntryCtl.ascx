<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTaskBudgetEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskBudgetEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            tacProjectBudget.setValue('');
            tacProjectBudget.setText('');
            $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
            $('#<%=txtRealizationBudget.ClientID %>').val(0).trigger('changeValue');
            $('#<%=txtUsedAmount.ClientID %>').val(0).trigger('changeValue');
            $('#<%=txtRemarks.ClientID %>').val('');
            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                cbpProcessPopup.PerformCallback('save');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divPopupDelete').die('click');
    $('#<%=grdView.ClientID %> .divPopupDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divPopupEdit').die('click');
    $('#<%=grdView.ClientID %> .divPopupEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetID);
        tacProjectBudget.setValue(entity.BudgetID);
        tacProjectBudget.setText(entity.BudgetName);
        $('#<%=txtProposedBudget.ClientID %>').val(entity.ProposedAmount).trigger('changeValue');
        $('#<%=txtRealizationBudget.ClientID %>').val(entity.RealizationAmount).trigger('changeValue');
        $('#<%=txtUsedAmount.ClientID %>').val(entity.UsedBudget).trigger('changeValue');
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        $('#entryDetailContainerPopup').show();
    });

    //#region ProjectBudget
    window.onGetProjectBudgetFilterExpression = function () {
        var filterExpression = "ProjectID = " + $('#<%=hdnProjectID.ClientID %>').val() + " AND ItemID IS NULL";
        return filterExpression;
    }

    function onTacProjectBudgetButtonSearchClick() {
        openSearchDialog('projectbudget', onGetProjectBudgetFilterExpression(), function (value) {
            var filterExpression = onGetProjectBudgetFilterExpression() + " AND BudgetCode = '" + value + "'";
            Methods.getObject('GetvProjectBudgetList', filterExpression, function (result) {
                if (result != null) {
                    tacProjectBudget.setValue(result.BudgetID);
                    tacProjectBudget.setText(result.BudgetName);
                    $('#<%=txtProposedBudget.ClientID %>').val(result.ProposedAmount).trigger('changeValue');
                    $('#<%=txtRealizationBudget.ClientID %>').val(result.RealizationAmount).trigger('changeValue');
                    entityToControlProjectBudget(result);
                }
                else {
                    tacProjectBudget.setValue('');
                    tacProjectBudget.setText('');
                    $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                    $('#<%=txtRealizationBudget.ClientID %>').val(0).trigger('changeValue');
                    entityToControlProjectBudget(null);
                }
            });
        });
    }

    function onTacProjectBudgetValueChanged() {
        var id = tacProjectBudget.getValue();
        if (id != '') {
            var filterExpression = "BudgetID = " + id;
            Methods.getObject('GetvProjectBudgetList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=txtProposedBudget.ClientID %>').val(result.ProposedAmount).trigger('changeValue');
                    $('#<%=txtRealizationBudget.ClientID %>').val(result.RealizationAmount).trigger('changeValue');
                    entityToControlProjectBudget(result);
                }
                else {
                    $('#<%=txtProposedBudget.ClientID %>').val(0).trigger('changeValue');
                    $('#<%=txtRealizationBudget.ClientID %>').val(0).trigger('changeValue');
                    entityToControlProjectBudget(null);
                }
            });
        } else {
            entityToControlProjectBudget(null);
        }
    }

    function entityToControlProjectBudget(result) {
        if (result != null)
            $('#<%=hdnBudgetID.ClientID %>').val(result.BudgetID);
        else
            $('#<%=hdnBudgetID.ClientID %>').val(null);
    }
    //#endregion

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnProjectID" value="" runat="server" />
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td ><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Mulai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox ID="txtPTStartDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                        <td style="width:10px; text-align:center">&nbsp;</td>
                        <td><asp:TextBox ID="txtPTStartTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox ID="txtPTEndDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                        <td style="width:10px; text-align:center">&nbsp;</td>
                        <td><asp:TextBox ID="txtPTEndTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
            <td><asp:TextBox runat="server" ID="txtPTRemarks" TextMode="MultiLine" Rows="2" Width="300px" ReadOnly="true" /></td>
        </tr>
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table id="tblEntryPopup">
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Anggaran")%></label></td>
                        <td>
                            <input type="hidden" value="" id="hdnBudgetID" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacProjectBudget" ClientInstanceName="tacProjectBudget" MethodName="GetvProjectBudgetList" GetFilterExpressionFunction="onGetProjectBudgetFilterExpression"
                                SearchFields="BudgetName,BudgetCode" TextField="BudgetName" ValueField="BudgetID" SearchText="${BudgetName} (<b>${BudgetCode}</b>)" OrderByExpression="BudgetName">
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
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jmlh. Diterima")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtRealizationBudget" ReadOnly="true" Width="120px" CssClass="txtCurrency" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jmlh. Digunakan")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtUsedAmount" Width="120px" CssClass="txtCurrency" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr id="trSaveEntryPopup">
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="BudgetName" HeaderText="Anggaran" HeaderStyle-Width="250px" />
                            <asp:TemplateField HeaderText="Keterangan" >
                                <ItemTemplate>
                                    <%#Eval("CustomRemarks")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UsedBudget" HeaderText="Jumlah" HeaderStyle-Width="120px" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divPopupDelete">X</div>
                                    <div style='float:right;margin-right:10px;' class="divPopupEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("BudgetID") %>" bindingfield="BudgetID" />
                                    <input type="hidden" value="<%#Eval("BudgetName") %>" bindingfield="BudgetName" />
                                    <input type="hidden" value="<%#Eval("ProposedAmount") %>" bindingfield="ProposedAmount" />
                                    <input type="hidden" value="<%#Eval("RealizationAmount") %>" bindingfield="RealizationAmount" />
                                    <input type="hidden" value="<%#Eval("UsedBudget") %>" bindingfield="UsedBudget" />
                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

