<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPMain.master" AutoEventWireup="true" 
    CodeBehind="StudentClassInformation.aspx.cs" Inherits="CodeX.Muses.Web.Mobile.Program.StudentClassInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        Date.prototype.yyyymmdd = function () {
            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = this.getDate().toString();
            return yyyy + (mm[1] ? mm : "0" + mm[0]) + (dd[1] ? dd : "0" + dd[0]); // padding
        };

        var lstHoliday = [];
        var lstEntity = [];
        function setListEntitySchedule() {
            var year = $('#<%=hdnYear.ClientID %>').val();
            var month = $('#<%=hdnMonth.ClientID %>').val();
            if (month != '') {
                var date = new Date();
                date.setDate(1);
                date.setMonth(month - 1);
                date.setYear(year);
                $('#calSchedule').datepicker("setDate", date);
            }
        }

        function setCalSchedule() {
            var temp = $('#<%=hdnMaxDate.ClientID %>').val().split('-');
            var maxDate = new Date(temp[0], temp[1], temp[2]);
            temp = $('#<%=hdnMinDate.ClientID %>').val().split('-');
            var minDate = new Date(temp[0], temp[1], temp[2]);

            $('#calSchedule').datepicker({
                inline: true,
                minDate: minDate,
                maxDate: maxDate,
                beforeShowDay: function (date) {
                    var theday = date.yyyymmdd();
                    for (var i = 0; i < lstEntity.length; ++i) {
                        var entity = lstEntity[i];
                        if (theday >= entity.StartDateyyyyMMdd && theday <= entity.EndDateyyyyMMdd)
                            return [true, "date" + entity.cfGCPeriodScheduleType, entity.PeriodScheduleName];
                    }

                    for (var i = 0; i < lstHoliday.length; ++i) {
                        var entity = lstHoliday[i];
                        if ((entity.IsAnnualHoliday && entity.HolidayDate == date.getDate() && entity.HolidayMonth == date.getMonth() + 1)
                           || (!entity.IsAnnualHoliday && entity.HolidayDate == date.getDate() && entity.HolidayMonth == date.getMonth() + 1 && entity.HolidayYear == date.getFullYear()))
                            return [true, "specialDate", entity.HolidayName];
                    }

                    //if (date.getDay() < 1)
                    //    return [true, "specialDate"];
                    return [true, "", "Kegiatan Belajar Mengajar"];
                },
                onChangeMonthYear: function (year, month, instance) {
                    $('#<%=hdnYear.ClientID %>').val(year);
                    $('#<%=hdnMonth.ClientID %>').val(month);
                }
            });
        }
        $(function () {
            Methods.getListObject('GetHolidayList', 'IsDeleted = 0', function (result) {
                lstHoliday = result;

                lstEntity = [];
                Methods.getListObject('GetvPeriodScheduleList', "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND IsDeleted = 0", function (result) {
                    lstEntity = result;
                    setListEntitySchedule();
                    setCalSchedule();
                });
            });

            setDatePicker('<%=txtSchoolDate.ClientID %>');

            $('#<%=txtSchoolDate.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
            var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");
            $('#<%=imgPatientImage.ClientID %>').each(function () {
                $('#divImageHeaderBannerPreview').attr('style', "background-image:url('" + this.src + "')");
                $(this).error(function () {
                    var gender = $(this).attr('gender');
                    if (gender == '0003^F')
                        $('#divImageHeaderBannerPreview').attr('style', "background-image:url('" + imgUrlF + "')")
                    else
                        $('#divImageHeaderBannerPreview').attr('style', "background-image:url('" + imgUrlM + "')");
                }).attr('src', this.src);
            });

            $('#ulTabMenuLevel2 li').click(function () {
                $('#ulTabMenuLevel2 li.selected').removeClass('selected');
                $(this).addClass('selected');

                $('.divContent:visible').hide();
                $('#' + $(this).attr('contentid')).show();
            });
        });

        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
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
        }
        //#endregion

        $(function () {
            registerCollapseExpandHandler();
        });
    </script>
    <style type="text/css">
        #tblBill tr th, #tblBill tr td          { border: 1px solid #EAEAEA; font-size: 16px; padding: 2px 5px; word-wrap:break-word; }
        #tblBill tr th                          { background-color: #AAA; }
        #tblBill                                { table-layout: fixed; }
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 75px; cursor: pointer; }
        .tblSchedule tr.T001 b              { color: Red; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #2FD933; }
        
        .specialDate a.ui-state-default, .specialDate a.ui-state-hover       { background: #FF1901 !important; color: White; }
        
        <asp:Repeater ID="rptDateStyle" runat="server">
            <ItemTemplate>
                .nts<%#Eval("cfStandardCodeID")%>, .date<%#Eval("cfStandardCodeID")%> a.ui-state-default, .date<%#Eval("cfStandardCodeID")%> a.ui-state-hover      { background: <%#Eval("TagProperty")%> !important; }
            </ItemTemplate>
        </asp:Repeater>
    </style>
    <input type="hidden" runat="server" id="hdnStudentID" />
    <input type="hidden" runat="server" id="hdnSiteID" />
    <input type="hidden" runat="server" id="hdnSchoolClassID" />
    <input type="hidden" id="hdnMonth" runat="server" />
    <input type="hidden" id="hdnYear" runat="server" />
    <input type="hidden" id="hdnMaxDate" runat="server" />
    <input type="hidden" id="hdnMinDate" runat="server" />
    
    <div id="divContainerBanner" style="margin: -10px;padding:10px; padding-bottom:0px; margin-bottom:0px;">
        <table style="width:100%;">
            <tr>
                <td style="width:100px">
                    <img src="" id="imgPatientImage" style="display:none" runat="server" />
                    <table cellpadding="0" cellspacing="0" style="">
                        <tr>
                            <td class="tdPatientPhotoContainer1">
                                <div class="circleBanner" id="divImageHeaderBannerPreview"></div>
                                <input type="hidden" id="hdnPatientGender" runat="server" class="hdnPatientGender" />
                                <img id="imgPatientProfilePicture" class="imgPatientProfilePicture" runat="server" src='' alt="" width="240" height="280" style="position:absolute; top: 10px; display: none" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:400px">
                    <div>Parent Of</div>
                    <h3 id="h3Title" runat="server" style="font-weight:normal;"></h3>
                    <table cellpadding="0" cellspacing="0" id="tblMPBaseDetailPageTitle">
                        <tr>
                            <td><div id="divClass" runat="server" /></td>
                            <td style="padding-left:2px;"><div id="divPeriodSection" runat="server" /></td>
                            <td style="padding-left:2px;"><div id="divSchoolPeriod" runat="server" /></td>
                        </tr>
                    </table>
                </td>
                <td valign="bottom">            
                    <div style="width:100%; text-align:right">
                        <ul class="ulTabMenuLevel2" id="ulTabMenuLevel2" style="">
                            <li class="selected" contentid="divContentOverview">Overview</li>
                            <li>Task</li>
                            <li contentid="divContentViewAttendance">Attendance</li>
                            <li contentid="divContentAnnouncement">Announcement</li>
                            <li contentid="divContentTimetable">Timetable</li>
                            <li contentid="divContentAcademicSchedule">Academic Calender</li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
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
    </table>
    
    <div class="divContent" id="divContentTimetable" style="display:none">
        <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto;
            position: relative; font-size: 0.95em;">
            <table style="width:100%">
                <tr>
                    <td valign="top" id="tdSchoolDay1" runat="server">
                        <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                        <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top" id="tdSchoolDay2" runat="server"> 
                        <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                        <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top" id="tdSchoolDay3" runat="server"> 
                        <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                        <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top" id="tdSchoolDay4" runat="server"> 
                        <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                        <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top" id="tdSchoolDay5" runat="server"> 
                        <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                        <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top" id="tdSchoolDay6" runat="server"> 
                        <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                        <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                            <HeaderTemplate>
                                <table class="tblSchedule" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                    <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                                    <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                                    <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
    </div>

    <div class="divContent" id="divContentAnnouncement" style="display:none">
        <asp:Repeater ID="rptAnnouncement" runat="server">
            <ItemTemplate>
                <h4 class="h4expanded"><%#Eval("Title")%></h4>
                <div class="containerTblEntryContent">
                    <div><%#Eval("Remarks")%></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="divContent" id="divContentAcademicSchedule" style="display:none">
        <div id="calSchedule"></div>                
        <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
        <asp:Repeater ID="rptRemarks" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><div class='nts<%#Eval("cfStandardCodeID") %>' style="width: 20px; height: 20px; border: 1px solid black;"></div></td>
                    <td><%#Eval("StandardCodeName") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div class="divContent" id="divContentViewAttendance" style="display:none">    
        <div style="width:1250px; overflow-x: auto;"> 
            <asp:Panel runat="server" ID="Panel2">
                <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                    <tr>
                        <th rowspan="2"><%=GetLabel("Mata Pelajaran") %></th>
                        <th id="thHeaderAttendance" runat="server" class="thCenter"><%=GetLabel("STATUS KEHADIRAN") %></th>
                    </tr>
                    <tr>
                        <asp:Repeater ID="rptHeader" runat="server">
                            <ItemTemplate>
                                <th class="thCenter" style="width:100px">
                                    <%#Eval("StandardCodeName") %><br />
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <asp:Repeater ID="rptSubject" runat="server" OnItemDataBound="rptSubject_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("SubjectName") %></td>
                                <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                                    <ItemTemplate>
                                        <td align="center">
                                            <input type="hidden" class="hdnAttendanceStatus" value='<%#Eval("StandardCodeID") %>' />
                                            <label class="lblAttendance lblLink"><div id="divStudentAttendance" runat="server"></div></label>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:Panel>
        </div>
    </div>
    
    <div class="divContent" id="divContentOverview">
        <table>        
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal")%></label></td>
                <td><asp:TextBox ID="txtSchoolDate" runat="server" CssClass="datepicker" Width="120px" /></td>
            </tr>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <div style="padding: 5px;" runat="server" id="divBill">
                            <div style="font-size:16px;">Status Kehadiran : <span id="spnAttendanceStatus" runat="server">-</span></div>
                            <table cellpadding="0" cellspacing="0" id="tblBill">
                                <colgroup>
                                    <col style="width:100px"/>
                                    <col style="width:200px"/>
                                    <col style="width:200px"/>
                                    <col style="width:80px"/>
                                </colgroup>
                                <tr>
                                    <th class="thLeft">Mata Pelajaran</th>
                                    <th>Catatan</th>
                                    <th>Catatan Pertemuan Selanjutnya</th>
                                    <th>Kehadiran</th>
                                </tr>
                                <asp:Repeater ID="rptClassMeeting" runat="server" OnItemDataBound="rptClassMeeting_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("SubjectName") %></td>
                                            <td><%#Eval("Remarks") %></td>
                                            <td><%#Eval("NextMeetingRemarks")%></td>
                                            <td><div id="divAttendanceStatus" runat="server">-</div></td>
                                        </tr>    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel> 
    </div>   
</asp:Content>