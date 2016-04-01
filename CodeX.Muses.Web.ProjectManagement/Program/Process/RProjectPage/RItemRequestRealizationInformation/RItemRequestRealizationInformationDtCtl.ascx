<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RItemRequestRealizationInformationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.RItemRequestRealizationInformationDtCtl" %>
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
<input type="hidden" id="hdnItemRequestID" value="" runat="server" />
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
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Permintaan")%></label></td>
                    <td><asp:TextBox ID="txtItemRequestNo" Width="150px" ReadOnly="true" runat="server" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dari Bagian")%></label></td>
                    <td>
                        <input type="hidden" id="hdnFromSiteServiceUnitID" value="" runat="server" />
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <colgroup>
                                <col style="width: 30%" />
                                <col style="width: 3px" />
                                <col />
                            </colgroup>
                            <tr>
                                <td><asp:TextBox ID="txtFromServiceUnitCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtFromServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal" runat="server" id="lblLocation"><%=GetLabel("Dari Lokasi")%></label></td>
                    <td>
                        <input type="hidden" id="hdnFromLocationID" value="" runat="server" />
                        <input type="hidden" id="hdnLstFilterFromLocationItemGroup" value="" runat="server" />
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <colgroup>
                                <col style="width: 30%" />
                                <col style="width: 3px" />
                                <col />
                            </colgroup>
                            <tr>
                                <td><asp:TextBox ID="txtFromLocationCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtFromLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                            </tr>
                        </table>
                    </td>
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
                    <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu") %></td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtItemOrderDate" Width="120px" CssClass="datepicker" ReadOnly="true" runat="server" /></td>
                                <td style="width: 5px">&nbsp;</td>
                                <td><asp:TextBox ID="txtItemOrderTime" Width="100px" CssClass="time" runat="server" ReadOnly="true" Style="text-align: center" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Ke Bagian")%></label></td>
                    <td>
                        <input type="hidden" id="hdnToSiteServiceUnitID" value="" runat="server" />
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <colgroup>
                                <col style="width: 30%" />
                                <col style="width: 3px" />
                                <col />
                            </colgroup>
                            <tr>
                                <td><asp:TextBox ID="txtToServiceUnitCode" Width="100%" runat="server" ReadOnly="true"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtToServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
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
                                        <th class="keyField">&nbsp;</th>
                                        <th style="width:200px"><%=GetLabel("Nama Item")%></th>                              
                                        
                                        <th id="thRequest" runat="server" class="thCenter"><%=GetLabel("Diminta") %></th>
                                        <th id="thRealization" runat="server" class="thCenter"><%=GetLabel("Realisasi") %></th>
                                    </tr>
                                </thead>
                                <asp:Repeater runat="server" ID="rptView" OnItemDataBound="rptView_ItemDataBound">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr class="trData">
                                                <td class="keyField"><%#:Eval("ID")%></td>
                                                <td><%#:Eval("ItemName1")%></td>
                                                <td align="right" id="tdTotalRequest" runat="server"></td>
                                                <td align="right" id="tdTotalDistribution" runat="server"></td>
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
