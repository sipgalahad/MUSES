<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="OrganizationStructureCompEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.OrganizationStructureCompEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#lblParent').click(function () {
                openSearchDialog('OrganizationDepartment', 'IsDeleted = 0', function (value) {
                    alert(value);
                });
            });
        });

        //#region Organization Department
        function onGetOrganizationDepartmentFilterExpression() {
            var filterExpression = "IsHeader = 1 AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacOrganizationDepartmentParentIDSearchClick() {
            openSearchDialog('OrganizationDepartment', onGetOrganizationDepartmentFilterExpression(), function (value) {
                var filterExpression = onGetOrganizationDepartmentFilterExpression() + " AND OrganizationDepartmentCode = '" + value + "'";
                Methods.getObject('GetvOrganizationDepartmentList', filterExpression, function (result) {
                    if (result != null) {
                        tacOrganizationDepartmentParentID.setValue(result.OrganizationDepartmentID);
                        tacOrganizationDepartmentParentID.setText(result.OrganizationDepartmentName);
                    }
                    else {
                        tacOrganizationDepartmentParentID.setValue('');
                        tacOrganizationDepartmentParentID.setText('');
                    }
                });
            });

        }

        function onTacOrganizationDepartmentParentIDValueChanged() {
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtOrganizationDepartmentCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtOrganizationDepartmentName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                         <td class="tdLabel"><label class="lblNormal" id="lblParent"><%=GetLabel("Parent")%></label></td>
                         <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacOrganizationDepartmentParentID" ClientInstanceName="tacOrganizationDepartmentParentID" MethodName="GetvOrganizationDepartmentList" GetFilterExpressionFunction="onGetOrganizationDepartmentFilterExpression"
                                SearchFields="OrganizationDepartmentName,OrganizationDepartmentCode" TextField="OrganizationDepartmentName" ValueField="OrganizationDepartmentID" SearchText="${OrganizationDepartmentName} (<b>${OrganizationDepartmentCode}</b>)" OrderByExpression="OrganizationDepartmentName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacOrganizationDepartmentParentIDSearchClick(); }"
                                    ValueChanged="function(){ onTacOrganizationDepartmentParentIDValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                     <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsHeader" Text="Is Header" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
