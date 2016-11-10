<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateEmployeeGroupPicksCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Inventory.Program.TemplateEmployeeGroupPicksCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    <%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    //#region Get Employee
    window.onGetTemplateEmployeeFilterExpression = function() {
        var filterExpression = "<%=OnGetTemplateEmployeeFilterExpression() %>";
        return filterExpression;
    }

    function ontacTemplateEmployeeSearchClick() {
        openSearchDialog('templateemployee', onGetTemplateEmployeeFilterExpression(), function (value) {
            var filterExpression = onGetTemplateEmployeeFilterExpression() + " AND TemplateCode = '" + value + "'";
            Methods.getObject('GetTemplateEmployeeGroupHdList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnTemplateID.ClientID %>').val(result.TemplateID);
                    tacTemplateEmployee.setValue(result.TemplateID);
                    tacTemplateEmployee.setText(result.TemplateName);
                }
                else {
                    $('#<%=hdnTemplateID.ClientID %>').val('');
                    tacTemplateEmployee.setValue('');
                    tacTemplateEmployee.setText('');
                }
                cbpViewPopup.PerformCallback('refresh');
            });
        });
    }

    function ontacTemplateEmployeeValueChanged() {
        $('#<%=hdnTemplateID.ClientID %>').val(tacTemplateEmployee.getValue());
        cbpViewPopup.PerformCallback('refresh');       
    }
    //#endregion

    
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnTemplateIDDelete" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelompok Karyawan")%></label></td>
            <td>
                <input type="hidden" id="hdnTemplateID" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTemplateEmployee" ClientInstanceName="tacTemplateEmployee" MethodName="GetTemplateEmployeeGroupHdList" GetFilterExpressionFunction="onGetTemplateEmployeeFilterExpression"
                    SearchFields="TemplateName,TemplateCode" TextField="TemplateName" ValueField="TemplateID" SearchText="${TemplateName} (<b>${TemplateCode}</b>)" OrderByExpression="TemplateName">
                    <ClientSideEvents ButtonSearchClick="function(){ ontacTemplateEmployeeSearchClick(); }"
                        ValueChanged="function(){ ontacTemplateEmployeeValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
    </table>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="TemplateID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="EmployeeName" HeaderText="Nama" />
                            <asp:BoundField DataField="EmployeeCode" HeaderText="Asal" HeaderStyle-CssClass="thLeft" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input type="hidden" value="<%#Eval("EmployeeID") %>" bindingfield="EmployeeID" />
                                    <input type="hidden" value="<%#Eval("EmployeeName") %>" bindingfield="EmployeeName" />
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
</div>

