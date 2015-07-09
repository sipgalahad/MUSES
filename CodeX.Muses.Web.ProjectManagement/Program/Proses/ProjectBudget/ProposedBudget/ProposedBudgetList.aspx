<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProposedBudgetList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProposedBudgetList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnProcess" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Process")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $('#btnProcess').click(function () {
                var lstSelectedMember = [];
                $('.chkSelectedItem').each(function () {
                    if ($(this).is(':checked')) {
                        $tr = $(this).closest('tr');
                        var id = $tr.find('.keyField').html();
                        lstSelectedMember.push(id);
                    }
                });
                $('#<%=hdnSelectedItem.ClientID %>').val(lstSelectedMember.join(','));
                cbpProcess.PerformCallback('save');
            })
        })

        $('.lblLink').live('click', function () {
            var url = "~/Program/Proses/ProjectBudget/ProposedBudgetDetailCtl.ascx";
            var id = $(this).closest('tr').find('.keyField').html();
            openUserControlPopup(url, id, 'Detail', 1200, 500);
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
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    cbpView.PerformCallback('refresh');
                }
            }
        }

    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedItem" runat="server" value="" />
    <div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="ProposedBudgetID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input type="checkbox" id="chkSelectedItem" class="chkSelectedItem" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProposedBudgetNo" HeaderText="No Anggaran" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="Position" HeaderText="Bagian" HeaderStyle-Width="300px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
                                <asp:BoundField DataField="TotalAmount" ItemStyle-CssClass="ItemLineAmount" HeaderText="Total" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px" DataFormatString="{0:N}" />
                                <asp:HyperLinkField Text="Detail" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-CssClass="lblLink" />
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("No Data To Display")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div>
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>