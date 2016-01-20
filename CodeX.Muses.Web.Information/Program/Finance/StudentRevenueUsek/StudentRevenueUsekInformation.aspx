<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentRevenueUsekInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentRevenueUsekInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            });

            onCbpViewEndCallback();
        });

        function onCbpViewEndCallback() {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.tblStudentRevenueInformation').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            hideLoadingPanel();
        }

        $('.lblStudentFeeMonth').live('click', function () {
            var url = ResolveUrl("~/Program/Finance/StudentRevenueUsek/StudentRevenueUsekInformationDtCtl.ascx");
            var siteID = $(this).attr('siteid');
            var month = $(this).attr('month');
            var year = $(this).attr('year');
            var param = siteID + '|' + month + '|' + year;
            openUserControlPopup(url, param, 'Detail Information', 900, 550);
        });
    </script>
    <style type="text/css">
        .divRemarks             { height: 30px; }
    </style>
    <input type="hidden" id="hdnExportControl" runat="server" />
    <table>
        <colgroup>
            <col width="120px" />
            <col width="80px" />
            <col width="120px" />
            <col width="80px" />
            <col />
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px" /></td>
            <td><input type="button" id="btnRefresh" value="Refresh" /></td>   
        </tr>
    </table>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnTempPeriodText" class="hdnTempPeriodText" runat="server" />
                        <div id="divContainerView">
                            <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblStudentRevenueInformation">
                                <tr>
                                    <th class="thCenter" style="width: 80px" rowspan="2"><%=GetLabel("UNIT") %></th>
                                    <th class="thCenter" colspan="12"><%=GetLabel("UANG SEKOLAH") %></th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptMonth" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter"><%# ((DateTime)GetDataItem()).ToString("MMM yyyy") %></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptSite" runat="server" OnItemDataBound="rptSite_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center" valign="top"><%#Eval("SiteName") %></td>
                                            <asp:Repeater ID="rptStudentFeeMonth" runat="server" OnItemDataBound="rptStudentFeeMonth_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <label class="lblLink lblStudentFeeMonth" runat="server" id="lblStudentFeeMonth"></label>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td><%#Eval("Total") %></td>
                                    <asp:Repeater ID="rptStudentFeeMonthTotal" runat="server" OnItemDataBound="rptStudentFeeMonthTotal_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="right" id="tdStudentFeeMonth" runat="server"></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
