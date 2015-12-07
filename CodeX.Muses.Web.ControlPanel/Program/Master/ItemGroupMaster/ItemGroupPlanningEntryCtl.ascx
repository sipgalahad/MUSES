<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemGroupPlanningEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.ItemGroupPlanningEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
    });
</script>

<input type="hidden" id="hdnSiteID" value="" runat="server" />
<input type="hidden" id="hdnItemGroupID" value="" runat="server" />
<table class="tblContentArea">
    <colgroup>
        <col style="width:100%"/>
    </colgroup>
    <tr>            
        <td style="padding:5px;vertical-align:top">
            <fieldset id="fsEntryPopup" style="margin:0"> 
                <table class="tblEntryContent" >
                    <colgroup>
                        <col style="width:160px"/>
                        <col style="width:120px"/>
                        <col style="width:20px"/>
                        <col style="width:120px"/>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Item Group")%></label></td>
                        <td colspan="4"><asp:TextBox ID="txtItemName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Site")%></label></td>
                        <td colspan="3"><asp:TextBox ID="txtSiteName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Cara Pembelian")%></label></td>
                        <td colspan="3"><dxe:ASPxComboBox ID="cboPurchaseMethod" Width="100%" runat="server" /></td>
                    </tr> 
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Backward (Hari)")%></label></td>
                        <td><asp:TextBox ID="txtNDaysBackward" CssClass="number" runat="server" Width="80px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Forward (Hari)")%></label></td>
                        <td><asp:TextBox ID="txtNDaysForward" CssClass="number" runat="server" Width="80px" /></td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>

