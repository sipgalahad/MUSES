<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectMeetingPlanEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectMeetingPlanEntry" %>

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
                    $('#<%=txtMeetingNo.ClientID %>').val('');
                    $('#<%=txtRemarks.ClientID %>').val('');
                    lstBasicCompetencyID = '';
                    cboCompetencyStandard.SetValue('');
                    ddeBasicCompetency.SetText('');
                    cbpBasicCompetency.PerformCallback('refresh');

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

            $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectMeetingPlanHdID);
            $('#<%=txtMeetingNo.ClientID %>').val(entity.MeetingNo);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            lstBasicCompetencyID = entity.ListSubjectBasicCompetencyID;
            cboCompetencyStandard.SetValue(entity.SubjectCompetencyStandardID);
            cbpBasicCompetency.PerformCallback('refresh');
            $('#entryDetailContainer').show();
        });

        //#endregion

        var lstBasicCompetencyID = '';

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
                    cboCompetencyStandard.PerformCallback();
                    cbpView.PerformCallback('refresh');
                });
            });

        }

        function onTacSubjectMatterHdValueChanged() {
            cboCompetencyStandard.PerformCallback();
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        $('.chkBasicCompetency input').live('change', function () {
            setDdeBasicCompetencyText();
        });

        function setDdeBasicCompetencyText() {
            var lstID = '';
            var lstName = '';
            $('.chkBasicCompetency input:checked').each(function () {
                if (lstName != '') {
                    lstName += ', ';
                    lstID += ',';
                }
                lstID += $(this).parent().attr('id');
                lstName += $(this).parent().attr('name');
            });
            $('#<%=hdnLstBasicCompetencyID.ClientID %>').val(lstID);
            ddeBasicCompetency.SetText(lstName);
        }

        function onCbpBasicCompetencyEndCallback() {
            var lst = lstBasicCompetencyID.split(',');
            for (var i = 0; i < lst.length; ++i) {
                $('.chkBasicCompetency').each(function () {
                    if ($(this).attr('id') == lst[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeBasicCompetencyText();
            hideLoadingPanel();
        }

        function onCboCompetencyStandardValueChanged() {
            cbpBasicCompetency.PerformCallback('refresh');
        }

        $('.lnkIndicator a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectMeetingPlan/SubjectMeetingPlanIndicatorEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectMeetingPlanHdID, 'Indikator', 1100, 550);
        });
    </script>
    <input type="hidden" id="hdnLstBasicCompetencyID" value="" runat="server" />
    <fieldset id="fsFilter">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:200px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Materi")%></label></td>
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
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Pertemuan Ke")%></label></td>
                                    <td><asp:TextBox ID="txtMeetingNo" runat="server" Width="80px" CssClass="number" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Standar Kompetensi")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cboCompetencyStandard" ClientInstanceName="cboCompetencyStandard" runat="server" Width="200px" OnCallback="cboCompetencyStandard_Callback">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboCompetencyStandardValueChanged() }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kompetensi Dasar")%></label></td>
                                    <td>
                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeBasicCompetency" ID="ddeBasicCompetency"
                                            Width="500px" runat="server" EnableAnimation="False">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dxcp:ASPxCallbackPanel ID="cbpBasicCompetency" runat="server" Width="100%" ClientInstanceName="cbpBasicCompetency"
                                                    ShowLoadingPanel="false" OnCallback="cbpBasicCompetency_Callback">
                                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                                        EndCallback="function(s,e){ onCbpBasicCompetencyEndCallback(); }" />
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent1" runat="server">
                                                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                                                position: relative;">
                                                                <asp:GridView ID="grdBasicCompetency" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdBasicCompetency_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkBasicCompetency" CssClass="chkBasicCompetency" runat="server"  />         
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="SubjectCompetencyStandardName" HeaderText="Standar Kompetensi" HeaderStyle-Width="150px" />
                                                                        <asp:BoundField DataField="SubjectBasicCompetencyName" HeaderText="Kompetensi Dasar" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </DropDownWindowTemplate>
                                        </dxe:ASPxDropDownEdit>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
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
                                <asp:BoundField DataField="MeetingNo" HeaderText="Pertemuan Ke" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="SubjectCompetencyStandardName" HeaderText="Standar Kompetensi" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="ListSubjectBasicCompetencyName" HeaderText="Kompetensi Dasar" HeaderStyle-Width="300px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:HyperLinkField HeaderText="Indikator" Text="Indikator" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkIndicator" HeaderStyle-Width="120px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SubjectMeetingPlanHdID") %>" bindingfield="SubjectMeetingPlanHdID" />
                                        <input type="hidden" value="<%#Eval("MeetingNo") %>" bindingfield="MeetingNo" />
                                        <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                        <input type="hidden" value="<%#Eval("SubjectCompetencyStandardID") %>" bindingfield="SubjectCompetencyStandardID" />
                                        <input type="hidden" value="<%#Eval("ListSubjectBasicCompetencyID") %>" bindingfield="ListSubjectBasicCompetencyID" />
                                        <input type="hidden" value="<%#Eval("ListSubjectBasicCompetencyName") %>" bindingfield="ListSubjectBasicCompetencyName" />
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