<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalDocumentCtl.ascx.cs" 
    Inherits="CodeX.Web.Accounting.Program.JournalDocumentCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
   
</script>

<div style="height:440px; overflow-y:auto; overflow-x:hidden;">
    <input type="hidden" id="hdnReferenceNo" value="" runat="server" />
    <input type="hidden" id="hdnGLAccount" value="" runat="server" />
    <input type="hidden" id="hdnSubLedger" value="" runat="server" />
    <input type="hidden" id="hdnFilterExpression" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Dokumen")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtReferenceNo" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Akun")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtGLAccountName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Sub Akun")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtSubLedgerName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                </table>

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="JournalNo" HeaderText="Journal" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="JournalDateInString" HeaderText="Tanggal" />
                                        <asp:TemplateField HeaderStyle-Width="300px">
                                            <HeaderTemplate><%=GetLabel("Nama Perkiraan")%></HeaderTemplate>
                                            <ItemTemplate>
                                                <div><%#Eval("GLAccountName")%></div>
                                                <div><%#Eval("SubLedgerName")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Keterangan Transaksi" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                            <HeaderTemplate><%=GetLabel("Debit") %></HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Position").ToString() == "D" ? Eval("DebitAmount", "{0:N}") : "0"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                            <HeaderTemplate><%=GetLabel("Kredit") %></HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Position").ToString() == "K" ? Eval("CreditAmount", "{0:N}") : "0"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                <td>
                    <div style="width: 450px;">
                        <div class="lblComponent"><%=GetLabel("Document Properties") %></div>
                        <div style="background-color: #EAEAEA;">
                            <table width="300px" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="130px" />
                                    <col width="30px" />
                                </colgroup>
                                <tr>
                                    <td align="right"><%=GetLabel("Dibuat Oleh") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divCreatedBy"></div></td>
                                </tr>
                                <tr>
                                    <td align="right"><%=GetLabel("Dibuat Pada") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divCreatedDate"></div></td>
                                </tr>
                                <tr>
                                    <td align="right"><%=GetLabel("Diubah Oleh") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divLastUpdatedBy"></div></td>
                                </tr>
                                <tr>
                                    <td align="right"><%=GetLabel("Diubah Pada") %></td>
                                    <td align="center">:</td>
                                    <td><div runat="server" id="divLastUpdatedDate"></div></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
                <td style="float:right;">
                    <table width="300px">
                        <colgroup>
                            <col width="120px" />
                        </colgroup>
                        <tr>
                            <td><div class="lblComponent" style="text-align:left"><%=GetLabel("Total Debet") %></div></td>
                            <td><asp:TextBox ID="txtTotalDebet" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td><div class="lblComponent" style="text-align:left"><%=GetLabel("Total Kredit") %></div></td>
                            <td><asp:TextBox ID="txtTotalKredit" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td><div class="lblComponent" style="text-align:left"><%=GetLabel("Total Selisih") %></div></td>
                            <td><asp:TextBox ID="txtTotalSelisih" runat="server" CssClass="txtCurrency" Width="100%" ReadOnly="true" /></td>
                        </tr>
                    </table>                                
                </td>
            </tr>
        </table>
    </div>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Tutup")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

