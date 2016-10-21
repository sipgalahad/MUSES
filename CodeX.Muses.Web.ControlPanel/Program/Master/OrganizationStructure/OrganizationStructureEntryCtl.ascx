<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationStructureEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.OrganizationStructureEntryCtl" %>

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
            $('#<%=txtOrganizationPositionName.ClientID %>').val('');
            cboGCPositionLevel.SetValue('');
            cboGCPositionType.SetValue('');
            cboGCScheduleType.SetValue('');
            tacOrganizationPositionEmployee.setValue('');
            tacOrganizationPositionEmployee.setText('');
            $('#<%=hdnPICEmployeeID.ClientID %>').val('');
            cboWeeklyScheduleID.SetValue('');
            $('#<%=chkIsSchedule.ClientID %>').prop('checked', false);
            $('#entryDetailContainerPopup').show();
        });

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
                $('#<%=hdnEntryID.ClientID %>').val(entity.OrganizationPositionID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.OrganizationPositionID);
        $('#<%=txtOrganizationPositionName.ClientID %>').val(entity.OrganizationPositionName);
        cboGCPositionLevel.SetValue(entity.GCPositionLevel);
        cboGCPositionType.SetValue(entity.GCPositionType);
        cboGCScheduleType.SetValue(entity.GCScheduleType);
        cboWeeklyScheduleID.SetValue(entity.WeeklyScheduleID);
        tacOrganizationPositionEmployee.setValue(entity.PICEmployeeID);
        tacOrganizationPositionEmployee.setText(entity.PICEmployeeName);
        $('#<%=hdnPICEmployeeID.ClientID %>').val(entity.PICEmployeeID);
        $('#<%=chkIsSchedule.ClientID %>').prop('checked', entity.IsScheduleAllowChanged == 'True');
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

    //#region Organization Position Get Employee

    function onGetEmployeeFilterExpression() {
        var filterExpression = "<%=OnGetEmployeeFilterExpression() %>";
        return filterExpression;
    }

    function onTacOrganizationPositionEmployeeSearchClick() {
        openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
            var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
            Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnPICEmployeeID.ClientID %>').val(result.EmployeeID);
                    tacOrganizationPositionEmployee.setValue(result.EmployeeID);
                    tacOrganizationPositionEmployee.setText(result.EmployeeName);
                }
                else {
                    $('#<%=hdnPICEmployeeID.ClientID %>').val('');
                    tacOrganizationPositionEmployee.setValue('');
                    tacOrganizationPositionEmployee.setText('');
                }
            });
        });

    }

    function onTacOrganizationPositionEmployeeValueChanged() {
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    
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
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtOrganizationPositionName"  Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Level")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboGCPositionLevel" ClientInstanceName="cboGCPositionLevel" Width="200px"></dxe:ASPxComboBox></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboGCPositionType" ClientInstanceName="cboGCPositionType" Width="200px"></dxe:ASPxComboBox></td>
                    </tr>
                     <tr>
                         <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Supervisor")%></label></td>
                         <td>
                            <input type="hidden" id="hdnPICEmployeeID" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacOrganizationPositionEmployee" ClientInstanceName="tacOrganizationPositionEmployee" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                SearchFields="EmployeeName,EmployeeCode" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacOrganizationPositionEmployeeSearchClick(); }"
                                    ValueChanged="function(){ onTacOrganizationPositionEmployeeValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Jadwal")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboGCScheduleType" ClientInstanceName="cboGCScheduleType" Width="200px"></dxe:ASPxComboBox></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jadwal Mingguan")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboWeeklyScheduleID" ClientInstanceName="cboWeeklyScheduleID" Width="200px"></dxe:ASPxComboBox></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsSchedule" Text="Is Schedule Allow Change" /></td>
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
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="OrganizationPositionID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="OrganizationPositionName" HeaderText="Nama" HeaderStyle-CssClass="thLeft" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="PositionLevel" HeaderText="Level"  />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("OrganizationPositionID") %>" bindingfield="OrganizationPositionID" />
                                    <input type="hidden" value="<%#Eval("OrganizationPositionName") %>" bindingfield="OrganizationPositionName" />
                                    <input type="hidden" value="<%#Eval("GCPositionLevel") %>" bindingfield="GCPositionLevel" />
                                    <input type="hidden" value="<%#Eval("PositionLevel") %>" bindingfield="PositionLevel" />
                                    <input type="hidden" value="<%#Eval("GCPositionType") %>" bindingfield="GCPositionType" />
                                    <input type="hidden" value="<%#Eval("PositionType") %>" bindingfield="PositionType" />
                                    <input type="hidden" value="<%#Eval("GCScheduleType") %>" bindingfield="GCScheduleType" />
                                    <input type="hidden" value="<%#Eval("ScheduleType") %>" bindingfield="ScheduleType" />
                                    <input type="hidden" value="<%#Eval("PICEmployeeID") %>" bindingfield="PICEmployeeID" />
                                    <input type="hidden" value="<%#Eval("PICEmployeeName") %>" bindingfield="PICEmployeeName" />
                                    <input type="hidden" value="<%#Eval("WeeklyscheduleID") %>" bindingfield="WeeklyScheduleID" />
                                    <input type="hidden" value="<%#Eval("WeeklyscheduleName") %>" bindingfield="WeeklyscheduleName" />
                                    <input type="hidden" value="<%#Eval("IsScheduleAllowChanged") %>" bindingfield="IsScheduleAllowChanged" />
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

