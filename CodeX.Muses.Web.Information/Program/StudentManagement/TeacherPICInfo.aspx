<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="TeacherPICInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.TeacherPICInfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
         function onCboSchoolPeriodValueChanged(s) {
             cbpView.PerformCallback('refresh');
         }

         //#region Teacher
         function onGetTeacherFilterExpression() {
             var filterExpression = "IsDeleted = 0";
             return filterExpression;
         }

         function onTacTeacherButtonSearchClick() {
             openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                 var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                 Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                     if (result != null) {
                         tacTeacher.setValue(result.TeacherID);
                         tacTeacher.setText(result.TeacherName);
                     }
                     else {
                         tacTeacher.setValue('');
                         tacTeacher.setText('');
                     }
                     onTacTeacherValueChanged();
                 });
             });

         }

         function onTacTeacherValueChanged() {
             cbpView.PerformCallback('refresh');
         }
         //#endregion
    </script>
    <table>
        <colgroup>
            <col style="width: 120px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
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
        .tblSchedule tr.T001                { height: 100px; cursor: pointer; }
        .tblSchedule tr.T001 .divTime, .nts001                   { background-color: #2FD933; }
        .tblSchedule tr.T001 b              { color: Red; font-weight: normal; }
        
        .tblSchedule tr.T001 .tdHtmlText    { background-color: #FFD837 !important; }
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
                            <tr>
                                <td valign="top" id="tdSchoolDay1" runat="server">
                                    <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                                    <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay2" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay3" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay4" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay5" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay6" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server" valign="top">
                                                    <div class="divTime"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%" class="tblTeacherDt">
                                                        <asp:Repeater ID="rptTeacherScheduleDt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><input type="hidden" value='<%#Eval("TeacherID") %>' class="hdnTeacherID"/><%#Eval("TeacherName") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                        <br />    
                        <table style="width:100%">
                            <colgroup>
                                <col style="width: 50%" />
                            </colgroup>
                            <tr>
                                <td valign="top">
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
