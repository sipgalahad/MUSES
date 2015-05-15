<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentSubjectMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentSubjectMarkList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onBeforeRightPanelPrint(reportCode, filterExpression, errMessage) {
            if (reportCode == "SM-00002" || reportCode == "SM-00003") {
                filterExpression.text = "<%=GetFilterExpression() %>";
                return true;
            }
        }
    </script>
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />          
        <table rules="all" class="grdBorder grdSelected">
            <thead>
                <tr>
                    <th rowspan="3" class="thCenter"><%=GetLabel("Mata Pelajaran") %></th>
                    <th rowspan="3" class="thCenter" style="width:60px"><%=GetLabel("KKM") %></th>
                    <th class="thCenter" id="thMark" runat="server"><%=GetLabel("NILAI") %></th>
                    <th class="thCenter" id="thCompetencyDescription" runat="server"><%=GetLabel("Deskripsi Kompetensi") %></th>
                </tr>
                <tr>
                    <asp:Repeater ID="rptHeader2" runat="server" OnItemDataBound="rptHeader2_ItemDataBound">
                        <ItemTemplate>
                            <th class="thCenter" id="thHeader" runat="server" style="width:80px"></th>    
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptHeader2Desc" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" rowspan="2" style="width:80px"><%#Eval("CurriculumMarkTypeName")%></th>    
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <tr>
                    <asp:Repeater ID="rptHeader3" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" style="width:80px"><%=GetLabel("Nilai") %></th>    
                            <th class="thCenter" style="width:80px"><%=GetLabel("Predikat") %></th>    
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </thead>
            <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("SubjectName") %></td>
                        <td align="center"><%#Eval("PassingGrade") %></td>
                        <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                            <ItemTemplate>
                                <td class="thCenter" id="tdFinalMark" runat="server"></td>
                                <td class="thCenter" id="tdPredicateMark" runat="server"></td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptStudentMarkDesc" runat="server" OnItemDataBound="rptStudentMarkDesc_ItemDataBound">
                            <ItemTemplate>
                                <td class="thCenter" id="tdDescription" runat="server"></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>