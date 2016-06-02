<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassMeetingEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassMeetingEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtMeetingDate.ClientID %>');

            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
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
                    onCustomButtonClick('save');
                }
            });

            $('#btnMeetingPlanDt').click(function () {
                var id = tacSubjectCurriculumMeetingPlan.getValue();
                if (id != null && id != '') {
                    id += '|' + $('#<%=hdnSubjectID.ClientID %>').val();
                    var url = ResolveUrl("~/Program/ClassMeeting/ClassMeeting/SubjectCurriculumMeetingPlanDtCtl.ascx");
                    openUserControlPopup(url, id, 'Detil Pertemuan', 1000, 610);
                }
            });
        });

        //#region Room
        function onGetRoomFilterExpression() {
            var filterExpression = "<%=OnGetRoomFilterExpression() %>";
            return filterExpression;
        }

        function onTacRoomButtonSearchClick() {
            openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
                var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
                Methods.getObject('GetRoomList', filterExpression, function (result) {
                    if (result != null) {
                        tacRoom.setValue(result.RoomID);
                        tacRoom.setText(result.RoomName);
                    }
                    else {
                        tacRoom.setValue('');
                        tacRoom.setText('');
                    }
                });
            });

        }

        function onTacRoomValueChanged() {
        }
        //#endregion

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeacher.setValue(result.TeacherID);
                        tacTeacher.setText(result.TeacherName);
                    }
                    else {
                        tacTeacher.setValue('');
                        tacTeacher.setText('');
                    }
                });
            });

        }

        function onTacTeacherValueChanged() {
        }
        //#endregion

        //#region Assistant Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacAssistantTeacherButtonSearchClick() {
            openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                    if (result != null) {
                        tacAssistantTeacher.setValue(result.TeacherID);
                        tacAssistantTeacher.setText(result.TeacherName);
                    }
                    else {
                        tacAssistantTeacher.setValue('');
                        tacAssistantTeacher.setText('');
                    }
                });
            });

        }

        function onTacAssistantTeacherValueChanged() {
        }
        //#endregion

        //#region SubjectCurriculumMeetingPlan
        function onGetSubjectCurriculumMeetingPlanFilterExpression() {
            var filterExpression = "<%=OnGetSubjectCurriculumMeetingPlanFilterExpression() %>";
            return filterExpression;
        }

        function onTacSubjectCurriculumMeetingPlanButtonSearchClick() {
            openSearchDialog('subjectcurriculummeetingplan', onGetSubjectCurriculumMeetingPlanFilterExpression(), function (value) {
                var filterExpression = onGetSubjectCurriculumMeetingPlanFilterExpression() + " AND SubjectCurriculumMeetingPlanID = '" + value + "'";
                Methods.getObject('GetvSubjectCurriculumMeetingPlanList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubjectCurriculumMeetingPlan.setValue(result.SubjectCurriculumMeetingPlanID);
                        tacSubjectCurriculumMeetingPlan.setText(result.SubjectCurriculumMeetingPlanName);
                    }
                    else {
                        tacSubjectCurriculumMeetingPlan.setValue('');
                        tacSubjectCurriculumMeetingPlan.setText('');
                    }
                    entityToControlSubjectCurriculumMeetingPlan(result);
                });
            });

        }

        function onTacSubjectCurriculumMeetingPlanValueChanged() {
            var id = tacSubjectCurriculumMeetingPlan.getValue();
            if (id != '') {
                var filterExpression = "SubjectCurriculumMeetingPlanID = '" + value + "'";
                Methods.getObject('GetvSubjectCurriculumMeetingPlanList', filterExpression, function (result) {
                    entityToControlSubjectCurriculumMeetingPlan(result);
                });
            }
        }

        function entityToControlSubjectCurriculumMeetingPlan(result) {
            $('#tblEntry .trSubjectIndicatorDt').each(function () {
                $(this).remove();
            });
            if (result != null) {
                $('#<%=hdnParentSubjectCurriculumMeetingPlanID.ClientID %>').val(result.ParentID);
                var filterExpression = onGetSubjectIndicatorFilterExpression();

                var filterExpression1 = "";
                if (result.ParentID > 0)
                    filterExpression1 += "(DisplayPath LIKE '%/" + result.SubjectCurriculumMeetingPlanID + "/%' OR DisplayPath LIKE '%/" + result.ParentID + "/%' )";
                else
                    filterExpression1 += "(DisplayPath LIKE '%/" + result.SubjectCurriculumMeetingPlanID + "/%')";

                filterExpression += " AND SubjectCurriculumSyllabusID IN (SELECT ReferenceID FROM vSubjectCurriculumMeetingPlan WHERE <%=OnGetSubjectIndicatorMeetingPlanFilterExpression() %> AND " + filterExpression1 + ")";
                Methods.getListObject('GetvSubjectCurriculumSyllabusList', filterExpression, function (result) {
                    for (var i = 0; i < result.length; ++i) {
                        var entity = result[i];
                        $('#divEntryDtAdd').click();

                        $tr = $('.trSubjectIndicatorDt').last();
                        $tacSubjectIndicator = $tr.find('.tacSubjectIndicator');
                        $tacSubjectIndicator.find('.hdnAutoCompleteValue').val(entity.SubjectCurriculumSyllabusID);
                        $tacSubjectIndicator.find('.hdnAutoCompleteText').val(entity.SubjectCurriculumSyllabusName);
                        $tacSubjectIndicator.find('.txtAutoComplete').val(entity.SubjectCurriculumSyllabusName);

                        $tr.find('.divDetailDelete').hide();
                    }
                });
            }
            else
                $('#<%=hdnParentSubjectCurriculumMeetingPlanID.ClientID %>').val('');
        }
        //#endregion        

        window.onGetSubjectIndicatorFilterExpression = function () {
            var filterExpression = "<%=OnGetSubjectIndicatorFilterExpression() %>";
            return filterExpression;
        }

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

            setTimeout(function () {
                $('#divEntryDtAdd').show();

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
                            $tr.find('.keyField').html(entity.ClassMeetingIndicatorID);
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
            }, 500);
        });

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
            <td class="tdLabel"><%=GetLabel("Indikator") %></td>
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
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnClassMeetingID" runat="server" />
    <input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
    <input type="hidden" id="hdnSubjectIndicatorSave" runat="server" />
    <table style="width:100%" id="tblEntry">
        <colgroup>
            <col style="width:130px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
            <td><asp:TextBox ID="txtMeetingDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam") %></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:80px" />
                        <col style="width:10px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>    
                        <td align="center"><%=GetLabel("s/d") %></td>
                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Ruangan")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRoom" ClientInstanceName="tacRoom" MethodName="GetRoomList" GetFilterExpressionFunction="onGetRoomFilterExpression"
                    SearchFields="RoomName,RoomID" TextField="RoomName" ValueField="RoomID" SearchText="${RoomName} (<b>${RoomID}</b>)" OrderByExpression="RoomName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacRoomButtonSearchClick(); }"
                        ValueChanged="function(){ onTacRoomValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetvTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Guru 2")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacAssistantTeacher" ClientInstanceName="tacAssistantTeacher" MethodName="GetvTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacAssistantTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacAssistantTeacherValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pertemuan")%></label></td>
            <td>
                <input type="hidden" id="hdnParentSubjectCurriculumMeetingPlanID" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubjectCurriculumMeetingPlan" ClientInstanceName="tacSubjectCurriculumMeetingPlan" MethodName="GetvSubjectCurriculumMeetingPlanList" GetFilterExpressionFunction="onGetSubjectCurriculumMeetingPlanFilterExpression"
                    SearchFields="MeetingNo,SubjectCompetencyStandardName" TextField="MeetingNo" ValueField="SubjectCurriculumMeetingPlanID" SearchText="${MeetingNo} (<b>${SubjectCompetencyStandardName}</b>)(<b>${ListSubjectBasicCompetencyID}</<b>)" OrderByExpression="MeetingNo">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCurriculumMeetingPlanButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSubjectCurriculumMeetingPlanValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
                <input type="button" id="btnMeetingPlanDt" class="btnMore" value="..." />
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan")%></label></td>
            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan Berikutnya")%></label></td>
            <td><asp:TextBox ID="txtNextMeetingRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
        </tr>
        <tr>
            <td colspan="2"><h4><%=GetLabel("Indikator") %></h4></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Indikator")%></span><br /></td>
        </tr>
        <tr id="trSaveEntryPopup">
        </tr>
    </table>
</asp:Content>