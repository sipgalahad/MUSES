<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPSupplierPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="APInvoiceSupplierPayment.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierPayment" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhEntry" runat="server">   
    <script type="text/javascript">
        var total = 0;
        function onLoad() {
            setDatePicker('<%=txtPaymentDate.ClientID %>');
            setDatePicker('<%=txtReferenceDate.ClientID %>');

            $('.chkIsSelected input').die('change');
            $('.chkIsSelected input').live('change', function () {
                var isChecked = $(this).is(":checked");
                $txt = $(this).closest('tr').find('.txtPembayaran');
                if (isChecked) {
                    $txt.removeAttr('readonly');
                }
                else {
                    $txt.attr('readonly', 'readonly');
                }
            });

            if ($('#<%=hdnIsAdd.ClientID %>').val() == "1") {
                $('#<%=panel1.ClientID %>').show();
                $('#<%=panel2.ClientID %>').hide();
            }
            else {
                $('#<%=panel1.ClientID %>').hide();
                $('#<%=panel2.ClientID %>').show();
            }

            $('.txtPembayaran').each(function () {
                $(this).trigger('changeValue');
            });

            //#region Supplier Payment No
            $('#lblSupplierPaymentNo.lblLink').click(function () {
                openSearchDialog('supplierpaymenthd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtPaymentNo.ClientID %>').val(value);
                    onTxtSupplierPaymentNoChanged(value);
                });
            });

            $('#<%=txtPaymentNo.ClientID %>').change(function () {
                onTxtSupplierPaymentNoChanged($(this).val());
            });

            function onTxtSupplierPaymentNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion
        }

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            $('.txtTotalToBeVerified').val(0).trigger('changeValue');
            var param = s.cpResult.split('|');
        }

        function onCboPaymentMethodValueChanged(evt) {
            var value = cboPaymentMethod.GetValue();
            if (value == '<%=GetSupplierPaymentMethodTransfer() %>' || value == '<%=GetSupplierPaymentMethodGiro() %>' || value == 'GetSupplierPaymentMethodCheque()') {
                $('#<%=trBank.ClientID %>').removeAttr('style');
                $('#<%=trBankRef.ClientID %>').removeAttr('style');
            }
            else {
                $('#<%=trBank.ClientID %>').attr('style', 'display:none');
                $('#<%=trBankRef.ClientID %>').attr('style', 'display:none');
            }
        }

        $('#chkSelectAllInvoice').die('change');
        $('#chkSelectAllInvoice').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        function getCheckedPurchaseInvoice() {
            var lstSelectedPurchaseInvoice = '';
            var lstSelectedPayment = '';
            var result = '';
            $('.chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').val();
                    var payment = parseFloat($tr.find('.txtPembayaran').attr('hiddenVal'));
                    if (lstSelectedPurchaseInvoice != '') {
                        lstSelectedPurchaseInvoice += ',';
                        lstSelectedPayment += ',';
                    }
                    lstSelectedPurchaseInvoice += key;
                    lstSelectedPayment += payment;
                }
            });
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedPurchaseInvoice);
            $('#<%=hdnSelectedPayment.ClientID %>').val(lstSelectedPayment);
        }

        function onBeforeSaveRecord(errMessage) {
            getCheckedPurchaseInvoice();
            if ($('#<%=hdnSelectedMember.ClientID %>').val() != '') {
                return true;
            }
            else {
                showToast('Process Failed', 'Please Select Purchase Invoice First');
                return false;
            }
        }

        $('.lblPurchaseInvoiceNo').die('click');
        $('.lblPurchaseInvoiceNo').live('click', function () {
            $tr = $(this).closest('tr');
            var id = $tr.find('.keyField').val();

            var url = ResolveUrl("~/Program/APInvoiceSupplier/APInvoiceSupplierVerification/APInvoiceSupplierVerificationDtCtl.ascx");
            openUserControlPopup(url, id, 'Detail Information', 1100, 400);
        });

        $('.lblgrdPurchaseInvoiceNo').die('click');
        $('.lblgrdPurchaseInvoiceNo').live('click', function () {
            $row = $(this).closest('tr');
            var id = $row.find('.keyField').html();
            var url = ResolveUrl("~/Program/APInvoiceSupplier/APInvoiceSupplierVerification/APInvoiceSupplierVerificationDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil No Tukar Faktur', 1100, 400);
        });
    </script> 
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnSelectedPayment" runat="server" value="" />
    <input type="hidden" id="hdnTransactionHdID" runat="server" value="" /> 
    <input type="hidden" value="" id="hdnSupplierPaymentID" runat="server"/> 
    <input type="hidden" value="" id="hdnIsAdd" runat="server"/> 

    <div style="height:435px;overflow-y:auto;">
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
                            <td class="tdLabel"><label id="lblSupplierPaymentNo" class="lblLink"><%=GetLabel("No. Pembayaran")%></label></td>
                            <td><asp:TextBox ID="txtPaymentNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtPaymentDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Keterangan") %></td>
                            <td style="padding-right: 1px;"><asp:TextBox ID="txtRemarks" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Cara Pembayaran")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboPaymentMethod" ClientInstanceName="cboPaymentMethod" Width="250px"
                                    runat="server">
                                     <ClientSideEvents ValueChanged="function(s,e) { onCboPaymentMethodValueChanged(e); }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr id="trBank" runat="server" style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bank")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboBank" ClientInstanceName="cboBank" Width="250px" runat="server"/></td>
                        </tr>
                        <tr id="trBankRef" runat="server" style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No. Cek/Giro") %></label></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtBankReferenceNo" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Uang")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboCurrency" ClientInstanceName="cboCurrency" Width="250px" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><%=GetLabel("Nilai Kurs (Rp)") %></td>
                            <td><asp:TextBox ID="txtKurs" Width="150px" CssClass="number" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Reference No")%></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="150px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Reference Date") %></td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtReferenceDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="position:relative;" id="divView">
                        <dxcp:ASPxCallbackPanel ID="cbpProcessDetail" runat="server" Width="100%" ClientInstanceName="cbpProcessDetail"
                            ShowLoadingPanel="false">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ hideLoadingPanel(); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="panel1" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                        <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                            <EmptyDataTemplate>
                                                <table id="tblView" runat="server" class="tblTransactionEntryResult" cellspacing="0" rules="all" >
                                                    <tr>
                                                        <th style="width:40px" class="thCenter" id="thSelectAll"><input id="chkSelectAllInvoice" type="checkbox" /></th>
                                                        <th><%=GetLabel("No. Tukar Faktur")%></th>
                                                        <th class="thCenter" style="width:150px"><%=GetLabel("Tgl. Jatuh Tempo")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Total Hutang")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Terbayar")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Sisa Hutang")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Pembayaran")%></th>
                                                    </tr>
                                                    <tr class="trEmpty">
                                                        <td colspan="7">
                                                            <%=GetLabel("No Data To Display")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table id="tblView" runat="server" class="tblTransactionEntryResult" cellspacing="0" rules="all" >
                                                    <tr>
                                                        <th style="width:40px" class="thCenter" id="thSelectAll"><input id="chkSelectAllInvoice" type="checkbox" /></th>
                                                        <th><%=GetLabel("No. Tukar Faktur")%></th>
                                                        <th class="thCenter" style="width:150px"><%=GetLabel("Tgl. Jatuh Tempo")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Total Hutang")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Terbayar")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Sisa Hutang")%></th>
                                                        <th class="thRight" style="width:150px"><%=GetLabel("Pembayaran")%></th>
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder" ></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center">
                                                        <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected"/>
                                                        <input type="hidden" class="keyField" id="keyField" runat="server" value='<%# Eval("PurchaseInvoiceID")%>' />
                                                    </td>
                                                    <td><label class="lblLink lblPurchaseInvoiceNo"><%# Eval("PurchaseInvoiceNo") %></label></td>
                                                    <td align="center"><%# Eval("DueDateInString")%></td>
                                                    <td align="right"><%# Eval("TotalNetTransactionAmount", "{0:N}")%></td>
                                                    <td align="right"><%# Eval("PaymentAmount", "{0:N}")%></td>
                                                    <td align="right"><%# Eval("CustomSisaHutang", "{0:N}")%></td>

                                                    <td align="center"><asp:TextBox ID="txtPembayaran" Width="80%" runat="server" ReadOnly="true" CssClass="txtPembayaran txtCurrency"/></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panel2" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false"
                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="PurchaseInvoiceID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:TemplateField HeaderText="No. Tukar Faktur" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <label class="lblLink lblgrdPurchaseInvoiceNo"><%# Eval("PurchaseInvoiceNo") %></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DueDateInString" HeaderText="Tgl. Jatuh Tempo" HeaderStyle-Width="140px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="TotalNetTransactionAmount" HeaderText="Total Hutang" HeaderStyle-Width="180px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                <asp:BoundField DataField="PaymentAmount" HeaderText="Jumlah Pembayaran" HeaderStyle-Width="180px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <%=GetLabel("No Data To Display")%>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                        <div class="imgLoadingGrdView" id="Div1">
                            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
