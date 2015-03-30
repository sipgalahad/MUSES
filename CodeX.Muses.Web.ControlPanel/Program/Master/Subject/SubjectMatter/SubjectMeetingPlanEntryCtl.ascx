<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectMeetingPlanEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectMeetingPlanEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtMeetingNo.ClientID %>').val('');
            $('#<%=txtRemarks.ClientID %>').val('');
            cboGCPeriodSection.SetEnabled(false);

            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            cboGCPeriodSection.SetEnabled(true);
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) 
                cbpProcessPopup.PerformCallback('save');
        });
    });

    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectMeetingPlanHdID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectMeetingPlanHdID);
        $('#<%=txtMeetingNo.ClientID %>').val(entity.MeetingNo);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        cboGCPeriodSection.SetEnabled(false);

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

    function onCboGCPeriodSectionValueChanged() {
        var filterExpression = 'SubjectMatterID = ' + $('#<%=hdnID.ClientID %>').val() + " AND GCPeriodSection = '" + cboGCPeriodSection.GetValue() + "'";
        Methods.getObject('GetSubjectCompetencyStandardSummaryList', filterExpression, function (result) {
            $('#<%=txtHeaderText2.ClientID %>').val(result.SummaryName);
            cbpViewPopup.PerformCallback('refresh');
        });
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Materi")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
            <td colspan="2">
                <dxe:ASPxComboBox ID="cboGCPeriodSection" ClientInstanceName="cboGCPeriodSection" Width="200px" runat="server">
                    <ClientSideEvents ValueChanged="function(){ onCboGCPeriodSectionValueChanged(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>  
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Standar Kompetensi Utama")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText2" ReadOnly="true" Width="100%" runat="server" /></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Pertemuan Ke")%></label></td>
                        <td><asp:TextBox ID="txtMeetingNo" runat="server" Width="80px" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
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
                            <asp:BoundField DataField="MeetingNo" HeaderText="Pertemuan Ke" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("SubjectMeetingPlanHdID") %>" bindingfield="SubjectMeetingPlanHdID" />
                                    <input type="hidden" value="<%#Eval("MeetingNo") %>" bindingfield="MeetingNo" />
                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
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

