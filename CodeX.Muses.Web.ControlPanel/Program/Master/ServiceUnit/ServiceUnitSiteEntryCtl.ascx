<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitSiteEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.ServiceUnitSiteEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtSiteCode.ClientID %>').removeAttr('readonly');

            $('#<%=txtSiteCode.ClientID %>').val('');
            $('#<%=txtSiteName.ClientID %>').val('');
            $('#<%=txtServiceInterval.ClientID %>').val('0');
            $('#<%=txtServiceUnitOfficer.ClientID %>').val('');

            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
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
        showToastConfirmation('Are You Sure Want To Delete?', function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.SiteServiceUnitID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.SiteServiceUnitID);
        $('#<%=txtSiteCode.ClientID %>').attr('readonly', 'readonly');

        $('#<%=txtSiteCode.ClientID %>').val(entity.SiteID);
        $('#<%=txtSiteName.ClientID %>').val(entity.SiteName);
        $('#<%=txtServiceInterval.ClientID %>').val(entity.ServiceInterval);
        $('#<%=txtServiceUnitOfficer.ClientID %>').val(entity.ServiceUnitOfficer);
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



    //#region Site
    function onGetSiteFilterExpression() {
        var serviceUnitID = $('#<%=hdnID.ClientID %>').val();
        var filterExpression = "SiteID NOT IN (SELECT SiteID FROM SiteServiceUnit WHERE ServiceUnitID = " + serviceUnitID + " AND IsDeleted = 0)";
        return filterExpression;
    }
    $('#lblSite.lblLink').die('click');
    $('#lblSite.lblLink').live('click', function () {
        openSearchDialog('site', onGetSiteFilterExpression(), function (value) {
            $('#<%=txtSiteCode.ClientID %>').val(value);
            onTxtSHUSiteCodeChanged(value);
        });
    });

    $('#<%=txtSiteCode.ClientID %>').die('change');
    $('#<%=txtSiteCode.ClientID %>').live('change', function () {
        onTxtSHUSiteCodeChanged($(this).val());
    });

    function onTxtSHUSiteCodeChanged(value) {
        var filterExpression = onGetSiteFilterExpression() + " AND SiteID = '" + value + "'";
        Methods.getObject('GetSiteList', filterExpression, function (result) {
            if (result != null)
                $('#<%=txtSiteName.ClientID %>').val(result.SiteName);
            else
                $('#<%=txtSiteName.ClientID %>').val('');
        });
    }
    //#endregion

    $('.lnkDetail a').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        var url = ResolveUrl('~/Program/Master/ServiceUnit/SiteServiceUnitPageLauncher.aspx?id=' + entity.SiteServiceUnitID);
        openWindowPopup(url, 'SiteServiceUnit', '1300', '650');
    });
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />

    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Unit")%></label></td>
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
                        <col style="width:200px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblSite"><%=GetLabel("Site")%></label></td>
                        <td>
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtSiteCode" CssClass="required" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtSiteName" ReadOnly="true" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Service Interval")%></label></td>
                        <td><asp:TextBox ID="txtServiceInterval" CssClass="required number" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Service Unit Officer")%></label></td>
                        <td><asp:TextBox ID="txtServiceUnitOfficer" Width="100%" runat="server" /></td>
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
            <dx:PanelContent ID="PanelContent2" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField HeaderStyle-Width="150px" DataField="SiteName" HeaderText="Site Name" />
                            <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="80px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#:Eval("SiteServiceUnitID") %>" bindingfield="SiteServiceUnitID" />
                                    <input type="hidden" value="<%#:Eval("SiteID") %>" bindingfield="SiteID" />
                                    <input type="hidden" value="<%#:Eval("SiteName") %>" bindingfield="SiteName" />
                                    <input type="hidden" value="<%#:Eval("ServiceUnitOfficer") %>" bindingfield="ServiceUnitOfficer" />
                                    <input type="hidden" value="<%#:Eval("ServiceInterval") %>" bindingfield="ServiceInterval" />
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

