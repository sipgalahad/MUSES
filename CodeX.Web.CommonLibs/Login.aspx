<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="CodeX.Web.CommonLibs.Login" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnLogin').click(function (evt) {
                if (IsValid(evt, 'fsLogin', 'mpLogin'))
                    cbpProcess.PerformCallback();
                return false;
            });
        });

        function onLoginSuccess() {
            document.location = ResolveUrl('~/Libs/Program/Main.aspx');
        }
    </script>
    <div class="loginBg borderBox">Medinfras: <%=moduleName%></div>
    <div id="loginContainerLoginInfo" class="loginContainerLoginInfo" runat="server">
        <center>
            <fieldset id="fsLogin">     
                <table cellpadding="2">
                    <tr>
                        <td><%=GetLabel("User ID")%></td>
                        <td><asp:TextBox ID="txtUserName" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Password")%></td>
                        <td><asp:TextBox ID="txtPassword" TextMode="Password" Width="200px" runat="server" /></td>
                        <td><input type="submit" value="Log In" id="btnLogin" /></td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <div class="loginBg borderBox" style="height:530px">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){
                    var result = s.cpResult.split('|');
                    if(result[0] == 'success')
                        onLoginSuccess();
                    else 
                        showToast('Login Failed', result[1]);
                    
                    hideLoadingPanel();
                }" />
        </dxcp:ASPxCallbackPanel>
    </div>
    
</asp:Content>
