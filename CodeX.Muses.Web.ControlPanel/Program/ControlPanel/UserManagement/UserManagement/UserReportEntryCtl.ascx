<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserReportEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.UserReportEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    var pageCountPopup = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            getCheckedReport();
            cbpEntryPopupView.PerformCallback('changepage|' + page);
        });
    });

    function getCheckedReport() {
        var lstSelectedReport = $('#<%=hdnSelectedReport.ClientID %>').val().split(',');
        var result = '';
        $('#<%=grdView.ClientID %> .chkReport input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedReport.indexOf(key) < 0)
                    lstSelectedReport.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedReport.indexOf(key) > -1)
                    lstSelectedReport.splice(lstSelectedReport.indexOf(key), 1);
            }
        });
        $('#<%=hdnSelectedReport.ClientID %>').val(lstSelectedReport.join(','));
    }


    function onCbpEntryPopupViewEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);

            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedReport();
                cbpEntryPopupView.PerformCallback('changepage|' + page);
            });
        }
    }

    function onRefreshGridReport(s) {
        getCheckedReport();
        cbpEntryPopupView.PerformCallback('refresh');
    }

    function onCbpViewPopupProcessEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[1] == 'fail')
            showToast('Save Failed', 'Error Message : ' + param[2]);
        else
            pcRightPanelContent.Hide();
    }

    $(function () {
        $('#btnSaveReportUser').click(function () {
            getCheckedReport();
            cbpViewPopupProcess.PerformCallback('save');
        });
    });

    $('#chkSelectAllReport').die('change');
    $('#chkSelectAllReport').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkReport').each(function () {
            $(this).find('input').prop('checked', isChecked);
        });
    });
</script>

<div style="height:450px; overflow-y:auto;overflow-x:hidden">
    <input type="hidden" id="hdnUserID" value="" runat="server" />
    <input type="hidden" id="hdnSiteID" value="" runat="server" />
    <input type="hidden" id="hdnOldSelectedReport" runat="server" value="" />
    <input type="hidden" id="hdnSelectedReport" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("User")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtUserName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Site")%></label></td>
                        <td><asp:TextBox ID="txtSiteName" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr> 
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Module")%></label></td>
                        <td colspan="2">
                            <dxe:ASPxComboBox ID="cboModule" runat="server" Width="100%">
                                <ClientSideEvents ValueChanged="function(s,e){ onRefreshGridReport(s); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>   
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Type")%></label></td>
                        <td colspan="2">
                            <dxe:ASPxComboBox ID="cboReportType" runat="server" Width="100%">
                                <ClientSideEvents ValueChanged="function(s,e){ onRefreshGridReport(s); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>  
                </table>                

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewPopup').show(); }"
                        EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em; ">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" OnRowDataBound="grdView_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                            <HeaderTemplate>
                                                <input id="chkSelectAllReport" type="checkbox" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkReport" runat="server" CssClass="chkReport" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReportID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:TemplateField HeaderStyle-Width="200px" >
                                            <HeaderTemplate>
                                                <div style="padding-left:3px">
                                                    <%=GetLabel("Report Code")%>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style='margin-left:<%# Eval("Level") %>0px;'><%# Eval("ReportCode")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReportName" HeaderText="Report Name" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup">
                                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                                </div>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>

    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpViewPopupProcess" runat="server" Width="100%" ClientInstanceName="cbpViewPopupProcess"
            ShowLoadingPanel="false" OnCallback="cbpViewPopupProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewPopupProcessEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>

    <div style="width:100%;text-align:center">
        <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
            <tr>
                <td><input type="button" value='<%= GetLabel("Save")%>' style="width:70px" id="btnSaveReportUser" /></td>
                <td><input type="button" value='<%= GetLabel("Close")%>' style="width:70px" onclick="pcRightPanelContent.Hide();" /></td>
            </tr>
        </table>
    </div>
</div>