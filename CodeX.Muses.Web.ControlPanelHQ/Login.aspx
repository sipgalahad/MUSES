<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanelHQ.Login" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <style type="text/css">
        .divLoginLogo           { background: url(Libs/Images/mainbg.jpg) no-repeat; width: 475px; height: 340px; float: left; }         
        .Heading1               { font-size: 16pt; }
        .Footer                 { font-size: 10pt; }                
        .WaterMarkedTextBox     { color: gray; font-size: 10pt; }
        .WaterMarkedTextBoxPSW  { color: gray; background-position: left center; background-image: url(Libs/Images/psw_wMark.png); background-repeat: no-repeat; }
        .NormalTextBox          { height: 24px; }
        
        #divLogin-mainContent   { width: 980px; margin: 0 auto; padding: 0px; padding-top: 50px; text-align: left; }
        #divLogin-leftContent   { width: 480px; margin: 0 auto; padding: 0px; text-align: left; float: left; }
        #divLogin-rightContent  { width: 480px; margin: 0 auto; padding: 0px; text-align: left; float: left; }
        #login_content_bottom         { float: left; width: 467px; height: 150px; background-color: #00ABA9; color: #ffffff; padding: 0 4px 4px 4px; }
        #login_content_left           { float: left; padding: 0 30px 30px 30px; width: 445px; height: auto; }
        #login_content_right          { float: left; padding: 5px; width: 200px; height: auto; }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#btnLogin').click(function (evt) {
                if (IsValid(evt, 'fsLogin', 'mpLogin'))
                    cbpProcess.PerformCallback();
                return false;
            });
        });

        function onLoginSuccess(isHasLoginAttribute, loginData, defaultUrl) {
            $('#<%=hdnLoginData.ClientID %>').val(loginData);
            $('#<%=hdnDefaultUrl.ClientID %>').val(defaultUrl);             
            if (isHasLoginAttribute == '0')
                openRemoteLogon();
            else {
                var url = ResolveUrl("~/Libs/Controls/SetLoginAttributeCtl.ascx");
                openUserControlPopup(url, '', '<%=GetLabel("Login Attribute") %>', 400, 200);
            }
        }

        function onAfterPopupControlClosing() {
            openRemoteLogon();
        }

        function openRemoteLogon() {
            showLoadingPanel();
            var loginData = $('#<%=hdnLoginData.ClientID %>').val();
            var defaultUrl = $('#<%=hdnDefaultUrl.ClientID %>').val();
            var mapForm = document.createElement("form");
            mapForm.method = "POST";
            mapForm.action = ResolveUrl(defaultUrl);

            var mapInput = document.createElement("input");
            mapInput.type = "hidden";
            mapInput.name = "id";
            mapInput.value = loginData + '|1';
            mapForm.appendChild(mapInput);

            document.body.appendChild(mapForm);

            mapForm.submit();

            $(mapForm).remove();
        }
    </script>
    <div style="width: 100%; height: 100%;">
        <input type="hidden" id="hdnSiteID" value="" runat="server" />
        <input type="hidden" id="hdnLoginData" value="" runat="server" />
        <input type="hidden" id="hdnDefaultUrl" value="" runat="server" />
        <div id="divLogin-mainContent">
            <div id="divLogin-leftContent">
                <div class="divLoginLogo">
                </div>
                <div id="login_content_bottom">
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                        irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
                        pariatur...</p>
                </div>
            </div>
            <div id="divLogin-rightContent">
                <fieldset id="fsLogin">     
                    <table style="margin: 50px 0px 0px 50px;" cellpadding="4">
                        <tr>
                            <td class="Heading1">
                                Product Logo
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 50px;">
                                <%=GetLabel("Login with your account")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="300px" Height="26px"
                                    placeholder="Username" CssClass="required" ToolTip="Username" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" Width="300px" Height="26px" TextMode="Password"
                                    placeholder="Password" CssClass="required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="submit" id="btnLogin" value="<%=GetLabel("Login") %>" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 150px;">
                                <hr />                            
                            </td>
                        </tr>
                        <tr>
                            <td class="Footer" style="vertical-align: bottom;">
                                &copy 2013 [Company Name]
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
    <div style="display: none;">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){
                    var result = s.cpResult.split('|');
                    if(result[0] == 'success')
                        onLoginSuccess(result[1], s.cpLoginData, s.cpUrl);
                    else 
                        showToast('Login Failed', result[1]);
                    
                    hideLoadingPanel();
                }" />
        </dxcp:ASPxCallbackPanel>
    </div> 
</asp:Content>
