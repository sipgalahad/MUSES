<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FADepreciationEntryCtl.ascx.cs" 
    Inherits="Codex.Muses.Web.AssetManagement.Program.FADepreciationEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $(function () {
        $('#btnProses').click(function () {
            cbpProcessPopup.PerformCallback('process');
        });

        $('#btnVoid').click(function () {
            cbpProcessPopup.PerformCallback('delete');
        });
    });

    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            cbpViewPopup.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, page, rowCountPerPagePopup);
        });
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpViewPopup.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion

    function onCbpProcessPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'process') {
            if (param[1] == 'fail')
                showToast('Process Failed', 'Error Message : ' + param[2]);
            else 
                cbpViewPopup.PerformCallback('refresh');
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }

</script>
<input type="hidden" id="hdnFixedAssetID" runat="server" />
<table width="100%">
    <colgroup>
        <col width="50%"/>
    </colgroup>
    <tr>
        <td align="left"><input type="button" value='<%= GetLabel("Proses Akumulasi Penyusutan")%>' id="btnProses" /></td>
        <td align="right" style="display:none"><input type="button" value='<%= GetLabel("Batal Proses Jurnal Penyusutan")%>' id="btnVoid" /></td>
    </tr>
</table>

<dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
    ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
        EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <asp:Panel runat="server" ID="pnlView" CssClass="pnlEntryPopupGrdView">
                <asp:GridView ID="grdViewPopup" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="FADepreciationID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="cfPeriodNo" HeaderText="Periode" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="DepreciationDateInString" HeaderText="Tgl. Susut" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" />
                        <asp:BoundField DataField="AssetValue" HeaderText="Nilai Buku" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" HeaderStyle-Width="120px" />
                        <asp:BoundField DataField="DepreciationAmount" HeaderText="Nilai Susut" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" HeaderStyle-Width="120px" />
                        <asp:BoundField DataField="TotalDepreciationAmount" HeaderText="Akumulasi Penyusutan" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
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
    <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
    <div class="wrapperPaging">
        <div id="pagingPopup"></div>
    </div>
</div> 

<div style="display:none">
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpProcessPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

