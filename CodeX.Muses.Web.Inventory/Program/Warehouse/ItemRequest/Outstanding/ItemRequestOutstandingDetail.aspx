<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="ItemRequestOutstandingDetail.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.ItemRequestOutstandingDetail" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnItemReqHdProcess" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Process")%></div></li>
    <li id="btnItemReqHdDecline" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Decline")%></div></li>
    <li id="btnOrderListBack" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><div><%=GetLabel("Back")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            $('#<%=btnItemReqHdDecline.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                    showToast('Warning', 'Please Select Item First');
                }
                else {
                    onCustomButtonClick('decline');
                }
            });

            $('#<%=btnItemReqHdProcess.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    getCheckedMember();
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                        showToast('Warning', 'Please Select Item First');
                    else
                        onCustomButtonClick('approve');
                }
            });

            $('#<%=btnOrderListBack.ClientID %>').click(function () {
                showLoadingPanel();
                document.location = ResolveUrl('~/Program/Warehouse/ItemRequest/Outstanding/ItemRequestOutstandingList.aspx');
            });

            $('.txtDistribution').change(function () {
                if ($(this).val() != '' && $(this).val() != '0') {
                    $(this).closest('tr').find('.txtConsumption').val('0');
                }
            });

            $('.txtConsumption').change(function () {
                if ($(this).val() != '' && $(this).val() != '0') {
                    $(this).closest('tr').find('.txtDistribution').val('0');
                }
            });
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var itemRequestID = $('#<%=hdnOrderID.ClientID %>').val();
            filterExpression.text = "ItemRequestID = " + itemRequestID;
            return true;
        }

        $('.chkIsSelected input').live('change', function () {
            $tr = $(this).closest('tr');
            if ($(this).is(':checked')) {
                if ($('#<%=hdnIsAllowItemDistribution.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnDistributionQty').val()) == 0)
                        $tr.find('.txtDistribution').removeAttr('readonly');
                }
                if ($('#<%=hdnIsAllowItemConsumption.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnConsumptionQty').val()) == 0)
                        $tr.find('.txtConsumption').removeAttr('readonly');
                }
                if ($('#<%=hdnIsAllowPurchaseRequest.ClientID %>').val() == '1') {
                    if (parseInt($tr.find('.hdnPurchaseRequestQty').val()) == 0)
                        $tr.find('.txtPurchaseRequest').removeAttr('readonly');
                }
            }
            else {
                $tr.find('.txtDistribution').attr('readonly', 'readonly');
                $tr.find('.txtConsumption').attr('readonly', 'readonly');
                $tr.find('.txtPurchaseRequest').attr('readonly', 'readonly');
            }
        });

        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        function onAfterCustomClickSuccess(type, retval) {
            var param = retval.split('|');
            var messageText = '';
            if (param[1] != '')
                messageText += 'Permintaan Pembelian Berhasil Dibuat Dengan No Transaksi <b>' + param[1] + '</b>';
            if (param[2] != '') {
                if (messageText != '')
                    messageText += '<br />';
                messageText += 'Distribusi Berhasil Dibuat Dengan No Transaksi <b>' + param[2] + '</b>';
            }
            if (param[3] != '') {
                if (messageText != '')
                    messageText += '<br />';
                messageText += 'Pemakaian Berhasil Dibuat Dengan No Transaksi <b>' + param[3] + '</b>';
            }
            showToast('Save Success', messageText, function () {
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                $('#<%=hdnParamDistribution.ClientID %>').val('');
                $('#<%=hdnParamConsumption.ClientID %>').val('');
                $('#<%=hdnParamPurchaseReq.ClientID %>').val('');
                if (param[0] == '0')
                    $('#<%=btnOrderListBack.ClientID %>').click();
                cbpView.PerformCallback('refresh');
            });
        }

        function getCheckedMember() {
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
            var lstDistribution = $('#<%=hdnParamDistribution.ClientID %>').val().split(',');
            var lstConsumption = $('#<%=hdnParamConsumption.ClientID %>').val().split(',');
            var lstPR = $('#<%=hdnParamPurchaseReq.ClientID %>').val().split(',');
            var result = '';
            $('.grdItemRequest .chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').html();
                    var itemRequestDtDistribution = $tr.find('.txtDistribution').val();
                    var itemRequestDtConsumption = $tr.find('.txtConsumption').val();
                    var itemRequestDtPR = $tr.find('.txtPurchaseRequest').val();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx < 0) {
                        lstSelectedMember.push(key);
                        lstDistribution.push(itemRequestDtDistribution);
                        lstConsumption.push(itemRequestDtConsumption);
                        lstPR.push(itemRequestDtPR);
                    }
                    else {
                        lstDistribution[idx] = itemRequestDtDistribution;
                        lstConsumption[idx] = itemRequestDtConsumption;
                        lstPR[idx] = itemRequestDtPR;
                    }
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').html();
                    var itemRequestDtDistribution = $(this).closest('tr').find('.txtDistribution').val();
                    var itemRequestDtConsumption = $(this).closest('tr').find('.txtConsumption').val();
                    var itemRequestDtPR = $(this).closest('tr').find('.txtPurchaseRequest').val();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx > -1) {
                        lstSelectedMember.splice(idx, 1);
                        lstDistribution.splice(idx, 1);
                        lstConsumption.splice(idx, 1);
                        lstPR.splice(idx, 1);
                    }
                }
            });
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            $('#<%=hdnParamDistribution.ClientID %>').val(lstDistribution.join(','));
            $('#<%=hdnParamConsumption.ClientID %>').val(lstConsumption.join(','));
            $('#<%=hdnParamPurchaseReq.ClientID %>').val(lstPR.join(','));
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        $('.lblAvailableStock.lblLink').live("click", function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            var itemID = $tr.find('.hdnItemID').val();
            var stock = $tr.find('.hdnQuantityEND').val();
            var itemUnit = $tr.find('.hdnItemUnit').val();
            var orderID = $('#<%=hdnOrderID.ClientID %>').val();
            var param = orderID + '|' + itemID + '|' + stock + '|' + itemUnit;
            var url = ResolveUrl("~/Program/Warehouse/ItemRequest/Outstanding/ItemRequestProcessedDtCtl.ascx");
            openUserControlPopup(url, param, 'Detail Penggunaan Item', 800, 500);
        });
    </script>
    <input type="hidden" value="" id="hdnParamID" runat="server" />
    <input type="hidden" value="" id="hdnParamDistribution" runat="server" />
    <input type="hidden" value="" id="hdnParamConsumption" runat="server" />
    <input type="hidden" value="" id="hdnParamPurchaseReq" runat="server" />
    <input type="hidden" value="" id="hdnOrderID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultGCConsumptionType" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" value="" id="hdnIsAllowPurchaseRequest" runat="server" />
    <input type="hidden" value="" id="hdnIsAllowItemConsumption" runat="server" />
    <input type="hidden" value="" id="hdnIsAllowItemDistribution" runat="server" />
    <div style="overflow-y: auto; overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 50%" />
                <col style="width: 50%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" id="lblOrderNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" runat="server" id="lblLocation"><%=GetLabel("Dari Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationIDFrom" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCode" Width="100%" runat="server" ReadOnly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup> 
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" ReadOnly="true" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemOrderTime" Width="100px" CssClass="time" runat="server" ReadOnly="true" Style="text-align: center" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal" runat="server" id="lblLocationTo"><%=GetLabel("Kepada Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationIDTo" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCodeTo" Width="100%" runat="server" ReadOnly="true" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationNameTo" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdItemRequest grdSelected" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">&nbsp;</th>
                                                    <th rowspan="2" style="width: 20px">
                                                        &nbsp;
                                                    </th>
                                                    <th rowspan="2"><%=GetLabel("NAMA BARANG")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("SEDANG DIPROSES")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH PROSES")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 110px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 110px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 110px" class="thCenter"><%=GetLabel("Bisa Digunakan")%></th>

                                                    <th style="width: 110px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                    <th style="width: 110px" class="thCenter"><%=GetLabel("Diterima")%></th>

                                                    <th style="width: 150px" class="thCenter"><%=GetLabel("Distribusi")%></th>
                                                    <th style="width: 150px" class="thCenter"><%=GetLabel("Pemakaian")%></th>
                                                    <th style="width: 150px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="10">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdItemRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                                <tr>
                                                    <th class="keyField" rowspan="2">
                                                        
                                                    </th>
                                                    <th rowspan="2" style="width: 20px; text-align: center">
                                                        <input id="chkSelectAll" type="checkbox" />
                                                    </th>
                                                    <th rowspan="2"><%=GetLabel("NAMA BARANG")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH BARANG")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("SEDANG DIPROSES")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JUMLAH PROSES")%></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diminta")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Tersedia")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Bisa Digunakan")%></th>

                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                    <th style="width: 100px" class="thCenter"><%=GetLabel("Diterima")%></th>

                                                    <th style="width: 140px" class="thCenter"><%=GetLabel("Distribusi")%></th>
                                                    <th style="width: 140px" class="thCenter"><%=GetLabel("Pemakaian")%></th>
                                                    <th style="width: 140px" class="thCenter"><%=GetLabel("Minta Beli")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%# Eval("ID")%></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                </td>
                                                <td>
                                                    <input type="hidden" value='<%#Eval("ItemID") %>' class="hdnItemID" />
                                                    <input type="hidden" value='<%#Eval("ItemUnit") %>' class="hdnItemUnit" />
                                                    <input type="hidden" value='<%#Eval("EndingBalance") %>' class="hdnQuantityEND" />
                                                    <%# Eval("ItemName1")%>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:35px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("Quantity")%></td>
                                                            <td>&nbsp;<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:35px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("EndingBalance")%></td>
                                                            <td>&nbsp;<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:35px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right">
                                                                <label id="lblAvailableStock" runat="server" class="lblAvailableStock lblLink"></label>
                                                            </td>    
                                                            <td>&nbsp<%# Eval("BaseUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:35px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("PurchaseRequestQty")%></td>
                                                            <td>&nbsp;<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:35px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("PurchaseRequestReceivedQty")%></td>
                                                            <td>&nbsp;<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtDistribution" Width="75px" runat="server" value="0" CssClass="number max txtDistribution" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp; <%# Eval("ItemUnit")%>
                                                                <input type="hidden" class="hdnDistributionQty" value='<%# Eval("DistributionQty")%>' />
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                        
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtConsumption" Width="75px" runat="server" value="0" CssClass="number max txtConsumption" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp; <%# Eval("ItemUnit")%>
                                                                <input type="hidden" class="hdnConsumptionQty" value='<%# Eval("ConsumptionQty")%>' />
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtPurchaseRequest" Width="75px" runat="server" value="0" CssClass="number txtPurchaseRequest" ReadOnly="true"/></td>
                                                            <td>
                                                                &nbsp; <%# Eval("ItemUnit")%>
                                                                <input type="hidden" class="hdnPurchaseRequestQty" value='<%# Eval("PurchaseRequestQty")%>' />
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
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
    </div>
</asp:Content>
