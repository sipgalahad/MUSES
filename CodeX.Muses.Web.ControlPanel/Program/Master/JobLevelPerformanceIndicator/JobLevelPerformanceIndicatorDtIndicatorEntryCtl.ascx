<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobLevelPerformanceIndicatorDtIndicatorEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.JobLevelPerformanceIndicatorDtIndicatorEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    <%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#entryDetailContainerPopup').show();

            tacJobLevelID.setValue('');
            tacJobLevelID.setText('');
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
                cbpProcessPopup.PerformCallback('save');
        });
    });

    

    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation('Are You Sure Want To Delete?', function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.JobLevelID);
                cbpProcess.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.JobLevelPerformanceIndicatorID);
        $('#entryDetailContainerPopup').show();
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }

    //#region Organization Position Get Employee



    //#region PerFormanceIndicatorDt
    function onGetJobLevelFilterExpression() {
        var filterExpression = " IsDeleted = 0 ";
        return filterExpression;
    }

    function ontacJobLevelIDSearchClick() {
        openSearchDialog('performanceindicatordt', onGetJobLevelFilterExpression(), function (value) {
            var filterExpression = onGetJobLevelFilterExpression() + " AND PerformanceIndicatorID = '" + value + "'";
            Methods.getObject('GetPerformanceIndicatorDtList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnInput.ClientID %>').val(result.PerformanceIndicatorID);
                    tacJobLevelID.setValue(result.PerformanceIndicatorDtID);
                    tacJobLevelID.setText(result.PerformanceIndicatorDtName);
                }
                else {
                    tacJobLevelID.setValue('');
                    tacJobLevelID.setText('');
                }
            });
        });
    }

    function ontacJobLevelIDValueChanged() {
    }

    //#endregion
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnInput" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory" id="lblPosition"><%=GetLabel("Performance")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacJobLevelID" ClientInstanceName="tacJobLevelID" MethodName="GetPerformanceIndicatorDtList" GetFilterExpressionFunction="onGetJobLevelFilterExpression"
                                SearchFields="PerformanceIndicatorDtName,PerformanceIndicatorDtID" TextField="PerformanceIndicatorDtName" ValueField="PerformanceIndicatorDtID" SearchText="${PerformanceIndicatorDtName} (<b>${DisplayOrder}</b>)" OrderByExpression="PerformanceIndicatorDtName">
                                <ClientSideEvents ButtonSearchClick="function(){ ontacJobLevelIDSearchClick(); }"
                                    ValueChanged="function(){ ontacJobLevelIDValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                     
                    <tr>
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="PerformanceIndicatorDtName" HeaderText="Nama" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                   <%-- <<div style='float:right;<%#IsEditable().ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>--%>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <input type="hidden" value="<%#Eval("PerformanceIndicatorDtID") %>" bindingfield="PerformanceIndicatorDtID" />
                                    <input type="hidden" value="<%#Eval("PerformanceIndicatorDtName") %>" bindingfield="PerformanceIndicatorDtName" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

