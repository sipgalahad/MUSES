<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseReceiveDetailCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseReceiveDetailCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    //#region Order No
    function getPurchaseOrderExpression() {
        var filterExpression = "<%=filterExpressionPurchaseOrder %>";
        return filterExpression;
    }

    $('#lblOrderNo.lblLink').click(function () {
        openSearchDialog('purchaseorderhd', getPurchaseOrderExpression(), function (value) {
            $('#<%=txtOrderNo.ClientID %>').val(value);
            onTxtOrderNoChanged(value);
        });
    });

    $('#<%=txtOrderNo.ClientID %>').change(function () {
        onTxtOrderNoChanged($(this).val());
    });

    function onTxtOrderNoChanged(value) {
        var filterExpression = "PurchaseOrderNo = '" + value + "'";
        Methods.getObject('GetPurchaseOrderHdList', filterExpression, function (result) {
            if (result != null) 
                $('#<%=hdnOrderID.ClientID %>').val(result.PurchaseOrderID);
            else 
                $('#<%=hdnOrderID.ClientID %>').val('');
            cbpViewPopup.PerformCallback("refresh");
        });
    }
    //#endregion

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
        $('.grdPurchaseReceiveDt .txtCurrency').each(function () {
            $(this).trigger('changeValue');
        });

        $('.txtReceivedItem').each(function () {
            $(this).change();
        });

        $('.grdPurchaseReceiveDt tr:gt(0)').each(function () {
            $txtExpired = $(this).find('.txtExpired');
            if ($txtExpired != null) {
                setDatePickerElement($txtExpired);
                $txtExpired.val('<%=DateTimeNowDatePicker() %>');
            }
        });
    }

    $('#chkSelectAll').die('change');
    $('#chkSelectAll').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkIsSelected input').each(function () {
            $(this).prop('checked', isChecked);
            $(this).change();
        });
    });

    $('.chkIsSelected input').die('change');
    $('.chkIsSelected input').live('change', function () {
        $tr = $(this).closest('tr');
        if ($(this).is(':checked')) {
            $tr.find('.txtReceivedItem').removeAttr('readonly');
            $tr.find('.txtUnitPrice').removeAttr('readonly');
            $tr.find('.txtDiscountPercentage1').removeAttr('readonly');
            $tr.find('.txtDiscount1').removeAttr('readonly');
            $tr.find('.txtDiscountPercentage2').removeAttr('readonly');
            $tr.find('.txtDiscount2').removeAttr('readonly');
            $tr.find('.txtBatchNo').removeAttr('readonly');
            $tr.find('.txtExpired').removeAttr('readonly');
            $tr.find('.lblPurchaseUnit').addClass('lblLink');
        }
        else {
            $tr.find('.txtReceivedItem').attr('readonly', 'readonly');
            $tr.find('.txtUnitPrice').attr('readonly', 'readonly');
            $tr.find('.txtDiscountPercentage1').attr('readonly', 'readonly');
            $tr.find('.txtDiscount1').attr('readonly', 'readonly');
            $tr.find('.txtDiscountPercentage2').attr('readonly', 'readonly');
            $tr.find('.txtDiscount2').attr('readonly', 'readonly');
            $tr.find('.txtBatchNo').attr('readonly', 'readonly');
            $tr.find('.txtExpired').attr('readonly', 'readonly');
            $tr.find('.lblPurchaseUnit').removeClass('lblLink');
        }
    });

    $('.txtReceivedItem').die('change');
    $('.txtReceivedItem').live('change', function () {
        var $tr = $(this).closest('tr');
        $tr.find('.txtDiscountPercentage1').change();
    });

    $('.txtUnitPrice').die('change');
    $('.txtUnitPrice').live('change', function () {
        $(this).blur();
        var $tr = $(this).closest('tr');
        $tr.find('.txtDiscountPercentage1').change();
    });

    $('.txtDiscountPercentage1').die('change');
    $('.txtDiscountPercentage1').live('change', function () {
        var $tr = $(this).closest('tr');
        var discountPercentage = parseFloat($(this).val());
        var receivedItem = parseFloat($tr.find('.txtReceivedItem').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var discountAmount = (receivedItem * unitPrice) * discountPercentage / 100;
        $tr.find('.txtDiscount1').val(discountAmount).trigger('changeValue');

        $tr.find('.txtDiscountPercentage2').change();
    });

    $('.txtDiscount1').die('change');
    $('.txtDiscount1').live('change', function () {
        $(this).blur();
        var $tr = $(this).closest('tr');
        var discountAmount = parseFloat($(this).attr('hiddenVal'));
        var receivedItem = parseFloat($tr.find('.txtReceivedItem').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var discountPercentage = (discountAmount * 100) / (receivedItem * unitPrice);
        $tr.find('.txtDiscountPercentage1').val(discountPercentage);

        $tr.find('.txtDiscountPercentage2').change();
    });

    $('.txtDiscount2').die('change');
    $('.txtDiscount2').live('change', function () {
        $(this).blur();
        var $tr = $(this).closest('tr');
        var discountAmount = parseFloat($(this).attr('hiddenVal'));
        var discountAmount1 = parseFloat($tr.find('.txtDiscount1').attr('hiddenVal'));
        var receivedItem = parseFloat($tr.find('.txtReceivedItem').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var discountPercentage = (discountAmount * 100) / ((receivedItem * unitPrice) - discountAmount1);
        $tr.find('.txtDiscountPercentage2').val(discountPercentage);
    });

    $('.txtDiscountPercentage2').die('change');
    $('.txtDiscountPercentage2').live('change', function () {
        var $tr = $(this).closest('tr');
        var discountPercentage = parseFloat($(this).val());
        var discountAmount1 = parseFloat($tr.find('.txtDiscount1').attr('hiddenVal'));
        var receivedItem = parseFloat($tr.find('.txtReceivedItem').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var discountAmount = ((receivedItem * unitPrice) - discountAmount1) * discountPercentage / 100;
        $tr.find('.txtDiscount2').val(discountAmount).trigger('changeValue');
    });

    function onBeforeSaveRecord(errMessage) {
        var count = 0;
        $('.chkIsSelected input').each(function () {
            if ($(this).is(':checked')) {
                count += 1;
            }
        });
        if (count == 0) {
            errMessage.text = 'Please Select Item First';
            return false;
        }
        return true;
    }

    //#region Purchase Unit
    function getPurchaseUnitFilterExpression() {
        var filterExpression = "ItemID = " + itemID;
        return filterExpression;
    }

    var itemID = 0;
    $tr = null;
    $('.lblPurchaseUnit.lblLink').live('click', function () {
        $tr = $(this).closest('tr');
        itemID = $tr.find('.hdnItemID').val();
        openSearchDialog('itemalternateunit', getPurchaseUnitFilterExpression(), function (value) {
            onTxtPurchaseUnitChanged(value);
        });
    });

    function onTxtPurchaseUnitChanged(value) {
        var filterExpression = getPurchaseUnitFilterExpression() + " AND GCAlternateUnit = '" + value + "'";
        Methods.getObject('GetvItemAlternateUnitList', filterExpression, function (result) {
            if (result != null) {
                var baseUnit = $tr.find('.hdnBaseUnit').val();
                $tr.find('.hdnGCPurchaseUnit').val(result.GCAlternateUnit);
                $tr.find('.lblPurchaseUnit').html(result.AlternateUnit);
                $tr.find('.hdnConversionFactor').val(result.ConversionFactor);
                $tr.find('.lblConversion').html("1 " + result.AlternateUnit + " = " + result.ConversionFactor + " " + baseUnit);
            }
            else {
                $tr.find('.hdnGCPurchaseUnit').val('');
                $tr.find('.lblPurchaseUnit').html('');
                $tr.find('.hdnConversionFactor').val('');
                $tr.find('.lblConversion').val('');
            }
        });
    }
    //#endregion
</script>
<input type="hidden" id="hdnSupplierID" value="" runat="server" />
<input type="hidden" id="hdnSelectedMember" runat="server" value="" />

<table class="tblContentArea">
    <tr>
        <td style="padding: 5px; vertical-align: top">
            <table class="tblEntryContent" style="width: 50%">
                <colgroup>
                    <col style="width: 30%" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">
                        <label class="lblLink" id="lblOrderNo">
                            <%=GetLabel("No. Pemesanan")%></label>
                    </td>
                    <td>
                        <input type="hidden" id="hdnOrderID" value="" runat="server" />
                        <asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                            position: relative; font-size: 0.95em;">
                            <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                <EmptyDataTemplate>
                                     <table id="tblView" runat="server" class="grdBorder grdPurchaseReceiveDt grdNormal" cellspacing="0" rules="all" >
                                        <tr>
                                            <th style="width:40px" class="thCenter" rowspan="2"><input id="chkSelectAll" type="checkbox" /></th>
                                            <th rowspan="2"><%=GetLabel("Item")%></th>
                                            <th class="thRight" style="width:40px;" rowspan="2"><%=GetLabel("Sisa")%></th>
                                            <th class="thCenter" style="width:70px;" rowspan="2"><%=GetLabel("Diterima")%></th>
                                            <th class="thCenter" style="width:90px;" rowspan="2"><%=GetLabel("Harga")%></th>
                                            <th class="thCenter" style="width:70px;" rowspan="2"><%=GetLabel("Satuan")%></th>
                                            <th class="thCenter" colspan="2"><%=GetLabel("Diskon 1")%></th>
                                            <th class="thCenter" colspan="2"><%=GetLabel("Diskon 2")%></th>
                                            <th class="thCenter" style="width:150px;" rowspan="2"><%=GetLabel("Konversi")%></th>
                                            <th class="thCenter" style="width:80px;" rowspan="2"><%=GetLabel("No Serial")%></th>
                                            <th class="thCenter" style="width:90px;" rowspan="2"><%=GetLabel("No Batch")%></th>
                                            <th class="thCenter" style="width:120px;" rowspan="2"><%=GetLabel("Expired")%></th>
                                        </tr>
                                        <tr>
                                            <th class="thCenter" style="width:50px;"><%=GetLabel("%")%></th>
                                            <th class="thCenter" style="width:80px;"><%=GetLabel("Nilai")%></th>
                                            <th class="thCenter" style="width:50px;"><%=GetLabel("%")%></th>
                                            <th class="thCenter" style="width:80px;"><%=GetLabel("Nilai")%></th>
                                        </tr>
                                        <tr class="trEmpty">
                                            <td colspan="20">
                                                <%=GetLabel("No Data To Display")%>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table id="tblView" runat="server" class="grdBorder grdPurchaseReceiveDt grdNormal" cellspacing="0" rules="all" >
                                        <tr>
                                            <th style="width:40px" class="thCenter" rowspan="2"><input id="chkSelectAll" type="checkbox" /></th>
                                            <th rowspan="2"><%=GetLabel("Item")%></th>
                                            <th class="thRight" style="width:40px;" rowspan="2"><%=GetLabel("Sisa")%></th>
                                            <th class="thCenter" style="width:70px;" rowspan="2"><%=GetLabel("Diterima")%></th>
                                            <th class="thCenter" style="width:90px;" rowspan="2"><%=GetLabel("Harga")%></th>
                                            <th class="thCenter" style="width:70px;" rowspan="2"><%=GetLabel("Satuan")%></th>
                                            <th class="thCenter" colspan="2"><%=GetLabel("Diskon 1")%></th>
                                            <th class="thCenter" colspan="2"><%=GetLabel("Diskon 2")%></th>
                                            <th class="thCenter" style="width:150px;" rowspan="2"><%=GetLabel("Konversi")%></th>
                                            <th class="thCenter" style="width:80px;" rowspan="2"><%=GetLabel("No Serial")%></th>
                                            <th class="thCenter" style="width:90px;" rowspan="2"><%=GetLabel("No Batch")%></th>
                                            <th class="thCenter" style="width:120px;" rowspan="2"><%=GetLabel("Expired")%></th>
                                        </tr>
                                        <tr>
                                            <th class="thCenter" style="width:50px;"><%=GetLabel("%")%></th>
                                            <th class="thCenter" style="width:80px;"><%=GetLabel("Nilai")%></th>
                                            <th class="thCenter" style="width:50px;"><%=GetLabel("%")%></th>
                                            <th class="thCenter" style="width:80px;"><%=GetLabel("Nilai")%></th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder" ></tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                            <input type="hidden" class="keyField" id="keyField" runat="server" value='<%# Eval("ID")%>' />
                                            <input type="hidden" id="hdnPOHdID" runat="server" value='<%# Eval("PurchaseOrderID")%>' />
                                            <input type="hidden" id="hdnItemID" class="hdnItemID" runat="server" value='<%# Eval("ItemID")%>' />
                                            <input type="hidden" id="hdnGCPurchaseUnit" class="hdnGCPurchaseUnit" runat="server" value='<%# Eval("GCPurchaseUnit")%>' />
                                            <input type="hidden" id="hdnConversionFactor" class="hdnConversionFactor" runat="server" value='<%# Eval("ConversionFactor")%>' />
                                            <input type="hidden" id="hdnBaseUnit" class="hdnBaseUnit" runat="server" value='<%# Eval("BaseUnit")%>' />
                                        </td>
                                        <td><%# Eval("ItemName1")%></td>
                                        <td align="right"><%# Eval("CustomQtyRemaining")%></td>
                                        <td align="center"><asp:TextBox ID="txtReceivedItem" ReadOnly="true" Width="50%" value ="0" runat="server" CssClass="number txtReceivedItem"/> </td>
                                        <td align="center"><asp:TextBox ID="txtUnitPrice" ReadOnly="true" Width="100%" runat="server" CssClass="txtCurrency txtUnitPrice"/> </td>
                                        <td align="center"><label runat="server" id="lblPurchaseUnit" class="lblPurchaseUnit"></label></td>
                                        <td align="center"><asp:TextBox ID="txtDiscountPercentage1" ReadOnly="true" Width="100%" runat="server" Text="0" CssClass="number txtDiscountPercentage1"/> </td>
                                        <td align="center"><asp:TextBox ID="txtDiscount1" ReadOnly="true" Width="100%" runat="server" Text="0" CssClass="txtCurrency txtDiscount1"/> </td>
                                        <td align="center"><asp:TextBox ID="txtDiscountPercentage2" ReadOnly="true" Width="100%" runat="server" Text="0" CssClass="number txtDiscountPercentage2"/> </td>
                                        <td align="center"><asp:TextBox ID="txtDiscount2" ReadOnly="true" Width="100%" runat="server" Text="0" CssClass="txtCurrency txtDiscount2"/> </td>
                                        <td align="center"><label runat="server" id="lblConversion" class="lblConversion"><%#Eval("CustomConversion")%></label></td>
                                        <td align="center"><asp:TextBox ID="txtSerialNo" ReadOnly="true" Width="50%" value ="0" runat="server" CssClass="number txtSerialNo"/> </td>
                                        <td align="center"><asp:TextBox ID="txtBatchNo" ReadOnly="true" Width="50%" value ="0" runat="server" CssClass="txtBatchNo"/> </td>
                                        <td align="center"><asp:TextBox ID="txtExpired" ReadOnly="true" Width="50%" value ="0" runat="server" CssClass="txtExpired datepicker"/> </td>
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
        </td>
    </tr>
</table>
