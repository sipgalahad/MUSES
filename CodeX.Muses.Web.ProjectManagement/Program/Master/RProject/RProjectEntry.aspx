<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="RProjectEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectEntry" %>

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

        //#region Project Group
        function onGetProjectGroupFilterExpression() {
            var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacProjectGroupButtonSearchClick() {
            openSearchDialog('rprojectgroup', onGetProjectGroupFilterExpression(), function (value) {
                var filterExpression = onGetProjectGroupFilterExpression() + " AND ProjectGroupCode = '" + value + "'";
                Methods.getObject('GetvRProjectGroupList', filterExpression, function (result) {
                    if (result != null) {
                        tacProjectGroup.setValue(result.ProjectGroupID);
                        tacProjectGroup.setText(result.ProjectGroupName);
                    }
                    else {
                        tacProjectGroup.setValue('');
                        tacProjectGroup.setText('');
                    }
                });
            });
        }

        function onTacProjectGroupValueChanged() {
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
                            <col style="width:150px"/>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Project Group")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacProjectGroup" ClientInstanceName="tacProjectGroup" MethodName="GetvRProjectGroupList" GetFilterExpressionFunction="onGetRProjectGroupFilterExpression"
                                    SearchFields="ProjectGroupName,ProjectGroupCode" TextField="ProjectName" ValueField="ProjectGroupID" SearchText="${ProjectGroupName} (<b>${ProjectGroupCode}</b>)" OrderByExpression="ProjectGroupName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacProjectGroupButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacProjectGroupValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
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

