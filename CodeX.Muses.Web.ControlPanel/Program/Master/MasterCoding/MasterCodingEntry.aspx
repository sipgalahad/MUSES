<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="MasterCodingEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.MasterCodingEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

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
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Master Code")%></label></td>
                        <td><asp:TextBox ID="txtMasterCode" Width="100px" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Master Name")%></label></td>
                        <td><asp:TextBox ID="txtMasterName" Width="300px" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Prefix Type")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboPrefixType" Enabled="false" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Default Perfix")%></label></td>
                        <td><asp:TextBox ID="txtDefaultPrefix" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Perfix Length")%></label></td>
                        <td><asp:TextBox ID="txtPrefixLength" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Counter Digit")%></label></td>
                        <td><asp:TextBox ID="txtCounterDigit" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsBySite" Text="Is By Site" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsAllowChangeInitial" Text="Is Allow Change Initial" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsEditable" Text="Is Editable" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
