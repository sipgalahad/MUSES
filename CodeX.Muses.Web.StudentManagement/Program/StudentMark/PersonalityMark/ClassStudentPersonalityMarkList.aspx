<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentPersonalityMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentPersonalityMarkList" %>

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
                var lstClassSubjectID = '';
                $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                    if (result != '') {
                        result += '|';
                        lstClassSubjectID += ',';
                    }
                    lstClassSubjectID += $(this).find('.keyField').html();
                    result += $(this).find('.keyField').html() + ';' + $(this).find('.txtMarkDescription').val();
                });
                $('#<%=hdnSaveValue.ClientID %>').val(result);
                $('#<%=hdnLstClassSubjectID.ClientID %>').val(lstClassSubjectID);
                onCustomButtonClick('save');
            });
        });
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassSubjectID" runat="server" />
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />  
         <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ClassSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                <asp:BoundField DataField="SubjectName" HeaderText="Aspek" ItemStyle-CssClass="tdSubjectName" />
                <asp:TemplateField HeaderStyle-Width="700px" HeaderStyle-CssClass="thCenter" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtMarkDescription" Width="100%" runat="server" CssClass="txtMarkDescription" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <%=GetLabel("No Data To Display")%>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>