<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskPerIndicatorDtViewCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskPerIndicatorDtViewCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        setStudentImage();
    });
</script>

<input type="hidden" id="hdnSubjectIndicatorID" runat="server" value="" />
<div style="overflow-y: scroll; height: 440px">
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
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 35px;">
                                    <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                    <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                    <div class="gridCircle divStudentImage"></div>
                                </td>
                                <td>
                                    <%#Eval("StudentName") %>
                                    <input type="hidden" id="Hidden1" class="hdnAttendance" runat="server" value="" />
                                </td>
                            </tr>
                        </table>
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
</div>

