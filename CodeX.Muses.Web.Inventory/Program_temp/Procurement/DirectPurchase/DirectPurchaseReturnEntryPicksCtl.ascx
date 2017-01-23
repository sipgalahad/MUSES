<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchaseReturnEntryPicksCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseReturnEntryPicksCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_directpurchasereturnentryctl">
    $row = null;
    $(function () {
        $row = $(this).closest('tr');
        $editRow = null;
        $selectedRow = null;

        $('.chkItem input').die('change');
        $('.chkItem input').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboPurchaseReturnReason = eval('cboPurchaseReturnReason' + idx);
            var isChecked = $(this).is(":checked");
            if (isChecked) {
                $tr.find('.txtQtyRetur').removeAttr("readonly");
                cboPurchaseReturnReason.SetEnabled(true);
            }
            else {
                $tr.find('.txtQtyRetur').attr("readonly", "readonly");
                cboPurchaseReturnReason.SetEnabled(false);
            }
        });

        setTimeout(function () {
            $('.chkItem input').each(function () {
                $(this).change();
            });
        }, 500);
    });

    function onBeforeSaveRecord(errMessage) {
        getCheckedItem();
        if ($('#<%=hdnSelectedItem.ClientID %>').val() == '') {
            errMessage.text = 'Please Select Item First';
            return false;
        }
        return true;
    }

    function getCheckedItem() {
        var lstSelectedItem = '';
        var lstSelectedQtyRetur = '';
        var lstSelectedReturnReason = '';
        $('.chkItem input').each(function () {
            if ($(this).is(':checked')) {
                $tr = $(this).closest('tr');
                var key = $tr.find('.keyField').val();
                var qty = $tr.find('.txtQtyRetur').val();
                var idx = $tr.find('.hdnItemIndex').val();
                var cboPurchaseReturnReason = eval('cboPurchaseReturnReason' + idx);
                if (lstSelectedItem != '') {
                    lstSelectedItem += ',';
                    lstSelectedQtyRetur += ',';
                    lstSelectedReturnReason += ',';
                }
                lstSelectedItem += key;
                lstSelectedQtyRetur += qty;
                lstSelectedReturnReason += cboPurchaseReturnReason.GetValue();
            }
        });
        $('#<%=hdnSelectedItem.ClientID %>').val(lstSelectedItem);
        $('#<%=hdnSelectedQtyRetur.ClientID %>').val(lstSelectedQtyRetur);
        $('#<%=hdnSelectedReturnReason.ClientID %>').val(lstSelectedReturnReason); 
    }

    $('#chkSelectAllItem').die('change');
    $('#chkSelectAllItem').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkItem').each(function () {
            $chk = $(this).find('input');
            if (!$chk.is(":disabled")) {
                $chk.prop('checked', isChecked);
                $chk.change();
            }
        });
    });
</script>

<div style="height:440px; overflow-y:auto;overflow-x: hidden">
    <input type="hidden" id="hdnSelectedItem" runat="server" value="" />
    <input type="hidden" id="hdnSelectedQtyRetur" runat="server" value="" />
    <input type="hidden" id="hdnSelectedReturnReason" runat="server" value="" />
    <input type="hidden" id="hdnVisitID" value="" runat="server" />
    <input type="hidden" id="hdnDirectPurchaseID" value="" runat="server" />
    <input type="hidden" id="hdnDirectPurchaseReturnID" value="" runat="server" />
    <input type="hidden" id="hdnTransactionID" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <dxcp:ASPxCallbackPanel ID="cbpProcessDetail" runat="server" Width="100%" ClientInstanceName="cbpProcessDetail"
                    ShowLoadingPanel="false">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                    <EmptyDataTemplate>
                                        <table id="tblView" runat="server" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                                            <tr>
                                                <th style="width:40px" class="thCenter"><input id="chkSelectAllItem" type="checkbox" /></th>
                                                <th style="width:100px"><%=GetLabel("Kode Item")%></th>
                                                <th align="center"><%=GetLabel("Nama")%></th>
                                                <th style="width:140px" class="thRight"><%=GetLabel("Quantity Beli")%></th>                                
                                                <th style="width:100px" class="thRight"><%=GetLabel("Quantity Retur")%></th>
                                                <th style="width:60px" class="thCenter"><%=GetLabel("Satuan")%></th>
                                                <th style="width:100px" class="thRight"><%=GetLabel("Harga")%></th>
                                                <th style="width:120px" class="thRight"><%=GetLabel("Alasan Retur")%></th>
                                            </tr>
                                            <tr class="trEmpty">
                                                <td colspan="7">
                                                    <%=GetLabel("No Data To Display")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tblView" runat="server" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                                            <tr>
                                                <th style="width:40px" class="thCenter"><input id="chkSelectAllItem" type="checkbox" /></th>
                                                <th style="width:100px"><%=GetLabel("Kode Item")%></th>
                                                <th align="center"><%=GetLabel("Nama")%></th>
                                                <th style="width:140px" class="thRight"><%=GetLabel("Quantity Beli")%></th>                                
                                                <th style="width:100px" class="thRight"><%=GetLabel("Quantity Retur")%></th>
                                                <th style="width:60px" class="thCenter"><%=GetLabel("Satuan")%></th>
                                                <th style="width:100px" class="thRight"><%=GetLabel("Harga")%></th>
                                                <th style="width:120px" class="thRight"><%=GetLabel("Alasan Retur")%></th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder" ></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <asp:CheckBox ID="chkItem" runat="server" CssClass="chkItem" />
                                                <input type="hidden" class="keyField" id="keyField" runat="server" value='<%# Eval("ID")%>' />
                                            </td>
                                            <td><%# Eval("ItemCode")%></td>
                                            <td><%# Eval("ItemName1")%></td>
                                            <td align="right"><%# Eval("CustomItemUnit")%></td>
                                            <td align="center"><asp:TextBox ID="txtQtyRetur" Text="0" Width="80px" runat="server" CssClass="txtQtyRetur number max"/></td>
                                            <td align="center"><%# Eval("ItemUnit")%></td>
                                            <td align="right"><%# Eval("UnitPrice", "{0:N}")%></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                                <dxe:ASPxComboBox ID="cboPurchaseReturnReason" runat="server" Width="100%" />
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