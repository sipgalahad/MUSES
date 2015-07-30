<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToDoListCalendarEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ToDoListCalendarEntryCtl" %>

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
        setDatePicker('<%=txtStartDateDt.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');

        $('#btnSave').click(function (evt) {
            if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                cbpProcessPopup.PerformCallback('save');
            }
        });
    });
    
    //#region TeamDt
    window.onGetTeamDtFilterExpression = function() {
        var filterExpression = "ProjectID = " + cboProject.GetValue() + " AND IsDeleted = 0";
        if ($('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() != "0")
            filterExpression += " AND (EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID1 LIKE '%;" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + ";%')";
        return filterExpression;
    }

    function onTacTeamDtButtonSearchClick() {
        openSearchDialog('teamdt', onGetTeamDtFilterExpression(), function (value) {
            var filterExpression = "TeamDtID = " + value;
            Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                if (result != null) {
                    tacTeamDt.setValue(result.TeamDtID);
                    tacTeamDt.setText(result.Position);
                    entityToControlTeamDt(result);
                }
                else {
                    tacTeamDt.setValue('');
                    tacTeamDt.setText('');
                    entityToControlTeamDt(null);
                }
            });
        });
    }

    function onTacTeamDtValueChanged() {
        var id = tacTeamDt.getValue();
        if (id != '') {
            var filterExpression = "TeamDtID = " + id;
            Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                if (result != null)
                    entityToControlTeamDt(result);
                else
                    entityToControlTeamDt(null);
            });
        } else {
            entityToControlTeamDt(null);
        }
    }

    function entityToControlTeamDt(result) {
        if (result != null) {
            $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
        } else {
            $('#<%=hdnTeamDtID.ClientID %>').val('');
        }
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

<div>
    <input type="hidden" id="hdnEntryID" value="" runat="server" />
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value=""/>
    <fieldset id="fsTrx" style="margin:0"> 
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:160px"/>
                <col/>
            </colgroup>
                <tr>
                    <td class="tdLabel" style="width:100px;"><%=GetLabel("Project") %></td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="cboProject" ClientInstanceName="cboProject" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                    <td><asp:TextBox ID="txtProjectTaskCode" Width="100px" runat="server" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                    <td><asp:TextBox ID="txtProjectTaskName" Width="300px" runat="server" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                    <td>
                        <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeamDt" ClientInstanceName="tacTeamDt" MethodName="GetvTeamDtList" GetFilterExpressionFunction="onGetTeamDtFilterExpression"
                            SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="${Position}" OrderByExpression="Position">
                            <ClientSideEvents ButtonSearchClick="function(){ onTacTeamDtButtonSearchClick(); }"
                                ValueChanged="function(){ onTacTeamDtValueChanged(); }" />
                        </cdx:CodeXAutoCompleteTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Prioritas")%></label></td>
                    <td><dxe:ASPxComboBox runat="server" ID="cboPriority" ClientInstanceName="cboPriority" Width="200px" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:TextBox ID="txtStartDateDt" Width="120px" runat="server" CssClass="datepicker" /></td>
                                <td style="width:10px; text-align:center">&nbsp;</td>
                                <td><asp:TextBox ID="txtStartTimeDt" CssClass="thCenter" Width="70px" runat="server"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Selesai")%></label></td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:TextBox ID="txtEndDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                <td style="width:10px; text-align:center">&nbsp;</td>
                                <td><asp:TextBox ID="txtEndTime" CssClass="thCenter" Width="70px" runat="server"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                    <td><asp:TextBox runat="server" ID="txtRemarksDt" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td> 
                        <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                    </td>
                </tr>
        </table>
    </fieldset>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

