<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateRenumerationEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.UpdateRenumerationEntryCtl" %>

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
        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
                cbpProcessPopup.PerformCallback('save');
        });
    });

    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnGCDayType.ClientID %>').val(entity.GCDayType);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnGCDayType.ClientID %>').val(entity.GCDayType);
        $('#<%=txtDayType.ClientID %>').val(entity.DayType);
        tacFormulaID.setValue(entity.FormulaID);
        tacFormulaID.setText(entity.FormulaName);
        $('#entryDetailContainerPopup').show();
    });

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

    //#region DtFormula
    function onGetDtFormulaFilterExpression() {
        var filterExpression = "<%=OnGetFormulaFilterExpression() %>";
        return filterExpression;
    }

    function onTacFormulaSearchClick() {
        openSearchDialog('renumerationcompformulahd', onGetDtFormulaFilterExpression(), function (value) {
            var filterExpression = onGetDtFormulaFilterExpression() + " AND FormulaCode = '" + value + "'";
            Methods.getObject('GetRenumerationCompFormulaHdList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnFormulaID.ClientID %>').val(result.FormulaID);
                    tacFormulaID.setValue(result.FormulaID);
                    tacFormulaID.setText(result.FormulaName);
                }
                else {
                    $('#<%=hdnFormulaID.ClientID %>').val('');
                    tacFormulaID.setValue('');
                    tacFormulaID.setText('');
                }
            });
        });
    }

    function onTacFormulaValueChanged() {
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnLstDayTypeID" value="" runat="server" />
    <input type="hidden" id="hdnDisplayOrder" value="" runat="server" />

    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                         <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Hari")%></label></td>
                         <td>
                            <input type="hidden" id="hdnGCDayType" runat="server" />
                            <asp:TextBox ID="txtDayType" runat="server" ReadOnly="true" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                         <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Formula")%></label></td>
                         <td>
                            <input type="hidden" id="hdnFormulaID" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacFormulaID" ClientInstanceName="tacFormulaID" MethodName="GetvRenumerationCompFormulaHdList" GetFilterExpressionFunction="onGetDtFormulaFilterExpression"
                                SearchFields="FormulaCode,FormulaName" TextField="FormulaName" ValueField="FormulaID" SearchText="${FormulaName} (<b>${FormulaCode}</b>)" OrderByExpression="FormulaName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacFormulaSearchClick(); }"
                                    ValueChanged="function(){ onTacFormulaValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
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
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="StandardCodeName" HeaderText="Tipe Hari" HeaderStyle-Width="120px"/> 
                            <asp:TemplateField HeaderText="Formula">
                                <ItemTemplate>
                                    <div id="divFormula" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("StandardCodeID") %>" bindingfield="GCDayType" />
                                    <input type="hidden" value="<%#Eval("StandardCodeName") %>" bindingfield="DayType" />
                                    <input type="hidden" id="hdnFormulaID" runat="server" bindingfield="FormulaID" />
                                    <input type="hidden" id="hdnFormulaName" runat="server"  bindingfield="FormulaName" />
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

