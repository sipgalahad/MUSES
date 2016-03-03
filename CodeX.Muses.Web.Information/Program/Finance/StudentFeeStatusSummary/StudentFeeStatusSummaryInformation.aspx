<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="StudentFeeStatusSummaryInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentFeeStatusSummaryInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#btnRefresh').click(function () {
                $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
                $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
                $('#<%=hdnSelectedYear.ClientID %>').val(cboYear.GetValue());
                $('#<%=hdnSelectedMonth.ClientID %>').val(cboMonth.GetValue());

                cbpView.PerformCallback('refresh');
            });
        });

        function onCbpViewEndCallback(s) {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.grdView').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            hideLoadingPanel();
        }

    </script>
    <input type="hidden" id="hdnSiteID" runat="server" />
    <input type="hidden" id="hdnSiteName" runat="server" />
    <input type="hidden" id="hdnSelectedYear" runat="server" />
    <input type="hidden" id="hdnSelectedMonth" runat="server" />
    <input type="hidden" id="hdnExportControl" runat="server" />
    <div>
        <table>
            <colgroup>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><%=GetLabel("Site") %></td>
                <td colspan="2"><dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Periode")%></td>
                <td><dxe:ASPxComboBox ID="cboYear" Width="80px" ClientInstanceName="cboYear" runat="server" HorizontalAlign="Center" /></td>
                <td><dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><input type="button" id="btnRefresh" value='<%=GetLabel("Refresh") %>' /></td>
            </tr>
        </table>
    </div>
    <div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <div id="divContainerView">
                            <table class="grdView notAllowSelect grdBorder" rules="all">
                                <tr>
                                    <th rowspan="2"><%=GetLabel("Kelas") %></th>
                                    <th rowspan="2" class="thCenter" style="width:100px;"><%=GetLabel("Jmlh Siswa") %></th>
                                    <th colspan="7" class="thCenter"><%=GetLabel("UANG SEKOLAH") %></th>
                                </tr>
                                <tr>
                                    <th class="thCenter" style="width:100px"><%=GetLabel("Uang Sekolah") %></th>
                                    <th class="thCenter" colspan="2"><%=GetLabel("Sudah Bayar") %></th>
                                    <th class="thCenter" style="width:50px"><%=GetLabel("%") %></th>
                                    <th class="thCenter" colspan="2" style="width:100px"><%=GetLabel("Belum Bayar") %></th>
                                    <th class="thCenter" style="width:50px"><%=GetLabel("%") %></th>
                                </tr>
                                <asp:Repeater ID="rptView" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("SchoolClassCode") %></td>
                                            <td align="center"><%#Eval("StudentCount") %></td>
                                            <td align="right"><%#Eval("StudentAmount", "{0:N}") %></td>
                                            <td align="right" style="width:150px;"><%#Eval("StudentPaidAmount", "{0:N}") %></td>
                                            <td align="right" style="width:50px;"><%#Eval("StudentPaidCount") %></td>
                                            <td align="right"><%#Eval("StudentPaidCountPercentage") %></td>
                                            <td align="right" style="width:150px;"><%#Eval("StudentNotPaidAmount", "{0:N}") %></td>
                                            <td align="right" style="width:50px;"><%#Eval("StudentNotPaidCount") %></td>
                                            <td align="right"><%#Eval("StudentNotPaidCountPercentage") %></td>
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
