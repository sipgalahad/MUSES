<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTaskStructureCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskStructureCtl" %>

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
            tacProjectTask.setValue('');
            tacProjectTask.setText('');
            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function () {
            cbpProcessPopup.PerformCallback('save');
        });
    });

    $('.divDetailPopupDelete').die('click');
    $('.divDetailPopupDelete').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.PrevProjectTaskID);
        cbpProcessPopup.PerformCallback('delete');
    });

    //#region Project Task
    window.onGetProjectTaskFilterExpression = function () {
        var filterExpression = "<%=OnGetProjectTaskFilterExpression() %>";
        return filterExpression;
    }

    function onTacProjectTaskButtonSearchClick() {
        openSearchDialog('projecttask', onGetProjectTaskFilterExpression(), function (value) {
            var filterExpression = onGetProjectTaskFilterExpression() + " AND ProjectTaskID = '" + value + "'";
            Methods.getObject('GetvProjectTaskList', filterExpression, function (result) {
                if (result != null) {
                    tacProjectTask.setValue(result.ProjectTaskID);
                    tacProjectTask.setText(result.ProjectTaskName);
                    entityToControlProjectTask(result);
                }
                else {
                    tacProjectTask.setValue('');
                    tacProjectTask.setText('');
                    entityToControlProjectTask(null);
                }
            });
        });
    }

    function onTacProjectTaskValueChanged() {
        var id = tacProjectTask.getValue();
        if (id != '') {
            var filterExpression = "ProjectTaskID = " + value;
            Methods.getObject('GetvProjectTaskList', filterExpression, function (result) {
                entityToControlProjectTask(result);
            });
        }
    }

    function entityToControlProjectTask(result) {
        if (result != null)
            $('#<%=hdnProjectTaskID.ClientID %>').val(result.ProjectTaskID);
        else
            $('#<%=hdnProjectTaskID.ClientID %>').val('');
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
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtProjectTaskName" ReadOnly="true" Width="100%" runat="server" /></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tugas")%></label></td>
                        <td>
                            <input type="hidden" id="hdnProjectTaskID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacProjectTask" ClientInstanceName="tacProjectTask" MethodName="GetProjectTaskList" GetFilterExpressionFunction="onGetProjectTaskFilterExpression"
                                SearchFields="ProjectTaskName,ProjectTaskCode" TextField="ProjectTaskName" ValueField="ProjectTaskID" SearchText="${ProjectTaskName} (<b>${ProjectTaskCode}</b>)" OrderByExpression="ProjectTaskName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacProjectTaskButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacProjectTaskValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
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
                    <asp:GridView ID="grdPopupView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="PrevProjectTaskCode" HeaderText="Kode" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="PrevProjectTaskName" HeaderText="Nama"/>
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailPopupDelete">X</div>
                                    <input type="hidden" value="<%#Eval("PrevProjectTaskID") %>" bindingfield="PrevProjectTaskID" />
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

