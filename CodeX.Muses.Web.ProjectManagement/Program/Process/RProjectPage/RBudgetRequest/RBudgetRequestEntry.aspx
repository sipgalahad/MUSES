<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPRProjectPageTrx.master" AutoEventWireup="true" 
    CodeBehind="RBudgetRequestEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RBudgetRequestEntry" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
    <input type="hidden" id="hdnMyProjectOrganizationID" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
            }
            else {
                $('#divTransactionAdd').hide();
            }

            setDatePicker('<%=txtRequestDate.ClientID %>');
            setDatePicker('<%=txtDueDate.ClientID %>');

            //#region Order No
            $('#lblBudgetRequestNo.lblLink').click(function () {
                openSearchDialog('rbudgetrequesthd', "<%=GetFilterExpression() %>", function (value) {
                    $('#<%=txtBudgetRequestNo.ClientID %>').val(value);
                    onTxtBudgetRequestNoChanged(value);
                });
            });

            $('#<%=txtBudgetRequestNo.ClientID %>').change(function () {
                onTxtBudgetRequestNoChanged($(this).val());
            });

            function onTxtBudgetRequestNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=txtBudgetRequestDtName.ClientID %>').val('');
                    $('#<%=txtTotalAmount.ClientID %>').val(0).trigger('changeValue');
                    $('#<%=txtRemarks.ClientID %>').val('');
                    $('.txtFundItem').each(function () {
                        $(this).val(0).trigger('changeValue');
                    });
                    $('#entryDetailContainer').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    var lstSave = '';
                    $('.txtFundItem').each(function () {
                        var value = $(this).attr('hiddenVal');
                        var GCProjectFundType = $(this).attr('GCProjectFundType');
                        if (lstSave != "")
                            lstSave += '|';
                        lstSave += GCProjectFundType + ';' + value;
                    });
                    $('#<%=hdnLstSaveFund.ClientID %>').val(lstSave); 
                    cbpProcess.PerformCallback('save');
                }
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

        $('.txtFundItem').die('change');
        $('.txtFundItem').live('change', function () {
            $(this).blur();
            var total = 0;
            $('.txtFundItem').each(function () {
                total += parseFloat($(this).attr('hiddenVal'));
            });
            $('#<%=txtTotalAmount.ClientID %>').val(total).trigger('changeValue');
        });

        $('.divDetailEdit').die('click');
        $('.divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetRequestDtID);
            $('#<%=txtBudgetRequestDtName.ClientID %>').val(entity.BudgetRequestDtName);
            $('#<%=txtTotalAmount.ClientID %>').val(entity.TotalAmount).trigger('changeValue');
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            $('.txtFundItem').each(function () {
                var value = 0;
                var GCProjectFundType = $(this).attr('GCProjectFundType');
                $row.find('.tdTotalAmount').each(function () {
                    if ($(this).attr('GCProjectFundType') == GCProjectFundType) { 
                        value = $(this).attr('TotalAmount');
                    }
                });
                $(this).val(value).trigger('changeValue');
            });

            $('#entryDetailContainer').show();
        });

        $('.divDetailDelete').die('click');
        $('.divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.BudgetRequestDtID);
            cbpProcess.PerformCallback('delete');
        });

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var OrderID = s.cpOrderID;
                    onAfterSaveRecordDtSuccess(OrderID);
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

        function onAfterSaveRecordDtSuccess(OrderID) {
            if ($('#<%=hdnRequestID.ClientID %>').val() == '0') {
                $('#<%=hdnRequestID.ClientID %>').val(OrderID);
                var filterExpression = 'BudgetRequestID = ' + OrderID;
                Methods.getObject('GetRBudgetRequestHdList', filterExpression, function (result) {
                    $('#<%=txtBudgetRequestNo.ClientID %>').val(result.BudgetRequestNo);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }
        
        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
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
        }
        //#endregion
    </script>
    <input type="hidden" value="0" id="hdnRequestID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="1" id="hdnLstSaveFund" runat="server" />
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
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblBudgetRequestNo"><%=GetLabel("No. Permintaan")%></label></td>
                            <td><asp:TextBox ID="txtBudgetRequestNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelompok Tugas")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboProjectTaskGroup" ClientInstanceName="cboProjectTaskGroup" Width="200px" runat="server"/></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
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
                            <td><asp:TextBox ID="txtDueDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align:top; padding-top: 5px"><%=GetLabel("Keterangan") %></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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
                                                    <col style="width: 140px" />
                                                </colgroup>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Anggaran")%></label></td>
                                                    <td><asp:TextBox runat="server" ID="txtBudgetRequestDtName" Width="200px" /></td>
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
                                                                        <td><asp:TextBox ID="txtFundItem" CssClass="txtCurrency txtFundItem" runat="server" Width="120px" /></td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jmlh. Diminta")%></label></td>
                                                    <td><asp:TextBox runat="server" ReadOnly="true" ID="txtTotalAmount" Width="120px" CssClass="txtCurrency" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabel" style="padding-top:5px; vertical-align:top;"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                                    <td><asp:TextBox runat="server" ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="3" /></td>
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
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                                        <thead>
                                            <tr>
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th style="width:200px" rowspan="2"><%=GetLabel("Nama Anggaran")%></th>                              
                                                <th id="thContainerAmount" runat="server" class="thCenter"><%=GetLabel("Sumber Dana") %></th>
                                                <th style="width:100px;" class="thRight" rowspan="2"><%=GetLabel("Total")%></th>
                                                <th rowspan="2"><%=GetLabel("Catatan")%></th>
                                                <th style="width:80px;" rowspan="2"></th>
                                            </tr>
                                            <tr>
                                                <asp:Repeater runat="server" ID="rptViewHeader">
                                                    <ItemTemplate>
                                                        <th style="width:100px;" class="thRight"><%#:Eval("StandardCodeName") %></th>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </thead>
                                        <asp:Repeater runat="server" ID="rptView" OnItemDataBound="rptView_ItemDataBound">
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr class="trData">
                                                        <td class="keyField"><%#:Eval("BudgetRequestDtID")%></td>
                                                        <td><%#:Eval("BudgetRequestDtName")%></td>
                                                        <asp:Repeater runat="server" ID="rptViewItem" OnItemDataBound="rptViewItem_ItemDataBound">
                                                            <ItemTemplate>
                                                                <td align="right" id="tdTotalAmount" class="tdTotalAmount" runat="server"></td>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <td align="right"><%#:Eval("TotalAmount","{0:N}")%></td>
                                                        <td><%#:Eval("Remarks")%></td>
                                                        <td>
                                                            <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                            <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                            <input type="hidden" value="<%#Eval("BudgetRequestDtID") %>" bindingfield="BudgetRequestDtID" />
                                                            <input type="hidden" value="<%#Eval("BudgetRequestDtName") %>" bindingfield="BudgetRequestDtName" />
                                                            <input type="hidden" value="<%#Eval("TotalAmount") %>" bindingfield="TotalAmount" />
                                                            <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
