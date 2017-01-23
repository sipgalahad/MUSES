<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="ItemProductionEntry.aspx.cs" Inherits="CodeX.Ottimo.Web.Inventory.Program.ItemProductionEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtProductionDate.ClientID %>');
            $('#<%=txtProductionDate.ClientID %>').datepicker('option', 'maxDate', '0');

            //#region Production No
            $('#lblProductionNo.lblLink').click(function () {
                openSearchDialog('itemproductionhd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtProductionNo.ClientID %>').val(value);
                    onTxtDistributionNoChanged(value);
                });
            });

            $('#<%=txtProductionNo.ClientID %>').change(function () {
                onTxtDistributionNoChanged($(this).val());
            });

            function onTxtDistributionNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Location From
            function getLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionFromLocation() %>";
                return filterExpression;
            }

            $('#<%=lblLocation.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('locationroleuser', getLocationFilterExpression(), function (value) {
                    $('#<%=txtLocationCode.ClientID %>').val(value);
                    onTxtLocationCodeChanged(value);
                });
            });

            $('#<%=txtLocationCode.ClientID %>').live('change', function () {
                onTxtLocationCodeChanged($(this).val());
            });

            function onTxtLocationCodeChanged(value) {
                var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnLocationIDFrom.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                    }
                    else {
                        $('#<%=hdnLocationIDFrom.ClientID %>').val('');
                        $('#<%=txtLocationCode.ClientID %>').val('');
                        $('#<%=txtLocationName.ClientID %>').val('');
                    }
                    cbpView.PerformCallback();
                });
            }
            //#endregion

            //#region Location To
            function getLocationFilterExpressionTo() {
                var filterExpression = "<%=OnGetFilterExpressionToLocation() %>";
                return filterExpression;
            }

            $('#<%=lblLocationTo.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('locationroleuser', getLocationFilterExpressionTo(), function (value) {
                    $('#<%=txtLocationCodeTo.ClientID %>').val(value);
                    onTxtLocationToCodeChanged(value);
                });
            });

            $('#<%=txtLocationCodeTo.ClientID %>').live('change', function () {
                onTxtLocationToCodeChanged($(this).val());
            });

            function onTxtLocationToCodeChanged(value) {
                var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
                Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnLocationIDTo.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationNameTo.ClientID %>').val(result.LocationName);
                    }
                    else {
                        $('#<%=hdnLocationIDTo.ClientID %>').val('');
                        $('#<%=txtLocationCodeTo.ClientID %>').val('');
                        $('#<%=txtLocationNameTo.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Item
            function getItemProductFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemProduct() %>";
                return filterExpression;
            }

            $('#<%=lblItem.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('item', getItemProductFilterExpression(), function (value) {
                    $('#<%=txtItemCode.ClientID %>').val(value);
                    onTxtItemCodeChanged(value);
                });
            });

            $('#<%=txtItemCode.ClientID %>').live('change', function () {
                onTxtItemCodeChanged($(this).val());
            });

            function onTxtItemCodeChanged(value) {
                var filterExpression = getItemProductFilterExpression() + " AND ItemCode = '" + value + "'";
                Methods.getObject('GetItemMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemID.ClientID %>').val(result.ItemID);
                        $('#<%=txtItemName.ClientID %>').val(result.ItemName1);
                    }
                    else {
                        $('#<%=hdnItemID.ClientID %>').val('');
                        $('#<%=txtItemCode.ClientID %>').val('');
                        $('#<%=txtItemName.ClientID %>').val('');
                    }
                    cbpView.PerformCallback();
                });
            }
            //#endregion

            $('#<%=txtQuantity.ClientID %>').change(function () {
                var totalCostAmount = 0;
                $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                    var itemQty = parseFloat($(this).find('.hdnItemQuantity').val());
                    var BOMQty = parseFloat($(this).find('.hdnBOMQuantity').val());
                    var allQty = parseFloat($('#<%=txtQuantity.ClientID %>').val());

                    var qty = BOMQty / itemQty * allQty;
                    $(this).find('.txtQuantity').val(qty).trigger('changeValue');

                    var cost = parseFloat($(this).find('.hdnCostAmount').val()) * qty;
                    totalCostAmount += cost;
                    $(this).find('.txtCostAmount').val(cost).trigger('changeValue');
                });

                $('#<%=hdnTotalCostAmount.ClientID %>').val(totalCostAmount);
                $('#<%=chkIsFixedCost.ClientID %>').change();
            });

            $('#<%=chkIsFixedCost.ClientID %>').change(function () {
                if ($(this).is(':checked'))
                    $('#<%=txtFixedCostAmount.ClientID %>').removeAttr('readonly');
                else {
                    $('#<%=txtFixedCostAmount.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtFixedCostAmount.ClientID %>').val($('#<%=hdnTotalCostAmount.ClientID %>').val()).trigger('changeValue');
                }
            });

            $('#<%=grdView.ClientID %> .txtCurrency').each(function () {
                $(this).val(parseFloat($(this).val())).trigger('changeValue');
            });
            $('#<%=txtQuantity.ClientID %>').change();
            $('#<%=chkIsFixedCost.ClientID %>').change();
        }

        function onBeforeSaveRecord(errMessage) {
            $('#<%=grdView.ClientID %> .txtQuantity').each(function () {
                $(this).removeAttr('readonly');
            });
            var isValid = (IsValid(null, 'fsMPEntry', 'mpEntry'))
            $('#<%=grdView.ClientID %> .txtQuantity').each(function () {
                $(this).attr('readonly', 'readonly');
            });
            return isValid;
        }

        function onCbpViewEndCallback(s) {
            $('#<%=grdView.ClientID %> .txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            hideLoadingPanel();
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var productionID = $('#<%=hdnProductionID.ClientID %>').val();
            if (productionID == '' || productionID == '0') {
                errMessage.text = 'Please Set Transaction First!';
                return false;
            }
            else {
                filterExpression.text = "ProductionID = " + productionID;
                return true;
            }
        }
    </script>
    <input type="hidden" value="" id="hdnProductionID" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
    <input type="hidden" value="" id="hdnTotalCostAmount" runat="server" />
    <div style="height: 495px; overflow-y: auto; overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 50%" />
                <col style="width: 50%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblProductionNo"><%=GetLabel("No. Produksi")%></label></td>
                            <td><asp:TextBox ID="txtProductionNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocation"><%=GetLabel("Dari Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationIDFrom" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblItem"><%=GetLabel("Item")%></label></td>
                            <td>
                                <input type="hidden" id="hdnItemID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtItemCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Jumlah") %></td>
                            <td><asp:TextBox ID="txtQuantity" Width="60px" CssClass="number min" min="0" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Harga Satuan") %></td>
                            <td><asp:TextBox ID="txtUnitPrice" Width="120px" CssClass="txtCurrency" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>    
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><asp:CheckBox ID="chkIsFixedCost" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Fixed Cost")%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Cost Amount") %></td>
                            <td><asp:TextBox ID="txtFixedCostAmount" Width="120px" CssClass="txtCurrency" runat="server" /></td>
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
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td><asp:TextBox ID="txtProductionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocationTo"><%=GetLabel("Kepada Lokasi")%></label></td>
                            <td>
                                <input type="hidden" id="hdnLocationIDTo" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLocationCodeTo" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLocationNameTo" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Batch Number") %></td>
                            <td><asp:TextBox ID="txtBatchNumber" Width="250px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("No Referensi") %></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="250px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                        OnRowDataBound="grdView_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="BillOfMaterialID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="BillOfMaterialCode" HeaderText="BOM Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="160px" />
                                            <asp:BoundField DataField="BillOfMaterialName1" HeaderText="BOM Name" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" />
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Stok" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <div id="divRemainingStock" runat="server"></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <input type="hidden" value="<%#Eval("ItemQuantity") %>" class="hdnItemQuantity" />
                                                    <input type="hidden" value="<%#Eval("BOMQuantity") %>" class="hdnBOMQuantity" />
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtCurrency txtQuantity" ReadOnly="true" Width="145px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Cost Amount" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <input type="hidden" class="hdnCostAmount" runat="server" id="hdnCostAmount" />    
                                                    <asp:TextBox ID="txtCostAmount" runat="server" CssClass="txtCurrency txtCostAmount" ReadOnly="true" Width="195px" />
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
