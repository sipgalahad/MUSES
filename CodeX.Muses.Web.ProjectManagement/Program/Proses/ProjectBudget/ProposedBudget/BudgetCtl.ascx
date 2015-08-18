<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BudgetCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.BudgetCtl" %>

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
    function onLoadBudget() {
        $('#divTransactionAdd').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtProposedBudgetCode.ClientID %>').val('');
            $('#<%=txtProposedBudgetName.ClientID %>').val('');
            $('#<%=txtRealizationDate.ClientID %>').val('');
            $('#<%=txtEntryRemarks.ClientID %>').val('');
            $('#<%=txtProposedBudgetCode.ClientID %>').attr('readonly', false);

            $('.txtBudgetFund').each(function () {
                $(this).val(0).trigger('changeValue');
            });
            $('#<%=txtTotalLineAmount.ClientID %>').val(0).trigger('changeValue');

            $('#containerEntryBudget').show();
        });

        $('#btnBudgetSave').click(function (evt) {
            if (IsValid(evt, 'fsTrx', 'mpTrxPopup')) {
                var lst = '';
                $('.txtBudgetFund').each(function () {
                    var value = $(this).attr('hiddenVal');
                    if (lst != "")
                        lst += '|';
                    lst += value;
                });
                $('#<%=hdnLstFundItem.ClientID %>').val(lst);
                cbpBudgetView.PerformCallback('save');
            }
        });

        $('#btnBudgetCancel').click(function () {
            $('#containerEntryBudget').hide();
        });

        var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
        var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
        var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
        setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
        setPaging($("#paging"), pageCount, function (page) {
            cbpBudgetView.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
        });
    }

    //#region Paging
    function onCbpBudgetViewEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                var OrderID = s.cpOrderID;
                $('#divTransactionAdd').click();
                cbpBudgetView.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpBudgetView.PerformCallback('refresh');
        }
        else if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);

            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpBudgetView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion

    $('.txtBudgetFund').die('change');
    $('.txtBudgetFund').live('change', function () {
        $(this).trigger('changeValue');
        calculateTotalBudgetAmount();
    });

    $('.divDetailEdit.divBudgetEdit').die('click');
    $('.divDetailEdit.divBudgetEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnID.ClientID %>').val(entity.ProposedBudgetID);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
        $('#<%=txtProposedBudgetCode.ClientID %>').val(entity.ProposedBudgetCode);
        $('#<%=txtProposedBudgetCode.ClientID %>').attr('readonly', true);

        $('#<%=txtProposedBudgetName.ClientID %>').val(entity.ProposedBudgetName);
        $('#<%=txtRealizationDate.ClientID %>').val(entity.RealizationDateInDatePicker);

        var listFund = entity.ListFund;
        var data = listFund.split('|');
        var count = 0;
        $('.txtBudgetFund').each(function () {
            $(this).val(data[count]).trigger('changeValue');
            count++;
        });

        calculateTotalBudgetAmount();
        $('#<%=txtEntryRemarks.ClientID %>').val(entity.Remarks);
        $('#containerEntryBudget').show();
    });

    $('.divDetailDelete.divBudgetDelete').die('click');
    $('.divDetailDelete.divBudgetDelete').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
        cbpBudgetView.PerformCallback('delete');
    });

    function calculateTotalBudgetAmount() {
        var total = 0;
        $('.txtBudgetFund').each(function () {
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
<input type="hidden" value="" id="hdnPageCount" runat="server" />
<input type="hidden" value="" id="hdnRowCount" runat="server" />
<input type="hidden" value="1" id="hdnIsEditable" runat="server" />
<div class="divTransactionEntry">
    <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
    <div id="containerEntryBudget" class="entryDetailContainer" style="display: none">
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
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                    <td><asp:TextBox runat="server" ID="txtProposedBudgetName" Width="220px" /></td>
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
                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Realisasi")%></label></td>
                    <td><asp:TextBox ID="txtRealizationDate" Width="120px" runat="server" CssClass="datepicker" /></td>
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
                        <input type="button" id="btnBudgetSave" class="btnWhite" value="Commit"/>
                        <input type="button" id="btnBudgetCancel" class="btnWhite" value="Cancel"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
<dxcp:ASPxCallbackPanel ID="cbpBudgetView" runat="server" Width="100%" ClientInstanceName="cbpBudgetView"
    ShowLoadingPanel="false" OnCallback="cbpBudgetView_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpBudgetViewEndCallback(s); }" />
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
                    <asp:Repeater runat="server" ID="grdView" OnItemDataBound="grdView_ItemDataBound">
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
                                        <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete divBudgetDelete"></div>
                                        <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit divBudgetEdit"><%=GetLabel("Edit")%></div>
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
    <div class="divInformationNumEntries" id="informationNumEntries"></div>
    <div class="wrapperPaging">
        <div id="paging"></div>
    </div>
</div>