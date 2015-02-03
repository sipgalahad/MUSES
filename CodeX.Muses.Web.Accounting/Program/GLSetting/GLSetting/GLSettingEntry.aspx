<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="GLSettingEntry.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.GLSettingEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            function onGetGLAccountFilterExpression() {
                var filterExpression = "IsHeader = 0 AND IsDeleted = 0";
                return filterExpression;
            }

            //#region COA
            $('#lbl.lblLink').click(function () {
                openSearchDialog('chartofaccount', onGetGLAccountFilterExpression(), function (value) {
                    $('#<%=txtGLAccountNo.ClientID %>').val(value);
                    onTxtGLAccountCodeChanged(value);
                });
            });

            $('#<%=txtGLAccountNo.ClientID %>').change(function () {
                onTxtGLAccountCodeChanged($(this).val());
            });

            function onTxtGLAccountCodeChanged(value) {
                var filterExpression = onGetGLAccountFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnGLAccountID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtGLAccountName.ClientID %>').val(result.GLAccountName);
                        $('#<%=hdnSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=hdnSearchDialogTypeName.ClientID %>').val(result.SearchDialogTypeName);
                        $('#<%=hdnIDFieldName.ClientID %>').val(result.IDFieldName);
                        $('#<%=hdnCodeFieldName.ClientID %>').val(result.CodeFieldName);
                        $('#<%=hdnDisplayFieldName.ClientID %>').val(result.DisplayFieldName);
                        $('#<%=hdnMethodName.ClientID %>').val(result.MethodName);
                        $('#<%=hdnFilterExpression.ClientID %>').val(result.FilterExpression);
                        onSubLedgerIDChanged();
                    }
                    else {
                        $('#<%=hdnGLAccountID.ClientID %>').val('');
                        $('#<%=txtGLAccountName.ClientID %>').val('');
                        $('#<%=hdnSubLedgerID.ClientID %>').val('');
                        $('#<%=hdnSearchDialogTypeName.ClientID %>').val('');
                        $('#<%=hdnIDFieldName.ClientID %>').val('');
                        $('#<%=hdnCodeFieldName.ClientID %>').val('');
                        $('#<%=hdnDisplayFieldName.ClientID %>').val('');
                        $('#<%=hdnMethodName.ClientID %>').val('');
                        $('#<%=hdnFilterExpression.ClientID %>').val('');
                    }

                    $('#<%=hdnSubLedger.ClientID %>').val('');
                    $('#<%=txtSubLedgerCode.ClientID %>').val('');
                    $('#<%=txtSubLedgerName.ClientID %>').val('');
                });
            }

            function onSubLedgerIDChanged() {
                if ($('#<%=hdnSubLedgerID.ClientID %>').val() == '0' || $('#<%=hdnSubLedgerID.ClientID %>').val() == '') {
                    $('#<%=lblSubLedger.ClientID %>').attr('class', 'lblDisabled');
                    $('#<%=txtSubLedgerCode.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=lblSubLedger.ClientID %>').attr('class', 'lblLink');
                    $('#<%=txtSubLedgerCode.ClientID %>').removeAttr('readonly');
                }
            }
            //#endregion

            //#region Sub Ledger
            function onGetSubLedgerDtFilterExpression() {
                var filterExpression = $('#<%=hdnFilterExpression.ClientID %>').val().replace('@SubLedgerID', $('#<%=hdnSubLedgerID.ClientID %>').val());
                return filterExpression;
            }

            $('#<%=lblSubLedger.ClientID %>').click(function () {
                if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
                    openSearchDialog($('#<%=hdnSearchDialogTypeName.ClientID %>').val(), onGetSubLedgerDtFilterExpression(), function (value) {
                        $('#<%=txtSubLedgerCode.ClientID %>').val(value);
                        onTxtSubLedgerCodeChanged(value);
                    });
                }
            });

            $('#<%=txtSubLedgerCode.ClientID %>').change(function () {
                onTxtSubLedgerCodeChanged($(this).val());
            });

            function onTxtSubLedgerCodeChanged(value) {
                if ($('#<%=hdnSearchDialogTypeName.ClientID %>').val() != '') {
                    var filterExpression = onGetSubLedgerDtFilterExpression() + " AND " + $('#<%=hdnCodeFieldName.ClientID %>').val() + " = '" + value + "'";
                    Methods.getObject($('#<%=hdnMethodName.ClientID %>').val(), filterExpression, function (result) {
                        if (result != null) {
                            $('#<%=hdnSubLedger.ClientID %>').val(result[$('#<%=hdnIDFieldName.ClientID %>').val()]);
                            $('#<%=txtSubLedgerName.ClientID %>').val(result[$('#<%=hdnDisplayFieldName.ClientID %>').val()]);
                        }
                        else {
                            $('#<%=hdnSubLedger.ClientID %>').val('');
                            $('#<%=txtSubLedgerCode.ClientID %>').val('');
                            $('#<%=txtSubLedgerName.ClientID %>').val('');
                        }
                    });
                }
            }
            //#endregion

            onSubLedgerIDChanged();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtGLSettingCode" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtGLSettingName" Width="220px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top" style="padding-top:5px"><label><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" runat="server" TextMode="MultiLine" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <colgroup>
                        <col style="width: 50%"/>
                    </colgroup>
                    <tr>
                        <td>
                            <table width="100%">
                                <colgroup>
                                    <col width="120px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblLink" id="lbl"><%=GetLabel("Perkiraan")%></label></td>
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
                                                <td><asp:TextBox runat="server" ID="txtGLAccountNo" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtGLAccountName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="tdLabel"><label class="lblDisabled" runat="server" id="lblSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnSubLedger" runat="server" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtSubLedgerCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtSubLedgerName" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
