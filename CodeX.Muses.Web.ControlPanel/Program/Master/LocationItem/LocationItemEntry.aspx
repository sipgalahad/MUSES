<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="LocationItemEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.LocationItemEntry" %>

<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                tacItem.setValue('');
                tacItem.setText('');
                $('#<%=txtMinimum.ClientID %>').val('');
                $('#<%=txtMaximum.ClientID %>').val('');
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region Location
        function onGetLocationFilterExpression() {
            var filterExpression = "<%:OnGetLocationFilterExpression() %>";
            return filterExpression;
        }

        function onTacLocationButtonSearchClick() {
            openSearchDialog('locationroleuser', onGetLocationFilterExpression(), function (value) {
                var filterExpression = onGetLocationFilterExpression() + "LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        tacLocation.setValue(result.LocationID);
                        tacLocation.setText(result.LocationName);
                        filterExpression = "LocationID = " + result.LocationID;
                        Methods.getObject('GetLocationList', filterExpression, function (result) {
                            $('#<%=hdnLocationItemGroupID.ClientID %>').val(result.ItemGroupID);
                            setTimeout(function () {
                                cbpView.PerformCallback('refresh');
                            }, 100);
                        });
                    }
                    else {
                        tacLocation.setValue('');
                        tacLocation.setText('');
                        $('#<%=hdnLocationItemGroupID.ClientID %>').val('');
                        cbpView.PerformCallback('refresh');
                    }
                });
            });

        }

        function onTacLocationValueChanged() {
            var locationID = tacLocation.getValue();
            if (locationID != '') {
                var filterExpression = "LocationID = " + locationID;
                Methods.getObject('GetLocationList', filterExpression, function (result) {
                    $('#<%=hdnLocationItemGroupID.ClientID %>').val(result.ItemGroupID);
                    setTimeout(function () {
                        cbpView.PerformCallback('refresh');
                    }, 100);
                });
            }
        }
        //#endregion

        function onRefreshGrid() {
            $('#<%=hdnFilterExpressionQuickSearch.ClientID %>').val(txtSearchView.GenerateFilterExpression());
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGrid();
            }, 0);
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

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        //#region Item
        function onGetItemFilterExpression() {
            var filterExpression = "<%=OnGetItemProductFilterExpression() %> AND ItemID NOT IN (SELECT ItemID FROM ItemBalance WHERE LocationID = " + tacLocation.getValue() + " AND IsDeleted = 0)";
            if ($('#<%=hdnLocationItemGroupID.ClientID %>').val() != '')
                filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath like '%/" + $('#<%=hdnLocationItemGroupID.ClientID %>').val() + "/%')";
            return filterExpression;
        }

        function onTacItemButtonSearchClick() {
            openSearchDialog('item', onGetItemFilterExpression(), function (value) {
                var filterExpression = onGetItemFilterExpression() + " AND ItemCode = '" + value + "'";
                Methods.getObject('GetItemMasterList', filterExpression, function (result) {
                    if (result != null) {
                        tacItem.setValue(result.ItemID);
                        tacItem.setText(result.ItemName1);
                    }
                    else {
                        tacItem.setValue('');
                        tacItem.setText('');
                    }
                });
            });

        }

        function onTacItemValueChanged() {
        }
        //#endregion

        //#region edit and delete
        $('.grdItemBalance .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('.grdItemBalance .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            tacItem.setValue(entity.ItemID);
            tacItem.setText(entity.ItemName1);
            $('#<%=txtMinimum.ClientID %>').val(entity.QuantityMIN);
            $('#<%=txtMaximum.ClientID %>').val(entity.QuantityMAX);
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
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
    </script>
    
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <table>
        <colgroup>
            <col width="120px" />
            <col />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Location") %></label></td>
            <td>
                <input type="hidden" id="hdnLocationItemGroupID" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacLocation" ClientInstanceName="tacLocation" MethodName="GetLocationUserAccessList" GetFilterExpressionFunction="onGetLocationFilterExpression"
                    SearchFields="LocationName,LocationCode" TextField="LocationName" ValueField="LocationID" SearchText="${LocationName} (<b>${LocationCode}</b>)" OrderByExpression="LocationCode">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacLocationButtonSearchClick(); }"
                        ValueChanged="function(){ onTacLocationValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Quick Filter")%></label></td>
            <td>
                <cdx:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView" Width="300px" Watermark="Search">
                    <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
                    <IntellisenseHints>
                        <cdx:QISIntellisenseHint Text="ItemName1" FieldName="ItemName1" />
                    </IntellisenseHints>
                </cdx:QISIntellisenseTextBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 160px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacItem" ClientInstanceName="tacItem" MethodName="GetItemMasterList" GetFilterExpressionFunction="onGetItemFilterExpression"
                                            SearchFields="ItemName1,ItemCode" TextField="ItemName1" ValueField="ItemID" SearchText="${ItemName1} / ${PreferredName} (<b>${ItemCode}</b>)" OrderByExpression="ItemName1">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacItemButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacItemValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>  
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Reorder Minimum")%></label></td>
                                    <td><asp:TextBox ID="txtMinimum" CssClass="number required" runat="server" Width="100px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Reorder Maximum")%></label></td>
                                    <td><asp:TextBox ID="txtMaximum" CssClass="number required" runat="server" Width="100px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                            <EmptyDataTemplate>
                                <table id="tblView" runat="server" class="grdView grdBorder notAllowSelect grdItemBalance" cellspacing="0" rules="all" >
                                    <tr>
                                        <th style="width:250px" rowspan="2" class="thCenter"><%=GetLabel("Item")%></th>
                                        <th colspan="2" class="thCenter"><%=GetLabel("Reorder Point")%></th>
                                        <th colspan="4" class="thCenter"><%=GetLabel("Balance")%></th>
                                        <th rowspan="2" style="width:100px" class="thCenter"><%=GetLabel("Expired Date")%></th>
                                        <th style="width:70px" rowspan="2">&nbsp;</th>
                                    </tr>
                                    <tr>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Minimum")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Maximum")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Beginning")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("In")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Out")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Ending")%></th>
                                    </tr>
                                    <tr class="trEmpty">
                                        <td colspan="9">
                                            <%=GetLabel("No Data To Display")%>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table id="tblView" runat="server" class="grdView grdBorder notAllowSelect grdItemBalance" cellspacing="0" rules="all" >
                                    <tr>
                                        <th style="width:250px" rowspan="2" align="center"><%=GetLabel("Item")%></th>
                                        <th colspan="2" class="thCenter"><%=GetLabel("Reorder Point")%></th>
                                        <th colspan="4" class="thCenter"><%=GetLabel("Balance")%></th>
                                        <th rowspan="2" style="width:100px" class="thCenter"><%=GetLabel("Expired Date")%></th>
                                        <th style="width:70px" rowspan="2">&nbsp;</th>
                                    </tr>
                                    <tr>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Minimum")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Maximum")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Beginning")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("In")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Out")%></th>
                                        <th style="width:100px" class="thCenter"><%=GetLabel("Ending")%></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" ></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#: Eval("ItemName1")%></td>
                                    <td align="right"><%#: Eval("QuantityMIN")%></td>
                                    <td align="right"><%#: Eval("QuantityMAX")%></td>
                                    <td align="right"><%#: Eval("QuantityBEGIN")%></td>
                                    <td align="right"><%#: Eval("QuantityIN")%></td>
                                    <td align="right"><%#: Eval("QuantityOUT")%></td>
                                    <td align="right"><%#: Eval("QuantityEND")%></td>
                                    <td align="center"><label id="lblExpiredDate" runat="server" class="lblExpiredDate lblLink"><%=GetLabel("Expired Date") %></label></td>
                                    <td align="center">
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                        <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                        <input type="hidden" value="<%#Eval("ItemName1") %>" bindingfield="ItemName1" />
                                        <input type="hidden" value="<%#Eval("QuantityMIN") %>" bindingfield="QuantityMIN" />
                                        <input type="hidden" value="<%#Eval("QuantityMAX") %>" bindingfield="QuantityMAX" />
                                        <input type="hidden" value="<%#Eval("QuantityBEGIN") %>" bindingfield="QuantityBEGIN" />
                                        <input type="hidden" value="<%#Eval("QuantityIN") %>" bindingfield="QuantityIN" />
                                        <input type="hidden" value="<%#Eval("QuantityOUT") %>" bindingfield="QuantityOUT" />
                                        <input type="hidden" value="<%#Eval("QuantityEND") %>" bindingfield="QuantityEND" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>