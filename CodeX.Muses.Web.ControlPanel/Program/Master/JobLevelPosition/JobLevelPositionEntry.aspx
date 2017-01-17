<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="JobLevelPositionEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.JobLevelPositionEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
    <%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region Organization Position
        function onGetOrganizationPositionFilterExpression() {
            var filterExpression = "IsDeleted = 0 ";
            return filterExpression;
        }

        function onTacOrganizationPositionIDSearchClick() {
            openSearchDialog('OrganizationPosition', onGetOrganizationPositionFilterExpression(), function (value) {
                var filterExpression = onGetOrganizationPositionFilterExpression() + " AND OrganizationPositionID = '" + value + "'";
                Methods.getObject('GetvOrganizationPositionList', filterExpression, function (result) {
                    if (result != null) {
                        tacOrganizationPosition.setValue(result.OrganizationPositionID);
                        tacOrganizationPosition.setText(result.OrganizationPositionName);
                    }
                    else {
                        tacOrganizationPosition.setValue('');
                        tacOrganizationPosition.setText('');
                    }
                });
            });
        }

        function onTacOrganizationPositionIDValueChanged() {
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jabatan")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacOrganizationPosition" ClientInstanceName="tacOrganizationPosition" MethodName="GetvOrganizationPositionList" GetFilterExpressionFunction="onGetOrganizationPositionFilterExpression"
                                            SearchFields="OrganizationPositionName,OrganizationPositionID" TextField="OrganizationPositionName" ValueField="OrganizationPositionID" SearchText="${OrganizationPositionName} (<b>${PositionLevel}</b>)" OrderByExpression="OrganizationPositionName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacOrganizationPositionIDSearchClick(); }"
                                                ValueChanged="function(){ onTacOrganizationPositionIDValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>      
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tingkat")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboJobLevelType" Width="200px" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
