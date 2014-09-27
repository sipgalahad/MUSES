<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Login2" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        var loginData = '';
        $(function () {
            $('.imgOpenModule.enabled').live('click', function () {
                if ($('#<%=pnlUserLoginInformation.ClientID %>').is(":visible")) {
                    var link = $(this).attr('link');
                    var moduleID = $(this).attr('moduleid');
                    if (loginData == '')
                        cbpProcess.PerformCallback('getdata|' + link + '|' + moduleID);
                    else
                        openModule(link, moduleID);
                }
            });

            $('#btnLogin').click(function (evt) {
                if (IsValid(evt, 'fsLogin', 'mpLogin'))
                    cbpProcess.PerformCallback('login');
                return false;
            });
        });

        var listWindow = [];
        function openModule(link, moduleID) {
            showLoadingPanel();
            var url = ResolveUrl(link);
            var windowID = 'Map' + moduleID;
            var win = window.open("", windowID, 'type=fullWindow, fullscreen, status=1,toolbar=0,menubar=0,resizable=1,location=0,scrollbars=1');
            if (win) {
                win.moveTo(0, 0);
                win.focus();
                listWindow.push(win);
                win.resizeTo(screen.width, screen.height - 20);

                var mapForm = document.createElement("form");
                mapForm.target = windowID;
                mapForm.method = "POST";
                mapForm.action = url;

                var mapInput = document.createElement("input");
                mapInput.type = "hidden";
                mapInput.name = "id";
                mapInput.value = loginData + '|' + $('#<%=ddlSite.ClientID %>').val() + '|1';
                mapForm.appendChild(mapInput);

                document.body.appendChild(mapForm);

                mapForm.submit();

                $(mapForm).remove();

            } else {
                showToast('Warning', 'You must allow popups for this map to work.');
            }
            hideLoadingPanel();
        }

        function onLnkLogoutClientClick() {
            for (var i = 0; i < listWindow.length; ++i) {
                if (!listWindow[i].closed) {
                    listWindow[i].close();
                }
            }
            listWindow = [];
            __doPostBack('<%=lnkLogout.UniqueID%>', '');
        }

        function onLoginSuccess(userName) {
            $('#<%=loginContainerLoginInfo.ClientID %>').hide();
            $('#<%=pnlUserLoginInformation.ClientID %>').show();
            $('#<%=lblUserLoginInfo.ClientID %>').html(userName);
            cbpSelectUserRole.PerformCallback();
        }

        $('#<%=ddlSite.ClientID %>').live('change', function () {
            cbpRptModule.PerformCallback($('#<%=ddlSite.ClientID %>').val());
        });

        function onCbpSelectUserRoleEndCallback() {
            cbpRptModule.PerformCallback($('#<%=ddlSite.ClientID %>').val());
        }
    </script>
    <style type="text/css">
        body
        {
            background-image:url('<%=ResolveUrl("~/Libs/Images/medinfras_bg.jpg")%>');
        }
    </style>
    <div style="float:right; margin: 5px 10px 0 0;"><img src='<%=ResolveUrl("~/Libs/Images/qislogos.png")%>' alt="" /></div>
    <div class="loginBg borderBox"><img src='<%=ResolveUrl("~/Libs/Images/medinfras_logo.png")%>' alt="" /></div>
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
    <div runat="server" id="pnlUserLoginInformation" class="loginContainerLoginInfo borderBox pnlUserLoginInformation">
        <div class="borderBox" style="float:right; font-size: 0.8em;">
            <dxcp:ASPxCallbackPanel ID="cbpSelectUserRole" runat="server" Width="80%" ClientInstanceName="cbpSelectUserRole"
                ShowLoadingPanel="false" OnCallback="cbpSelectUserRole_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                    EndCallback="function(s,e){ onCbpSelectUserRoleEndCallback(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="pnlSelectUserRole" runat="server">    
                        <table style="margin-top: 10px">
                            <tr>
                                <td style="width: 100px"><%=GetLabel("Site")%></td>
                                <td><asp:DropDownList ID="ddlSite" runat="server" Width="200px" /></td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </div>
        <div style="margin-top: 10px;">
            <%=GetLabel("Welcome")%>, <span runat="server" id="lblUserLoginInfo"></span><br />
            <asp:LinkButton ID="lnkLogout" CssClass="lnkLogout" Text="[Logout]" OnClick="lnkLogout_Click" OnClientClick="onLnkLogoutClientClick();" runat="server" />
        </div>
    </div>    
    <div class="loginBg borderBox" style="padding: 1% 0;width:100%;">
        <table style="width:100%;">
            <colgroup>
                <col style="width:50%"/>
                <col style="width:50%"/>
            </colgroup>
            <tr>
                <td valign="bottom" style="text-align:center;padding-top: 20px"><img src='<%=ResolveUrl("~/Libs/Images/Sitelogo.png")%>' alt="" /></td>
                <td valign="top">
                    <div style="text-align:left;width:100%; padding-left:20px;">
                        <dxcp:ASPxCallbackPanel ID="cbpRptModule" runat="server" Width="80%" ClientInstanceName="cbpRptModule"
                            ShowLoadingPanel="false" OnCallback="cbpRptModule_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ hideLoadingPanel(); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">     
                                    <asp:Repeater ID="rptModule" runat="server">
                                        <HeaderTemplate>
                                            <ul id="loginUlListModule">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li class="<%# Eval("CssClass")%>">
                                                <img class="imgOpenModule <%# Eval("CssClass")%>" src='<%# Eval("ImageUrl")%>' alt="" link='<%# Eval("Link")%>' moduleid='<%# Eval("ModuleID")%>'  />
                                                <div class="<%# Eval("CssClass")%>"><%# Eval("ModuleName")%></div>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </dx:PanelContent>
                            </PanelCollection>                            
                        </dxcp:ASPxCallbackPanel> 
                    </div>
                </td>
            </tr>
        </table>
        <div style="display:none">        
            <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
                ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                    EndCallback="function(s,e){
                        if(s.cpParam == 'login'){
                            var result = s.cpResult.split('|');
                            if(result[0] == 'success'){
                                loginData = s.cpLoginData;
                                onLoginSuccess(result[1]);
                            }
                            else {
                                showToast('Login Failed', 'Error Message : ' + result[1]);
                                hideLoadingPanel();
                            }
                        }
                        else {
                            loginData = s.cpLoginData;
                            openModule(s.cpLink, s.cpModuleID);
                        }
                    }" />
            </dxcp:ASPxCallbackPanel>
        </div>
    </div>
    <div id="loginFooter" class="loginBg borderBox" >
        <div style="float:right">Licensed to: Quantum Medical Care</div>
        <div>2013 © PT. Quantum Infra Solusindo</div>
    </div>
</asp:Content>
