<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="PeriodClassTypeEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodClassTypeEntry" %>

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
                cboClassType.SetValue('');
                cboDailySchedulePackage.SetValue('');
                $('#<%=txtNoOfClass.ClientID %>').val('');
                $('#<%=chkIsGradePromotionFormulaDefault.ClientID %>').prop('checked', true);
                $('#<%=chkIsGradePromotionFormulaDefault.ClientID %>').change();

                $('.chkIsCurriculumFinalMarkDefault input').each(function () {
                    $(this).prop('checked', true);
                    $(this).change();
                });
                cboGrade.SetValue('');
                cboGrade.SetEnabled(true);
                cboGradePromotionFormula.SetEnabled(false);
                cboClassType.SetEnabled(true);
                cboDailySchedulePackage.SetEnabled(true);
                $('#<%=txtNoOfClass.ClientID %>').removeAttr('readonly');
                $('#entryDetailContainer').show();
            });

            $('#<%=chkIsGradePromotionFormulaDefault.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    cboGradePromotionFormula.SetEnabled(false);
                    cboGradePromotionFormula.SetValue('');
                }
                else
                    cboGradePromotionFormula.SetEnabled(true);
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    var result = '';
                    $('.hdnCurriculumMarkTypeID').each(function () {
                        $tr = $(this).parent();
                        var idx = $tr.find('.hdnItemIndex').val();
                        var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
                        var formulaID = '';
                        if (cboCurriculumFinalMarkFormulaID.GetValue() != null)
                            formulaID = cboCurriculumFinalMarkFormulaID.GetValue();
                        if (result != '')
                            result += '|';
                        result += $tr.find('.hdnCurriculumMarkTypeID').val() + ';' + formulaID;
                    });
                    $('#<%=hdnSaveValue.ClientID %>').val(result);
                    cbpProcess.PerformCallback('save');
                }
            });

            $('#btnDailySchedulePackageDt').click(function () {
                var schedulePackage = cboDailySchedulePackage.GetValue();
                if (schedulePackage != null && cboDailySchedulePackage != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
                    openUserControlPopup(url, schedulePackage, 'Jadwal', 1000, 550);
                }
            });

            $('#btnGradePromotionFormula').click(function () {
                var id = cboGradePromotionFormula.GetValue();
                if (id != null && id != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/GradePromotionFormulaDtCtl.ascx");
                    openUserControlPopup(url, id, 'Detil Formula', 900, 400);
                }
            });
        });

        $('.btnCurriculumFinalMarkFormulaDt').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
            var id = cboCurriculumFinalMarkFormulaID.GetValue();
            if (id != null && id != '') {
                var url = ResolveUrl("~/Program/Master/SchoolPeriod/CurriculumFinalMarkFormulaDtCtl.ascx");
                openUserControlPopup(url, id, 'Detil Formula', 900, 400);
            }
        });

        $('.chkIsCurriculumFinalMarkDefault input').live('change', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);

            if ($(this).is(':checked')) {
                cboCurriculumFinalMarkFormulaID.SetEnabled(false);
                cboCurriculumFinalMarkFormulaID.SetValue('');
            }
            else
                cboCurriculumFinalMarkFormulaID.SetEnabled(true);
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodClassTypeID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodClassTypeID);
            cboClassType.SetValue(entity.CurriculumClassTypeID);
            cboDailySchedulePackage.SetValue(entity.DailySchedulePackageID);

            if (entity.IsAllowEditItem == 'True') {
                cboClassType.SetEnabled(true);
                cboDailySchedulePackage.SetEnabled(true);
                $('#<%=txtNoOfClass.ClientID %>').removeAttr('readonly');
            }
            else {
                //cboClassType.SetEnabled(false);
                cboDailySchedulePackage.SetEnabled(false);
                $('#<%=txtNoOfClass.ClientID %>').attr('readonly', 'readonly');
            }

            if (entity.GradePromotionFormulaID == 0) {
                $('#<%=chkIsGradePromotionFormulaDefault.ClientID %>').prop('checked', true);
                cboGradePromotionFormula.SetValue('');
                cboGradePromotionFormula.SetEnabled(false);
            }
            else {
                cboGradePromotionFormula.SetValue(entity.GradePromotionFormulaID);
                cboGradePromotionFormula.SetEnabled(true);
                $('#<%=chkIsGradePromotionFormulaDefault.ClientID %>').prop('checked', false);
            }

            $('#<%=txtNoOfClass.ClientID %>').val(entity.NoOfClass);

            var filterExpression = "PeriodClassTypeID = " + entity.PeriodClassTypeID;
            Methods.getListObject('GetPeriodClassTypeFinalMarkFormulaList', filterExpression, function (result) {
                $('.hdnCurriculumMarkTypeID').each(function () {
                    $tr = $(this).closest('tr');
                    var idx = $tr.find('.hdnItemIndex').val();
                    var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
                    $chk = $tr.find('.chkIsCurriculumFinalMarkDefault input');
                    var isFound = false;
                    var curriculumMarkTypeID = $(this).val();
                    for (var i = 0; i < result.length; ++i) {
                        if (result[i].CurriculumMarkTypeID == curriculumMarkTypeID) {
                            isFound = true;
                            if (result[i].CurriculumFinalMarkFormulaID == null) {
                                $chk.prop('checked', true);
                                $chk.change();
                            }
                            else {
                                $chk.prop('checked', false);
                                $chk.change();
                                cboCurriculumFinalMarkFormulaID.SetValue(result[i].CurriculumFinalMarkFormulaID);
                            }
                        }
                    }
                    if (!isFound) {
                        $chk.prop('checked', true);
                        $chk.change();
                    }
                });
                $('#entryDetailContainer').show();
            });
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

        $('.lnkSchedule').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
            openUserControlPopup(url, entity.DailySchedulePackageID, 'Jadwal', 1000, 550);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" value="" />
    <input type="hidden" id="hdnGCSchoolType" runat="server" value="" />
    <input type="hidden" id="hdnListCurriculumID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedCurriculumID" runat="server" value="" />
    <input type="hidden" id="hdnPeriodCurriculumID" runat="server" value="" />
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 210px" />
                                    <col style="width: 300px" />
                                    <col style="width: 40px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                                    <td><dxe:ASPxComboBox runat="server" ID="cboClassType" ClientInstanceName="cboClassType" Width="300px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal")%></label></td>
                                    <td><dxe:ASPxComboBox runat="server" ID="cboDailySchedulePackage" ClientInstanceName="cboDailySchedulePackage" Width="300px" /></td>
                                    <td><input type="button" id="btnDailySchedulePackageDt" class="btnMore" value="..." /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Formula Kenaikan Kelas")%></label></td>
                                    <td><dxe:ASPxComboBox runat="server" ID="cboGradePromotionFormula" ClientInstanceName="cboGradePromotionFormula" Width="300px" /></td>
                                    <td><input type="button" id="btnGradePromotionFormula" class="btnMore" value="..." /></td>
                                    <td><asp:CheckBox ID="chkIsGradePromotionFormulaDefault" runat="server" /><%=GetLabel("Default") %></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah Kelas")%></label></td>
                                    <td><asp:TextBox ID="txtNoOfClass" CssClass="number" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3"><h4><%=GetLabel("Formula Nilai Rapor") %></h4></td>
                                </tr>   
                                <asp:Repeater ID="rptFinalMarkFormula" runat="server" OnItemDataBound="rptFinalMarkFormula_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("CurriculumMarkTypeName")%></label></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                <input type="hidden" class="hdnCurriculumMarkTypeID" value='<%#Eval("CurriculumMarkTypeID") %>' />
                                                <dxe:ASPxComboBox ID="cboCurriculumFinalMarkFormulaID" runat="server" Width="100%" />
                                            </td>
                                            <td><input type="button" class="btnCurriculumFinalMarkFormulaDt btnMore" value="..." /></td>
                                            <td><asp:CheckBox ID="chkIsCurriculumFinalMarkDefault" CssClass="chkIsCurriculumFinalMarkDefault" runat="server" /><%=GetLabel("Default") %></td>
                                        </tr>  
                                    </ItemTemplate>
                                </asp:Repeater>    
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
                                <asp:BoundField DataField="PeriodClassTypeID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="CurriculumClassTypeName" HeaderText="Tipe Kelas"/>
                                <asp:BoundField DataField="Grade" HeaderText="Tingkat" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="CurriculumMajorName" HeaderText="Jurusan" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderText="Tipe Jadwal" HeaderStyle-Width="300px">
                                    <ItemTemplate>
                                        <a class="lnkSchedule"><%#Eval("DailySchedulePackageName")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NoOfClass" HeaderText="Jumlah Kelas" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("PeriodClassTypeID") %>" bindingfield="PeriodClassTypeID" />
                                        <input type="hidden" value="<%#Eval("CurriculumClassTypeID") %>" bindingfield="CurriculumClassTypeID" />
                                        <input type="hidden" value="<%#Eval("DailySchedulePackageID") %>" bindingfield="DailySchedulePackageID" />
                                        <input type="hidden" value="<%#Eval("GradePromotionFormulaID") %>" bindingfield="GradePromotionFormulaID" />
                                        <input type="hidden" value="<%#Eval("NoOfClass") %>" bindingfield="NoOfClass" />
                                        <input type="hidden" value="<%#Eval("IsAllowEditItem") %>" bindingfield="IsAllowEditItem" />
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