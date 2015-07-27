<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsedTaskAmountCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.UsedTaskAmountCtl" %>

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

<div style="overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <div>
        <table width="50%">
            <colgroup>
                <col width="50px" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label><%=GetLabel("Anggaran")%></label></td>
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
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" 
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="ProjectTaskName" HeaderText="Kegiatan" HeaderStyle-Width="250px" />
                            <asp:TemplateField HeaderText="Keterangan" >
                                <ItemTemplate>
                                    <%#Eval("CustomRemarks")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UsedBudget" HeaderText="Jumlah" HeaderStyle-Width="120px" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <div style="float:right; margin-top:5px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Total")%></label></td>
                                <td></td>
                                <td><asp:TextBox ID="txtTotalUsedBudget" CssClass="txtCurrency" ReadOnly="true" Width="108px" runat="server" /></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</div>

