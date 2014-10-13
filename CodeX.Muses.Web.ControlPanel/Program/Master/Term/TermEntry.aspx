<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="TermEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.TermEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Termin")%></label></td>
                        <td><asp:TextBox ID="txtTermCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Termin")%></label></td>
                        <td><asp:TextBox ID="txtTermName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Termin (hari)")%></label></td>
                        <td><asp:TextBox ID="txtTermDay" Width="100px" runat="server" CssClass="number required" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
