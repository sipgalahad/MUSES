<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassAttendanceEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassAttendanceEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.grdStudent tr:gt(1)').each(function () {
                    var studentID = $(this).find('.keyField').html();
                    var selected = $(this).find("input[type='radio']:checked");
                    var attendanceStatus = '';
                    if (selected.length > 0)
                        attendanceStatus = selected.val();
                    if (result != '')
                        result += '|';
                    result += studentID + ',' + attendanceStatus;
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });

            $('.grdStudent tr:gt(1)').each(function () {
                var hdnAttendance = $(this).find(".hdnAttendance").val();
                if (hdnAttendance != '') {
                    $rdo = $(this).find("input[type='radio'][value='" + hdnAttendance + "']");
                    $rdo.attr('checked', true);
                }
            });

            $("input[type='radio'][name='rdoAttendanceAll']").live('change', function () {
                var selectedValue = $(this).val();
                $('.grdStudent tr:gt(1)').each(function () {
                    $rdo = $(this).find("input[type='radio'][value='" + selectedValue + "']");
                    $rdo.attr('checked', true);
                });
            });

            $('.rdoAttendanceDt').live('change', function () {
                $rdo = $("input[type='radio'][name='rdoAttendanceAll']:checked");
                if ($rdo != null)
                    $rdo.attr('checked', false);
            });
        });
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="2"><%=GetLabel("Siswa") %></th>
            <th colspan="10" class="thCenter"><%=GetLabel("STATUS KEHADIRAN") %></th>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width:100px">
                        <%#Eval("StandardCodeName") %><br />
                        <input type="radio" name="rdoAttendanceAll" value='<%#Eval("StandardCodeID") %>' />
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
                    <asp:Repeater ID="rptStudentAttendance" runat="server">
                        <ItemTemplate>
                            <td align="center">
                                <input type="radio" class="rdoAttendanceDt" name="rdoAttendance<%# ((RepeaterItem)Container.Parent.Parent).ItemIndex %>" value='<%#Eval("StandardCodeID") %>' />
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>