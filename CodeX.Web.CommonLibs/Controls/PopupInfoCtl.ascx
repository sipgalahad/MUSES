<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupInfoCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.PopupInfoCtl" %>

<script type="text/javascript" id="dxss_registrationprintctl">
    $('.goRightPanelContent').click(function () {
        if ($(this).attr('enabled') == null) {
            pcRightPanelContent.Hide();
            var url = $(this).attr('url');
            $('#hdnRightPanelContentIsLoadContent').val('1');
            $('#hdnRightPanelContentUrl').val(url);
            $('#hdnRightPanelContentFirstTimeLoad').val('1');
            var rightPanelContentParam = '';
            if (typeof onBeforeLoadRightPanelContent == 'function') {
                rightPanelContentParam = onBeforeLoadRightPanelContent($(this).attr('code'));
            }
            $('#hdnRightPanelContentCode').val($(this).attr('code'));
            $('#hdnRightPanelContentParam').val(rightPanelContentParam);

            var title = $(this).parent().find('.qmtitle').html();
            var width = $(this).attr('pcWidth');
            var height = $(this).attr('pcHeight');
            pcRightPanelContent.SetHeaderText(title);
            pcRightPanelContent.SetSize(width, height);
            pcRightPanelContent.Show();
            $('#imgCloseRightPanel').click();
        }
    });
</script>
<div class="divListRightPanel information">
<asp:Repeater ID="rptInformation" runat="server">
    <ItemTemplate>
        <div class="rightPanelContent borderBox">
            <a class="goRightPanelContent" href="javascript:void(0);" pcWidth="<%# Eval("Width")%>" pcHeight="<%# Eval("Height")%>" id="<%# Eval("ID")%>" code="<%# Eval("Code")%>" url="<%# Eval("Url")%>">Go</a>
            <div class='qmtitle'><%# Eval("Title")%></div>
            <div class='qmdescription'><%# Eval("Description")%></div>
        </div>
    </ItemTemplate>
</asp:Repeater> 
</div>