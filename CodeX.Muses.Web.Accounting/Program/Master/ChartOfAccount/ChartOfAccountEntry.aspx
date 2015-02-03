<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="ChartOfAccountEntry.aspx.cs" Inherits="CodeX.Muses.Web.Accounting.Program.ChartOfAccountEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            //#region Parent
            function onGetParentFilterExpression() {
                var filterExpression = "GCGLAccountType = '" + cboGCGLAccountType.GetValue() + "' AND IsHeader = 1 AND IsDeleted = 0";
                return filterExpression;
            }

            $('#lblParent.lblLink').click(function () {
                openSearchDialog('vchartofaccount', onGetParentFilterExpression(), function (value) {
                    $('#<%=txtParentAccountNo.ClientID %>').val(value);
                    onTxtParentAccountNoChanged(value);
                });
            });

            $('#<%=txtParentAccountNo.ClientID %>').change(function () {
                onTxtParentAccountNoChanged($(this).val());
            });

            function onTxtParentAccountNoChanged(value) {
                var filterExpression = onGetParentFilterExpression() + " AND GLAccountNo = '" + value + "'";
                Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnParentAccountID.ClientID %>').val(result.GLAccountID);
                        $('#<%=txtParentAccountName.ClientID %>').val(result.GLAccountName);
                        $('#<%=txtAccountLevel.ClientID %>').val(result.AccountLevel + 1);
                    }
                    else {
                        $('#<%=hdnParentAccountID.ClientID %>').val('');
                        $('#<%=txtParentAccountNo.ClientID %>').val('');
                        $('#<%=txtParentAccountName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Sub Ledger
            function onGetSubLedgerFilterExpression() {
                var filterExpression = "IsDeleted = 0";
                return filterExpression;
            }

            $('#lblSubLedger.lblLink').click(function () {
                openSearchDialog('subledgerhd', onGetSubLedgerFilterExpression(), function (value) {
                    $('#<%=txtSubLedgerCode.ClientID %>').val(value);
                    onTxtSubLedgerCodeChanged(value);
                });
            });

            $('#<%=txtSubLedgerCode.ClientID %>').change(function () {
                onTxtSubLedgerCodeChanged($(this).val());
            });

            function onTxtSubLedgerCodeChanged(value) {
                var filterExpression = onGetSubLedgerFilterExpression() + " AND SubLedgerCode = '" + value + "'";
                Methods.getObject('GetSubLedgerHdList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSubLedgerID.ClientID %>').val(result.SubLedgerID);
                        $('#<%=txtSubLedgerName.ClientID %>').val(result.SubLedgerName);
                    }
                    else {
                        $('#<%=hdnSubLedgerID.ClientID %>').val('');
                        $('#<%=txtSubLedgerCode.ClientID %>').val('');
                        $('#<%=txtSubLedgerName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#btnSubLedgerDt').click(function () {
                var subLedgerID = $('#<%=hdnSubLedgerID.ClientID %>').val();
                if (subLedgerID != '' && subLedgerID != '0') {
                    var url = ResolveUrl("~/Program/Master/SubLedger/SubLedgerDtViewCtl.ascx");
                    openUserControlPopup(url, subLedgerID, 'Detail', 1000, 520);
                }
            });
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Perkiraan")%></label></td>
                        <td><asp:TextBox ID="txtGLAccountNo" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Perkiraan")%></label></td>
                        <td><asp:TextBox ID="txtGLAccountName" Width="350" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelompok Perkiraan")%></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboGCGLAccountType" ClientInstanceName="cboGCGLAccountType" Width="355 px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblParent"><%=GetLabel("Kode Induk")%></label></td>
                        <td>
                            <input type="hidden" id="hdnParentAccountID" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtParentAccountNo" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtParentAccountName" Width="100%" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblSubLedger"><%=GetLabel("Sub Perkiraan")%></label></td>
                        <td>
                            <input type="hidden" id="hdnSubLedgerID" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                    <col style="width:3px"/>
                                    <col style="width:20px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtSubLedgerCode" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtSubLedgerName" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><input type="button" class="btnMore" value="..." id="btnSubLedgerDt" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("D / K")%></label></td>
                        <td><asp:RadioButtonList ID="rblPosition" runat="server" RepeatDirection="Horizontal" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Level")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtAccountLevel" Width="120px" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsHeader" Text="Induk" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"></td>
                        <td><asp:CheckBox runat="server" ID="chkIsUsingDocumentControl" Text="Kontrol Dokumen" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
