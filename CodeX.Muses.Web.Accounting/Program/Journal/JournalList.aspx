<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="JournalList.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.JournalList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnApprove" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Approve")%></div></li>
    <li id="btnUnapprove" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/cancel.png")%>' alt="" /><div><%=GetLabel("Decline")%></div></li>
    <li id="btnVoid" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><div><%=GetLabel("Void")%></div></li>
</asp:Content>

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

        $(function () {
            setDatePicker('<%=txtFromJournalDate.ClientID %>');
            $('#<%=txtFromJournalDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtToJournalDate.ClientID %>');
            $('#<%=txtToJournalDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $("#<%=txtFromJournalDate.ClientID%>").change(function () {
                cbpView.PerformCallback('refresh');
            });

            $("#<%=txtToJournalDate.ClientID%>").change(function () {
                cbpView.PerformCallback('refresh');
            });

            $("#<%=rblJournalGroup.ClientID%> input").change(function () {
                $('#<%=hdnSelectedJournalGroup.ClientID %>').val($(this).val());
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnApprove.ClientID %>').click(function () {
                var status = $('#<%=grdView.ClientID %> tr.selected .hdnGCTransactionStatus').val();
                if (status == "<%=GetGCTransactionStatusOpen() %>")
                    onCustomButtonClick('approve');
                else
                    showToast('Warning', 'Status Jurnal Tidak Open');
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnVoid.ClientID %>').click(function () {
                var status = $('#<%=grdView.ClientID %> tr.selected .hdnGCTransactionStatus').val();
                if (status == "<%=GetGCTransactionStatusOpen() %>")
                    onCustomButtonClick('void');
                else
                    showToast('Warning', 'Status Jurnal Tidak Open');
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnUnapprove.ClientID %>').click(function () {
                var date = $('#<%=txtToJournalDate.ClientID %>').val().split('-');
                var filterExpression = "JournalDate LIKE '" + date[2] + "-" + date[1] + "%' AND TransactionCode = '7299'";
                Methods.getObject('GetGLTransactionHdList', filterExpression, function (result) {
                    if (result == null) {
                        var status = $('#<%=grdView.ClientID %> tr.selected .hdnGCTransactionStatus').val();
                        if (status != "<%=GetGCTransactionStatusOpen() %>")
                            onCustomButtonClick('unapprove');
                        else
                            showToast('Warning', 'Status Jurnal Tidak Approved atau Void');
                    } else {
                        showToast('Warning', 'Telah dilakukan Posting pada periode ini');
                    }
                    cbpView.PerformCallback('refresh');
                });
            });
        });

        $('.lnkDetail a').live('click', function () {
            $tr = $(this).closest('tr');
            var id = $tr.find('.keyField').html();
            var url = ResolveUrl("~/Program/Journal/JournalListDtCtl.ascx");
            openUserControlPopup(url, id, 'Detail Transaksi Jurnal', 1200, 520);
        });

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var transactionID = $('#<%=hdnID.ClientID %>').val();
            
            if (transactionID == '' || transactionID == '0') {
                errMessage.text = 'Pilih Jurnal Terlebih Dahulu!';
                return false;
            }
            else {
                var status = $('#<%=grdView.ClientID %> tr.selected .hdnGCTransactionStatus').val();
                if (status == "<%=GetGCTransactionStatusOpen() %>") {
                    errMessage.text = 'Jurnal Belum di Approve';
                    return false;
                } else {
                    filterExpression.text = 'GLTransactionID = ' + transactionID;
                    return true;
                }
            }
        }
    </script>
    <style type="text/css">
        .rblJournalGroup input[type="radio"]            { margin-right: 5px; }
    </style>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <input type="hidden" id="hdnSelectedJournalGroup" runat="server" value="" />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Transaksi")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px"/>
                        <col style="width:3px"/>
                        <col style="width:145px"/>
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtFromJournalDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        <td><%=GetLabel("s/d") %></td>
                        <td><asp:TextBox ID="txtToJournalDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Jurnal")%></label></td>
            <td colspan="2">
                <asp:RadioButtonList CssClass="rblJournalGroup" ID="rblJournalGroup" runat="server" RepeatDirection="Horizontal" />
            </td>
        </tr>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="GLTransactionID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="JournalNo" HeaderText="Nomor Jurnal" HeaderStyle-Width="150px"  />
                                <asp:BoundField DataField="JournalDateInString" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Tanggal" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
                                <asp:BoundField DataField="TransactionStatus" HeaderText="Status" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderText="Perkiraan" HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <input type="hidden" class="hdnGCTransactionStatus" value='<%#Eval("GCTransactionStatus") %>'>
                                        <a>Lihat</a>
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
</asp:Content>