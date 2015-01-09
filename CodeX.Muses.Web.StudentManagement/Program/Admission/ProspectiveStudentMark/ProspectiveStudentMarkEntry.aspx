<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentMarkEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentMarkEntry" %>

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
                $('.grdStudent tr:gt(2)').each(function () {
                    var studentID = $(this).find('.keyField').html();
                    var tempResult = '';
                    $(this).find('.txtMark').each(function () {
                        var mark = $(this).val();
                        var remarks = $(this).closest('td').next('td').find('.txtStudentMarkRemarks').val();
                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += mark + ';' + remarks;
                    });
                    if (result != '')
                        result += '|';
                    result += studentID + ',' + tempResult;
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });
        });
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="3" style="width:100px"><%=GetLabel("No Pendaftaran") %></th>
            <th rowspan="3"><%=GetLabel("Calon Siswa") %></th>
            <th id="thMarkHeader" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" colspan="2">
                        <%#Eval("SelectionName")%> (<b><%#Eval("FinalMarkPercentage")%>%</b>)
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader2" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width:60px"><%=GetLabel("Nilai") %></th>
                    <th class="thCenter" style="width:200px"><%=GetLabel("Keterangan") %></th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="keyField"><%#Eval("RegistrationID")%></td>
                    <td><%#Eval("RegistrationNo")%></td>
                    <td><%#Eval("ProspectiveStudentName") %></td>
                    <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                        <ItemTemplate>
                            <td align="center">
                                <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtMark" Text="" Width="95%" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentMarkRemarks" runat="server" CssClass="txtStudentMarkRemarks" Text="" Width="95%" />
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>