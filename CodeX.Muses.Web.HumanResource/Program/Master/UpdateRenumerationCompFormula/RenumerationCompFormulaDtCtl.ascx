<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenumerationCompFormulaDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Information.Program.RenumerationCompFormulaDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
</script>
<input type="hidden" id="hdnHdID" runat="server" />
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnStartEffectiveDate" runat="server" />

<table class="tblContentArea">
    <tr>
        <td>
            <table class="tblEntryContent" style="width:70%">
                <colgroup>
                    <col style="width:160px"/>
                    <col/>
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Renumerasi")%></label></td>
                    <td><asp:TextBox ID="txtHeader" ReadOnly="true" Width="100%" runat="server" /></td>
                </tr>  
            </table>

            <div style="position: relative;">
                <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                    <ClientSideEvents EndCallback="function(s,e){hideLoadingPanel()}" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:330px; overflow-y: scroll;">
                                <table class="grdSelected" rules="all" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th class="keyField"></th>
                                        <th><%=GetLabel("Tipe")%></th>
                                        <th style="width:150px" class="thCenter"><%=GetLabel("Tarif Flat")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Max Jam")%></th>
                                        <th style="width:70px" class="thRight"><%=GetLabel("Pengali")%></th>
                                    </tr>
                                    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%#Eval("TransactionDtID")%></td>
                                                <td><%#Eval("cfBaseTariffType")%></td>
                                                <td align="center"><asp:CheckBox ID="chkIsTariffFlat" runat="server" Enabled="false" Value='<%#Eval("IsTariffFlat") %>' /></td>
                                                <td align="center"><%#Eval("MaxNHour")%></td>                                                
                                                <td align="right"><div id="divMultiplyBy" runat="server"></div></td>
                                            </tr>
                                            <asp:Repeater ID="rptFormula" runat="server">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <td class="keyField"></td>
                                                        <td colspan="4" style="padding-left:10px;">
                                                            <table class="grdSelected" rules="all" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Dari Jam")%></th>
                                                                    <th style="width:120px" class="thCenter"><%=GetLabel("Sampai Jam")%></th>
                                                                    <th class="thCenter"><%=GetLabel("Pengali")%></th>
                                                                </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                                <tr>
                                                                    <td align="center"><%#Eval("FromHoursIndex") %></td>
                                                                    <td align="center"><%#Eval("ToHoursIndex") %></td>
                                                                    <td align="center"><%#Eval("MultiplyBy") %></td>
                                                                </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>    
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>    
                <div class="imgLoadingGrdView" id="containerImgLoadingView" >
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
            </div>
        </td>
    </tr>
</table>