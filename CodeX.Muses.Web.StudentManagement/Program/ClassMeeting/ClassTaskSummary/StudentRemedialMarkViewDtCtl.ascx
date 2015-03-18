<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentRemedialMarkViewDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentRemedialMarkViewDtCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_generatebilldtctl">
</script>
<input type="hidden" id="hdnStudentID" runat="server" />
<input type="hidden" id="hdnClassSubjectTaskID" runat="server" />
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
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtHeaderText2" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr> 
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Awal")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtOriginalMark" CssClass="number" ReadOnly="true" Width="80px" runat="server" /></td>
    </tr> 
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Akhir")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtFinalMark" CssClass="number" ReadOnly="true" Width="80px" runat="server" /></td>
    </tr> 
</table>


<h4><%=GetLabel("Remidi") %></h4>
<asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
    <Columns>  
        <asp:TemplateField HeaderText="Kode" HeaderStyle-Width="80px">
            <ItemTemplate>
                R<%#Eval("DisplayOrder") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="TaskDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
        <asp:BoundField DataField="Mark" HeaderText="Nilai" HeaderStyle-Width="80px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
    </Columns>
    <EmptyDataTemplate>
        <%=GetLabel("No Data To Display")%>
    </EmptyDataTemplate>
</asp:GridView>
            