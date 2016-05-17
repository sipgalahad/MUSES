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
        setDatePicker('<%=txtStartDate.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');
        setDatePicker('<%=txtDueDateEndDate.ClientID %>');

        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnIsAllowEdit.ClientID %>').val('1');
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtProjectTaskName.ClientID %>').val('');
            $('#<%=txtRemarks.ClientID %>').val('');
            tacOrganizationCoordinator.setValue($('#<%=hdnProjectOrganizationID.ClientID %>').val());
            tacOrganizationCoordinator.setText($('#<%=hdnPosition.ClientID %>').val());
            $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val($('#<%=hdnProjectOrganizationID.ClientID %>').val());
            cboPriority.SetValue('');
            cboStatus.SetSelectedIndex(0);
            cboDueDateType.SetSelectedIndex(0);

            $('#<%=txtStartDate.ClientID %>').val('');
            $('#<%=txtEndDate.ClientID %>').val('');
            $('#<%=txtDueDateEndDate.ClientID %>').val('');

            tacOrganizationCoordinator.setEnabled(true);
            cboPriority.SetEnabled(true);
            cboDueDateType.SetEnabled(true);
            $('#<%=txtProjectTaskName.ClientID %>').removeAttr('readonly');
            $('#<%=txtDueDateEndDate.ClientID %>').removeAttr('readonly');
            $('#<%=txtStartDate.ClientID %>').removeAttr('readonly');
            $('#trIsVerified').removeAttr('style');
            $('#<%=txtEndDate.ClientID %>').removeAttr('readonly');
            $('#<%=chkIsVerified.ClientID %>').removeAttr("disabled");

            $('#<%=chkIsVerified.ClientID %>').prop('checked', false);

            onCboDueDateTypeValueChanged();

            idxOrganization = 0;
            $('.trOrganizationDt').each(function () {
                $(this).remove();
            });

            $('#entryDetailContainerPopup').show();
        });

        $('#<%=chkIsShowAllTask.ClientID %>').change(function () {
            cbpViewPopup.PerformCallback('refresh');
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
        $('#<%=hdnIsAllowEdit.ClientID %>').val($row.find('.hdnIsAllowEdit').val());
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskID);
        $('#<%=txtProjectTaskName.ClientID %>').val(entity.ProjectTaskName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        tacOrganizationCoordinator.setValue(entity.OrganizationCoordinatorID);
        tacOrganizationCoordinator.setText(entity.OrganizationCoordinatorName);
        $('#<%=hdnOrganizationCoordinatorID.ClientID %>').val(entity.OrganizationCoordinatorID);
        cboPriority.SetValue(entity.GCProjectTaskPriority);
        cboStatus.SetValue(entity.GCProjectTaskStatus);
        cboDueDateType.SetValue(entity.GCDueDateType);

        $('#<%=chkIsVerified.ClientID %>').prop('checked', entity.IsVerified == 'True');

        if ($row.find('.hdnIsAllowEdit').val() == '0') {
            tacOrganizationCoordinator.setEnabled(false);
            cboPriority.SetEnabled(false);
            cboDueDateType.SetEnabled(false);
            $('#<%=txtProjectTaskName.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtDueDateEndDate.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtStartDate.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtEndDate.ClientID %>').attr('readonly', 'readonly');
            $('#trIsVerified').attr('style', 'display:none');
            $('#<%=chkIsVerified.ClientID %>').attr("disabled", true);
        }
        else {
            tacOrganizationCoordinator.setEnabled(true);
            cboPriority.SetEnabled(true);
            cboDueDateType.SetEnabled(true);
            $('#<%=txtProjectTaskName.ClientID %>').removeAttr('readonly');
            $('#<%=txtDueDateEndDate.ClientID %>').removeAttr('readonly');
            $('#<%=txtStartDate.ClientID %>').removeAttr('readonly');
            $('#trIsVerified').removeAttr('style');
            $('#<%=txtEndDate.ClientID %>').removeAttr('readonly');
            $('#<%=chkIsVerified.ClientID %>').removeAttr("disabled");
        }

        if (entity.GCDueDateType == '<%=OnGetDueDateNoDueDate() %>') {
            $('#<%=txtStartDate.ClientID %>').val('');
            $('#<%=txtEndDate.ClientID %>').val('');
            $('#<%=txtDueDateEndDate.ClientID %>').val('');
        }
        else {
            $('#<%=txtStartDate.ClientID %>').val(entity.StartDate);
            $('#<%=txtEndDate.ClientID %>').val(entity.EndDate);
            $('#<%=txtDueDateEndDate.ClientID %>').val(entity.EndDate);
        }

        onCboStatusValueChanged();
        onCboDueDateTypeValueChanged();

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

    $('#btnPopupRefresh').click(function () {
        cbpViewPopup.PerformCallback('refresh');
    });

    function onCboStatusValueChanged() {
        var value = cboStatus.GetValue();
        if (value == '<%=OnGetProjectTaskStatusClosed() %>' && $('#<%=hdnIsAllowEdit.ClientID %>').val() == '1') {
            $('#trIsVerified').removeAttr('style');
            $('#<%=chkIsVerified.ClientID %>').removeAttr("disabled");
        }
        else {
            $('#trIsVerified').attr('style', 'display:none');
            $('#<%=chkIsVerified.ClientID %>').attr("disabled", true);
        }
    }

    function onCboFilterStatusValueChanged() {
        if (cboFilterStatus.GetValue() == '1')
            $('#trFilterStatus').removeAttr('style');
        else
            $('#trFilterStatus').attr('style', 'display:none');
    }

    function onCboDueDateTypeValueChanged() {
        var value = cboDueDateType.GetValue();
        $('#trDueDateEndDate').attr('style', 'display:none');
        $('#trDueDateRange').attr('style', 'display:none');

        if (value == '<%=OnGetDueDateRange() %>')
            $('#trDueDateRange').removeAttr('style');
        else if (value == '<%=OnGetDueDateEndDate() %>')
            $('#trDueDateEndDate').removeAttr('style');
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

    $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
        $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
        $(this).addClass('selected');
        $('#<%=hdnProjectTaskID.ClientID %>').val($(this).find('.keyField').html());
        cbpViewPopup2.PerformCallback('refresh');
        cbpViewPopup3.PerformCallback('refresh');
        $('#divTransactionEntry2').show();
        $('#divTransactionEntry3').show();
    });

    //#region Log
    $(function () {
        setDatePicker('<%=txtLogDate.ClientID %>');
        $('#divTransactionAddPopup2').click(function () {
            $('#<%=hdnEntry2ID.ClientID %>').val('');
            var currentDate = new Date();
            var h = currentDate.getHours();
            var mnt = currentDate.getMinutes();
            var d = currentDate.getDate();
            var m = currentDate.getMonth() + 1;
            var y = currentDate.getFullYear();
            $('#<%=txtLogDate.ClientID %>').val('' + (d <= 9 ? '0' + d : d) + '-' + (m <= 9 ? '0' + m : m) + '-' + y);
            $('#<%=txtLogTime.ClientID %>').val('' + (h <= 9 ? '0' + h : h) + ':' + (mnt <= 9 ? '0' + mnt : mnt));
            $('#<%=txtLogText.ClientID %>').val('');

            $('#entryDetailContainerPopup2').show();
        });

        $('#btnCancelPopup2').click(function () {
            $('#entryDetailContainerPopup2').hide();
        });

        $('#btnSavePopup2').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup2', 'mpTrxPopup')) {
                cbpProcessPopup2.PerformCallback('save');
            }
        });

        setTimeout(function () {
            setDdeFilterStatusText();
            cbpViewPopup.PerformCallback('refresh');
        }, 500);
    });

    $('#<%=grdView2.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView2.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntry2ID.ClientID %>').val(entity.ProjectTaskLogID);
                cbpProcessPopup2.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView2.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView2.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntry2ID.ClientID %>').val(entity.ProjectTaskLogID);
        $('#<%=txtLogText.ClientID %>').val(entity.LogText);
        $('#<%=txtLogDate.ClientID %>').val(entity.LogDate);
        $('#<%=txtLogTime.ClientID %>').val(entity.LogTime);

        $('#entryDetailContainerPopup2').show();
    });

    function onCbpProcesPopup2EndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup2').click();
                cbpViewPopup2.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup2.PerformCallback('refresh');
        }
    }
    //#endregion

    $('.chkFilterStatus input').live('change', function () {
        setDdeFilterStatusText();
    });

    function setDdeFilterStatusText() {
        var lstFilterStatusID = '';
        var lstFilterStatusName = '';
        $('.chkFilterStatus input:checked').each(function () {
            if (lstFilterStatusName != '') {
                lstFilterStatusName += ', ';
                lstFilterStatusID += ',';
            }
            lstFilterStatusID += $(this).parent().attr('standardcodeid');
            lstFilterStatusName += $(this).parent().attr('standardcodename');
        });
        $('#<%=hdnLstFilterStatusID.ClientID %>').val(lstFilterStatusID);
        ddeFilterStatus.SetText(lstFilterStatusName);
    }

    $('.lblDownload').die('click');
    $('.lblDownload').live('click', function () {
        document.location = $(this).closest('tr').find('.hdnDownloadedFile').val();
    });

    //#region Upload
    $(function () {
        $('#divTransactionAddPopup3').click(function () {
            $('#<%=hdnEntry3ID.ClientID %>').val('');
            $('#FileUpload').val('');
            $('#<%=hdnUploadedFile.ClientID %>').val('');
            $('#<%=txtFileName.ClientID %>').val(''); 
            $('#entryDetailContainerPopup3').show();
        });

        $('#btnCancelPopup3').click(function () {
            $('#entryDetailContainerPopup3').hide();
        });

        $('#FileUpload').change(function (evt) {
            var files = evt.target.files;
            var temp = {};
            var tempArr = [];
            temp["ListData"] = tempArr;

            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var reader = new FileReader();

                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (evt) {
                        var arr = {};
                        arr['filename'] = theFile.name;
                        arr['data'] = [];
                        var text = evt.target.result;
                        for (var s = 0; s < text.length; s++) {
                            arr['data'].push(text.charCodeAt(s));
                        }
                        tempArr.push(arr);
                        var json = JSON.stringify(temp);
                        $('#<%=hdnUploadedFile.ClientID %>').val(json);
                    };
                })(file);
                reader.readAsBinaryString(file);
            }
        });

        $('#btnSavePopup3').click(function () {
            cbpProcessPopup3.PerformCallback('save');
        });
    });

    function onCbpProcesPopup3EndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup3').click();
                $('#FileUpload').val('');
                $('#<%=hdnUploadedFile.ClientID %>').val('');
                cbpViewPopup3.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup3.PerformCallback('refresh');
        }
    }
    //#endregion
</script>

<style type="text/css">
    .tr003 td, .nts003      { background-color: #40CF4E; }
    .tr002 td, .nts002      { background-color: #40A7CF; }
    .tr001 td, .nts001      { background-color: #EB6A7D; }
    
    .grdTask .selected      { border: 1px solid Red; }
    .grdTask .selected td   { border-top: 1px solid Red; border-bottom: 1px solid Red; }
</style>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnProjectOrganizationID" value="" runat="server" />
    <input type="hidden" id="hdnPosition" value="" runat="server" />
    <input type="hidden" id="hdnProjectTaskID" value="" runat="server" />
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
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Posisi")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtPosition" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
    
    <table style="width:100%">
        <tr>
            <td style="width:630px; vertical-align: top" >
                <h4><%=GetLabel("Task") %></h4>
                <div class="divTransactionEntry">   
                    <table>
                        <tr>
                            <td></td>
                            <td><asp:CheckBox ID="chkIsShowAllTask" runat="server" Checked="false" Text="Tampilkan Semua Tugas" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="width:120px"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboFilterStatus" ClientInstanceName="cboFilterStatus" runat="server" Width="200px">
                                    <ClientSideEvents ValueChanged="function(s,e){ onCboFilterStatusValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr id="trFilterStatus">
                            <td class="tdLabel" style="width:120px"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLstFilterStatusID" runat="server" />
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeFilterStatus" ID="ddeFilterStatus"
                                    Width="250px" runat="server" EnableAnimation="False">
                                    <DropDownWindowStyle BackColor="#EDEDED" />
                                    <DropDownWindowTemplate>
                                        <asp:Repeater ID="rptFilterStatus" runat="server" OnItemDataBound="rptFilterStatus_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFilterStatus" CssClass="chkFilterStatus" runat="server"  /> <%#Eval("StandardCodeName") %><br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><input type="button" id="btnPopupRefresh" value='<%=GetLabel("Refresh") %>' /></td>
                        </tr>
                    </table>
                    <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                    <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrxPopup" style="margin:0"> 
                            <input type="hidden" id="hdnEntryID" runat="server" value="" />
                            <input type="hidden" id="hdnIsAllowEdit" value="" runat="server" />
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
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" ID="cboStatus" ClientInstanceName="cboStatus" Width="200px">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboStatusValueChanged(); }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr id="trIsVerified">
                                    <td>&nbsp;</td>
                                    <td><asp:CheckBox ID="chkIsVerified" runat="server" /><%=GetLabel("Verified") %></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tenggat Waktu")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" ID="cboDueDateType" ClientInstanceName="cboDueDateType" Width="200px">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboDueDateTypeValueChanged(); }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr id="trDueDateRange">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtStartDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                                <td style="width:40px; text-align:center">s/d</td>
                                                <td><asp:TextBox ID="txtEndDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trDueDateEndDate">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tgl Tenggat Waktu")%></label></td>
                                    <td><asp:TextBox ID="txtDueDateEndDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" valign="top" style="padding-top: 5px"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
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
                                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdTask" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="ProjectTaskName" HeaderText="Tugas" />
                                        <asp:BoundField DataField="OrganizationCoordinatorName" HeaderText="Koordinator" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="ProjectTaskPriority" HeaderText="Prioritas" HeaderStyle-Width="80px" />
                                        <asp:CheckBoxField DataField="IsVerified" HeaderStyle-Width="60px" HeaderText="Verified" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="cfDueDate" HeaderText="Tenggat Waktu" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style='float:right;' class="divDetailDelete" id="divDetailDelete" runat="server"></div>
                                                <div style='float:right;margin-right:10px;' class="divDetailEdit" id="divDetailEdit" runat="server"><%=GetLabel("Edit")%></div>
                                                <input type="hidden" id="hdnIsAllowEdit" runat="server" class="hdnIsAllowEdit" />
                                                <input type="hidden" value="<%#Eval("ProjectTaskID") %>" bindingfield="ProjectTaskID" />
                                                <input type="hidden" value="<%#Eval("ProjectTaskName") %>" bindingfield="ProjectTaskName" />
                                                <input type="hidden" value="<%#Eval("OrganizationCoordinatorID") %>" bindingfield="OrganizationCoordinatorID" />
                                                <input type="hidden" value="<%#Eval("OrganizationCoordinatorName") %>" bindingfield="OrganizationCoordinatorName" />
                                                <input type="hidden" value="<%#Eval("ListOrganizationID") %>" bindingfield="ListOrganizationID" />
                                                <input type="hidden" value="<%#Eval("ListOrganizationName") %>" bindingfield="ListOrganizationName" />
                                                <input type="hidden" value="<%#Eval("GCProjectTaskPriority") %>" bindingfield="GCProjectTaskPriority" />
                                                <input type="hidden" value="<%#Eval("GCProjectTaskStatus") %>" bindingfield="GCProjectTaskStatus" />
                                                <input type="hidden" value="<%#Eval("GCDueDateType") %>" bindingfield="GCDueDateType" />
                                                <input type="hidden" value="<%#Eval("IsVerified") %>" bindingfield="IsVerified" />
                                                <input type="hidden" value="<%#Eval("StartDate", "{0:dd-MM-yyyy}") %>" bindingfield="StartDate" />
                                                <input type="hidden" value="<%#Eval("EndDate", "{0:dd-MM-yyyy}") %>" bindingfield="EndDate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
                                <asp:Repeater ID="rptRemarks" runat="server" OnItemDataBound="rptRemarks_ItemDataBound">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><div class='nts<%#Eval("cfStandardCodeID") %>' style="width: 20px; height: 20px; border: 1px solid black;"></div></td>
                                            <td><%#Eval("StandardCodeName") %></td>
                                            <td id="tdStatistic" runat="server"></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
                    ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
                </dxcp:ASPxCallbackPanel>
            </td>
            <td style="vertical-align: top">
                 <h4><%=GetLabel("Log") %></h4>
                 <div class="divTransactionEntry" id="divTransactionEntry2" style="display:none">   
                    <span id="divTransactionAddPopup2" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                    <div id="entryDetailContainerPopup2" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrxPopup2" style="margin:0"> 
                            <input type="hidden" id="hdnEntry2ID" runat="server" value="" />
                            <table id="tblEntry">
                                <colgroup>
                                    <col style="width:150px"/>
                                    <col />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtLogDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                                <td style="width:10px; text-align:center">&nbsp;</td>
                                                <td><asp:TextBox ID="txtLogTime" CssClass="thCenter" Width="70px" runat="server"/></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="top" style="padding-top: 5px">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtLogText" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                                </tr>
                                <tr id="trSaveEntry">
                                    <td></td>
                                    <td> 
                                        <input type="button" id="btnSavePopup2" class="btnWhite" value="Commit"/>
                                        <input type="button" id="btnCancelPopup2" class="btnWhite" value="Cancel"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </div>
                <dxcp:ASPxCallbackPanel ID="cbpViewPopup2" runat="server" Width="100%" ClientInstanceName="cbpViewPopup2"
                    ShowLoadingPanel="false" OnCallback="cbpViewPopup2_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectTaskLogID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="LogDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="LogTime" HeaderText="Waktu" HeaderStyle-Width="70px" />
                                        <asp:BoundField DataField="LogText" HeaderText="Keterangan" />
                                        <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="150px" />
                                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div class="divDetailDelete" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;'" %>></div>
                                                <div class="divDetailEdit" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;margin-right:10px;'" %>><%=GetLabel("Edit")%></div>
                                                <input type="hidden" value="<%#Eval("ProjectTaskLogID") %>" bindingfield="ProjectTaskLogID" />
                                                <input type="hidden" value="<%#Eval("LogDate", "{0:dd-MM-yyyy}") %>" bindingfield="LogDate" />
                                                <input type="hidden" value="<%#Eval("LogTime") %>" bindingfield="LogTime" />
                                                <input type="hidden" value="<%#Eval("LogText") %>" bindingfield="LogText" />
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
                <dxcp:ASPxCallbackPanel ID="cbpProcessPopup2" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup2"
                    ShowLoadingPanel="false" OnCallback="cbpProcessPopup2_Callback">
                    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopup2EndCallback(s); }" />
                </dxcp:ASPxCallbackPanel>

                
                 <h4><%=GetLabel("File") %></h4>
                 <div class="divTransactionEntry" id="divTransactionEntry3" style="display:none">   
                    <span id="divTransactionAddPopup3" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                    <div id="entryDetailContainerPopup3" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrxPopup3" style="margin:0"> 
                            <input type="hidden" id="hdnEntry3ID" runat="server" value="" />
                            <table>
                                <colgroup>
                                    <col style="width:150px"/>
                                    <col />
                                </colgroup>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <input type="hidden" id="hdnUploadedFile" runat="server" value="" />
                                        <input type="file" id="FileUpload" name="FileUpload" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama File") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtFileName" Width="300px" /></td>
                                </tr>
                                <tr valign="top" style="padding-top: 5px">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtFileRemarks" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> 
                                        <input type="button" id="btnSavePopup3" class="btnWhite" value="Commit"/>
                                        <input type="button" id="btnCancelPopup3" class="btnWhite" value="Cancel"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </div>
                <dxcp:ASPxCallbackPanel ID="cbpViewPopup3" runat="server" Width="100%" ClientInstanceName="cbpViewPopup3"
                    ShowLoadingPanel="false" OnCallback="cbpViewPopup3_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">
                            <asp:Panel runat="server" ID="Panel2" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView3" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView3_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectTaskFileID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <label class="lblDownload lblLink"><%#Eval("FileName") %></label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                        <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="120px" />
                                        <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div class="divDetailDelete" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;'" %>></div>
                                                <input type="hidden" id="hdnDownloadedFile" runat="server" class="hdnDownloadedFile" />
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
                <dxcp:ASPxCallbackPanel ID="cbpProcessPopup3" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup3"
                    ShowLoadingPanel="false" OnCallback="cbpProcessPopup3_Callback">
                    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopup3EndCallback(s); }" />
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</div>

