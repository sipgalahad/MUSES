<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ToDoListInCalendar.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ToDoListInCalendar" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
<link rel='stylesheet' href='<%= ResolveUrl("~/Libs/Scripts/Jquery/FullCalendar/fullcalendar.css")%>' />
<script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/Jquery/FullCalendar/lib/moment.min.js")%>'></script>
<script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/Jquery/FullCalendar/fullcalendar.js")%>'></script>

<script type="text/javascript">
    $(function () {
        $('#calendar').fullCalendar({
            header: {
                left: '',
                center: 'title',
                right: 'today prev,next'
            },
            height: 500,
            dayClick: function (date, jsEvent, view) {
                var url = ResolveUrl('~/Program/Proses/ToDoListInCalendar/ToDoListCalendarEntryCtl.ascx');
                var d = (new Date(date)).toISOString().slice(0, 10);
                var temp = d.split('-');
                var param = "|" + temp[2] + "-" + temp[1] + "-" + temp[0];
                openUserControlPopup(url, param, 'Log Entry', 700, 400);
            },
            eventClick: function (event, jsEvent, view) {
                $('#<%=hdnID.ClientID %>').val(event.id);
                pcProjectTask.Show();
            }
        });

        refreshCalendar();

        $('.fc-left').html('<span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />');

        $('#divTransactionAdd').click(function () {
            var url = ResolveUrl('~/Program/Proses/ToDoListInCalendar/ToDoListCalendarEntryCtl.ascx');
            var date = new Date();
            var temp = Methods.dateToYMD(date).split('-');
            var param = "|" + temp[2] + '-' + temp[1] + '-' + temp[0];
            openUserControlPopup(url, param, 'Log Entry', 700, 400);
        });
    });

    $('#btnPCEdit').die('click');
    $('#btnPCEdit').live('click', function () {
        var url = ResolveUrl('~/Program/Proses/ToDoListInCalendar/ToDoListCalendarEntryCtl.ascx');
        var id = $('#<%=hdnID.ClientID %>').val();
        openUserControlPopup(url, id, 'Log Entry', 700, 400);
    });

    $('#btnPCLog').die('click');
    $('#btnPCLog').live('click',function(){
        var url = ResolveUrl('~/Program/Proses/ToDoList/ProjectTaskLogEntryCtl.ascx');
        var id = $('#<%=hdnID.ClientID %>').val();
        openUserControlPopup(url, id, 'Log Entry', 700, 500);
    });

    $('.popupClose').die('click');
    $('.popupClose').live('click', function () {
        cancelPc();
    });

    function refreshCalendar() {
        $('#calendar').fullCalendar('removeEvents');
        var filterExpression = "<%=GetProjectTaskFilterExpression() %>";
        Methods.getListObject('GetvProjectTaskList', filterExpression, function (result) {
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    var event = new Object();
                    event.id = result[i].ProjectTaskID;
                    event.title = result[i].ProjectTaskName;
                    var date = result[i].StartDateInDatePicker;
                    var str = date.split('-');
                    event.start = str[2] + '-' + str[1] + '-' + str[0] + 'T' + result[i].StartTime + ":00";

                    date = result[i].EndDateInDatePicker;
                    str = date.split('-');
                    event.end = str[2] + '-' + str[1] + '-' + str[0] + 'T' + result[i].EndTime + ":00";
                    $('#calendar').fullCalendar('renderEvent', event, 'stick');
                }
            }
        });

        Methods.getListObject('GetvProjectList', '', function (result) {
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    var event = new Object();
                    event.id = result[i].ProjectID;
                    event.title = result[i].ProjectName;
                    var date = result[i].StartDateInDatePicker;

                    var str = date.split('-');
                    event.start = str[2] + '-' + str[1] + '-' + str[0];

                    date = result[i].EndDateInDatePicker;
                    str = date.split('-');
                    event.end = str[2] + '-' + str[1] + '-' + str[0];
                    event.rendering = 'background'
                    $('#calendar').fullCalendar('renderEvent', event, 'stick');
                }
            }
        });
    }

    function onAfterPopupControlClosing() {
        refreshCalendar();
    }

    function cancelPc() {
        pcProjectTask.Hide();
    }

    function onCbpViewEndCallback(s) {
        hideLoadingPanel();
    }
</script>
<input type="hidden" value="" id="hdnID" runat="server" />
<input type="hidden" id="hdnFilterExpression" runat="server" value="" />
<input type="hidden" id="hdnStartDate" runat="server" value="" />
<input type="hidden" id="hdnEndDate" runat="server" value="" />

<div class="divTransactionEntry">
    <div id='calendar'></div>
    <dx:ASPxPopupControl ID="pcProjectTask" runat="server" ClientInstanceName="pcProjectTask"
        height="150px" HeaderText="Project Task" AllowDragging="True" CloseAction="CloseButton" width="700px" Modal="True" PopupAction="None" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseButtonImage-Width="0">
        <ClientSideEvents Shown="function(s,e){showLoadingPanel(); cbpView.PerformCallback();}" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" ID="pccc1">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="text-align:center;width:100%;">
                                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                                position: relative; font-size: 0.95em;">
                                                <table class="tblEntryContent" style="width:90%">
                                                    <colgroup>
                                                        <col style="width:160px"/>
                                                        <col/>
                                                    </colgroup>
                                                    <tr>
                                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
                                                        <td><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Mulai")%></label></td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><asp:TextBox ID="txtPTStartDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                                                                    <td style="width:10px; text-align:center">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtPTStartTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Selesai")%></label></td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><asp:TextBox ID="txtPTEndDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                                                                    <td style="width:10px; text-align:center">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtPTEndTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                                        <td align="left"><asp:TextBox runat="server" ID="txtPTRemarks" TextMode="MultiLine" Rows="2" Width="300px" ReadOnly="true" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td align="left">
                                                            <input type="button" id="btnPCLog" style="width:100px" value='<%= GetLabel("Log")%>' />
                                                            <input type="button" id="btnPCEdit" style="width:100px" value='<%= GetLabel("Edit")%>' />
                                                            <%--<input type="button" id="btnPCCancel" style="width:100px" value='<%= GetLabel("Cancel")%>' />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
</asp:Content>
