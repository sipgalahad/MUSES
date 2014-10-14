<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        setDatePicker('<%=txtTaskDate.ClientID %>');
        setDatePicker('<%=txtStartDate.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');
    });
</script>

<input type="hidden" id="hdnID" runat="server" value="" />
<div>
    <table>
        <colgroup>
            <col style="width: 160px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Kode")%></label></td>
            <td><asp:TextBox ID="txtClassTaskCode" Width="100px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Topik")%></label></td>
            <td><asp:TextBox ID="txtTopic" Width="200px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tugas")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboTaskType" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("% Nilai Akhir")%></label></td>
            <td><asp:TextBox ID="txtFinalMarkPercentage" CssClass="number" Width="80px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Tugas")%></label></td>
            <td><asp:TextBox ID="txtTaskDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Mulai")%></label></td>
            <td>    
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
            <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
        </tr>
    </table>
</div>

