<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="CustomerContractList.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CustomerContractList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');
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

        //#region Customer
        function onGetCustomerFilterExpression() {
            return "<%=OnGetCustomerFilterExpression() %>";
        }

        $('#lblCustomer.lblLink').live('click', function () {
            openSearchDialog('businesspartners', onGetCustomerFilterExpression(), function (value) {
                $('#<%=txtCustomerCode.ClientID %>').val(value);
                onTxtCustomerCodeChanged(value);
            });
        });

        $('#<%=txtCustomerCode.ClientID %>').live('change', function () {
            onTxtCustomerCodeChanged($(this).val());
        });

        function onTxtCustomerCodeChanged(value) {
            var filterExpression = onGetCustomerFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
            Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnCustomerID.ClientID %>').val(result.BusinessPartnerID);
                    $('#<%=txtCustomerName.ClientID %>').val(result.BusinessPartnerName);
                }
                else {
                    $('#<%=hdnCustomerID.ClientID %>').val('');
                    $('#<%=txtCustomerCode.ClientID %>').val('');
                    $('#<%=txtCustomerName.ClientID %>').val('');
                }
                cbpView.PerformCallback('refresh');
            });
        }
        //#endregion

        $('.lnkMember a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/CustomerContract/CustomerContractMemberEntryCtl.ascx");
            openUserControlPopup(url, id, 'Member', 850, 600);
        });

        $('.lnkContractSummary a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Master/CustomerContract/CustomerContractSummaryViewCtl.ascx");
            openUserControlPopup(url, id, 'Ringkasan Kontrak', 700, 600);
        });
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <table>
        <colgroup>
            <col style="width:100px"/>
            <col style="width:500px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblLink" id="lblCustomer"><%=GetLabel("Instansi")%></label></td>
            <td>
                <input type="hidden" value="" runat="server" id="hdnCustomerID" />
                <table style="width:100%" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:100px"/>
                        <col style="width:3px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtCustomerCode" Width="100%" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtCustomerName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ $('#tempContainerGrdDetail').append($('#containerGrdDetail')); $('#hdnID').val(''); showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="ContractID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="ContractNo" HeaderText="No Kontrak" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="StartDateInString" ItemStyle-HorizontalAlign="Center" HeaderText="Tanggal Mulai" HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" />
                                <asp:BoundField DataField="EndDateInString" ItemStyle-HorizontalAlign="Center" HeaderText="Tanggal Akhir" HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" />
                                <asp:HyperLinkField HeaderText="Ringkasan Kontrak" HeaderStyle-CssClass="thCenter" Text="Ringkasan Kontrak" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkContractSummary" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" />
                                <asp:HyperLinkField HeaderText="Member" HeaderStyle-CssClass="thCenter" Text="Member" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkMember" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" />
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
    </div>
</asp:Content>