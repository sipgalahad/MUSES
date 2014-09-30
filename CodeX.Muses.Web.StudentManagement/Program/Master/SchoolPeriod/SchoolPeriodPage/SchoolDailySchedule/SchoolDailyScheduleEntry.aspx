<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SchoolDailyScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolDailyScheduleEntry" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnMedicalRecordSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
        });
        
    </script> 
    <table style="width:100%">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Date") %> - <%=GetLabel("Time") %></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-right: 1px;width:145px"><asp:TextBox ID="txtNoteDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    <td style="width:5px">&nbsp;</td>
                                    <td><asp:TextBox ID="txtNoteTime" Width="80px" CssClass="time" runat="server" Style="text-align:center" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Subjective")%></label></td>
                        <td><asp:TextBox ID="txtSubjective" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Objective")%></label></td>
                        <td><asp:TextBox ID="txtObjective" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                    </tr>
                </table>            
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Assessment")%></label></td>
                        <td><asp:TextBox ID="txtAssessment" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                    </tr>
                </table>            
            </td>
            <td>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Planning")%></label></td>
                        <td><asp:TextBox ID="txtPlanning" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                    </tr>
                </table>         
            </td>
        </tr>
    </table>
</asp:Content>