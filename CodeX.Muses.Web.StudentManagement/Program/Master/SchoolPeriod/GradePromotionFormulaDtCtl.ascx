<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GradePromotionFormulaDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.GradePromotionFormulaDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
</script>

<div>
    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
        <Columns>
            <asp:BoundField DataField="GradePromotionFormulaDtName" HeaderText="Nama" />
            <asp:CheckBoxField DataField="IsCurrentGrade" HeaderText="Kelas Sekarang" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" />
            <asp:BoundField DataField="Grade" HeaderText="Kelas / Tingkat" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="CurriculumSchoolPeriodSectionName" HeaderText="Semester"  HeaderStyle-Width="150px" />
            <asp:BoundField DataField="FinalMarkPercentage" HeaderText="[%] Bobot Nilai Akhir" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" />
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("No Data To Display")%>
        </EmptyDataTemplate>
    </asp:GridView>
</div>

