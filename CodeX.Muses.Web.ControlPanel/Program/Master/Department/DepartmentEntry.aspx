<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="DepartmentEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.DepartmentEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Instalasi")%></label></td>
                        <td><asp:TextBox ID="txtDepartmentCode" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Instalasi")%></label></td>
                        <td><asp:TextBox ID="txtDepartmentName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Singkat")%></label></td>
                        <td><asp:TextBox ID="txtShortName" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Inisial")%></label></td>
                        <td><asp:TextBox ID="txtInitial" Width="150px" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <div><asp:CheckBox ID="chkIsActive" runat="server" /> <%=GetLabel("Aktif")%></div>
            </td>
        </tr>
    </table>
</asp:Content>
