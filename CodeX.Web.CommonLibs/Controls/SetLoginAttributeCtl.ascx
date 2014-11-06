<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetLoginAttributeCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.SetLoginAttributeCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_changepasswordctl">
    
</script>

<fieldset id="fsLoginAttribute" style="margin:0"> 
    <table class="tblEntryContent" style="width:100%">
        <colgroup>
            <col style="width:140px"/>
        </colgroup>
        <asp:Repeater ID="rptLoginAttribute" runat="server" OnItemDataBound="rptLoginAttribute_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%#Eval("LoginAttributeCaption")%></label></td>
                    <td>
                        <input type="hidden" id="hdnSessionName" runat="server" />
                        <dxe:ASPxComboBox ID="cboLoginAttribute" runat="server" Width="100%" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</fieldset>
