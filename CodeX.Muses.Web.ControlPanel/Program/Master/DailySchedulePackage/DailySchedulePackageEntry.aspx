<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="DailySchedulePackageEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.DailySchedulePackageEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #21B424; }
    </style>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtDailySchedulePackageCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtDailySchedulePackageName" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table style="width:100%">
        <tr>
            <td valign="top" id="tdSchoolDay1" runat="server">
                <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType1" ClientInstanceName="cboScheduleType1" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType1.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType1" runat="server" Width="100%" ClientInstanceName="cbpScheduleType1"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType1_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent6" runat="server">
                            <asp:Repeater ID="rptDay1" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel> 
            </td>
            <td valign="top" id="tdSchoolDay2" runat="server"> 
                <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType2" ClientInstanceName="cboScheduleType2" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType2.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType2" runat="server" Width="100%" ClientInstanceName="cbpScheduleType2"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType2_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent5" runat="server">
                            <asp:Repeater ID="rptDay2" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel> 
            </td>
            <td valign="top" id="tdSchoolDay3" runat="server"> 
                <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType3" ClientInstanceName="cboScheduleType3" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType3.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType3" runat="server" Width="100%" ClientInstanceName="cbpScheduleType3"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType3_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent4" runat="server">
                            <asp:Repeater ID="rptDay3" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel> 
            </td>
            <td valign="top" id="tdSchoolDay4" runat="server"> 
                <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType4" ClientInstanceName="cboScheduleType4" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType4.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType4" runat="server" Width="100%" ClientInstanceName="cbpScheduleType4"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType4_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">
                            <asp:Repeater ID="rptDay4" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel> 
            </td>
            <td valign="top" id="tdSchoolDay5" runat="server"> 
                <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType5" ClientInstanceName="cboScheduleType5" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType5.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType5" runat="server" Width="100%" ClientInstanceName="cbpScheduleType5"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType5_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:Repeater ID="rptDay5" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel> 
            </td>
            <td valign="top" id="tdSchoolDay6" runat="server"> 
                <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                <dxe:ASPxComboBox runat="server" ID="cboScheduleType6" ClientInstanceName="cboScheduleType6" Width="100%">
                    <ClientSideEvents ValueChanged="function(s,e) { cbpScheduleType6.PerformCallback(); }" />
                </dxe:ASPxComboBox>
                <dxcp:ASPxCallbackPanel ID="cbpScheduleType6" runat="server" Width="100%" ClientInstanceName="cbpScheduleType6"
                    ShowLoadingPanel="false" OnCallback="cbpScheduleType6_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Repeater ID="rptDay6" runat="server">
                                <HeaderTemplate>
                                    <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                        <td><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>    
            </td>
        </tr>
    </table>
    <br />
    <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
    <asp:Repeater ID="rptRemarks" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><div class='nts<%#Eval("cfStandardCodeID") %>' style="width: 20px; height: 20px; border: 1px solid black;"></div></td>
                <td><%#Eval("StandardCodeName") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
