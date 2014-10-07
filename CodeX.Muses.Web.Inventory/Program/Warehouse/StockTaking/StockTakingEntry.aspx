<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="StockTakingEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.StockTakingEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=txtFormDate.ClientID %>').attr('readonly') == null) {
                setDatePicker('<%=txtFormDate.ClientID %>');
                $('#<%=txtFormDate.ClientID %>').datepicker('option', 'minDate', '0');
            }

            //#region Stock Taking No
            $('#lblStockTakingNo.lblLink').click(function () {
                openSearchDialog('stocktakinghd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtStockTakingNo.ClientID %>').val(value);
                    onTxtStockTakingNoChanged(value);
                });
            });

            $('#<%=txtStockTakingNo.ClientID %>').change(function () {
                onTxtStockTakingNoChanged($(this).val());
            });

            function onTxtStockTakingNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            //#region Location
            function getLocationFilterExpression() {
                var filterExpression = "<%=GetLocationFilterExpression() %>";
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
                        $('#<%=hdnLocationID.ClientID %>').val(result.LocationID);
                        $('#<%=txtLocationName.ClientID %>').val(result.LocationName);
                    }
                    else {
                        $('#<%=hdnLocationID.ClientID %>').val('');
                        $('#<%=txtLocationCode.ClientID %>').val('');
                        $('#<%=txtLocationName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#<%=btnStartCalculate.ClientID %>').click(function () {
                cbpProcess.PerformCallback('calculate');
            });

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'calculate') {
                if (param[1] == 'fail')
                    showToast('Proses Hitung Gagal', 'Error Message : ' + param[2]);
                else {
                    $('#<%=btnStartCalculate.ClientID %>').attr('enabled', 'false');
                    cbpView.PerformCallback('refresh');
                }
            }
            else {
                var result = s.cpResult.split('|');
                if (result[1] == 'success') {
                    $tr = $btnSave.closest('tr');
                    $btnSave.attr('enabled', 'false');
                    cboCheckCountType.SetEnabled(false);
                    $lblExpiredDate.attr('class', 'lblExpiredDate lblDisabled');
                }
                else {
                    if (result[2] != '')
                        showToast('Save Failed', 'Error Message : ' + result[2]);
                    else
                        showToast('Save Failed', '');
                }
            }
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });

            }

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }
        //#endregion

        function onAfterSaveAddRecord(param) {
            $('#<%=btnStartCalculate.ClientID %>').removeAttr('enabled');
        }

        $('.txtAdjustment').live('change', function () {
            var qtyBSO = parseFloat($(this).parent().find('.hdnQuantityBSO').val());
            var adjustment = parseFloat($(this).val());
            $tr = $(this).closest('tr');
            $tr.find('.txtQuantityEND').val(qtyBSO + adjustment).trigger('changeValue');
            $tr.find('.btnSave').removeAttr('enabled');
            var idx = $tr.find('.hdnItemIndex').val();
            cboCheckCountType = eval('cboCheckCountType' + idx);
            cboCheckCountType.SetEnabled(true);
            if (cboCheckCountType.GetValue() == null)
                cboCheckCountType.SetValue($('#<%=hdnDefaultCycleCountType.ClientID %>').val());
        });

        $('.txtQuantityEND').live('change', function () {
            $tr = $(this).closest('tr');
            var qtyBSO = parseFloat($tr.find('.hdnQuantityBSO').val());
            var qtyEnd = parseFloat($(this).val());
            $tr.find('.txtAdjustment').val(qtyEnd - qtyBSO).trigger('changeValue');
            $tr.find('.btnSave').removeAttr('enabled');
            var idx = $tr.find('.hdnItemIndex').val();
            cboCheckCountType = eval('cboCheckCountType' + idx);
            cboCheckCountType.SetEnabled(true);
            if (cboCheckCountType.GetValue() == null)
                cboCheckCountType.SetValue($('#<%=hdnDefaultCycleCountType.ClientID %>').val());
        });

        var cboCheckCountType = null;
        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                $tr = $(this).closest('tr');
                var itemID = $tr.find('.keyField').html();
                var adjustment = $tr.find('.txtAdjustment').val();
                $txtQuantityEND = $tr.find('.txtQuantityEND');
                var quantityEND = $txtQuantityEND.attr('hiddenVal');
                if (quantityEND > 0) {
                    $txtQuantityEND.removeClass('error');
                    var idx = $tr.find('.hdnItemIndex').val();
                    cboCheckCountType = eval('cboCheckCountType' + idx);

                    var checkCountType = '';
                    if (cboCheckCountType.GetValue() != null)
                        checkCountType = cboCheckCountType.GetValue();
                    var param = 'save|' + itemID + '|' + adjustment + '|' + quantityEND + '|' + checkCountType;
                    $btnSave = $(this);
                    cbpProcess.PerformCallback(param);
                }
                else
                    $txtQuantityEND.addClass('error');
            }
        });

        function onRefreshGridView() {
            var filterExpression = txtSearchView.GenerateFilterExpression();
            //if (typeof onRefreshControl == 'function')
            //onRefreshControl(filterExpression);
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
            }, 0);
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var stockTakingID = $('#<%=hdnStockTakingID.ClientID %>').val();
            if (stockTakingID == '' || stockTakingID == '0') {
                errMessage.text = 'Please Set Transaction First!';
                return false;
            }
            else {
                filterExpression.text = "StockTakingID = " + stockTakingID;
                return true;
            }
        }

        $lblExpiredDate = null;
        $('.lblExpiredDate').die('click');
        $('.lblExpiredDate').live('click', function () {
            $lblExpiredDate = $(this);
            $tr = $(this).closest('tr');
            var itemID = $tr.find('.keyField').html();
            var hdnStockTakingID = $('#<%=hdnStockTakingID.ClientID %>').val();

            $txtQuantityEND = $tr.find('.txtQuantityEND');
            var quantityEND = $txtQuantityEND.attr('hiddenVal');

            var param = hdnStockTakingID + '|' + itemID + '|' + quantityEND;
            var url = ResolveUrl("~/Program/WareHouse/StockTaking/StockTakingExpiredDateCtl.ascx");
            openUserControlPopup(url, param, 'Expired Date Per Item', 550, 450);
        });

    </script>
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnFilterExpression" runat="server" />
    <input type="hidden" value="" id="hdnDefaultCycleCountType" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td valign="top">
                <input type="hidden" id="hdnStockTakingID" value="0" runat="server" />
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 30%" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblStockTakingNo"><%=GetLabel("No Bukti Opname")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 30%" />
                                    <col style="width: 3px" />
                                    <col style="width: 250px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStockTakingNo" Width="150px" runat="server" TabIndex="1" /></td>
                                    <td>&nbsp;</td>
                                    <td><input type="button" runat="server" id="btnStartCalculate" value="Mulai Hitung Stok Fisik" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Opname") %></label></td>
                        <td><asp:TextBox ID="txtFormDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblLocation"><%=GetLabel("Lokasi")%></label></td>
                        <td>
                            <input type="hidden" id="hdnLocationID" value="" runat="server" />
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width: 30%" />
                                    <col style="width: 3px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtLocationCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtLocationName" Width="100%" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col style="width: 30%" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Quick Search")%></label></td>
                        <td>
                            <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView"
                                Width="300px" Watermark="Search">
                                <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
                                <IntellisenseHints>
                                    <qis:QISIntellisenseHint Text="Kode Item" FieldName="ItemCode" />
                                    <qis:QISIntellisenseHint Text="Nama Item" FieldName="ItemName1" />
                                </IntellisenseHints>
                            </qis:QISIntellisenseTextBox>
                        </td>
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
                            <asp:Panel runat="server" ID="pnlGridView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:365px;overflow-y:scroll;">
                                <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                    <EmptyDataTemplate>
                                        <table id="tblView" runat="server" class="grdSelected grdBorder" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2" style="width:80px"><%=GetLabel("Kode")%></th>
                                                <th rowspan="2" style="width:150px"><%=GetLabel("Nama Item")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Quantity")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Expired Date")%></th>
                                                <th rowspan="2" class="thCenter" style="width:140px"><%=GetLabel("Check Count Type")%></th>
                                                <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Simpan")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:100px" class="thCenter"><%=GetLabel("Qty BSO")%></th>  
                                                <th style="width:120px" class="thCenter"><%=GetLabel("Selisih")%></th>  
                                                <th style="width:120px" class="thCenter"><%=GetLabel("Qty Akhir")%></th>  
                                                <th style="width:80px" class="thCenter"><%=GetLabel("Satuan Kecil")%></th>  
                                                <th style="width:80px" class="thCenter"><%=GetLabel("Satuan Besar")%></th>  
                                                <th style="width:150px" class="thCenter"><%=GetLabel("Konversi")%></th>  
                                            </tr>
                                            <tr class="trEmpty">
                                                <td colspan="10">
                                                    <%=GetLabel("No Data To Display")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tblView" runat="server" class="grdStokTaking grdSelected grdBorder" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2" style="width:80px"><%=GetLabel("Kode")%></th>
                                                <th rowspan="2" style="width:150px"><%=GetLabel("Nama Item")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Quantity")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:120px"><%=GetLabel("Expired Date")%></th>
                                                <th rowspan="2" class="thCenter" style="width:140px"><%=GetLabel("Check Count Type")%></th>
                                                <th rowspan="2" class="thCenter" style="width:80px"><%=GetLabel("Simpan")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:100px" class="thCenter"><%=GetLabel("Qty BSO")%></th>  
                                                <th style="width:120px" class="thCenter"><%=GetLabel("Selisih")%></th>  
                                                <th style="width:120px" class="thCenter"><%=GetLabel("Qty Akhir")%></th>  
                                                <th style="width:80px" class="thCenter"><%=GetLabel("Satuan Kecil")%></th>  
                                                <th style="width:80px" class="thCenter"><%=GetLabel("Satuan Besar")%></th>  
                                                <th style="width:150px" class="thCenter"><%=GetLabel("Konversi")%></th>  
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder" ></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%# Eval("ItemID")%></td>
                                            <td><%# Eval("ItemCode")%></td>
                                            <td><%# Eval("ItemName1")%></td>
                                            <td align="right"><%# Eval("QuantityBSO")%></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                                <input type="hidden" class="hdnQuantityBSO" value='<%#Eval("QuantityBSO") %>' />
                                                <input type="text" runat="server" value='<%#Eval("QuantityAdjustment") %>' id="txtAdjustment" class="txtAdjustment number" style="width:100%" />
                                            </td>
                                            <td><input type="text" class="txtQuantityEND txtCurrency min" min="0" id="txtQuantityEND" runat="server" style="width:100%" /></td>
                                            <td><%# Eval("ItemUnit")%></td>
                                            <td><div id="divPurchaseUnit" runat="server"></div></td>
                                            <td align="center"><div id="divConversionFactor" runat="server"></div></td>
                                            <td align="center">
                                                <input type="hidden" runat="server" id="hdnControlExpired" class="hdnControlExpired" value='<%#Eval("IsControlExpired") %>' />
                                                <label id="lblExpiredDate" runat="server" class="lblExpiredDate lblLink" ><%=GetLabel("Expired Date") %></label>
                                            </td>
                                            <td align="center"><dxe:ASPxComboBox ID="cboCheckCountType" ClientEnabled="false" runat="server" Width="90%" /></td>
                                            <td align="center"><input type="button" id="btnSave" class="btnSave" enabled="false" value="Simpan" runat="server" /></td>
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
            </td>
        </tr>
    </table>
    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
