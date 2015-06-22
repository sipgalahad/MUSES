<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APInvoiceSupplierProcessEditCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierProcessEditCtl" %>

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
    setDatePicker('<%=txtDateReferrence.ClientID %>');

    //#region Inside Grid
    $('.txtUnitPrice').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr').parent().closest('tr');
        calculateSubTotal($tr);
    });

    $('.txtDiscountPercentage1').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var subTotal = qty * unitPrice;

        var discountPercentage1 = parseFloat($tr.find('.txtDiscountPercentage1').attr('hiddenVal'));
        var discountAmount1 = subTotal * discountPercentage1 / 100;
        $tr.find('.txtDiscountAmount1').val(discountAmount1).trigger('changeValue');

        calculateSubTotal($tr);
    });

    $('.txtDiscountAmount1').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));

        var subTotal = qty * unitPrice;

        var discountAmount1 = parseFloat($tr.find('.txtDiscountAmount1').attr('hiddenVal'));
        var discountPercentage1 = discountAmount1 * 100 / subTotal;
        $tr.find('.txtDiscountPercentage1').val(discountPercentage1).trigger('changeValue');

        calculateSubTotal($tr);
    });

    $('.txtDiscountPercentage2').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));
        var discountAmount1 = parseFloat($tr.find('.txtDiscountAmount1').attr('hiddenVal'));

        var subTotal = (qty * unitPrice) - discountAmount1;

        var discountPercentage2 = parseFloat($tr.find('.txtDiscountPercentage2').attr('hiddenVal'));
        var discountAmount2 = subTotal * discountPercentage2 / 100;
        $tr.find('.txtDiscountAmount2').val(discountAmount2).trigger('changeValue');

        calculateSubTotal($tr);
    });

    $('.txtDiscountAmount2').change(function () {
        $(this).trigger('changeValue');
        $tr = $(this).closest('tr');
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));
        var discountAmount1 = parseFloat($tr.find('.txtDiscountAmount1').attr('hiddenVal'));

        var subTotal = (qty * unitPrice) - discountAmount1;

        var discountAmount2 = parseFloat($tr.find('.txtDiscountAmount2').attr('hiddenVal'));
        var discountPercentage2 = discountAmount2 * 100 / subTotal;
        $tr.find('.txtDiscountPercentage2').val(discountPercentage2).trigger('changeValue');

        calculateSubTotal($tr);
    });

    var VATPercentage = parseInt('<%=GetVATPercentageLabel() %>');
    function calculateSubTotal($tr) {
        var qty = parseFloat($tr.find('.hdnQuantity').val());
        var unitPrice = parseFloat($tr.find('.txtUnitPrice').attr('hiddenVal'));
        var discountAmount1 = parseFloat($tr.find('.txtDiscountAmount1').attr('hiddenVal'));
        var discountAmount2 = parseFloat($tr.find('.txtDiscountAmount2').attr('hiddenVal'));

        var subTotal = (qty * unitPrice) - discountAmount1 - discountAmount2;
        $tr.find('.txtLineAmount').val(subTotal).trigger('changeValue');

        var total = 0;
        $('.txtLineAmount').each(function () {
            total += parseFloat($(this).attr('hiddenVal'));
        });
        $('#<%=txtTransactionAmount.ClientID %>').val(total).trigger('changeValue');
        $('#<%=txtFinalDiscountPercentage.ClientID %>').change();


        var qty = parseFloat($tr.find('.hdnReturnQuantity').val());
        var discountAmount1 = parseFloat($tr.find('.txtDiscountPercentage1').val()) * (qty * unitPrice) / 100;
        var discountAmount2 = parseFloat($tr.find('.txtDiscountPercentage2').val()) * ((qty * unitPrice) - discountAmount1) / 100;
        var subTotal = (qty * unitPrice) - discountAmount1 - discountAmount2;
        $tr.find('.txtReturnLineAmount').val(subTotal).trigger('changeValue');
        var total = 0;
        $('.txtReturnLineAmount').each(function () {
            total += parseFloat($(this).attr('hiddenVal'));
        });
        $('#<%=txtReturnTransactionAmount.ClientID %>').val(total).trigger('changeValue');
        calculateReturnTotal(false);
    }
    //#endregion

    setDatePicker('<%=txtPurchaseReceiveDate.ClientID %>');

    $('#<%=chkPPN.ClientID %>').change(function () {
        $('#<%=chkReturnPPN.ClientID %>').prop('checked', $(this).is(':checked'));
        calculateTotal();
        calculateReturnTotal(false);
    });

    $('#<%=txtDP.ClientID %>').change(function () {
        $(this).trigger('changeValue');
        calculateTotal();
    });

    $('#<%=txtCharges.ClientID %>').change(function () {
        $(this).trigger('changeValue');
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
    calculateReturnTotal(true);

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
        var DP = parseFloat($('#<%=txtDP.ClientID %>').attr('hiddenVal'));
        var Charge = parseFloat($('#<%=txtCharges.ClientID %>').attr('hiddenVal'));
        totalHarga = totalHarga - discountAmount - DP + Charge;
        $('#<%=txtTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
    }

    function calculateReturnTotal(isLoad) {
        var totalKotor = parseFloat($('#<%=txtReturnTransactionAmount.ClientID %>').attr('hiddenVal'));
        if ($('#<%=chkReturnPPN.ClientID %>').is(':checked')) {
            var temp = parseFloat($('#<%=txtReturnTransactionAmount.ClientID %>').attr('hiddenVal'));
            var PPN = VATPercentage / 100 * parseFloat(temp);
            $('#<%=txtReturnPPN.ClientID %>').val(PPN).trigger('changeValue');
        }
        else
            $('#<%=txtReturnPPN.ClientID %>').val('0').trigger('changeValue');
        var PPN = parseFloat($('#<%=txtReturnPPN.ClientID %>').attr('hiddenVal'));
        var totalHarga = totalKotor + PPN;
        $('#<%=txtReturnTotalNetTransactionAmount.ClientID %>').val(totalHarga).trigger('changeValue');
        if (!isLoad)
            $('#<%=txtCNAmount.ClientID %>').val(totalHarga).trigger('changeValue');
    }

    function onBeforeSaveRecord(errMessage) {
        var result = '';
        var lstID = '';
        $('.grdPurchaseReceive > tbody > tr:gt(1)').each(function () {
            var id = parseFloat($(this).find('.hdnID').val());
            var unitPrice = parseFloat($(this).find('.txtUnitPrice').attr('hiddenVal'));
            var discountPercentage1 = parseFloat($(this).find('.txtDiscountPercentage1').attr('hiddenVal'));
            var discountAmount1 = parseFloat($(this).find('.txtDiscountAmount1').attr('hiddenVal'));
            var discountPercentage2 = parseFloat($(this).find('.txtDiscountPercentage2').attr('hiddenVal'));
            var discountAmount2 = parseFloat($(this).find('.txtDiscountAmount2').attr('hiddenVal'));
            var lineAmount = parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            var returnID = parseFloat($(this).find('.hdnPurchaseReturnDtID').val());
            var returnLineAmount = parseFloat($(this).find('.txtReturnLineAmount').attr('hiddenVal'));
            if (result != '') {
                result += "|";
                lstID += ',';
            }
            result += id + ';' + unitPrice + ';' + discountPercentage1 + ';' + discountAmount1 + ';' + discountPercentage2 + ';' + discountAmount2 + ';' + lineAmount + ';' + returnID + ';' + returnLineAmount;
            lstID += id;
        });

        $('#<%=hdnLstID.ClientID %>').val(lstID);
        $('#<%=hdnSaveValue.ClientID %>').val(result);
        return true;
    }
</script>
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnPurchaseReturnID" runat="server" />
<input type="hidden" id="hdnGCPurchaseReturnType" runat="server" />
<input type="hidden" id="hdnCreditNoteID" runat="server" />
<input type="hidden" id="hdnItemID" runat="server" />
<input type="hidden" id="hdnLocationID" runat="server" />
<input type="hidden" id="hdnDateFrom" runat="server" />
<input type="hidden" id="hdnDateTo" runat="server" />
<input type="hidden" id="hdnPurchaseReceiveID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />

<input type="hidden" id="hdnLstID" runat="server" />
<input type="hidden" id="hdnSaveValue" runat="server" />

<div style="max-height: 440px; overflow-y: auto" id="containerPopup">
    <table style="width:100%">
        <colgroup>
            <col style="width:450px"/>
            <col style="width:400px"/>
        </colgroup>
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:200px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No BPB")%></label></td>
                        <td><asp:TextBox ID="txtPurchaseReceiveNo" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu Penerimaan") %></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtPurchaseReceiveDate" ReadOnly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    <td style="width: 5px">&nbsp;</td>
                                    <td><asp:TextBox ID="txtPurchaseReceiveTime" Width="60px" ReadOnly="true" CssClass="time" runat="server" Style="text-align: center"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No.Faktur/Kirim")%></label></td>
                        <td><asp:TextBox ID="txtReferenceNo" CssClass="required" ValidationGroup="mpEntry" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal di Faktur") %></td>
                        <td><asp:TextBox ID="txtDateReferrence" Width="120px" CssClass="datepicker" runat="server" /></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboTerm" ClientInstanceName="cboTerm" Width="200px" runat="server" /></td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                        <td><dxe:ASPxComboBox Visible="false" ID="cboCurrency" ClientInstanceName="cboCurrency" Width="100%" runat="server" /></td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                        <td><asp:TextBox ID="txtKurs" Width="120px" runat="server" /></td>
                    </tr>
                    <tr id="trPurchaseReturnNo" runat="server">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Retur")%></label></td>
                        <td><asp:TextBox ID="txtPurchaseReturnNo" ReadOnly="true" Width="200px" runat="server" /></td>
                    </tr>  
                    <tr id="trPurchaseReturnDate" runat="server">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tgl Retur")%></label></td>
                        <td><asp:TextBox ID="txtPurchaseReturnDate" ReadOnly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>  
                    <tr id="trPurchaseReturnType" runat="server">
                        <td class="tdLabel"><%=GetLabel("Tipe Retur") %></td>
                        <td><asp:TextBox ID="txtPurchaseReturnType" Width="200px" ReadOnly="true" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding: 5px; vertical-align: top">
                <table style="width: 100%;">
                    <colgroup>
                        <col style="width: 70px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
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
                                                    <th rowspan="2" style="width:30px" align="center"><%=GetLabel("Bonus")%></th>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diterima")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diretur")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 1")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 2")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total (Sebelum Retur)")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total Retur")%></th>   
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Penerima")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>

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
                                                    <th rowspan="2" style="width:30px" align="center"><%=GetLabel("Bonus")%></th>
                                                    <th rowspan="2" style="width:60px"><%=GetLabel("Kode Item")%></th>
                                                    <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Harga Satuan")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diterima")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Diretur")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 1")%></th>
                                                    <th colspan="2" class="thCenter"><%=GetLabel("DISKON 2")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total (Sebelum Retur)")%></th>
                                                    <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Total Retur")%></th>   
                                                    <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Penerima")%></th>                                                
                                                </tr>
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>

                                                    <th style="width:40px" class="thCenter"><%=GetLabel("[%]")%></th>
                                                    <th style="width:70px" class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" ></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center"><asp:CheckBox ID="chkIsBonus" Enabled="false" runat="server" Checked='false' /></td>
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
                                                <td>
                                                    <input type="hidden" class="hdnPurchaseReturnDtID" id="hdnPurchaseReturnDtID" runat="server" value='0' />
                                                    <input type="hidden" class="hdnReturnQuantity" id="hdnReturnQuantity" runat="server" value='0' />
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:40px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="lblReadOnlyText" align="right" id="tdReturnQuantity" runat="server">0</td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </td>
                                                <td align="center"><asp:TextBox ID="txtDiscountPercentage1" runat="server" Width="100%" CssClass="txtCurrency txtDiscountPercentage1"/></td>
                                                <td align="center"><asp:TextBox ID="txtDiscountAmount1" runat="server" Width="100%" CssClass="txtCurrency txtDiscountAmount1"/></td>
                                                <td align="center"><asp:TextBox ID="txtDiscountPercentage2" runat="server" Width="100%" CssClass="txtCurrency txtDiscountPercentage2"/></td>
                                                <td align="center"><asp:TextBox ID="txtDiscountAmount2" runat="server" Width="100%" CssClass="txtCurrency txtDiscountAmount2"/></td>
                                                <td align="center"><asp:TextBox ID="txtLineAmount" ReadOnly="true" runat="server" Width="100%" CssClass="txtCurrency txtLineAmount"/></td>
                                                <td align="center"><asp:TextBox ID="txtReturnLineAmount" ReadOnly="true" runat="server" Width="100%" CssClass="txtCurrency txtReturnLineAmount"/></td>
                                                <td><%# Eval("Username")%></td>
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
                            <div id="divPurchaseReturnFooter" runat="server">
                                <h4><%=GetLabel("Informasi Retur") %></h4>
                                <table style="width: 100%;">
                                    <colgroup>
                                        <col style="width: 180px" />
                                        <col style="width: 10px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Retur")%></label></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtReturnTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("PPN")%> (<%=GetVATPercentageLabel()%>%)</label></td>
                                        <td align="right"><asp:CheckBox ID="chkReturnPPN" Enabled="false" runat="server" /></td>
                                        <td><asp:TextBox ID="txtReturnPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server"/></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Nilai Retur")%></label></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtReturnTotalNetTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                    </tr>
                                    <tr id="trCreditNoteType" runat="server">
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Nota Kredit")%></label></td>
                                        <td>&nbsp;</td>
                                        <td><dxe:ASPxComboBox ID="cboCreditNoteType"  Width="180px" runat="server" /></td>
                                    </tr>
                                    <tr id="trCreditNoteAmount" runat="server">
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Nota Kredit")%></label></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtCNAmount" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <h4><%=GetLabel("Informasi Penerimaan Barang") %></h4>
                            <table style="width: 100%;">
                                <colgroup>
                                    <col style="width: 180px" />
                                    <col style="width: 50px" />
                                    <col style="width: 10px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Nilai Pembelian")%></label></td>
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
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Reff Uang Muka")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtDPReferrenceNo" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Uang Muka")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtDP" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pembiayaan")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><dxe:ASPxComboBox ID="cboChargesType" ClientInstanceName="cboChargesType" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Biaya")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtCharges" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Nilai Penerimaan")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTotalNetTransactionAmount" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>