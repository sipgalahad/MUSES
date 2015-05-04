<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectMatterInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectMatterInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region Subject Competency Standard
        function onGetSubjectCompetencyStandardFilterExpression() {
            var filterExpression = "SubjectMatterID = " + $('#<%=hdnSubjectCurriculumID.ClientID %>').val() + " AND GCPeriodSection = '" + cboGCPeriodSection.GetValue() + "' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacSubjectCompetencyStandardButtonSearchClick() {
            openSearchDialog('subjectcompetencystandard', onGetSubjectCompetencyStandardFilterExpression(), function (value) {
                var filterExpression = onGetSubjectCompetencyStandardFilterExpression() + " AND SubjectCompetencyStandardID = '" + value + "'";
                Methods.getObject('GetSubjectCompetencyStandardList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubjectCompetencyStandard.setValue(result.SubjectCompetencyStandardID);
                        tacSubjectCompetencyStandard.setText(result.SubjectCompetencyStandardName);
                    }
                    else {
                        tacSubjectCompetencyStandard.setValue('');
                        tacSubjectCompetencyStandard.setText('');
                    }
                    cbpView.PerformCallback('refresh');
                });
            });

        }

        function onTacSubjectCompetencyStandardValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        $('.lnkDetail a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/SubjectMatter/SubjectBasicCompetencyDtInformationCtl.ascx");
            openUserControlPopup(url, id, 'Detil', 1100, 550);
        });

        $('.lnkIndicator a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/SubjectMatter/SubjectIndicatorInformationCtl.ascx");
            openUserControlPopup(url, id, 'Indikator', 1100, 550);
        });

        function onCboGCPeriodSectionValueChanged() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
            <td>
                <dxe:ASPxComboBox ID="cboGCPeriodSection" ClientInstanceName="cboGCPeriodSection" Width="200px" runat="server">
                    <ClientSideEvents ValueChanged="function(){ onCboGCPeriodSectionValueChanged(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Standar Kompetensi")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubjectCompetencyStandard" ClientInstanceName="tacSubjectCompetencyStandard" MethodName="GetSubjectCompetencyStandardList" GetFilterExpressionFunction="onGetSubjectCompetencyStandardFilterExpression"
                    SearchFields="SubjectCompetencyStandardName" TextField="SubjectCompetencyStandardName" ValueField="SubjectCompetencyStandardID" SearchText="${SubjectCompetencyStandardName}" OrderByExpression="SubjectCompetencyStandardName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCompetencyStandardButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSubjectCompetencyStandardValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
    </table>
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
                                <asp:BoundField DataField="SubjectBasicCompetencyID" ItemStyle-CssClass="keyField" HeaderStyle-CssClass="keyField" />
                                <asp:BoundField DataField="SubjectBasicCompetencyName" HeaderText="Kompetensi Dasar" HeaderStyle-Width="250px" />
                                <asp:BoundField DataField="StudySource" HeaderText="Sumber / Bahan / Alat" />
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