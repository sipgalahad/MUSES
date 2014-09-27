<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupPrintCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.PopupPrintCtl" %>

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
                if (isAllowPrint)
                    openReportViewer(reportCode, filterExpression.text);
                else
                    showToast('Warning', errMessage.text);
            }
        }
    });
</script>
<asp:Repeater ID="rptPrint" runat="server">
    <ItemTemplate>
        <input type="radio" name="rboRegistrationPrint" value="1" reportcode='<%# Eval("ReportCode")%>' isDisplayPrintCount='<%# Eval("isDisplayPrintCount")%>' /><%# Eval("Title")%><br />
    </ItemTemplate>
</asp:Repeater>

<div style="text-align:right; padding-right: 10px;">
    <input type="button" runat="server" id="btnMPEntryPopupPrint" value="Print" />
</div>