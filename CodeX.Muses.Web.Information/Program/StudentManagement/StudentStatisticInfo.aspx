<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentStatisticInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentStatisticInfo" %>

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
        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodClassType.setValue('');
            tacPeriodClassType.setText('');
            cbpView.PerformCallback('refresh');
        }

        //#region Period Class Type
        function onGetPeriodClassTypeFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacPeriodClassTypeButtonSearchClick() {
            openSearchDialog('periodclasstype', onGetPeriodClassTypeFilterExpression(), function (value) {
                var filterExpression = onGetPeriodClassTypeFilterExpression() + " AND CurriculumClassTypeCode = '" + value + "'";
                Methods.getObject('GetvPeriodClassTypeList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodClassType.setValue(result.PeriodClassTypeID);
                        tacPeriodClassType.setText(result.CurriculumClassTypeName);
                    }
                    else {
                        tacPeriodClassType.setValue('');
                        tacPeriodClassType.setText('');
                    }
                    onTacPeriodClassTypeValueChanged();
                });
            });

        }

        function onTacPeriodClassTypeValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        function onCbpViewEndCallback() {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.divClassTypeInformation').each(function () {
                $(this).attr('border', '1');
            });
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            $('#<%=hdnExportTitle.ClientID %>').val($('.hdnTempExportTitle').val());
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnExportControl" runat="server" />
    <input type="hidden" id="hdnExportTitle" runat="server" />
    <table>
        <colgroup>
            <col style="width: 120px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodClassType" ClientInstanceName="tacPeriodClassType" MethodName="GetvPeriodClassTypeList" GetFilterExpressionFunction="onGetPeriodClassTypeFilterExpression"
                    SearchFields="CurriculumClassTypeName,CurriculumClassTypeCode" TextField="PeriodClassTypeID" ValueField="SchoolClassID" SearchText="${CurriculumClassTypeName} (<b>${}</b>)" OrderByExpression="CurriculumClassTypeName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodClassTypeButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodClassTypeValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnTempExportTitle" class="hdnTempExportTitle" runat="server" />
                        <div id="divContainerView">
                            <asp:Repeater ID="rptPeriod" runat="server" OnItemDataBound="rptPeriod_ItemDataBound">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" rules="all" style="margin-bottom: 10px;">
                                        <td>
                                            <table cellpadding="0" cellspacing="0" rules="all" class="grdSelected grdBorder divClassTypeInformation">
                                                <tr>
                                                    <th rowspan="3" style="width: 30px" class="thCenter"><%=GetLabel("NO") %></th>
                                                    <th rowspan="3" style="width: 80px"><%=GetLabel("KELAS") %></th>
                                                    <th id="thPeriodName" runat="server" class="thCenter"></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 60px" class="thCenter" rowspan="2"><%=GetLabel("JML KLS") %></th>
                                                    <th class="thCenter" colspan="3"><%=GetLabel("JML SISWA") %></th>
                                                    <th class="thCenter" id="thReligion" runat="server"><%=GetLabel("AGAMA") %></th>
                                                </tr>
                                                <tr>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>

                                                    <asp:Repeater ID="rptReligion" runat="server">
                                                        <ItemTemplate>
                                                            <th class="thCenter" style="width: 40px"><%#Eval("TagProperty") %></th>    
                                                        </ItemTemplate>
                                                    </asp:Repeater>                                               
                                                </tr>
                                                <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%#Eval("CurriculumClassTypeName") %></td>
                                                            <td align="center"><%#Eval("NoOfClass") %></td>
                                                            <td id="tdStudentCount" runat="server" align="center"></td>
                                                            <td id="tdStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdStudentFemaleCount" runat="server" align="center"></td>
                                                            <asp:Repeater ID="rptStudentReligion" runat="server" OnItemDataBound="rptStudentReligion_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td id="tdReligion" runat="server" align="center"></td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>               
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </table>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
