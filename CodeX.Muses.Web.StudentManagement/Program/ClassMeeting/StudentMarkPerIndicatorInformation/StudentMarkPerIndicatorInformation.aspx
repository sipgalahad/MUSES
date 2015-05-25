<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="StudentMarkPerIndicatorInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentMarkPerIndicatorInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setStudentImage();

            var width = parseInt('<%=OnGetTableViewWidth() %>');
            if (width < 1250)
                width = 1250;
            $('#tblView').width(width);
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
                <th rowspan="3"><%=GetLabel("Siswa") %></th>
                <asp:Repeater ID="rptHeader1" runat="server" OnItemDataBound="rptHeader1_ItemDataBound">
                    <ItemTemplate>
                        <th class="thCenter" id="thMarkType" runat="server"><%#Eval("CurriculumMarkTypeName")%><br /></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader2" runat="server" OnItemDataBound="rptHeader2_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater ID="rptHeader2Dt" runat="server" OnItemDataBound="rptHeader2Dt_ItemDataBound">
                            <ItemTemplate>
                                <th class="thCenter" id="thIndicator" runat="server">
                                    <input type="hidden" class="hdnIndicatorName" value='<%#Eval("SubjectIndicatorName")%>' />
                                    <%#Eval("SubjectIndicatorName")%>
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader3" runat="server" OnItemDataBound="rptHeader3_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater ID="rptHeader3Dt" runat="server" OnItemDataBound="rptHeader3Dt_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptHeader3Dt2" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width: 80px">
                                            <input type="hidden" class="hdnClassSubjectTaskID" value='<%#Eval("ClassSubjectTaskID") %>' />
                                            <label class="lblTask lblLink"><%#Eval("ClassTaskCode")%></label><br />
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                <ItemTemplate>
                    <tr class="trStudent">
                        <td class="keyField"><%#Eval("StudentID") %></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 35px;">
                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                        <div class="gridCircle divStudentImage"></div>
                                    </td>
                                    <td>
                                        <%#Eval("StudentName") %>
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                        </td>
                        <asp:Repeater ID="rptStudentMarkType" runat="server" OnItemDataBound="rptStudentMarkType_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptStudentMarkDt" runat="server" OnItemDataBound="rptStudentMarkDt_ItemDataBound">
                                            <ItemTemplate>
                                                <td align="center" id="tdStudentMark" runat="server"></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>