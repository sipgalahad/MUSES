<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalTemplateCtl.ascx.cs" 
    Inherits="CodeX.Web.Accounting.Program.JournalTemplateCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_drugslogisticsquickpicksctl">
    //#region Template No
    function onGetJournalTemplateFilterExpression() {
        var filterExpression = "IsDeleted = 0";
        return filterExpression;
    }

    $('#lblTemplate.lblLink').live('click', function () {
        openSearchDialog('journaltemplatehd', onGetJournalTemplateFilterExpression(), function (value) {
            $('#<%=txtTemplateCode.ClientID %>').val(value);
            onTxtTemplateCodeChanged(value);
        });
    });

    $('#<%=txtTemplateCode.ClientID %>').live('change', function () {
        onTxtTemplateCodeChanged($(this).val());
    });

    function onTxtTemplateCodeChanged(value) {
        var filterExpression = onGetJournalTemplateFilterExpression() + " AND TemplateCode = '" + value + "'";
        Methods.getObject('GetvJournalTemplateHdList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=hdnTemplateID.ClientID %>').val(result.TemplateID);
                $('#<%=txtTemplateName.ClientID %>').val(result.TemplateName);
            } else {
                $('#<%=hdnTemplateID.ClientID %>').val('');
                $('#<%=txtTemplateCode.ClientID %>').val('');
                $('#<%=txtTemplateName.ClientID %>').val('');
            }
        });
    }
    //#endregion
</script>
<input type="hidden" id="hdnGLTransactionID" runat="server" />
<input type="hidden" id="hdnJournalDate" runat="server" />
<input type="hidden" id="hdnGCJournalGroup" runat="server" />
<div style="padding:10px;">
    <table style="width:100%">
        <colgroup>
            <col width="120px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal lblLink" id="lblTemplate"><%=GetLabel("Template") %></label></td>
            <td>
                <input type="hidden" id="hdnTemplateID" runat="server" />
                <table style="width:100%" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:30%"/>
                        <col style="width:3px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td><asp:TextBox runat="server" ID="txtTemplateCode" Width="100%" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox runat="server" ID="txtTemplateName" ReadOnly="true" Width="100%" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah") %></label></td>
            <td><asp:TextBox runat="server" CssClass="txtCurrency" Width="120px" ID="txtAmount" /></td>
        </tr>
    </table>
</div>