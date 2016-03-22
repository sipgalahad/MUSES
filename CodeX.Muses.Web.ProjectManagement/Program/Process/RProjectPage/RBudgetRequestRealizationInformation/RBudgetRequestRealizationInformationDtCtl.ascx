<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RBudgetRequestRealizationInformationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.RBudgetRequestRealizationInformationDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    var rowCountPopup = parseInt('<%=RowCount %>');
    var rowCountPerPagePopup = parseInt('<%=RowCountPerPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCountPopup, 1, rowCountPerPagePopup);
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
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
                cbpEntryPopupView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });

        }
    }
    //#endregion
</script>
<input type="hidden" id="hdnBudgetRequestID" value="" runat="server" />
<table class="tblContentArea" style="width:100%">
    <colgroup>
        <col style="width:50%"/>
    </colgroup>
    <tr>
        <td style="padding: 5px; vertical-align: top">
            <table class="tblEntryContent" style="width: 100%">
                <colgroup>
                    <col style="width: 30%" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblNormal" id="lblDistribution"><%=GetLabel("No. Distribusi")%></label></td>
                    <td><asp:TextBox ID="txtBudgetRequestNo" Width="150px" ReadOnly="true" runat="server" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal" runat="server" id="Label5"><%=GetLabel("Kelompok Tugas")%></label></td>
                    <td><asp:TextBox ID="txtProjectTaskGroupName" Width="100%" ReadOnly="true" runat="server" /></td>
                </tr>
            </table>
        </td>
        <td style="padding: 5px; vertical-align: top">
            <table class="tblEntryContent" style="width: 100%">
                <colgroup>
                    <col style="width: 30%" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><%=GetLabel("Keterangan") %></td>
                    <td><asp:TextBox ID="txtNotes" ReadOnly="true" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
                ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ hideLoadingPanel(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto; height:290px; overflow-y:scroll;
                            position: relative; font-size: 0.95em;">
                            <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                                <thead>
                                    <tr>
                                        <th class="keyField" rowspan="3">&nbsp;</th>
                                        <th style="width:200px" rowspan="3"><%=GetLabel("Nama Anggaran")%></th>                              
                                        
                                        <th id="thRequest" runat="server" class="thCenter"><%=GetLabel("Diminta") %></th>
                                        <th id="thRealization" runat="server" class="thCenter"><%=GetLabel("Realisasi") %></th>

                                        <th rowspan="3"><%=GetLabel("Catatan")%></th>
                                    </tr>
                                    <tr>
                                        <th id="thContainerAmount" runat="server" class="thCenter"><%=GetLabel("Sumber Dana") %></th>
                                        <th style="width:70px;" class="thRight" rowspan="2"><%=GetLabel("Total")%></th>
                                        <th id="thContainerAmount2" runat="server" class="thCenter"><%=GetLabel("Sumber Dana") %></th>
                                        <th style="width:70px;" class="thRight" rowspan="2"><%=GetLabel("Total")%></th>
                                    </tr>
                                    <tr>
                                        <asp:Repeater runat="server" ID="rptViewHeader">
                                            <ItemTemplate>
                                                <th style="width:70px;" class="thRight"><%#:Eval("StandardCodeName") %></th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="rptViewHeader2">
                                            <ItemTemplate>
                                                <th style="width:70px;" class="thRight"><%#:Eval("StandardCodeName") %></th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </thead>
                                <asp:Repeater runat="server" ID="rptView" OnItemDataBound="rptView_ItemDataBound">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr class="trData">
                                                <td class="keyField"><%#:Eval("BudgetRequestDtID")%></td>
                                                <td><%#:Eval("BudgetRequestDtName")%></td>
                                                <asp:Repeater runat="server" ID="rptViewItem" OnItemDataBound="rptViewItem_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right" id="tdTotalAmount" class="tdTotalAmount" runat="server"></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td align="right"><%#:Eval("TotalAmount","{0:N}")%></td>
                                                
                                                <asp:Repeater runat="server" ID="rptViewItemRealization" OnItemDataBound="rptViewItemRealization_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right" id="tdTotalAmount" class="tdTotalAmount" runat="server"></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td align="right" id="tdTotalRealizationAmount" class="tdTotalAmount" runat="server"></td>

                                                <td><%#:Eval("Remarks")%></td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
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
