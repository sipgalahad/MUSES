<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="BudgetInformationList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.BudgetInformationList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
    </style>
        
    <script type="text/javascript">
        
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                        <thead>
                            <tr>
                                <th rowspan="2" class="keyField" rowspan="2">&nbsp;</th>
                                <th rowspan="2" style="width:70px; text-align:left"><%=GetLabel("Kode")%></th>
                                <th rowspan="2" style="text-align:left"><%=GetLabel("Nama Anggaran")%></th>
                                <th rowspan="2" style="width:170px;text-align:left"><%=GetLabel("Bagian")%></th>
                                <th rowspan="2" style="width:200px;text-align:left"><%=GetLabel("Catatan")%></th>
                                <th style="text-align:center" id="thDana" runat="server" ><%=GetLabel("Sumber Dana")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Dianggarkan")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Direalisasikan")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Digunakan")%></th>
                            </tr>
                            <tr>
                                <asp:Repeater runat="server" ID="rptViewHeader">
                                    <ItemTemplate>
                                        <th style="width:70px; text-align:right"><%#:Eval("StandardCodeName") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </thead>
                        <asp:Repeater runat="server" ID="grdView" OnItemDataBound="grdView_ItemDataBound">
                            <ItemTemplate>
                                <tbody>
                                    <tr class="trData">
                                        <td class="keyField"><%#:Eval("BudgetID")%></td>
                                        <td><%#:Eval("BudgetCode")%></td>
                                        <td><%#:Eval("BudgetName")%></td>
                                        <td><%#:Eval("Position")%></td>
                                        <td><%#:Eval("Remarks")%></td>
                                        <asp:Repeater runat="server" ID="rptViewItem">
                                            <ItemTemplate>
                                                <td align="right"><%# Convert.ToDecimal(Container.DataItem.ToString()).ToString("N") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td align="right"><%#:Eval("ProposedAmount","{0:N}")%></td>
                                        <td align="right"><%#:Eval("RealizationAmount","{0:N}")%></td>
                                        <td align="right"><%#:Eval("UsedAmount","{0:N}")%></td>
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
</asp:Content>