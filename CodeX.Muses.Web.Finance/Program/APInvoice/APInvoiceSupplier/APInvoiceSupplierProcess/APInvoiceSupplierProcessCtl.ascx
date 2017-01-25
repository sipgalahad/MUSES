<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APInvoiceSupplierProcessCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierProcessCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_apinvoicesupplierprocessctl">
    function getCheckedPurchaseReceive() {
        var lstSelectedPurchaseReceive = $('#<%=hdnSelectedPurchaseReceive.ClientID %>').val().split(',');
        var lstSelectedPurchaseReturn = $('#<%=hdnSelectedIncludePurchaseReturn.ClientID %>').val().split(',');
        $('.chkPurchaseReceive input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').val();
                if (lstSelectedPurchaseReceive.indexOf(key) < 0)
                    lstSelectedPurchaseReceive.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').val();
                if (lstSelectedPurchaseReceive.indexOf(key) > -1)
                    lstSelectedPurchaseReceive.splice(lstSelectedPurchaseReceive.indexOf(key), 1);
            }
        });
        $('.chkIsIncludePurchaseReturn input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').val();
                if (lstSelectedPurchaseReturn.indexOf(key) < 0)
                    lstSelectedPurchaseReturn.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').val();
                if (lstSelectedPurchaseReturn.indexOf(key) > -1)
                    lstSelectedPurchaseReturn.splice(lstSelectedPurchaseReturn.indexOf(key), 1);
            }
        });
        $('#<%=hdnSelectedPurchaseReceive.ClientID %>').val(lstSelectedPurchaseReceive.join(','));
        $('#<%=hdnSelectedIncludePurchaseReturn.ClientID %>').val(lstSelectedPurchaseReturn.join(','));
    }

    $('.chkPurchaseReceive input').live('change', function () {
        $(this).closest('tr').find('.chkIsIncludePurchaseReturn input').prop('checked', $(this).is(':checked'));
    });

    $('#chkSelectAllPR').die('change');
    $('#chkSelectAllPR').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkPurchaseReceive').each(function () {
            $chk = $(this).find('input');
            if (!$chk.is(":disabled")) {
                $chk.prop('checked', isChecked);
            }
        });
    });

    function onBeforeSaveRecordPopup(errMessage) {
        getCheckedPurchaseReceive();
        if ($('#<%=hdnSelectedPurchaseReceive.ClientID %>').val() == '') {
            errMessage.text = 'Please Select Purchase Receive First';
            return false;
        }
        return true;
    }

    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCount, function (page) {
            getCheckedPurchaseReceive();
            cbpProcessDetail.PerformCallback('changepage|' + page);
        });
    });

    function onCbpViewEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedPurchaseReceive();
                cbpProcessDetail.PerformCallback('changepage|' + page);
            });
        }
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto;overflow-x: hidden">
    <input type="hidden" id="hdnSelectedPurchaseReceive" runat="server" value="" />
    <input type="hidden" id="hdnSelectedIncludePurchaseReturn" runat="server" value="" />
    <input type="hidden" id="hdnPurchaseInvoiceID" value="" runat="server" />
    <input type="hidden" id="hdnItemID" value="" runat="server" />
    <input type="hidden" id="hdnLabResultID" value="" runat="server" />
    <input type="hidden" id="hdnTransactionID" value="" runat="server" />
    <input type="hidden" id="hdnGCItemType" value="" runat="server" />
    <input type="hidden" id="hdnGCPurchaseType" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <dxcp:ASPxCallbackPanel ID="cbpProcessDetail" runat="server" Width="100%" ClientInstanceName="cbpProcessDetail"
                    ShowLoadingPanel="false" OnCallback="cbpProcessDetail_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                    <EmptyDataTemplate>
                                        <table id="tblView" runat="server" class="tblTransactionEntryResult" cellspacing="0" rules="all" >
                                            <tr>
                                                <th style="width:40px" class="thCenter"><input id="chkSelectAllPR" type="checkbox" /></th>
                                                <th align="center"><%=GetLabel("No. Penerimaan")%></th>
                                                <th style="width:130px" class="thRight"><%=GetLabel("Jumlah Penerimaan")%></th>                             
                                                <th style="width:130px" class="thRight"><%=GetLabel("Nota Kredit")%></th>
                                                <th style="width:130px" class="thRight"><%=GetLabel("Sub Total")%></th>
                                                <th style="width:50px" class="thCenter"><%=GetLabel("Retur")%></th>
                                            </tr>
                                            <tr class="trEmpty">
                                                <td colspan="6">
                                                    <%=GetLabel("No Data To Display")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tblView" runat="server" class="tblTransactionEntryResult" cellspacing="0" rules="all" >
                                            <tr>
                                                <th style="width:40px" class="thCenter"><input id="chkSelectAllPR" type="checkbox" /></th>
                                                <th align="center"><%=GetLabel("No. Penerimaan")%></th>
                                                <th style="width:150px" align="center"><%=GetLabel("No. Faktur")%></th>
                                                <th style="width:160px" class="thRight"><%=GetLabel("Jumlah Penerimaan")%></th>                            
                                                <th style="width:130px" class="thRight"><%=GetLabel("Nota Kredit")%></th>
                                                <th style="width:130px" class="thRight"><%=GetLabel("Sub Total")%></th>
                                                <th style="width:50px" class="thCenter"><%=GetLabel("Retur")%></th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder" ></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <asp:CheckBox ID="chkPurchaseReceive" runat="server" CssClass="chkPurchaseReceive" />
                                                <input type="hidden" class="keyField" id="keyField" runat="server" value='<%# Eval("PurchaseReceiveID")%>' />
                                            </td>
                                            <td><%# Eval("PurchaseReceiveNo")%></td>
                                            <td><%# Eval("ReferenceNo")%></td>
                                            <td align="right"><%# Eval("TotalNetTransactionAmount", "{0:N}")%></td>
                                            <td align="right"><%# Eval("CNAmount", "{0:N}")%></td>
                                            <td align="right"><%# Eval("CustomSubTotal", "{0:N}")%></td>
                                            <td align="center">
                                                <span <%#Eval("CreditNoteID").ToString() == "" ? "style='display:none'" : "" %>><asp:CheckBox ID="chkIsIncludePurchaseReturn" runat="server" Checked="true" CssClass="chkIsIncludePurchaseReturn" /></span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>
</div>