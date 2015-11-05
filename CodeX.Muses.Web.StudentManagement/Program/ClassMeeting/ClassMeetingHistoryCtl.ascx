<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassMeetingHistoryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassMeetingHistoryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $('#ulMeetingViewList li').live('click', function () {
        var id = $(this).find('.hdnClassMeetingID').val();
        $('#<%=hdnClassMeetingID.ClientID %>').val(id);
        $('#ulMeetingViewList li.selected').removeClass('selected');
        $(this).addClass('selected');
        cbpMeetingDetail.PerformCallback();
    });

    function onHistoryInit() {
        if ($('#ulMeetingViewList li').length > 0)
            $('#ulMeetingViewList li:eq(0)').click();

        $('#lblDetail').click(function () {
            var id = $('#<%=hdnPeriodSection.ClientID %>').val() + '|' + $('#<%=hdnClassSubjectID.ClientID %>').val() + '|' + $('#<%=hdnClassScheduleID.ClientID %>').val() + '|' + $('#<%=hdnClassMeetingID.ClientID %>').val();
            var url = ResolveUrl('~/Program/ClassMeeting/ClassMeetingPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'ClassMeeting', '1300', '650');
        });
        $('#lblAddData').click(function () {
            var id = $('#<%=hdnPeriodSection.ClientID %>').val() + '|' + $('#<%=hdnClassSubjectID.ClientID %>').val() + '|' + $('#<%=hdnClassScheduleID.ClientID %>').val() + '|0';
            var url = ResolveUrl('~/Program/ClassMeeting/ClassMeetingPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'ClassMeeting', '1300', '650');
        });
    }
    
    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    var rowCount = parseInt('<%=RowCount %>');
    var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
    var currPage = parseInt('<%=CurrPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
        setPaging($("#paging"), pageCount, function (page) {
            cbpViewPopup.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
        }, null, currPage);
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpViewPopup.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }
        else
            $('#ulMeetingViewList li:eq(0)').click();
    }
    //#endregion

    function onCbpMeetingDetailEndCallback(s) {
        registerCollapseExpandHandler();
        hideLoadingPanel();
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnPeriodSection" runat="server" />
    <input type="hidden" id="hdnClassSubjectID" runat="server" />
    <input type="hidden" id="hdnClassScheduleID" runat="server" />
    <input type="hidden" id="hdnClassMeetingID" runat="server" />
    <style type="text/css">
        #ulMeetingViewList .divMeetingDate        { float: left; width: 66px; margin: 3px 10px 0 0; background-color: #6BBD46; padding: 3px 10px; font-size: 20px; color: White; vertical-align: middle; text-align: center; }
        #ulMeetingViewList li                          { padding: 5px 3px; cursor: pointer; list-style-type:none; margin-bottom: 1px; }
        #ulMeetingViewList li.selected                 { background-color: #D5D5D5; }
        #ulMeetingViewList li:hover                    { background-color: #BCBCBC; }
        #ulMeetingViewList                             { margin: 0; padding: 0; }
        #ulMeetingViewList .tdMeetingDetail       { padding-left: 5px; }
    
        h4                                                  { color: #013EDD; }
    </style>
    <table style="width:100%">
        <colgroup>
            <col style="width:330px"/>
        </colgroup>
        <tr>
            <td valign="top">
                <label class="lblLink" id="lblAddData"><%=GetLabel("Tambah Data")%></label>
                <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                    ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:Repeater ID="rptMeetingView" runat="server">
                                <HeaderTemplate>
                                    <ul id="ulMeetingViewList">                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <input type="hidden" value='<%# Eval("ClassMeetingID") %>' class="hdnClassMeetingID" />
                                        <div class="divMeetingDate"><%# Eval("MeetingDate", "{0:dd MMM}")%><br /><%# Eval("MeetingDate", "{0:yyyy}")%></div>
                                        <div style="font-size: 16px; font-weight: 100;"><%#Eval("cfTeacherName") %></div>
                                        <div style="font-size: 12px;"><%#Eval("RoomName") %><br /><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                    </li>                        
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>  
                <div class="containerPaging">
                    <div class="divInformationNumEntries" id="informationNumEntries"></div>
                    <div class="wrapperPaging">
                        <div id="paging"></div>
                    </div>
                </div> 
            </td>
            <td valign="top">
                <label class="lblLink" id="lblDetail"><%=GetLabel("Lihat Detil Pertemuan") %></label>
                <dxcp:ASPxCallbackPanel ID="cbpMeetingDetail" runat="server" Width="100%" ClientInstanceName="cbpMeetingDetail"
                    ShowLoadingPanel="false" OnCallback="cbpMeetingDetail_Callback">
                    <ClientSideEvents Init="function(s,e){ onHistoryInit(); }" BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e) { onCbpMeetingDetailEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="height: 415px; overflow-y: scroll; overflow-x: hidden; font-size: 12px;">
                                <h4 class="h4expanded"><%=GetLabel("Ringkasan Pertemuan")%></h4>                            
                                <div class="containerTblEntryContent">
                                    <table style="width:95%">
                                        <colgroup>
                                            <col style="width:130px;" />
                                        </colgroup>
                                        <tr>
                                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan")%></label></td>
                                            <td><asp:TextBox ID="txtRemarks" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan Berikutnya")%></label></td>
                                            <td><asp:TextBox ID="txtNextMeetingRemarks" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</div>

