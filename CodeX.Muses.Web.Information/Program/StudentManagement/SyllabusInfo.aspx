<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="SyllabusInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.SyllabusInfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        function onCboSchoolTypeValueChanged() {
            tacCurriculum.setValue('');
            tacCurriculum.setText('');
        }

        //#region Curriculum
        function onGetCurriculumFilterExpression() {
            var filterExpression = "GCSchoolType = '" + cboSchoolType.GetValue() + "' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacCurriculumButtonSearchClick() {
            openSearchDialog('curriculum', onGetCurriculumFilterExpression(), function (value) {
                var filterExpression = onGetCurriculumFilterExpression() + " AND CurriculumCode = '" + value + "'";
                Methods.getObject('GetCurriculumList', filterExpression, function (result) {
                    if (result != null) {
                        tacCurriculum.setValue(result.CurriculumID);
                        tacCurriculum.setText(result.CurriculumName);
                    }
                    else {
                        tacCurriculum.setValue('');
                        tacCurriculum.setText('');
                    }
                    onTacCurriculumValueChanged();
                });
            });

        }

        function onTacCurriculumValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Subject
        function onGetSubjectFilterExpression() {
            var filterExpression = "SubjectID IN (SELECT SubjectID FROM CurriculumSubject WHERE CurriculumID = " + tacCurriculum.getValue() + ") AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacSubjectButtonSearchClick() {
            openSearchDialog('subject', onGetSubjectFilterExpression(), function (value) {
                var filterExpression = onGetSubjectFilterExpression() + " AND SubjectCode = '" + value + "'";
                Methods.getObject('GetSubjectList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubject.setValue(result.SubjectID);
                        tacSubject.setText(result.SubjectName);
                    }
                    else {
                        tacSubject.setValue('');
                        tacSubject.setText('');
                    }
                    onTacSubjectValueChanged();
                });
            });

        }

        function onTacSubjectValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion
    </script>
    <table>
        <colgroup>
            <col style="width: 120px" />
        </colgroup>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tipe Sekolah") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolType" ClientInstanceName="cboSchoolType" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSchoolTypeValueChanged(); }"  ValueChanged="function(s,e){ onCboSchoolTypeValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Kurikulum") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacCurriculum" ClientInstanceName="tacCurriculum" MethodName="GetCurriculumList" GetFilterExpressionFunction="onGetCurriculumFilterExpression"
                    SearchFields="CurriculumName,CurriculumCode" TextField="CurriculumName" ValueField="CurriculumID" SearchText="${CurriculumName} (<b>${CurriculumCode}</b>)" OrderByExpression="CurriculumName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacCurriculumButtonSearchClick(); }"
                        ValueChanged="function(){ onTacCurriculumValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubject" ClientInstanceName="tacSubject" MethodName="GetSubjectList" GetFilterExpressionFunction="onGetSubjectExpression"
                    SearchFields="SubjectName,SubjectCode" TextField="SubjectName" ValueField="SubjectID" SearchText="${SubjectName} (<b>${SubjectCode}</b>)" OrderByExpression="SubjectName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSubjectValueChanged(); }" />
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
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" OnRowDataBound="grdView_RowDataBound"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="SubjectCurriculumName" HeaderText="Nama" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="ListClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:TemplateField HeaderText="Silabus" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" >
                                    <ItemTemplate>
                                        <div id="divSyllabusCount" runat="server" />
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
