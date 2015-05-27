<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentRevenueInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentRevenueInformation" %>

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
            $('#<%=hdnExportPeriodText.ClientID %>').val($('.hdnTempPeriodText').val());
            hideLoadingPanel();
        }
    </script>
    <style type="text/css">
        .divRemarks             { height: 30px; }
    </style>
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
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnTempPeriodText" class="hdnTempPeriodText" runat="server" />
                        <div id="divContainerView">
                            <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblStudentRevenueInformation">
                                <tr>
                                    <th class="thCenter" style="width: 80px"><%=GetLabel("UNIT") %></th>
                                    <th class="thCenter" style="width: 80px"><%=GetLabel("JUMLAH SISWA") %></th>
                                    <th class="thCenter" style="width: 200px"><%=GetLabel("KETERANGAN") %></th>
                                    <asp:Repeater ID="rptStudentFeeCompType" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter" style="width: 100px"><%#Eval("StudentFeeCompTypeName") %></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptSite" runat="server" OnItemDataBound="rptSite_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center" valign="top" rowspan="2"><%#Eval("SiteName") %></td>
                                            <td align="center" valign="top" rowspan="2" id="tdStudentCount" runat="server"></td>
                                            <td valign="top">
                                                <div class="divRemarks">Usek dibayar ortu</div>
                                                <div class="divRemarks">Usek dibantu gereja (PSE)</div>
                                                <div class="divRemarks">Murid Keluar</div>
                                                <div class="divRemarks">Siswa masuk</div>
                                                <div class="divRemarks">Beasiswa</div>
                                            </td>
                                            <asp:Repeater ID="rptStudentFeeCompTypeDt" runat="server" OnItemDataBound="rptStudentFeeCompTypeDt_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <div class="divRemarks" id="divStudentAmount" runat="server">-</div>
                                                        <div class="divRemarks" id="divPayerAmount" runat="server">-</div>
                                                        <div class="divRemarks" id="divStudentMoveOut" runat="server">-</div>
                                                        <div class="divRemarks" id="divProspectiveStudentAmount" runat="server">-</div>
                                                        <div class="divRemarks" id="divScholarshipAmount" runat="server">-</div>                                                        
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                        <tr>
                                            <td><%=GetLabel("Pendapatan Seharusnya Diterima") %></td>
                                            <asp:Repeater ID="rptStudentFeeCompTypeTotal" runat="server" OnItemDataBound="rptStudentFeeCompTypeTotal_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right" id="tdStudentFeeCompTypeTotal" runat="server"></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
