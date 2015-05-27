<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCurriculumPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="CurriculumMarkTypeEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumMarkTypeEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtCurriculumMarkTypeCode.ClientID %>').val('');
                $('#<%=txtCurriculumMarkTypeName.ClientID %>').val('');
                cboTaskMarkType.SetValue('');
                cboFinalMarkType.SetValue('');
                cboPredicateMarkType.SetValue('');
                $('#<%=chkIsAllowTask.ClientID %>').prop('checked', false);
                $('#<%=chkIsShowCompetencyDescription.ClientID %>').prop('checked', false);
                cboCompetencyDescriptionType.SetValue('');
                cboCompetencyMarkType.SetValue('');
                cboStudentMarkGroup.SetValue('');

                $('#<%=hdnLstClassStudyTypeID.ClientID %>').val('');
                ddeClassStudyType.SetText('');
                $('.chkClassStudyType input:checked').each(function () {
                    $(this).prop('checked', false);
                });

                $('#<%=chkIsShowCompetencyDescription.ClientID %>').change();
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            $('#<%=chkIsShowCompetencyDescription.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    cboCompetencyDescriptionType.SetEnabled(true);
                    cboCompetencyMarkType.SetEnabled(true);
                }
                else {
                    cboCompetencyDescriptionType.SetEnabled(false);
                    cboCompetencyMarkType.SetEnabled(false);
                    cboCompetencyDescriptionType.SetValue('');
                    cboCompetencyMarkType.SetValue('');
                }
            });
        });

        //#region Class Study Type
        $('.chkClassStudyType input').live('change', function () {
            setDdeClassStudyTypeText();
        });

        function setDdeClassStudyTypeText() {
            var lstClassStudyTypeID = '';
            var lstClassStudyTypeName = '';
            $('.chkClassStudyType input:checked').each(function () {
                if (lstClassStudyTypeName != '') {
                    lstClassStudyTypeName += ', ';
                    lstClassStudyTypeID += ',';
                }
                lstClassStudyTypeID += $(this).parent().attr('gcclassstudytype');
                lstClassStudyTypeName += $(this).parent().attr('classstudytype');
            });
            $('#<%=hdnLstClassStudyTypeID.ClientID %>').val(lstClassStudyTypeID);
            ddeClassStudyType.SetText(lstClassStudyTypeName);
        }
        //#endregion

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumMarkTypeID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumMarkTypeID);
            $('#<%=txtCurriculumMarkTypeCode.ClientID %>').val(entity.CurriculumMarkTypeCode);
            $('#<%=txtCurriculumMarkTypeName.ClientID %>').val(entity.CurriculumMarkTypeName);
            cboStudentMarkGroup.SetValue(entity.GCStudentMarkGroup);
            cboTaskMarkType.SetValue(entity.TaskMarkTypeID);
            cboFinalMarkType.SetValue(entity.FinalMarkTypeID);
            cboPredicateMarkType.SetValue(entity.PredicateMarkTypeID);
            $('#<%=chkIsAllowTask.ClientID %>').prop('checked', entity.IsAllowTask == 'True');
            $('#<%=chkIsShowCompetencyDescription.ClientID %>').prop('checked', entity.IsShowCompetencyDescription == 'True');
            cboCompetencyDescriptionType.SetValue(entity.GCCompetencyDescriptionType);
            cboCompetencyMarkType.SetValue(entity.CompetencyMarkTypeID);
            $('#<%=chkIsShowCompetencyDescription.ClientID %>').change();

            $('.chkClassStudyType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            var lstGCClassStudyType = entity.ListGCClassStudyType.split(',');
            for (var i = 0; i < lstGCClassStudyType.length; ++i) {
                $('.chkClassStudyType').each(function () {
                    if ($(this).attr('gcclassstudytype') == lstGCClassStudyType[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeClassStudyTypeText();
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

        $('.lnkDetail a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Curriculum/CurriculumMarkType/CurriculumMarkTypeDtEntryCtl.ascx");
            openUserControlPopup(url, entity.CurriculumMarkTypeID, 'Detil', 800, 550);
        });
    </script>
    <input type="hidden" id="hdnLstClassStudyTypeID" runat="server" />
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
                                    <col style="width: 250px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                                    <td><asp:TextBox ID="txtCurriculumMarkTypeCode" runat="server" Width="100px" /></td>
                                </tr> 
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtCurriculumMarkTypeName" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Nilai")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboStudentMarkGroup" ClientInstanceName="cboStudentMarkGroup" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Pelajaran")%></label></td>
                                    <td>
                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeClassStudyType" ID="ddeClassStudyType"
                                            Width="300px" runat="server" EnableAnimation="False">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <asp:Repeater ID="rptClassStudyType" runat="server" OnItemDataBound="rptClassStudyType_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkClassStudyType" CssClass="chkClassStudyType" runat="server"  /> <%#Eval("StandardCodeName")%><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </DropDownWindowTemplate>
                                        </dxe:ASPxDropDownEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><h4><%=GetLabel("Format Nilai") %></h4></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Tugas")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboTaskMarkType" ClientInstanceName="cboTaskMarkType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Rapor")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFinalMarkType" ClientInstanceName="cboFinalMarkType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Predikat Akhir")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboPredicateMarkType" ClientInstanceName="cboPredicateMarkType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Allow Task")%></label></td>
                                    <td><asp:CheckBox ID="chkIsAllowTask" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><h4><%=GetLabel("Deskripsi Kompetensi")%></h4></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Allow Deskripsi Kompetensi")%></label></td>
                                    <td><asp:CheckBox ID="chkIsShowCompetencyDescription" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Deskripsi Kompetensi")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboCompetencyDescriptionType" ClientInstanceName="cboCompetencyDescriptionType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Format Kompetensi")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboCompetencyMarkType" ClientInstanceName="cboCompetencyMarkType" runat="server" Width="200px" /></td>
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
                                <asp:BoundField DataField="CurriculumMarkTypeCode" HeaderText="Kode" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="CurriculumMarkTypeName" HeaderText="Nama" />
                                <asp:BoundField DataField="TaskMarkTypeName" HeaderText="Format Nilai Tugas" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="FinalMarkTypeName" HeaderText="Format Nilai Akhir" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="PredicateMarkTypeName" HeaderText="Format Predikat Akhir" HeaderStyle-Width="150px" />
                                <asp:CheckBoxField DataField="IsShowCompetencyDescription" HeaderText="Kompetensi" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                                <asp:BoundField DataField="CompetencyMarkTypeName" HeaderText="Format Kompetensi" HeaderStyle-Width="150px" />
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("CurriculumMarkTypeID") %>" bindingfield="CurriculumMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("CurriculumMarkTypeCode") %>" bindingfield="CurriculumMarkTypeCode" />
                                        <input type="hidden" value="<%#Eval("CurriculumMarkTypeName") %>" bindingfield="CurriculumMarkTypeName" />
                                        <input type="hidden" value="<%#Eval("GCStudentMarkGroup") %>" bindingfield="GCStudentMarkGroup" />
                                        <input type="hidden" value="<%#Eval("TaskMarkTypeID") %>" bindingfield="TaskMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("FinalMarkTypeID") %>" bindingfield="FinalMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("PredicateMarkTypeID") %>" bindingfield="PredicateMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("ListGCClassStudyType") %>" bindingfield="ListGCClassStudyType" />
                                        <input type="hidden" value="<%#Eval("ListClassStudyType") %>" bindingfield="ListClassStudyType" />
                                        <input type="hidden" value="<%#Eval("IsAllowTask") %>" bindingfield="IsAllowTask" />
                                        <input type="hidden" value="<%#Eval("IsShowCompetencyDescription") %>" bindingfield="IsShowCompetencyDescription" />
                                        <input type="hidden" value="<%#Eval("GCCompetencyDescriptionType") %>" bindingfield="GCCompetencyDescriptionType" />
                                        <input type="hidden" value="<%#Eval("CompetencyMarkTypeID") %>" bindingfield="CompetencyMarkTypeID" />
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