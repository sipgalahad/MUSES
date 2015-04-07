<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentAttendanceList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentAttendanceList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('.lblDetail a').live('click', function () {
            var id = $(this).closest('tr').parent().closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/StudentMark/StudentAttendance/StudentAttendanceDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil', 800, 400);
        });
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassSubjectID" runat="server" />
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />  
         <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
            <Columns>
                <asp:BoundField DataField="StandardCodeID" ItemStyle-CssClass="keyField" HeaderStyle-CssClass="keyField" />
                <asp:BoundField DataField="StandardCodeName" HeaderText="Alasan Ketidakhadiran" HeaderStyle-Width="250px" />
                <asp:TemplateField HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 50px" align="right" class="lblDetail"><a><%#Eval("TagProperty") %></a></td>
                                <td style="width: 5px">&nbsp;</td>
                                <td><%=GetLabel("hari") %></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <%=GetLabel("No Data To Display")%>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>