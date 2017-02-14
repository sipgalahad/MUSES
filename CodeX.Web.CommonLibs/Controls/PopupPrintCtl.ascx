<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupPrintCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.PopupPrintCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_registrationprintctl">
    $('#<%=btnMPEntryPopupPrint.ClientID %>').click(function () {
        $rbo = $('input[name=rboRegistrationPrint]:checked');
        if ($rbo.length > 0) {
            var filterExpression = { text: "" };
            var errMessage = { text: "" };
            var reportCode = $rbo.attr('reportcode');
            if (reportCode != '') {
                var isAllowPrint = true;
                if (typeof onBeforeRightPanelPrint == 'function') {
                    isAllowPrint = onBeforeRightPanelPrint(reportCode, filterExpression, errMessage);
                }
                if (isAllowPrint) {
                    $('#<%=hdnReportCode.ClientID %>').val(reportCode);
                    $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression.text);
                    cbpProcessPrintPopup.PerformCallback('print');
                }
                else
                    showToast('Warning', errMessage.text);
            }
        }
    });

    function onCbpProcessPrintPopupEndCallback(s) {
        if (s.cpIsUsingDirectPrint == '0') {
            var lang = '';
            if ($('#<%=hdnIsChooseLang.ClientID %>').val() == '1')
                lang = cboLanguage.GetValue();
            openReportViewer($('#<%=hdnReportCode.ClientID %>').val(), $('#<%=hdnFilterExpression.ClientID %>').val(), lang, $('#<%=hdnDepartmentID.ClientID %>').val());
        }
        else {
            var param = s.cpResult.split('|');
            if (param[0] == 'fail')
                showToast('Print Failed', 'Error Message : ' + param[1]);
        }
        hideLoadingPanel();
    }
</script>
<input type="hidden" id="hdnReportCode" runat="server" />
<input type="hidden" id="hdnFilterExpression" runat="server" />
<input type="hidden" id="hdnIsChooseLang" runat="server" value="" />
<input type="hidden" id="hdnDepartmentID" runat="server" />
<div id="divLanguage" runat="server">
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><label class="lblMandatory"><%=GetLabel("Bahasa")%></label></td>
            <td><dxe:ASPxComboBox ID="cboLanguage" Width="200px" ClientInstanceName="cboLanguage" runat="server" /></td>
        </tr>
    </table>
</div>
<asp:Repeater ID="rptPrint" runat="server">
    <ItemTemplate>
        <input type="radio" name="rboRegistrationPrint" value="1" <%#Eval("IsSelected").ToString() == "True" ? "checked='checked'" : "" %> reportcode='<%# Eval("ReportCode")%>' /><%# Eval("ReportName")%><br />
    </ItemTemplate>
</asp:Repeater>

<div style="text-align:right; padding-right: 10px;">
    <input type="button" runat="server" id="btnMPEntryPopupPrint" value="Print" />
</div>


<dxcp:ASPxCallbackPanel ID="cbpProcessPrintPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPrintPopup"
    ShowLoadingPanel="false" OnCallback="cbpProcessPrintPopup_Callback">
    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcessPrintPopupEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>