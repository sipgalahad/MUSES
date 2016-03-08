<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RProjectTaskDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectTaskDtEntryCtl" %>

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
            $('#<%=txtProjectTaskName.ClientID %>').val('');
            $('#<%=txtRemarks.ClientID %>').val('');
            tacOrganizationCoordinator.setValue('');
            tacOrganizationCoordinator.setText('');
            $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val('');
            cboPriority.SetValue('');

            idxOrganization = 0;
            $('.trOrganizationDt').each(function () {
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
                $('.tacOrganization').each(function () {
                    if (result != "")
                        result += ',';
                    result += $(this).find('.hdnAutoCompleteValue').val();
                });
                $('#<%=hdnOrganizationSave.ClientID %>').val(result);
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
                $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
        $('#<%=txtProjectTaskName.ClientID %>').val(entity.ProjectTaskName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        tacOrganizationCoordinator.setValue(entity.OrganizationCoordinatorID);
        tacOrganizationCoordinator.setText(entity.OrganizationCoordinatorName);
        $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val(entity.OrganizationCoordinatorID);
        cboPriority.SetValue(entity.GCProjectTaskPriority);

        idxOrganization = 0;
        $('.trOrganizationDt').each(function () {
            $(this).remove();
        });

        if (entity.ListOrganizationID != '') {
            var lstOrganizationID = entity.ListOrganizationID.split(',');
            var lstOrganizationName = entity.ListOrganizationName.split(', ');
            for (var i = 0; i < lstOrganizationID.length; ++i) {
                $('#divEntryDtAdd').click();

                $tr = $('.trOrganizationDt').last();
                $tacOrganization = $tr.find('.tacOrganization');
                $tacOrganization.find('.hdnAutoCompleteValue').val(lstOrganizationID[i]);
                $tacOrganization.find('.hdnAutoCompleteText').val(lstOrganizationName[i]);
                $tacOrganization.find('.txtAutoComplete').val(lstOrganizationName[i]);
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

    //#region Organization
    window.onGetOrganizationFilterExpression = function() {
        var filterExpression = "<%=OnGetOrganizationFilterExpression() %>";
        return filterExpression;
    }

    function onTacOrganizationCoordinatorButtonSearchClick() {
        openSearchDialog('rprojectorganization', onGetOrganizationFilterExpression(), function (value) {
            var filterExpression = onGetOrganizationFilterExpression() + " AND ProjectOrganizationID = '" + value + "'";
            Methods.getObject('GetvRProjectOrganizationList', filterExpression, function (result) {
                if (result != null) {
                    tacOrganizationCoordinator.setValue(result.ProjectOrganizationID);
                    tacOrganizationCoordinator.setText(result.Position);
                    $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val(result.ProjectOrganizationID);
                }
                else {
                    tacOrganizationCoordinator.setValue('');
                    tacOrganizationCoordinator.setText('');
                    $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val('');
                }
            });
        });

    }

    function onTacOrganizationCoordinatorValueChanged() {
        $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val(tacOrganizationCoordinator.getValue());
    }
    //#endregion

    var idxOrganization = 0;
    $('#divEntryDtAdd').click(function () {
        $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
        $newTr = $newTr.replace(/\$\{idx}/g, idxOrganization);
        $newTr = $($newTr);
        $newTr.insertBefore($('#trSaveEntryPopup'));

        var tempHelper = new CodeXClientAutoCompleteHelper();
        tempHelper.init("Organization" + idxOrganization, "ProjectOrganizationName", "GetvRProjectOrganizationList", "", "onGetOrganizationFilterExpression", "ProjectOrganizationID");
        tempHelper.setClientSideEvents(onOrganizationIDValueChanged);
        tempHelper.initializeControl();
        idxOrganization++;
    });

    function onOrganizationIDValueChanged($s) {
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

    $('.tacOrganization .btnAutoCompleteSearchMore').die('click');
    $('.tacOrganization .btnAutoCompleteSearchMore').live('click', function () {
        $tacTr = $(this).closest('tr');
        openSearchDialog('rprojectorganization', onGetOrganizationFilterExpression(), function (value) {
            var filterExpression = onGetOrganizationFilterExpression() + " AND ProjectOrganizationID = '" + value + "'";
            Methods.getObject('GetvRProjectOrganizationList', filterExpression, function (result) {
                $tacCOA = $tacTr.find('.tacOrganization');
                if (result != null) {
                    $tacCOA.find('.hdnAutoCompleteValue').val(result.ProjectOrganizationID);
                    $tacCOA.find('.hdnAutoCompleteText').val(result.Position);
                    $tacCOA.find('.txtAutoComplete').val(result.Position);
                }
                else {
                    $tacCOA.find('.hdnAutoCompleteValue').val('');
                    $tacCOA.find('.hdnAutoCompleteText').val('');
                    $tacCOA.find('.txtAutoComplete').val('');
                }
                onOrganizationIDValueChanged($tacCOA.find('.txtAutoComplete'));
            });
            //var trIdx = $('.trPrescriptionEntry').index($tacTr);
            //if (trIdx == $('.trPrescriptionEntry').length - 1)
            //    addEntityRowPrescription();
            $tacTr = null;
        });
    });
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnOrganizationSave" value="" runat="server" />
    <script id="tmplEntityDt" type="text/x-jquery-tmpl">
        <tr class="trOrganizationDt">
            <td>&nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div id="Organization${idx}" class="tacOrganization">
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
                                            ${ProjectOrganizationName}
                                            <input type='hidden' value='${ProjectOrganizationName}' class='hdnAutoCompleteRowText'/>
                                            <input type='hidden' value='${ProjectOrganizationID}' class='hdnAutoCompleteRowValue'/>
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
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelompok Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tugas") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtProjectTaskName" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Koordinator")%></label></td>
                        <td>
                            <input type="hidden" id="hdnOrganizationCoordinatorID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacOrganizationCoordinator" ClientInstanceName="tacOrganizationCoordinator" MethodName="GetvRProjectOrganizationList" GetFilterExpressionFunction="onGetProjectOrganizationFilterExpression"
                                SearchFields="ProjectOrganizationName" TextField="ProjectOrganizationName" ValueField="ProjectOrganizationID" SearchText="${ProjectOrganizationName}" OrderByExpression="ProjectOrganizationName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacOrganizationCoordinatorButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacOrganizationCoordinatorValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Prioritas")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboPriority" ClientInstanceName="cboPriority" Width="200px" /></td>
                    </tr>
                    <tr valign="top" style="padding-top: 5px">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Bagian")%></span><br /></td>
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
                            <asp:BoundField DataField="ProjectTaskName" HeaderText="Tugas" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="OrganizationCoordinatorName" HeaderText="Koordinator" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="ListOrganizationName" HeaderText="Bagian Yg Terlibat" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("ProjectTaskID") %>" bindingfield="ProjectTaskID" />
                                    <input type="hidden" value="<%#Eval("ProjectTaskName") %>" bindingfield="ProjectTaskName" />
                                    <input type="hidden" value="<%#Eval("OrganizationCoordinatorID") %>" bindingfield="OrganizationCoordinatorID" />
                                    <input type="hidden" value="<%#Eval("OrganizationCoordinatorName") %>" bindingfield="OrganizationCoordinatorName" />
                                    <input type="hidden" value="<%#Eval("ListOrganizationID") %>" bindingfield="ListOrganizationID" />
                                    <input type="hidden" value="<%#Eval("ListOrganizationName") %>" bindingfield="ListOrganizationName" />
                                    <input type="hidden" value="<%#Eval("GCProjectTaskPriority") %>" bindingfield="GCProjectTaskPriority" />
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

