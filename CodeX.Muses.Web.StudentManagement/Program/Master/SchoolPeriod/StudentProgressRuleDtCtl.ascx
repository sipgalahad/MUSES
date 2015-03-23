<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentProgressRuleDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentProgressRuleDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
</script>

<div>
    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
        <Columns>
            <asp:BoundField DataField="StudentProgressRuleDtName" HeaderText="Kriteria" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="cfValue" HeaderText="Nilai" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remarks" HeaderText="Deskripsi" />
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("No Data To Display")%>
        </EmptyDataTemplate>
    </asp:GridView>
</div>

