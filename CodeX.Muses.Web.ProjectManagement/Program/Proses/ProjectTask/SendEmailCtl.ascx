<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendEmailCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.SendEmailCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $('#btnSend').die('click');
    $('#btnSend').live('click', function () {
        $('#<%=hdnEmailMessage.ClientID %>').val($('#<%=txtContent.ClientID %>').text());
        cbpProcessPopup.PerformCallback('email');
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'email') {
            if (param[1] == 'fail')
                showToast('Send Email Failed', 'Error Message : ' + param[2]);
        }
    }
</script>

<div style="overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnEmailMessage" value="" runat="server" />
    <table width="100%">
        <tr>
            <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("To")%></label></td>
            <td><asp:TextBox runat="server" ID="txtTo" Width="300px" /></td>
        </tr>
        <tr>
            <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("CC")%></label></td>
            <td><asp:TextBox runat="server" ID="txtCC" Width="300px" /></td>
        </tr>
        <tr>
            <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("Subject")%></label></td>
            <td><asp:TextBox runat="server" ID="txtSubject" Width="300px" /></td>
        </tr>
        <tr>
            <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("Template")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboTemplate" ClientInstanceName="cboTemplate" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("Content")%></label></td>
            <td>
                <asp:TextBox runat="server" ID="txtContent" Width="500px" Height="300px" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="center">
                <input type="button" value="Send" id="btnSend" />
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

