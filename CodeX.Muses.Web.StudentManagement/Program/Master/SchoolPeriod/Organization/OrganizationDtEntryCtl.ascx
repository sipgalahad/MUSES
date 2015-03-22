<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.OrganizationDtEntryCtl" %>

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
            $('#<%=txtDisplayOrder.ClientID %>').val('');
            $('#<%=txtPosition.ClientID %>').val('');
            tacStudentCoordinator.setValue('');
            tacStudentCoordinator.setText('');
            $('#<%=hdnStudentCoordinatorID.ClientID %>').val('');
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
                cboGrade.SetValue(entity.GCGrade);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.OrganizationDtID);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $('#<%=txtPosition.ClientID %>').val(entity.Position);
        tacStudentCoordinator.setValue(entity.StudentCoordinatorID);
        tacStudentCoordinator.setText(entity.StudentCoordinatorName);
        $('#<%=hdnStudentCoordinatorID.ClientID %>').val(result.StudentCoordinatorID);  

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

    //#region Student
    function onGetStudentFilterExpression() {
        var filterExpression = "<%=OnGetStudentFilterExpression() %>";
        return filterExpression;
    }

    function onTacStudentCoordinatorButtonSearchClick() {
        openSearchDialog('student', onGetStudentFilterExpression(), function (value) {
            var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
            Methods.getObject('GetStudentList', filterExpression, function (result) {
                if (result != null) {
                    tacStudentCoordinator.setValue(result.StudentID);
                    tacStudentCoordinator.setText(result.StudentName);
                    entityToControlStudent(result);
                }
                else {
                    tacStudentCoordinator.setValue('');
                    tacStudentCoordinator.setText('');
                    entityToControlStudent(null);
                }
            });
        });

    }

    function onTacStudentCoordinatorValueChanged() {
        var id = tacStudentCoordinator.getValue();
        if (id != '') {
            var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
            Methods.getObject('GetStudentList', filterExpression, function (result) {
                entityToControlStudent(result);
            });
        }
    }

    function entityToControlStudent(result) {
        if (result != null)
            $('#<%=hdnStudentCoordinatorID.ClientID %>').val(result.StudentID);        
        else
            $('#<%=hdnStudentCoordinatorID.ClientID %>').val('');
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Formula")%></label></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jabatan / Posisi") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtPosition" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan")%></label></td>
                        <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="80px" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Koordinator")%></label></td>
                        <td>
                            <input type="hidden" id="hdnStudentCoordinatorID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacStudentCoordinator" ClientInstanceName="tacStudentCoordinator" MethodName="GetStudentList" GetFilterExpressionFunction="onGetStudentFilterExpression"
                                SearchFields="StudentName,StudentCode" TextField="StudentName" ValueField="StudentID" SearchText="${StudentName} (<b>${StudentCode}</b>)" OrderByExpression="StudentName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacStudentCoordinatorButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacStudentCoordinatorValueChanged(); }" />
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
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="Position" HeaderText="Jabatan / Posisi" />
                            <asp:BoundField DataField="StudentCoordinatorName" HeaderText="Koordinator" HeaderStyle-Width="300px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("OrganizationDtID") %>" bindingfield="OrganizationDtID" />
                                    <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                    <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                    <input type="hidden" value="<%#Eval("StudentCoordinatorID") %>" bindingfield="StudentCoordinatorID" />
                                    <input type="hidden" value="<%#Eval("StudentCoordinatorName") %>" bindingfield="StudentCoordinatorName" />
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

