<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="BudgetInformationList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.BudgetInformationList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
    </style>
        
    <script type="text/javascript">
        $('.lblLink.lblUsedAmount').live('click', function () {
            var url = "~/Program/Proses/ProjectBudget/UseOfBudget/UsedTaskAmountCtl.ascx";
            $row = $(this).closest('tr');
            var id = $row.find('.keyField').html();
            var param = id + '|BI';
            openUserControlPopup(url, param, 'Detail', 700, 440);
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

        function onCboProjectChanged() {
            showLoadingPanel();
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnLstParentID" runat="server" value="" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value=""/>
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Project") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboProject" ClientInstanceName="cboProject" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){onCboProjectChanged()}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                        <thead>
                            <tr>
                                <th rowspan="2" class="keyField" rowspan="2">&nbsp;</th>
                                <th rowspan="2" style="width:50px; text-align:left"><%=GetLabel("Kode")%></th>
                                <th rowspan="2" style="text-align:left"><%=GetLabel("Nama Anggaran")%></th>
                                <th rowspan="2" style="width:150px;text-align:left"><%=GetLabel("Project")%></th>
                                <th rowspan="2" style="width:100px;text-align:left"><%=GetLabel("Bagian")%></th>
                                <th rowspan="2" style="width:150px;text-align:left"><%=GetLabel("Catatan")%></th>
                                <th style="text-align:center" id="thDana" runat="server" ><%=GetLabel("Sumber Dana")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Dianggarkan")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Direalisasikan")%></th>
                                <th rowspan="2" style="width:70px; text-align:right"><%=GetLabel("Digunakan")%></th>
                            </tr>
                            <tr>
                                <asp:Repeater runat="server" ID="rptViewHeader">
                                    <ItemTemplate>
                                        <th style="width:70px; text-align:right"><%#:Eval("StandardCodeName") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </thead>
                        <asp:Repeater runat="server" ID="grdView" OnItemDataBound="grdView_ItemDataBound">
                            <ItemTemplate>
                                <tbody>
                                    <tr class="trData">
                                        <td class="keyField"><%#:Eval("BudgetID")%></td>
                                        <td><%#:Eval("BudgetCode")%></td>
                                        <td><%#:Eval("BudgetName")%></td>
                                        <td><%#:Eval("ProjectName")%></td>
                                        <td><%#:Eval("Position")%></td>
                                        <td><%#:Eval("Remarks")%></td>
                                        <asp:Repeater runat="server" ID="rptViewItem">
                                            <ItemTemplate>
                                                <td align="right"><%# Convert.ToDecimal(Container.DataItem.ToString()).ToString("N") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td align="right"><%#:Eval("ProposedAmount","{0:N}")%></td>
                                        <td align="right"><%#:Eval("RealizationAmount","{0:N}")%></td>
                                        <td align="right"><label class="lblLink lblUsedAmount"><%#:Eval("UsedAmount","{0:N}")%></label></td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr class="trEmpty" runat="server" id="trEmpty">
                                    <td colspan="100">
                                        <%=GetLabel("No Data To Display")%>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
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
</asp:Content>