 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBudgetManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="UseOfBudgetList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.UseOfBudgetList" %>
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
        $('.btnSave').die('click');
        $('.btnSave').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var idx = entity.ItemIndex;
            $('#<%=hdnID.ClientID %>').val(entity.BudgetDtID);
            var usedAmount = parseFloat($('.txtUsedAmount' + idx).attr('hiddenVal'));
            var amount = parseFloat($row.find('.hdnAmount').val());
            $('#<%=hdnUsedAmount.ClientID %>').val(usedAmount + amount);
            cbpProcess.PerformCallback('save');
        });

        $('.lblLink.lblUsedTaskAmount').live('click', function () {
            var url = "~/Program/Proses/ProjectBudget/UseOfBudget/UsedTaskAmountCtl.ascx";
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var id = entity.BudgetDtID;
            var param = id + '|UB';
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
                    $('#<%=lvwView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=lvwView.ClientID %> tr:eq(1)').click();
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

        function onCboProjectChanged() {
            showLoadingPanel();
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnLstParentID" runat="server" value="" />
    <input type="hidden" id="hdnUsedAmount" runat="server" value="" />
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
                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                        <EmptyDataTemplate>
                            <table id="tblView" runat="server" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                                <tr>
                                    <th style="width:70px" rowspan="2" align="left"><%=GetLabel("Kode") %></th>
                                    <th style="width:70px" rowspan="2" align="left"><%=GetLabel("Nama") %></th>
                                    <th style="width:170px" rowspan="2" align="left"><%=GetLabel("Bagian") %></th>
                                    <th style="width:200px" rowspan="2" align="left"><%=GetLabel("Keterangan") %></th>
                                    <th style="width:70px" rowspan="2" align="left"><%=GetLabel("Dianggarkan") %></th>
                                    <th style="width:70px" rowspan="2" align="right"><%=GetLabel("Direalisasikan") %></th>
                                    <th style="width:70px" colspan="2" align="right"><%=GetLabel("Digunakan") %></th>
                                    <th style="width:100px" rowspan="2" >&nbsp;</th>
                                </tr>
                                <tr>
                                    <th style="width:35px" ><%=GetLabel("Kegiatan") %></th>
                                    <th style="width:35px" ><%=GetLabel("Lain-lain") %></th>
                                </tr>
                                <tr class="trEmpty">
                                    <td colspan="9">
                                        <%=GetLabel("Data Tidak Tersedia")%>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table id="tblView" runat="server" class="grdView notAllowSelect" width="100%" cellspacing="0" rules="all" >
                                <tr>
                                    <th style="width:70px" rowspan="2" align="left"><%=GetLabel("Kode") %></th>
                                    <th rowspan="2" align="left"><%=GetLabel("Nama") %></th>
                                    <th style="width:170px" rowspan="2" align="left"><%=GetLabel("Bagian") %></th>
                                    <th style="width:250px" rowspan="2" align="left"><%=GetLabel("Keterangan") %></th>
                                    <th style="width:70px" rowspan="2" align="left"><%=GetLabel("Dianggarkan") %></th>
                                    <th style="width:70px" rowspan="2" class="thRight"><%=GetLabel("Direalisasikan") %></th>
                                    <th style="width:140px" colspan="2" class="thCenter"><%=GetLabel("Digunakan") %></th>
                                    <th style="width:100px" rowspan="2" >&nbsp;</th>
                                </tr>
                                <tr>
                                    <th style="width:70px" class="thCenter"><%=GetLabel("Kegiatan") %></th>
                                    <th style="width:70px" class="thCenter" ><%=GetLabel("Lain-lain") %></th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder" ></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#:Eval("BudgetDtCode") %></td>
                                <td><%#:Eval("BudgetDtName") %></td>
                                <td><%#:Eval("Position") %></td>
                                <td><%#:Eval("Remarks") %></td>
                                <td align="right"><%#:Eval("ProposedAmount","{0:N}") %></td>
                                <td align="right"><%#:Eval("RealizationAmount","{0:N}") %></td>
                                <td align="right" runat="server" id="UsedTaskAmount"></td>
                                <td>
                                    <input type="hidden" class="hdnAmount" id="hdnAmount" runat="server" value="0" />
                                    <input type="text" class="txtCurrency" id="txtUsedAmount" runat="server" style="width:100%" value=""/>
                                </td>
                                <td align="center">
                                    <input type="button" value="Simpan" class="btnSave" id="btnSave" runat="server" />
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' bindingfield="ItemIndex" />
                                    <input type="hidden" value="<%#Eval("BudgetDtID") %>" bindingfield="BudgetDtID" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
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
</asp:Content>