<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="SchoolPeriodEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolPeriodEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#btnDailySchedulePackageDt').click(function () {
                var schedulePackage = cboDailySchedulePackage.GetValue();
                if (schedulePackage != null && cboDailySchedulePackage != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
                    openUserControlPopup(url, schedulePackage, 'Jadwal', 1000, 550);
                }
            });
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
                        <col style="width:300px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtSchoolPeriodCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtSchoolPeriodName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Selesai")%></label></td>
                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboDailySchedulePackage" ClientInstanceName="cboDailySchedulePackage" Width="300px" /></td>
                        <td><input type="button" id="btnDailySchedulePackageDt" class="btnMore" value="..." /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
