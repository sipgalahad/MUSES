<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" 
    CodeBehind="Sender.aspx.cs" Inherits="CSASPNETReverseAJAX.Sender" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnSend').click(function () {
                cbpProcess.PerformCallback();
            });
        });
    </script>
    <asp:Label ID="lbNotification" runat="server" ForeColor="Red"></asp:Label><br /><br />

    RecipientName:<br />
    <asp:TextBox ID="tbRecipientName" runat="server" Width="100px"></asp:TextBox><br />

    Message:<br />
    <asp:TextBox ID="tbMessageContent" runat="server" Width="300px"></asp:TextBox><br />

    <input type="button" id="btnSend" value="Click To Send" />

     <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { hideLoadingPanel(); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
