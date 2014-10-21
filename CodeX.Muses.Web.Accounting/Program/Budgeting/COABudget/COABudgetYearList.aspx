<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="COABudgetYearList.aspx.cs" Inherits=" Codex.Muses.Web.Accounting.Program.COABudgetYearList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

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

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }
        //#endregion

        function onCboYearValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

        $('.lnkSubLedger a').live('click', function () {
            var subLedgerID = $(this).closest('td').find('.hdnSubLedgerID').val();
            if (subLedgerID != '' && subLedgerID != '0') {
                var url = ResolveUrl("~/Program/Master/SubLedger/SubLedgerDtViewCtl.ascx");
                openUserControlPopup(url, subLedgerID, 'Detail', 1000, 520);
            }
        });

        $('.txtBudgetAmount').live('change', function () {
            $tr = $(this).closest('tr');
            $tr.find('.btnSave').removeAttr('enabled');
        });

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                $tr = $(this).closest('tr');
                var GLAccountID = $tr.find('.keyField').html();
                $txtBudgetAmount = $tr.find('.txtBudgetAmount');
                var budgetAmount = $txtBudgetAmount.attr('hiddenVal');

                var param = 'save|' + GLAccountID + '|' + budgetAmount;
                $btnSave = $(this);
                cbpProcess.PerformCallback(param);
            }
        });

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var result = s.cpResult.split('|');
            if (result[1] == 'success') {
                $tr = $btnSave.closest('tr');
                $btnSave.attr('enabled', 'false');
            }
            else {
                if (result[2] != '')
                    showToast('Save Failed', 'Error Message : ' + result[2]);
                else
                    showToast('Save Failed', '');
            }
        }
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <table cellpadding="0" cellspacing="0" style="margin-bottom: 5px;">
        <tr>
            <td style="width:100px" class="tdLabel"><label><%=GetLabel("Tahun") %></label></td>
            <td>
                <dxe:ASPxComboBox ID="cboYear" runat="server" ClientInstanceName="cboYear" Width="150px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboYearValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="max-height:430px;">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="GLAccountID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="120px">
                                    <HeaderTemplate>
                                        <div style="padding-left: 3px; text-align: left">
                                            <%=GetLabel("No. Perkiraan")%>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style='margin-left: <%# Eval("Level") %>0px;'>
                                            <%# Eval("GLAccountNo")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div style="padding-left: 3px; text-align: left">
                                            <%=GetLabel("Nama Perkiraan")%>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style='margin-left: <%# Eval("Level") %>0px;'>
                                            <%# Eval("GLAccountName")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cfIsheader" HeaderText="I/A" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="GLAccountType" HeaderText="Kelompok Perkiraan" HeaderStyle-Width="150px"
                                    HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkSubLedger"
                                    HeaderText="Sub Perkiraan" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <input type="hidden" class="hdnSubLedgerID" value='<%#Eval("SubLedgerID") %>' />
                                        <a><%#Eval("SubLedgerName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Anggaran" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <input type="text" class="txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount" runat="server" style="width:100%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Simpan" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <input type="button" id="btnSave" class="btnSave" enabled="false" value="Simpan" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    </div>
    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
