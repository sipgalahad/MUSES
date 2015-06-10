<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APInvoiceSupplierVerificationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierVerificationDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            cbpEntryPopupView.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
        });
    });

    function onCbpEntryPopupViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpEntryPopupView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion
</script>
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnItemID" runat="server" />
<input type="hidden" id="hdnLocationID" runat="server" />
<input type="hidden" id="hdnDateFrom" runat="server" />
<input type="hidden" id="hdnDateTo" runat="server" />
<input type="hidden" id="hdnPurchaseInvoiceID" runat="server" />

<table class="tblContentArea">
    <tr>
        <td>
            <table class="tblEntryContent" style="width:70%">
                <colgroup>
                    <col style="width:160px"/>
                    <col/>
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Purchase Invoice No")%></label></td>
                    <td><asp:TextBox ID="txtPurchaseInvoiceNo" ReadOnly="true" Width="100%" runat="server" /></td>
                </tr>  
            </table>

            <div style="position: relative;">
                <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                    <ClientSideEvents EndCallback="function(s,e){onCbpPopupViewEndCallback()}" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:240px; overflow-y: scroll;">
                                <table class="tblTransactionEntryResult" cellspacing="0" width="100%" rules="all">
                                    <tr>
                                        <th align="left"><%=GetLabel("No Penerimaan") %></th>
                                        <th align="left" style="width:60px"><%=GetLabel("No Faktur") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Jumlah") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Diskon Item") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Diskon Final") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("PPN") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("PPH23") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("PPH25") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Materai") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Ongkir") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("DP") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Nota Kredit") %></th>
                                        <th class="thRight" style="width:75px"><%=GetLabel("Total") %></th>
                                    </tr>
                                    <asp:ListView runat="server" ID="lvwView">
                                        <EmptyDataTemplate>
                                            <tr class="trEmpty">
                                                <td colspan="14"><%=GetLabel("Data Tidak Tersedia") %></td>
                                            </tr>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="left"><%#Eval("PurchaseReceiveNo") %></td>
                                                <td align="left"><%#Eval("ReferenceNo")%></td>
                                                <td align="right"><%#Eval("TransactionAmount","{0:N}")%></td>
                                                <td align="right"><%#Eval("DiscountAmount","{0:N}")%></td>
                                                <td align="right"><%#Eval("FinalDiscountAmount","{0:N}")%></td>
                                                <td align="right"><%#Eval("VATAmount","{0:N}") %></td>
                                                <td align="right"><%#Eval("PPH23Amount","{0:N}") %></td>
                                                <td align="right"><%#Eval("PPH25Amount", "{0:N}")%></td>
                                                <td align="right"><%#Eval("StampAmount", "{0:N}")%></td>
                                                <td align="right"><%#Eval("ChargesAmount", "{0:N}")%></td>
                                                <td align="right"><%#Eval("DownPaymentAmount", "{0:N}")%></td>
                                                <td align="right"><%#Eval("CreditNoteAmount", "{0:N}")%></td>
                                                <td align="right"><%#Eval("LineAmount", "{0:N}")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>    
                <div class="containerPaging">
                    <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div> 
                <table style="width: 350px; float:right;">
                    <colgroup>
                        <col style="width: 200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"  style="text-align:right"><label class="lblNormal"><%=GetLabel("Total Penerimaan")%></label></td>
                        <td><asp:TextBox ID="txtTotalAmount" CssClass="txtCurrency" ReadOnly="true" Width="100%" runat="server" hiddenVal="0" /></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>                                                                    
                                    <td><asp:TextBox class= "txtFinalDiscountPIPctg txtCurrency" ReadOnly="true" ID="txtFinalDiscountPIPctg" Width="60px" runat="server" hiddenVal="0"/> %</td>
                                </tr>
                            </table>
                        </td>
                        <td><asp:TextBox ID="txtFinalDIscountPI" CssClass="txtCurrency" ReadOnly="true" Width="100%" runat="server" hiddenVal="0"/></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="text-align:right;"><asp:CheckBox ID="chkPPN" Enabled="false" runat="server" />&nbsp;<%=GetLabel("PPN")%></td>
                        <td><asp:TextBox ID="txtPPNPI" CssClass="txtCurrency" Width="100%" ReadOnly="true" runat="server" hiddenVal="0"/></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("PPH")%></label></td>
                                    <td><asp:TextBox class= "txtPPHPIPctg txtCurrency" ID="txtPPHPIPctg" ReadOnly="true" Width="60px" runat="server" hiddenVal="0"/> %</td>
                                </tr>
                            </table>
                        </td>
                        <td><asp:TextBox ID="txtPPHPI" CssClass="txtCurrency" Width="100%" ReadOnly="true" runat="server" hiddenVal="0"/></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Ongkos Kirim")%></label></td>
                        <td><asp:TextBox ID="txtChargesPI" CssClass="txtCurrency" Width="100%" ReadOnly="true" runat="server" hiddenVal="0"/></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="text-align:right;"><label class="lblNormal"><%=GetLabel("Materai")%></label></td>
                        <td><asp:TextBox ID="txtStampPI" CssClass="txtCurrency" Width="100%" ReadOnly="true" runat="server" hiddenVal="0"/></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="text-align:right"><label class="lblNormal"><%=GetLabel("Total Faktur")%></label></td>
                        <td><asp:TextBox ID="txtGrandTotalPI" CssClass="txtCurrency" ReadOnly="true" Width="100%" runat="server" hiddenVal="0" /></td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>