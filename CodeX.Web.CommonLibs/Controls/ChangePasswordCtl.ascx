<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.ChangePasswordCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<script type="text/javascript" id="dxss_changepasswordctl">
    function checkConfirmPasswordChangePassword(value, element) {
        return value == $('#<%=txtNewPassword.ClientID %>').val();
    }

    jQuery.validator.addMethod("confirmpasswordchangepassword", checkConfirmPasswordChangePassword, "");

    $('#btnSaveChangePassword').click(function (evt) {
        if (IsValid(evt, 'fsChangePassword', 'mpChangePassword'))
            cbpChangePasswordProcess.PerformCallback('save');
        return false;
    });

    function onCbpChangePasswordProcessEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'fail')
            showToast('Change password failed','Error Message : ' + param[1]);
        else {
            showToast("Success", "Change password success and it will be affected on next login");
            pcRightPanelContent.Hide();
        }
        hideLoadingPanel();
    }
</script>

<fieldset id="fsChangePassword" style="margin:0"> 
    <table class="tblEntryContent" style="width:100%">
        <colgroup>
            <col style="width:200px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Password Lama")%></label></td>
            <td><asp:TextBox ID="txtOldPassword" CssClass="required" TextMode="Password" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Password Baru")%></label></td>
            <td><asp:TextBox ID="txtNewPassword" CssClass="required" TextMode="Password" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Konfirmasi Password")%></label></td>
            <td><asp:TextBox ID="txtConfirmPassword" CssClass="confirmpasswordchangepassword required" TextMode="Password" Width="100%" runat="server" /></td>
        </tr>
    </table>
</fieldset>
<div style="width:100%;text-align:center">
    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
        <tr>
            <td><input type="button" value='<%= GetLabel("Simpan")%>' style="width:70px" id="btnSaveChangePassword" /></td>
            <td><input type="button" value='<%= GetLabel("Tutup")%>' style="width:70px" onclick="pcRightPanelContent.Hide();" /></td>
        </tr>
    </table>
</div>

<dxcp:ASPxCallbackPanel ID="cbpChangePasswordProcess" runat="server" Width="100%" ClientInstanceName="cbpChangePasswordProcess"
    ShowLoadingPanel="false" OnCallback="cbpChangePasswordProcess_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
        EndCallback="function(s,e){ onCbpChangePasswordProcessEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>

