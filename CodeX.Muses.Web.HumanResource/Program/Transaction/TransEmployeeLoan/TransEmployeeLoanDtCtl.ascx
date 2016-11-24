<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransEmployeeLoanDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Information.Program.TransEmployeeLoanDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $(function () {
        $('#btnGenerate').click(function (evt) {
            //if (IsValid(evt, 'fsTrxPopup'))
            cbpProcessPopup.PerformCallback('generate');
        });

        $('#btnSave').click(function (evt) {
            //if (IsValid(evt, 'fsTrxPopup'))
            var indexPayment = 1;
            var isAllowSave = true;
            if (parseFloat($('#<%=txtTotalTransactionAmount.ClientID %>').attr('hiddenVal')) != parseFloat($('#<%=hdnTotalAmount.ClientID %>').val()))
                isAllowSave = false;
            if (isAllowSave) {
                var result = '';
                $('.trEntity').each(function () {
                    if (result != '')
                        result += '|';
                    var paymentIndex = $(this).find('.tdPaymentIndex').html();
                    var paymentDate = $(this).find('.txtPaymentDate').val();
                    var transactionAmount = $(this).find('.txtTransactionAmount').attr('hiddenVal');
                    result += paymentIndex + ';' + paymentDate + ';' + transactionAmount;
                    indexPayment += 1;
                });
                $('#<%=hdnSaveValue.ClientID %>').val(result);
                //alert(result)
                cbpProcessPopup.PerformCallback('save');
                cbpPopupView.PerformCallback('refresh');
            }
            else
                showToast('Warning', 'Total Pembayaran Tidak Sama');
        });
        setControlDateAmount();

        $('#divEntryDtAdd').click(function () {
            $newTr = $('#addEntityDt').html();
            $newTr = $($newTr);
            $newTr.insertBefore($('#trFooter'));
            setControlIndexPayment();
            setControlDateAmount();
        });

        $('.divDeleteEntryDt').live('click', function () {
            $tr = $(this).closest('tr');
            $tr.remove();
            setControlIndexPayment();
        });
    });

    $('.txtTransactionAmount').die('change');
    $('.txtTransactionAmount').live('change', function () {
        $(this).blur();
        calculateTotalAmountRepeater();
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'generate') {
            if (param[1] == 'fail')
                showToast('Generate Failed', 'Error Message : ' + param[2]);
            else 
                cbpPopupView.PerformCallback('refresh');
        }
        else if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
        }
    }

    function onCbpPopupViewEndCallback() {
        setControlDateAmount();
    }

    function setControlIndexPayment() {
        var index = 1;
        $('.tdPaymentIndex').each(function () {
            $(this).html(index);
            index += 1;
        });
    }

    function setControlDateAmount() {
        $('.txtPaymentDate').each(function () {
            setDatePickerElement($(this));
        });

        $('.txtTransactionAmount').each(function () {
            $(this).trigger('changeValue');
        });

        if ($('#tblLoadDt tr').length > 1) {
            $('#btnGenerate').hide();
            $('#btnSave').show();
        }
        else {
            $('#btnGenerate').show();
            $('#btnSave').hide();
        }

        calculateTotalAmountRepeater();
    }

    function calculateTotalAmountRepeater() {
        var tempTotalAmount = 0;
        $('.txtTransactionAmount').each(function () {
            tempTotalAmount += parseFloat($(this).attr('hiddenVal'));
        });
        $('#<%=txtTotalTransactionAmount.ClientID %>').val(tempTotalAmount).trigger('changeValue');
    }
</script>
<input type="hidden" id="hdnSaveValue" runat="server" />
<input type="hidden" id="hdnHdID" runat="server" />
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnPaymentIndex" runat="server" />
<input type="hidden" id="hdnTotalAmount" runat="server" />
<input type="hidden" id="hdnStartPaymentDate" runat="server" />
<input type="hidden" id="hdnStartEffectiveDate" runat="server" />
<script id="addEntityDt" type="text/x-jquery-tmpl">
     <tr class="trEntity">
        <td class="tdPaymentIndex" align="center"></td>
        <td align="center"><asp:TextBox id="txtPaymentDate" Width="120px" CssClass="txtPaymentDate datepicker" runat="server" /></td>
        <td align="center"><asp:TextBox id="txtTransactionAmount" CssClass="txtTransactionAmount txtCurrency" runat="server" /></td>
        <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
    </tr>
</script>


<div>
    <table class="tblContentArea">
        <tr>
            <td>
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Header")%></label></td>
                        <td><asp:TextBox ID="txtHeader" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Peminjaman")%></label></td>
                        <td><asp:TextBox ID="txtTotal" ReadOnly="true" CssClass="txtCurrency" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><input type="button" id="btnGenerate" class="btnWhite" value='<%=GetLabel("Generate") %>'/></td>
                    </tr>  
                </table>

                <div style="position: relative;">
                    <fieldset id="fsTrxPopup">
                        <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                            ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                            <ClientSideEvents EndCallback="function(s,e){ onCbpPopupViewEndCallback()}" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:330px; overflow-y: scroll;">
                                        <table id="tblLoadDt" class="grdSelected" rules="all" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <th class="keyField"></th>
                                                <th style="width:70px" class="thCenter"><%=GetLabel("No.")%></th>
                                                <th style="width:150px" class="thCenter"><%=GetLabel("Tanggal Pembayaran")%></th>
                                                <th class="thCenter"><%=GetLabel("Jumlah")%></th>
                                                <th style="width:80px" class="thCenter"></th>
                                            </tr>
                                            <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="trEntity">
                                                        <td class="tdPaymentIndex" align="center"><%#Eval("PaymentIndex")%></td>
                                                        <td align="center"><asp:TextBox id="txtPaymentDate" Width="120px" CssClass="txtPaymentDate datepicker" runat="server" /></td>
                                                        <td align="center"><asp:TextBox id="txtTransactionAmount" CssClass="txtTransactionAmount txtCurrency" runat="server" /></td>
                                                        <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="trFooter">
                                                <td colspan="3" align="center"><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Pembayaran")%></span></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="tdLabel"><%=GetLabel("Total") %></td>
                                                <td align="center"><asp:TextBox ID="txtTotalTransactionAmount" ReadOnly="true" CssClass="txtCurrency" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>    
                    </fieldset>
                    <input type="button" id="btnSave" style="float:right;" class="btnWhite" value='<%=GetLabel("Save") %>'/>
                    <div class="imgLoadingGrdView" id="containerImgLoadingView" >
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>