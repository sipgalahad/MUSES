<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenumerationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Information.Program.RenumerationDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
</script>
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
                <tr id="trOrganizationPosition" runat="server" style="display:none"> 
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Posisi")%></label></td>
                    <td><asp:TextBox ID="txtorganizationPosition" ReadOnly="true" Width="100%" runat="server" /></td>
                </tr>  
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Renumerasi")%></label></td>
                    <td><asp:TextBox ID="txtHeader" ReadOnly="true" Width="100%" runat="server"/></td>
                </tr>  
            </table>

            <div style="position: relative;">
                <dxcp:ASPxCallbackPanel ID="cbpPopupView" runat="server" Width="100%" ClientInstanceName="cbpPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpPopupView_Callback">
                    <ClientSideEvents EndCallback="function(s,e){hideLoadingPanel()}" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:330px; overflow-y: scroll;">
                                <asp:GridView ID="grdPopupView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="RenumerationCompName" HeaderText="Komponen" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="thLeft" />
                                        <asp:BoundField DataField="RenumerationCompType" HeaderText="Tipe Pembayaran" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="thLeft" HeaderStyle-Width="150px" />
                                        <asp:CheckBoxField DataField="IsAllowChange" HeaderText="Is Allow Change" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"/>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="70px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
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