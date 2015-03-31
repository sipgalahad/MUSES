<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentSubjectMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentSubjectMarkList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnPrint" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/print.png")%>' alt="" /><div><%=GetLabel("Print")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnPrint.ClientID %>').click(function () {
                var reportCode = "SM-00002";
                var filterExpression = "<%=GetFilterExpression() %>";
                openReportViewer(reportCode, filterExpression);
            });
        });
    </script>
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />  
        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
            <HeaderTemplate>
                <table rules="all" class="grdBorder grdSelected">
                    <thead>
                        <tr>
                            <th rowspan="2" class="thCenter"><%=GetLabel("Mata Pelajaran") %></th>
                            <th rowspan="2" class="thCenter" style="width:60px"><%=GetLabel("KKM") %></th>
                            <th colspan="2" class="thCenter"><%=GetLabel("NILAI") %></th>
                            <th colspan="2" class="thCenter"><%=GetLabel("Affective") %></th>
                            <th rowspan="2" class="thCenter" style="width:350px"><%=GetLabel("Deskripsi Kemajuan Belajar") %></th>
                        </tr>
                        <tr>
                            <th class="thCenter" style="width:60px"><%=GetLabel("Teori") %></th>
                            <th class="thCenter" style="width:60px"><%=GetLabel("Praktek") %></th>
                            <th class="thCenter" style="width:60px"><%=GetLabel("Nilai") %></th>
                            <th class="thCenter" style="width:250px"><%=GetLabel("Deskripsi") %></th>
                        </tr>
            </HeaderTemplate> 
            <ItemTemplate>
                <tr>
                    <td><%#Eval("SubjectName") %></td>
                    <td align="center"><%#Eval("PassingGrade") %></td>
                    <td align="center"><div runat="server" id="divMarkTheory"></div></td>
                    <td align="center"><div runat="server" id="divMarkPractice"></div></td>
                    <td align="center"><div runat="server" id="divAffectiveMark"></div></td>
                    <td><div runat="server" id="divAffectiveDescription"></div></td>
                    <td><div runat="server" id="divProgressDescription"></div></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </thead>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>