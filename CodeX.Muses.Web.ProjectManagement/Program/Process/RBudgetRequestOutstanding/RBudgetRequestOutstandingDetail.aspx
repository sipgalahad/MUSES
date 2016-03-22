<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true"
    CodeBehind="RBudgetRequestOutstandingDetail.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RBudgetRequestOutstandingDetail" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnItemReqHdProcess" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/list.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Process")%></div></li>
    <li id="btnItemReqHdDecline" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Decline")%></div></li>
    <li id="btnOrderListBack" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><div><%=GetLabel("Back")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtRequestDate.ClientID %>');
            $('#<%=btnItemReqHdDecline.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                    showToast('Warning', 'Please Select Item First');
                }
                else {
                    onCustomButtonClick('decline');
                }
            });

            $('#<%=btnItemReqHdProcess.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    getCheckedMember();
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                        showToast('Warning', 'Please Select Item First');
                    else 
                        onCustomButtonClick('approve');
                }
            });

            $('#<%=btnOrderListBack.ClientID %>').click(function () {
                showLoadingPanel();
                document.location = ResolveUrl('~/Program/Process/RBudgetRequestOutstanding/RBudgetRequestOutstandingList.aspx');
            });

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }

        $('.chkIsSelected input').live('change', function () {
            $tr = $(this).closest('tr');
            if ($(this).is(':checked')) {
                $tr.find('.txtTotalAmount').each(function () {
                    $(this).removeAttr('readonly');
                });
            }
            else {
                $tr.find('.txtTotalAmount').each(function () {
                    $(this).attr('readonly', 'readonly');
                });
            }
        });

        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected input').each(function () {
                $(this).prop('checked', isChecked);
                $(this).change();
            });
        });

        function onAfterCustomClickSuccess(type, retval) {
            var param = retval.split('|');
            var messageText = '';
            if (param[1] != '')
                messageText += 'Realisasi Budget Berhasil Dibuat Dengan No Transaksi <b>' + param[1] + '</b>';
            
            showToast('Save Success', messageText, function () {
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                $('#<%=hdnLstSaveValue.ClientID %>').val('');
                if (param[0] == '0')
                    $('#<%=btnOrderListBack.ClientID %>').click();
                cbpView.PerformCallback('refresh');
            });
        }

        function getCheckedMember() {
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
            var lstSaveValue = $('#<%=hdnLstSaveValue.ClientID %>').val().split('|');

            var result = '';
            $('.grdBudgetRequest .chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    $tr = $(this).closest('tr');
                    var key = $tr.find('.keyField').html();
                    var saveValue = '';
                    $tr.find('.txtTotalAmount').each(function () {
                        if (saveValue != '')
                            saveValue += '%';
                        saveValue += $(this).attr('GCProjectFundType') + ';' + $(this).attr('hiddenVal');
                    });
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx < 0) {
                        lstSelectedMember.push(key);
                        lstSaveValue.push(saveValue);
                    }
                    else {
                        lstSaveValue[idx] = saveValue;
                    }
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').html();
                    var idx = lstSelectedMember.indexOf(key);
                    if (idx > -1) {
                        lstSelectedMember.splice(idx, 1);
                        lstSaveValue.splice(idx, 1);
                    }
                }
            });
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            $('#<%=hdnLstSaveValue.ClientID %>').val(lstSaveValue.join('|'));
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        $('.lblAvailableStock.lblLink').live("click", function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            var itemID = $tr.find('.hdnItemID').val();
            var stock = $tr.find('.hdnQuantityEND').val();
            var itemUnit = $tr.find('.hdnItemUnit').val();
            var orderID = $('#<%=hdnRequestID.ClientID %>').val();
            var param = orderID + '|' + itemID + '|' + stock + '|' + itemUnit;
            var url = ResolveUrl("~/Program/Warehouse/ItemRequest/Outstanding/ItemRequestProcessedDtCtl.ascx");
            openUserControlPopup(url, param, 'Detail Penggunaan Item', 800, 500);
        });
    </script>
    <input type="hidden" value="" id="hdnRequestID" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnLstSaveValue" runat="server" value="" />
    <div style="overflow-y: auto; overflow-x: hidden;">
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
                            <td class="tdLabel"><label class="lblNormal" id="lblOrderNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtBudgetRequestNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Project")%></label></td>
                            <td><asp:TextBox ID="txtProjectName" Width="200px" ReadOnly="true" runat="server"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelompok Tugas")%></label></td>
                            <td><asp:TextBox ID="txtProjectTaskGroup" Width="200px" ReadOnly="true" runat="server"/></td>
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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtRequestDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td><asp:TextBox ID="txtRequestTime" Width="100px" CssClass="time" runat="server" Style="text-align: center" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Max Tgl Dibutuhkan") %></label></td>
                            <td><asp:TextBox ID="txtDueDate" Width="120px" ReadOnly="true" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top: 5px"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="2" /></td>
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
                                    <table id="tblView" class="grdBudgetRequest grdSelected grdBorder" cellspacing="0" rules="all">
                                        <tr>
                                            <th class="keyField" rowspan="2">
                                            </th>
                                            <th rowspan="2" style="width: 20px; text-align: center">
                                                <input id="chkSelectAll" type="checkbox" />
                                            </th>
                                            <th rowspan="2"><%=GetLabel("NAMA ANGGARAN")%></th>
                                            <th class="thCenter" id="thNotProcessedHeader" runat="server"><%=GetLabel("BELUM DIPROSES")%></th>
                                            <th class="thCenter" id="thProcessedHeader" runat="server"><%=GetLabel("JUMLAH PROSES")%></th>
                                        </tr>
                                        <tr>
                                            <asp:Repeater runat="server" ID="rptViewHeader">
                                                <ItemTemplate>
                                                    <th style="width:100px;" class="thRight"><%#:Eval("StandardCodeName") %></th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater runat="server" ID="rptViewHeader2">
                                                <ItemTemplate>
                                                    <th style="width:100px;" class="thRight"><%#:Eval("StandardCodeName") %></th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <LayoutTemplate>
                                            <tr runat="server" id="itemPlaceholder">
                                            </tr>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%#:Eval("BudgetRequestDtID")%></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                </td>
                                                <td><%#:Eval("BudgetRequestDtName")%></td>
                                                <asp:Repeater runat="server" ID="rptViewItem" OnItemDataBound="rptViewItem_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right" id="tdTotalAmount" class="tdTotalAmount" runat="server"></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="rptViewItem2" OnItemDataBound="rptViewItem2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right" runat="server">
                                                            <asp:TextBox ID="txtTotalAmount" CssClass="txtTotalAmount txtCurrency" ReadOnly="true" runat="server" Width="100%" />                                                        
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    </table>
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
