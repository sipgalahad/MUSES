<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ExtracurricularWeeklyScheduleList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ExtracurricularWeeklyScheduleList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpView.PerformCallback('refresh');
        }

        $('.T999').live('click', function () {
            //var id = 7 + '|' + 2 + '|' + 0;
            //var url = ResolveUrl('~/Program/WeeklySchedule/ClassMeetingPageLauncher.aspx?id=' + id);
            //openWindowPopup(url, 'ClassMeeting', '1300', '650');
            var classSubjectID = $(this).find('.tdClassSubjectID').html();
            var classScheduleID = $(this).find('.tdClassScheduleID').html();
            if (classScheduleID != '') {
                var id = tacPeriodSection.getValue() + '|' + classSubjectID + '|' + classScheduleID;
                var url = ResolveUrl("~/Program/ClassMeeting/ClassMeetingHistoryCtl.ascx");
                openUserControlPopup(url, id, 'Riwayat Pertemuan', 1000, 550);
            }
        });

        function onCboSiteValueChanged() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNowFilterExpression() %>";
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                if (result != null) {
                    tacSchoolPeriod.setValue(result.SchoolPeriodID);
                    tacSchoolPeriod.setText(result.SchoolPeriodName);
                }
                else {
                    tacSchoolPeriod.setValue('');
                    tacSchoolPeriod.setText('');
                }
                onTacSchoolPeriodValueChanged();
            });
        }

        //#region School Period
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "'";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                    }
                    onTacSchoolPeriodValueChanged();
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
            var filterExpression = onGetPeriodSectionFilterExpression() + " AND <%=OnGetPeriodSectionNowFilterExpression() %>";
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
        }
        //#endregion

        //#region Period Section
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND <%=OnGetPeriodSectionFilterExpression() %>";
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
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeExtracurricular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }
    </script>
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                    SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
    </table>
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T999                { height: 75px; cursor: pointer; }
        .tblSchedule tr.T999 td, .nts999    { background-color: #A32FD9; }
    </style>

    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <table style="width:100%">
                            <tr>
                                <td valign="top" id="tdSchoolDay1" runat="server">
                                    <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                                    <asp:Repeater ID="rptDay1" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay2" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay3" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay4" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay5" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay6" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T999'>
                                                <td style="display:none" class="tdClassSubjectID"><%#Eval("ClassSubjectID") %></td>
                                                <td style="display:none" class="tdClassScheduleID"><%#Eval("ClassScheduleID") %></td>
                                                <td id="tdHtmlText" runat="server">
                                                    <%#Eval("StartTime") %> - <%#Eval("EndTime") %><br />
                                                    <%#Eval("SchoolClassName") %><br />
                                                    (<b><%#Eval("SubjectName")%></b>)<br />
                                                    <%#Eval("RoomName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>