<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ToDoList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ToDoList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
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
            setProjectTaskRemarks();

            $('#divTransactionAdd').click(function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtProjectTaskCode.ClientID %>').val('');
                $('#<%=txtProjectTaskName.ClientID %>').val('');

                Methods.getObject('GetvTeamDtList', onGetTeamDtFilterExpression(), function (result) {
                    if (result != null) {
                        tacTeamDt.setValue(result.TeamDtID);
                        tacTeamDt.setText(result.Position);
                        $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
                    }
                });

                cboPriority.SetValue('<%=GetProjectTaskLowPriority() %>');
                $('#<%=txtRemarks.ClientID %>').val('');
                $('#<%=txtProjectTaskCode.ClientID %>').attr('readonly', false);
                cboTaskType.SetEnabled(true);
                $('#<%=chkIsScheduled.ClientID %>').removeProp('disabled');
            });

            $('#btnEntrySave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    var taskType = cboTaskType.GetValue();
                    var value = cboTaskType.GetValue();
                    if (value == '<%=GetFLoatingTaskType() %>') {
                        cbpProcess.PerformCallback('save1');
                    } else {
                        cbpProcess.PerformCallback('save');
                    }
                }
            });

            $('#<%=chkIsShowClosed.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=chkIsScheduled.ClientID %>').change(function () {
                var isChecked = $(this).is(":checked");
                if (isChecked) {
                    $('#trScheduledTask').show();
                }
                else {
                    $('#trScheduledTask').hide();
                }
            });
        });

        $('.lblLink').die('click');
        $('.lblLink').live('click', function () {
            var url = ResolveUrl('~/Program/Proses/ToDoList/ProjectTaskLogEntryCtl.ascx');
            var id = $(this).closest('tr').find('.keyField').html();
            openUserControlPopup(url, id, 'Log Entry', 900, 500);
        });

        $('.btnSave').die('click');
        $('.btnSave').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var idx = $row.find('.hdnItemIndex').val();
            cboTaskStatus = eval('cboTaskStatus' + idx);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            $('#<%=hdnStatus.ClientID %>').val(cboTaskStatus.GetValue());
            cbpProcess.PerformCallback('changestatus');
        });

        $('.detailEdit').die('click');
        $('.detailEdit').live('click', function () {
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
            $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
            cboPriority.SetValue(entity.GCProjectTaskPriority);
            cboTaskType.SetValue(entity.GCProjectTaskType);
            cboTaskType.SetEnabled(false);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            $('#trIsScheduled').show();
            
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

            $('#trStartDate').show();
            $('#trEndDate').show();
            $('#trPriority').show();
        });

        $('.detailDelete').die('click');
        $('.detailDelete').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            cbpProcess.PerformCallback('delete');
        });

        $('.detailEdit1').die('click');
        $('.detailEdit1').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            $('#<%=txtProjectTaskCode.ClientID %>').val(entity.ProjectTaskCode);
            $('#<%=txtProjectTaskCode.ClientID %>').attr('readonly', true);
            $('#<%=txtProjectTaskName.ClientID %>').val(entity.ProjectTaskName);
            tacTeamDt.setValue(entity.TeamDtID);
            tacTeamDt.setText(entity.Position);
            $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
            cboTaskType.SetValue(entity.GCProjectTaskType);
            cboTaskType.SetEnabled(false);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            $('#trIsScheduled').hide();
            $('#trScheduledTask').hide();
            $('#trStartDate').hide();
            $('#trEndDate').hide();
            $('#trPriority').hide();
        });

        $('.detailDelete1').die('click');
        $('.detailDelete1').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
            cbpProcess.PerformCallback('delete1');
        });

        $('#<%=txtScheduledStartDate.ClientID %>').die('change');
        $('#<%=txtScheduledStartDate.ClientID %>').live('change', function () {
            var val = $(this).val();
            if (val != "0" && val != "")
                cboScheduledDay.SetValue("0");
        });

        function OnScheduledDayChanged() {
            var val = cboScheduledDay.GetValue();
            if(val != "0")
                $('#<%=txtScheduledStartDate.ClientID %>').val("0");
        }

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
                filterExpression += " AND (EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID1 LIKE '%;" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + ";%')";
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
            if (result != null)
                $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
            else
                $('#<%=hdnTeamDtID.ClientID %>').val(null);
        }
        //#endregion

        //#region TeamDt1
        function onGetTeamDt1FilterExpression() {
            var filterExpression = "ProjectID = " + cboProject.GetValue() + " AND IsDeleted = 0";
            if ($('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() != "0")
                filterExpression += " AND (EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID LIKE '%" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + "%')";
            return filterExpression;
        }

        function onTacTeamDt1ButtonSearchClick() {
            openSearchDialog('teamdt', onGetTeamDt1FilterExpression(), function (value) {
                var filterExpression = onGetTeamDt1FilterExpression() + " AND TeamDtID = " + value;
                Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeamDt1.setValue(result.TeamDtID);
                        tacTeamDt1.setText(result.Position);
                    }
                    else {
                        tacTeamDt1.setValue('');
                        tacTeamDt1.setText('');
                    }
                });
            });
        }

        function onTacTeamDt1ValueChanged() {
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
                setProjectTaskRemarks();
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        //#region Paging1
        var pageCount1 = parseInt('<%=PageCount1 %>');
        var rowCount1 = parseInt('<%=RowCount1 %>');
        var rowCountPerPage1 = parseInt('<%=RowCountPerPage1 %>');
        var currPage1 = parseInt('<%=CurrPage1 %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries1'), rowCount1, currPage1, rowCountPerPage1);
            setPaging($("#paging1"), pageCount, function (page) {
                cbpView1.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries1'), rowCount1, page, rowCountPerPage1);
            }, null, currPage);
        });

        function onCbpView1EndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                
                if (pageCount > 0)
                    $('#<%=grdView1.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries1'), rowCount1, currPage1, rowCountPerPage1);
                setPaging($("#paging1"), pageCount, function (page) {
                    cbpView1.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries1'), rowCount1, page, rowCountPerPage1);
                });
                setProjectTaskRemarks1();
            }
            else
                $('#<%=grdView1.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCboProjectChanged() {
            showLoadingPanel();
            cbpView.PerformCallback('refresh');
        }

        function onCboStatusChanged() {
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
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            } 
            else if (param[0] == 'save1') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    cbpView1.PerformCallback('refresh');
                }
            } 
            else if (param[0] == 'delete1') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView1.PerformCallback('refresh');
            } 
        }

        function setProjectTaskRemarks() {
            var open = parseInt($('#<%=hdnOpen.ClientID %>').val());
            var inProgress = parseInt($('#<%=hdnInProgress.ClientID %>').val());
            var closed = parseInt($('#<%=hdnClosed.ClientID %>').val());
            var total = parseInt($('#<%=hdnTotalTask.ClientID %>').val());
            $('#tdOpen').text(open + " / " + total);
            $('#tdInProgress').text(inProgress + " / " + total);
            $('#tdClosed').text(closed + " / " + total);

            var finish = closed / total * 100;
            var pending = (open + inProgress) / total * 100;
            $('#tdFinish').text(Math.floor(finish) + "%");
            $('#tdPending').text(Math.ceil(pending) + "%");
        }

        function setProjectTaskRemarks1() {
            var open = parseInt($('#<%=hdnOpen1.ClientID %>').val());
            var inProgress = parseInt($('#<%=hdnInProgress1.ClientID %>').val());
            var closed = parseInt($('#<%=hdnClosed1.ClientID %>').val());
            var total = parseInt($('#<%=hdnTotalTask1.ClientID %>').val());
            $('#tdOpen').text(open + " / " + total);
            $('#tdInProgress').text(inProgress + " / " + total);
            $('#tdClosed').text(closed + " / " + total);

            var finish = closed / total * 100;
            var pending = (open + inProgress) / total * 100;
            $('#tdFinish').text(Math.floor(finish) + "%");
            $('#tdPending').text(Math.ceil(pending) + "%");
        }

        function onCboTaskTypeChanged() {
            var value = cboTaskType.GetValue();
            if (value == '<%=GetFLoatingTaskType() %>') {
                $('#trStartDate').hide();
                $('#trEndDate').hide();
                $('#trPriority').hide();
                $('#trScheduledTask').hide();
                $('#trIsScheduled').hide();
            } else {
                $('#trStartDate').show();
                $('#trEndDate').show();
                $('#trPriority').show();
                $('#trIsScheduled').show();
            }
        }

    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnStatus" runat="server" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value=""/>
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <table width="100%" style="margin-bottom:10px;">
        <tr>
            <td valign="top">
                <table width="50%">
                    <tr>
                        <td class="tdLabel" style="width:100px;"><%=GetLabel("Project") %></td>
                        <td>
                            <dxe:ASPxComboBox runat="server" ID="cboProject" ClientInstanceName="cboProject" Width="200px">
                                <ClientSideEvents ValueChanged="function(s,e){onCboProjectChanged()}" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="width:100px;"><%=GetLabel("Status") %></td>
                        <td>
                            <dxe:ASPxComboBox runat="server" ID="cboStatus" ClientInstanceName="cboStatus" Width="200px">
                                <ClientSideEvents ValueChanged="function(s,e){onCboStatusChanged()}" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:CheckBox runat="server" ID="chkIsShowClosed" Text="Show Closed Task" /></td>
                    </tr>
                </table>
            </td>
            <td valign="top" align="right">
                <div style="width:400px;height:80px; background-color:#F4F4F4; text-align:left; padding:3px;" id="divKeterangan" runat="server">
                    <div style="font-weight:bold;">Keterangan :</div>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="50%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td valign="top">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="100px;"/>
                                        <col width="7px;"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td>Open</td>
                                        <td align="center">:</td>
                                        <td id="tdOpen"></td>
                                    </tr>
                                    <tr>
                                        <td>In Progress</td>
                                        <td align="center">:</td>
                                        <td id="tdInProgress"></td>
                                    </tr>
                                    <tr>
                                        <td>Closed</td>
                                        <td align="center">:</td>
                                        <td id="tdClosed"></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="50%"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td align="center" style="font-weight:bold;">Finish</td>
                                        <td align="center" style="font-weight:bold;">Pending</td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size:x-large;" id="tdFinish"></td>
                                        <td align="center" style="font-size:x-large;" id="tdPending"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <table width="100%" cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="500px" />
                <col />
            </colgroup>
            <tr>
                <td style="border-right:1px solid; padding-right:3px;" valign="top">
                    <div>
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                    </div>
                    <div>
                        <fieldset id="fsTrx" style="margin: 0">
                            <input type="hidden" value="" id="hdnEntryID" runat="server" />
                            <table style="width: 100%">
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <colgroup>
                                                <col style="width: 200px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe")%></label></td>
                                                <td>
                                                    <dxe:ASPxComboBox runat="server" ID="cboTaskType" ClientInstanceName="cboTaskType" Width="200px">
                                                        <ClientSideEvents ValueChanged="function(s,e){onCboTaskTypeChanged()}" />
                                                    </dxe:ASPxComboBox>
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
                                                        SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="<b>${Position}</b>" OrderByExpression="TeamDtID">
                                                        <ClientSideEvents ButtonSearchClick="function(){ onTacTeamDtButtonSearchClick(); }"
                                                            ValueChanged="function(){ onTacTeamDtValueChanged(); }" />
                                                    </cdx:CodeXAutoCompleteTextBox>
                                                </td>
                                            </tr>
                                            <tr id="trPriority">
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Prioritas")%></label></td>
                                                <td><dxe:ASPxComboBox runat="server" ID="cboPriority" ClientInstanceName="cboPriority" Width="200px" /></td>
                                            </tr>
                                            <tr id="trIsScheduled">
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkIsScheduled" Text="Kegiatan Terjadwal" />
                                                </td>
                                            </tr>
                                            <tr id="trStartDate">
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
                                            <tr id="trEndDate">
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
                                                <td class="tdLabel">
                                                </td>
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
                                                <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                                <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td> 
                                                    <input type="button" id="btnEntrySave" class="btnWhite" value="Commit"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td style="border-left:1px solid; padding-left:3px;" valign="top">
                    <div style="height:500px; overflow-y:auto; overflow-x:hidden;">
                    <div>
                        <div style="font-weight:bold; font-size:medium;">Scheduled Task</div>
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                        position: relative; font-size: 0.95em;">
                                        <input type="hidden" id="hdnTotalTask" runat="server" />
                                        <input type="hidden" id="hdnOpen" runat="server" />
                                        <input type="hidden" id="hdnInProgress" runat="server" />
                                        <input type="hidden" id="hdnClosed" runat="server" />
                                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                            OnRowDataBound="grdView_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:TemplateField HeaderText="Nama" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <label class="lblLink"><%#:Eval("ProjectTaskName") %></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EndDateInString" HeaderText="Deadline" HeaderStyle-Width="90px" />
                                                <asp:BoundField DataField="ProjectTaskPriority" HeaderText="Prioritas" HeaderStyle-Width="70px"/>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <dxe:ASPxComboBox ID="cboTaskStatus" class="cboTaskStatus" Width="100px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="70px">
                                                    <ItemTemplate>
                                                        <input type="button" value="Simpan" class="btnSave" id="btnSave" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style='float:right;' class="divDetailDelete detailDelete" id="divDetailDelete" runat="server"></div>
                                                        <div style='float:right;margin-right:10px;' class="divDetailEdit detailEdit" id="divDetailEdit" runat="server"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
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
                                                        <input type="hidden" value="<%#Eval("GCProjectTaskType") %>" bindingfield="GCProjectTaskType" />
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
                    <div>
                        <div style="font-weight:bold; font-size:medium;">Floated Task</div>
                        <dxcp:ASPxCallbackPanel ID="cbpView1" runat="server" Width="100%" ClientInstanceName="cbpView1"
                            ShowLoadingPanel="false" OnCallback="cbpView1_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                EndCallback="function(s,e){ onCbpView1EndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <asp:Panel runat="server" ID="pnlView1" Style="width: 100%; margin-left: auto; margin-right: auto;
                                        position: relative; font-size: 0.95em;">
                                        <input type="hidden" id="hdnTotalTask1" runat="server" />
                                        <input type="hidden" id="hdnOpen1" runat="server" />
                                        <input type="hidden" id="hdnInProgress1" runat="server" />
                                        <input type="hidden" id="hdnClosed1" runat="server" />
                                        <asp:GridView ID="grdView1" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="ProjectTaskName" HeaderText="Nama" />
                                                <asp:BoundField DataField="StartDateInString" HeaderText="Tanggal" HeaderStyle-Width="100px"/>
                                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style='float:right;' class="divDetailDelete detailDelete1" id="divDetailDelete1" runat="server"></div>
                                                        <div style='float:right;margin-right:10px;' class="divDetailEdit detailEdit1" id="divDetailEdit1" runat="server"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                                        <input type="hidden" value="<%#Eval("ProjectTaskID") %>" bindingfield="ProjectTaskID" />
                                                        <input type="hidden" value="<%#Eval("ProjectTaskCode") %>" bindingfield="ProjectTaskCode" />
                                                        <input type="hidden" value="<%#Eval("ProjectTaskName") %>" bindingfield="ProjectTaskName" />
                                                        <input type="hidden" value="<%#Eval("TeamDtID") %>" bindingfield="TeamDtID" />
                                                        <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                                        <input type="hidden" value="<%#Eval("StartDateInDatePicker") %>" bindingfield="StartDateInDatePicker" />
                                                        <input type="hidden" value="<%#Eval("StartTime") %>" bindingfield="StartTime" />
                                                        <input type="hidden" value="<%#Eval("EndDateInDatePicker") %>" bindingfield="EndDateInDatePicker" />
                                                        <input type="hidden" value="<%#Eval("EndTime") %>" bindingfield="EndTime" />
                                                        <input type="hidden" value="<%#Eval("GCProjectTaskPriority") %>" bindingfield="GCProjectTaskPriority" />
                                                        <input type="hidden" value="<%#Eval("GCProjectTaskType") %>" bindingfield="GCProjectTaskType" />
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
                        <div class="imgLoadingGrdView" id="containerImgLoadingView1" >
                            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                        </div>
                        <div class="containerPaging">
                            <div class="divInformationNumEntries" id="informationNumEntries1"></div>
                            <div class="wrapperPaging">
                                <div id="paging1"></div>
                            </div>
                        </div>
                    </div>
                    </div>
                </td>
            </tr>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>    
    </div>
</asp:Content>
