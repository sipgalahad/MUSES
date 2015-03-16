<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentSubjectMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentSubjectMarkList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <div style="height:440px; overflow-y:auto">
        <input type="hidden" id="hdnID" value="" runat="server" />  
         <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" ItemStyle-CssClass="tdSubjectName" />
                <asp:BoundField DataField="PassingGrade" HeaderStyle-Width="60px" HeaderStyle-CssClass="thCenter" HeaderText="KKM" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderStyle-Width="60px" HeaderStyle-CssClass="thCenter" HeaderText="Nilai" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div runat="server" id="divMark"></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <%=GetLabel("No Data To Display")%>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>