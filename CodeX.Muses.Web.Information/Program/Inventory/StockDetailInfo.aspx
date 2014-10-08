<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StockDetailInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StockDetailInfo" %>

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
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView2();
            grd.init('grdStockDetail', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            setDatePicker('<%=txtDateFrom.ClientID %>');
            setDatePicker('<%=txtDateTo.ClientID %>');

            $('#<%=txtDateFrom.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });
            $('#<%=txtDateTo.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=txtItemName.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });
        });

        //#region Location
        function getLocationFilterExpression() {
            var filterExpression = "<%=OnGetLocationFilterExpression() %>";
            return filterExpression;
        }

        $('#lblLocation.lblLink').live('click', function () {
            openSearchDialog('locationroleuser', getLocationFilterExpression(), function (value) {
                $('#<%=txtLocationCode.ClientID %>').val(value);
                onTxtLocationCodeChanged(value);
            });
        });

        $('#<%=txtLocationCode.ClientID %>').live('change', function () {
            onTxtLocationCodeChanged($(this).val());
        });

        function onTxtLocationCodeChanged(value) {
            var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
            Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnLocationID.ClientID %>').val(result.LocationID);
                    $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                }
                else {
                    $('#<%=hdnLocationID.ClientID %>').val('');
                    $('#<%=txtLocationCode.ClientID %>').val('');
                    $('#<%=txtLocationName.ClientID %>').val('');
                }
            });

            cbpView.PerformCallback('refresh');
        }
        //#endregion

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
                    $('.grdStockDetail tr:eq(2)').click();

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('.grdStockDetail tr:eq(2)').click();
        }
        //#endregion

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGrid();
            }, 0);
        }

        function onCboTypeValueChanged() {
            onRefreshGrid();
        }

        function onCboDepartmentValueChanged() {
            onRefreshGrid();
        }

        $('.lblDetail').live('click', function () {
            $tr = $(this).closest('tr');
            var itemID = $tr.find('.keyField').html();

            var url = ResolveUrl("~/Program/Inventory/StockDetailInfoDtCtl.ascx");
            openUserControlPopup(url, itemID, 'Detail Information', 1200, 550);
        });

    </script>
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <input type="hidden" value="" id="hdnID" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <table class="tblEntryContent" style="width:550px;">
                    <colgroup>
                        <col style="width: 150px" />
                        <col style="width: 400px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblLocation"><%=GetLabel("Lokasi") %></label></td>
                        <td>
                            <input type="hidden" id="hdnLocationID" runat="server" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="100px" />
                                    <col width="3px" />
                                    <col width="250px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtLocationCode" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtLocationName" Width="100%" Enabled="false" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Tanggal") %></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateFrom" Width="120px" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateTo" Width="120px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Nama Item")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtItemName" Width="300px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView" ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                             <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                position: relative; font-size: 0.95em;">
                                <input type="hidden" id="hdnFilterExpression" value="" runat="server" />
                                <asp:ListView runat="server" ID="lvwView">
                                    <EmptyDataTemplate>
                                        <table id="tblView" runat="server" class="grdStockDetail grdSelected" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                <th rowspan="2" style="width:60px"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Stok Awal")%></th>
                                                <th colspan="5" class="thCenter"><%=GetLabel("Masuk")%></th>
                                                <th colspan="5" class="thCenter"><%=GetLabel("Keluar")%></th>
                                                <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Stok Akhir")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pembelian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Retur")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Batal Pelayanan")%></th> 

                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pelayanan")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pemakaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Batal Retur")%></th> 
                                            </tr>
                                            <tr class="trEmpty">
                                                <td colspan="25">
                                                    <%=GetLabel("No Data To Display")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tblView" runat="server" class="grdStockDetail grdBorder grdSelected" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                <th rowspan="2" style="width:60px"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Stok Awal")%></th>
                                                <th colspan="5" class="thCenter"><%=GetLabel("Masuk")%></th>
                                                <th colspan="5" class="thCenter"><%=GetLabel("Keluar")%></th>
                                                <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("Stok Akhir")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pembelian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Retur")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Batal Pelayanan")%></th> 

                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pelayanan")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Pemakaian")%></th> 
                                                <th style="width:60px" class="thCenter" align="right"><%=GetLabel("Batal Retur")%></th> 
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder" ></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%# Eval("ItemID")%></td>
                                            <td><label class="lblLink lblDetail"><%# Eval("ItemName1")%></label></td>
                                            <td><%# Eval("ItemUnit")%></td>
                                            <td align="right"><%# Eval("IN_QuantityBEGIN")%></td>
                                            <td align="right"><%# Eval("IN_PurchaseReceive")%></td>
                                            <td align="right"><%# Eval("IN_Distribution")%></td>
                                            <td align="right"><%# Eval("IN_Adjustment")%></td>
                                            <td align="right"><%# Eval("IN_Return")%></td>
                                            <td align="right"><%# Eval("IN_Void")%></td>
                                            <td align="right"><%# Eval("OUT_Charges")%></td>
                                            <td align="right"><%# Eval("OUT_Distribution")%></td>
                                            <td align="right"><%# Eval("OUT_Adjustment")%></td>
                                            <td align="right"><%# Eval("OUT_Consumption")%></td>
                                            <td align="right"><%# Eval("OUT_Void")%></td>
                                            <td align="right"><%# Eval("QuantityEND")%></td>
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
            </td>
        </tr>
    </table>
</asp:Content>
