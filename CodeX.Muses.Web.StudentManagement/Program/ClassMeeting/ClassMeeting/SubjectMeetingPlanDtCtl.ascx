<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectMeetingPlanDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectMeetingPlanDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    function onCboGCSubjectMeetingPlanDtValueChanged() {
        cbpViewPopup.PerformCallback('refresh');
    }
    function onCboGCSubjectMeetingPlanDt2ValueChanged() {
        cbpViewPopup2.PerformCallback('refresh');
    }
</script>

<div style="height:460px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pertemuan Ke")%></label></td>
            <td><asp:TextBox ID="txtMeetingNo" ReadOnly="true" Width="200px" runat="server" /></td>
        </tr> 
    </table>
    
    <h4 style="color: Maroon; font-weight: bold;"><%=GetLabel("Detil") %></h4>
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:80px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe")%></label></td>
            <td colspan="2">
                <dxe:ASPxComboBox ID="cboGCSubjectMeetingPlanDtType" ClientInstanceName="cboGCSubjectMeetingPlanDtType" Width="300px" runat="server">
                    <ClientSideEvents ValueChanged="function(){ onCboGCSubjectMeetingPlanDtValueChanged(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr> 
    </table>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <br /><br />
    <h4 style="color: Maroon; font-weight: bold;"><%=GetLabel("Langkah Pembelajaran") %></h4>
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:80px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe")%></label></td>
            <td colspan="2">
                <dxe:ASPxComboBox ID="cboGCSubjectMeetingPlanDtType2" ClientInstanceName="cboGCSubjectMeetingPlanDtType2" Width="300px" runat="server">
                    <ClientSideEvents ValueChanged="function(){ onCboGCSubjectMeetingPlanDt2ValueChanged(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr> 
    </table>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup2" runat="server" Width="100%" ClientInstanceName="cbpViewPopup2"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup2_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server">
                <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</div>

