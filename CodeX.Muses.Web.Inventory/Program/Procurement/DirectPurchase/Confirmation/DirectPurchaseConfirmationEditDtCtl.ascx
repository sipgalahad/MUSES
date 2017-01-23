<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchaseConfirmationEditDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseConfirmationEditDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#containerPopup .txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });
    setDatePicker('<%=txtReferenceDate.ClientID %>');

    $(function () {
        setBtnSavePopupEnabled(false);
    });
    $('#<%=txtTotalNetTransactionAmount.ClientID %>').change(function () {
        setBtnSavePopupEnabled(true);
    });

    var isInit = true;
    //#region Inside Grid
    $('.txtUnitPrice').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr').parent().closest('tr');
        $tr.find('.txtDiscountPercentage').change();
    });

    $('.txtDiscountPercentage').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var subTotal = qty * unitPrice;

        var discountPercentage = parseFloat($tr.find('.txtDiscountPercentage').attr('hiddenVal'));
        var discountAmount = subTotal * discountPercentage / 100;
        $tr.find('.txtDiscountAmount').val(discountAmount).trigger('changeValue');

        calculateSubTotal($tr);
    });

    $('.txtDiscountAmount').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var subTotal = qty * unitPrice;

        var discountAmount = parseFloat($tr.find('.txtDiscountAmount').attr('hiddenVal'));
        var discountPercentage = discountAmount * 100 / subTotal;
        $tr.find('.txtDiscountPercentage').val(discountPercentage).trigger('changeValue');

        calculateSubTotal($tr);
    });

    var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');
    function calculateSubTotal($tr) {
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));
        var discountAmount = parseFloat($tr.find('.txtDiscountAmount').attr('hiddenVal'));

        var subTotal = (qty * unitPrice) - discountAmount;
        if ($('#<%=hdnIsLineAmountRounded.ClientID %>').val() == '1') {
            var format = parseFloat($('#<%=hdnLineAmountRoundedFormat.ClientID %>').val());
            subTotal = Math.ceil(subTotal / format) * format;
        }
        $tr.find('.txtLineAmount').val(subTotal).trigger('changeValue');

        var total = 0;
        $('.txtLineAmount').each(function () {
            total += parseFloat($(this).attr('hiddenVal'));
        });
        $('#<%=txtTransactionAmount.ClientID %>').val(total).trigger('changeValue');
        $('#<%=txtFinalDiscountPercentage.ClientID %>').change();
    }
    //#endregion

    setDatePicker('<%=txtDirectPurchaseDate.ClientID %>');

    $('#<%=chkPPN.ClientID %>').change(function () {
        calculateTotal();
    });

    $('#<%=txtFinalDiscountPercentage.ClientID %>').change(function () {
        var transactionAmount = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
        var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
        var totalHarga = transactionAmount + PPN;
        var discountPercentage = parseFloat($(this).val());
        var discountAmount = totalHarga * discountPercentage / 100;
        $('#<%=txtFinalDiscountAmount.ClientID %>').val(discountAmount).trigger('changeValue');
        calculateTotal();
    });

    $('#<%=txtFinalDiscountAmount.ClientID %>').change(function () {
        $(this).blur();
        calculateTotal();
    });

    calculateTotal();

    function calculateTotal() {
        var totalKotor = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
        if ($('#<%=chkPPN.ClientID %>').is(':checked')) {
            var temp = parseFloat($('#<%=txtTransactionAmount.ClientID %>').attr('hiddenVal'));
            var PPN = VATPercentage / 100 * parseFloat(temp);
            $('#<%=txtPPN.ClientID %>').val(PPN).trigger('changeValue');
        }
        else
            $('#<%=txtPPN.ClientID %>').val('0').trigger('changeValue');
        var PPN = parseFloat($('#<%=txtPPN.ClientID %>').attr('hiddenVal'));
        var totalHarga = totalKotor + PPN;
        var discountAmount = parseFloat($('#<%=txtFinalDiscountAmount.ClientID %>').attr('hiddenVal'));
        if (totalHarga == 0)
            $('#<%=txtFinalDiscountPercentage.ClientID %>').val(0);
        else {
            var discountPercentage = discountAmount * 100 / totalHarga;
            $('#<%=txtFinalDiscountPercentage.ClientID %>').val(discountPercentage);
        }
        totalHarga = totalHarga - discountAmount;
        if ($('#<%=hdnIsTotalAmountRounded.ClientID %>').val() == '1') {
            var format = parseFloat($('#<%=hdnTotalAmountRoundedFormat.ClientID %>').val());
            totalHarga = Math.ceil(totalHarga / format) * format;
        }
        if (isInit)
            isInit = false;
        else {
            setBtnSavePopupEnabled(true);
            $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
        }
    }

    function onBeforeSaveRecordPopup(errMessage) {
        var result = '';
        var lstID = '';
        $('.grdPurchaseReceive > tbody > tr:gt(1)').each(function () {
            var id = parseFloat($(this).find('.hdnID').val());
            var unitPrice = parseFloat($(this).find('.txtUnitPrice').attr('hiddenVal'));
            var discountPercentage = parseFloat($(this).find('.txtDiscountPercentage').attr('hiddenVal'));
            var discountAmount = parseFloat($(this).find('.txtDiscountAmount').attr('hiddenVal'));
            var lineAmount = parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            if (result != '') {
                result += "|";
                lstID += ',';
            }
            result += id + ';' + unitPrice + ';' + discountPercentage + ';' + discountAmount + ';' + lineAmount;
            lstID += id;
        });

        $('#<%=hdnLstID.ClientID %>').val(lstID);
        $('#<%=hdnSaveValue.ClientID %>').val(result);
        return true;
    }
</script>
<input type="hidden" id="hdnDirectPurchaseID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />
<input type="hidden" id="hdnSaveValue" runat="server" />
<input type="hidden" id="hdnLstID" runat="server" />
<input type="hidden" id="hdnIsRevision" runat="server" />
<input type="hidden" id="hdnIsLineAmountRounded" value="" runat="server" />
<input type="hidden" id="hdnLineAmountRoundedFormat" value="" runat="server" />
<input type="hidden" id="hdnIsTotalAmountRounded" value="" runat="server" />
<input type="hidden" id="hdnTotalAmountRoundedFormat" value="" runat="server" />

<div style="max-height: 500px; overflow-y: auto" id="containerPopup">
    <table style="width:100%">
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:150px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Pembelian")%></label></td>
                        <td><asp:TextBox ID="txtDirectPurchaseNo" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal Pembelian") %></td>
                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtDirectPurchaseDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Supplier")%></label></td>
                        <td><asp:TextBox ID="txtSupplier" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr> 
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dari Bagian")%></label></td>
                        <td><asp:TextBox ID="txtServiceUnitName" ReadOnly="true" Width="250px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Ke Bagian")%></label></td>
                        <td><asp:TextBox ID="txtToServiceUnitName" ReadOnly="true" Width="250px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Lokasi")%></label></td>
                        <td><asp:TextBox ID="txtLocationName" ReadOnly="true" Width="250px" runat="server" /></td>
                    </tr>   
                </table>
            </td>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 150px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Pembelian")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboDirectPurchaseType" ClientInstanceName="cboDirectPurchaseType" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Referensi")%></label></td>
                        <td><asp:TextBox ID="txtReferenceNo" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Referensi")%></label></td>
                        <td><asp:TextBox ID="txtReferenceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>     
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div style="position: relative;">
                    <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                        ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                        <ClientSideEvents EndCallback="function(s,e){onCbpPopupViewEndCallback()}" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Jumlah")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Pembeli")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="16">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="grdPurchaseReceive grdBorder grdView notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Jumlah")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Pembeli")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" ></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ItemCode")%></td>
                                                <td><%# Eval("ItemName1")%></td>
                                                <td align="center">
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:50px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td><asp:TextBox ID="txtUnitPrice" runat="server" Width="100%" CssClass="txtCurrency txtUnitPrice"/></td>
                                                            <td>&nbsp/&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td>
                                                    <input type="hidden" class="hdnID" value='<%# Eval("ID")%>' />
                                                    <input type="hidden" class="hdnQuantity" value='<%# Eval("Quantity")%>' />
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right"><%# Eval("Quantity")%></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="center"><asp:TextBox ID="txtDiscountPercentage" runat="server" Width="100%" CssClass="txtCurrency txtDiscountPercentage"/></td>
                                                <td align="center"><asp:TextBox ID="txtDiscountAmount" runat="server" Width="100%" CssClass="txtCurrency txtDiscountAmount"/></td>
                                                <td align="center"><asp:TextBox ID="txtLineAmount" runat="server" Width="100%" CssClass="txtCurrency txtLineAmount"/></td>
                                                <td><%# Eval("CreatedByUsername")%></td>
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
                </div>
                <table style="width: 100%;">
                    <colgroup>
                        <col style="width: 50%" />
                        <col style="width: 40px" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <h4><%=GetLabel("Informasi Pembelian Tunai") %></h4>
                            <table style="width: 100%;">
                                <colgroup>
                                    <col style="width: 180px" />
                                    <col style="width: 50px" />
                                    <col style="width: 10px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Pembelian Tunai")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%> (<%=GetVATPercentageLabel()%>%)</label></td>
                                    <td>&nbsp;</td>
                                    <td align="right"><asp:CheckBox ID="chkPPN" runat="server" /></td>
                                    <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server"/></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                    <td><asp:TextBox ID="txtFinalDiscountPercentage" CssClass="number" Width="50px" runat="server" /></td>
                                    <td>[%]</td>
                                    <td><asp:TextBox ID="txtFinalDiscountAmount" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Pembelian Tunai")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTotalNetTransactionAmount" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>