<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentMarkPerTeacherInfoDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Information.Program.StudentMarkPerTeacherInfoDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCount, function (page) {
            cbpPopupView.PerformCallback('changepage|' + page);
        });
    });
    
    function onCbpPopupViewEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpPopupView.PerformCallback('changepage|' + page);
            });
        }
    }
    //#endregion

</script>
<input type="hidden" id="hdnTeacherID" runat="server" />
<input type="hidden" id="hdnClassSubjectID" runat="server" />

<div style="overflow-y: auto; overflow-x: auto; max-height: 400px">
    <table class="tblContentArea">
        <tr>
            <td>
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Guru")%></label></td>
                        <td><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
                        <td><asp:TextBox ID="txtHeaderText2" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
                        <td><asp:TextBox ID="txtHeaderText3" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>   
                </table>

                <div style="position: relative; overflow-x: scroll;">
                    <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent">
                        <tr>
                            <th rowspan="2" style="width:200px;"><%=GetLabel("Siswa") %></th>
                            <th colspan="10" class="thCenter"><%=GetLabel("NILAI") %></th>
                        </tr>
                        <tr>
                            <asp:Repeater ID="rptHeader" runat="server">
                                <ItemTemplate>
                                    <th class="thCenter" style="width:90px">
                                        <%#Eval("cfClassTaskCode")%>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td class="keyField"><%#Eval("StudentID") %></td>
                                    <td><%#Eval("StudentName") %>                                        </td>
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
            </td>
        </tr>
    </table>
</div>