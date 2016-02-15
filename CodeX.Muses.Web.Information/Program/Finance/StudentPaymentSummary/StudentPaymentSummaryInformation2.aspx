<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentPaymentSummaryInformation2.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentPaymentSummaryInformation2" %>

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
            $tempDiv.find('.tblStudentPaymentInformation').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            $('#<%=hdnExportPeriodText.ClientID %>').val($('.hdnTempPeriodText').val());
            hideLoadingPanel();
        }

        $('.lblThisMonth').live('click', function () {
            $td = $(this).closest('td');
            var siteID = $td.find('.hdnSiteID').val();
            var studentFeeCompTypeID = $(this).closest('tr').find('.hdnStudentFeeCompTypeID').val();
            openDetail(siteID, studentFeeCompTypeID, 'ThisMonth');
        });

        $('.lblDownPayment').live('click', function () {
            $td = $(this).closest('td');
            var siteID = $td.find('.hdnSiteID').val();
            var studentFeeCompTypeID = $(this).closest('tr').find('.hdnStudentFeeCompTypeID').val();
            openDetail(siteID, studentFeeCompTypeID, 'DownPayment');
        });

        $('.lblProspectiveStudent').live('click', function () {
            $td = $(this).closest('td');
            var siteID = $td.find('.hdnSiteID').val();
            var studentFeeCompTypeID = $(this).closest('tr').find('.hdnStudentFeeCompTypeID').val();
            openDetail(siteID, studentFeeCompTypeID, 'ProspectiveStudent');
        });

        $('.lblARStudent').live('click', function () {
            $td = $(this).closest('td');
            var siteID = $td.find('.hdnSiteID').val();
            var studentFeeCompTypeID = $(this).closest('tr').find('.hdnStudentFeeCompTypeID').val();
            openDetail(siteID, studentFeeCompTypeID, 'ARStudent');
        });

        function openDetail(siteID, studentFeeCompTypeID, type) {
            var url = ResolveUrl("~/Program/Finance/StudentPaymentSummary/StudentPaymentSummaryInformationDtCtl.ascx");
            var param = siteID + '|' + cboMonth.GetValue() + '|' + cboYear.GetValue() + '|' + type + '|' + studentFeeCompTypeID;
            openUserControlPopup(url, param, 'Detail Information', 1200, 550);
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
                            <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblStudentPaymentInformation">
                                <tr>
                                    <th class="thCenter" colspan="2"><%=GetLabel("URAIAN") %></th>
                                    <asp:Repeater ID="rptSite" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter" style="width: 100px"><%#Eval("SiteName") %></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th class="thCenter" style="width: 100px"><%=GetLabel("JUMLAH") %></th>
                                </tr>
                                <asp:Repeater ID="rptStudentFeeCompType" runat="server" OnItemDataBound="rptStudentFeeCompType_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center" rowspan="5" valign="top" style="width:60px"><b><%# Container.ItemIndex + 1 %></b></td>
                                            <td valign="top">
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' />
                                                <b><%#Eval("StudentFeeCompTypeName")%></b>
                                            </td>
                                            <asp:Repeater ID="rptSiteDt1" runat="server">
                                                <ItemTemplate>
                                                    <td align="right">&nbsp;</td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bulan Ini                                                
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' />
                                            </td>
                                            <asp:Repeater ID="rptSiteDt2" runat="server" OnItemDataBound="rptSiteDt2_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <input type="hidden" class="hdnSiteID" value='<%#Eval("SiteID") %>' />
                                                        <label id="lblStudentReceiveAmount" runat="server" class="lblStudentReceiveAmount lblThisMonth lblLink"></label>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right" id="tdTotalThisMonth" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Uang muka (bln yang akan datang)                                             
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' />
                                            </td>
                                            <asp:Repeater ID="rptSiteDt3" runat="server" OnItemDataBound="rptSiteDt3_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <input type="hidden" class="hdnSiteID" value='<%#Eval("SiteID") %>' />
                                                        <label id="lblStudentReceiveAmount" runat="server" class="lblStudentReceiveAmount lblDownPayment lblLink"></label>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right" id="tdTotalDP" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Siswa baru masuk                                        
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' />
                                            </td>
                                            <asp:Repeater ID="rptSiteDt4" runat="server" OnItemDataBound="rptSiteDt4_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <input type="hidden" class="hdnSiteID" value='<%#Eval("SiteID") %>' />
                                                        <label id="lblStudentReceiveAmount" runat="server" class="lblStudentReceiveAmount lblProspectiveStudent lblLink"></label>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right" id="tdTotalProspectiveStudent" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Piutang                                     
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' />
                                            </td>
                                            <asp:Repeater ID="rptSiteDt5" runat="server" OnItemDataBound="rptSiteDt5_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right">
                                                        <input type="hidden" class="hdnSiteID" value='<%#Eval("SiteID") %>' />
                                                        <label id="lblStudentReceiveAmount" runat="server" class="lblStudentReceiveAmount lblARStudent lblLink"></label>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right" id="tdTotalAR" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td><b><%=GetLabel("Total") %> <%#Eval("StudentFeeCompTypeName")%></b></td>
                                            <asp:Repeater ID="rptSiteTotal" runat="server" OnItemDataBound="rptSiteTotal_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="right" id="tdStudentFeeCompTypeTotal" runat="server"></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="right" id="tdTotalStudentFeeCompType" runat="server"></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><b><%=GetLabel("Total Seluruhnya") %></b></td>
                                    <asp:Repeater ID="rptSiteGrandTotal" runat="server" OnItemDataBound="rptSiteGrandTotal_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="right" id="tdStudentFeeCompTypeTotal" runat="server"></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td align="right" id="tdStudentFeeCompTypeGrandTotal" runat="server"></td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
