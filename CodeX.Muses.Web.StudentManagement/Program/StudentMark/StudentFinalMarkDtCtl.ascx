<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentFinalMarkDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentFinalMarkDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />   
    
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
     <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
        <Columns>
            <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
            <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" ItemStyle-CssClass="tdSubjectName" />
            <asp:BoundField DataField="PassingGrade" HeaderStyle-Width="60px" HeaderStyle-CssClass="thCenter" HeaderText="KKM" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderStyle-Width="60px" HeaderStyle-CssClass="thCenter" HeaderText="Nilai" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <div runat="server" id="divMark"></div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("No Data To Display")%>
        </EmptyDataTemplate>
    </asp:GridView>
</div>