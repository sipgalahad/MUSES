<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="ProjectEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectEntry" %>

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
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');
        });

        //#region Employee
        function onGetEmployeeFilterExpression() {
            var filterExpression = "<%=OnGetEmployeeFilterExpression() %>";
            return filterExpression;
        }

        function onTacEmployeeButtonSearchClick() {
            openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
                var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
                Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        tacPIC.setValue(result.EmployeeID);
                        tacPIC.setText(result.EmployeeName);
                    }
                    else {
                        tacPIC.setValue('');
                        tacPIC.setText('');
                    }
                });
            });
        }

        function onTacEmployeeValueChanged() {
        }
        //#endregion

        //#region Project
        function onGetProjectFilterExpression() {
            var filterExpression = "<%=OnGetProjectFilterExpression() %>";
            if ($('#<%=hdnID.ClientID %>').val() != '')
                filterExpression += " AND ProjectID != " + $('#<%=hdnID.ClientID %>').val();
            return filterExpression;
        }

        function onTacProjectButtonSearchClick() {
            openSearchDialog('project', onGetProjectFilterExpression(), function (value) {
                var filterExpression = onGetProjectFilterExpression() + " AND ProjectCode = '" + value + "'";
                Methods.getObject('GetvProjectList', filterExpression, function (result) {
                    if (result != null) {
                        tacParent.setValue(result.ProjectID);
                        tacParent.setText(result.ProjectName);
                        $('#<%=hdnParentID.ClientID %>').val(result.ProjectID);
                    }
                    else {
                        tacParent.setValue('');
                        tacParent.setText('');
                        $('#<%=hdnParentID.ClientID %>').val('');
                    }
                });
            });
        }

        function onTacProjectValueChanged() {
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea" >
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top" rowspan="2">
                <div>
                    <table class="tblEntryContent" style="width:50%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Proyek")%></label></td>
                            <td><asp:TextBox ID="txtProjectCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Proyek")%></label></td>
                            <td><asp:TextBox ID="txtProjectName" Width="220px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><asp:TextBox ID="txtStartDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                        <td style="width:40px; text-align:center">s/d</td>
                                        <td><asp:TextBox ID="txtEndDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Penanggung Jawab")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPIC" ClientInstanceName="tacPIC" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                    SearchFields="EmployeeName,EmployeeCode" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacEmployeeValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Parent")%></label></td>
                            <td>
                                <input type="hidden" id="hdnParentID" runat="server" value="" />
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacParent" ClientInstanceName="tacParent" MethodName="GetvProjectList" GetFilterExpressionFunction="onGetProjectFilterExpression"
                                    SearchFields="ProjectName,ProjectCode" TextField="ProjectName" ValueField="ProjectID" SearchText="${ProjectName} (<b>${ProjectCode}</b>)" OrderByExpression="ProjectName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacProjectButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacProjectValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:CheckBox ID="chkIsHeader" runat="server" /><%=GetLabel("Header")%></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Indikator Kinerja")%></label></td>
                            <td><asp:TextBox ID="txtProjectIndicator" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Target / Sasaran")%></label></td>
                            <td><asp:TextBox ID="txtProjectTarget" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

