<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="TeacherMarkEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.TeacherMarkEntry" %>

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
        //#region SchoolPeriod
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "<%=OnGetSchoolPeriodFilterExpression() %>";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                    }
                });
            });
        }
        //#endregion

        //#region PeriodSection
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "1 = 0";
            if (tacSchoolPeriod.getValue() != '') {
                filterExpression = "GCPeriodSectionStatus = '<%=OnSchoolPeriodStatusStart() %>' AND SchoolPeriodID = " + tacSchoolPeriod.getValue();
            }
            return filterExpression;
        }

        function onTacPeriodSectionButtonSearchClick() {
            openSearchDialog('periodsection', onGetPeriodSectionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodSectionFilterExpression() + " AND PeriodSectionCode = '" + value + "'";
                Methods.getObject('GetvPeriodSectionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodSection.setValue(result.PeriodSectionID);
                        $('#<%=hdnStartDate.ClientID %>').val(result.StartDateInDatePickerFormat);
                        $('#<%=hdnEndDate.ClientID %>').val(result.EndDateInDatePickerFormat);
                        tacPeriodSection.setText(result.PeriodSectionName);
                    }
                    else {
                        tacPeriodSection.setValue('');
                        $('#<%=hdnStartDate.ClientID %>').val('');
                        $('#<%=hdnEndDate.ClientID %>').val('');
                        tacPeriodSection.setText('');
                    }
                });
            });
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnStartDate" runat="server" value="" />
    <input type="hidden" id="hdnEndDate" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col width="50%"/>
            <col width="50%"/>
        </colgroup>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Ajaran")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolPeriodList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                                SearchFields="SchoolPeriodCode" TextField="SchoolPeriodCode" ValueField="ID" SearchText="${SchoolPeriodCode}" OrderByExpression="SchoolPeriodCode">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetvPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                                SearchFields="PeriodSectionCode" TextField="SchoolSection Code" ValueField="ID" SearchText="${PeriodSectionCode}" OrderByExpression="PeriodSectionCode">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bulan")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Akhir")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtFinalMark" CssClass="number" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
