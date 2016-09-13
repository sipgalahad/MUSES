<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskEntryCtl" %>

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
        setDatePicker('<%=txtTaskDate.ClientID %>');
        setDatePicker('<%=txtStartDate.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');
    });

    //#region Task Type
    function onGetCurriculumMarkTypeDtFilterExpression() {
        var filterExpression = "CurriculumMarkTypeID = " + cboLessonType.GetValue() + " AND IsExam = 0 AND IsDeleted = 0";
        return filterExpression;
    }

    function onTacTaskTypeButtonSearchClick() {
        openSearchDialog('curriculummarktypedt', onGetCurriculumMarkTypeDtFilterExpression(), function (value) {
            var filterExpression = onGetCurriculumMarkTypeDtFilterExpression() + " AND CurriculumMarkTypeDtID = '" + value + "'";
            Methods.getObject('GetCurriculumMarkTypeDtList', filterExpression, function (result) {
                if (result != null) {
                    tacTaskType.setValue(result.CurriculumMarkTypeDtID);
                    tacTaskType.setText(result.CurriculumMarkTypeDtName);
                    $('#<%=hdnCurriculumMarkTypeDtInitial.ClientID %>').val(result.Initial);
                }
                else {
                    tacTaskType.setValue('');
                    tacTaskType.setText('');
                    $('#<%=hdnCurriculumMarkTypeDtInitial.ClientID %>').val('');
                }
            });
        });

    }

    function onTacTaskTypeValueChanged() {
        var taskTypeID = tacTaskType.getValue();
        if (taskTypeID != "") {
            var filterExpression = "CurriculumMarkTypeDtID = " + taskTypeID;
            Methods.getObject('GetCurriculumMarkTypeDtList', filterExpression, function (result) {
                if (result != null) 
                    $('#<%=hdnCurriculumMarkTypeDtInitial.ClientID %>').val(result.Initial);
                else 
                    $('#<%=hdnCurriculumMarkTypeDtInitial.ClientID %>').val('');
            });
        }
        else
            $('#<%=hdnCurriculumMarkTypeDtInitial.ClientID %>').val('');
    }
    //#endregion

    function onBeforeSaveRecord() {
        var result = '';
        $('.tacSubjectIndicator').each(function () {
            if (result != "")
                result += '|';
            var subjectIndicatorID = '';
            var subjectIndicatorName = '';
            $tr = $(this).closest('tr');
            var keyField = $tr.find('.keyField').html();
            if ($tr.find('.chkIsFromMeetingPlan').is(':checked'))
                subjectIndicatorID = $tr.find('.hdnAutoCompleteValue').val();
            else
                subjectIndicatorName = $tr.find('.txtSubjectIndicatorName').val();
            result += keyField + ',' + subjectIndicatorID + ',' + subjectIndicatorName;
        });
        $('#<%=hdnSubjectIndicatorSave.ClientID %>').val(result);
        $('#<%=hdnTaskTypeID.ClientID %>').val(tacTaskType.getValue()); 
        return true;
    }

    window.onGetSubjectIndicatorFilterExpression = function () {
        var filterExpression = "<%=OnGetSubjectIndicatorFilterExpression() %>";
        if (cboLessonType.GetValue() != "")
            filterExpression += " AND (DisplayCurriculumMarkTypeID IS NULL OR DisplayCurriculumMarkTypeID = " + cboLessonType.GetValue() + ")";
        return filterExpression;
    }

    $('.chkIsFromMeetingPlan').live('change', function () {
        $tr = $(this).closest('tr');
        if ($(this).is(':checked')) {
            $tr.find('.tacSubjectIndicator').show();
            $tr.find('.txtSubjectIndicatorName').hide();
        }
        else {
            $tr.find('.tacSubjectIndicator').hide();
            $tr.find('.txtSubjectIndicatorName').show();
        }
    });

    var idxSubjectIndicator = 0;
    var tempHelper = null;
    $(function () {
        $('#divEntryDtAdd').click(function () {
            $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
            $newTr = $newTr.replace(/\$\{idx}/g, idxSubjectIndicator);
            $newTr = $($newTr);
            $newTr.insertBefore($('#trSaveEntryPopup'));

            tempHelper = new CodeXClientAutoCompleteHelper();
            tempHelper.init("SubjectIndicator" + idxSubjectIndicator, "SubjectCurriculumSyllabusName", "GetvSubjectCurriculumSyllabusList", "", "onGetSubjectIndicatorFilterExpression", "SubjectCurriculumSyllabusID");
            tempHelper.setClientSideEvents(onSubjectIndicatorIDValueChanged);
            tempHelper.initializeControl();
            idxSubjectIndicator++;
        });

        var classSubjectTaskID = $('#<%=hdnID.ClientID %>').val();
        if (classSubjectTaskID != '0' && classSubjectTaskID != '') {
            var filterExpression = 'ClassSubjectTaskID = ' + classSubjectTaskID;
            Methods.getListObject('GetvClassSubjectTaskIndicatorList', filterExpression, function (result) {
                for (var i = 0; i < result.length; ++i) {
                    var entity = result[i];
                    $('#divEntryDtAdd').click();

                    $tr = $('.trSubjectIndicatorDt').last();
                    $tr.find('.chkIsFromMeetingPlan').prop('checked', entity.SubjectIndicatorID > 0);
                    $tr.find('.chkIsFromMeetingPlan').change();
                    $tr.find('.keyField').html(entity.ClassSubjectTaskIndicatorID);
                    if (entity.SubjectIndicatorID > 0) {
                        $tacSubjectIndicator = $tr.find('.tacSubjectIndicator');
                        $tacSubjectIndicator.find('.hdnAutoCompleteValue').val(entity.SubjectIndicatorID);
                        $tacSubjectIndicator.find('.hdnAutoCompleteText').val(entity.SubjectIndicatorName);
                        $tacSubjectIndicator.find('.txtAutoComplete').val(entity.SubjectIndicatorName);
                    }
                    else {
                        $tr.find('.txtSubjectIndicatorName').val(entity.SubjectIndicatorName);
                    }
                }
            });
        }
        else {
            var classMeetingID = $('#<%=hdnClassMeetingID.ClientID %>').val();
            if (classMeetingID != '0') {
                var filterExpression = 'ClassMeetingID = ' + classMeetingID;
                Methods.getListObject('GetvClassMeetingIndicatorList', filterExpression, function (result) {
                    for (var i = 0; i < result.length; ++i) {
                        var entity = result[i];
                        $('#divEntryDtAdd').click();

                        $tr = $('.trSubjectIndicatorDt').last();
                        $tr.find('.chkIsFromMeetingPlan').prop('checked', entity.SubjectIndicatorID > 0);
                        $tr.find('.chkIsFromMeetingPlan').change();
                        if (entity.SubjectIndicatorID > 0) {
                            $tacSubjectIndicator = $tr.find('.tacSubjectIndicator');
                            $tacSubjectIndicator.find('.hdnAutoCompleteValue').val(entity.SubjectIndicatorID);
                            $tacSubjectIndicator.find('.hdnAutoCompleteText').val(entity.SubjectIndicatorName);
                            $tacSubjectIndicator.find('.txtAutoComplete').val(entity.SubjectIndicatorName);
                        }
                        else {
                            $tr.find('.txtSubjectIndicatorName').val(entity.SubjectIndicatorName);
                        }
                    }
                });
            }
        }
    });

    function onSubjectIndicatorIDValueChanged($s) {
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

    $('.tacSubjectIndicator .btnAutoCompleteSearchMore').die('click');
    $('.tacSubjectIndicator .btnAutoCompleteSearchMore').live('click', function () {
        if ($(this).attr('enabled') == null) {
            $tacTr = $(this).closest('tr');
            openSearchDialog('subjectcurriculumsyllabus', onGetSubjectIndicatorFilterExpression(), function (value) {
                var filterExpression = onGetSubjectIndicatorFilterExpression() + " AND SubjectCurriculumSyllabusID = '" + value + "'";
                Methods.getObject('GetvSubjectCurriculumSyllabusList', filterExpression, function (result) {
                    $tacCOA = $tacTr.find('.tacSubjectIndicator');
                    if (result != null) {
                        $tacCOA.find('.hdnAutoCompleteValue').val(result.SubjectCurriculumSyllabusID);
                        $tacCOA.find('.hdnAutoCompleteText').val(result.SubjectCurriculumSyllabusName);
                        $tacCOA.find('.txtAutoComplete').val(result.SubjectCurriculumSyllabusName);
                    }
                    else {
                        $tacCOA.find('.hdnAutoCompleteValue').val('');
                        $tacCOA.find('.hdnAutoCompleteText').val('');
                        $tacCOA.find('.txtAutoComplete').val('');
                    }
                    onSubjectIndicatorIDValueChanged($tacCOA.find('.txtAutoComplete'));
                });
                //var trIdx = $('.trPrescriptionEntry').index($tacTr);
                //if (trIdx == $('.trPrescriptionEntry').length - 1)
                //    addEntityRowPrescription();
                $tacTr = null;
            });
        }
    });
</script>

<script id="tmplEntityDt" type="text/x-jquery-tmpl">
    <tr class="trSubjectIndicatorDt">
        <td class="tdLabel"><%=GetLabel("IPK") %></td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class='keyField' style='display:none'>0</td>
                    <td style="width:450px">
                        <div id="SubjectIndicator${idx}" class="tacSubjectIndicator">
                            <div>
                                <div class="containerAutoComplete">
                                    <input type="hidden" class="hdnAutoCompleteValue"/>
                                    <input type="hidden" class="hdnAutoCompleteText"/>
                                    <input type="hidden" class="hdnIsRequired" value="1"/>
                                    <input type="hidden" class="hdnValidationGroup" value="mpDrugsQuickPicks"/>
                                    <input type="text" class="required txtAutoComplete" validationgroup="mpTrxPopup" style="width:400px"/>
                                    <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                                    <div class="divListAutoCompleteResultBox">
                                        <div class="divListAutoCompleteResult">
                                        </div>
                                    </div>
                                </div>
                                <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                    <div>
                                        ${SubjectCurriculumSyllabusName}
                                        <input type='hidden' value='${SubjectCurriculumSyllabusName}' class='hdnAutoCompleteRowText'/>
                                        <input type='hidden' value='${SubjectCurriculumSyllabusID}' class='hdnAutoCompleteRowValue'/>
                                    </div>
                                </script1>
                            </div>
                        </div>
                        <input type="text" class="txtSubjectIndicatorName" style="width:440px; display:none"/>
                    </td>
                    <td style='width:100px'><input type='checkbox' checked='checked' class='chkIsFromMeetingPlan'/><%=GetLabel("Dari Silabus") %></td>
                    <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
                </tr>
            </table>
        </td>
    </tr>
</script>

<input type="hidden" id="hdnClassMeetingID" runat="server" />
<input type="hidden" id="hdnSubjectID" runat="server" />
<input type="hidden" id="hdnPeriodClassTypeSubjectID" runat="server" />
<input type="hidden" id="hdnIsPeriodClassTypeSubjectIndicatorExists" runat="server" />
<input type="hidden" id="hdnCurriculumSubjectGroupID" runat="server" />
<input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
<input type="hidden" id="hdnSubjectIndicatorSave" runat="server" />
<input type="hidden" id="hdnSchoolClassInitial" runat="server" />
<input type="hidden" id="hdnSubjectInitial" runat="server" />
<input type="hidden" id="hdnSubjectGroupInitial" runat="server" />
<input type="hidden" id="hdnCurriculumMarkTypeDtInitial" runat="server" />
<input type="hidden" id="hdnID" runat="server" value="" />
<div>
    <table>
        <colgroup>
            <col style="width: 160px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
            <td><asp:TextBox ID="txtClassTaskCode" Width="100px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Topik")%></label></td>
            <td><asp:TextBox ID="txtTopic" Width="200px" runat="server" /></td>
        </tr>
        <tr id="trLessonType" runat="server">
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Pelajaran")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboLessonType" ClientInstanceName="cboLessonType" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Komponen Nilai")%></label></td>
            <td>
                <input type="hidden" id="hdnTaskTypeID" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTaskType" ClientInstanceName="tacTaskType" MethodName="GetCurriculumMarkTypeDtList" GetFilterExpressionFunction="onGetCurriculumMarkTypeDtFilterExpression"
                    SearchFields="CurriculumMarkTypeDtName" TextField="CurriculumMarkTypeDtName" ValueField="CurriculumMarkTypeDtID" SearchText="${CurriculumMarkTypeDtName}" OrderByExpression="CurriculumMarkTypeDtName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTaskTypeButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTaskTypeValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox> 
            </td>
        </tr>
        <tr style="display: none">
            <td class="tdLabel"><label><%=GetLabel("% Bobot Nilai")%></label></td>
            <td><asp:TextBox ID="txtFinalMarkPercentage" CssClass="number" Width="80px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Tugas")%></label></td>
            <td><asp:TextBox ID="txtTaskDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Mulai")%></label></td>
            <td>    
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr id="trIsIncludeInMidSemeterRapor" runat="server">
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Masuk Dalam Rapor Mid Semester")%></label></td>
            <td><asp:CheckBox ID="chkIsIncludeInMidSemesterRapor" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
            <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
        </tr>
        <tr>
            <td colspan="2"><h4><%=GetLabel("IPK")%></h4></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah IPK")%></span><br /></td>
        </tr>
        <tr id="trSaveEntryPopup">
        </tr>
    </table>
</div>

