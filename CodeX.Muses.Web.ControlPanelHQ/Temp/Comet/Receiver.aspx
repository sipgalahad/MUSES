<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" 
    CodeBehind="Receiver.aspx.cs" Inherits="CSASPNETReverseAJAX.Receiver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <asp:Label ID="lbNotification" runat="server" ForeColor="Red" Text="Please login:"></asp:Label><br />
    <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
    <asp:Button ID="btnLogin" runat="server" Text="Click to login" onclick="btnLogin_Click" />

    <script type="text/javascript">
        $(function () {
            waitEvent();
        });
        function waitEvent() {
            var username = '<%= Session["userName"] %>';
            if (username != '') {
                $.ajax({
                    type: "POST",
                    url: ResolveUrl('~/Temp/Comet/Dispatcher.asmx/WaitMessage'),
                    data: "{ userName:'" + username + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        alert('lalalalal');
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

        function displayMessage(message) {
            var panel = document.getElementById("<%= lbMessages.ClientID %>");
            panel.innerHTML += currentTime() + ": " + message + "<br />";
        }

        function currentTime() {
            var currentDate = new Date()
            return currentDate.getHours() + ":" + currentDate.getMinutes() + ":" + currentDate.getSeconds();
        }
    </script>

    <h3>Messages:</h3>
    <asp:Label ID="lbMessages" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
