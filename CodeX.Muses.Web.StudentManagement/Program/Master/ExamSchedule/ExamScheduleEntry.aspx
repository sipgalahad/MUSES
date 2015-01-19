<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="ExamScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ExamScheduleEntry" %>

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
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#btnGenerate').click(function () {
                if (IsValid(null, 'fsFilterGenerate', 'mpFilterGenerate'))
                    cbpView.PerformCallback('refresh|0');
            });

            $('#btnExamSchedulePackageDt').click(function () {
                var schedulePackage = cboExamSchedulePackage.GetValue();
                if (schedulePackage != null && schedulePackage != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
                    openUserControlPopup(url, schedulePackage, 'Jadwal', 1000, 550);
                }
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    getSaveValue();
                    onCustomButtonClick('save');
                }
            });
        });

        function getSaveValue() {
            var result = '';
            $('#<%=grdSubject.ClientID %> tr:gt(0)').each(function () {
                var subjectID = $(this).find('.keyField').html();
                var examDate = $(this).find('.divExamDate').html();
                var hoursIndex = $(this).find('.divHoursIndex').html();
                if (examDate != '') {
                    if (result != "")
                        result += "|";
                    result += subjectID + ',' + examDate + ',' + hoursIndex;
                }
            });
            $('#<%=hdnSaveValue.ClientID %>').val(result);
        }

        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
            tacSchoolClass.setValue('');
            tacSchoolClass.setText(''); 
            //cbpView.PerformCallback('refresh');
        }

        //#region Period Section
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND <%=OnGetPeriodSectionFilterExpression() %>";
            return filterExpression;
        }

        function onTacPeriodSectionButtonSearchClick() {
            openSearchDialog('periodsection', onGetPeriodSectionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodSectionFilterExpression() + " AND PeriodSectionCode = '" + value + "'";
                Methods.getObject('GetPeriodSectionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodSection.setValue(result.PeriodSectionID);
                        tacPeriodSection.setText(result.PeriodSectionName);
                    }
                    else {
                        tacPeriodSection.setValue('');
                        tacPeriodSection.setText('');
                    }
                    onTacPeriodSectionValueChanged();
                });
            });

        }

        function onTacPeriodSectionValueChanged() {
            //cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Class Type
        function onGetClassTypeFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassTypeButtonSearchClick() {
            openSearchDialog('periodclasstype', onGetClassTypeFilterExpression(), function (value) {
                var filterExpression = onGetClassTypeFilterExpression() + " AND ClassTypeCode = '" + value + "'";
                Methods.getObject('GetvPeriodClassTypeList', filterExpression, function (result) {
                    if (result != null) {
                        tacClassType.setValue(result.PeriodClassTypeID);
                        tacClassType.setText(result.ClassTypeName);
                    }
                    else {
                        tacClassType.setValue('');
                        tacClassType.setText('');
                    }
                    onTacClassTypeValueChanged();
                });
            });

        }

        function onTacClassTypeValueChanged() {
            //cbpView.PerformCallback('refresh');
        }
        //#endregion

        $('#<%=grdSubject.ClientID %> tr:gt(0)').live('click', function () {
            $('#<%=grdSubject.ClientID %> tr.selected').removeClass('selected');
            $(this).addClass('selected');
        });

        $('.tblSchedule tr.T001 td.tdHtmlText').live('click', function (e) {
            if (e.target !== this)
                return;
            if ($('#<%=grdSubject.ClientID %> tr.selected') != null) {
                $tr = $(this).parent();
                if ($tr.find('.tdValue').html() != '') {
                    $tr.find('.divDetailDelete').click();
                }

                var examDate = $('#<%=grdSubject.ClientID %> tr.selected').find('.divExamDate').html();
                if (examDate != '') {
                    var hoursIndex = $('#<%=grdSubject.ClientID %> tr.selected').find('.divHoursIndex').html();
                    $('.ulSchedule li').each(function () {
                        if ($(this).find('h4').html() == examDate) {
                            $(this).find('.tblSchedule').find('tr').each(function () {
                                if ($(this).find('.tdHoursIndex').html() == hoursIndex) {
                                    $(this).find('.divDetailDelete').click();
                                }
                            });
                        }
                    });
                }

                var text = $('#<%=grdSubject.ClientID %> tr.selected').find('.tdSubjectName').html();
                $(this).html('<div style="float:right" class="divDetailDelete"></div><b>' + text + '</b>');

                var examDate = $(this).closest('table').prev().html();
                var examTime = $(this).parent().find('.tdDefaultHtml').html();
                var hoursIndex = $(this).parent().find('.tdHoursIndex').html();
                $('#<%=grdSubject.ClientID %> tr.selected').find('.divExamDate').html(examDate);
                $('#<%=grdSubject.ClientID %> tr.selected').find('.divHoursIndex').html(hoursIndex);
                $('#<%=grdSubject.ClientID %> tr.selected').find('.divExamDateTime').html(examDate + ' (' + examTime + ')');

                $tr.find('.tdValue').html($('#<%=grdSubject.ClientID %> tr.selected').find('.keyField').html());
            }
        });

        $('.divDetailDelete').live('click', function (e) {
            $trDeleted = $(this).closest('tr');
            $tdValue = $trDeleted.find('.tdValue');
            var subjectID = $tdValue.html();
            $tdValue.html('');
            $trDeleted.find('.tdHtmlText').html($trDeleted.find('.tdDefaultHtml').html());

            var isFound = false;
            $('#<%=grdSubject.ClientID %> tr:gt(0)').each(function () {
                if (!isFound) {
                    var subjectID1 = $(this).find('.keyField').html();
                    if (subjectID1 == subjectID) {
                        isFound = true;
                        $(this).find('.divExamDateTime').html('');
                        $(this).find('.divExamDate').html('');
                        $(this).find('.divHoursIndex').html('');
                    }
                }
            });
        });
    </script>
    
    <style type"text/css">
        .ulSchedule                         { width: 100%; }
        .ulSchedule li                      { width: 180px; display: inline-block; text-align: center; margin-bottom: 30px; }
        
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 56px; cursor: pointer; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #2FD933; }
        .tblSchedule tr.T001 b              { color: Red; font-weight: normal; }
    </style>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <fieldset id="fsFilterGenerate">
        <table style="width:100%">
            <colgroup>
                <col style="width:150px"/>
                <col style="width:300px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Ajaran") %></label></td>
                <td colspan="2">
                    <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                        <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                    </dxe:ASPxComboBox> 
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
                <td colspan="2">
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                        SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                            ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                <td colspan="2">
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacClassType" ClientInstanceName="tacClassType" MethodName="GetvPeriodClassTypeList" GetFilterExpressionFunction="onGetPeriodClassTypeFilterExpression"
                        SearchFields="ClassTypeName,ClassTypeCode" TextField="ClassTypeName" ValueField="ClassTypeID" SearchText="${ClassTypeName} (<b>${ClassTypeCode}</b>)" OrderByExpression="ClassTypeName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacClassTypeButtonSearchClick(); }"
                            ValueChanged="function(){ onTacClassTypeValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Ujian")%></label></td>
                <td colspan="2"><dxe:ASPxComboBox runat="server" ID="cboExaminationType" ClientInstanceName="cboExaminationType" Width="300px" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal Ujian")%></label></td>
                <td><dxe:ASPxComboBox runat="server" ID="cboExamSchedulePackage" ClientInstanceName="cboExamSchedulePackage" Width="300px" /></td>
                <td><input type="button" id="btnExamSchedulePackageDt" class="btnMore" value="..." /></td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Ujian")%></label></td>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col style="width:145px"/>
                            <col style="width:5px"/>
                            <col style="width:145px"/>
                        </colgroup>
                        <tr>
                            <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker" Width="120px" /></td>
                            <td>&nbsp;</td>
                            <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker" Width="120px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">&nbsp;</td>
                <td><input type="button" id="btnGenerate" value='<%=GetLabel("Generate") %>' /></td>
            </tr>
        </table>
    </fieldset>
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView">
                    <table style="width:100%">
                        <tr>
                            <td valign="top" style="width:500px;">
                                <asp:GridView ID="grdSubject" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" ItemStyle-CssClass="tdSubjectName" />
                                        <asp:TemplateField HeaderText="Tanggal / Jam" HeaderStyle-Width="250px">
                                            <ItemTemplate>
                                                <div class="divExamDateTime"></div>
                                                <div class="divExamDate" style="display:none"></div>
                                                <div class="divHoursIndex" style="display:none"></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:Repeater ID="rptSchedule" runat="server" OnItemDataBound="rptSchedule_ItemDataBound">
                                    <HeaderTemplate>
                                        <ul class="ulSchedule">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <h4 style="text-align: center"><%#Eval("ExamDate","{0:dd-MM-yyyy}") %></h4>
                                            <asp:Repeater ID="rptScheduleDt" runat="server">
                                                <HeaderTemplate>
                                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                        <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                        <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                        <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
                                                        <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                        <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>    
</asp:Content>