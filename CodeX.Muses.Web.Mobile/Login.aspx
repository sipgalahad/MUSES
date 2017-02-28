<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPBase.master" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="CodeX.Muses.Web.Mobile.Program.Login" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            $(function () {
                $('#btnLogin').click(function (evt) {
                    if (IsValid(evt, 'fsLogin', 'mpLogin'))
                        cbpProcess.PerformCallback();
                    return false;
                });
            });
        });

        function onLoginSuccess(loginData) {
            window.location = ResolveUrl('~/Program/StudentClassInformation.aspx');
        }
    </script>
    
    <style type="text/css">
        body                        { background-color: #0195DD; }
    </style>
    <fieldset id="fsLogin">
        <div style="color: White; text-align: center;width: 100%; font-size: 2.2em; margin-bottom: 1em;">SIM RICCI</div>
        <div style="margin-bottom: 0.5em; padding: 0 20px;"><asp:TextBox ID="txtUserName" Style="font-size: 1.1em;padding: 0.3em 0;" runat="server" Width="100%" placeholder="Username" CssClass="required" ToolTip="Username" /></div>
        <div style="margin-bottom: 1em; padding: 0 20px;"><asp:TextBox ID="txtPassword" Style="font-size: 1.1em;padding: 0.3em 0;" runat="server" Width="100%" TextMode="Password" placeholder="Password" CssClass="required" /></div>
        <div style="padding: 0 20px;"><input type="submit" id="btnLogin" value="<%=GetLabel("Login") %>" style="width:100%; background-color: #013EDD; border: 0px; color: White; padding: 0.2em 0; font-size: 1.1em" /></div>
    </fieldset>

    
    <div style="display: none;">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){
                    var result = s.cpResult.split('|');
                    if(result[0] == 'success')
                        onLoginSuccess(s.cpLoginData);
                    else 
                        alert('Login Failed\n' + result[1]);
                    
                    hideLoadingPanel();
                }" />
        </dxcp:ASPxCallbackPanel>
    </div> 
</asp:Content>