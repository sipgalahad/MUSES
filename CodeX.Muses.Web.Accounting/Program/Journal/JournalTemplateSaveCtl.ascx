<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalTemplateSaveCtl.ascx.cs" 
    Inherits="CodeX.Web.Accounting.Program.JournalTemplateSaveCtl" %>

<script type="text/javascript" id="dxss_drugslogisticsquickpicksctl">
</script>

<input type="hidden" id="hdnLstJournalTemplateDt" runat="server" />
<div style="padding:10px;">
    <table class="tblEntryContent" style="width:100%">
        <colgroup>
            <col style="width:30%"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Template")%></label></td>
            <td><asp:TextBox ID="txtTemplateCode" Width="100px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Template")%></label></td>
            <td><asp:TextBox ID="txtTemplateName" Width="300px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
        </tr>
    </table>
</div>