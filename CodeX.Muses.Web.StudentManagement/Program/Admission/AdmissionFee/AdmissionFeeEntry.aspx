<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="AdmissionFeeEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.AdmissionFeeEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Simpan")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region Registration
        function onGetRegistrationFilterExpression() {
            var filterExpression = "<%=OnGetRegistrationFilterExpression() %>";
            return filterExpression;
        }

        function onTacRegistrationButtonSearchClick() {
            openSearchDialog('registration', onGetRegistrationFilterExpression(), function (value) {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    if (result != null) {
                        tacRegistration.setValue(result.RegistrationID);
                        tacRegistration.setText(result.ProspectiveStudentName);
                        if (result.IsFeeder)
                            $('#<%=hdnIsFeeder.ClientID %>').val('1');
                        else
                            $('#<%=hdnIsFeeder.ClientID %>').val('0');
                    }
                    else {
                        tacRegistration.setValue('');
                        tacRegistration.setText('');
                        $('#<%=hdnIsFeeder.ClientID %>').val('0');
                    }
                });
            });

        }

        function onTacRegistrationValueChanged() {
            var id = tacRegistration.getValue();
            if (id != '') {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    if (result.IsFeeder)
                        $('#<%=hdnIsFeeder.ClientID %>').val('1');
                    else
                        $('#<%=hdnIsFeeder.ClientID %>').val('0');
                });
            }
        }
        //#endregion

        //#region Admission Fee Rule
        function onGetAdmissionFeeRuleFilterExpression() {
            var filterExpression = "<%=OnGetAdmissionFeeRuleFilterExpression() %>";
            if ($('#<%=hdnIsFeeder.ClientID %>').val() == '1')
                filterExpression += "<%=OnGetAdmissionFeeRuleFeederFilterExpression() %>";
            else
                filterExpression += "<%=OnGetAdmissionFeeRuleNonFeederFilterExpression() %>";
            return filterExpression;
        }

        function onTacAdmissionFeeRuleButtonSearchClick() {
            openSearchDialog('admissionfeerule', onGetAdmissionFeeRuleFilterExpression(), function (value) {
                var filterExpression = onGetAdmissionFeeRuleFilterExpression() + " AND AdmissionFeeRuleID = '" + value + "'";
                Methods.getObject('GetAdmissionFeeRuleHdList', filterExpression, function (result) {
                    if (result != null) {
                        tacAdmissionFeeRule.setValue(result.AdmissionFeeRuleID);
                        tacAdmissionFeeRule.setText(result.AdmissionFeeRuleName);
                    }
                    else {
                        tacAdmissionFeeRule.setValue('');
                        tacAdmissionFeeRule.setText('');
                    }
                });
            });

        }

        function onTacAdmissionFeeRuleValueChanged() {
        }
        //#endregion
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnSchoolPeriodID" value="0" runat="server" />
    <div>
        <table>
            <colgroup>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Calon Siswa")%></label></td>
                <td>
                    <input type="hidden" id="hdnIsFeeder" runat="server" />
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRegistration" ClientInstanceName="tacRegistration" MethodName="GetvRegistrationList" GetFilterExpressionFunction="onGetRegistrationFilterExpression"
                        SearchFields="ProspectiveStudentName,RegistrationNo" TextField="ProspectiveStudentName" ValueField="RegistrationID" SearchText="${ProspectiveStudentName} (<b>${RegistrationNo}</b>)" OrderByExpression="ProspectiveStudentName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacRegistrationButtonSearchClick(); }"
                            ValueChanged="function(){ onTacRegistrationValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Biaya Siswa")%></label></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacAdmissionFeeRule" ClientInstanceName="tacAdmissionFeeRule" MethodName="GetAdmissionFeeRuleHdList" GetFilterExpressionFunction="onGetAdmissionFeeRuleFilterExpression"
                        SearchFields="AdmissionFeeRuleName" TextField="AdmissionFeeRuleName" ValueField="RegistrationID" SearchText="${AdmissionFeeRuleName}" OrderByExpression="AdmissionFeeRuleName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacAdmissionFeeRuleButtonSearchClick(); }"
                            ValueChanged="function(){ onTacAdmissionFeeRuleValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Beasiswa") %></td>
                <td><dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="150px" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Cara Pembayaran") %></td>
                <td><dxe:ASPxComboBox ID="cboPaymentType" runat="server" Width="150px" /></td>
            </tr>
            <tr>
                <td class="tdLabel">&nbsp;</td>
                <td><input type="button" id="btnGenerate" value='<%=GetLabel("Generate") %>' /></td>
            </tr>
        </table>
    </div>
</asp:Content>