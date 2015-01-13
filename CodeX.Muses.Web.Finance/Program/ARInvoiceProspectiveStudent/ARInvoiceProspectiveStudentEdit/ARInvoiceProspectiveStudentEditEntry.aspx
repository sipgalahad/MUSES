<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProspectiveStudentPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ARInvoiceProspectiveStudentEditEntry.aspx.cs" Inherits="CodeX.Web.Finance.Program.ARInvoiceProspectiveStudentEditEntry" %>

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

        $('.imgDelete').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            showToastConfirmation('Apakah Anda Yakin?', function (result) {
                if (result) {
                    var entity = rowToObject($tr);
                    $('#<%=hdnARInvoiceDtID.ClientID %>').val(entity.ARInvoiceDtID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('.grdARInvoiceHD .txtClaimedAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var transactionAmount = parseFloat($tr.find('.hdnTransactionAmount').val());
            var claimedAmount = parseFloat($(this).attr('hiddenVal'));
            $tr.find('.txtVarianceAmount').val(claimedAmount - transactionAmount).trigger('changeValue');
            $tr.find('.btnSave').removeAttr('enabled');
        });

        $('.grdARInvoiceHD .txtVarianceAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var transactionAmount = parseFloat($tr.find('.hdnTransactionAmount').val());
            var varianceAmount = parseFloat($(this).attr('hiddenVal'));
            $tr.find('.txtClaimedAmount').val(transactionAmount + varianceAmount).trigger('changeValue');
            $tr.find('.btnSave').removeAttr('enabled');
        });

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                $tr = $(this).closest('tr');
                var entity = rowToObject($tr);
                $('#<%=hdnARInvoiceDtID.ClientID %>').val(entity.ARInvoiceDtID);
                var claimedAmount = $tr.find('.txtClaimedAmount').val();
                var param = 'save|' + claimedAmount;
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
                $('#<%=txtTotalClaimed.ClientID %>').val(parseFloat(param[4])).trigger('changeValue');
                $('#<%=txtTotalVariance.ClientID %>').val(parseFloat(param[5])).trigger('changeValue');
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
                    $('#<%=txtTotalClaimed.ClientID %>').val(parseFloat(param[3])).trigger('changeValue');
                    $('#<%=txtTotalVariance.ClientID %>').val(parseFloat(param[4])).trigger('changeValue');
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
                            <td class="tdLabel"><label><%=GetLabel("Total Klaim") %></label></td>
                            <td><asp:TextBox ID="txtTotalClaimed" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" ForeColor="Blue" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Total Penyesuaian") %></label></td>
                            <td><asp:TextBox ID="txtTotalVariance" ReadOnly="true" Width="150px" CssClass="txtCurrency" runat="server" /></td>
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
                                        <table class="grdARInvoiceHD grdSelected" cellspacing="0" width="100%" rules="all">
                                            <colgroup>
                                                <col style="width:40px" />
                                                <col style="width:140px" />
                                                <col style="width:100px" />
                                                <col style="width:100px" />
                                                <col style="width:100px" />
                                                <col style="width:80px" />
                                            </colgroup>
                                            <tr>
                                                <th class="keyField"></th>
                                                <th></th>
                                                <th align="left"><%=GetLabel("No Referensi") %></th>
                                                <th class="thRight"><%=GetLabel("Transaksi") %></th>
                                                <th class="thRight"><%=GetLabel("Klaim") %></th>
                                                <th class="thRight"><%=GetLabel("Penyesuaian") %></th>
                                                <th class="thCenter"></th>
                                            </tr>
                                            <asp:ListView runat="server" ID="lvwView" OnItemDataBound="lvwView_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <tr class="trEmpty">
                                                        <td colspan="15"><%=GetLabel("Data Tidak Tersedia") %></td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><img class="imgDelete <%# IsEditable() == "0" ? "imgDisabled" : "imgLink"%>" title='<%=GetLabel("Delete")%>' src='<%# IsEditable() == "0" ? ResolveUrl("~/Libs/Images/Button/delete_disabled.png") : ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="keyField">
                                                            <input type="hidden" bindingfield="ARInvoiceDtID" value='<%#: Eval("ARInvoiceDtID")%>' />
                                                            <input type="hidden" class="hdnTransactionAmount" bindingfield="TransactionAmount" value='<%#: Eval("TransactionAmount")%>' />
                                                            <input type="hidden" bindingfield="VarianceAmount" value='<%#: Eval("VarianceAmount")%>' />
                                                            <input type="hidden" bindingfield="ClaimedAmount" value='<%#: Eval("ClaimedAmount")%>' />
                                                        </td>
                                                        <td align="left"><%#:Eval("ReferenceNo") %></td>
                                                        <td align="right"><%#:Eval("TransactionAmount","{0:N}") %></td>
                                                        <td align="center"><asp:TextBox ID="txtClaimedAmount" runat="server" Width="95%" CssClass="txtCurrency txtClaimedAmount" /></td>
                                                        <td align="center"><asp:TextBox ID="txtVarianceAmount" runat="server" Width="95%" CssClass="txtCurrency txtVarianceAmount" /></td>
                                                        <td align="center">
                                                            <input type="button" <%# IsEditable() == "0" ? "style='display:none'" : ""%> id="btnSave" class="btnSave" enabled="false" value="Simpan" />
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