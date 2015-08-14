<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfrastructureBudgetCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.InfrastructureBudgetCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_ptservicectl">
    function onLoadInfrastructure() {
        $('#divInfrastructureTransactionAdd').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtProposedBudgetCode.ClientID %>').val('');
            tacItem.setValue('');
            tacItem.setText('');
            $('#<%=txtItemQuantity.ClientID %>').val('0');
            $('#<%=txtConversion.ClientID %>').val('');
            $('#<%=txtRealizationDate.ClientID %>').val('');
            $('#<%=txtEntryRemarks.ClientID %>').val('');
            $('#<%=txtProposedBudgetCode.ClientID %>').attr('readonly', false);

            $('.txtInfrastructureFund').each(function () {
                $(this).val(0).trigger('changeValue');
            });
            $('#<%=txtTotalLineAmount.ClientID %>').val(0).trigger('changeValue');

            $('#containerEntryInfrastructure').show();
        });

        $('#btnInfrastructureSave').click(function (evt) {
            if (IsValid(evt, 'fsTrx', 'mpTrxPopup')) {
                var lst = '';
                $('.txtInfrastructureFund').each(function () {
                    var value = $(this).attr('hiddenVal');
                    if (lst != "")
                        lst += '|';
                    lst += value;
                });
                $('#<%=hdnLstFundItem.ClientID %>').val(lst);
                cbpInfrastructureView.PerformCallback('save');
            }
        });

        $('#btnInfrastructureCancel').click(function () {
            $('#containerEntryInfrastructure').hide();
        });

        var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
        var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
        var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
        setNumEntriesText($('#infrastructureInformationNumEntries'), rowCount, 1, rowCountPerPage);
        setPaging($("#infrastructurePaging"), pageCount, function (page) {
            cbpInfrastructureView.PerformCallback('changepage|' + page);
            setNumEntriesText($('#infrastructureInformationNumEntries'), rowCount, page, rowCountPerPage);
        });
    }

    //#region Item
    function onGetItemFilterExpression() {
        var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
        var requestID = $('#<%=hdnID.ClientID %>').val();
        if (requestID != '')
            filterExpression += " AND ItemID NOT IN (SELECT ItemID FROM ProposedBudgetDt WHERE ProposedBudgetID = " + requestID + " AND IsDeleted = 0 AND ItemID IS NOT NULL)";
        return filterExpression;
    }

    function onTacItemButtonSearchClick() {
        openSearchDialog('item', onGetItemFilterExpression(), function (value) {
            var filterExpression = onGetItemFilterExpression() + " AND ItemCode = '" + value + "'";
            Methods.getObject('GetvItemMasterList', filterExpression, function (result) {
                if (result != null) {
                    tacItem.setValue(result.ItemID);
                    tacItem.setText(result.ItemName1);
                    $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                    Methods.getItemMasterPurchase(result.ItemID, 0, function (result2) {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                        $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);
                    })
                    entityToControlItem(result);
                    cboItemUnit.PerformCallback();
                }
                else {
                    tacItem.setValue('');
                    tacItem.setText('');
                    entityToControlItem(null);
                }
            });
        });
    }

    function onTacItemValueChanged() {
        var id = tacItem.getValue();
        if (id != '') {
            var filterExpression = "ItemID = " + id;
            Methods.getObject('GetvItemMasterList', filterExpression, function (result) {
                if (result != null) {
                    Methods.getItemMasterPurchase(result.ItemID, 0, function (result2) {
                        $('#<%=hdnGCBaseUnit.ClientID %>').val(result2.ItemUnit);
                        $('#<%=hdnGCItemUnit.ClientID %>').val(result2.PurchaseUnit);
                    })
                    entityToControlItem(result);
                    cboItemUnit.PerformCallback();
                }
                else
                    entityToControlItem(null);
            });
        } else {
            entityToControlItem(null);
        }
    }

    function entityToControlItem(result) {
        if (result != null) {
            $('#<%=hdnItemName.ClientID %>').val(result.ItemName1);
            $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
        }

        else {
            $('#<%=hdnItemName.ClientID %>').val(null);
            $('#<%=hdnItemID.ClientID %>').val(null);
        }

    }
    //#endregion

    //#region cboItemUnit
    function onCboItemUnitEndCallBack() {
        if ($('#<%=hdnGCItemUnit.ClientID %>').val() == '') {
            cboItemUnit.SetValue($('#<%=hdnGCBaseUnit.ClientID %>').val());
        }
        else cboItemUnit.SetValue($('#<%=hdnGCItemUnit.ClientID %>').val());
        onCboItemUnitChanged();
    }

    function onCboItemUnitChanged() {
        var baseValue = $('#<%=hdnGCBaseUnit.ClientID %>').val();
        var toUnitItem = cboItemUnit.GetValue();
        var baseText = getItemUnitName(baseValue);

        if (baseValue == toUnitItem) {
            $('#<%=hdnItemUnitValue.ClientID %>').val('1');
            var conversion = "1 " + baseText + " = 1 " + baseText;
            $('#<%=txtConversion.ClientID %>').val(conversion);
        }
        else {
            var itemID = $('#<%=hdnItemID.ClientID %>').val();
            var filterExpression = "ItemID = " + itemID + " AND GCAlternateUnit = '" + toUnitItem + "'";
            Methods.getObjectValue('GetvItemAlternateUnitList', filterExpression, 'ConversionFactor', function (result) {
                var toConversion = getItemUnitName(toUnitItem);
                $('#<%=hdnItemUnitValue.ClientID %>').val(result);
                var conversion = "1 " + toConversion + " = " + result + " " + baseText;
                $('#<%=txtConversion.ClientID %>').val(conversion);
            });
        }
        var convertion = parseFloat($('#<%=hdnItemUnitValue.ClientID %>').val());
        var priceperitemunit = parseFloat(($('#<%=hdnPrice.ClientID %>').val()));
        var pricePerPurchaseUnit = convertion * priceperitemunit;
        //$('.txtFund_0').val(pricePerPurchaseUnit).trigger('changeValue');
    }

    function getItemUnitName(baseValue) {
        var value = cboItemUnit.GetValue();
        cboItemUnit.SetValue(baseValue);
        var text = cboItemUnit.GetText();
        cboItemUnit.SetValue(value);
        return text;
    }
    //#endregion

    //#region Paging
    function onCbpInfrastructureViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                var OrderID = s.cpOrderID;
                $('#divTransactionAdd').click();
                cbpInfrastructureView.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpInfrastructureView.PerformCallback('refresh');
        }
        else if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#infrastructureInformationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpInfrastructureView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#infrastructureInformationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion

    $('.txtInfrastructureFund').die('change');
    $('.txtInfrastructureFund').live('change', function () {
        $(this).trigger('changeValue');
        calculateTotalInfrastructureAmount();
    });

    $('.divDetailEdit.divInfrastructureEdit').die('click');
    $('.divDetailEdit.divInfrastructureEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnID.ClientID %>').val(entity.ProposedBudgetID);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
        $('#<%=txtProposedBudgetCode.ClientID %>').val(entity.ProposedBudgetCode);
        $('#<%=txtProposedBudgetCode.ClientID %>').attr('readonly', true);

        tacItem.setText(entity.ProposedBudgetName);
        tacItem.setValue(entity.ItemID);
        $('#<%=hdnItemName.ClientID %>').val(entity.ProposedBudgetName);
        $('#<%=hdnItemID.ClientID %>').val(entity.ItemID);
        cboItemUnit.PerformCallback();
        $('#<%=txtItemQuantity.ClientID %>').val(entity.Quantity);
        $('#<%=hdnGCBaseUnit.ClientID %>').val(entity.GCBaseUnit);
        var baseText = entity.BaseUnit;
        cboItemUnit.SetValue(entity.GCPurchaseUnit);
        var purchaseUnit = entity.PurchaseUnit;
        $('#<%=hdnItemUnitValue.ClientID %>').val(entity.ConversionFactor);
        var conversion = "1 " + baseText + " = " + entity.ConversionFactor + " " + purchaseUnit;
        $('#<%=txtConversion.ClientID %>').val(conversion);

        var listFund = entity.ListFund;
        var data = listFund.split('|');
        var count = 0;
        $('.txtInfrastructureFund').each(function () {
            $(this).val(data[count]).trigger('changeValue');
            count++;
        });

        calculateTotalInfrastructureAmount();
        $('#<%=txtEntryRemarks.ClientID %>').val(entity.Remarks);
        $('#containerEntryInfrastructure').show();
    });

    $('.divDetailDelete.divInfrastructureDelete').die('click');
    $('.divDetailDelete.divInfrastructureDelete').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
        cbpInfrastructureView.PerformCallback('delete');
    });

    function calculateTotalInfrastructureAmount() {
        var total = 0;
        $('.txtInfrastructureFund').each(function () {
            var value = parseFloat($(this).attr('hiddenVal'));
            total += value;
        });
        $('#<%=txtTotalLineAmount.ClientID %>').val(total).trigger('changeValue');
    }
</script>
<input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
<input type="hidden" id="hdnRecordFilterExpression" runat="server" />
<input type="hidden" id="hdnID" runat="server" value="" />
<input type="hidden" id="hdnEntryID" runat="server" value="" />
<input type="hidden" id="hdnLstFundItem" runat="server" value="" />
<input type="hidden" value="0" id="hdnPrice" runat="server" />
<input type="hidden" value="" id="hdnPageCount" runat="server" />
<input type="hidden" value="" id="hdnRowCount" runat="server" />
<input type="hidden" value="1" id="hdnIsEditable" runat="server" />
<div class="divTransactionEntry">
    <span id="divInfrastructureTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
    <div id="containerEntryInfrastructure" class="entryDetailContainer" style="display: none">
        <fieldset id="fsTrx" style="margin: 0">
            <table width="100%">
                <colgroup>
                    <col width="150px"/>
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                    <td><asp:TextBox runat="server" ID="txtProposedBudgetCode" Width="120px" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                    <td>
                        <input type="hidden" id="hdnItemName" value="" runat="server" />
                        <input type="hidden" id="hdnItemID" value="" runat="server" />
                        <input type="hidden" value="" id="hdnGCBaseUnit" runat="server" />
                        <input type="hidden" value="" id="hdnGCItemUnit" runat="server" />
                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacItem" ClientInstanceName="tacItem" MethodName="GetvItemMasterList" GetFilterExpressionFunction="onGetItemFilterExpression"
                            SearchFields="ItemName1,ItemCode" TextField="ItemName1" ValueField="ItemID" SearchText="${ItemName1} (<b>${ItemCode}</b>)" OrderByExpression="ItemName1">
                            <ClientSideEvents ButtonSearchClick="function(){ onTacItemButtonSearchClick(); }"
                                ValueChanged="function(){ onTacItemValueChanged(); }" />
                        </cdx:CodeXAutoCompleteTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah")%></label></td>
                    <td><asp:TextBox runat="server" ID="txtItemQuantity" Width="120px" CssClass="number" /></td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Satuan Item")%></label></td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="cboItemUnit" ClientInstanceName="cboItemUnit"
                            Width="300px" OnCallback="cboItemUnit_Callback">
                            <ClientSideEvents EndCallback="function(s,e){ onCboItemUnitEndCallBack(); }" ValueChanged="function(s,e){ onCboItemUnitChanged(); }" />
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Konversi")%></label></td>
                    <td>
                        <input type="hidden" value="" id="hdnItemUnitValue" runat="server" />
                        <asp:TextBox ID="txtConversion" Width="180px" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Realisasi")%></label></td>
                    <td><asp:TextBox ID="txtRealizationDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><label class="lblNormal"><%=GetLabel("Asal Dana")%></label></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="grdFund grdBorder" width="0">
                            <tr>
                                <asp:Repeater ID="rptFundHeader" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" width="100px"><%#:Eval("StandardCodeName") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptFundItem" runat="server" OnItemDataBound="rptFundItem_ItemDataBound">
                                    <ItemTemplate>
                                        <td><asp:TextBox ID="txtFundItem" runat="server" Width="120px" /></td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Total")%></label></td>
                    <td><asp:TextBox runat="server" ID="txtTotalLineAmount" CssClass="txtCurrency" ReadOnly="true" Width="120px" /></td>
                </tr>
                <tr>
                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                    <td><asp:TextBox runat="server" ID="txtEntryRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td> 
                        <input type="button" id="btnInfrastructureSave" class="btnWhite" value="Commit"/>
                        <input type="button" id="btnInfrastructureCancel" class="btnWhite" value="Cancel"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
<dxcp:ASPxCallbackPanel ID="cbpInfrastructureView" runat="server" Width="100%" ClientInstanceName="cbpInfrastructureView"
    ShowLoadingPanel="false" OnCallback="cbpInfrastructureView_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpInfrastructureViewEndCallback(s); }" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                position: relative;">
                <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                    <thead>
                        <tr>
                            <th class="keyField" rowspan="2">&nbsp;</th>
                            <th style="width:70px; text-align:left"><%=GetLabel("Kode")%></th>  
                            <th style="text-align:left"><%=GetLabel("Nama Anggaran")%></th>                              
                            <th style="width:250px;text-align:left"><%=GetLabel("Catatan")%></th>
                            <asp:Repeater runat="server" ID="rptViewHeader">
                                <ItemTemplate>
                                    <th style="width:100px; text-align:right"><%#:Eval("StandardCodeName") %></th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <th style="width:100px; text-align:right"><%=GetLabel("Total")%></th>
                            <th style="width:50px; text-align:center"></th>
                        </tr>
                    </thead>
                    <asp:Repeater runat="server" ID="grdInfrastructureView" OnItemDataBound="grdInfrastructureView_ItemDataBound">
                        <ItemTemplate>
                            <tbody>
                                <tr class="trData">
                                    <td class="keyField"><%#:Eval("ProposedBudgetDtID")%></td>
                                    <td><%#:Eval("ProposedBudgetCode")%></td>
                                    <td><%#:Eval("ProposedBudgetName")%></td>
                                    <td><%#:Eval("Remarks")%></td>
                                    <asp:Repeater runat="server" ID="rptViewItem">
                                        <ItemTemplate>
                                            <td align="right"><%# Convert.ToDecimal(Container.DataItem.ToString()).ToString("N") %></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td align="right"><%#:Eval("TotalAmount","{0:N}")%></td>
                                    <td>
                                        <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete divInfrastructureDelete"></div>
                                        <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit divInfrastructureEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("ProposedBudgetDtID") %>" bindingfield="ProposedBudgetDtID" />
                                        <input type="hidden" value="<%#Eval("ProposedBudgetID") %>" bindingfield="ProposedBudgetID" />
                                        <input type="hidden" value="<%#Eval("ProposedBudgetCode") %>" bindingfield="ProposedBudgetCode" />
                                        <input type="hidden" value="<%#Eval("ProposedBudgetName") %>" bindingfield="ProposedBudgetName" />
                                        <input type="hidden" value="<%#Eval("RealizationDateInDatePicker") %>" bindingfield="RealizationDateInDatePicker" />
                                        <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                        <input type="hidden" value="<%#Eval("Quantity") %>" bindingfield="Quantity" />
                                        <input type="hidden" value="<%#Eval("GCPurchaseUnit") %>" bindingfield="GCPurchaseUnit" />
                                        <input type="hidden" value="<%#Eval("PurchaseUnit") %>" bindingfield="PurchaseUnit" />
                                        <input type="hidden" value="<%#Eval("GCBaseUnit") %>" bindingfield="GCBaseUnit" />
                                        <input type="hidden" value="<%#Eval("BaseUnit") %>" bindingfield="BaseUnit" />
                                        <input type="hidden" value="<%#Eval("ConversionFactor") %>" bindingfield="ConversionFactor" />
                                        <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                        <input type="hidden" value="<%#Eval("ListFund") %>" bindingfield="ListFund" />
                                        <input type="hidden" value="<%#Eval("TotalAmount") %>" bindingfield="TotalAmount" />
                                    </td>
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
<div class="imgLoadingGrdView" id="containerImgLoadingView">
    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
</div>
<div class="containerPaging">
    <div class="divInformationNumEntries" id="infrastructureInformationNumEntries"></div>
    <div class="wrapperPaging">
        <div id="infrastructurePaging"></div>
    </div>
</div>