<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="AdmissionPaymentEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.AdmissionPaymentEntry" %>

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
                $('#<%=txtPaymentName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');

                $('.hdnAdmissionFeeCompID').each(function () {
                    var id = $(this).val();
                    $('#tblEntryDt .trFeeComp' + id + ':gt(0)').each(function () {
                        $(this).remove();
                    });
                });

                $('#tblEntryDt .chkIsPaymentDateNow input').each(function () {
                    $(this).prop('checked', false);
                    $(this).change();
                });

                $('#tblEntryDt .chkIsPaymentAmountInPercentage input').each(function () {
                    $(this).prop('checked', false);
                });

                $('#tblEntryDt .txtPaymentDate').each(function () {
                    $(this).val('');
                });

                $('#tblEntryDt .txtPaymentAmount').each(function () {
                    $(this).val('');
                });

                $('#tblEntryDt .txtNoOfPayment').each(function () {
                    $(this).val('');
                });

                $('#entryDetailContainer').show();
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
            $('.hdnAdmissionFeeCompID').each(function () {
                var id = $(this).val();
                var tempResult = '';
                $('#tblEntryDt .trFeeComp' + id).each(function () {
                    if (tempResult != '')
                        tempResult += ';';
                    var isPaymentDateNow = $(this).find('.chkIsPaymentDateNow input').is(':checked') ? '1' : '0';
                    var paymentDate = $(this).find('.txtPaymentDate').val();
                    var paymentAmount = $(this).find('.txtPaymentAmount').val();
                    var isPaymentAmountInPercentage = $(this).find('.chkIsPaymentAmountInPercentage input').is(':checked') ? '1' : '0';
                    var noOfPayment = $(this).find('.txtNoOfPayment').val();
                    tempResult += isPaymentDateNow + '^' + paymentDate + '^' + paymentAmount + '^' + isPaymentAmountInPercentage + '^' + noOfPayment;
                });
                if (result != '')
                    result += '|';
                result += id + ',' + tempResult;
            });
            $('#<%=hdnPaymentDtSaveValue.ClientID %>').val(result);
        }

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PaymentID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.PaymentID);
            $('#<%=txtPaymentName.ClientID %>').val(entity.PaymentName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            $('.hdnAdmissionFeeCompID').each(function () {
                var id = $(this).val();
                $('#tblEntryDt .trFeeComp' + id + ':gt(0)').each(function () {
                    $(this).remove();
                });
            });

            var filterExpression = "PaymentID = " + entity.PaymentID;
            Methods.getListObject('GetAdmissionPaymentDtList', filterExpression, function (result) {
                var feeCompCount = [];
                for (var i = 0; i < result.length; ++i) {
                    var entity = result[i];
                    if (feeCompCount[entity.AdmissionFeeCompID] == null)
                        feeCompCount[entity.AdmissionFeeCompID] = 0;
                    else
                        feeCompCount[entity.AdmissionFeeCompID]++;
                    var count = feeCompCount[entity.AdmissionFeeCompID];
                    if (count > 0)
                        addEntryDt('trFeeComp' + entity.AdmissionFeeCompID);
                    $tr = $('#tblEntryDt .trFeeComp' + entity.AdmissionFeeCompID + ':eq(' + count + ')');
                    $tr.find('.chkIsPaymentDateNow input').prop('checked', entity.IsPaymentDateNow);
                    $tr.find('.chkIsPaymentDateNow input').change();
                    $tr.find('.txtPaymentDate').val(entity.PaymentDateInDatePickerFormat);
                    $tr.find('.txtPaymentAmount').val(entity.PaymentAmount);
                    $tr.find('.chkIsPaymentAmountInPercentage input').prop('checked', entity.IsPaymentAmountInPercentage);
                    $tr.find('.txtNoOfPayment').val(entity.NoOfPayment);
                }
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
            $('.txtPaymentDate').each(function () {
                setDatePickerElement($(this));
            });
        });

        function addEntryDt(className) {
            $newTr = $($('#tmplEntityDt').html());
            $newTr.addClass(className);
            $newTr.find('.datepicker').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                setDatePickerElement($(this));
            });
            $newTr.insertAfter($('#tblEntryDt').find('.' + className).last());
        }

        $('.divEntryDtAdd').live('click', function () {
            $tr = $(this).closest('tr').prev('tr');
            var className = $tr.attr('class');
            addEntryDt(className);
        });

        $('.chkIsPaymentDateNow input').live('change', function () {
            $tr = $(this).closest('tr');
            $txtPaymentDate = $tr.find('.txtPaymentDate');
            $txtNoOfPayment = $tr.find('.txtNoOfPayment');
            if ($(this).is(':checked')) {
                $txtPaymentDate.attr('readonly', 'readonly');
                $txtNoOfPayment.attr('readonly', 'readonly');
                $txtPaymentDate.val('');
                $txtNoOfPayment.val('1');
            }
            else {
                $txtPaymentDate.removeAttr('readonly');
                $txtNoOfPayment.removeAttr('readonly');
            }
        });

        $('.divDeleteEntryDt').live('click', function () {
            $tr = $(this).closest('tr');
            $tr.remove();
        });
    </script>
    <input type="hidden" id="hdnPaymentDtSaveValue" runat="server" />
    <div class="divTransactionEntry">
        <script id="tmplEntityDt" type="text/x-jquery-tmpl">
            <tr>
                <td class="tdLabel">&nbsp;</td>
                <td align="center"><asp:CheckBox ID="chkIsPaymentDateNow" CssClass="chkIsPaymentDateNow" runat="server" /></td>
                <td align="center"><asp:TextBox ID="txtPaymentDate" Width="120px" runat="server" CssClass="datepicker txtPaymentDate" /></td>
                <td align="center"><asp:TextBox ID="txtPaymentAmount" Width="100%" runat="server" CssClass="txtCurrency txtPaymentAmount" /></td>
                <td align="center"><asp:CheckBox ID="chkIsPaymentAmountInPercentage" CssClass="chkIsPaymentAmountInPercentage" runat="server" /></td>
                <td align="center"><asp:TextBox ID="txtNoOfPayment" Width="100%" runat="server" CssClass="number txtNoOfPayment" /></td>
                <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
            </tr>
        </script>

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
                            <table id="tblEntryDt">
                                <colgroup>
                                    <col style="width: 160px" />
                                    <col style="width: 110px" />
                                    <col style="width: 145px" />
                                    <col style="width: 150px" />
                                    <col style="width: 40px" />
                                    <col style="width: 80px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td colspan="5"><asp:TextBox ID="txtPaymentName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td colspan="5"><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="100%" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Bayar Langsung") %></div></td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Tanggal Pembayaran") %></div></td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Jumlah Pembayaran") %></div></td>
                                    <td align="center"><div class="lblComponent">[%]</div></td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Frek Bayar") %></div></td>
                                </tr>
                                <asp:Repeater ID="rptAdmissionFeeComp" runat="server" OnItemDataBound="rptAdmissionFeeComp_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class='trFeeComp<%#Eval("AdmissionFeeCompID")%>'>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("AdmissionFeeCompType")%></label></td>
                                            <td align="center">
                                                <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID")%>' />
                                                <asp:CheckBox ID="chkIsPaymentDateNow" CssClass="chkIsPaymentDateNow" runat="server" />
                                            </td>
                                            <td align="center"><asp:TextBox ID="txtPaymentDate" Width="120px" runat="server" CssClass="datepicker txtPaymentDate" /></td>
                                            <td align="center"><asp:TextBox ID="txtPaymentAmount" Width="100%" runat="server" CssClass="txtCurrency txtPaymentAmount" /></td>
                                            <td align="center"><asp:CheckBox ID="chkIsPaymentAmountInPercentage" CssClass="chkIsPaymentAmountInPercentage" runat="server" /></td>
                                            <td align="center"><asp:TextBox ID="txtNoOfPayment" Width="100%" runat="server" CssClass="number txtNoOfPayment" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td colspan="5">
                                                <span class="divAdd divEntryDtAdd"><%=GetLabel("Tambah Data")%></span><br />
                                            </td>
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
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="PaymentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="PaymentName" HeaderText="Nama" HeaderStyle-Width="150px"/>
                                <asp:BoundField DataField="cfRemarks" HtmlEncode="false" HeaderText="Keterangan" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("PaymentID") %>" bindingfield="PaymentID" />
                                        <input type="hidden" value="<%#Eval("PaymentName") %>" bindingfield="PaymentName" />
                                        <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("No Data To Display")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
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