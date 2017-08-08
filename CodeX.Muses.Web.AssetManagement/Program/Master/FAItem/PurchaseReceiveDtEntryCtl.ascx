<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseReceiveDtEntryCtl.ascx.cs" 
    Inherits="Codex.Ottimo.Web.AssetManagement.Program.PurchaseReceiveDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_generatebilldtctl">
</script>
<input type="hidden" id="hdnPurchaseReceiveDtID" runat="server" />
<table style="width:100%">
    <colgroup>
        <col style="width:150px"/>
    </colgroup>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No BPB")%></label></td>
        <td><asp:TextBox ID="txtPurchaseReceiveNo" ReadOnly="true" Width="200px" runat="server" /></td>
    </tr>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Item")%></label></td>
        <td><asp:TextBox ID="txtItemName1" ReadOnly="true" Width="200px" runat="server" /></td>
    </tr>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Asset Accrual Type")%></label></td>
        <td><dxe:ASPxComboBox runat="server" ID="cboAssetAccrualType" ClientInstanceName="cboReason" Width="200px" /></td>
    </tr>
    <tr>
        <td></td>
        <td><asp:CheckBox ID="chkIsProcessAssetClosed" Width="100%" runat="server" Text="Tutup" /></td>
    </tr>
</table>