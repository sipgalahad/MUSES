<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="PeriodClassTypeSubjectExtracurricularEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodClassTypeSubjectExtracurricularEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                tacSubject.setValue('');
                tacSubject.setText('');
                tacTeacher.setValue('');
                tacTeacher.setText('');
                tacSubjectCurriculum.setValue('');
                tacSubjectCurriculum.setText('');
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodClassTypeSubjectID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodClassTypeSubjectID);
            tacSubject.setValue(entity.SubjectID);
            tacSubject.setText(entity.SubjectName);
            tacTeacher.setValue(entity.TeacherID);
            tacTeacher.setText(entity.TeacherName);
            tacSubjectCurriculum.setValue(entity.SubjectCurriculumID);
            tacSubjectCurriculum.setText(entity.SubjectCurriculumName);
            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Subject
        function onGetSubjectFilterExpression() {
            var filterExpression = "CurriculumClassTypeID = " + $('#<%=hdnClassTypeID.ClientID %>').val() + " AND IsDeleted = 0 AND SubjectID NOT IN (SELECT SubjectID FROM vPeriodClassTypeSubject WHERE PeriodClassTypeID = " + cboClassType.GetValue() + " AND IsDeleted = 0)";
            return filterExpression;
        }

        function onTacSubjectButtonSearchClick() {
            openSearchDialog('curriculumsubjectclasstype', onGetSubjectFilterExpression(), function (value) {
                var filterExpression = onGetSubjectFilterExpression() + " AND SubjectCode = '" + value + "'";
                Methods.getObject('GetvCurriculumSubjectClassTypeList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubject.setValue(result.SubjectID);
                        tacSubject.setText(result.SubjectName);
                    }
                    else {
                        tacSubject.setValue('');
                        tacSubject.setText('');
                    }
                });
            });

        }

        function onTacSubjectValueChanged() {
        }
        //#endregion

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "1 = 0";
            var subjectID = tacSubject.getValue();
            if (subjectID != '')
                filterExpression = "SubjectID = " + subjectID;
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('teachersubject', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetvTeacherSubjectList', filterExpression, function (result) {
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

        //#region Subject Matter
        function onGetSubjectCurriculumFilterExpression() {
            var filterExpression = "1 = 0";
            var subjectID = tacSubject.getValue();
            if (subjectID != '')
                filterExpression = "SubjectID = " + subjectID + " AND CurriculumID = " + $('#<%=hdnCurriculumID.ClientID %>').val() + " AND SubjectCurriculumID IN (SELECT SubjectCurriculumID FROM SubjectCurriculumClassType WHERE CurriculumClassTypeID = " + $('#<%=hdnClassTypeID.ClientID %>').val() + ") AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacSubjectCurriculumButtonSearchClick() {
            openSearchDialog('subjectcurriculum', onGetSubjectCurriculumFilterExpression(), function (value) {
                var filterExpression = onGetSubjectCurriculumFilterExpression() + " AND SubjectCurriculumID = '" + value + "'";
                Methods.getObject('GetSubjectCurriculumList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubjectCurriculum.setValue(result.SubjectCurriculumID);
                        tacSubjectCurriculum.setText(result.SubjectCurriculumName);
                    }
                    else {
                        tacSubjectCurriculum.setValue('');
                        tacSubjectCurriculum.setText('');
                    }
                });
            });

        }

        function onTacSubjectCurriculumValueChanged() {
        }
        //#endregion

        function onCboClassTypeValueChanged(s) {
            $('#btnCancel').click();
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

        function onCbpViewEndCallback() {
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" value="" id="hdnCurriculumID" runat="server" />
    <table>
        <colgroup>
            <col style="width: 150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tipe Kelas") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboClassType" ClientInstanceName="cboClassType" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboClassTypeValueChanged(s); }" />
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
                                    <col style="width: 150px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Ekskul")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubject" ClientInstanceName="tacSubject" MethodName="GetvSubjectClassTypeList" GetFilterExpressionFunction="onGetSubjectFilterExpression"
                                            SearchFields="SubjectName,SubjectCode" TextField="SubjectName" ValueField="SubjectID" SearchText="${SubjectName} (<b>${SubjectCode}</b>)" OrderByExpression="SubjectName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Pembina")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetvTeacherSubjectList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                                            SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubjectCurriculum" ClientInstanceName="tacSubjectCurriculum" MethodName="GetSubjectCurriculumList" GetFilterExpressionFunction="onGetSubjectCurriculumFilterExpression"
                                            SearchFields="SubjectCurriculumName,SubjectCurriculumCode" TextField="SubjectCurriculumName" ValueField="SubjectCurriculumID" SearchText="${SubjectCurriculumName} (<b>${SubjectCurriculumCode}</b>)" OrderByExpression="SubjectCurriculumName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCurriculumButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectCurriculumValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
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
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnClassTypeID" runat="server" value="" />
                        <input type="hidden" id="hdnClassRowCount" runat="server" value="" />
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PeriodClassTypeSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="SubjectName" HeaderText="Ekskul"/>
                                <asp:BoundField DataField="TeacherName" HeaderText="Pembina" HeaderStyle-Width="280px" />
                                <asp:BoundField DataField="SubjectCurriculumName" HeaderText="Jenis Kurikulum" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;<%#Eval("IsEditable").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;<%#Eval("IsEditable").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("PeriodClassTypeSubjectID") %>" bindingfield="PeriodClassTypeSubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectID") %>" bindingfield="SubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                        <input type="hidden" value="<%#Eval("TeacherID") %>" bindingfield="TeacherID" />
                                        <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumID") %>" bindingfield="SubjectCurriculumID" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumName") %>" bindingfield="SubjectCurriculumName" />
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
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>