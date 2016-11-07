<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="SchoolPeriodEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolPeriodEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#btnDailySchedulePackageDt').click(function () {
                var schedulePackage = cboDailySchedulePackage.GetValue();
                if (schedulePackage != null && schedulePackage != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
                    openUserControlPopup(url, schedulePackage, 'Jadwal', 1000, 550);
                }
            });

            $('#btnExamSchedulePackageDt').click(function () {
                var schedulePackage = cboExamSchedulePackage.GetValue();
                if (schedulePackage != null && schedulePackage != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/PeriodClassType/DailySchedulePackageDtCtl.ascx");
                    openUserControlPopup(url, schedulePackage, 'Jadwal', 1000, 550);
                }
            });

            $('#btnGradePromotionFormula').click(function () {
                var id = cboGradePromotionFormula.GetValue();
                if (id != null && id != '') {
                    var url = ResolveUrl("~/Program/Master/SchoolPeriod/GradePromotionFormulaDtCtl.ascx");
                    openUserControlPopup(url, id, 'Detil Formula', 950, 400);
                }
            });
        }

        $('.btnCurriculumFinalMarkFormulaDt').live('click', function () {
            $tr = $(this).closest('tr');
            var idx = $tr.find('.hdnItemIndex').val();
            var cboCurriculumFinalMarkFormulaID = eval('cboCurriculumFinalMarkFormulaID' + idx);
            var id = cboCurriculumFinalMarkFormulaID.GetValue();
            if (id != null && id != '') {
                var url = ResolveUrl("~/Program/Master/SchoolPeriod/CurriculumFinalMarkFormulaDtCtl.ascx");
                openUserControlPopup(url, id, 'Detil Formula', 900, 400);
            }
        }); 

        function onCboCurriculumValueChanged() {
            cbpFinalMarkFormula.PerformCallback();
            cboGradePromotionFormula.PerformCallback();
        }

        function onCboCopySchoolPeriodChanged() {
            if (cboCopySchoolPeriod.GetValue() != "") {
                var filterExpression = "SchoolPeriodID = " + cboCopySchoolPeriod.GetValue();
                Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        cboCurriculum.SetValue(result.CurriculumID);
                        cboDailySchedulePackage.SetValue(result.DailySchedulePackageID);
                        cboExamSchedulePackage.SetValue(result.ExamSchedulePackageID);
                        cboGradePromotionFormula.SetValue(result.GradePromotionFormulaID);
                        onCboCurriculumValueChanged();
                    }
                });
            }
        }

        function onBeforeSaveRecord() {
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
            return true;
        }

        function onBeforeGoToListPage(mapForm) {
            mapForm.appendChild(createInputHiddenPost("siteID", $('#<%=hdnSiteID.ClientID %>').val()));
        }
    </script>
    <input type="hidden" id="hdnSiteID" runat="server" value="" />
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSaveValue" runat="server" value="" />
    <input type="hidden" id="hdnGCSchoolType" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:210px"/>
                        <col style="width:300px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtSchoolPeriodCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtSchoolPeriodName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Selesai")%></label></td>
                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr id="trCopySchoolPeriod" runat="server">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Copy Tahun Ajaran")%></label></td>
                        <td>
                            <dxe:ASPxComboBox runat="server" ID="cboCopySchoolPeriod" ClientInstanceName="cboCopySchoolPeriod" Width="300px">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboCopySchoolPeriodChanged() }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kurikulum")%></label></td>
                        <td>
                            <dxe:ASPxComboBox runat="server" ID="cboCurriculum" ClientInstanceName="cboCurriculum" Width="300px">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboCurriculumValueChanged(); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboDailySchedulePackage" ClientInstanceName="cboDailySchedulePackage" Width="300px" /></td>
                        <td><input type="button" id="btnDailySchedulePackageDt" class="btnMore" value="..." /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal Ujian")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboExamSchedulePackage" ClientInstanceName="cboExamSchedulePackage" Width="300px" /></td>
                        <td><input type="button" id="btnExamSchedulePackageDt" class="btnMore" value="..." /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Formula Kenaikan Kelas")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboGradePromotionFormula" ClientInstanceName="cboGradePromotionFormula" Width="300px" OnCallback="cboGradePromotionFormula_Callback" /></td>
                        <td><input type="button" id="btnGradePromotionFormula" class="btnMore" value="..." /></td>
                    </tr>
                    <tr>
                        <td colspan="3"><h4><%=GetLabel("Formula Nilai Rapor") %></h4></td>
                    </tr>                     
                </table>
                <dxcp:ASPxCallbackPanel ID="cbpFinalMarkFormula" runat="server" Width="100%" ClientInstanceName="cbpFinalMarkFormula"
                    ShowLoadingPanel="false" OnCallback="cbpFinalMarkFormula_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView">
                                <asp:Repeater ID="rptFinalMarkFormula" runat="server" OnItemDataBound="rptFinalMarkFormula_ItemDataBound">
                                    <ItemTemplate>
                                        <table class="tblEntryContent" style="width:50%">
                                            <colgroup>
                                                <col style="width:210px"/>
                                                <col style="width:300px"/>
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%#Eval("CurriculumMarkTypeName")%></label></td>
                                                <td>
                                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                    <input type="hidden" class="hdnCurriculumMarkTypeID" value='<%#Eval("CurriculumMarkTypeID") %>' />
                                                    <dxe:ASPxComboBox ID="cboCurriculumFinalMarkFormulaID" runat="server" Width="100%" />
                                                </td>
                                                <td><input type="button" class="btnCurriculumFinalMarkFormulaDt btnMore" value="..." /></td>
                                            </tr>                     
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>  
            </td>
        </tr>
    </table>
</asp:Content>
