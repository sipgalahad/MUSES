<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="OrganizationMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.OrganizationMarkList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassSubjectID" runat="server" />
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />  
         <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Organisasi" HeaderStyle-Width="250px" />
                <asp:BoundField DataField="Value" HeaderText="Keterangan" />
            </Columns>
            <EmptyDataTemplate>
                <%=GetLabel("No Data To Display")%>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>