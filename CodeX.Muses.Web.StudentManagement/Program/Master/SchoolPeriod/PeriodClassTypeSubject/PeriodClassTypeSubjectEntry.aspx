<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="PeriodClassTypeSubjectEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodClassTypeSubjectEntry" %>

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
                tacSubjectCurriculum.setValue('');
                tacSubjectCurriculum.setText('');
                tacTeacher.setValue('');
                tacTeacher.setText('');
                $('#<%=txtNoMeetingHoursInWeek.ClientID %>').val('');
                $('#<%=txtPassingGrade.ClientID %>').val('0');
                cboCurriculumSubjectGroup.SetSelectedIndex(0);

                tacSubject.setEnabled(true);
                tacTeacher.setEnabled(true);
                $('#<%=txtNoMeetingHoursInWeek.ClientID %>').removeAttr('readonly');

                $('.chkIsCurriculumFinalMarkDefault input').each(function () {
                    $(this).prop('checked', true);
                    $(this).change();
                });

                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    var result = '';
                    $('.hdnCurriculumMarkTypeID').each(function () {
                        $tr = $(this).parent();
                        var idx = $tr.find('.hdnItemIndex').val();
                        var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
                        var formulaID = '';
                        if (cboCurriculumFinalMarkFormulaID.GetValue() != null)
                            formulaID = cboCurriculumFinalMarkFormulaID.GetValue();
                        if (result != '')
                            result += '|';
                        result += $tr.find('.hdnCurriculumMarkTypeID').val() + ';' + formulaID;
                    });
                    $('#<%=hdnSaveValue.ClientID %>').val(result);
                    cbpProcess.PerformCallback('save');
                }
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
            tacSubjectCurriculum.setValue(entity.SubjectCurriculumID);
            tacSubjectCurriculum.setText(entity.SubjectCurriculumName);
            tacTeacher.setValue(entity.TeacherID);
            tacTeacher.setText(entity.TeacherName);
            cboCurriculumSubjectGroup.SetValue(entity.CurriculumSubjectGroupID);
            $('#<%=txtNoMeetingHoursInWeek.ClientID %>').val(entity.NoMeetingHoursInWeek);
            $('#<%=txtPassingGrade.ClientID %>').val(entity.PassingGrade);
            if (entity.IsEditable == 'False') 
                $('#<%=txtNoMeetingHoursInWeek.ClientID %>').attr('readonly', 'readonly');
            else 
                $('#<%=txtNoMeetingHoursInWeek.ClientID %>').removeAttr('readonly');
            

            var filterExpression = "PeriodClassTypeSubjectID = " + entity.PeriodClassTypeSubjectID;
            Methods.getListObject('GetPeriodClassTypeSubjectFinalMarkFormulaList', filterExpression, function (result) {
                $('.hdnCurriculumMarkTypeID').each(function () {
                    $tr = $(this).closest('tr');
                    var idx = $tr.find('.hdnItemIndex').val();
                    var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
                    $chk = $tr.find('.chkIsCurriculumFinalMarkDefault input');
                    var isFound = false;
                    var curriculumMarkTypeID = $(this).val();
                    for (var i = 0; i < result.length; ++i) {
                        if (result[i].CurriculumMarkTypeID == curriculumMarkTypeID) {
                            isFound = true;
                            if (result[i].CurriculumFinalMarkFormulaID == null) {
                                $chk.prop('checked', true);
                                $chk.change();
                            }
                            else {
                                $chk.prop('checked', false);
                                $chk.change();
                                cboCurriculumFinalMarkFormulaID.SetValue(result[i].CurriculumFinalMarkFormulaID);
                            }
                        }
                    }
                    if (!isFound) {
                        $chk.prop('checked', true);
                        $chk.change();
                    }
                });
                $('#entryDetailContainer').show();
            });
        });

        //#endregion

        $('.btnCurriculumFinalMarkFormulaDt').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
            var id = cboCurriculumFinalMarkFormulaID.GetValue();
            if (id != null && id != '') {
                var url = ResolveUrl("~/Program/Master/SchoolPeriod/CurriculumFinalMarkFormulaDtCtl.ascx");
                openUserControlPopup(url, id, 'Detil Formula', 900, 400);
            }
        });

        $('.chkIsCurriculumFinalMarkDefault input').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);

            if ($(this).is(':checked')) {
                cboCurriculumFinalMarkFormulaID.SetEnabled(false);
                cboCurriculumFinalMarkFormulaID.SetValue('');
            }
            else
                cboCurriculumFinalMarkFormulaID.SetEnabled(true);
        });

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

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "1 = 0";
            var subjectID = tacSubject.getValue();
            if (subjectID != '')
                filterExpression = "SiteID = '" + $('#<%=hdnSiteID.ClientID %>').val() + "' AND SubjectID = " + subjectID;
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
    <input type="hidden" id="hdnSaveValue" runat="server" value="" />
    <input type="hidden" value="" id="hdnCurriculumID" runat="server" />
    <input type="hidden" value="" id="hdnSiteID" runat="server" />
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
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
                                    <td colspan="3">
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetvTeacherSubjectList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                                            SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
                                    <td colspan="3">
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacSubjectCurriculum" ClientInstanceName="tacSubjectCurriculum" MethodName="GetSubjectCurriculumList" GetFilterExpressionFunction="onGetSubjectCurriculumFilterExpression"
                                            SearchFields="SubjectCurriculumName,SubjectCurriculumCode" TextField="SubjectCurriculumName" ValueField="SubjectCurriculumID" SearchText="${SubjectCurriculumName} (<b>${SubjectCurriculumCode}</b>)" OrderByExpression="SubjectCurriculumName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCurriculumButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectCurriculumValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
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
                                    <td colspan="3"><h4><%=GetLabel("Formula Nilai Rapor") %></h4></td>
                                </tr>   
                                <asp:Repeater ID="rptFinalMarkFormula" runat="server" OnItemDataBound="rptFinalMarkFormula_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("CurriculumMarkTypeName")%></label></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                <input type="hidden" class="hdnCurriculumMarkTypeID" value='<%#Eval("CurriculumMarkTypeID") %>' />
                                                <dxe:ASPxComboBox ID="cboCurriculumFinalMarkFormulaID" runat="server" Width="100%" />
                                            </td>
                                            <td><input type="button" class="btnCurriculumFinalMarkFormulaDt btnMore" value="..." /></td>
                                            <td><asp:CheckBox ID="chkIsCurriculumFinalMarkDefault" CssClass="chkIsCurriculumFinalMarkDefault" runat="server" /><%=GetLabel("Default") %></td>
                                        </tr>  
                                    </ItemTemplate>
                                </asp:Repeater>    
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
                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran"/>
                                <asp:BoundField DataField="CurriculumSubjectGroupName" HeaderText="Jenis Pelajaran" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="TeacherName" HeaderText="Guru" HeaderStyle-Width="280px" />
                                <asp:BoundField DataField="SubjectCurriculumName" HeaderText="Jenis Kurikulum" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="NoMeetingHoursInWeek" HeaderText="Jam Pertemuan" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PassingGrade" HeaderText="KKM" HeaderStyle-Width="80px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;<%#Eval("IsEditable").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("PeriodClassTypeSubjectID") %>" bindingfield="PeriodClassTypeSubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectID") %>" bindingfield="SubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumID") %>" bindingfield="SubjectCurriculumID" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumName") %>" bindingfield="SubjectCurriculumName" />
                                        <input type="hidden" value="<%#Eval("TeacherID") %>" bindingfield="TeacherID" />
                                        <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
                                        <input type="hidden" value="<%#Eval("CurriculumSubjectGroupID") %>" bindingfield="CurriculumSubjectGroupID" />
                                        <input type="hidden" value="<%#Eval("NoMeetingHoursInWeek") %>" bindingfield="NoMeetingHoursInWeek" />
                                        <input type="hidden" value="<%#Eval("PassingGrade") %>" bindingfield="PassingGrade" />
                                        <input type="hidden" value="<%#Eval("IsEditable") %>" bindingfield="IsEditable" />
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