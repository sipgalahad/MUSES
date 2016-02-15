<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentPaymentSummaryInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentPaymentSummaryInformation" %>

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
        function onCbpViewEndCallback(s) {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.tblStudentPaymentInformation').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            $('#<%=hdnExportPeriodText.ClientID %>').val($('.hdnTempPeriodText').val());
            hideLoadingPanel();
        }

        function onCboSiteValueChanged() {
            $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
            $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
        }
    </script>
    <style type="text/css">
        .divRemarks             { height: 30px; }
    </style>
    <input type="hidden" value="" id="hdnSiteID" runat="server" />
    <input type="hidden" value="" id="hdnSiteName" runat="server" />
    <input type="hidden" id="hdnExportControl" runat="server" />
    <input type="hidden" id="hdnExportPeriodText" runat="server" />
    <table>
        <colgroup>
            <col width="120px" />
            <col width="80px" />
            <col width="120px" />
            <col width="80px" />
            <col />
        </colgroup>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
            <td colspan="3">
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr id="trPeriode" runat="server">
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Periode")%></label></td>
            <td><dxe:ASPxComboBox ID="cboYear" Width="80px" ClientInstanceName="cboYear" runat="server" HorizontalAlign="Center" /></td>
            <td><dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" /></td>
            <td><input type="button" id="btnRefresh" value="Refresh" /></td>   
        </tr>
    </table>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlGridView" CssClass="pnlContainerGrid" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:auto;">
                        <input type="hidden" id="hdnTempPeriodText" class="hdnTempPeriodText" runat="server" />
                        <div id="divContainerView">
                            <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="1" rules="all" class="grdSelected grdBorder">
                                        <colgroup>
                                            <col/>
                                            <col style="width:200px"/>
                                            <col style="width:200px"/>
                                            <col style="width:200px"/>
                                            <col style="width:200px"/>
                                            <col style="width:200px"/>
                                        </colgroup>
                                        <tr>
                                            <th rowspan="2" class="thCenter"><%=GetLabel("Tanggal") %></th> 
                                            <th colspan="4" class="thCenter"><%=GetLabel("Jenis Pembayaran") %></th>
                                            <th rowspan="2" class="thCenter"><%=GetLabel("Total") %></th>
                                        </tr>
                                        <tr>
                                            <th class="thCenter"><%=GetLabel("Uang Sekolah") %></th>
                                            <th class="thCenter"><%=GetLabel("Uang Kegiatan") %></th>
                                            <th class="thCenter"><%=GetLabel("Uang Pembangunan") %></th>
                                            <th class="thCenter"><%=GetLabel("Denda") %></th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="right"><%# Container.ItemIndex + 1%></td>
                                        <td align="right"><div id="divUsek" runat="server"></div></td>
                                        <td align="right"><div id="divKeg" runat="server"></div></td>
                                        <td align="right"><div id="divPemb" runat="server"></div></td>
                                        <td align="right"><div id="divDenda" runat="server"></div></td>
                                        <td align="right"><div id="divTotal" runat="server"></div></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                                <tr>
                                    <td style="font-weight:bold;" align="right"><%=GetLabel("Total") %></td>
                                    <td align="right"><div id="divTotalUsek" runat="server"></div></td>
                                    <td align="right"><div id="divTotalKeg" runat="server"></div></td>
                                    <td align="right"><div id="divTotalPemb" runat="server"></div></td>
                                    <td align="right"><div id="divTotalDenda" runat="server"></div></td>
                                    <td align="right"><div id="divTotalAll" runat="server"></div></td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
