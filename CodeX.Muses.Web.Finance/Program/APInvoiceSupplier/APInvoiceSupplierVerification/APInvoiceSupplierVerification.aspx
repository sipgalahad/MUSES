<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPSupplierPageTrxVisit.master" AutoEventWireup="true"
    CodeBehind="APInvoiceSupplierVerification.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierVerification" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnProcess" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbapprove.png")%>' alt="" /><div><%=GetLabel("Verified")%></div></li>
    <li id="btnDecline" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbvoid.png")%>' alt="" /><div><%=GetLabel("Decline")%></div></li>   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="plhEntry" runat="server">   
    <script type="text/javascript">
        var total = 0;
        function onLoad() {
            setDatePicker('<%=txtDueDate.ClientID %>');

            $('#<%=txtDueDate.ClientID %>').change(function (evt) {
                onRefreshGridView();
            });

            $('#<%=btnProcess.ClientID %>').click(function () {
                getCheckedMember();
                onCustomButtonClick('process');
            });

            $('#lblRefresh').click(function () {
                onRefreshGridView();
            });

            $('#<%=btnDecline.ClientID %>').click(function () {
                getCheckedMember();
                onCustomButtonClick('decline');
            });

            $('.txtTotalToBeVerified').change(function () {
                $(this).trigger('changeValue');
            });
        }

        function getCheckedMember() {
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
            $('.lvwView .chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    var key = $(this).closest('tr').find('.keyField').val();
                    if (lstSelectedMember.indexOf(key) < 0)
                        lstSelectedMember.push(key);
                }
                else {
                    var key = $(this).closest('tr').find('.keyField').val();
                    if (lstSelectedMember.indexOf(key) > -1)
                        lstSelectedMember.splice(lstSelectedMember.indexOf(key), 1);
                }
            });
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
        }

        function onAfterCustomClickSuccess(type, retval) {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            if (type == 'process') {
                showToast('Process Success', 'Proses Verifikasi Sudah Berhasil Dilakukan');
                onRefreshGridView();
            }
            else
                onRefreshGridView();
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
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

        function onCboStatusValueChanged(evt) {
            onRefreshGridView();
        }

        function onRefreshGridView() {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            cbpView.PerformCallback('refresh');
        }

        $('.chkIsSelected input').die('change');
        $('.chkIsSelected input').live('change', function () {
            var isChecked = $(this).is(":checked");
            if (isChecked) {
                total += parseFloat($(this).closest('tr').find('.hdnTotal').val());
                $('#<%=txtTotalToBeVerified.ClientID %>').val(total).trigger('changeValue');
            }
            else {
                total -= parseFloat($(this).closest('tr').find('.hdnTotal').val());
                $('#<%=txtTotalToBeVerified.ClientID %>').val(total).trigger('changeValue');
            }
        });

        $('#chkSelectAllInvoice').die('change');
        $('#chkSelectAllInvoice').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $chk = $(this).find('input')
                $chk.prop('checked', isChecked);
                $chk.change();
            });
        });

        $('.lblPurchaseInvoiceNo').die('click');
        $('.lblPurchaseInvoiceNo').live('click', function () {
            $tr = $(this).closest('tr');
            var id = $tr.find('.keyField').val();
            var url = ResolveUrl("~/Program/APInvoiceSupplier/APInvoiceSupplierVerification/APInvoiceSupplierVerificationDtCtl.ascx");
            openUserControlPopup(url, id, 'Detail Information', 1100, 400);
        });
            
    </script> 
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnTransactionHdID" runat="server" value="" />
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" value="" id="hdnPurchaseInvoiceID" runat="server"/>
    <div style="height:435px;overflow-y:auto;overflow-x:hidden">
        <table style="width:100%">
            <colgroup>
                <col style="width:50%" />
            </colgroup>
            <tr>
                <td style="padding:5px;vertical-align:top">
                    <table class="tblEntryContent">
                        <colgroup>
                            <col style="width:200px"/>
                            <col/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Jatuh Tempo Maks.")%></label></td>
                            <td><asp:TextBox ID="txtDueDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Status")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboStatus" ClientInstanceName="cboStatus" Width="150px" runat="server">
                                    <ClientSideEvents ValueChanged="function(s,e) { onCboStatusValueChanged(e); }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding:5px;vertical-align:top">
                    <table class="tblEntryContent">
                        <colgroup>
                            <col style="width:270px"/>
                            <col/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Hutang yang Akan Diverifikasi")%></label></td>
                            <td><asp:TextBox class= "txtCurrency" ID="txtTotalToBeVerified" Width="120px" runat="server" ReadOnly="true"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span class="lblLink" id="lblRefresh">[refresh]</span>
                    <br />
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em; height: 300px; overflow-y: auto;">
                                    <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table id="tblView" runat="server" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><input id="chkSelectAllInvoice" type="checkbox" /></th>
                                                    <th style="width:40px" align="center"><%=GetLabel("Verified")%></th>
                                                    <th style="width:40px" align="center"><%=GetLabel("Bayar")%></th>
                                                    <th align="left"><%=GetLabel("No. Tukar Faktur")%></th>
                                                    <th style="width:150px" align="center"><%=GetLabel("Tgl. Jatuh Tempo")%></th>
                                                    <th style="width:100px" align="center"><%=GetLabel("Umur (hari)")%></th>
                                                    <th style="width:100px" align="center"><%=GetLabel("Pembayaran Ke-")%></th>
                                                    <th style="width:150px" align="right"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr class="trEmpty">
                                                    <td colspan="10">
                                                        <%=GetLabel("No Data To Display")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="tblView" runat="server" class="lvwView grdView notAllowSelect" cellspacing="0" rules="all" >
                                                <tr>
                                                    <th style="width:40px" class="thCenter"><input id="chkSelectAllInvoice" type="checkbox" /></th>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("Verified")%></th>
                                                    <th style="width:40px" class="thCenter"><%=GetLabel("Bayar")%></th>
                                                    <th align="left"><%=GetLabel("No. Tukar Faktur")%></th>
                                                    <th style="width:150px" class="thCenter"><%=GetLabel("Tgl. Jatuh Tempo")%></th>
                                                    <th style="width:100px" class="thCenter"><%=GetLabel("Umur (hari)")%></th>
                                                    <th style="width:100px" class="thCenter"><%=GetLabel("Pembayaran Ke-")%></th>
                                                    <th style="width:150px" class="thRight"><%=GetLabel("Jumlah")%></th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" ></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                                    <input type="hidden" class="keyField" id="keyField" runat="server" value='<%# Eval("PurchaseInvoiceID")%>' />
                                                    <input type="hidden" class="keyField" id="hdnPurchaseInvoiceNo" runat="server" value='<%# Eval("PurchaseInvoiceNo")%>' />
                                                    <input type="hidden" class="hdnTotal" id="hdnTotal" runat="server" value='<%# Eval("TotalNetTransactionAmount")%>' />
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkVerified" runat="server" CssClass="chkVerified" Enabled="false" />
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkBayar" runat="server" CssClass="chkBayar" Enabled="false" />
                                                </td>
                                                <td><label class="lblLink lblPurchaseInvoiceNo"><%# Eval("PurchaseInvoiceNo") %></label></td>
                                                <td align="center"><%# Eval("DueDateInString") %></td>
                                                <td align="center"><%# Eval("CustomUmur") %></td>
                                                <td align="center"><%# Eval("NumberOfPayment") %></td>
                                                <td align="right"><%# Eval("TotalNetTransactionAmount", "{0:N}")%></td>
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
                            <div id="paging">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

