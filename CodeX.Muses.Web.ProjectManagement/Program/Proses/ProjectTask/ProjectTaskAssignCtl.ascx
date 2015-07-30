<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTaskAssignCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskAssignCtl" %>

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
            tacEmployeeCoordinator.setValue('');
            tacEmployeeCoordinator.setText('');
            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function () {
            alert('test');
            cbpProcessPopup.PerformCallback('save');
        });
    });

    $('.divDetailPopupEdit').die('click');
    $('.divDetailPopupEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.AssigneeID);
        tacEmployeeCoordinator.setValue(entity.AssigneeID);
        tacEmployeeCoordinator.setText(entity.EmployeeName);
        if (entity.IsAllowChangeStatus)
            $('#<%=chkIsAllowChangeStatus.ClientID %>').attr('checked');
        $('#entryDetailContainerPopup').show();
    });

    $('.divDetailPopupDelete').die('click');
    $('.divDetailPopupDelete').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.AssigneeID);
        cbpProcessPopup.PerformCallback('delete');
    });

    //#region Employee
    window.onGetEmployeeFilterExpression = function () {
        var filterExpression = "<%=OnGetEmployeeFilterExpression() %>";
        return filterExpression;
    }

    function onTacEmployeeCoordinatorButtonSearchClick() {
        openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
            var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
            Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                if (result != null) {
                    tacEmployeeCoordinator.setValue(result.EmployeeID);
                    tacEmployeeCoordinator.setText(result.EmployeeName);
                    entityToControlEmployee(result);
                }
                else {
                    tacEmployeeCoordinator.setValue('');
                    tacEmployeeCoordinator.setText('');
                    entityToControlEmployee(null);
                }
            });
        });
    }

    function onTacEmployeeCoordinatorValueChanged() {
        var id = tacEmployeeCoordinator.getValue();
        if (id != '') {
            var filterExpression = "EmployeeID = " + value;
            Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                entityToControlEmployee(result);
            });
        }
    }

    function entityToControlEmployee(result) {
        if (result != null)
            $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val(result.EmployeeID);
        else
            $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val('');
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
    <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Member")%></label></td>
                        <td>
                            <input type="hidden" id="hdnEmployeeCoordinatorID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacEmployeeCoordinator" ClientInstanceName="tacEmployeeCoordinator" MethodName="GetEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                SearchFields="EmployeeName,EmployeeCode" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeCoordinatorButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacEmployeeCoordinatorValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox runat="server" ID="chkIsAllowChangeStatus" Text="Ubah Status" /></td>
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
                            <asp:BoundField DataField="EmployeeName" HeaderText="Nama"/>
                            <asp:BoundField DataField="Position" HeaderText="Posisi"/>
                            <asp:BoundField DataField="IsAllowChangeStatus" HeaderText="Ubah Status" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailPopupDelete">X</div>
                                    <div style='float:right;margin-right:10px;' class="divDetailPopupEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("ProjectTaskID") %>" bindingfield="ProjectTaskID" />
                                    <input type="hidden" value="<%#Eval("AssigneeID") %>" bindingfield="AssigneeID" />
                                    <input type="hidden" value="<%#Eval("EmployeeName") %>" bindingfield="EmployeeName" />
                                    <input type="hidden" value="<%#Eval("IsAllowChangeStatus") %>" bindingfield="IsAllowChangeStatus" />
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

