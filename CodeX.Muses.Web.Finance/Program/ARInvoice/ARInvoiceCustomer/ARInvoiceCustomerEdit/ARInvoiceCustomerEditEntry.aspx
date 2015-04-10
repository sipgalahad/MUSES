<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCustomerPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ARInvoiceCustomerEditEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.ARInvoiceCustomerEditEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }
        
        //#region AR Invoice
        function onGetARInvoiceFilterExpression() {
            var filterExpression = "<%=OnGetARInvoiceFilterExpression() %>";
            return filterExpression;
        }

        $('#lblARInvoiceNo.lblLink').live('click', function () {
            openSearchDialog('arinvoicehd', onGetARInvoiceFilterExpression(), function (value) {
                $('#<%=txtARInvoiceNo.ClientID %>').val(value);
                onTxtProcessedDateChanged(value);
            });
        });

        $('#<%=txtARInvoiceNo.ClientID %>').live('change', function () {
            onTxtProcessedDateChanged($(this).val());
        });

        function onTxtProcessedDateChanged(value) {
            onLoadObject(value);
        }
        //#endregion

        $('.divDetailDelete').live('click', function () {
            $tr = $(this).closest('tr');
            showToastConfirmation('Apakah Anda Yakin?', function (result) {
                if (result) {
                    var entity = rowToObject($tr);
                    $('#<%=hdnARInvoiceDtID.ClientID %>').val(entity.ARInvoiceDtID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('.grdARInvoiceHD .txtDiscountAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var transactionAmount = parseFloat($tr.find('.hdnTransactionAmount').val());
            var discountAmount = parseFloat($(this).attr('hiddenVal'));
            $tr.find('.txtClaimedAmount').val(transactionAmount - discountAmount).trigger('changeValue');
            $tr.find('.btnSave').removeAttr('enabled');
        });

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                $tr = $(this).closest('tr');
                var entity = rowToObject($tr);
                $('#<%=hdnARInvoiceDtID.ClientID %>').val(entity.ARInvoiceDtID);
                var discountAmount = $tr.find('.txtDiscountAmount').val();
                var param = 'save|' + discountAmount;
                $btnSave = $(this);
                cbpProcess.PerformCallback(param);
            }
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                cbpView.PerformCallback('changepage|' + page);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                $('#<%=txtTotalTransaction.ClientID %>').val(parseFloat(param[3])).trigger('changeValue');
                $('#<%=txtTotalDiscount.ClientID %>').val(parseFloat(param[4])).trigger('changeValue');
                $('#<%=txtTotalClaimed.ClientID %>').val(parseFloat(param[5])).trigger('changeValue');
                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                    cbpView.PerformCallback('changepage|' + page);
                });

                $('.grdARInvoiceHD .txtCurrency').each(function () {
                    $(this).trigger('changeValue');
                });
            }
        }
        //#endregion

        function onCbpProcessEndCallback(s) {
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $tr = $btnSave.closest('tr');
                    $btnSave.attr('enabled', 'false');

                    $('#<%=txtTotalTransaction.ClientID %>').val(parseFloat(param[2])).trigger('changeValue');
                    $('#<%=txtTotalDiscount.ClientID %>').val(parseFloat(param[3])).trigger('changeValue');
                    $('#<%=txtTotalClaimed.ClientID %>').val(parseFloat(param[4])).trigger('changeValue');
                }

            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else {
                    cbpView.PerformCallback('refresh');
                }
            }
            hideLoadingPanel();
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var arInvoiceID = $('#<%=hdnARInvoiceID.ClientID %>').val();
            if (arInvoiceID == '' || arInvoiceID == '0') {
                errMessage.text = 'Please Save Transaction First!';
                return false;
            }
            else {
                filterExpression.text = "ARInvoiceID = " + arInvoiceID;
                return true;
            }
        }
    </script>
    <div>
        <input type="hidden" id="hdnARInvoiceDtID" runat="server" value="" />
        <input type="hidden" id="hdnPaymentID" runat="server" value="" />
        <input type="hidden" id="hdnARInvoiceID" value="" runat="server" />
        <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
        <input type="hidden" id="hdnPageCount" value="" runat="server" />
        <table class="tblContentArea" width="100%">
            <tr>
                <td valign="top">
                    <table>
                        <colgroup>
                            <col style="width: 200px" />
                            <col style="width: 250px" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal lblLink" id="lblARInvoiceNo"><%=GetLabel("Nomor Invoice") %></label></td>
                            <td><asp:TextBox ID="txtARInvoiceNo" Width="150px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Invoice") %></label></td>
                            <td><asp:TextBox runat="server" Width="150px" ID="txtInvoiceDate" CssClass="datepicker" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Jatuh Tempo") %></label></td>
                            <td><asp:TextBox runat="server" Width="150px" ID="txtDueDate" CssClass="datepicker" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top:5px;"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtRemarks" ReadOnly="true" Width="400px" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="right">
                    <table>
                        <colgroup>
                            <col style="width: 200px" />
                            <col style="width: 250px" />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Total Transaksi") %></label></td>
                            <td><asp:TextBox ID="txtTotalTransaction" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Total Diskon") %></label></td>
                            <td><asp:TextBox ID="txtTotalDiscount" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Total") %></label></td>
                            <td><asp:TextBox ID="txtTotalClaimed" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" ForeColor="Blue" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="position:relative;" id="divView">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                                        <table class="grdARInvoiceHD tblTransactionEntryResult" cellspacing="0" width="100%" rules="all">
                                            <colgroup>
                                                <col />
                                                <col />
                                                <col style="width:140px" />
                                                <col style="width:100px" />
                                                <col style="width:100px" />
                                                <col style="width:100px" />
                                                <col style="width:80px" />
                                                <col style="width:40px" />
                                            </colgroup>
                                            <tr>
                                                <th class="keyField"></th>
                                                <th align="left"><%=GetLabel("Siswa") %></th>
                                                <th align="left"><%=GetLabel("Keterangan") %></th>
                                                <th align="left"><%=GetLabel("No Referensi") %></th>
                                                <th class="thRight"><%=GetLabel("Transaksi") %></th>
                                                <th class="thRight"><%=GetLabel("Diskon") %></th>
                                                <th class="thRight"><%=GetLabel("Total") %></th>
                                                <th class="thCenter"></th>
                                                <th></th>
                                            </tr>
                                            <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <tr class="trEmpty">
                                                        <td colspan="15"><%=GetLabel("Data Tidak Tersedia") %></td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="keyField">
                                                            <input type="hidden" bindingfield="ARInvoiceDtID" value='<%#: Eval("ARInvoiceDtID")%>' />
                                                            <input type="hidden" class="hdnTransactionAmount" bindingfield="TransactionAmount" value='<%#: Eval("TransactionAmount")%>' />
                                                            <input type="hidden" bindingfield="VarianceAmount" value='<%#: Eval("VarianceAmount")%>' />
                                                            <input type="hidden" bindingfield="ClaimedAmount" value='<%#: Eval("ClaimedAmount")%>' />
                                                        </td>
                                                        <td><%#:Eval("PayedStudentName") %></td>
                                                        <td><%#:Eval("cfStudentFeeCompTypeName") %></td>
                                                        <td align="left"><%#:Eval("ReferenceNo") %></td>
                                                        <td align="right"><%#:Eval("TransactionAmount","{0:N}") %></td>
                                                        <td align="center"><asp:TextBox ID="txtDiscountAmount" runat="server" Width="95%" CssClass="txtCurrency txtDiscountAmount" /></td>
                                                        <td align="center"><asp:TextBox ID="txtClaimedAmount" runat="server" Width="95%" ReadOnly="true" CssClass="txtCurrency txtClaimedAmount" /></td>
                                                        <td align="center">
                                                            <input type="button" <%# IsEditable() == "0" ? "style='display:none'" : ""%> id="btnSave" class="btnSave" enabled="false" value="Simpan" />
                                                        </td>
                                                        <td align="center">
                                                            <div style='float:right;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                        </td>
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
                                <div id="paging"></div>
                            </div>
                        </div> 
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <dxcp:ASPxCallbackPanel runat="server" ID="cbpProcess" ClientInstanceName="cbpProcess"
        OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>