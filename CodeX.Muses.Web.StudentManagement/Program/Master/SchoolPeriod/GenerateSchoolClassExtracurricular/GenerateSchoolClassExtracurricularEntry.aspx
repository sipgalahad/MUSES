<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="GenerateSchoolClassExtracurricularEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.GenerateSchoolClassExtracurricularEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('.lnkGenerate').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/GenerateSchoolClassExtracurricular/GenerateSchoolClassExtracurricularEntryCtl.ascx");
            openUserControlPopup(url, id, 'Generate Kelas', 1000, 550);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PeriodClassTypeID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="CurriculumClassTypeName" HeaderText="Tipe Kelas"/>
                                <asp:BoundField DataField="NoOfClass" HeaderText="Jumlah Kelas" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a class="lnkGenerate" <%#Eval("IsAllowEditItem").ToString() == "False" ? "style='display:none'" : "" %>><%=GetLabel("Generate Kelas")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("No Data To Display")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>