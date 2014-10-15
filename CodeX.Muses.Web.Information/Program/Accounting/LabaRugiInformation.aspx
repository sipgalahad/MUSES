<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="LabaRugiInformation.aspx.cs" Inherits="CodeX.Web.Accounting.Program.LabaRugiInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnProcess').click(function () {
                cbpView.PerformCallback('refresh');
            });
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });

            $('#btnProcess').click(function () {
                cbpView.PerformCallback('refresh');
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion    
    </script>
    <div>
        <table width="100%">
            <colgroup>
                <col width="120px" />
                <col />
            </colgroup>
            <tr>
                <td>
                    <table>
                        <colgroup>
                            <col width="120px" />
                            <col width="80px" />
                            <col width="120px" />
                            <col width="80px" />
                            <col />
                        </colgroup>
                        <tr id="trPeriode" runat="server">
                            <td class="tdLabel">
                                <label class="tdLabel"><%=GetLabel("Periode")%></label>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboYear" Width="80px" ClientInstanceName="cboYear" runat="server" HorizontalAlign="Center" />
                                <input type="hidden" id="hdnSelectedYear" runat="server" />
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" />
                                <input type="hidden" value="" id="hdnSelectedMonth" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="tdLabel"><%=GetLabel("Level")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboLevel" Width="120px" ClientInstanceName="cboLevel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><input type="button" id="btnProcess" value="Refresh" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView" ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" >
                                        <Columns>
                                            <asp:BoundField DataField="GLAccountID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" >
                                                <HeaderTemplate>
                                                    <div style="padding-left:3px">
                                                        <%=GetLabel("NOMOR PERKIRAAN")%>
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style='margin-left:<%# Eval("Level") %>0px;'>
                                                        <label <%# Eval("IsHeader").ToString() == "False" ? "class='lblLink lblAccountNo'":"" %> ><%# Eval("GLAccountNo")%></label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="GLAccountName" ItemStyle-HorizontalAlign="Left" HeaderText="NAMA PERKIRAAN" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="BalanceENDLastMonth" ItemStyle-HorizontalAlign="Right" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}" HeaderStyle-Width="100px" HeaderText="Bulan Lalu" HeaderStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="ProfitLoss" ItemStyle-HorizontalAlign="Right" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}" HeaderStyle-Width="100px" HeaderText="Bulan Ini" HeaderStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="BalanceEND" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" HeaderText="s/d Bulan Ini" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}"  HeaderStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                    <div class="imgLoadingGrdView" id="containerImgLoadingView">
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                    <div class="containerPaging">
                        <div class="wrapperPaging">
                            <div id="paging">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
