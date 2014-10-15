<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="GLBalanceInformation.aspx.cs" Inherits="Codex.Muses.Web.Information.Program.GLBalanceInformation" %>

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
                cbpTotal.PerformCallback('refresh');
            });
        });

        $('.lblAccountNo').live('click', function () {
            $tr = $(this).closest('tr');
            var accountID = $tr.find('.keyField').html();

            var url = ResolveUrl('~/Program/Information/GLBalanceInformationCtl.ascx');
            var id = accountID;
            var period = cboYear.GetValue() + '|' + cboMonth.GetValue();
            var param = id + '|' + period;
            openUserControlPopup(url, param, 'Detail', 900, 600);
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

    </script>
    <div>
        <input type="hidden" runat="server" id="hdnID" />
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
                                <label class="tdLabel">
                                <%=GetLabel("Periode")%></label>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboYear" Width="80px" ClientInstanceName="cboYear" runat="server" HorizontalAlign="Center" />
                                <input type="hidden" id="hdnSelectedYear" runat="server" />
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" />
                                <input type="hidden" value="" id="hdnSelectedMonth" runat="server" />
                            </td>
                            <td><input type="button" id="btnProcess" value="Refresh" /></td>
                            <td>
                                <asp:CheckBox runat="server" Text="Tampilkan Perkiraan Detail Saja" ID="chkIsDetailOnly" />
                            </td>     
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
                                            <asp:BoundField DataField="BalanceBEGIN" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="SALDO AWAL" HeaderStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="BalanceDEBIT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="DEBIT" HeaderStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="BalanceCREDIT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="100px" HeaderText="KREDIT" HeaderStyle-HorizontalAlign="Right"  />
                                            <asp:BoundField DataField="BalanceEND" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" HeaderText="SALDO AKHIR" DataFormatString="{0,15:#,##0.00 ;(#,##0.00);-}"  HeaderStyle-HorizontalAlign="Right" />
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
                        <div class="divInformationNumEntries" id="informationNumEntries"></div>
                        <div class="wrapperPaging">
                            <div id="paging">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpTotal" runat="server" Width="100%" ClientInstanceName="cbpTotal" ShowLoadingPanel="false" OnCallback="cbpTotal_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ cbpView.PerformCallback('refresh'); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <table>
                                    <colgroup>
                                        <col style="width: 120px;"/>
                                        <col style="width: 150px;"/>
                                    </colgroup>
                                    <tr>
                                        <td><%=GetLabel("Total Debit") %></td>
                                        <td><asp:TextBox Width="100%" runat="server" ReadOnly="true" CssClass="number" ID="txtTotalBalanceDEBIT" /></td>
                                    </tr>
                                    <tr>
                                        <td><%=GetLabel("Total Kredit") %></td>
                                        <td><asp:TextBox Width="100%" runat="server" ReadOnly="true" CssClass="number" ID="txtTotalBalanceCREDIT" /></td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
