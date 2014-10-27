<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ItemProductList.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.ItemProductList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSync" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsave.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Sync")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSync.ClientID %>').click(function () {
                onCustomButtonClick('sync');
            });

            var grd = new customGridView2();
            grd.init('grdDrug', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#ulTabGrdDetail li').click(function () {
                $('#ulTabGrdDetail li.selected').removeAttr('class');
                $('.containerGrdDt').filter(':visible').hide();
                $contentID = $(this).attr('contentid');
                $('#' + $contentID).show();
                $(this).addClass('selected');

            });

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                return "<%=OnGetItemGroupFilterExpression() %>";
            }

            $('#lblItemGroupMaster.lblLink').live('click', function () {
                openSearchDialog('itemgroup', onGetItemGroupFilterExpression(), function (value) {
                    $('#<%=txtItemGroupCode.ClientID %>').val(value);
                    onTxtItemGroupCodeChanged(value);
                });
            });

            $('#<%=txtItemGroupCode.ClientID %>').live('change', function () {
                onTxtItemGroupCodeChanged($(this).val());
            });

            function onTxtItemGroupCodeChanged(value) {
                var filterExpression = onGetItemGroupFilterExpression() + " AND ItemGroupCode = '" + value + "'";
                Methods.getObject('GetItemGroupMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                    }
                    else {
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                    cbpView.PerformCallback('refresh');
                });
            }
            //#endregion
        });

        function onAfterCustomClickSuccess() {
            cbpView.PerformCallback('refresh');
        }

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
                if (pageCount > 0)
                    $('.grdDrug tr:eq(2)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('.grdDrug tr:eq(2)').click();
        }
        //#endregion

        $('.grdDrug td.tdExpand').live('click', function () {
            $tr = $(this).parent();
            $trDetail = $(this).parent().next();
            $('#containerDetail2').hide();
            if ($trDetail.attr('class') != 'trDetail') {
                $('#ulTabGrdDetail li:eq(0)').click();

                $trCollapse = $('.trDetail');

                $(this).find('.imgExpand').attr('src', '<%= ResolveUrl("~/Libs/Images/down-arrow.png")%>');
                $newTr = $("<tr><td></td><td colspan='5'></td></tr>").attr('class', 'trDetail');
                $newTr.insertAfter($tr);
                $newTr.find('td').last().append($('#containerGrdDetail'));

                if ($trCollapse != null) {
                    $trCollapse.prev().find('.imgExpand').attr('src', '<%= ResolveUrl("~/Libs/Images/right-arrow.png")%>');
                    $trCollapse.remove();
                }

                $('.grdDetail1 tr:gt(0)').remove();
                $('.grdDetail2 tr:gt(0)').remove();
                $('#<%=hdnExpandID.ClientID %>').val($tr.find('.keyField').html());
                cbpViewDetail2.PerformCallback('refresh');
                cbpViewDetail1.PerformCallback('refresh');
            }
            else {
                $(this).find('.imgExpand').attr('src', '<%= ResolveUrl("~/Libs/Images/right-arrow.png")%>');
                $('#tempContainerGrdDetail').append($('#containerGrdDetail'));

                $('.grdDetail1 tr:gt(0)').remove();
                $('.grdDetail2 tr:gt(0)').remove();

                $trDetail.remove();
            }
        });

        function onGetEntryPopupReturnValue() {
            return '';
        }

        function onAfterSaveRightPanelContent(code, value) {
            if($('#containerDetail2').is(":visible"))
                cbpViewDetail2.PerformCallback();
            else if ($('#containerDetail1').is(":visible"))
                cbpViewDetail1.PerformCallback();
        }

        $('.lnkEditItemCost').live('click', function () {
            var itemCostID = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Libs/Program/Master/ItemService/ItemServiceItemCostEntryCtl.ascx");
            openUserControlPopup(url, itemCostID, 'Item Cost', 600, 400);
        });

        $('.lnkEditItemPlanning').live('click', function () {
            var itemPlanningID = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/ItemProduct/ItemPlanningEntryCtl.ascx");
            openUserControlPopup(url, itemPlanningID, 'Perencanaan Persediaan', 600, 500);
        });

        $('.tdAlternateUnit a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/ItemProduct/ItemAlternateUnitEntryCtl.ascx");
            openUserControlPopup(url, id, 'Alternate Unit', 600, 500);
        });
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <input type="hidden" value="" id="hdnExpandID" runat="server" />
    <table>
        <colgroup>
            <col style="width:140px"/>
            <col style="width:500px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblLink" id="lblItemGroupMaster"><%=GetLabel("Kelompok Item")%></label></td>
            <td>
                <input type="hidden" value="" runat="server" id="hdnItemGroupID" />
                <table style="width:100%" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:100px"/>
                        <col style="width:3px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtItemGroupCode" Width="100%" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtItemGroupName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:ListView runat="server" ID="grdView">
                            <EmptyDataTemplate>
                                <table id="tblView" runat="server" class="grdProductLine grdSelected grdBorder" cellspacing="0" rules="all" >
                                    <tr>  
                                        <th class="keyField" rowspan="2">&nbsp;</th>
                                        <th rowspan="2" style="width:20px">&nbsp;</th>
                                        <th rowspan="2" style="width:70px;"><%=GetLabel("Kode")%></th>  
                                        <th rowspan="2" style="text-align:left"><%=GetLabel("Nama Obat")%></th>                              
                                        <th rowspan="2" style="width:250px;text-align:left"><%=GetLabel("Kelompok Obat")%></th>                              
                                        <th colspan="2" class="thCenter"><%=GetLabel("Satuan")%></th>                     
                                    </tr>
                                    <tr>
                                        <th style="width:125px" class="thCenter"><%=GetLabel("Kecil")%></th>  
                                        <th style="width:125px" class="thCenter"><%=GetLabel("Alternatif")%></th>  
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="10">
                                            <%=GetLabel("No Data To Display")%>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table id="tblView" runat="server" class="grdDrug grdSelected grdBorder" cellspacing="0" rules="all" >
                                    <tr>  
                                        <th class="keyField" rowspan="2">&nbsp;</th>
                                        <th rowspan="2" style="width:20px">&nbsp;</th>
                                        <th rowspan="2" style="width:70px;text-align:left"><%=GetLabel("Kode")%></th>  
                                        <th rowspan="2" style="text-align:left"><%=GetLabel("Nama Barang")%></th>                              
                                        <th rowspan="2" style="width:250px;text-align:left"><%=GetLabel("Kelompok Barang")%></th>                              
                                        <th colspan="2" class="thCenter"><%=GetLabel("Satuan")%></th>                     
                                    </tr>
                                    <tr>
                                        <th style="width:125px" class="thCenter"><%=GetLabel("Kecil")%></th>  
                                        <th style="width:125px" class="thCenter"><%=GetLabel("Alternatif")%></th>  
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" ></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="keyField"><%# Eval("ItemID")%></td>
                                    <td align="center" class="tdExpand"><img class="imgExpand imgLink" src='<%= ResolveUrl("~/Libs/Images/right-arrow.png")%>' alt='' /></td>
                                    <td><%# Eval("ItemCode")%></td>
                                    <td><%# Eval("ItemName1")%></td>
                                    <td><%# Eval("ItemGroupName1")%></td>
                                    <td align="center"><%# Eval("ItemUnit")%></td>
                                    <td align="center" class="tdAlternateUnit"><a type="10"><%=GetLabel("Alternate Unit")%></a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>

    <div id="tempContainerGrdDetail" style="display:none">
        <div id="containerGrdDetail" class="borderBox" style="width: 100%;padding: 10px 5px;">
            <div class="containerUlTabPage">
                <ul class="ulTabPage" id="ulTabGrdDetail">
                    <li class="selected" contentid="containerDetail1"><%=GetLabel("Perencanaan") %></li>
                    <li contentid="containerDetail2" style="display:none"><%=GetLabel("Item Cost") %></li>
                </ul>
            </div>
            <div style="position: relative;">
                <div id="containerDetail1" class="containerGrdDt">
                    <dxcp:ASPxCallbackPanel ID="cbpViewDetail1" runat="server" Width="100%" ClientInstanceName="cbpViewDetail1"
                        ShowLoadingPanel="false" OnCallback="cbpViewDetail1_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto">
                                    <asp:ListView runat="server" ID="lvwDetail1">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdView notAllowSelect grdDetail1" cellspacing="0" rules="all" >
                                                <tr>  
                                                    <th class="keyField" rowspan="2"></th>
                                                    <th style="width:50px" rowspan="2">&nbsp;</th>
                                                    <th style="width:300px;text-align:left" rowspan="2"><%=GetLabel("Site")%></th>
                                                    <th style="text-align:left" rowspan="2"><%=GetLabel("Default Supplier")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("Harga")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Rata-Rata")%></th>   
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Satuan Kecil")%></th>
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Satuan Besar")%></th>   
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="20">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>  
                                                    <th class="keyField" rowspan="2"></th>
                                                    <th style="width:50px" rowspan="2">&nbsp;</th>
                                                    <th style="width:300px;text-align:left" rowspan="2"><%=GetLabel("Site")%></th>
                                                    <th rowspan="2" style="text-align:left"><%=GetLabel("Default Supplier")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("Harga")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Rata-Rata")%></th>   
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Satuan Kecil")%></th>
                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Satuan Besar")%></th>   
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" ></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%# Eval("ID")%></td>
                                                <td align="center"><img class="lnkEditItemPlanning imgLink" title="<%=GetLabel("Edit") %>" src='<%# ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt=""  /></td>
                                                <td><%# Eval("SiteName")%></td>
                                                <td><%# Eval("BusinessPartnerName")%></td>
                                                <td align="right"><%# Eval("AveragePrice", "{0:N}")%></td>
                                                <td align="right"><%# Eval("UnitPrice", "{0:N}")%></td>
                                                <td align="right"><%# Eval("PurchaseUnitPrice", "{0:N}")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>    
                    <div class="imgLoadingGrdView" id="containerImgLoadingViewDetail1">
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                </div>
                <div id="containerDetail2" class="containerGrdDt">
                    <dxcp:ASPxCallbackPanel ID="cbpViewDetail2" runat="server" Width="100%" ClientInstanceName="cbpViewDetail2"
                        ShowLoadingPanel="false" OnCallback="cbpViewDetail2_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewDetail2').show(); }"
                            EndCallback="function(s,e){ $('#containerImgLoadingViewDetail2').hide(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                                <asp:Panel runat="server" ID="Panel2" Style="width: 100%; margin-left: auto; margin-right: auto">
                                    <asp:GridView ID="grdDetail2" runat="server" CssClass="grdView notAllowSelect grdDetail2" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCostID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                <HeaderTemplate></HeaderTemplate>
                                                <ItemTemplate>
                                                    <img class="lnkEditItemCost imgLink" title="<%=GetLabel("Edit") %>" src='<%# ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt=""  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SiteName" HeaderText="Site" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="cfTotalMaterial" HeaderText="Material" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"/>
                                            <asp:BoundField DataField="cfTotalLabor" HeaderText="Labor" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"/>
                                            <asp:BoundField DataField="cfTotalOverhead" HeaderText="Overhead" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"/>
                                            <asp:BoundField DataField="cfTotalSubContract" HeaderText="Sub Contract" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"/>
                                            <asp:BoundField DataField="cfTotalBurden" HeaderText="Burden" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"/>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>                                  
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>  
                    <div class="imgLoadingGrdView" id="containerImgLoadingViewDetail2">
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                </div> 
            </div>
        </div>
    </div>
</asp:Content>