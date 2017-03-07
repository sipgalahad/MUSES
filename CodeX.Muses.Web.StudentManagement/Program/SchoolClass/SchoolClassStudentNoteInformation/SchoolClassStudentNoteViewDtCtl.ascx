<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolClassStudentNoteViewDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassStudentNoteViewDtCtl" %>

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

    function onCbpViewPopup2EndCallback(s) {
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
</script>
<input type="hidden" id="hdnStudentID" runat="server" />
<input type="hidden" id="hdnNoteCategory" runat="server" />
<input type="hidden" id="hdnNoteRate" runat="server" />
<table class="tblEntryContent" style="width:70%">
    <colgroup>
        <col style="width:160px"/>
        <col/>
    </colgroup>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Siswa")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr> 
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kategori")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtNoteCategory" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtNoteRate" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr> 
</table>

<div class="containerTblEntryContent">
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpViewPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server">
                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="StudentNoteID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="NoteDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteTime" HeaderText="Jam" HeaderStyle-Width="80px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteCategory" HeaderText="Kategori" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="NoteRate" HeaderText="Nilai" HeaderStyle-Width="150px" />
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