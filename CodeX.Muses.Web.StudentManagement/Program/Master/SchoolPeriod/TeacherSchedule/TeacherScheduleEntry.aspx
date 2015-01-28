<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="TeacherScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherScheduleEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
            cbpClassSubject.PerformCallback('refresh');
        }

        $('.tblSchedule tr.T001 td.tdHtmlText').live('click', function (e) {
            if (e.target !== this)
                return;
            var teacherID = $('#hdnSelectedTrTeacherID').val();
            if (teacherID != '') {
                $tr = $(this).parent();
                var teacherName = $('#lblTeacher').html();
                $(this).find('.tblTeacherDt').append('<tr><td><div style="float:right" class="divDetailDelete"></div><input type="hidden" value="' + teacherID + '" class="hdnTeacherID"/>' + teacherName + '</td></tr>');

                $tr.find('.tdValue').html(teacherID);
            }
        });

        $('.divDetailDelete').live('click', function (e) {
            $tr = $(this).closest('tr');
            $tr.remove();
        });

        var isChangePage = false;
        function onBeforeChangePage() {
            isChangePage = true;
            $('#<%=btnSave.ClientID %>').click();
        }

        function onAfterCustomClickSuccess() {
            if (isChangePage)
                goToNextPage();
        }

        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.tblTeacherDt tr').each(function () {
                    var teacherID = $(this).find('.hdnTeacherID').val();
                    $trParent = $(this).closest('table').parent().closest('tr');
                    var hoursIndex = $trParent.find('.tdHoursIndex').html();
                    var dayNumber = $trParent.find('.tdDayNumber').html();

                    if (result != "")
                        result += "|";
                    result += teacherID + ',' + hoursIndex + ',' + dayNumber;
                });
                $('#<%=hdnSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });

            function onGetTeacherFilterExpression() {
                var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
                return filterExpression;
            }

            $('#lblChangeTeacher').click(function () {
                openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                    var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                    Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                        $('#hdnSelectedTrTeacherID').val(result.TeacherID);
                        $('#tdSelectedTeacher').html("<label class='lblLink' id='lblTeacher'>" + result.TeacherName + "</label>");
                    });
                });
            });
        });

        $('#lblTeacher.lblLink').live('click', function () {
            var teacherID = $('#hdnSelectedTrTeacherID').val();
            if (teacherID != '') {
                var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSchedule/TeacherScheduleDtCtl.ascx");
                openUserControlPopup(url, teacherID, 'Jadwal Guru', 1250, 550);
            }
        });
    </script>
    <input type="hidden" runat="server" id="hdnSaveValue" />

    <input type="hidden" id="hdnSelectedTrTeacherID" value="" />
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 100px; cursor: pointer; }
        .tblSchedule tr.T001 .divTime, .nts001                   { background-color: #2FD933; }
        .tblSchedule tr.T001 b              { color: Red; font-weight: normal; }
        
        .tblSchedule tr.T001 .tdHtmlText    { background-color: #FFD837 !important; }
    </style>

    <table style="float:right; border: 1px solid black">
        <colgroup>
            <col style="width:150px" />
            <col style="width:350px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblLink" id="lblChangeTeacher" style="font-weight: bold;"><%=GetLabel("Dipilih") %></label> :</td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Guru") %> :</td>
            <td id="tdSelectedTeacher">&nbsp;</td>
        </tr>
    </table>
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
                                    <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">1</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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
                                    <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">2</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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
                                    <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">3</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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
                                    <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">4</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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
                                    <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">5</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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
                                    <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>' valign="top">
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">6</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float:right" class="divDetailDelete"></div>
                                                                        <input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
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