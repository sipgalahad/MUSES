<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="BudgetManagementList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.BudgetManagementList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');
        });

        $('.lblLink').die('click');
        $('.lblLink').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl('~/Program/Proses/BudgetManagement/BudgetManagementPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'Budget Management' + id, '1300', '650');
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        $('.btnSave').die('click');
        $('.btnSave').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var idx = $row.find('.hdnItemIndex').val();
            cboStatus = eval('cboStatus' + idx);
            $('#<%=hdnID.ClientID %>').val(entity.ProjectID);
            $('#<%=hdnStatus.ClientID %>').val(cboStatus.GetValue());
            cbpProcess.PerformCallback('save');
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
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnStatus" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="BudgetID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="200px" >
                                    <HeaderTemplate>
                                        <div style="padding-left:3px">
                                            <%=GetLabel("Budget Code")%>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style='margin-left:<%# Eval("BudgetLevel") %>0px;'><%# Eval("BudgetCode") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BudgetName" HeaderText="Nama" HeaderStyle-Width="220px"  HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan"  HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Detail" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="lblLink"><%=GetLabel("Detail") %></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="120px" ItemStyle-CssClass="thCenter">
                                    <ItemTemplate>
                                        <dxe:ASPxComboBox ID="cboStatus" class="cboStatus" Width="120px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <input type="button" value="Simpan" class="btnSave" id="btnSave" runat="server" />
                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                        <input type="hidden" value="<%#Eval("BudgetID") %>" bindingfield="BudgetID" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("Data Tidak Tersedia")%>
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