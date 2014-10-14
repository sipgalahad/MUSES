<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalTemplateDtEntryCtl.ascx.cs" 
    Inherits="Codex.Muses.Web.Accounting.Program.JournalTemplateDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#lblEntryPopupAddData').live('click', function () {
        $('#<%=hdnEntryID.ClientID %>').val('');
        $('#<%=hdnGLAccountID.ClientID %>').val('');
        $('#<%=txtGLAccountCode.ClientID %>').val('');
        $('#<%=txtGLAccountName.ClientID %>').val('');
        $('#<%=txtAmountPercentage.ClientID %>').val('0');
        $('#<%=hdnSubLedgerDtID.ClientID %>').val('');
        $('#<%=txtSubLedgerDtCode.ClientID %>').val('');
        $('#<%=txtSubLedgerDtName.ClientID %>').val('');
        $('#<%=txtDisplayOrder.ClientID %>').val('0');

        $('#<%=hdnSubLedgerID.ClientID %>').val('');
        $('#<%=hdnSearchDialogTypeName.ClientID %>').val('');

        onSubLedgerIDChanged();

        $('#containerPopupEntryData').show();
    });
    
    $('#btnEntryPopupCancel').live('click', function () {
        $('#containerPopupEntryData').hide();
    });

    $('#btnEntryPopupSave').click(function (evt) {
        if (IsValid(evt, 'fsEntryPopup', 'mpEntryPopup'))
            cbpEntryPopupView.PerformCallback('save');
        return false;
    });

    $('.imgDelete.imgLink').die('click');
    $('.imgDelete.imgLink').live('click', function (evt) {
        $row = $(this).closest('tr').parent().closest('tr');
        if (confirm("Are You Sure Want To Delete This Data?")) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                cbpEntryPopupView.PerformCallback('delete');
            }
        }
    });

    $('.imgEdit.imgLink').die('click');
    $('.imgEdit.imgLink').live('click', function () {
        $row = $(this).closest('tr').parent().closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
        $('#<%=hdnGLAccountID.ClientID %>').val(entity.GLAccountID);
        $('#<%=txtGLAccountCode.ClientID %>').val(entity.GLAccountNo);
        $('#<%=txtGLAccountName.ClientID %>').val(entity.GLAccountName);
        $('#<%=hdnSubLedgerDtID.ClientID %>').val(entity.SubLedgerDtID);
        $('#<%=txtSubLedgerDtCode.ClientID %>').val(entity.SubLedgerDtCode);
        $('#<%=txtSubLedgerDtName.ClientID %>').val(entity.SubLedgerDtName);
        $('#<%=txtAmountPercentage.ClientID %>').val(entity.AmountPercentage);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $("#<%=rblPosition.ClientID %> input[value=" + entity.Position + "]").prop('checked', true);

        $('#<%=hdnSubLedgerID.ClientID %>').val(entity.SubLedgerID);
        $('#<%=hdnSearchDialogTypeName.ClientID %>').val(entity.SearchDialogTypeName);
        $('#<%=hdnFilterExpression.ClientID %>').val(entity.FilterExpression);
        $('#<%=hdnIDFieldName.ClientID %>').val(entity.IDFieldName);
        $('#<%=hdnCodeFieldName.ClientID %>').val(entity.CodeFieldName);
        $('#<%=hdnDisplayFieldName.ClientID %>').val(entity.DisplayFieldName);
        $('#<%=hdnMethodName.ClientID %>').val(entity.MethodName);

        onSubLedgerIDChanged();

        $('#containerPopupEntryData').show();
    });

    function onCbpEntryPopupViewEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else 
                $('#containerPopupEntryData').hide();
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
        }
        hideLoadingPanel();
    }

    //#region GL Account
    function onGetGLAccountFilterExpression() {
        var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
        return filterExpression;
    }

    $('#lblGLAccount.lblLink').click(function () {
        openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
            $('#<%=txtGLAccountCode.ClientID %>').val(value);
            onTxtGLAccountNoChanged(value);
        });
    });

    $('#<%=txtGLAccountCode.ClientID %>').change(function () {
        onTxtGLAccountNoChanged($(this).val());
    });

    function onTxtGLAccountNoChanged(value) {
        var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
        Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=hdnGLAccountID.ClientID %>').val(result.GLAccountID);
                $('#<%=txtGLAccountName.ClientID %>').val(result.GLAccountName);

                $('#<%=hdnSubLedgerID.ClientID %>').val(result.SubLedgerID);
                $('#<%=hdnSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                $('#<%=hdnFilterExpression.ClientID %>').val(result.FilterExpression);
                $('#<%=hdnIDFieldName.ClientID %>').val(result.IDFieldName);
                $('#<%=hdnCodeFieldName.ClientID %>').val(result.CodeFieldName);
                $('#<%=hdnDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                $('#<%=hdnMethodName.ClientID %>').val(result.MethodName);
                $("#<%=rblPosition.ClientID %> input[value=" + result.Position + "]").prop('checked', true);
                onSubLedgerIDChanged();
            }
            else {
                $('#<%=hdnGLAccountID.ClientID %>').val('');
                $('#<%=txtGLAccountCode.ClientID %>').val('');
                $('#<%=txtGLAccountName.ClientID %>').val('');

                $('#<%=hdnSubLedgerID.ClientID %>').val('');
                $('#<%=hdnSearchDialogTypeName.ClientID %>').val('');
                $('#<%=hdnFilterExpression.ClientID %>').val('');
                $('#<%=hdnIDFieldName.ClientID %>').val('');
                $('#<%=hdnCodeFieldName.ClientID %>').val('');
                $('#<%=hdnDisplayFieldName.ClientID %>').val('');
                $('#<%=hdnMethodName.ClientID %>').val('');
            }

            $('#<%=hdnSubLedgerDtID.ClientID %>').val('');
            $('#<%=txtSubLedgerDtCode.ClientID %>').val('');
            $('#<%=txtSubLedgerDtName.ClientID %>').val('');
        });
    }

    function onSubLedgerIDChanged() {
        if ($('#<%=hdnSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnSubLedgerID.ClientID %>').val() == '') {
            $('#lblSubLedger').attr('class', 'lblDisabled');
            $('#<%=txtSubLedgerDtCode.ClientID %>').attr('readonly', 'readonly');
        }
        else {
            $('#lblSubLedger').attr('class', 'lblMandatory lblLink');
            $('#<%=txtSubLedgerDtCode.ClientID %>').removeAttr('readonly');
        }
    }
    //#endregion

    //#region Sub Ledger
    function onGetSubLedgerFilterExpression() {
        var filterExpression = $('#<%=hdnFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnSubLedgerID.ClientID %>').val());
        return filterExpression;
    }

    $('#lblSubLedger.lblLink').die('click');
    $('#lblSubLedger.lblLink').live('click', function () {
        if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
            openSearchDialog($('#<%=hdnSearchDialogTypeName.ClientID %>').val(), onGetSubLedgerFilterExpression(), function (value) {
                $('#<%=txtSubLedgerDtCode.ClientID %>').val(value);
                onTxtSubLedgerDtCodeChanged(value);
            });
        }
    });

    $('#<%=txtSubLedgerDtCode.ClientID %>').change(function () {
        onTxtSubLedgerDtCodeChanged($(this).val());
    });

    function onTxtSubLedgerDtCodeChanged(value) {
        if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
            var filterExpression = onGetSubLedgerFilterExpression() + " AND " + $('#<%=hdnCodeFieldName.ClientID %>').val() + " = '" + value + "'";
            Methods.getObject($('#<%=hdnMethodName.ClientID %>').val(), filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnSubLedgerDtID.ClientID %>').val(result[$('#<%=hdnIDFieldName.ClientID %>').val()]);
                    $('#<%=txtSubLedgerDtName.ClientID %>').val(result[$('#<%=hdnDisplayFieldName.ClientID %>').val()]);
                }
                else {
                    $('#<%=hdnSubLedgerDtID.ClientID %>').val('');
                    $('#<%=txtSubLedgerDtCode.ClientID %>').val('');
                    $('#<%=txtSubLedgerDtName.ClientID %>').val('');
                }
            });
        }
    }
    //#endregion
</script>

<div style="height:440px; overflow-y:auto;overflow-x: hidden">
    <input type="hidden" id="hdnTemplateID" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Template")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtTemplateCode" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Template")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtTemplateName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                </table>

                <div id="containerPopupEntryData" style="margin-top:10px;display:none;">
                    <div class="pageTitle"><%=GetLabel("Entry")%></div>
                    <fieldset id="fsEntryPopup" style="margin:0"> 
                        <input type="hidden" runat="server" id="hdnEntryID" />
                        <table class="tblEntryDetail" style="width:100%">
                            <colgroup>
                                <col style="width:150px"/>
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory lblLink" id="lblGLAccount"><%=GetLabel("Perkiraan")%></label></td>
                                <td>
                                    <input type="hidden" id="hdnGLAccountID" runat="server" />
                                    <input type="hidden" id="hdnSubLedgerID" runat="server" />
                                    <input type="hidden" id="hdnSearchDialogTypeName" runat="server" />
                                    <input type="hidden" id="hdnIDFieldName" runat="server" />
                                    <input type="hidden" id="hdnCodeFieldName" runat="server" />
                                    <input type="hidden" id="hdnDisplayFieldName" runat="server" />
                                    <input type="hidden" id="hdnMethodName" runat="server" />
                                    <input type="hidden" id="hdnFilterExpression" runat="server" />
                                    <table style="width:100%" cellpadding="0" cellspacing="0">
                                        <colgroup>
                                            <col style="width:30%"/>
                                            <col style="width:3px"/>
                                            <col/>
                                        </colgroup>
                                        <tr>
                                            <td><asp:TextBox ID="txtGLAccountCode" CssClass="required" Width="100%" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox ID="txtGLAccountName" ReadOnly="true" Width="100%" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory lblLink" id="lblSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                <td>
                                    <input type="hidden" id="hdnSubLedgerDtID" runat="server" />
                                    <table style="width:100%" cellpadding="0" cellspacing="0">
                                        <colgroup>
                                            <col style="width:30%"/>
                                            <col style="width:3px"/>
                                            <col/>
                                        </colgroup>
                                        <tr>
                                            <td><asp:TextBox runat="server" CssClass="required" ID="txtSubLedgerDtCode" Width="100%" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox runat="server" ID="txtSubLedgerDtName" ReadOnly="true" Width="100%" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                                <td><asp:TextBox ID="txtAmountPercentage" CssClass="number required" runat="server" Width="80px" /> %</td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Posisi")%></label></td>
                                <td><asp:RadioButtonList ID="rblPosition" runat="server" RepeatDirection="Horizontal" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan Cetak")%></label></td>
                                <td><asp:TextBox ID="txtDisplayOrder" CssClass="number required" runat="server" Width="80px" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="button" id="btnEntryPopupSave" value='<%= GetLabel("Save")%>' />
                                            </td>
                                            <td>
                                                <input type="button" id="btnEntryPopupCancel" value='<%= GetLabel("Cancel")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlEntryPopupGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width: 50%" valign="top">
                                            <h4 style="text-align: center"><%=GetLabel("DEBIT") %></h4>
                                            <asp:GridView ID="grdViewD" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                           <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><img class="imgEdit imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt="" style="float:left; margin-left:7px" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><img class="imgDelete imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" /></td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                                            <input type="hidden" value="<%#Eval("GLAccountID") %>" bindingfield="GLAccountID" />
                                                            <input type="hidden" value="<%#Eval("GLAccountNo") %>" bindingfield="GLAccountNo" />
                                                            <input type="hidden" value="<%#Eval("GLAccountName") %>" bindingfield="GLAccountName" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerID") %>" bindingfield="SubLedgerID" />
                                                            <input type="hidden" value="<%#Eval("SearchDialogTypeName") %>" bindingfield="SearchDialogTypeName" />
                                                            <input type="hidden" value="<%#Eval("IDFieldName") %>" bindingfield="IDFieldName" />
                                                            <input type="hidden" value="<%#Eval("CodeFieldName") %>" bindingfield="CodeFieldName" />
                                                            <input type="hidden" value="<%#Eval("DisplayFieldName") %>" bindingfield="DisplayFieldName" />
                                                            <input type="hidden" value="<%#Eval("MethodName") %>" bindingfield="MethodName" />
                                                            <input type="hidden" value="<%#Eval("FilterExpression") %>" bindingfield="FilterExpression" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtID") %>" bindingfield="SubLedgerDtID" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtCode") %>" bindingfield="SubLedgerDtCode" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtName") %>" bindingfield="SubLedgerDtName" />
                                                            <input type="hidden" value="<%#Eval("AmountPercentage") %>" bindingfield="AmountPercentage" />
                                                            <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                                            <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Perkiraan">
                                                        <ItemTemplate>
                                                            <div style="font-size: 14px;"><%#Eval("GLAccountName") %></div>
                                                            <div style="font-size: 12px;"><%#Eval("SubLedgerDtName") %></div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField DataField="AmountPercentage" HeaderText="Bagian (%)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign=Right />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <%=GetLabel("No Data To Display")%>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                        <td style="width: 50%" valign="top">
                                            <h4 style="text-align: center"><%=GetLabel("KREDIT") %></h4>
                                            <asp:GridView ID="grdViewK" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                           <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td><img class="imgEdit imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt="" style="float:left; margin-left:7px" /></td>
                                                                    <td style="width:3px">&nbsp;</td>
                                                                    <td><img class="imgDelete imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" /></td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                                            <input type="hidden" value="<%#Eval("GLAccountID") %>" bindingfield="GLAccountID" />
                                                            <input type="hidden" value="<%#Eval("GLAccountNo") %>" bindingfield="GLAccountNo" />
                                                            <input type="hidden" value="<%#Eval("GLAccountName") %>" bindingfield="GLAccountName" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerID") %>" bindingfield="SubLedgerID" />
                                                            <input type="hidden" value="<%#Eval("SearchDialogTypeName") %>" bindingfield="SearchDialogTypeName" />
                                                            <input type="hidden" value="<%#Eval("IDFieldName") %>" bindingfield="IDFieldName" />
                                                            <input type="hidden" value="<%#Eval("CodeFieldName") %>" bindingfield="CodeFieldName" />
                                                            <input type="hidden" value="<%#Eval("DisplayFieldName") %>" bindingfield="DisplayFieldName" />
                                                            <input type="hidden" value="<%#Eval("MethodName") %>" bindingfield="MethodName" />
                                                            <input type="hidden" value="<%#Eval("FilterExpression") %>" bindingfield="FilterExpression" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtID") %>" bindingfield="SubLedgerDtID" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtCode") %>" bindingfield="SubLedgerDtCode" />
                                                            <input type="hidden" value="<%#Eval("SubLedgerDtName") %>" bindingfield="SubLedgerDtName" />
                                                            <input type="hidden" value="<%#Eval("AmountPercentage") %>" bindingfield="AmountPercentage" />
                                                            <input type="hidden" value="<%#Eval("Position") %>" bindingfield="Position" />
                                                            <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Perkiraan">
                                                        <ItemTemplate>
                                                            <div style="font-size: 14px;"><%#Eval("GLAccountName") %></div>
                                                            <div style="font-size: 12px;"><%#Eval("SubLedgerDtName") %></div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                    <asp:BoundField DataField="AmountPercentage" HeaderText="Bagian (%)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign=Right />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <%=GetLabel("No Data To Display")%>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                    <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("Add Data")%></span>
                </div>
            </td>
        </tr>
    </table>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Close")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

