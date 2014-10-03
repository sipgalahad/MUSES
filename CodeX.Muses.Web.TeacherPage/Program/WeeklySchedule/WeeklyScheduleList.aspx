<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="WeeklyScheduleList.aspx.cs" Inherits="CodeX.Muses.Web.TeacherPage.Program.WeeklyScheduleList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        function onCboSchoolPeriodValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 44px; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #2FD933; }
    </style>

    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <table style="width:100%">
                            <colgroup>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                            </colgroup>
                            <tr>
                                <td valign="top">
                                    <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                                    <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
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