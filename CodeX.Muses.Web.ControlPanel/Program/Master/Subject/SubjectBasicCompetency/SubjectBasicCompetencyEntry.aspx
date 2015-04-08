<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectBasicCompetencyEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectBasicCompetencyEntry" %>

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
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsFilter', 'mpFilter')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=txtSubjectBasicCompetencyName.ClientID %>').val('');
                    $('#<%=txtStudySource.ClientID %>').val('');

                    $('#entryDetailContainer').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectBasicCompetencyID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectBasicCompetencyID);
            $('#<%=txtSubjectBasicCompetencyName.ClientID %>').val(entity.SubjectBasicCompetencyName);
            $('#<%=txtStudySource.ClientID %>').val(entity.StudySource);
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        function onCboFilterValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        //#region SubjectMatterHd
        function onGetSubjectMatterHdFilterExpression() {
            var filterExpression = "<%=OnGetSubjectMatterHdFilterExpression() %>";
            return filterExpression;
        }

        function onTacSubjectMatterHdButtonSearchClick() {
            openSearchDialog('subjectmatter', onGetSubjectMatterHdFilterExpression(), function (value) {
                var filterExpression = onGetSubjectMatterHdFilterExpression() + " AND SubjectMatterCode = '" + value + "'";
                Methods.getObject('GetSubjectMatterHdList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubjectMatterHd.setValue(result.SubjectMatterID);
                        tacSubjectMatterHd.setText(result.SubjectMatterName);
                    }
                    else {
                        tacSubjectMatterHd.setValue('');
                        tacSubjectMatterHd.setText('');
                    }
                    tacSubjectCompetencyStandard.setValue('');
                    tacSubjectCompetencyStandard.setText('');
                    cbpView.PerformCallback('refresh');
                });
            });

        }

        function onTacSubjectMatterHdValueChanged() {
            tacSubjectCompetencyStandard.setValue('');
            tacSubjectCompetencyStandard.setText('');
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Subject Competency Standard
        function onGetSubjectCompetencyStandardFilterExpression() {
            var filterExpression = "SubjectMatterID = " + tacSubjectMatterHd.getValue() + " AND GCPeriodSection = '" + cboGCPeriodSection.GetValue() + "' AND IsDeleted = 0";
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
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectBasicCompetency/SubjectBasicCompetencyDtEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectBasicCompetencyID, 'Detil', 800, 550);
        });

        $('.lnkIndicator a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectBasicCompetency/SubjectIndicatorEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectBasicCompetencyID, 'Indikator', 800, 550);
        });
    </script>
    <fieldset id="fsFilter">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:200px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
                <td>            
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubjectMatterHd" ClientInstanceName="tacSubjectMatterHd" MethodName="GetSubjectMatterHdList" GetFilterExpressionFunction="onGetSubjectMatterHdFilterExpression"
                        SearchFields="SubjectMatterName,SubjectMatterID" TextField="SubjectMatterName" ValueField="SubjectMatterID" SearchText="${SubjectMatterName} (<b>${SubjectMatterCode}</b>)" OrderByExpression="SubjectMatterName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectMatterHdButtonSearchClick(); }"
                            ValueChanged="function(){ onTacSubjectMatterHdValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr> 
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
    </fieldset>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 160px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kompetensi Dasar")%></label></td>
                                    <td><asp:TextBox ID="txtSubjectBasicCompetencyName" runat="server" Width="500px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Sumber / Bahan / Alat") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtStudySource" TextMode="MultiLine" Rows="2" Width="500px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
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
                                <asp:BoundField DataField="SubjectBasicCompetencyName" HeaderText="Kompetensi Dasar" HeaderStyle-Width="250px" />
                                <asp:BoundField DataField="StudySource" HeaderText="Sumber / Bahan / Alat" />
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="100px" />
                                <asp:HyperLinkField HeaderText="Indikator" Text="Indikator" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkIndicator" HeaderStyle-Width="120px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SubjectBasicCompetencyID") %>" bindingfield="SubjectBasicCompetencyID" />
                                        <input type="hidden" value="<%#Eval("SubjectBasicCompetencyName") %>" bindingfield="SubjectBasicCompetencyName" />
                                        <input type="hidden" value="<%#Eval("StudySource") %>" bindingfield="StudySource" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>