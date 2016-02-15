<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentDropOutEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentDropOutEntryCtl" %>
    
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_patientpaymentreceiptvoidctl">
    $(function () {
        hideLoadingPanel();
        setDatePicker('<%=txtDropOutDate.ClientID %>');

        $('#<%=btnSaveDropOut.ClientID %>').click(function () {
            onDropOutDateSaveClick($('#<%=txtDropOutDate.ClientID %>').val());
            pcRightPanelContent.Hide();
        });
    });
</script>

<div style="padding:10px;">     
    <table>
        <colgroup>
            <col style="width:120px"/>
            <col style="width:200px"/>
        </colgroup>
        <tr id="trReason" runat="server">
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal")%></label></td>
            <td><asp:TextBox ID="txtDropOutDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
    </table>
</div>
<div style="text-align:right; padding-right: 10px;">
    <input type="button" runat="server" id="btnSaveDropOut" value="Save" />
</div>