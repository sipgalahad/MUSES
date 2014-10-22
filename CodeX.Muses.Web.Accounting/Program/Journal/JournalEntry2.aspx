<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
CodeBehind="JournalEntry2.aspx.cs" Inherits="Codex.Muses.Web.Accounting.Program.JournalEntry2" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divTemplatePick').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divTemplatePick').hide();
            }

            $('#divTemplatePick').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    showLoadingPanel();
                    var url = ResolveUrl('~/Program/Journal/JournalTemplateCtl.ascx');
                    var glTransactionID = $('#<%=hdnID.ClientID %>').val();
                    var id = glTransactionID;
                    openUserControlPopup(url, id, 'Template', 600, 300);
                }
            });

            addEntityRow();
        }

        function onGetCOAFilterExpression() {
            var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
            return filterExpression;
        }

        $tacTr = null;
        //#region Signa
        $('.tacCOA .btnAutoCompleteSearchMore').die('click');
        $('.tacCOA .btnAutoCompleteSearchMore').live('click', function () {
            $tacTr = $(this).closest('tr');
            openSearchDialog('chartofaccount', onGetCOAFilterExpression(), function (value) {
                var filterExpression = onGetCOAFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    $tacCOA = $tacTr.find('.tacCOA');
                    if (result != null) {
                        $tacCOA.find('.hdnAutoCompleteValue').val(result.GLAccountID);
                        $tacCOA.find('.hdnAutoCompleteText').val(result.GLAccountName);
                        $tacCOA.find('.txtAutoComplete').val(result.GLAccountName);
                    }
                    else {
                        $tacCOA.find('.hdnAutoCompleteValue').val('');
                        $tacCOA.find('.hdnAutoCompleteText').val('');
                        $tacCOA.find('.txtAutoComplete').val('');
                    }
                });
                var trIdx = $('.trJournalEntry').index($tacTr);
                if (trIdx == $('.trJournalEntry').length - 1)
                    addEntityRow();
                $tacTr = null;
            });
        });
        //#endregion

        var idx = 1;
        function addEntityRow() {
            $newTr = $('#tmplEntity').html().replace('script1', 'script').replace('script1', 'script');
            //$newTr = $newTr.replace(/\$\{ItemName1}/g, $selectedTr.find('.tdItemName1').html());
            $newTr = $newTr.replace(/\$\{idx}/g, idx);
            $newTr = $($newTr);

            $newTr.insertBefore($('#trFooter'));

            $newTr.find('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            var tempHelper = new CodeXClientAutoCompleteHelper();
            tempHelper.init("COA" + idx, "GLAccountNo,GLAccountName", "GetChartOfAccountList", "", "onGetCOAFilterExpression", "GLAccountNo");
            tempHelper.setClientSideEvents(function ($s) {
                $tacTr = $s.closest('tr');
                var trIdx = $('.trJournalEntry').index($tacTr);
                if (trIdx == $('.trJournalEntry').length - 1)
                    addEntityRow();
                $tacTr = null;
            });
            tempHelper.initializeControl();

            idx++;
        }

        $('.txtKredit').live('focus', function () {
            var debit = parseFloat($(this).closest('tr').find('.txtDebit').attr('hiddenVal'));
            if (debit == 0) {
                var totalDebit = 0;
                $('#tblJournalEntry .txtDebit').each(function () {
                    totalDebit += parseFloat($(this).attr('hiddenVal'));
                });
                var totalKredit = 0;
                $('#tblJournalEntry .txtKredit').each(function () {
                    totalKredit += parseFloat($(this).attr('hiddenVal'));
                });
                $(this).val(totalDebit - totalKredit).trigger('changeValue');
            }
        });

        function onCboTransactionCodeValueChanged(s) {
            var value = s.GetValue();
            var filterExpression = "TransactionCode = '" + value + "'";
            Methods.getObject('GetTransactionTypeList', filterExpression, function (result) {
                if (result != null)
                    $('#<%=txtJournalPrefix.ClientID %>').val(result.TransactionInitial);
                else
                    $('#<%=txtJournalPrefix.ClientID %>').val('');
            });
        }

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var transactionID = $('#<%=hdnID.ClientID %>').val();

            if (transactionID == '' || transactionID == '0') {
                errMessage.text = 'Pilih Jurnal Terlebih Dahulu!';
                return false;
            }
            else {
                var status = $('#<%=hdnGCTransactionStatus.ClientID %>').val();
                if (status == "<%=GetGCTransactionStatusOpen() %>") {
                    errMessage.text = 'Jurnal Belum di Approve';
                    return false;
                } else {
                    filterExpression.text = 'GLTransactionID = ' + transactionID;
                    return true;
                }
            }
        }
    </script>
    <style type="text/css">
        .rblJournalGroup input[type="radio"]            { margin-left: 40px; margin-right: 1px; }
    </style>
    <script id="tmplEntity" type="text/x-jquery-tmpl">
        <tr class="trJournalEntry">
            <td>
                <div id="COA${idx}" class="tacCOA">
                    <div>
                        <div class="containerAutoComplete">
                            <input type="hidden" class="hdnAutoCompleteValue">
                            <input type="hidden" class="hdnAutoCompleteText">
                            <input type="hidden" class="hdnIsRequired" value="1">
                            <input type="hidden" class="hdnValidationGroup" value="mpTrx">
                            <input type="text" class="required txtAutoComplete" validationgroup="mpTrx" style="width:200px"/>
                            <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                            <div class="divListAutoCompleteResultBox">
                                <div class="divListAutoCompleteResult">
                                </div>
                            </div>
                        </div>
                        <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                            <div>
                                ${GLAccountName} (<b>${GLAccountNo}</b>)
                                <input type='hidden' value='${GLAccountName}' class='hdnAutoCompleteRowText'/>
                                <input type='hidden' value='${GLAccountID}' class='hdnAutoCompleteRowValue'/>
                            </div>
                        </script1>
                    </div>
                </div>
            </td>
            <td>
                <div id="divSubCOA${idx}" class="tacSubCOA">
                    <div>
                        <div class="containerAutoComplete">
                            <input type="hidden" class="hdnAutoCompleteValue">
                            <input type="hidden" class="hdnAutoCompleteText">
                            <input type="hidden" class="hdnIsRequired" value="1">
                            <input type="hidden" class="hdnValidationGroup" value="mpTrx">
                            <input type="text" readonly="readonly" class="required txtAutoComplete" validationgroup="mpTrx" style="width:200px"/>
                            <input type="button" enabled="false" class="btnAutoCompleteSearchMore btnSearch"/>
                            <div class="divListAutoCompleteResultBox">
                                <div class="divListAutoCompleteResult">
                                </div>
                            </div>
                        </div>
                        <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                            <div>
                                ${GLAccountName} (<b>${GLAccountNo}</b>)
                                <input type='hidden' value='${GLAccountName}' class='hdnAutoCompleteRowText'/>
                                <input type='hidden' value='${GLAccountID}' class='hdnAutoCompleteRowValue'/>
                            </div>
                        </script1>
                    </div>
                </div>
            </td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtRemarks" value="" style="width:99%" /></td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtCurrency txtDebit" value="0" style="width:99%" /></td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtCurrency txtKredit" value="0" style="width:99%" /></td>
            <td align="center"><input type="text" validationgroup="mpTrx" class="txtDocumentNo" value="" style="width:99%" /></td>
        </tr>
    </script>

    <input type="hidden" id="hdnGCTransactionStatus" runat="server" value="" />
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnLastPostingDate" runat="server" value="" />
    <input type="hidden" id="hdnIsEditable" runat="server" value="" />
    <input type="hidden" value="" id="hdnRecordFilterExpression" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber Data") %></label></td>
                        <td>
                            <dxe:ASPxComboBox ID="cboTransactionCode" ClientInstanceName="cboTransactionCode" Width="100%" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboTransactionCodeValueChanged(s); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblJournalNo"><%=GetLabel("Nomor Jurnal") %></label></td>
                        <td id="tdTransactionNoAdd" runat="server">
                            <table  cellpadding="0" cellspacing="0" width="100%">
                                <colgroup>
                                    <col style="width: 50px" />
                                    <col style="width: 3px" />
                                    <col style="width: 160px"/>
                                    <col style="width: 100px" />
                                    <col style="width: 140px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtJournalPrefix" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtJournalNo1" Width="100%" runat="server" ReadOnly="true" /></td>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtJournalDate" CssClass="datepicker" Width="120px" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="display:none;" id="tdTransactionNoEdit" runat="server"><asp:TextBox runat="server" ID="txtJournalNo" Width="220px" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="width: 150px; vertical-align:top; padding-top:5px; "><label class="lblNormal"><%=GetLabel("Keterangan Jurnal")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divTransactionEntry">
                    <table id="tblJournalEntry" class="grdView grdBorder notAllowSelect" cellspacing="0" rules="all" >
                        <tr id="trHeader2">
                            <th style="width:250px"><%=GetLabel("Perkiraan")%></th> 
                            <th style="width:250px"><%=GetLabel("Sub Perkiraan")%></th> 
                            <th><%=GetLabel("Keterangan")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("DEBET")%></th> 
                            <th class="thRight" style="width:110px"><%=GetLabel("KREDIT")%></th> 
                            <th style="width:150px"><%=GetLabel("No. Dokumen")%></th> 
                        </tr>
                        <tr id="trFooter">
                            <td colspan="3" align="right"><%=GetLabel("Total") %> : </td>
                            <td align="center"><input type="text" validationgroup="mpTrx" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td align="center"><input type="text" validationgroup="mpTrx" readonly="readonly" class="txtCurrency" value="0" style="width:99%" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
