<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="COABudgetMonthList.aspx.cs" Inherits=" CodeX.Muses.Web.Accounting.Program.COABudgetMonthList" %>

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
                $txtBudgetAmount1 = $tr.find('.txtBudgetAmount1');
                $txtBudgetAmount2 = $tr.find('.txtBudgetAmount2');
                $txtBudgetAmount3 = $tr.find('.txtBudgetAmount3');
                $txtBudgetAmount4 = $tr.find('.txtBudgetAmount4');
                $txtBudgetAmount5 = $tr.find('.txtBudgetAmount5');
                $txtBudgetAmount6 = $tr.find('.txtBudgetAmount6');
                $txtBudgetAmount7 = $tr.find('.txtBudgetAmount7');
                $txtBudgetAmount8 = $tr.find('.txtBudgetAmount8');
                $txtBudgetAmount9 = $tr.find('.txtBudgetAmount9');
                $txtBudgetAmount10 = $tr.find('.txtBudgetAmount10');
                $txtBudgetAmount11 = $tr.find('.txtBudgetAmount11');
                $txtBudgetAmount12 = $tr.find('.txtBudgetAmount12');
                $txtBudgetAmountYear = $tr.find('.txtBudgetAmountYear');

                var budgetAmount1 = $txtBudgetAmount1.attr('hiddenVal');
                var budgetAmount2 = $txtBudgetAmount2.attr('hiddenVal');
                var budgetAmount3 = $txtBudgetAmount3.attr('hiddenVal');
                var budgetAmount4 = $txtBudgetAmount4.attr('hiddenVal');
                var budgetAmount5 = $txtBudgetAmount5.attr('hiddenVal');
                var budgetAmount6 = $txtBudgetAmount6.attr('hiddenVal');
                var budgetAmount7 = $txtBudgetAmount7.attr('hiddenVal');
                var budgetAmount8 = $txtBudgetAmount8.attr('hiddenVal');
                var budgetAmount9 = $txtBudgetAmount9.attr('hiddenVal');
                var budgetAmount10 = $txtBudgetAmount10.attr('hiddenVal');
                var budgetAmount11 = $txtBudgetAmount11.attr('hiddenVal');
                var budgetAmount12 = $txtBudgetAmount12.attr('hiddenVal');
                var budgetAmountYear = $txtBudgetAmountYear.attr('hiddenVal');

                var param = 'save|' + GLAccountID + '|' + budgetAmount1 + '|' + budgetAmount2 + '|' + budgetAmount3 + '|' + budgetAmount4 + '|' + budgetAmount5 + '|' + budgetAmount6 + '|' + budgetAmount7 + '|' + budgetAmount8 + '|' + budgetAmount9 + '|' + budgetAmount10 + '|' + budgetAmount11 + '|' + budgetAmount12 + '|' + budgetAmountYear;
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
                        <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                            <EmptyDataTemplate>
                                <table id="tblView" runat="server" class="grdSelected grdBorder" cellspacing="0" rules="all" >
                                    <tr>  
                                        <th class="keyField" rowspan="2">&nbsp;</th>
                                        <th rowspan="2" style="width:80px"><%=GetLabel("No. Perkiraan")%></th>
                                        <th rowspan="2"><%=GetLabel("Nama Perkiraan")%></th>
                                        <th colspan="12" class="thCenter"><%=GetLabel("Anggaran Bulan")%></th>
                                        <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Anggaran Tahun")%></th>
                                        <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Simpan")%></th>
                                    </tr>
                                    <tr>
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Januari")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Februari")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Maret")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("April")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Mei")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Juni")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Juli")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Agustus")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("September")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Oktober")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("November")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Desember")%></th>  
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="20">
                                            <%=GetLabel("No Data To Display")%>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table id="tblView" runat="server" class="grdStokTaking grdSelected grdBorder" cellspacing="0" rules="all" >
                                    <tr>  
                                        <th class="keyField" rowspan="2">&nbsp;</th>
                                        <th rowspan="2" style="width:80px"><%=GetLabel("No. Perkiraan")%></th>
                                        <th rowspan="2"><%=GetLabel("Nama Perkiraan")%></th>
                                        <th colspan="12" class="thCenter"><%=GetLabel("Anggaran Bulan")%></th>
                                        <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Anggaran Tahun")%></th>
                                        <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Simpan")%></th>
                                    </tr>
                                    <tr>
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Januari")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Februari")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Maret")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("April")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Mei")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Juni")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Juli")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Agustus")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("September")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Oktober")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("November")%></th>  
                                        <th style="width:60px" class="thCenter"><%=GetLabel("Desember")%></th>  
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" ></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="keyField"><%# Eval("GLAccountID")%></td>
                                    <td><%# Eval("GLAccountNo")%></td>
                                    <td><div style='margin-left: <%# Eval("Level") %>0px;'><%# Eval("GLAccountName")%></div></td>
                                    <td><input type="text" class="txtBudgetAmount1 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount1" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount2 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount2" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount3 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount3" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount4 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount4" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount5 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount5" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount6 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount6" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount7 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount7" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount8 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount8" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount9 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount9" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount10 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount10" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount11 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount11" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmount12 txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmount12" runat="server" style="width:100%" /></td>
                                    <td><input type="text" class="txtBudgetAmountYear txtBudgetAmount txtCurrency min" min="0" id="txtBudgetAmountYear" runat="server" style="width:100%" /></td>
                                    <td align="center"><input type="button" id="btnSave" class="btnSave" enabled="false" value="Simpan" runat="server" /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
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
