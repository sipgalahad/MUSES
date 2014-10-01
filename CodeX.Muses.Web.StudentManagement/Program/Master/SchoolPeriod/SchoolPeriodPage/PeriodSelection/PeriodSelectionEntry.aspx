<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="PeriodSelectionEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodSelectionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
        });
        
    </script> 
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #21B424; }
    </style>
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
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="1" />
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
            </td>
            <td valign="top"> 
                <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="2" />
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
            </td>
            <td valign="top"> 
                <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="3" />
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
            </td>
            <td valign="top"> 
                <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="4" />
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
            </td>
            <td valign="top"> 
                <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="5" />
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
            </td>
            <td valign="top"> 
                <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                <center><span class="lblLink"><%=GetLabel("Ubah Data")%></span><br /></center>
                <input type="hidden" class="hdnDayNumber" value="6" />
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