<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPaymentViewCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Finance.Program.DirectPaymentViewCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('.txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });
    
    $('.lnkCardNumber').click(function () {
        $td = $(this).parent();
        cboCardType.SetValue($td.find('.hdnGCCardType').val());
        $('#<%=txtCardNumber4.ClientID %>').val($td.find('.hdnCardNumber4').val());
        $('#<%=txtHolderName.ClientID %>').val($td.find('.hdnCardHolderName').val());
        var cardValidThru = $td.find('.hdnCardValidThru').val().split('/');
        var expiredDateMonth = parseInt(cardValidThru[0]);
        var expiredDateYear = 2000 + parseInt(cardValidThru[1]);
        cboCardDateMonth.SetValue(expiredDateMonth);
        cboCardDateYear.SetValue(expiredDateYear);
        cboCardProvider.SetValue($td.find('.hdnGCCardProvider').val());

        pcCardInformation.Show();
    });
</script>
<input type="hidden" value="" id="hdnInvoiceID" runat="server" />
<div style="height:442px;overflow-y:auto;">
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Informasi Pembayaran") %></h4>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><div style="position: relative;"><label><%=GetLabel("No Pembayaran")%></label></div></td>
                        <td><asp:TextBox ID="txtPaymentNo" Width="150px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal / Jam")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-right: 1px;width:145px"><asp:TextBox ReadOnly="true" ID="txtPaymentDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    <td style="width:5px">&nbsp;</td>
                                    <td><asp:TextBox ID="txtPaymentTime" Width="80px" ReadOnly="true" CssClass="time" runat="server" Style="text-align:center" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Pembayaran")%></label></td>
                        <td><asp:TextBox ID="txtPaymentType" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Informasi Tagihan") %></h4>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Faktur")%></label></td>
                        <td><asp:TextBox ID="txtInvoiceNo" ReadOnly="true" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Tagihan")%></label></td>
                        <td><asp:TextBox ID="txtInvoiceTotal" ReadOnly="true" CssClass="txtCurrency" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Pembayaran")%></label></td>
                        <td><asp:TextBox ID="txtPayment" ReadOnly="true" CssClass="txtCurrency" Width="150px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h4 style="text-align:left"><%=GetLabel("Detil Pembayaran")%></h4>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="grdNormal grdBorder" id="tblPaymentDtEdit" style="width:100%;font-size:0.9em" cellpadding="0" cellspacing="0">
                    <tr>  
                        <th rowspan="2" align="left">
                            <div style="padding:3px;float:left;">
                                <div><%= GetLabel("Metode Pembayaran")%></div>
                            </div>
                        </th>
                        <th colspan="2" class="thCenter"><%=GetLabel("Kartu Kredit")%></th>
                        <th colspan="2" class="thCenter"><%=GetLabel("Informasi Bank")%></th>
                        <th colspan="3" class="thCenter"><%=GetLabel("Jumlah")%></th>
                    </tr>
                    <tr>
                        <th style="width:120px" class="thCenter"><%=GetLabel("EDC")%></th>
                        <th style="width:180px" class="thCenter"><%=GetLabel("Informasi Kartu")%></th>
                        <th style="width:150px" class="thCenter"><%=GetLabel("Bank")%></th>
                        <th style="width:150px" class="thCenter"><%=GetLabel("No Referensi")%></th>
                        <th style="width:150px">
                            <div style="text-align:right;padding-right:3px">
                                <%=GetLabel("Pembayaran")%>
                            </div>
                        </th>
                        <th style="width:150px">
                            <div style="text-align:right;padding-right:3px">
                                <%=GetLabel("Fee")%>
                            </div>
                        </th>
                        <th style="width:150px">
                            <div style="text-align:right;padding-right:3px">
                                <%=GetLabel("Line Total")%>
                            </div>
                        </th>
                    </tr>
                <asp:ListView ID="lvwPaymentDt" runat="server">
                    <LayoutTemplate>
                        <tr runat="server" id="itemPlaceholder" ></tr>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("PaymentMethod") %></td>
                            <td><%#Eval("EDCMachineName")%></td>
                            <td>
                                <a class="lnkCardNumber"><%#Eval("CardNumber")%></a>
                                <input type="hidden" class="hdnGCCardType" value="<%#Eval("GCCardType")%>" />
                                <input type="hidden" class="hdnGCCardProvider" value="<%#Eval("GCCardProvider")%>" />
                                <input type="hidden" class="hdnCardNumber4" value="<%#Eval("CardNumber4")%>" />
                                <input type="hidden" class="hdnCardHolderName" value="<%#Eval("CardHolderName")%>" />
                                <input type="hidden" class="hdnCardValidThru" value="<%#Eval("CardValidThru")%>" />
                            </td>
                            <td><%#Eval("BankName")%></td>
                            <td><%#Eval("ReferenceNo")%></td>
                            <td align="right"><%#Eval("PaymentAmount", "{0:N}")%></td>
                            <td align="right"><%#Eval("CardFeeAmount", "{0:N}")%></td>
                            <td align="right"><%#Eval("LineTotal", "{0:N}")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                    <tr class="trFooter">  
                        <td colspan="5">
                            <div style="text-align:right;padding:3px">
                                <%=GetLabel("Total")%>
                            </div>
                        </td>
                        <td>
                            <div style="text-align:right;padding:3px" id="tdTotalPatientEdit" runat="server">0</div>
                        </td>
                        <td>
                            <div style="text-align:right;padding:3px" id="tdTotalCardFeeEdit" runat="server">0</div>
                        </td>
                        <td>
                            <div style="text-align:right;padding:3px" id="tdLineTotalEdit" runat="server">0</div>
                        </td>
                    </tr>
                </table>
                <table style="width:100%" id="tblCashback">
                    <tr>
                        <td align="right" style="padding-right:5px"><%=GetLabel("Uang Kembalian") %></td>
                        <td style="width:150px"><asp:TextBox ID="txtCashReturnAmount" ReadOnly="true" runat="server" CssClass="txtCurrency min" Width="150px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>  

    <!-- Popup Entry Notes -->
    <dxpc:ASPxPopupControl ID="pcCardInformation" runat="server" ClientInstanceName="pcCardInformation" CloseAction="CloseButton"
        Height="180px" HeaderText="Informasi Kartu" Width="400px" Modal="True" PopupAction="None"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" ID="pccc1">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <fieldset id="fsCardInformation" style="margin:0"> 
                                <div style="text-align: left; width: 100%;">
                                    <table>
                                        <colgroup>
                                            <col style="width: 500px"/>
                                        </colgroup>
                                        <tr>
                                            <td valign="top">
                                                <table>
                                                    <colgroup>
                                                        <col style="width:150px"/>
                                                        <col style="width:200px"/>
                                                    </colgroup>
                                                    <tr>
                                                        <td><%=GetLabel("Tipe Kartu")%></td>
                                                        <td><dxe:ASPxComboBox ID="cboCardType" ClientEnabled="false" ClientInstanceName="cboCardType" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Bank Penerbit")%></td>
                                                        <td><dxe:ASPxComboBox ID="cboCardProvider" ClientEnabled="false" ClientInstanceName="cboCardProvider" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("No Kartu")%></td>
                                                        <td>
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><asp:TextBox ID="txtCardNumber1" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber2" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber3" ReadOnly="true" Enabled="false" Text="XXXX" Width="100%" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><asp:TextBox ID="txtCardNumber4" ReadOnly="true" Width="100%" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Pemegang Kartu")%></td>
                                                        <td><asp:TextBox ID="txtHolderName" ReadOnly="true" Width="100%" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><%=GetLabel("Masa Berlaku")%></td>
                                                        <td>
                                                            <table style="width:100%;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><dxe:ASPxComboBox ID="cboCardDateMonth" ClientEnabled="false" ClientInstanceName="cboCardDateMonth" Width="100px" runat="server" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><dxe:ASPxComboBox ID="cboCardDateYear" ClientEnabled="false" ClientInstanceName="cboCardDateYear" Width="80px" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                     
                                                </table>  
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
                                        <tr>
                                            <td>
                                                <input type="button" id="btnPaymentCardInformationClose" value='<%= GetLabel("Tutup")%>' onclick="pcCardInformation.Hide();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>                                
                            </fieldset>                                
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</div>
