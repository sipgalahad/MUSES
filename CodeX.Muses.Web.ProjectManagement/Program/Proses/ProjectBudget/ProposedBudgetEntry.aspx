<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProposedBudgetEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProposedBudgetEntry" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
        
        .grdFund th     { background-color: #EEE; color: Black; border:1px solid #D5D5D5; font-weight: bolder; padding: 5px;}
        
    </style>
        
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtRealizationDate.ClientID %>');
            setDatePicker('<%=txtProposedBudgetDate.ClientID %>');
            calculateTotalProjectBudget();
            $('#trBudgetItem').hide();
            $('#trItemQuantity').hide();
        });
        
        $('#divTransactionAdd').die('click');
        $('#divTransactionAdd').live('click',function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtProposedBudgetCode.ClientID %>').val('');
            $('#<%=txtProposedBudgetName.ClientID %>').val('');
            $('#<%=txtRealizationDate.ClientID %>').val('');
            $('#<%=txtEntryRemarks.ClientID %>').val('');

            $('.txtFund').each(function () {
                $(this).val(0).trigger('changeValue');
            });
            $('#<%=txtTotalLineAmount.ClientID %>').val(0).trigger('changeValue');

            $('.entryDetailContainer').show();
        });

        $('#btnCancel').die('click');
        $('#btnCancel').live('click',function () {
            $('.entryDetailContainer').hide();
        });

        $('#btnSave').die('click');
        $('#btnSave').live('click',function () {
            var lst = '';
            $('.txtFund').each(function () {
                var value = $(this).attr('hiddenVal');
                if (lst != "")
                    lst += '|';
                lst += value;
            });
            $('#<%=hdnLstFundItem.ClientID %>').val(lst);
            cbpProcess.PerformCallback('save');
        });

        //#region ProposedBudgetHd
        function onGetProposedBudgetHdFilterExpression() {
            var filterExpression = "<%=OnGetProposedBudgetHdFilterExpression() %>";
            return filterExpression;
        }

        $('#lblProposedBudgetNo.lblLink').die('click');
        $('#lblProposedBudgetNo.lblLink').live('click',function () {
            openSearchDialog('proposedbudgethd', onGetProposedBudgetHdFilterExpression(), function (value) {
                $('#<%=txtProposedBudgetNo.ClientID %>').val(value);
                ontxtProposedBudgetNoChanged(value);
            });
        });

        $('#<%=txtProposedBudgetNo.ClientID %>').die('change');
        $('#<%=txtProposedBudgetNo.ClientID %>').live('change',function () {
            ontxtProposedBudgetNoChanged($(this).val());
        });

        function ontxtProposedBudgetNoChanged(value) {
            onLoadObject(value);
        }
        //#endregion

        $('.txtFund').die('change');
        $('.txtFund').live('change', function () {
            $(this).trigger('changeValue');
            calculateTotalAmount();
        });

        $('.divDetailEdit').die('click');
        $('.divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnID.ClientID %>').val(entity.ProposedBudgetID);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
            $('#<%=txtProposedBudgetCode.ClientID %>').val(entity.ProposedBudgetCode);
            $('#<%=txtProposedBudgetName.ClientID %>').val(entity.ProposedBudgetName);
            $('#<%=txtRealizationDate.ClientID %>').val(entity.RealizationDateInDatePicker);
            var listFund = entity.ListFund;
            var data = listFund.split('|');
            var count = 0;
            $('.txtFund').each(function () {
                $(this).val(data[count]).trigger('changeValue');
                count++;
            });
            calculateTotalAmount();
            $('#<%=txtEntryRemarks.ClientID %>').val(entity.Remarks);
            $('.entryDetailContainer').show();
        });

        $('.divDetailDelete').die('click');
        $('.divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProposedBudgetDtID);
            cbpProcess.PerformCallback('delete');
        });

        function calculateTotalAmount() {
            var total = 0;
            $('.txtFund').each(function () {
                var value = parseFloat($(this).attr('hiddenVal'));
                total += value;
            });
            $('#<%=txtTotalLineAmount.ClientID %>').val(total).trigger('changeValue');
        }

        function calculateTotalProjectBudget() {
            var total = 0;
            var $table = $('#<%=grdView.ClientID %>');
            $('.trData').each(function () {
                var entity = rowToObject($(this));
                total += parseFloat(entity.TotalAmount);
            });
            $('#<%=txtTotalProjectBudget.ClientID %>').val(total).trigger('changeValue');
        }

        function onCboBudgetTypeChanged() {
            if (cboBudgetType.GetValue() == 'DT007^002') {
                $('#trBudgetName').hide();
                $('#trBudgetItem').show();
                $('#trItemQuantity').show();
            } else {
                $('#trBudgetName').show();
                $('#trBudgetItem').hide();
                $('#trItemQuantity').hide();
                
            }
        }

        //#region TeamDt
        function onGetTeamDtFilterExpression() {
            var filterExpression = "<%=OnGetTeamDtFilterExpression()%>";
            if ($('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() != "0")
                filterExpression += " AND (EmployeeCoordinatorID = " + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + " OR ListEmployeeID1 LIKE '%;" + $('#<%=hdnEmployeeCoordinatorID.ClientID %>').val() + ";%')";
            return filterExpression;
        }

        function onTacTeamDtButtonSearchClick() {
            openSearchDialog('teamdt', onGetTeamDtFilterExpression(), function (value) {
                var filterExpression = onGetTeamDtFilterExpression() + " AND TeamDtID = " + value;
                Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeamDt.setValue(result.TeamDtID);
                        tacTeamDt.setText(result.Position);
                        entityToControlTeamDt(result);
                    }
                    else {
                        tacTeamDt.setValue('');
                        tacTeamDt.setText('');
                        entityToControlTeamDt(null);
                    }
                });
            });
        }

        function onTacTeamDtValueChanged() {
            var id = tacTeamDt.getValue();
            if (id != '') {
                var filterExpression = "TeamDtID = " + id;
                Methods.getObject('GetvTeamDtList', filterExpression, function (result) {
                    if (result != null)
                        entityToControlTeamDt(result);
                    else
                        entityToControlTeamDt(null);
                });
            } else {
                entityToControlTeamDt(null);
            }
        }

        function entityToControlTeamDt(result) {
            if (result != null)
                $('#<%=hdnTeamDtID.ClientID %>').val(result.TeamDtID);
            else
                $('#<%=hdnTeamDtID.ClientID %>').val(null);
        }
        //#endregion

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
                calculateTotalProjectBudget();
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
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
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnEntryID" runat="server" value="" />
    <input type="hidden" id="hdnLstFundItem" runat="server" value="" />
    <input type="hidden" id="hdnEmployeeCoordinatorID" runat="server" value=""/>
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td>
                <table class="tblEntryContent" style="width: 100%">
                    <colgroup>
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblProposedBudgetNo"><%=GetLabel("No. Rancangan Anggaran")%></label></td>
                        <td><asp:TextBox ID="txtProposedBudgetNo" Width="150px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label id="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                        <td><asp:TextBox ID="txtProposedBudgetDate" CssClass="datepicker" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                        <td>
                            <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeamDt" ClientInstanceName="tacTeamDt" MethodName="GetvTeamDtList" GetFilterExpressionFunction="onGetTeamDtFilterExpression"
                                SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="<b>${Position}</b>" OrderByExpression="TeamDtID">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacTeamDtButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacTeamDtValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="tblEntryContent">
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divTransactionEntry">
                    <span style="<%=IsEditable.ToString() == "False" ? "display:none" : "" %>" id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                    <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrx" style="margin: 0">
                            <table width="100%">
                                <colgroup>
                                    <col width="150px"/>
                                    <col />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Anggaran")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" ID="cboBudgetType" ClientInstanceName="cboBudgetType" Width="200px">
                                            <ClientSideEvents ValueChanged="function(s,e){onCboBudgetTypeChanged()}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                                    <td><asp:TextBox runat="server" ID="txtProposedBudgetCode" Width="120px" /></td>
                                </tr>
                                <tr id="trBudgetName">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox runat="server" ID="txtProposedBudgetName" Width="220px" /></td>
                                </tr>
                                <tr id="trBudgetItem">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item")%></label></td>
                                    <td>
                                        <input type="hidden" id="Hidden1" value="" runat="server" />
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="CodeXAutoCompleteTextBox1" ClientInstanceName="tacTeamDt" MethodName="GetvTeamDtList" GetFilterExpressionFunction="onGetTeamDtFilterExpression"
                                            SearchFields="Position" TextField="Position" ValueField="TeamDtID" SearchText="<b>${Position}</b>" OrderByExpression="TeamDtID">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacTeamDtButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacTeamDtValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>
                                    </td>
                                </tr>
                                <tr id="trItemQuantity">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jumlah")%></label></td>
                                    <td><asp:TextBox runat="server" ID="txtItemQuantity" Width="120px" CssClass="number" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Realisasi")%></label></td>
                                    <td><asp:TextBox ID="txtRealizationDate" Width="120px" runat="server" CssClass="datepicker" /></td>
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
                                        <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                        <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </div>
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
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
                                                        <div style='<%=IsEditable.ToString() == "False" ? "float:right;display:none" : "float:right;" %>' class="divDetailDelete" id="divDetailDelete"></div>
                                                        <div style='float:right;margin-right:10px;<%=IsEditable.ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit" id="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" value="<%#Eval("ProposedBudgetDtID") %>" bindingfield="ProposedBudgetDtID" />
                                                        <input type="hidden" value="<%#Eval("ProposedBudgetID") %>" bindingfield="ProposedBudgetID" />
                                                        <input type="hidden" value="<%#Eval("ProposedBudgetCode") %>" bindingfield="ProposedBudgetCode" />
                                                        <input type="hidden" value="<%#Eval("ProposedBudgetName") %>" bindingfield="ProposedBudgetName" />
                                                        <input type="hidden" value="<%#Eval("RealizationDateInDatePicker") %>" bindingfield="RealizationDateInDatePicker" />
                                                        <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
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
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="right">
                <table>
                    <tr>
                        <td class="tdLabel" colspan="2"><label class="lblNormal"><%=GetLabel("Total Diajukan")%></label></td>
                        <td></td>
                        <td><asp:TextBox ID="txtTotalProjectBudget" CssClass="txtCurrency" ReadOnly="true" Width="180px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>    
</asp:Content>