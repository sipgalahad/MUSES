<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="BudgetEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.BudgetEntry" %>

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

        //#region Project
        function onGetBudgetFilterExpression() {
            var filterExpression = "<%=OnGetBudgetFilterExpression() %>";
            if ($('#<%=hdnID.ClientID %>').val() != '')
                filterExpression += " AND BudgetID != " + $('#<%=hdnID.ClientID %>').val();
            return filterExpression;
        }

        function onTacBudgetButtonSearchClick() {
            openSearchDialog('projectbudgethd', onGetBudgetFilterExpression(), function (value) {
                var filterExpression = onGetBudgetFilterExpression() + " AND BudgetCode = '" + value + "'";
                Methods.getObject('GetvProjectBudgetHdList', filterExpression, function (result) {
                    if (result != null) {
                        tacParent.setValue(result.BudgetID);
                        tacParent.setText(result.BudgetName);
                        entityToControlBudget(result);
                    }
                    else {
                        tacParent.setValue('');
                        tacParent.setText('');
                        entityToControlBudget(null);
                    }
                });
            });
        }

        function onTacBudgetValueChanged() {
            var id = tacParent.getValue();
            if (id != '') {
                var filterExpression = "BudgetID = " + id;
                Methods.getObject('GetvProjectBudgetHdList', filterExpression, function (result) {
                    if (result != null) {
                        entityToControlBudget(result);
                    }
                    else
                        entityToControlBudget(null);
                });
            } else {
                entityToControlBudget(null);
            }
        }

        function entityToControlBudget(result) {
            if (result != null) {
                $('#<%=hdnBudgetLevel.ClientID %>').val(result.BudgetLevel);
                $('#<%=hdnParentID.ClientID %>').val(result.BudgetID);
            }

            else {
                $('#<%=hdnBudgetLevel.ClientID %>').val(null);
                $('#<%=hdnParentID.ClientID %>').val(null);
            }

        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnBudgetLevel" runat="server" value="" />
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Anggaran")%></label></td>
                            <td><asp:TextBox ID="txtBudgetCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Anggaran")%></label></td>
                            <td><asp:TextBox ID="txtBudgetName" Width="220px" runat="server" /></td>
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Parent")%></label></td>
                            <td>
                                <input type="hidden" id="hdnParentID" runat="server" value="" />
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacParent" ClientInstanceName="tacParent" MethodName="GetvProjectBudgetHdList" GetFilterExpressionFunction="onGetProjectBudgetHdFilterExpression"
                                    SearchFields="BudgetName,BudgetCode" TextField="BudgetName" ValueField="BudgetID" SearchText="${BudgetName} (<b>${BudgetCode}</b>)" OrderByExpression="BudgetName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacBudgetButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacBudgetValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:CheckBox ID="chkIsHeader" runat="server" /><%=GetLabel("Header")%></td>
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

