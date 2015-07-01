<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ProjectTaskList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');
            $('#trScheduledTask').hide();
            $('#<%=chkIsScheduled.ClientID %>').prop('checked', false);

            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#divTransactionAdd').click(function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtProjectTaskCode.ClientID %>').val('');
                $('#<%=txtProjectTaskName.ClientID %>').val('');
                tacTeamDt.setValue('');
                tacTeamDt.setText('');
                Methods.getObject('GetvTeamDtList', onGetTeamDtFilterExpression(), function (result) {
                    if (result != null) {
                        tacTeamDt.setValue(result.TeamDtID);
                        tacTeamDt.setText(result.Position);
                        entityToControlTeamDt(result);
                    }
                });
                cboPriority.SetValue('DT002^002');
                $('#<%=txtRemarks.ClientID %>').val('');
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function () {
                cbpProcess.PerformCallback('save');
            });
        });

        $('#<%=txtScheduledStartDate.ClientID %>').die('change');
        $('#<%=txtScheduledStartDate.ClientID %>').live('change', function () {
            var val = $(this).val();
            if (val != "0" && val != "")
                cboScheduledDay.SetValue("0");
        });

        function OnScheduledDayChanged() {
            var val = cboScheduledDay.GetValue();
            if (val != "0")
                $('#<%=txtScheduledStartDate.ClientID %>').val("0");
        }

        $('#<%=chkIsScheduled.ClientID %>').die('change');
        $('#<%=chkIsScheduled.ClientID %>').live('change',function () {
            var isChecked = $(this).is(":checked");
            if (isChecked) {
                $('#trScheduledTask').show();
            }
            else {
                $('#trScheduledTask').hide();
            }
        });

        //#region Popup
        $('#chkSelectAllEmail').die('change');
        $('#chkSelectAllEmail').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        $('.lblAssign').die('click');
        $('.lblAssign').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            $('#<%=hdnPopupID.ClientID %>').val(id);
            pcProjectTask.Show();
        });

        $('#divTransactionAddPopup').die('click');
        $('#divTransactionAddPopup').live('click',function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            tacEmployeeCoordinator.setValue('');
            tacEmployeeCoordinator.setText('');
            $('#<%=chkIsAllowChangeStatus.ClientID %>').attr('checked', false);
            $('#entryDetailContainerPopup').show();
        });
        
        $('#btnCancelPopup').die('click');
        $('#btnCancelPopup').live('click',function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').die('click');
        $('#btnSavePopup').live('click',function () {
            cbpProcessPopup.PerformCallback('save');
        });

        $('.divDetailPopupEdit').die('click');
        $('.divDetailPopupEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.AssigneeID);
            tacEmployeeCoordinator.setValue(entity.AssigneeID);
            tacEmployeeCoordinator.setText(entity.EmployeeName);
            if (entity.IsAllowChangeStatus == 'True') {
                $('#<%=chkIsAllowChangeStatus.ClientID %>').attr('checked', true);
            } else {
                $('#<%=chkIsAllowChangeStatus.ClientID %>').attr('checked', false);
            }

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
            filterExpression += " AND (EmployeeID IN (SELECT EmployeeID FROM TeamDtMember WHERE TeamDtID = " + $('#<%=hdnTeamDtID.ClientID %>').val() + ") OR " +
                                "EmployeeID = (SELECT EmployeeCoordinatorID FROM TeamDt WHERE TeamDtID = " + $('#<%=hdnTeamDtID.ClientID %>').val() + ") OR " +
                                "EmployeeID IN (SELECT EmployeeCoordinatorID FROM TeamDt WHERE ReportTo = " + $('#<%=hdnTeamDtID.ClientID %>').val() + ") OR " +
                                "EmployeeID IN (SELECT EmployeeID FROM TeamDtMember WHERE TeamDtID IN (SELECT TeamDtID FROM TeamDt WHERE ReportTo = " + $('#<%=hdnTeamDtID.ClientID %>').val() + "))) AND " +
                                "EmployeeID NOT IN (SELECT AssigneeID FROM MemberTask WHERE ProjectTaskID = " + $('#<%=hdnPopupID.ClientID %>').val() + ") AND " +
                                "EmployeeID NOT IN (SELECT ISNULL(OwnerID,0) FROM ProjectTask WHERE ProjectTaskID = " + $('#<%=hdnPopupID.ClientID %>').val() + ")";
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

        $('.lblFile').die('click');
        $('.lblFile').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            $('#<%=hdnPopupID.ClientID %>').val(id);
            var url = ResolveUrl('~/Program/Proses/ProjectTask/ProjectTaskFileCtl.ascx');
            openUserControlPopup(url, id, 'File', 900, 500);
        });
        //#endregion

        $('.lblProjectChild').die('click');
        $('.lblProjectChild').live('click', function () {
            var url = ResolveUrl('~/Program/Proses/ProjectTask/ProjectTaskStructureCtl.ascx');
            var id = $('#<%=hdnEntryID.ClientID %>').val();
            var taskCode = $('#<%=txtProjectTaskCode.ClientID %>').val();
            var taskName = $('#<%=txtProjectTaskName.ClientID %>').val();
            var projectID = cboProject.GetValue();
            var param = id + '|' + taskCode + '|' + taskName + '|' + projectID;
            openUserControlPopup(url, param, 'Project Child', 900, 500);
        });

        $('.divDetailEdit').die('click');
        $('.divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            $('#<%=txtProjectTaskCode.ClientID %>').val(entity.ProjectTaskCode);
            $('#<%=txtProjectTaskCode.ClientID %>').attr('readonly', true);
            $('#<%=txtProjectTaskName.ClientID %>').val(entity.ProjectTaskName);
            $('#<%=txtStartDate.ClientID %>').val(entity.StartDateInDatePicker);
            $('#<%=txtEndDate.ClientID %>').val(entity.EndDateInDatePicker);
            $('#<%=txtStartTime.ClientID %>').val(entity.StartTime);
            $('#<%=txtEndTime.ClientID %>').val(entity.EndTime);
            tacTeamDt.setValue(entity.TeamDtID);
            tacTeamDt.setText(entity.Position);
            entityToControlTeamDt(entity);
            cboPriority.SetValue(entity.GCProjectTaskPriority);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            if (entity.ScheduledTaskID != "0") {
                $('#<%=txtStartDate.ClientID %>').val(entity.ScheduleTaskStartDateInDatePicker);
                $('#<%=txtEndDate.ClientID %>').val(entity.ScheduleTaskEndDateInDatePicker);
                $('#<%=chkIsScheduled.ClientID %>').prop('checked', true);
                $('#<%=chkIsScheduled.ClientID %>').prop('disabled', 'true');
                $('#<%=txtScheduledStartDate.ClientID %>').val(entity.RepeatedDate);
                cboScheduledDay.SetValue(entity.RepeatedDay);
                $('#trScheduledTask').show();
            }
            else {
                $('#<%=chkIsScheduled.ClientID %>').prop('checked', false);
                $('#<%=chkIsScheduled.ClientID %>').removeProp('disabled');
                $('#trScheduledTask').hide();
            }

            $('#entryDetailContainer').show();
        });

        $('.divDetailDelete').die('click');
        $('.divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            cbpProcess.PerformCallback('delete');
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        //#region TeamDt
        function onGetTeamDtFilterExpression() {
            var filterExpression = "ProjectID = " + cboProject.GetValue() + " AND IsDeleted = 0";
            if ($('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() != "0")
                filterExpression += " AND ((EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID1 LIKE '%;" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + ";%') OR" +
                                    " ReportTo = (SELECT TeamDtID FROM TeamDt WHERE EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + "))";
            return filterExpression;
        }

        function onTacTeamDtButtonSearchClick() {
            openSearchDialog('teamdt', onGetTeamDtFilterExpression(), function (value) {
                var filterExpression = onGetTeamDtFilterExpression() + " AND TeamDtID = " + value;
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
                    if(result != null)
                        entityToControlTeamDt(result);
                    else
                        entityToControlTeamDt(null);
                });
            } else {
                entityToControlTeamDt(null);
            }
        }

        function entityToControlTeamDt(result) {
            if (result != null)
                $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);        
            else
                $('#<%=hdnTeamDtID.ClientID %>').val(null);
        }
        //#endregion

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCboProjectChanged() {
            showLoadingPanel();
            cbpView.PerformCallback('refresh');
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
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
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value=""/>
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Project") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboProject" ClientInstanceName="cboProject" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){onCboProjectChanged()}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 200px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Kode")%></label></td>
                                    <td><asp:TextBox ID="txtProjectTaskCode" Width="100px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtProjectTaskName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeamDt" ClientInstanceName="tacTeamDt" MethodName="GetvTeamDtList" GetFilterExpressionFunction="onGetTeamDtFilterExpression"
                                            SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="<b>${Position}</b>" OrderByExpression="TeamDtID">
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
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsScheduled" Text="Kegiatan Terjadwal" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtStartDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                                <td style="width:10px; text-align:center">&nbsp;</td>
                                                <td><asp:TextBox ID="txtStartTime" CssClass="thCenter" Width="70px" runat="server"/></td>
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
                                <tr id="trScheduledTask">
                                    <td class="tdLabel"></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td><label><%=GetLabel("Tanggal") %></label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtScheduledStartDate" Width="50px" CssClass="number" runat="server" />
                                                            </td>
                                                            <td></td>
                                                            <td><label><%=GetLabel("Hari") %></label></td>
                                                            <td>
                                                                <dxe:ASPxComboBox runat="server" ID="cboScheduledDay" ClientInstanceName="cboScheduledDay" Width="70px">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){OnScheduledDayChanged()}" />
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><label class="lblLink lblProjectChild"><%=GetLabel("Project Child")%></label></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="ProjectTaskCode" HeaderText="Kode" HeaderStyle-Width="100px"/>
                                <asp:BoundField DataField="ProjectTaskName" HeaderText="Nama" HeaderStyle-Width="200px"/>
                                <asp:BoundField DataField="Position" HeaderText="Bagian" HeaderStyle-Width="150px"/>
                                <asp:BoundField DataField="StartDateInString" HeaderText="Mulai" HeaderStyle-Width="100px"/>
                                <asp:BoundField DataField="EndDateInString" HeaderText="Deadline" HeaderStyle-Width="100px"/>
                                <asp:TemplateField HeaderText="Keterangan">
                                    <ItemTemplate>
                                        <%#Eval("CustomRemarks")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProjectTaskPriority" HeaderText="Prioritas" HeaderStyle-Width="100px"/>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="File" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="lblLink lblFile" id="lblFile"><%=GetLabel("File")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Assign" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="lblLink lblAssign" id="lblAssign" runat="server"></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete" id="divDetailDelete" runat="server"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit" id="divDetailEdit" runat="server"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("ProjectTaskID") %>" bindingfield="ProjectTaskID" />
                                        <input type="hidden" value="<%#Eval("ScheduledTaskID") %>" bindingfield="ScheduledTaskID" />
                                        <input type="hidden" value="<%#Eval("RepeatedDate") %>" bindingfield="RepeatedDate" />
                                        <input type="hidden" value="<%#Eval("RepeatedDay") %>" bindingfield="RepeatedDay" />
                                        <input type="hidden" value="<%#Eval("ProjectTaskCode") %>" bindingfield="ProjectTaskCode" />
                                        <input type="hidden" value="<%#Eval("ProjectTaskName") %>" bindingfield="ProjectTaskName" />
                                        <input type="hidden" value="<%#Eval("TeamDtID") %>" bindingfield="TeamDtID" />
                                        <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                        <input type="hidden" value="<%#Eval("ScheduleTaskStartDateInDatePicker") %>" bindingfield="ScheduleTaskStartDateInDatePicker" />
                                        <input type="hidden" value="<%#Eval("ScheduleTaskEndDateInDatePicker") %>" bindingfield="ScheduleTaskEndDateInDatePicker" />
                                        <input type="hidden" value="<%#Eval("StartDateInDatePicker") %>" bindingfield="StartDateInDatePicker" />
                                        <input type="hidden" value="<%#Eval("StartTime") %>" bindingfield="StartTime" />
                                        <input type="hidden" value="<%#Eval("EndDateInDatePicker") %>" bindingfield="EndDateInDatePicker" />
                                        <input type="hidden" value="<%#Eval("EndTime") %>" bindingfield="EndTime" />
                                        <input type="hidden" value="<%#Eval("GCProjectTaskPriority") %>" bindingfield="GCProjectTaskPriority" />
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
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
    <dx:ASPxPopupControl ID="pcProjectTask" runat="server" ClientInstanceName="pcProjectTask"
        height="150px" HeaderText="Project Task" AllowDragging="True" CloseAction="CloseButton" width="800px" Modal="True" PopupAction="None" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseButtonImage-Width="0">
        <ClientSideEvents Shown="function(s,e){showLoadingPanel(); cbpViewPopup.PerformCallback();}" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" ID="pccc1">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="width:100%;">
                                <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                                    ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                            <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                                <input type="hidden" runat="server" id="hdnPopupID" />
                                                <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
                                                <div class="divTransactionEntry">
                                                    <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                                                    <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
                                                        <fieldset id="fsTrxPopup" style="margin:0"> 
                                                            <input type="hidden" id="Hidden1" runat="server" value="" />
                                                            <table id="tblEntryPopup">
                                                                <colgroup>
                                                                    <col style="width:150px"/>
                                                                    <col />
                                                                </colgroup>
                                                                <tr>
                                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Member")%></label></td>
                                                                    <td>
                                                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacEmployeeCoordinator" ClientInstanceName="tacEmployeeCoordinator" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
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
                                                <asp:GridView ID="grdPopupView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input id="chkSelectAllEmail" type="checkbox" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox CssClass="chkIsSelected" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <input type="button" value="Email" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>