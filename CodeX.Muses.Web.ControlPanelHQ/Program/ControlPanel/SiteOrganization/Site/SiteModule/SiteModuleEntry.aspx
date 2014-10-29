<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSitePageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SiteModuleEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanelHQ.Program.SiteModuleEntry" %>

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
           $('#divQuickPicks').click(function () {
               showLoadingPanel();
               var url = ResolveUrl('~/Program/ControlPanel/SiteOrganization/Site/SiteModule/SiteModuleQuickPicksEntryCtl.ascx');
               openUserControlPopup(url, '', 'Quick Picks', 1000, 600);
           });
       });

       //#region Paging
       var pageCount = parseInt('<%=PageCount %>');
       var rowCount = parseInt('<%=RowCount %>');
       var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
       var currPage = parseInt('<%=CurrPage %>');
       $(function () {
           setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
           setPaging($("#paging"), pageCount, function (page) {
               cbpView.PerformCallback('changepage|' + page);
               setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
           }, null, currPage);
       });

       function onCbpViewEndCallback(s) {
           hideLoadingPanel();

           var param = s.cpResult.split('|');
           if (param[0] == 'refresh') {
               var pageCount = parseInt(param[1]);
               var rowCount = parseInt(param[2]);

               setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
               setPaging($("#paging"), pageCount, function (page) {
                   cbpView.PerformCallback('changepage|' + page);
                   setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
               });
           }
       }
       //#endregion

       $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
           $row = $(this).closest('tr');
           showToastConfirmation('Are You Sure Want To Delete?', function (result) {
               if (result) {
                   var entity = rowToObject($row);
                   $('#<%=hdnEntryID.ClientID %>').val(entity.SiteModuleID);
                   cbpProcess.PerformCallback('delete');
               }
           });
       });

       function onCbpProcesEndCallback(s) {
           hideLoadingPanel();

           var param = s.cpResult.split('|');
           if (param[0] == 'delete') {
               if (param[1] == 'fail')
                   showToast('Delete Failed', 'Error Message : ' + param[2]);
               else
                   cbpView.PerformCallback('refresh');
           }
       }

       function onAfterSaveAddRecordEntryPopup() {
           cbpView.PerformCallback('refresh');
       }
    </script>
    <input type="hidden" id="hdnEntryID" runat="server"/>
    <div class="divTransactionEntry">
        <span id="divQuickPicks" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="SiteModuleID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="ModuleName" HeaderText="Item"/>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <input type="hidden" value="<%#Eval("SiteModuleID") %>" bindingfield="SiteModuleID" />
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
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>