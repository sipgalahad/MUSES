<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master"
    AutoEventWireup="true" CodeBehind="PurchaseRequestReorder.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.PurchaseRequestReorder" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnReorderPurchaseRequestProcess" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/list.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Process")%></div></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">   
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            onCboReorderTypeValueChanged();

            setDatePicker('<%=txtPurchaseRequestDate.ClientID %>');
            $('#<%=txtPurchaseRequestDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#<%=btnReorderPurchaseRequestProcess.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    getCheckedMember();
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                        showToast('Warning', 'Please Select Item First');
                    }
                    else {
                        onCustomButtonClick('approve');
                    }
                }
            });

            $('#<%=txtItemName.ClientID %>').keydown(function (e) {
                if (e.keyCode == 13) { //Enter
                    getCheckedMember();
                    cbpView.PerformCallback('refresh');
                    e.preventDefault();
                }
            });

            //#region Service Unit
            function getLocationFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionLocation() %>";
                return filterExpression;
            }

            function getServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', getServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtServiceUnitCode.ClientID %>').val(value);
                    onTxtServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtServiceUnitCode.ClientID %>').live('change', function () {
                onTxtServiceUnitCodeChanged($(this).val());
            });

            function onTxtServiceUnitCodeChanged(value) {
                var filterExpression = getServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtServiceUnitName.ClientID %>').val(result.ServiceUnitName);

                        var filterExpression = getLocationFilterExpression() + "LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = '" + result.SiteServiceUnitID + "')";
                        Methods.getListObject('GetLocationUserAccessList', filterExpression, function (result) {
                            var lstLocationID = '';
                            for (var i = 0; i < result.length; ++i) {
                                if (lstLocationID != '')
                                    lstLocationID += ',';
                                lstLocationID += result[i].LocationID;
                            }
                            var filterExpression = "LocationID IN (" + lstLocationID + ")";
                            Methods.getListObject('GetLocationItemGroupList', filterExpression, function (result) {
                                var filterLocationItemGroup = '';
                                for (var i = 0; i < result.length; ++i) {
                                    if (filterLocationItemGroup != '')
                                        filterLocationItemGroup += ' OR ';
                                    filterLocationItemGroup += "DisplayPath LIKE '%/" + result[i].ItemGroupID + "/%'";
                                }
                                if (filterLocationItemGroup != '')
                                    $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                                else
                                    $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val("");

                                cbpLocation.PerformCallback();
                            });
                        });
                    }
                    else {
                        $('#<%=hdnSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtServiceUnitName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region To Service Unit
            function getToServiceUnitFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionToServiceUnit() %>";
                return filterExpression;
            }

            $('#<%=lblToSiteServiceUnit.ClientID %>.lblLink').live('click', function () {
                openSearchDialog('serviceunitpersite', getToServiceUnitFilterExpression(), function (value) {
                    $('#<%=txtToServiceUnitCode.ClientID %>').val(value);
                    ontxtToServiceUnitCodeChanged(value);
                });
            });

            $('#<%=txtToServiceUnitCode.ClientID %>').live('change', function () {
                ontxtToServiceUnitCodeChanged($(this).val());
            });

            function ontxtToServiceUnitCodeChanged(value) {
                var filterExpression = getToServiceUnitFilterExpression() + " AND ServiceUnitCode = '" + value + "'";
                Methods.getObject('GetvSiteServiceUnitList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnToSiteServiceUnitID.ClientID %>').val(result.SiteServiceUnitID);
                        $('#<%=txtToServiceUnitName.ClientID %>').val(result.ServiceUnitName);

                        var filterExpression = "SiteServiceUnitID = " + result.SiteServiceUnitID + " AND <%=OnGetFilterExpressionItemGroup() %>";
                        Methods.getListObject('GetvServiceUnitItemGroupList', filterExpression, function (result) {
                            var filterLocationItemGroup = '';
                            for (var i = 0; i < result.length; ++i) {
                                if (filterLocationItemGroup != '')
                                    filterLocationItemGroup += ' OR ';
                                filterLocationItemGroup += "DisplayPath LIKE '%/" + result[i].ItemGroupID + "/%'";
                            }
                            if (filterLocationItemGroup != '')
                                $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val("(" + filterLocationItemGroup + ")");
                            else
                                $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val("");
                        });
                    }
                    else {
                        $('#<%=hdnToSiteServiceUnitID.ClientID %>').val('');
                        $('#<%=txtToServiceUnitCode.ClientID %>').val('');
                        $('#<%=txtToServiceUnitName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Item Group
            function onGetItemGroupFilterExpression() {
                var filterExpression = "<%=OnGetFilterExpressionItemGroup() %>";
                if ($('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterLocationItemGroup.ClientID %>').val() + ")";
                if ($('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() != '')
                    filterExpression += " AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE " + $('#<%=hdnLstFilterToLocationItemGroup.ClientID %>').val() + ")";
                return filterExpression;
            }

            $('#lblItemGroup.lblLink').live('click', function () {
                openSearchDialog('itemgroup', onGetItemGroupFilterExpression(), function (value) {
                    $('#<%=txtItemGroupCode.ClientID %>').val(value);
                    onTxtItemGroupCodeChanged(value);
                });
            });

            $('#<%=txtItemGroupCode.ClientID %>').live('change', function () {
                onTxtItemGroupCodeChanged($(this).val());
            });

            function onTxtItemGroupCodeChanged(value) {
                var filterExpression = onGetItemGroupFilterExpression() + " AND ItemGroupCode = '" + value + "'";
                Methods.getObject('GetItemGroupMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                    }
                    else {
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
            });

            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            });

            setDdeLocationText();

            $('#btnRefresh').click();

            //#region Order No
            $('#lblOrderNo.lblLink').click(function () {
                openSearchDialog('purchaserequesthd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtOrderNo.ClientID %>').val(value);
                    onTxtOrderNoChanged(value);
                });
            });

            $('#<%=txtOrderNo.ClientID %>').change(function () {
                onTxtOrderNoChanged($(this).val());
            });

            function onTxtOrderNoChanged(value) {
                var filterExpression = "PurchaseRequestNo = '" + value + "' AND <%=GetFilterExpression() %>";
                Methods.getObject('GetvPurchaseRequestHdList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=hdnOrderID.ClientID %>').val(result.PurchaseRequestID);
                    else
                        $('#<%=hdnOrderID.ClientID %>').val('');
                });
            }
            //#endregion
        }

        $('.lblEndingBalance').live('click', function () {
            var itemID = $(this).closest('tr').parent().closest('tr').find('.keyField').html();
            var locationID = $('#<%=hdnLstLocationID.ClientID %>').val();
            if (itemID != '' && locationID != '') {
                var param = itemID + '|' + locationID;
                var url = ResolveUrl("~/Program/Information/ItemBalanceDtCtl.ascx");
                openUserControlPopup(url, param, 'Item Per Lokasi', 700, 500);
            }
        });

        $('.lblQtyOnOrder').live('click', function () {
            var itemID = $(this).closest('tr').parent().closest('tr').find('.keyField').html();
            var siteServiceUnitID = $('#<%=hdnSiteServiceUnitID.ClientID %>').val();
            var param = siteServiceUnitID + '|' + itemID;
            var url = ResolveUrl("~/Program/Procurement/PurchaseRequest/PurchaseRequestQtyOnOrderCtl.ascx");
            openUserControlPopup(url, param, 'Qty On Process', 1100, 500);
        });

        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        $('.chkIsSelected input').live('change', function () {
            $tr = $(this).closest('tr');
            $lblItemUnit = $tr.find('.lblItemUnit');
            if ($(this).is(':checked')) {
                $tr.find('.txtQty').removeAttr('readonly');
                $lblItemUnit.removeClass('lblDisabled');
                $lblItemUnit.addClass('lblLink');
            }
            else {
                $tr.find('.txtQty').attr('readonly', 'readonly');
                $lblItemUnit.removeClass('lblLink');
                $lblItemUnit.addClass('lblDisabled');
            }
        });

        function onAfterCustomClickSuccess(type, retval) {
            var message = '';
            if ($('#<%=hdnOrderID.ClientID %>').val() != '')
                message = 'Permintaan Pembelian Berhasil Ditambahkan Ke No Permintaan <b>' + $('#<%=txtOrderNo.ClientID %>').val() + '</b>';
            else
                message = 'Permintaan Pembelian Berhasil Dibuat Dengan No Permintaan <b>' + retval + '</b>';
            showToast('Save Success', message, function () {
                $('#<%=hdnListGCItemUnit.ClientID %>').val('');
                $('#<%=hdnListItemUnit.ClientID %>').val('');
                $('#<%=hdnListConversionFactor.ClientID %>').val('');
                $('#<%=hdnListQty.ClientID %>').val('');
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                cbpView.PerformCallback('refresh');
            });
        }

        function getCheckedMember() {
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split('|');
            var lstQty = $('#<%=hdnListQty.ClientID %>').val().split('|');
            var lstGCItemUnit = $('#<%=hdnListGCItemUnit.ClientID %>').val().split('|');
            var lstItemUnit = $('#<%=hdnListItemUnit.ClientID %>').val().split('|'); 
            var lstConversionFactor = $('#<%=hdnListConversionFactor.ClientID %>').val().split('|');
            var result = '';
            $grdView = null;
            if (cboReorderType.GetValue() == '<%=OnGetReorderTypeStatic() %>')
                $grdView = $('#<%=grdView.ClientID %>');
            else
                $grdView = $('#<%=grdView2.ClientID %>');
            $grdView.find('.chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').html();
                    var qty = $tr.find('.txtQty').val();
                    var conversionFactor = $tr.find('.hdnConversionFactor').val();
                    var GCItemUnit = $tr.find('.hdnGCItemUnit').val();
                    var itemUnit = $tr.find('.hdnItemUnit').val();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx < 0) {
                        lstSelectedMember.push(key);
                        lstQty.push(qty);
                        lstConversionFactor.push(conversionFactor);
                        lstItemUnit.push(itemUnit);
                        lstGCItemUnit.push(GCItemUnit);
                    }
                    else {
                        lstQty[idx] = qty;
                        lstConversionFactor[idx] = conversionFactor;
                        lstGCItemUnit[idx] = GCItemUnit;
                        lstItemUnit[idx] = itemUnit;
                    }
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').html();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx > -1) {
                        lstSelectedMember.splice(idx, 1);
                        lstQty.splice(idx, 1);
                        lstGCItemUnit.splice(idx, 1);
                        lstItemUnit.splice(idx, 1);
                        lstConversionFactor.splice(idx, 1);
                    }
                }
            });
            $('#<%=hdnListGCItemUnit.ClientID %>').val(lstGCItemUnit.join('|'));
            $('#<%=hdnListItemUnit.ClientID %>').val(lstItemUnit.join('|'));
            $('#<%=hdnListConversionFactor.ClientID %>').val(lstConversionFactor.join('|'));
            $('#<%=hdnListQty.ClientID %>').val(lstQty.join('|'));
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join('|'));
        }

        $('.txtQty').live('change', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            var conversionFactor = parseFloat($tr.find('.hdnConversionFactor').val());
            var qty = parseFloat($tr.find('.txtQty').val());
            $tr.find('.txtTotalQty').val(qty * conversionFactor);
        });

        //#region Item Unit
        function getItemUnitFilterExpression() {
            var filterExpression = "ItemID = " + itemID;
            return filterExpression;
        }

        var itemID = 0;
        $('.lblItemUnit.lblLink').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            itemID = $tr.find('.keyField').html();
            openSearchDialog('itemalternateunit', getItemUnitFilterExpression(), function (value) {
                onTxtItemUnitChanged(value);
            });
        });

        function onTxtItemUnitChanged(value) {
            var temp = value.split('|');
            var filterExpression = getItemUnitFilterExpression() + " AND GCAlternateUnit = '" + temp[0] + "' AND ConversionFactor = " + temp[1];
            Methods.getObject('GetvItemAlternateUnitCustomList', filterExpression, function (result) {
                if (result != null) {
                    $tr.find('.hdnGCItemUnit').val(result.GCAlternateUnit);
                    $tr.find('.hdnItemUnit').val(result.AlternateUnit);
                    $tr.find('.lblItemUnit').html(result.cfAlternateUnit);
                    $tr.find('.hdnConversionFactor').val(result.ConversionFactor);
                }
                else {
                    $tr.find('.hdnGCItemUnit').val('');
                    $tr.find('.hdnItemUnit').val('');
                    $tr.find('.lblItemUnit').html('');
                    $tr.find('.hdnConversionFactor').val('');
                }
                var conversionFactor = parseFloat($tr.find('.hdnConversionFactor').val());
                var qty = parseFloat($tr.find('.txtQty').val());
                $tr.find('.txtTotalQty').val(qty * conversionFactor);
            });
        }
        //#endregion

        $(function () {
            $('.chkLocation input').change(function () {
                setDdeLocationText();
            });
        });

        function setDdeLocationText() {
            var lstLocationID = '';
            var lstLocationName = '';
            $('.chkLocation input:checked').each(function () {
                if (lstLocationName != '') {
                    lstLocationName += ', ';
                    lstLocationID += ',';
                }
                lstLocationID += $(this).parent().attr('locationid');
                lstLocationName += $(this).parent().attr('locationname');
            });
            $('#<%=hdnLstLocationID.ClientID %>').val(lstLocationID);
            ddeLocation.SetText(lstLocationName);
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
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });

            }
        }
        //#endregion

        function onCbpLocationEndCallback(s) {
            hideLoadingPanel();
            setDdeLocationText();
        }

        function onCboReorderTypeValueChanged() {
            if (cboReorderType.GetValue() == '<%=OnGetReorderTypeStatic() %>') {
                $('#trViewTypeDynamic').attr('style', 'display:none');
                $('#trViewTypeStatic').removeAttr('style');
            }
            else {
                $('#trViewTypeDynamic').removeAttr('style');
                $('#trViewTypeStatic').attr('style', 'display:none');
            }
        }
    </script>
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" id="hdnOrderID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnListQty" runat="server" value="" />
    <input type="hidden" id="hdnListGCItemUnit" runat="server" value="" />
    <input type="hidden" id="hdnListItemUnit" runat="server" value="" />
    <input type="hidden" id="hdnListConversionFactor" runat="server" value="" />
    <input type="hidden" value="" id="hdnListSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnLstLocationID" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterLocationItemGroup" runat="server" />
    <input type="hidden" value="" id="hdnLstFilterToLocationItemGroup" runat="server" />
    <input type="hidden" value="" id="hdnDefaultSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToSiteServiceUnitID" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitCode" runat="server" />
    <input type="hidden" value="" id="hdnDefaultToServiceUnitName" runat="server" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
    <div style="overflow-x: hidden;">
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
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblOrderNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtOrderNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblSiteServiceUnit"><%=GetLabel("Dari Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtServiceUnitCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" runat="server" id="lblToSiteServiceUnit"><%=GetLabel("Ke Bagian")%></label></td>
                            <td>
                                <input type="hidden" id="hdnToSiteServiceUnitID" value="" runat="server" />
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col style="width: 3px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtToServiceUnitCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtToServiceUnitName" Width="100%" runat="server" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Lokasi")%></label></td>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="cbpLocation" runat="server" Width="100%" ClientInstanceName="cbpLocation"
                                    ShowLoadingPanel="false" OnCallback="cbpLocation_Callback">
                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpLocationEndCallback(s); }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                            <dxe:ASPxDropDownEdit ClientInstanceName="ddeLocation" ID="ddeLocation"
                                                Width="300px" runat="server" EnableAnimation="False">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <asp:Repeater ID="rptLocation" runat="server" OnItemDataBound="rptLocation_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkLocation" CssClass="chkLocation" runat="server"  /> <%#Eval("LocationName") %><br />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </DropDownWindowTemplate>
                                            </dxe:ASPxDropDownEdit>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Reorder")%></label></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboReorderType" ClientInstanceName="cboReorderType" Width="300px">
                                    <ClientSideEvents ValueChanged="function(s,e){ onCboReorderTypeValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr id="trViewTypeDynamic">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tampilan")%></label></td>
                            <td><dxe:ASPxComboBox runat="server" ID="cboViewType" ClientInstanceName="cboViewType" Width="300px" /></td>
                        </tr>
                        <tr id="trViewTypeStatic">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tampilan")%></label></td>
                            <td><dxe:ASPxComboBox runat="server" ID="cboViewTypeStatic" ClientInstanceName="cboViewTypeStatic" Width="300px" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><input type="button" id="btnRefresh" value='<%=GetLabel("Refresh") %>' /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %> - <%=GetLabel("Waktu") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtPurchaseRequestDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtPurchaseRequestTime" Width="100px" CssClass="time" runat="server" Style="text-align: center" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Persediaan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboPurchaseOrderType" ClientInstanceName="cboPurchaseOrderType" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top:5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblItemGroup"><%=GetLabel("Kelompok Item")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnItemGroupID" runat="server" />
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width: 120px" />
                                        <col style="width: 3px" />
                                        <col style="width: 250px" />
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtItemGroupCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemGroupName" ReadOnly="true" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Nama Item")%></label></td>
                            <td><asp:TextBox runat="server" ID="txtItemName" Width="300px" /></td>
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
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                        OnRowDataBound="grdView_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <input id="chkSelectAll" type="checkbox" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item"/>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Minimum" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><div id="divMinimum" runat="server"></div></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Maximum" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><div id="divMaximum" runat="server"></div></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Stok Saat Ini" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><label id="lblEndingBalance" runat="server" class="lblLink lblEndingBalance"></label></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Diminta" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <input type="hidden" value="0" class="hdnGCItemUnit" id="hdnGCItemUnit" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnItemUnit" id="hdnItemUnit" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnConversionFactor" id="hdnConversionFactor" runat="server"/>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><asp:TextBox ID="txtQty" Width="100%" runat="server" CssClass="number txtQty" ReadOnly="true"/></td>
                                                            <td>&nbsp<label runat="server" id="lblItemUnit" class="lblItemUnit"></label></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Total Diminta" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><asp:TextBox ID="txtTotalQty" Width="100%" runat="server" CssClass="number txtTotalQty" ReadOnly="true"/></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Quantity On Order" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><label id="lblQtyOnOrder" runat="server" class="lblLink lblQtyOnOrder"></label></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlView2" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                        OnRowDataBound="grdView2_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <input id="chkSelectAll" type="checkbox" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" HeaderStyle-Width="350px" />
                                            <asp:BoundField DataField="NDaysBackward" HeaderStyle-CssClass="thRight" HeaderText="Backward (Hari)" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="NDaysForward" HeaderStyle-CssClass="thRight" HeaderText="Forward (Hari)" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Stok Saat Ini" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><label id="lblEndingBalance" runat="server" class="lblLink lblEndingBalance"></label></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Qty Rata-Rata / Hari" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><%#Eval("AvgQuantityOut", "{0:N}")%></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Diminta" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <input type="hidden" value="0" class="hdnGCItemUnit" id="hdnGCItemUnit" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnItemUnit" id="hdnItemUnit" runat="server"/>
                                                    <input type="hidden" value="0" class="hdnConversionFactor" id="hdnConversionFactor" runat="server"/>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><asp:TextBox ID="txtQty" Width="100%" runat="server" CssClass="number txtQty" ReadOnly="true"/></td>
                                                            <td>&nbsp<label runat="server" id="lblItemUnit" class="lblItemUnit"></label></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="thCenter" HeaderText="Total Diminta" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><asp:TextBox ID="txtTotalQty" Width="100%" runat="server" CssClass="number txtTotalQty" ReadOnly="true"/></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity On Order" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" >
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:60px" />
                                                        </colgroup>
                                                        <tr>
                                                            <td align="right" class="lblReadOnlyText"><label id="lblQtyOnOrder" runat="server" class="lblLink lblQtyOnOrder"></label></td>
                                                            <td>&nbsp<%# Eval("ItemUnit")%></td>
                                                        </tr>
                                                    </table>  
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
                    <div class="containerPaging">
                        <div class="divInformationNumEntries" id="informationNumEntries"></div>
                        <div class="wrapperPaging">
                            <div id="paging">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
