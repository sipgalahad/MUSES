<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="TeacherPeriodClassTypeSubjectEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherPeriodClassTypeSubjectEntry" %>

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
            $('#<%=chkIsTheoryFormulaDefault.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    cboTheoryFinalMarkFormula.SetEnabled(false);
                    cboTheoryFinalMarkFormula.SetValue('');
                }
                else
                    cboTheoryFinalMarkFormula.SetEnabled(true);
            });

            $('#<%=chkIsPracticeFormulaDefault.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    cboPracticeFinalMarkFormula.SetEnabled(false);
                    cboPracticeFinalMarkFormula.SetValue('');
                }
                else
                    cboPracticeFinalMarkFormula.SetEnabled(true);
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            $('#btnTheoryFinalMarkFormulaDt').click(function () {
                var id = cboTheoryFinalMarkFormula.GetValue();
                if (id != null && id != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/StudentFinalMarkFormulaDtCtl.ascx");
                    openUserControlPopup(url, id, 'Detil Formula', 900, 400);
                }
            });

            $('#btnPracticeFinalMarkFormulaDt').click(function () {
                var id = cboPracticeFinalMarkFormula.GetValue();
                if (id != null && id != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/StudentFinalMarkFormulaDtCtl.ascx");
                    openUserControlPopup(url, id, 'Detil Formula', 900, 400);
                }
            });
            $('#btnSubjectCurriculumDt').click(function () {
                var id = tacSubjectCurriculum.getValue();
                if (id != null && id != '') {
                    var url = ResolveUrl('~/Program/Master/Subject/SubjectPageLauncher.aspx?id=' + tacSubject.getValue() + '|' + id);
                    openWindowPopup(url, 'Subject', '1300', '650');
                }
            });
        });

        //#region edit
        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodClassTypeSubjectID);
            $('#<%=hdnClassTypeID.ClientID %>').val(entity.CurriculumClassTypeID);
            tacSubject.setValue(entity.SubjectID);
            tacSubject.setText(entity.SubjectName);
            tacSubjectCurriculum.setValue(entity.SubjectCurriculumID);
            tacSubjectCurriculum.setText(entity.SubjectCurriculumName);
            cboCurriculumSubjectGroup.SetValue(entity.CurriculumSubjectGroupID);
            cboCurriculumSubjectGroup.SetEnabled(false);
            $('#<%=txtNoMeetingHoursInWeek.ClientID %>').val(entity.NoMeetingHoursInWeek);
            $('#<%=txtPassingGrade.ClientID %>').val(entity.PassingGrade);
            tacSubject.setEnabled(false);
            $('#<%=txtNoMeetingHoursInWeek.ClientID %>').attr('readonly', 'readonly');

            if (entity.TheoryFinalMarkFormulaID == 0) {
                $('#<%=chkIsTheoryFormulaDefault.ClientID %>').prop('checked', true);
                cboTheoryFinalMarkFormula.SetValue('');
                cboTheoryFinalMarkFormula.SetEnabled(false);
            }
            else {
                cboTheoryFinalMarkFormula.SetValue(entity.TheoryFinalMarkFormulaID);
                cboTheoryFinalMarkFormula.SetEnabled(true);
                $('#<%=chkIsTheoryFormulaDefault.ClientID %>').prop('checked', false);
            }

            if (entity.PracticeFinalMarkFormulaID == 0) {
                $('#<%=chkIsPracticeFormulaDefault.ClientID %>').prop('checked', true);
                cboPracticeFinalMarkFormula.SetValue('');
                cboPracticeFinalMarkFormula.SetEnabled(false);
            }
            else {
                cboPracticeFinalMarkFormula.SetValue(entity.PracticeFinalMarkFormulaID);
                cboPracticeFinalMarkFormula.SetEnabled(true);
                $('#<%=chkIsPracticeFormulaDefault.ClientID %>').prop('checked', false);
            }
            $('#entryDetailContainer').show();
        });

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

        function onCboSchoolPeriodValueChanged(s) {
            var filterExpression = 'SchoolPeriodID = ' + cboSchoolPeriod.GetValue();
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                $('#<%=hdnCurriculumID.ClientID %>').val(entity.CurriculumID);
                cbpView.PerformCallback('refresh');
            });
        }

        function onCbpViewEndCallback() {
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" value="" id="hdnCurriculumID" runat="server" />    
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <input type="hidden" id="hdnClassTypeID" runat="server" value="" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 210px" />
                                    <col style="width: 300px" />
                                    <col style="width: 40px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Pelajaran")%></label></td>
                                    <td colspan="3">
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacSubject" ClientInstanceName="tacSubject" MethodName="GetvSubjectClassTypeList" GetFilterExpressionFunction="onGetSubjectFilterExpression"
                                            SearchFields="SubjectName,SubjectCode" TextField="SubjectName" ValueField="SubjectID" SearchText="${SubjectName} (<b>${SubjectCode}</b>)" OrderByExpression="SubjectName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Pelajaran")%></label></td>
                                    <td colspan="3"><dxe:ASPxComboBox runat="server" ID="cboCurriculumSubjectGroup" ClientInstanceName="cboCurriculumSubjectGroup" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
                                    <td colspan="3">
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacSubjectCurriculum" ClientInstanceName="tacSubjectCurriculum" MethodName="GetSubjectCurriculumList" GetFilterExpressionFunction="onGetSubjectCurriculumFilterExpression"
                                            SearchFields="SubjectCurriculumName,SubjectCurriculumCode" TextField="SubjectCurriculumName" ValueField="SubjectCurriculumID" SearchText="${SubjectCurriculumName} (<b>${SubjectCurriculumCode}</b>)" OrderByExpression="SubjectCurriculumName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCurriculumButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectCurriculumValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                        <input type="button" id="btnSubjectCurriculumDt" class="btnMore" value="..." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam Pertemuan")%></label></td>
                                    <td colspan="3"><asp:TextBox ID="txtNoMeetingHoursInWeek" CssClass="number" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("KKM")%></label></td>
                                    <td colspan="3"><asp:TextBox ID="txtPassingGrade" CssClass="number" Width="80px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Formula Nilai Rapor (Teori)")%></label></td>
                                    <td><dxe:ASPxComboBox runat="server" ID="cboTheoryFinalMarkFormula" ClientInstanceName="cboTheoryFinalMarkFormula" Width="300px" /></td>
                                    <td><input type="button" id="btnTheoryFinalMarkFormulaDt" class="btnMore" value="..." /></td>
                                    <td><asp:CheckBox ID="chkIsTheoryFormulaDefault" runat="server" /><%=GetLabel("Default") %></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Formula Nilai Rapor (Praktek)")%></label></td>
                                    <td><dxe:ASPxComboBox runat="server" ID="cboPracticeFinalMarkFormula" ClientInstanceName="cboPracticeFinalMarkFormula" Width="300px" /></td>
                                    <td><input type="button" id="btnPracticeFinalMarkFormulaDt" class="btnMore" value="..." /></td>
                                    <td><asp:CheckBox ID="chkIsPracticeFormulaDefault" runat="server" /><%=GetLabel("Default") %></td>
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
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PeriodClassTypeSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="CurriculumClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="280px" />
                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran"/>
                                <asp:BoundField DataField="CurriculumSubjectGroupName" HeaderText="Jenis Pelajaran" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="SubjectCurriculumName" HeaderText="Jenis Kurikulum" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="NoMeetingHoursInWeek" HeaderText="Jam Pertemuan" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PassingGrade" HeaderText="KKM" HeaderStyle-Width="80px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("PeriodClassTypeSubjectID") %>" bindingfield="PeriodClassTypeSubjectID" />
                                        <input type="hidden" value="<%#Eval("CurriculumClassTypeID") %>" bindingfield="CurriculumClassTypeID" />
                                        <input type="hidden" value="<%#Eval("SubjectID") %>" bindingfield="SubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumID") %>" bindingfield="SubjectCurriculumID" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumName") %>" bindingfield="SubjectCurriculumName" />
                                        <input type="hidden" value="<%#Eval("CurriculumSubjectGroupID") %>" bindingfield="CurriculumSubjectGroupID" />
                                        <input type="hidden" value="<%#Eval("NoMeetingHoursInWeek") %>" bindingfield="NoMeetingHoursInWeek" />
                                        <input type="hidden" value="<%#Eval("TheoryFinalMarkFormulaID") %>" bindingfield="TheoryFinalMarkFormulaID" />
                                        <input type="hidden" value="<%#Eval("PracticeFinalMarkFormulaID") %>" bindingfield="PracticeFinalMarkFormulaID" />
                                        <input type="hidden" value="<%#Eval("PassingGrade") %>" bindingfield="PassingGrade" />
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