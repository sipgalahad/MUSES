<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProposedBudgetDetailCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProposedBudgetDetailCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <div>
        <table width="50%">
            <colgroup>
                <col width="220px" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label><%=GetLabel("No. Rancangan Anggaran")%></label></td>
                <td><asp:TextBox ID="txtProposedBudgetNo" Width="100%" ReadOnly="true" runat="server" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><label><%=GetLabel("Bagian")%></label></td>
                <td><asp:TextBox ID="txtPosition" Width="100%" ReadOnly="true" runat="server" /></td>
            </tr>
        </table>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                        <thead>
                            <tr>
                                <th class="keyField" rowspan="2">&nbsp;</th>
                                <th style="width:70px; text-align:left"><%=GetLabel("Kode")%></th>  
                                <th style="text-align:left"><%=GetLabel("Nama Anggaran")%></th>                              
                                <th style="width:250px;text-align:left"><%=GetLabel("Catatan")%></th>
                                <asp:Repeater runat="server" ID="rptViewHeader">
                                    <ItemTemplate>
                                        <th style="width:100px; text-align:right"><%#:Eval("StandardCodeName") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <th style="width:100px; text-align:right"><%=GetLabel("Total")%></th>
                            </tr>
                        </thead>
                        <asp:Repeater runat="server" ID="grdView" OnItemDataBound="grdView_ItemDataBound">
                            <ItemTemplate>
                                <tbody>
                                    <tr class="trData">
                                        <td class="keyField"><%#:Eval("ProposedBudgetDtID")%></td>
                                        <td><%#:Eval("ProposedBudgetCode")%></td>
                                        <td><%#:Eval("ProposedBudgetName")%></td>
                                        <td><%#:Eval("Remarks")%></td>
                                        <asp:Repeater runat="server" ID="rptViewItem">
                                            <ItemTemplate>
                                                <td align="right"><%# Convert.ToDecimal(Container.DataItem.ToString()).ToString("N") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td align="right"><%#:Eval("TotalAmount","{0:N}")%></td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr class="trEmpty" runat="server" id="trEmpty">
                                    <td colspan="100">
                                        <%=GetLabel("No Data To Display")%>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</div>

