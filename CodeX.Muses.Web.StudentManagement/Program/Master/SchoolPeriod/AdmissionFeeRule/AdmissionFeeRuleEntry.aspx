<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="AdmissionFeeRuleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.AdmissionFeeRuleEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtAdmissionFeeRuleName.ClientID %>').val('');
                $('#<%=chkIsFeeder.ClientID %>').prop('checked', false);
                $('.txtAdmissionFeeAmount').each(function () {
                    if ($(this).attr('readonly') == null)
                        $(this).val('0').trigger('changeValue');
                });

                $('.trPeriodAdmission').each(function () {
                    var total = 0;
                    $(this).find('.txtAdmissionFeeAmount').each(function () {
                        var feeAmount = parseFloat($(this).attr('hiddenVal'));
                        total += feeAmount;
                    });
                    $(this).find('.txtAdmissionFeeAmountTotal').val(total).trigger('changeValue');
                });

                $('#entryDetailContainer').show();
            });

            $('.txtAdmissionFeeAmount').change(function () {
                $(this).blur();
                $tr = $(this).closest('tr');
                var total = 0;
                $tr.find('.txtAdmissionFeeAmount').each(function () {
                    var feeAmount = parseFloat($(this).attr('hiddenVal'));
                    total += feeAmount;
                });
                $tr.find('.txtAdmissionFeeAmountTotal').val(total).trigger('changeValue');
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    getSaveValue();
                    cbpProcess.PerformCallback('save');
                }
            });
        });

        function getSaveValue() {
            var result = '';
            $('.txtAdmissionFeeAmount').each(function () {
                if ($(this).attr('readonly') == null) {
                    var feeAmount = parseFloat($(this).attr('hiddenVal'));
                    var admissionFeeCompID = $(this).parent().find('.hdnAdmissionFeeCompID').val();
                    var periodAdmissionID = $(this).closest('tr').find('.hdnPeriodAdmissionID').val();
                    if (result != '')
                        result += '|';
                    result += periodAdmissionID + ';' + admissionFeeCompID + ';' + feeAmount;
                }
            });
            $('#<%=hdnAdmissionFeeRuleDtSaveValue.ClientID %>').val(result);
        }

        //#region edit and delete
        $('#tblView .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.AdmissionFeeRuleID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#tblView .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.AdmissionFeeRuleID);
            $('#<%=txtAdmissionFeeRuleName.ClientID %>').val(entity.AdmissionFeeRuleName);
            $('#<%=chkIsFeeder.ClientID %>').prop('checked', entity.IsFeeder == 'True');

            $('.txtAdmissionFeeAmount').each(function () {
                if ($(this).attr('readonly') == null) {
                    $txt = $(this);
                    var admissionFeeCompID = $(this).parent().find('.hdnAdmissionFeeCompID').val();
                    var periodAdmissionID = $(this).closest('tr').find('.hdnPeriodAdmissionID').val();

                    $row.find('.tdFeeRuleDt').each(function () {
                        var periodAdmissionID1 = $(this).find('.hdnPeriodAdmissionID').val();
                        var admissionFeeCompID1 = $(this).find('.hdnAdmissionFeeCompID').val();

                        if (periodAdmissionID == periodAdmissionID1 && admissionFeeCompID == admissionFeeCompID1) {
                            var feeAmount = $(this).find('.hdnTotalAmount').val();
                            $txt.val(feeAmount).trigger('changeValue');
                        }
                    });
                }
            });

            $('.trPeriodAdmission').each(function () {
                var total = 0;
                $(this).find('.txtAdmissionFeeAmount').each(function () {
                    var feeAmount = parseFloat($(this).attr('hiddenVal'));
                    total += feeAmount;
                });
                $(this).find('.txtAdmissionFeeAmountTotal').val(total).trigger('changeValue');
            });

            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
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

        $(function () {
            addTableHeader();
        });

        function addTableHeader() {
            $('#tblView thead').html($('#tblView1 thead').html());
        }

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            addTableHeader();
        }
    </script>
    <input type="hidden" runat="server" id="hdnAdmissionFeeRuleDtSaveValue" />
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 160px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td colspan="20"><asp:TextBox ID="txtAdmissionFeeRuleName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Feeder")%></label></td>
                                    <td colspan="20"><asp:CheckBox ID="chkIsFeeder" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <asp:Repeater ID="rptAdmissionFeeComp" runat="server">
                                        <ItemTemplate>
                                            <td align="center" width="150px"><div class="lblComponent"><%#Eval("AdmissionFeeCompType")%></div></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td align="center" width="150px"><div class="lblComponent"><%=GetLabel("Total")%></div></td>
                                </tr>
                                <asp:Repeater ID="rptPeriodAdmission" runat="server" OnItemDataBound="rptPeriodAdmission_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="trPeriodAdmission">
                                            <td class="tdLabel">
                                                <input type="hidden" class="hdnPeriodAdmissionID" value='<%#Eval("PeriodAdmissionID")%>' />
                                                <%#Eval("PeriodAdmissionName")%>
                                            </td>
                                            <asp:Repeater ID="rptAdmissionFeeCompDt" runat="server" OnItemDataBound="rptAdmissionFeeCompDt_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="center">
                                                        <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID")%>' />
                                                        <asp:TextBox ID="txtAdmissionFeeAmount" runat="server" Width="99%" CssClass="txtAdmissionFeeAmount txtCurrency"/>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="center"><asp:TextBox ID="txtAdmissionFeeAmountTotal" ReadOnly="true" runat="server" Width="99%" CssClass="txtAdmissionFeeAmountTotal txtCurrency"/></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <table id="tblView1" rules="all" class="tblTransactionEntryResult grdBorder" style="display:none">
            <thead>
                <tr>
                    <th rowspan="2"><%=GetLabel("Nama")%></th>
                    <th style="width:80px" class="thCenter" rowspan="2"><%=GetLabel("Feeder")%></th>
                    <asp:Repeater ID="rptAdmissionFeeCompView" runat="server" OnItemDataBound="rptAdmissionFeeCompView_ItemDataBound">
                        <ItemTemplate>
                            <th class="thCenter" id="thAdmissionFeeCompType" runat="server"><%#Eval("AdmissionFeeCompType")%></th>
                        </ItemTemplate>
                    </asp:Repeater>
                    <th id="thFeeCompTotal" runat="server" class="thCenter"><%=GetLabel("Total")%></th>
                    <th style="width:80px" rowspan="2"></th>
                </tr>
                <tr id="trHeader1">
                    <asp:Repeater ID="rptAdmissionFeeCompViewDt" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" width="130px"><%#Eval("PeriodAdmissionName")%></th>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptAdmissionFeeCompViewDtTotal" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" width="130px"><%#Eval("PeriodAdmissionName")%></th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </thead>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblView" rules="all" class="tblTransactionEntryResult grdBorder">
                                    <thead>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="trDt">
                                    <td><%#Eval("AdmissionFeeRuleName")%></td>
                                    <td align="center"><asp:CheckBox ID="chkIsFeeder" Enabled="false" Checked='<%#Eval("IsFeeder")%>' runat="server" /></td>
                                    <asp:Repeater ID="rptViewDt" runat="server">
                                        <ItemTemplate>
                                            <td class="thRight tdFeeRuleDt">
                                                <input type="hidden" class="hdnTotalAmount" value='<%#Eval("TotalAmount")%>' />
                                                <input type="hidden" class="hdnPeriodAdmissionID" value='<%#Eval("PeriodAdmissionID")%>' />
                                                <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID")%>' />
                                                <%#Eval("TotalAmount", "{0:N}")%>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="rptViewDtTotal" runat="server">
                                        <ItemTemplate>
                                            <td class="thRight">
                                                <%#Eval("TotalAmount", "{0:N}")%>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td align="center">
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("AdmissionFeeRuleID") %>" bindingfield="AdmissionFeeRuleID" />
                                        <input type="hidden" value="<%#Eval("AdmissionFeeRuleName") %>" bindingfield="AdmissionFeeRuleName" />
                                        <input type="hidden" value="<%#Eval("IsFeeder") %>" bindingfield="IsFeeder" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>                                
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>