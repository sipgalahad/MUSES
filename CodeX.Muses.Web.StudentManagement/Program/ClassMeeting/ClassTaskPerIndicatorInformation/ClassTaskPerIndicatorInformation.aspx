<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassTaskPerIndicatorInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskPerIndicatorInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('.lblIndicator.lblLink').live('click', function () {
            var id = $(this).parent().find('.hdnSubjectIndicatorID').val();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskPerIndicatorInformation/ClassTaskPerIndicatorDtViewCtl.ascx");
            openUserControlPopup(url, id, 'Detil Tugas', 800, 550);
        });
        $('.lblTask.lblLink').live('click', function () {
            var id = $(this).parent().find('.hdnClassSubjectTaskID').val();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskSummary/ClassTaskViewCtl.ascx");
            openUserControlPopup(url, id, 'Detil Tugas', 800, 550);
        });
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnListIndicatorName" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnLstMarkTypeDt" runat="server" />
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnTableWidth" runat="server" />
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
            <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
        </tr>
    </table>
    <div style="width:1250px; overflow-x: auto;">
        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent" id="tblView">
            <tr>
                <th rowspan="2"></th>
                <th id="thClassTask" runat="server" class="thCenter">Tugas</th>
                <th colspan="3" class="thCenter">Nilai</th>
            </tr>
            <tr>
                <asp:Repeater ID="rptClassTaskHeader" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width: 80px">
                            <input type="hidden" class="hdnClassSubjectTaskID" value='<%#Eval("ClassSubjectTaskID") %>' />
                            <label class="lblTask lblLink"><%#Eval("ClassTaskCode")%></label><br />
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <th class="thCenter" style="width: 80px">Rata-Rata</th>
                <th class="thCenter" style="width: 80px">Tertinggi</th>
                <th class="thCenter" style="width: 80px">Terrendah</th>
            </tr>
            <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="hidden" class="hdnSubjectIndicatorID" value='<%#Eval("SubjectIndicatorID")%>' />
                            <label class="lblIndicator lblLink"><%#Eval("SubjectIndicatorName")%></label><br />
                        </td>
                        <asp:Repeater ID="rptClassTask" runat="server" OnItemDataBound="rptClassTask_ItemDataBound">
                            <ItemTemplate>
                                <td class="thCenter" style="width: 80px">
                                    <div id="divClassTask" runat="server">✔</div>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td id="tdAverage" align="center" runat="server"></td>
                        <td id="tdMax" align="center" runat="server"></td>
                        <td id="tdMin" align="center" runat="server"></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>