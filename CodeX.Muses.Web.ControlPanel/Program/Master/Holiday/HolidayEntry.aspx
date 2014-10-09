<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="HolidayEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.HolidayEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            $('#<%=chkIsAnnual.ClientID %>').change(function () {
                var isAnnual = $(this).is(":checked");
                cboHolidayYear.SetEnabled(!isAnnual);
            });
            $('#<%=chkIsAnnual.ClientID %>').change();
        }
    </script>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Libur")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboHolidayDate" runat="server" Width="100px" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bulan Libur")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboHolidayMonth" runat="server" Width="300px" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Libur Tahunan")%></label></td>
                        <td><asp:CheckBox ID="chkIsAnnual" runat="server" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Libur")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboHolidayYear" ClientInstanceName="cboHolidayYear" runat="server" Width="150px" /> </td>
                    </tr>
                    <tr>
                         <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Libur")%></label></td>
                         <td><asp:TextBox ID="txtHolidayName" runat="server" Width="300px" /> </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>