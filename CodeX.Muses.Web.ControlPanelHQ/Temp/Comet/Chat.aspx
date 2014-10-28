<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" 
    CodeBehind="Chat.aspx.cs" Inherits="CSASPNETReverseAJAX.Chat" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            waitEvent();

            $('#<%=txtSenderName.ClientID %>').change(function () {
                cbpProcess.PerformCallback('login');
            });

            $('#btnSend').click(function () {
                cbpProcess.PerformCallback('send');
            });
        });
        function waitEvent() {
            var username = $('#<%=txtSenderName.ClientID %>').val();
            if (username != '') {
                $.ajax({
                    type: "POST",
                    url: ResolveUrl('~/Temp/Comet/Dispatcher.asmx/WaitMessage2'),
                    data: "{ userName:'" + username + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        displayMessage(msg.d);
                        setTimeout(waitEvent, 0);
                    },
                    failure: function (msg) {
                        alert('fail');
                        alert(msg);
                        setTimeout(waitEvent, 0);
                    }
                });
            }
        }

        function displayMessage(entity) {
            if (entity != null) {
                var temp = $('#divMessage').html();
                $('#divMessage').html(temp + entity.SenderName + ": " + entity.MessageContent + "<br />");
            }
        }

        function currentTime() {
            var currentDate = new Date()
            return currentDate.getHours() + ":" + currentDate.getMinutes() + ":" + currentDate.getSeconds();
        }

        function onCbpProcessEndCallback(s) {
            hideLoadingPanel();
            if (s.cpResult == 'login')
                waitEvent();
            else {
                var temp = $('#divMessage').html();
                $('#divMessage').html(temp + $('#<%=txtSenderName.ClientID %>').val() + ": " + $('#<%=tbMessageContent.ClientID %>').val() + "<br />");
            }

        }
    </script>

    SenderName:<br />
    <asp:TextBox ID="txtSenderName" runat="server" Width="100px"></asp:TextBox><br />

    RecipientName:<br />
    <asp:TextBox ID="tbRecipientName" runat="server" Width="100px"></asp:TextBox><br />

    Message:<br />
    <asp:TextBox ID="tbMessageContent" runat="server" Width="300px"></asp:TextBox><br />

    <input type="button" id="btnSend" value="Click To Send" />

     <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>

    <h3>Messages:</h3>
    <div id="divMessage" style="width: 600px; height: 300px; overflow-y:scroll; background-color: White;">
    
    </div>
    <asp:Label ID="lbMessages" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
