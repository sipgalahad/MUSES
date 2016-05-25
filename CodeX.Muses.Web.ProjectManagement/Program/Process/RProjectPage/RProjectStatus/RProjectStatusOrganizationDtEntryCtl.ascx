<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RProjectStatusOrganizationDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectStatusOrganizationDtEntryCtl" %>

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
            tacEmployeeCoordinator.setValue('');
            tacEmployeeCoordinator.setText('');
            //tacParent.setValue('');
            //tacParent.setText('');
            $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val('');

            idxEmployee = 0;
            $('.trEmployeeDt').each(function () {
                $(this).remove();
            });

            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                var result = '';
                $('.tacEmployee').each(function () {
                    if (result != "")
                        result += ',';
                    result += $(this).find('.hdnAutoCompleteValue').val();
                });
                $('#<%=hdnEmployeeSave.ClientID %>').val(result);
                cbpProcessPopup.PerformCallback('save');
            }
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
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectOrganizationID);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $('#<%=txtPosition.ClientID %>').val(entity.Position);
        $('#<%=hdnParent.ClientID %>').val(entity.ParentID);
        tacParent.setValue(entity.ParentID);
        tacParent.setText(entity.ParentName);
        $('#<%=chkIsHeader.ClientID %>').attr('checked', entity.IsHeader == 'True');
        $('#<%=chkIsAllowAddTeam.ClientID %>').attr('checked', entity.IsAllowAddTeam == 'True');
        $('#<%=chkIsProjectAdmin.ClientID %>').attr('checked', entity.IsProjectAdmin == 'True'); 
        tacEmployeeCoordinator.setValue(entity.EmployeeCoordinatorID);
        tacEmployeeCoordinator.setText(entity.EmployeeCoordinatorName);
        $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val(entity.EmployeeCoordinatorID);

        idxEmployee = 0;
        $('.trEmployeeDt').each(function () {
            $(this).remove();
        });

        if (entity.ListEmployeeID != '') {
            var lstEmployeeID = entity.ListEmployeeID.split(',');
            var lstEmployeeName = entity.ListEmployeeName.split(', ');
            for (var i = 0; i < lstEmployeeID.length; ++i) {
                $('#divEntryDtAdd').click();

                $tr = $('.trEmployeeDt').last();
                $tacEmployee = $tr.find('.tacEmployee');
                $tacEmployee.find('.hdnAutoCompleteValue').val(lstEmployeeID[i]);
                $tacEmployee.find('.hdnAutoCompleteText').val(lstEmployeeName[i]);
                $tacEmployee.find('.txtAutoComplete').val(lstEmployeeName[i]);
            }
        }

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

    //#region Employee
    window.onGetEmployeeFilterExpression = function() {
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
            var filterExpression = "EmployeeID = " + id;
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

    //#region Parent
    window.onGetParentFilterExpression = function () {
        var filterExpression = "<%=OnGetParentFilterExpression() %>";
        return filterExpression;
    }

    function onTacParentButtonSearchClick() {
        openSearchDialog('rprojectorganization', onGetParentFilterExpression(), function (value) {
            var filterExpression = onGetParentFilterExpression() + " AND ProjectOrganizationID = " + value;
            Methods.getObject('GetvRProjectOrganizationList', filterExpression, function (result) {
                if (result != null) {
                    tacParent.setValue(result.ProjectOrganizationID);
                    tacParent.setText(result.Position);
                    $('#<%=hdnParent.ClientID %>').val(result.ProjectOrganizationID);
                }
                else {
                    tacParent.setValue('');
                    tacParent.setText('');
                    $('#<%=hdnParent.ClientID %>').val('');
                }
            });
        });

    }

    function onTacParentValueChanged() {
        $('#<%=hdnParent.ClientID %>').val(tacParent.getValue());
    }
    //#endregion

    var idxEmployee = 0;
    $('#divEntryDtAdd').click(function () {
        $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
        $newTr = $newTr.replace(/\$\{idx}/g, idxEmployee);
        $newTr = $($newTr);
        $newTr.insertBefore($('#trSaveEntryPopup'));

        var tempHelper = new CodeXClientAutoCompleteHelper();
        tempHelper.init("Employee" + idxEmployee, "EmployeeCode,EmployeeName", "GetvEmployeeList", "", "onGetEmployeeFilterExpression", "EmployeeID");
        tempHelper.setClientSideEvents(onEmployeeIDValueChanged);
        tempHelper.initializeControl();
        idxEmployee++;
    });

    function onEmployeeIDValueChanged($s) {
        $tacTr = $s.closest('tr');
        if ($s.val() != '') {
            //var trIdx = $('.trJournalEntry').index($tacTr);
            //if (trIdx == $('.trJournalEntry').length - 1)
            //    addEntityRowPrescription();
        }
    }

    $('.divDeleteEntryDt').live('click', function () {
        $tr = $(this).closest('tr').parent().closest('tr');
        $tr.remove();
    });

    $('.tacEmployee .btnAutoCompleteSearchMore').die('click');
    $('.tacEmployee .btnAutoCompleteSearchMore').live('click', function () {
        $tacTr = $(this).closest('tr');
        openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
            var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
            Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                $tacCOA = $tacTr.find('.tacEmployee');
                if (result != null) {
                    $tacCOA.find('.hdnAutoCompleteValue').val(result.EmployeeID);
                    $tacCOA.find('.hdnAutoCompleteText').val(result.EmployeeName);
                    $tacCOA.find('.txtAutoComplete').val(result.EmployeeName);
                }
                else {
                    $tacCOA.find('.hdnAutoCompleteValue').val('');
                    $tacCOA.find('.hdnAutoCompleteText').val('');
                    $tacCOA.find('.txtAutoComplete').val('');
                }
                onEmployeeIDValueChanged($tacCOA.find('.txtAutoComplete'));
            });
            //var trIdx = $('.trPrescriptionEntry').index($tacTr);
            //if (trIdx == $('.trPrescriptionEntry').length - 1)
            //    addEntityRowPrescription();
            $tacTr = null;
        });
    });
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
    <input type="hidden" id="hdnMyProjectOrganizationID" value="" runat="server" />
    <script id="tmplEntityDt" type="text/x-jquery-tmpl">
        <tr class="trEmployeeDt">
            <td>&nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div id="Employee${idx}" class="tacEmployee">
                                <div>
                                    <div class="containerAutoComplete">
                                        <input type="hidden" class="hdnAutoCompleteValue"/>
                                        <input type="hidden" class="hdnAutoCompleteText"/>
                                        <input type="hidden" class="hdnIsRequired" value="1"/>
                                        <input type="hidden" class="hdnValidationGroup" value="mpDrugsQuickPicks"/>
                                        <input type="text" class="required txtAutoComplete" validationgroup="mpTrxPopup" style="width:145px"/>
                                        <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                                        <div class="divListAutoCompleteResultBox">
                                            <div class="divListAutoCompleteResult">
                                            </div>
                                        </div>
                                    </div>
                                    <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                        <div>
                                            ${EmployeeName} (<b>${EmployeeCode}</b>)
                                            <input type='hidden' value='${EmployeeName}' class='hdnAutoCompleteRowText'/>
                                            <input type='hidden' value='${EmployeeID}' class='hdnAutoCompleteRowValue'/>
                                        </div>
                                    </script1>
                                </div>
                            </div>
                        </td>
                        <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
                    </tr>
                </table>
            </td>
        </tr>
    </script>
                
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
                            <input type="hidden" id="hdnEmployeeCoordinatorID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacEmployeeCoordinator" ClientInstanceName="tacEmployeeCoordinator" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                SearchFields="EmployeeName,EmployeeCode" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeCoordinatorButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacEmployeeCoordinatorValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Parent")%></label></td>
                        <td>
                            <input type="hidden" id="hdnParent" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacParent" ClientInstanceName="tacParent" MethodName="GetTeamDtList" GetFilterExpressionFunction="onGetParentFilterExpression"
                                SearchFields="Position" TextField="Position" ValueField="ProjectOrganizationID" SearchText="${Position}" OrderByExpression="DisplayOrder">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacParentButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacParentValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox ID="chkIsHeader" runat="server" /><%=GetLabel("Header")%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox ID="chkIsProjectAdmin" runat="server" /><%=GetLabel("Project Admin")%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:CheckBox ID="chkIsAllowAddTeam" runat="server" /><%=GetLabel("Bisa Tambah Tim")%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Member")%></span><br /></td>
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
                            <asp:TemplateField HeaderStyle-Width="200px" >
                                <HeaderTemplate>
                                    <div style="padding-left:3px">
                                        <%=GetLabel("Jabatan / Posisi")%>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style='margin-left:<%# Eval("Level") %>0px;'><%# Eval("Position") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeCoordinatorName" HeaderText="Koordinator" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="ListEmployeeName" HeaderText="Member" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("ProjectOrganizationID") %>" bindingfield="ProjectOrganizationID" />
                                    <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                    <input type="hidden" value="<%#Eval("IsHeader") %>" bindingfield="IsHeader" />
                                    <input type="hidden" value="<%#Eval("IsAllowAddTeam") %>" bindingfield="IsAllowAddTeam" />
                                    <input type="hidden" value="<%#Eval("IsProjectAdmin") %>" bindingfield="IsProjectAdmin" />
                                    <input type="hidden" value="<%#Eval("ParentID") %>" bindingfield="ParentID" />
                                    <input type="hidden" value="<%#Eval("ParentName") %>" bindingfield="ParentName" />
                                    <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                    <input type="hidden" value="<%#Eval("EmployeeCoordinatorID") %>" bindingfield="EmployeeCoordinatorID" />
                                    <input type="hidden" value="<%#Eval("EmployeeCoordinatorName") %>" bindingfield="EmployeeCoordinatorName" />
                                    <input type="hidden" value="<%#Eval("ListEmployeeID") %>" bindingfield="ListEmployeeID" />
                                    <input type="hidden" value="<%#Eval("ListEmployeeName") %>" bindingfield="ListEmployeeName" />
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

