<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPRProjectPageTrx.master" AutoEventWireup="true" 
    CodeBehind="RBudgetRealizationConfirmationList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RBudgetRealizationConfirmationList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnRBudgetRealizationHdApprove" crudmode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><br style="clear: both" /><div><%=GetLabel("Approve")%></div></li>
    <li id="btnRBudgetRealizationHdDecline" crudmode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><br style="clear: both" /><div><%=GetLabel("Decline")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#<%=btnRBudgetRealizationHdApprove.ClientID %>').click(function () {
                if ($('.chkIsSelected input:checked').length < 1) {
                    showToast('Warning', 'Please Select Item Distribution First');
                }
                else {
                    var param = '';
                    $('.chkIsSelected input:checked').each(function () {
                        var itemDistributionHdID = $(this).closest('tr').find('.keyField').html();
                        if (param != '')
                            param += ',';
                        param += itemDistributionHdID;
                    });
                    $('#<%=hdnParam.ClientID %>').val(param);
                    onCustomButtonClick('approve');
                }
            });

            $('#<%=btnRBudgetRealizationHdDecline.ClientID %>').click(function () {
                if ($('.chkIsSelected input:checked').length < 1) {
                    showToast('Warning', 'Please Select Item Distribution First');
                }
                else {
                    var param = '';
                    $('.chkIsSelected input:checked').each(function () {
                        var itemDistributionHdID = $(this).closest('tr').find('.keyField').html();
                        if (param != '')
                            param += ',';
                        param += itemDistributionHdID;
                    });
                    $('#<%=hdnParam.ClientID %>').val(param);
                    onCustomButtonClick('decline');
                }
            });
        });

        function onAfterCustomClickSuccess(type) {
            cbpView.PerformCallback('refresh');
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        $('.lnkDetail a').live('click', function () {
            var param = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Process/RProjectPage/RBudgetRealizationConfirmation/RBudgetRealizationConfirmationDetailCtl.ascx");
            openUserControlPopup(url, param, 'Konfirmasi Realisasi Anggaran', 1200, 600);
        });
    </script>
    <input type="hidden" value="" id="hdnParam" runat="server" />
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnFilterExpression" runat="server" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="BudgetRealizationID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataTextField="BudgetRealizationNo" HeaderText="No. Realisasi" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="RealizationDateInString" HeaderText="Tanggal Realisasi" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="160px" />
                                <asp:BoundField DataField="ProjectTaskGroupName" HeaderText="Kelompok Tugas" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
                <div id="paging">
                </div>
            </div>
        </div>
    </div>
</asp:Content>