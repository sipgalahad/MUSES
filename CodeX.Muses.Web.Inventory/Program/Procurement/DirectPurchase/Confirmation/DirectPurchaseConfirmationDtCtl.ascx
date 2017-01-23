<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchaseConfirmationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Inventory.Program.DirectPurchaseConfirmationDtCtl" %>
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
</script>
<input type="hidden" id="hdnDirectPurchaseID" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />

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
                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtDirectPurchaseDate" Readonly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
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
                        <td><dxe:ASPxComboBox ID="cboDirectPurchaseType" ClientEnabled="false" ClientInstanceName="cboDirectPurchaseType" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Referensi")%></label></td>
                        <td><asp:TextBox ID="txtReferenceNo" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Referensi")%></label></td>
                        <td><asp:TextBox ID="txtReferenceDate" ReadOnly="true" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                    <asp:ListView runat="server" ID="lvwView">
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
                                                            <td align="right" class="lblReadOnlyText"><%#Eval("UnitPrice","{0:N}")%></td>
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
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountPercentage")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("DiscountAmount","{0:N}")%></td>
                                                <td class="lblReadOnlyText" align="right" style="color:Maroon"><%#Eval("LineAmount","{0:N}")%></td>
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
                                    <td align="right"><asp:CheckBox ID="chkPPN" Enabled="false" runat="server" /></td>
                                    <td><asp:TextBox ID="txtPPN" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server"/></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Diskon Final")%></label></td>
                                    <td><asp:TextBox ID="txtFinalDiscountPercentage" ReadOnly="true" CssClass="number" Width="50px" runat="server" /></td>
                                    <td>[%]</td>
                                    <td><asp:TextBox ID="txtFinalDiscountAmount" ReadOnly="true" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total Pembelian Tunai")%></label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtTotalNetTransactionAmount" ReadOnly="true" CssClass="txtCurrency" Width="180px" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>