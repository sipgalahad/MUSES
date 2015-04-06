<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectMatterInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectMatterInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('.lnkDetail a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/SubjectMatter/SubjectMeetingPlanDtInformationCtl.ascx");
            openUserControlPopup(url, id, 'Detil', 1100, 550);
        });

        $('.lnkIndicator a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/SubjectMatter/SubjectMeetingPlanIndicatorInformationCtl.ascx");
            openUserControlPopup(url, id, 'Indikator', 1100, 550);
        });
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
                                <asp:BoundField DataField="SubjectMeetingPlanHdID" ItemStyle-CssClass="keyField" HeaderStyle-CssClass="keyField" />
                                <asp:BoundField DataField="MeetingNo" HeaderText="Pertemuan Ke" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" />
                                <asp:TemplateField HeaderStyle-Width="10px" />
                                <asp:BoundField DataField="SubjectCompetencyStandardName" HeaderText="Standar Kompetensi" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="ListSubjectBasicCompetencyName" HeaderText="Kompetensi Dasar" HeaderStyle-Width="300px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="100px" />
                                <asp:HyperLinkField HeaderText="Indikator" Text="Indikator" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkIndicator" HeaderStyle-Width="120px" />
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