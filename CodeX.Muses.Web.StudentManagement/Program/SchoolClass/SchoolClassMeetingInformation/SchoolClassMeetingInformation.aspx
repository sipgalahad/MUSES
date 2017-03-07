<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolClassPageTrx.master" AutoEventWireup="true" EnableEventValidation="false"  
    CodeBehind="SchoolClassMeetingInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassMeetingInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('#ulMeetingViewList li').live('click', function () {
            var id = $(this).find('.hdnClassMeetingID').val();
            $('#<%=hdnClassMeetingID.ClientID %>').val(id);
            $('#ulMeetingViewList li.selected').removeClass('selected');
            $(this).addClass('selected');
            cbpMeetingDetail.PerformCallback();
        });

        function onCbpMeetingDetailEndCallback(s) {
            registerCollapseExpandHandler();
            hideLoadingPanel();
        }

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

        function onCboSubjectValueChanged() {
            $('#<%=hdnSubject.ClientID %>').val(cboSubject.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCbpViewEndCallback(s) {
            setStudentImage();
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
    </script>
    <style type="text/css">
        #ulMeetingViewList .divMeetingDate        { float: left; width: 66px; margin: 3px 10px 0 0; background-color: #6BBD46; padding: 3px 10px; font-size: 20px; color: White; vertical-align: middle; text-align: center; }
        #ulMeetingViewList li                          { padding: 5px 3px; cursor: pointer; list-style-type:none; margin-bottom: 1px; }
        #ulMeetingViewList li.selected                 { background-color: #D5D5D5; }
        #ulMeetingViewList li:hover                    { background-color: #BCBCBC; }
        #ulMeetingViewList                             { margin: 0; padding: 0; }
        #ulMeetingViewList .tdMeetingDetail       { padding-left: 5px; }
    
        h4                                                  { color: #013EDD; }
    </style>
    <table cellspacing="0">
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td>    
                <input type="hidden" id="hdnSubject" runat="server" />
                <dxe:ASPxComboBox runat="server" ID="cboSubject" ClientInstanceName="cboSubject" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboSubjectValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdnClassMeetingID" runat="server" />
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView">
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
                                        <dx:PanelContent ID="PanelContent3" runat="server">
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
                                        <dx:PanelContent ID="PanelContent4" runat="server">
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
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>  
</asp:Content>