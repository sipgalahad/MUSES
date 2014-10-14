<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalListDtCtl.ascx.cs" 
    Inherits="Codex.Muses.Web.Accounting.Program.JournalListDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
   
</script>

<div style="height:440px; overflow-y:auto; overflow-x:hidden;">
    <input type="hidden" id="hdnGLTransactionID" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup
                        <col style="width:140px"/>
                        <col style="width:150px" />
                        <col />
                        <col style="width:140px"/>
                        <col style="width:160px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nomor Jurnal")%></label></td>
                        <td><asp:TextBox ID="txtJournalNo" ReadOnly="true" Width="100%" runat="server" /></td>
                        <td>&nbsp</td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelompok Jurnal")%></label></td>
                        <td><asp:TextBox ID="txtJournalGroup" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                </table>

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;">
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="80px">
                                            <HeaderTemplate><%=GetLabel("Perkiraan")%></HeaderTemplate>
                                            <ItemTemplate>
                                                <div><%#Eval("GLAccountNo")%></div>
                                                <div><%#Eval("SubLedgerCode")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="200px">
                                            <HeaderTemplate><%=GetLabel("Nama Perkiraan")%></HeaderTemplate>
                                            <ItemTemplate>
                                                <div><%#Eval("GLAccountName")%></div>
                                                <div><%#Eval("SubLedgerName")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Keterangan Transaksi" />
                                        <asp:BoundField DataField="DebitAmount" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderText="DEBIT" HeaderStyle-Width="90px" DataFormatString="{0:N}"/>
                                        <asp:BoundField DataField="CreditAmount" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderText="CREDIT" HeaderStyle-Width="90px" DataFormatString="{0:N}"/>
                                        <asp:TemplateField HeaderStyle-Width="10px" />
                                        <asp:BoundField DataField="ReferenceNo" HeaderText="No. Dokumen" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="130px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("Data Tidak Tersedia")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup">
                                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                                </div>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    
    <div>
        <table width="100%">
            <tr>
                <td style="vertical-align:top">
                    <div style="width: 450px">
                        <div class="lblComponent" style="text-align:left; padding-left:5px;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("Informasi Jurnal") %></div>
                        <div style="background-color: #EAEAEA;">
                            <table width="450px" cellpadding="0" cellspacing="1">
                                <colgroup>
                                    <col width="200px" />
                                    <col width="20px" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td align="right"><%=GetLabel("Dibuat Oleh / Tanggal") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divCreatedBy" style="color:Maroon"></div></td>
                                </tr>
                                <tr>
                                    <td align="right"><%=GetLabel("Diubah Oleh / Tanggal") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divLastUpdatedBy" style="color:Maroon"></div></td>
                                </tr>
                                <tr>
                                    <td>&nbsp</td>
                                    <td>&nbsp</td>
                                    <td>&nbsp</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
                <td style="float:right;">
                    <table width="300px" cellpadding="0" cellspacing="1">
                        <colgroup>
                            <col width="120px" />
                        </colgroup>
                        <tr>
                            <td><div class="lblComponent" style="text-align:right;padding-right:5px;padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL DEBET") %></div></td>
                            <td><asp:TextBox ID="txtTotalDebet" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td><div class="lblComponent" style="text-align:right;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL KREDIT") %></div></td>
                            <td><asp:TextBox ID="txtTotalKredit" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td><div class="lblComponent" style="text-align:right;padding-right:5px; padding-bottom:4px;padding-top:4px"><%=GetLabel("TOTAL SELISIH") %></div></td>
                            <td><asp:TextBox ID="txtTotalSelisih" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                    </table>                                
                </td>
            </tr>
        </table>
    </div>
</div>

