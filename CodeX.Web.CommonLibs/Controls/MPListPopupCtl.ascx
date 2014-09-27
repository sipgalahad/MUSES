<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MPListPopupCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Program.MPListPopupCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

    <input type="hidden" runat="server" id="hdnIsAdd" value="1" />
    <div style="padding:5px 0;">  
        <script type="text/javascript" id="dxss_mpentrypopupctl">
            $(function () {
                $('#<%=btnMPListPopupExport.ClientID %>').click(function () {
                    showLoadingPanel();
                    $('#<%=btnExport.ClientID%>').click();
                    setTimeout(function () {
                        hideLoadingPanel();
                    }, 1000);
                });
            });
        </script>
       <asp:Panel ID="pnlListPopup" runat="server" />  
       <input type="hidden" id="hdnPageTitle" runat="server" />
        <div style="display:none">
            <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
            <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
        </div>
        <div style="text-align:right; padding-right: 10px;">
            <input type="button" runat="server" id="btnMPListPopupExport" value="Export" />
        </div>
    </div>

