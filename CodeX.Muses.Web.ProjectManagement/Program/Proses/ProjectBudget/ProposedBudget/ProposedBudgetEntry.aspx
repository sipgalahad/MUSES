<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProposedBudgetEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProposedBudgetEntry" %>
    
<%@ Register Src="~/Program/Proses/ProjectBudget/ProposedBudget/BudgetCtl.ascx" TagName="BudgetCtl" TagPrefix="uc1" %>
<%@ Register Src="~/Program/Proses/ProjectBudget/ProposedBudget/InfrastructureBudgetCtl.ascx" TagName="InfrastructureCtl" TagPrefix="uc1" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
        
        .grdFund th     { background-color: #EEE; color: Black; border:1px solid #D5D5D5; font-weight: bolder; padding: 5px;}
        
    </style>
        
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
            }
            else {
                $('#divTransactionAdd').hide();
            }

            setDatePicker('<%=txtProposedBudgetDate.ClientID %>');
            
            $('#ulTabClinicTransaction li').click(function () {
                $('#ulTabClinicTransaction li.selected').removeAttr('class');
                $('.containerTransDt').filter(':visible').hide();
                $contentID = $(this).attr('contentid');
                $('#' + $contentID).show();
                $(this).addClass('selected');
            });

            onLoadBudget();
            onLoadInfrastructure();
        };
        
        //#region ProposedBudgetHd
        function onGetProposedBudgetHdFilterExpression() {
            var filterExpression = "<%=OnGetProposedBudgetHdFilterExpression() %>";
            return filterExpression;
        }

        $('#lblProposedBudgetNo.lblLink').die('click');
        $('#lblProposedBudgetNo.lblLink').live('click',function () {
            openSearchDialog('proposedbudgethd', onGetProposedBudgetHdFilterExpression(), function (value) {
                $('#<%=txtProposedBudgetNo.ClientID %>').val(value);
                ontxtProposedBudgetNoChanged(value);
            });
        });

        $('#<%=txtProposedBudgetNo.ClientID %>').die('change');
        $('#<%=txtProposedBudgetNo.ClientID %>').live('change',function () {
            ontxtProposedBudgetNoChanged($(this).val());
        });

        function ontxtProposedBudgetNoChanged(value) {
            onLoadObject(value);
        }
        //#endregion

        //#region TeamDt
        function onGetTeamDtFilterExpression() {
            var filterExpression = "<%=OnGetTeamDtFilterExpression()%>";
            if ($('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() != "0")
                filterExpression += " AND (EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID1 LIKE '%;" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + ";%')";
            return filterExpression;
        }

        function onTacTeamDtButtonSearchClick() {
            openSearchDialog('teamdt', onGetTeamDtFilterExpression(), function (value) {
                var filterExpression = onGetTeamDtFilterExpression() + " AND TeamDtID = " + value;
                Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeamDt.setValue(result.TeamDtID);
                        tacTeamDt.setText(result.Position);
                        entityToControlTeamDt(result);
                    }
                    else {
                        tacTeamDt.setValue('');
                        tacTeamDt.setText('');
                        entityToControlTeamDt(null);
                    }
                });
            });
        }

        function onTacTeamDtValueChanged() {
            var id = tacTeamDt.getValue();
            if (id != '') {
                var filterExpression = "TeamDtID = " + id;
                Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                    if (result != null)
                        entityToControlTeamDt(result);
                    else
                        entityToControlTeamDt(null);
                });
            } else {
                entityToControlTeamDt(null);
            }
        }

        function entityToControlTeamDt(result) {
            if (result != null)
                $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
            else
                $('#<%=hdnTeamDtID.ClientID %>').val(null);
        }
        //#endregion

        function onAfterSaveRecordDtSuccess(OrderID) {
            if ($('#<%=hdnID.ClientID %>').val() == '0') {
                $('#<%=hdnID.ClientID %>').val(OrderID);
                var filterExpression = 'ProposedBudgetID = ' + OrderID;
                Methods.getObject('GetProposedBudgetHdList', filterExpression, function (result) {
                    $('#<%=txtProposedBudgetNo.ClientID %>').val(result.ProposedBudgetNo);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnEntryID" runat="server" value="" />
    <input type="hidden" id="hdnLstFundItem" runat="server" value="" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value="0"/>
    <input type="hidden" value="0" id="hdnPrice" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td>
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblProposedBudgetNo"><%=GetLabel("No. Rancangan Anggaran")%></label></td>
                        <td><asp:TextBox ID="txtProposedBudgetNo" Width="150px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label id="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                        <td><asp:TextBox ID="txtProposedBudgetDate" CssClass="datepicker" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                        <td>
                            <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeamDt" ClientInstanceName="tacTeamDt" MethodName="GetvTeamDtList" GetFilterExpressionFunction="onGetTeamDtFilterExpression"
                                SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="<b>${Position}</b>" OrderByExpression="TeamDtID">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacTeamDtButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacTeamDtValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="tblEntryContent">
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="containerUlTabPage">
                    <ul class="ulTabPage" id="ulTabClinicTransaction">
                        <li class="selected" contentid="containerService"><%=GetLabel("Anggaran") %></li>
                        <li contentid="containerInfrastructure"><%=GetLabel("Sarana / Fasilitas") %></li>
                    </ul>
                </div>
                <div id="containerService" class="containerTransDt">
                    <uc1:BudgetCtl ID="ctlBudget" runat="server" />
                </div>
                <div id="containerInfrastructure" style="display:none" class="containerTransDt">
                    <uc1:InfrastructureCtl ID="ctlInfrastructure" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="right">
                <table>
                    <tr>
                        <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Total Diajukan")%></label></td>
                        <td></td>
                        <td><asp:TextBox ID="txtTotalProjectBudget" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>