<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentResultList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentResultList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbset.png")%>' alt="" /><div><%=GetLabel("Terima")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $chk = $(this).find('input');
                $chk.prop('checked', isChecked);
            });
        });
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="2" style="width:40px" class="thCenter"><input id="chkSelectAll" type="checkbox" /></th>
            <th rowspan="2"><%=GetLabel("Calon Siswa") %></th>
            <th colspan='<%=OnGetSelectionCount() %>' class="thCenter"><%=GetLabel("NILAI") %></th>
            <th rowspan="2" style="width:110px" class="thCenter"><%=GetLabel("Nilai Akhir") %></th>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width:110px">
                        <%#Eval("SelectionName")%><br />
                        (<%#Eval("FinalMarkPercentage")%>%)
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="center"><asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" /></td>
                    <td class="keyField"><%#Eval("RegistrationID")%></td>
                    <td><%#Eval("ProspectiveStudentName") %></td>
                    <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                        <ItemTemplate>
                            <td align="center">
                                <div id="divStudentMark" runat="server"></div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <td align="center"><%#Eval("FinalMark") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>