<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="StudentMarkInformation.aspx.cs" Inherits="CodeX.Muses.Web.TeacherPage.Program.StudentMarkInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="2"><%=GetLabel("Siswa") %></th>
            <th colspan="10" class="thCenter"><%=GetLabel("NILAI") %></th>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width:90px">
                        <%#Eval("ClassTaskCode")%><br />
                        (<%#Eval("FinalMarkPercentage")%>%)
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="keyField"><%#Eval("StudentID") %></td>
                    <td>
                        <%#Eval("StudentName") %>
                        <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                    </td>
                    <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                        <ItemTemplate>
                            <td align="center">
                                <div id="divStudentMark" runat="server"></div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>