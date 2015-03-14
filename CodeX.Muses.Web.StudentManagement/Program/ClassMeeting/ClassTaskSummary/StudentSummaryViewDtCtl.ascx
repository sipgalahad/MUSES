<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentSummaryViewDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentSummaryViewDtCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_generatebilldtctl">
    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    var rowCount = parseInt('<%=RowCount %>');
    var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
    var currPage = parseInt('<%=CurrPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
        setPaging($("#pagingPopup"), pageCount, function (page) {
            cbpViewPopup.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
        }, null, currPage);
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpViewPopup.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion

    //#region Paging 2
    var pageCount = parseInt('<%=PageCount2 %>');
    var rowCount = parseInt('<%=RowCount2 %>');
    var rowCountPerPage = parseInt('<%=RowCountPerPage2 %>');
    var currPage = parseInt('<%=CurrPage2 %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup2'), rowCount, currPage, rowCountPerPage);
        setPaging($("#pagingPopup2"), pageCount, function (page) {
            cbpViewPopup2.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup2'), rowCount, page, rowCountPerPage);
        }, null, currPage);
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);
            setNumEntriesText($('#informationNumEntriesPopup2'), rowCount, currPage, rowCountPerPage);
            setPaging($("#pagingPopup2"), pageCount, function (page) {
                cbpViewPopup2.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup2'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion
    registerCollapseExpandHandler();
</script>
<input type="hidden" id="hdnStudentID" runat="server" />
<table class="tblEntryContent" style="width:70%">
    <colgroup>
        <col style="width:160px"/>
        <col/>
    </colgroup>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Siswa")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr> 
</table>

<h4 class="h4expanded"><%=GetLabel("Catatan Individu") %></h4>
<div class="containerTblEntryContent">
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpViewPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="StudentNoteID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="NoteDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteTime" HeaderText="Jam" HeaderStyle-Width="80px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
                    </Columns>
                    <EmptyDataTemplate>
                        <%=GetLabel("No Data To Display")%>
                    </EmptyDataTemplate>
                </asp:GridView>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
        <div class="wrapperPaging">
            <div id="pagingPopup"></div>
        </div>
    </div> 
</div>
<br />
<h4 class="h4expanded"><%=GetLabel("Informasi Kehadiran") %></h4>
<div class="containerTblEntryContent">
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup2" runat="server" Width="100%" ClientInstanceName="cbpViewPopup2"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup2_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpViewPopup2EndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server">
                <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                    <tr>
                        <asp:Repeater ID="rptHeader" runat="server">
                            <ItemTemplate>
                                <th class="thCenter" style="width:90px">
                                    <%#Eval("MeetingDate", "{0:dd-MM-yy}") %><br />
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                            <ItemTemplate>
                                <td align="center">
                                    <div id="divStudentAttendance" runat="server"></div>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntriesPopup2"></div>
        <div class="wrapperPaging">
            <div id="pagingPopup2"></div>
        </div>
    </div> 
</div>