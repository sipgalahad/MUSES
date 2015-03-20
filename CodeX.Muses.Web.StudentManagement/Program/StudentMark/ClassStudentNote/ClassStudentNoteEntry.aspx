<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentNoteEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentNoteEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                onCustomButtonClick('save');
            });
        });
    </script>
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td class="tdLabel" style="width:100px; vertical-align: top; padding-top: 5px"><%=GetLabel("Catatan") %></td>
            <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="5" Width="500px" /></td>
        </tr>
    </table>
</asp:Content>