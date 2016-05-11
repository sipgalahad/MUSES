<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RProjectTaskGroupCopyEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectTaskGroupCopyEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $('#<%=grdView.ClientID %> tr:gt(0)').die('click');
    $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
        $row = $(this);
        var entity = rowToObject($row);
        $('#<%=hdnCopyProjectTaskGroupID.ClientID %>').val(entity.ProjectTaskGroupID);
        $('#<%=txtProjectTaskGroupName.ClientID %>').val(entity.ProjectTaskGroupName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

        $('#<%=grdView.ClientID %> tr.selected').addClass('selected');
        $(this).addClass('selected');
    });

    $('#btnSavePopup').click(function () {
        cbpProcessPopup.PerformCallback('save');
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                cbpView2.PerformCallback('refresh');
                pcRightPanelContent.Hide();
            }
        }
    }
</script>

<div style="height:380px; overflow-y:auto">
    <input type="hidden" id="hdnOrganizationCoordinatorID" value="" runat="server" />
    <input type="hidden" id="hdnCopyProjectTaskGroupID" value="" runat="server" />
    <table style="width:100%">
        <tr>
            <td style="width:50%; vertical-align: top" >
                <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                    ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ hideLoadingPanel(); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectTaskGroupID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="ProjectTaskGroupName" HeaderText="Kelompok Tugas" />
                                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input type="hidden" value="<%#Eval("ProjectTaskGroupID") %>" bindingfield="ProjectTaskGroupID" />
                                                <input type="hidden" value="<%#Eval("ProjectTaskGroupName") %>" bindingfield="ProjectTaskGroupName" />
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
            </td>
            <td style="vertical-align: top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelompok Tugas") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtProjectTaskGroupName" Width="300px" /></td>
                    </tr>
                    <tr valign="top" style="padding-top: 5px">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><input type="button" id="btnSavePopup" value='<%=GetLabel("Save") %>' /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

