<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="StandardCodeList.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.StandardCodeList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        var isInit = true;
        $(function () {
            $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
                $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnID.ClientID %>').val($(this).find('.keyField').html());
                cbpView1.PerformCallback('refresh');
            });

            var id = $('#<%=hdnID.ClientID %>').val();
            if (id != '') {
                $('#<%=grdView.ClientID %> > tbody > tr').each(function () {
                    if ($(this).find('.keyField').html() == id)
                        $(this).addClass('selected');
                });
            }
        });

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        $(function () {
            $('#<%=grdView1.ClientID %> tr:gt(0)').live('click', function () {
                $('#<%=grdView1.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnID1.ClientID %>').val($(this).find('.keyField').html());
            });

            var id = $('#<%=hdnID1.ClientID %>').val();
            if (id != '') {
                $('#<%=grdView1.ClientID %> > tbody > tr').each(function () {
                    if ($(this).find('.keyField').html() == id)
                        $(this).click();
                });
            }
            else 
                $('#<%=grdView1.ClientID %> > tbody > tr:eq(1)').click();
        });

        $(function () {
            $('#<%=chkIsShowAll.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
                $('#<%=grdView1.ClientID %> tr:eq(1)').click();
            });
        });


        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging grdview
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

        //#region Paging grdview1
        var pageCount1 = parseInt('<%=PageCount1 %>');
        var rowCount1 = parseInt('<%=RowCount1 %>');
        var currPage1 = parseInt('<%=CurrPage1 %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries1'), rowCount1, currPage1, rowCountPerPage);
            setPaging($("#paging1"), pageCount1, function (page) {
                cbpView1.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries1'), rowCount1, page, rowCountPerPage);
            }, null, currPage1);
        });

        function onCbpViewEndCallback1(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView1.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID1.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries1'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging1"), pageCount, function (page) {
                    cbpView1.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries1'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView1.ClientID %> tr:eq(1)').click();
        }
        //#endregion
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnID1" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <div align="center">
            <asp:CheckBox ID="chkIsShowAll" runat="server" Text="Show All Standard Code"/>
        </div>
        <table width="100%" cellspacing="5px">
            <colgroup>
                <col width="50%" />
                <col width="50%" />
            </colgroup>
                <td valign="top">
                     <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="StandardCodeID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="StandardCodeID" HeaderText="Standard Code ID" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="StandardCodeName" HeaderText="Standard Code Name" />
                                            <asp:CheckBoxField DataField="IsEditableByUser" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Editable By User" />
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
                </td>
                <td valign="top">
                    <dxcp:ASPxCallbackPanel ID="cbpView1" runat="server" Width="100%" ClientInstanceName="cbpView1"
                        ShowLoadingPanel="false" OnCallback="cbpView1_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback1(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" CssClass="pnlContainerGrid">
                                    <asp:GridView ID="grdView1" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="StandardCodeID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="StandardCodeID" HeaderText="Standard Code ID" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="StandardCodeName" HeaderText="Standard Code Name" />
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
                        <div class="divInformationNumEntries" id="informationNumEntries1"></div> 
                        <div class="wrapperPaging">
                            <div id="paging1"></div>
                        </div>
                    </div>
                </td>
            </tr>
        </table> 
    </div>
</asp:Content>
