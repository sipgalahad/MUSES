<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="HRWeeklyScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.HRWeeklyScheduleEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>

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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><uc1:MasterCodingCtl ID="ctlEntityCode" runat="server" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtWeeklyScheduleName" Width="300px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Senin")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD1" ClientInstanceName="cboWeeklyScheduleD1" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Selasa")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD2" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Rabu")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD3" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Kamis")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD4" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Jumat")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD5" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Sabtu")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD6" Width="200px" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari Minggu")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboWeeklyScheduleD7" Width="200px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
