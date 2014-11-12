<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterCodingCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.MasterCodingCtl" %>
    
<input type="hidden" id="hdnIsAdd" runat="server" />
<input type="hidden" id="hdnMasterType" runat="server" />
<div id="divAddCode" runat="server">
    <input type="hidden" id="hdnDefaultPrefix" runat="server" />
    <table style="width:150px" cellpadding="0" cellspacing="0">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:3px"/>
            <col/>
        </colgroup>
        <tr>
            <td><asp:TextBox ID="txtCodeInitial" Width="100%" runat="server" /></td>
            <td>&nbsp;</td>
            <td><asp:TextBox ID="txtCodeNumber" Width="100%" runat="server" ReadOnly="true" /></td>
        </tr>
    </table>
</div>
<div id="divEditCode" runat="server"><asp:TextBox ID="txtCode" Width="150px" runat="server" /></div>