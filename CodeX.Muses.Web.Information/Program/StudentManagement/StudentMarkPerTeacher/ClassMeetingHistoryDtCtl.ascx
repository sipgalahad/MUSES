<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassMeetingHistoryDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Information.Program.ClassMeetingHistoryDtCtl" %>

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
    <input type="hidden" id="hdnClassMeetingID" runat="server" />
    <input type="hidden" id="hdnSchoolClassID" runat="server" />
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
                                <h4 class="h4expanded"><%=GetLabel("Kehadiran")%></h4>                    
                                <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                                    <tr>
                                        <th rowspan="2"><%=GetLabel("Siswa") %></th>
                                        <th id="thHeaderAttendance" runat="server" class="thCenter"><%=GetLabel("STATUS KEHADIRAN") %></th>
                                        <th rowspan="2" style="width:200px"><%=GetLabel("Keterangan") %></th>
                                    </tr>
                                    <tr>
                                        <asp:Repeater ID="rptHeader" runat="server">
                                            <ItemTemplate>
                                                <th class="thCenter" style="width:50px">
                                                    <%#Eval("StandardCodeName") %><br />
                                                </th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="trStudent">
                                                <td class="keyField"><%#Eval("StudentID") %></td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 35px;">
                                                                <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                                <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                                <div class="gridCircle divStudentImage"></div>
                                                            </td>
                                                            <td>
                                                                <%#Eval("StudentName") %>
                                                                <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="center">
                                                            <div id="divAttendance" runat="server"></div>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td><asp:TextBox ID="txtRemarks" CssClass="txtRemarks" runat="server" Width="100%" /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</div>

