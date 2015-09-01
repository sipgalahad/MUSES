<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ARInvoiceStudentProcessEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.ARInvoiceStudentProcessEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1')
                $('#divTransactionAdd').show();
            else
                $('#divTransactionAdd').hide();

            setDatePicker('<%=txtInvoiceDate.ClientID %>');
            setDatePicker('<%=txtDueDate.ClientID %>');

            //#region Add
            $('#divTransactionAdd').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    cboStudentFeeCompType.SetValue('');
                    cboYear.SetValue('');
                    cboMonth.SetValue('');
                    $('#<%=txtTransactionAmount.ClientID %>').val('0').trigger('changeValue');

                    $('#entryDetailContainer').show();
                }
            });
            //#endregion

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    cbpProcess.PerformCallback('save');
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
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

        function onAfterSaveAddRecordEntryPopup(param) {
            if ($('#<%=hdnARInvoiceID.ClientID %>').val() == '0') {
                $('#<%=hdnARInvoiceID.ClientID %>').val(param);
                var filterExpression = 'ARInvoiceID = ' + param;
                Methods.getObject('GetARInvoiceHdList', filterExpression, function (result) {
                    $('#<%=txtARInvoiceNo.ClientID %>').val(result.ARInvoiceNo);
                    onLoadObject(result.ARInvoiceNo);
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterCustomClickSuccess(type, retval) {
            showToast('Process Success', 'Proses Pembuatan Tagihan Piutang Pasien Instansi Berhasil Dibuat Dengan Nomor <b>' + retval + '</b>', function () {
                cbpView.PerformCallback('Refresh');
            });
        }

        //#region AR Invoice
        function onGetARInvoiceFilterExpression() {
            var filterExpression = "<%=onGetARInvoiceFilterExpression() %>";
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
        }
        //#endregion

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ARInvoiceDtID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ARInvoiceDtID);
            cboStudentFeeCompType.SetValue(entity.StudentFeeCompTypeID);
            cboYear.SetValue(entity.TransactionYear);
            cboMonth.SetValue(entity.TransactionMonth);
            $('#<%=txtTransactionAmount.ClientID %>').val(entity.TransactionAmount).trigger('changeValue');

            $('#entryDetailContainer').show();
        });
        //#endregion

        function onAfterSaveRecordDtSuccess(ARInvoiceID) {
            var ARInvoiceNo;
            if ($('#<%=hdnARInvoiceID.ClientID %>').val() == '0') {
                $('#<%=hdnARInvoiceID.ClientID %>').val(ARInvoiceID);
                var filterExpression = 'ARInvoiceID = ' + ARInvoiceID;
                Methods.getObject('GetARInvoiceHdList', filterExpression, function (result) {
                    $('#<%=txtARInvoiceNo.ClientID %>').val(result.ARInvoiceNo);
                    onLoadObject(result.ARInvoiceNo);
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var ARInvoiceID = s.cpARInvoiceID;
                    onAfterSaveRecordDtSuccess(ARInvoiceID);
                    $('#divTransactionAdd').click();
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
    <input type="hidden" id="hdnSelectedMember" runat="server" />
    <input type="hidden" id="hdnARInvoiceID" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <table class="tblContentArea" width="100%">
        <colgroup>
            <col style="width:50%" />
            <col />
        </colgroup>
        <tr>
            <td valign="top">
                <table>
                    <colgroup>
                        <col style="width:130px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal lblLink" id="lblARInvoiceNo"><%=GetLabel("Nomor Invoice") %></label></td>
                        <td><asp:TextBox ID="txtARInvoiceNo" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Invoice") %></label></td>
                        <td><asp:TextBox runat="server" Width="120px" ID="txtInvoiceDate" CssClass="datepicker" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jatuh Tempo")%></label></td>
                        <td><asp:TextBox runat="server" Width="120px" ID="txtDueDate" CssClass="datepicker" /></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <colgroup>
                        <col style="width:120px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bank") %></label></td>
                        <td><dxe:ASPxComboBox ID="cboBank" runat="server" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top:5px;"><%=GetLabel("Keterangan") %></td>
                        <td><asp:TextBox ID="txtRemarks" Width="400px" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divTransactionEntry">
                    <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                    <br />
                    <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrx" style="margin: 0">
                            <input type="hidden" value="" id="hdnEntryID" runat="server" />
                            <table style="width:100%">
                                <colgroup>
                                    <col style="width: 50%" />
                                </colgroup>
                                <tr>
                                    <td valign="top">
                                        <table style="width: 100%">
                                            <colgroup>
                                                <col style="width:150px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe") %></label></td>
                                                <td><dxe:ASPxComboBox id="cboStudentFeeCompType" ClientInstanceName="cboStudentFeeCompType" runat="server" Width="150px" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tahun") %></label></td>
                                                <td><dxe:ASPxComboBox id="cboYear" ClientInstanceName="cboYear" runat="server" Width="150px" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Bulan") %></label></td>
                                                <td><dxe:ASPxComboBox id="cboMonth" ClientInstanceName="cboMonth" runat="server" Width="150px" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah") %></label></td>
                                                <td><asp:TextBox id="txtTransactionAmount" runat="server" Width="150px" CssClass="txtCurrency" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td> 
                                        <input type="button" id="btnSave" class="btnWhite" value='<%=GetLabel("Commit") %>'/>
                                        <input type="button" id="btnCancel" class="btnWhite" value='<%=GetLabel("Cancel") %>'/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </div>
                <div style="position:relative;" id="divView">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView" 
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ARInvoiceDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="cfStudentFeeCompTypeName" HeaderText="Keterangan"  />
                                            <asp:BoundField DataField="TransactionAmount" HeaderText="Total Piutang" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-Width="200px" />
                                            <asp:TemplateField HeaderStyle-Width="80px">
                                                <ItemTemplate>
                                                    <div style='float:right;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" bindingfield="ARInvoiceDtID" value='<%# Eval("ARInvoiceDtID")%>' />
                                                    <input type="hidden" bindingfield="StudentFeeCompTypeID" value='<%# Eval("StudentFeeCompTypeID")%>' />
                                                    <input type="hidden" bindingfield="TransactionMonth" value='<%# Eval("TransactionMonth")%>' />
                                                    <input type="hidden" bindingfield="TransactionYear" value='<%# Eval("TransactionYear")%>' />
                                                    <input type="hidden" bindingfield="TransactionAmount" value='<%# Eval("TransactionAmount")%>' />                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("Data Tidak Tersedia")%>
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
                </div>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>