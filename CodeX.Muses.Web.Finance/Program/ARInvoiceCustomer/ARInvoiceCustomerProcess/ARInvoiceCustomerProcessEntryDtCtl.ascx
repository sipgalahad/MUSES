<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ARInvoiceCustomerProcessEntryDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Finance.Program.ARInvoiceCustomerProcessEntryDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_apinvoicesupplierprocessctl">
    setDatePicker('<%=txtPeriodFrom.ClientID %>');
    setDatePicker('<%=txtPeriodTo.ClientID %>');

    $('#chkCheckAll').live('click', function () {
        var isChecked = $(this).is(':checked');
        $('#<%=grdView.ClientID %> .chkIsSelected input').each(function () {
            $(this).prop('checked', isChecked);
        });
    });

    $('#btnRefresh').click(function () {
        cbpEntryPopupView.PerformCallback('refresh');
    });

    function getCheckedMember() {
        var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
        var result = '';
        $('#<%=grdView.ClientID %> .chkIsSelected input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedMember.indexOf(key) < 0)
                    lstSelectedMember.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedMember.indexOf(key) > -1)
                    lstSelectedMember.splice(lstSelectedMember.indexOf(key), 1);
            }
        });
        $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
    }

    function onBeforeSaveRecord(errMessage) {
        getCheckedMember();
        if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
            errMessage.text = 'Silakan Pilih Piutang Terlebih Dahulu';
            return false;
        }
        return true;
    }

    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            getCheckedMember();
            cbpEntryPopupView.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
        });
    });

    function onCbpEntryPopupViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedMember();
                cbpEntryPopupView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto;overflow-x: hidden">
    <input type="hidden" id="hdnSelectedMember" runat="server" />
    <input type="hidden" id="hdnARInvoiceID" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>
            <td>
                <table>
                    <colgroup>
                        <col style="width:120px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td><label class="lblNormal"><%=GetLabel("Periode Transaksi") %></label></td>
                        <td colspan="2">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:TextBox runat="server" Width="120px" ID="txtPeriodFrom" CssClass="datepicker" /></td>
                                    <td style="width: 5px;">s/d</td>
                                    <td><asp:TextBox runat="server" Width="120px" ID="txtPeriodTo" CssClass="datepicker" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><input type="button" id="btnRefresh" value="Refresh" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="StudentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="50px">
                                            <HeaderTemplate>
                                                <input type="checkbox" id="chkCheckAll" style="text-align:center;" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="StudentName" HeaderText="Nama" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="PayerAmount" HeaderText="Total Piutang" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="200px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("Data Tidak Tersedia")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="containerPaging">
                    <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>
</div>