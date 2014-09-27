<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="ZipCodesEntry.aspx.cs" Inherits="CodeX.Muses.Web.SystemSetup.Program.ZipCodesEntry" %>

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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("ZIP Code")%></label></td>
                        <td><asp:TextBox ID="txtZipCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Street Name")%></label></td>
                        <td><asp:TextBox ID="txtStreetName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("District")%></label></td>
                        <td><asp:TextBox ID="txtDistrict" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("County")%></label></td>
                        <td><asp:TextBox ID="txtCounty" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("City")%></label></td>
                        <td><asp:TextBox ID="txtCity" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Province")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboGCProvince" Width="300px" runat="server"  /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Latitude")%></label></td>
                        <td><asp:TextBox ID="txtLatitude" Width="100px" runat="server" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Longitude")%></label></td>
                        <td><asp:TextBox ID="txtLongitude" Width="100px" runat="server" CssClass="number" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
