<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="AdmissionFeeEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.AdmissionFeeEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnPrint" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/print.png")%>' alt="" /><div><%=GetLabel("Print")%></div></li>
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Simpan")%></div></li>
    <li id="btnGenerateAR" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Generate Tagihan")%></div></li>
    <li id="btnVoid" runat="server" CRUDMode="R" style="display:none;"><img src='<%=ResolveUrl("~/Libs/Images/Icon/delete.png")%>' alt="" /><div><%=GetLabel("Batal Tagihan")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region Registration
        function onGetRegistrationFilterExpression() {
            var filterExpression = "<%=OnGetRegistrationFilterExpression() %>";
            return filterExpression;
        }

        function onTacRegistrationButtonSearchClick() {
            openSearchDialog('registration', onGetRegistrationFilterExpression(), function (value) {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    if (result != null) {
                        tacRegistration.setValue(result.RegistrationID);
                        tacRegistration.setText(result.ProspectiveStudentName);
                        entityToControlRegistration(result);
                    }
                    else {
                        tacRegistration.setValue('');
                        tacRegistration.setText('');
                        entityToControlRegistration(result);
                    }
                });
            });

        }

        function onTacRegistrationValueChanged() {
            var id = tacRegistration.getValue();
            if (id != '') {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    entityToControlRegistration(result);
                });
            }
        }

        function entityToControlRegistration(result) {
            if (result != null) {
                if (result.IsFeeder)
                    $('#<%=hdnIsFeeder.ClientID %>').val('1');
                else
                    $('#<%=hdnIsFeeder.ClientID %>').val('0');

                $('#<%=hdnSchoolDate.ClientID %>').val(result.SchoolDateInDatePickerFormat); 
                $('#<%=hdnProspectiveStudentID.ClientID %>').val(result.ProspectiveStudentID); 
                if (result.GCRegistrationStatus == "<%=OnGetRegistrationStatusAccepted() %>") {
                    $('#<%=tblInfoOutstandingTransfer.ClientID %>').hide();
                    $('#<%=btnGenerateAR.ClientID %>').show();
                    $('#<%=btnSave.ClientID %>').show();
                    $('#<%=btnVoid.ClientID %>').hide();
                    $('#<%=btnPrint.ClientID %>').hide();
                }
                else {
                    $('#<%=tblInfoOutstandingTransfer.ClientID %>').show();
                    $('#<%=btnGenerateAR.ClientID %>').hide();
                    $('#<%=btnSave.ClientID %>').hide();
                    $('#<%=btnVoid.ClientID %>').show();
                    $('#<%=btnPrint.ClientID %>').show();
                }
                tacAdmissionFeeRule.setValue(result.AdmissionFeeRuleID);
                tacAdmissionFeeRule.setText(result.AdmissionFeeRuleName);
                if (result.PaymentID > 0)
                    cboPaymentType.SetValue(result.PaymentID);
                else
                    cboPaymentType.SetValue('');
                $('#<%=hdnAdmissionFeeRuleID.ClientID %>').val(result.AdmissionFeeRuleID);
            }
            else {
                $('#<%=hdnIsFeeder.ClientID %>').val('0');
                $('#<%=tblInfoOutstandingTransfer.ClientID %>').hide();
                $('#<%=btnGenerateAR.ClientID %>').show();
                $('#<%=btnSave.ClientID %>').show();
                $('#<%=btnVoid.ClientID %>').hide();
                $('#<%=btnPrint.ClientID %>').hide();
                tacAdmissionFeeRule.setValue('');
                tacAdmissionFeeRule.setText('');
                cboPaymentType.SetValue('');
                $('#<%=hdnAdmissionFeeRuleID.ClientID %>').val('0');
            }
            cbpScholarship.PerformCallback('refresh');
        }
        //#endregion

        //#region Admission Fee Rule
        function onGetAdmissionFeeRuleFilterExpression() {
            var filterExpression = "<%=OnGetAdmissionFeeRuleFilterExpression() %>";
            if ($('#<%=hdnIsFeeder.ClientID %>').val() == '1')
                filterExpression += "<%=OnGetAdmissionFeeRuleFeederFilterExpression() %>";
            else
                filterExpression += "<%=OnGetAdmissionFeeRuleNonFeederFilterExpression() %>";
            return filterExpression;
        }

        function onTacAdmissionFeeRuleButtonSearchClick() {
            openSearchDialog('admissionfeerule', onGetAdmissionFeeRuleFilterExpression(), function (value) {
                var filterExpression = onGetAdmissionFeeRuleFilterExpression() + " AND AdmissionFeeRuleID = '" + value + "'";
                Methods.getObject('GetAdmissionFeeRuleHdList', filterExpression, function (result) {
                    if (result != null) {
                        tacAdmissionFeeRule.setValue(result.AdmissionFeeRuleID);
                        tacAdmissionFeeRule.setText(result.AdmissionFeeRuleName);
                    }
                    else {
                        tacAdmissionFeeRule.setValue('');
                        tacAdmissionFeeRule.setText('');
                    }
                });
            });

        }

        function onTacAdmissionFeeRuleValueChanged() {
        }
        //#endregion

        function calculateTotal() {
            $('.txtTotalTransactionAmount').each(function () {
                $tbl = $(this).closest('.tblView');
                var totalPayment = 0;
                $tbl.find('tr.trDetail').each(function () {
                    totalPayment += parseFloat($(this).find('.txtTransactionAmount').attr('hiddenVal'));
                });
                $(this).val(totalPayment).trigger('changeValue');
            });
        }

        function onCbpViewEndCallback(s) {
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            $('.txtDueDate').each(function () {
                setDatePickerElement($(this));
            });
            calculateTotal();
            hideLoadingPanel();
        }

        function onAfterCustomClickSuccess(type) {
            if (type == 'generateAR') {
                $('#<%=tblInfoOutstandingTransfer.ClientID %>').show();
                $('#<%=btnGenerateAR.ClientID %>').hide();
                $('#<%=btnSave.ClientID %>').hide();
                $('#<%=btnVoid.ClientID %>').show();
                $('#<%=btnPrint.ClientID %>').show();
            }
            else {
                $('#<%=tblInfoOutstandingTransfer.ClientID %>').hide();
                $('#<%=btnGenerateAR.ClientID %>').show();
                $('#<%=btnSave.ClientID %>').show();
                $('#<%=btnVoid.ClientID %>').hide();
                $('#<%=btnPrint.ClientID %>').hide();
            }
        }

        $(function () {
            $('#btnGenerate').click(function () {
                if (IsValid(null, 'fsFilterGenerate', 'mpFilterGenerate'))
                    cbpView.PerformCallback('refresh|0');
            });

            $('#<%=btnGenerateAR.ClientID %>').click(function () {
                if (IsValid(null, 'fsFilterGenerate', 'mpFilterGenerate')) {
                    if (onBeforeSaveValue())
                        onCustomButtonClick('generateAR');
                }
            });

            $('#<%=btnPrint.ClientID %>').click(function () {
                var reportCode = "SM-00001";
                var registrationID = tacRegistration.getValue();
                var filterExpression = "RegistrationID = " + registrationID;
                openReportViewer(reportCode, filterExpression);
            });

            $('#<%=btnVoid.ClientID %>').click(function () {
                showToastConfirmation('Apakah Anda Yakin? Semua Tagihan Untuk Calon Siswa Ybs Akan Dibatalkan.', function (result) {
                    if (result)
                        onCustomButtonClick('void');
                });
            });
            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsFilterGenerate', 'mpFilterGenerate')) {
                    if (onBeforeSaveValue())
                        onCustomButtonClick('save');
                }
            });
        });

        function onBeforeSaveValue() {
            if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                var isAllowSave = true;
                $('.txtTotalTransactionAmount').each(function () {
                    var totalTransactionAmount = parseFloat($(this).attr('hiddenVal'));
                    var totalTransactionAmount1 = parseFloat($(this).closest('.tblView').parent().closest('tr').prev().find('.txtAdmissionFeeCompTransactionAmount').attr('hiddenVal'));
                    if (totalTransactionAmount != totalTransactionAmount1) {
                        $(this).addClass('error');
                        isAllowSave = false;
                    }
                    else
                        $(this).removeClass('error');
                });
                $('.txtLineAmount').each(function () {
                    var value = parseFloat($(this).attr('hiddenVal'));
                    if (value < 0) {
                        $(this).addClass('error');
                        isAllowSave = false;
                    }
                    else
                        $(this).removeClass('error');
                });
                if (isAllowSave) {
                    getSaveValue();
                }
                return isAllowSave;
            }
            return false;
        }

        function getSaveValue() {
            var lstSaveValue = '';
            $('.hdnStudentFeeCompTypeID').each(function () {
                var studentFeeCompTypeID = $(this).val();
                var GCAdmissionPaymentPeriod = $(this).attr('gcadmissionpaymentperiod');
                $tr = $(this).closest('tr');
                var noOfPeriod = $tr.prev().prev().prev().prev().find('.txtNoOfRegistrationPaymentPeriod').val();
                var admissionFeeCompValue = $tr.prev().prev().prev().prev().prev().find('.txtAdmissionFeeCompAmount').attr('hiddenVal');
                $trDiscount = $tr.prev().prev();
                var discountPercentage = $trDiscount.find('.txtDiscountPercentage').val();
                var discountAmount = $trDiscount.find('.txtTotalDiscountAmount').attr('hiddenVal');
                $tbl = $(this).next().find('.tblView');
                var lstTemp = '';
                var paymentDate = '';
                $tbl.find('.trDetail').each(function () {
                    if (lstTemp != '')
                        lstTemp += ',';
                    paymentDate = $(this).find('.txtDueDate').val();
                    var paymentAmountInPercentage = $(this).find('.txtTransactionAmountInPercentage').val();
                    var paymentAmount = $(this).find('.txtTransactionAmount').attr('hiddenVal');
                    lstTemp += paymentDate + '^' + paymentAmountInPercentage + '^' + paymentAmount;
                });
                if (lstSaveValue != '')
                    lstSaveValue += '|';
                lstSaveValue += studentFeeCompTypeID + ';' + noOfPeriod + ';' + admissionFeeCompValue + ';' + GCAdmissionPaymentPeriod + ';' + discountPercentage + ';' + discountAmount + ';' + paymentDate + ';' + lstTemp;
            });
            $('#<%=hdnSaveValue.ClientID %>').val(lstSaveValue);
        }

        //#region Header
        $('.txtAdmissionFeeCompAmount').live('change', function () {
            $(this).blur();
            var admissionFeeCompAmount = parseFloat($(this).attr('hiddenVal'));

            $tr = $(this).closest('tr');
            var noOfPeriod = parseInt($tr.next().find('.txtNoOfRegistrationPaymentPeriod').val());
            admissionFeeCompAmount = admissionFeeCompAmount * noOfPeriod;
            $tr.next().next().find('.txtAdmissionFeeCompGrossTransactionAmount').val(admissionFeeCompAmount).trigger('changeValue');

            $trDiscount = $tr.next().next().next();
            var discountPercentage = parseFloat($trDiscount.find('.txtDiscountPercentage').val());
            var discountAmount = admissionFeeCompAmount * discountPercentage / 100;
            $trDiscount.find('.txtTotalDiscountAmount').val(discountAmount).trigger('changeValue');

            admissionFeeCompAmount = admissionFeeCompAmount - discountAmount;
            setAdmissionFeeCompAmount($trDiscount, admissionFeeCompAmount);
        });

        $('.txtDiscountPercentage').live('change', function () {
            var discountPercentage = parseFloat($(this).val());
            $trDiscount = $(this).closest('tr');

            var admissionFeeCompAmount = parseFloat($trDiscount.prev().find('.txtAdmissionFeeCompGrossTransactionAmount').attr('hiddenVal'));
            var discountAmount = admissionFeeCompAmount * discountPercentage / 100;
            $trDiscount.find('.txtTotalDiscountAmount').val(discountAmount).trigger('changeValue');

            admissionFeeCompAmount = admissionFeeCompAmount - discountAmount;
            setAdmissionFeeCompAmount($trDiscount, admissionFeeCompAmount);
        });

        $('.txtTotalDiscountAmount').live('change', function () {
            $(this).blur();
            var discountAmount = parseFloat($(this).attr('hiddenVal'));
            $trDiscount = $(this).closest('tr');

            var admissionFeeCompAmount = parseFloat($trDiscount.prev().find('.txtAdmissionFeeCompGrossTransactionAmount').attr('hiddenVal'));
            var discountPercentage = discountAmount / admissionFeeCompAmount * 100;
            $trDiscount.find('.txtDiscountPercentage').val(discountPercentage).trigger('changeValue');

            admissionFeeCompAmount = admissionFeeCompAmount - discountAmount;
            setAdmissionFeeCompAmount($trDiscount, admissionFeeCompAmount);
        });

        function setAdmissionFeeCompAmount($trDiscount, admissionFeeCompAmount) {
            $trDiscount.next().find('.txtAdmissionFeeCompTransactionAmount').val(admissionFeeCompAmount).trigger('changeValue');

            $tbl = $trDiscount.next().next().find('.tblView');
            var totalPayment = 0;
            $tbl.find('.trDetail').each(function () {
                var paymentAmountInPercentage = parseFloat($(this).find('.txtTransactionAmountInPercentage').val());
                var paymentAmount = admissionFeeCompAmount * paymentAmountInPercentage / 100;
                $(this).find('.txtTransactionAmount').val(paymentAmount).trigger('changeValue');
                totalPayment += paymentAmount;
            });
            $tbl.find('.txtTotalTransactionAmount').val(totalPayment).trigger('changeValue');
        }
        //#endregion

        //#region Detail
        $('.txtTransactionAmountInPercentage').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var admissionFeeCompAmount = parseFloat($tr.parent().closest('tr').prev().find('.txtAdmissionFeeCompTransactionAmount').attr('hiddenVal'));

            var paymentAmountInPercentage = parseFloat($(this).val());
            var paymentAmount = admissionFeeCompAmount * paymentAmountInPercentage / 100;
            $tr.find('.txtTransactionAmount').val(paymentAmount).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtTransactionAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalTransactionAmount').val(totalPayment).trigger('changeValue');
            calculateTotal();
        });
        $('.txtTransactionAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var admissionFeeCompAmount = parseFloat($tr.parent().closest('tr').prev().find('.txtAdmissionFeeCompTransactionAmount').attr('hiddenVal'));

            var paymentAmount = parseFloat($(this).attr('hiddenVal'));
            var paymentAmountInPercentage = paymentAmount * 100 / admissionFeeCompAmount;
            $tr.find('.txtTransactionAmountInPercentage').val(paymentAmountInPercentage).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtTransactionAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalTransactionAmount').val(totalPayment).trigger('changeValue');
            calculateTotal();
        });
        //#endregion

        //#region Dde Scholarship
        $('.chkIsSelected input').live('change', function () {
            $('.chkSelectAll input').prop('checked', false);
            setDdeScholarshipText();
        });

        $('.chkSelectAll input').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
            setDdeScholarshipText();
        });

        function setDdeScholarshipText() {
            var scholarshipName = '';
            var lstScholarshipID = '';
            $('.chkIsSelected input:checked').each(function () {
                $tr = $(this).closest('tr');
                if (scholarshipName != '') {
                    scholarshipName += ', ';
                    lstScholarshipID += ',';
                }
                lstScholarshipID += $tr.find('.keyField').html();
                scholarshipName += $tr.find('.hdnScholarshipName').val();
            });
            ddeScholarship.SetText(scholarshipName);
            $('#<%=hdnLstScholarshipID.ClientID %>').val(lstScholarshipID);
        }

        function onCbpScholarshipEndCallback(s) {
            if (parseFloat($('#<%=hdnAdmissionFeeRuleID.ClientID %>').val()) > 0) {
                setDdeScholarshipText();
                cbpView.PerformCallback('refresh|1');
            }
            else {
                $('#tblAdmissionFee').remove();
                hideLoadingPanel();
            }
        }
        //#endregion



        $('#lblEntryPopupAddData').live('click', function () {
            $tr = $(this).closest('tr').find('.tblView tr:eq(1)');
            var className = $tr.attr('class').split(' ')[1];
            addEntryDt(className);
        });

        $('.divDetailDelete').live('click', function () {
            $row1 = $(this).closest('tr').parent().parent().parent();
            $row = $(this).closest('tr');
            $row.remove();
        });

        function addEntryDt(className) {
            var rowCount = parseInt($tr.closest('.tblView').find('.' + className).last().find('td:eq(1)').html()) + 1;
            $newTr = $($('#tmplEntityDt').html());
            $newTr.addClass(className);
            $newTr.insertAfter($('.tblView').find('.' + className).last());

            var keyField = className.replace("trDetail", "");
            var text = $newTr.html();
            text = text.replace('{DisplayOrder}', rowCount);
            text = text.replace('{KeyField}', keyField);
            text = text.replace('{KeyField}', keyField);
            $newTr.html(text);

            $('.txtDueDate').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                setDatePickerElement($(this));
            });
        }
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnYear" value="0" runat="server" />
    <input type="hidden" id="hdnMonth" value="0" runat="server" />
    <input type="hidden" id="hdnAdmissionType" value="0" runat="server" />
    <input type="hidden" id="hdnSchoolDate" value="0" runat="server" />
    <input type="hidden" id="hdnAdmissionFeeRuleID" value="0" runat="server" />
    <input type="hidden" id="hdnSchoolPeriodID" value="0" runat="server" />
    <input type="hidden" id="hdnSaveValue" value="0" runat="server" />
    <input type="hidden" id="hdnLstScholarshipID" value="" runat="server" />
    <div>
        <script id="tmplEntityDt" type="text/x-jquery-tmpl">
            <tr class="trDetail">
                <td class="keyField">0</td>
                <td align="center">{DisplayOrder}</td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtDueDate datepicker required txtDueDate{KeyField}" value='' style="width:120px" /></td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtTransactionAmountInPercentage number required txtTransactionAmountInPercentage{KeyField}" style="width:90%" value='0' /></td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtTransactionAmount txtCurrency required txtTransactionAmount{KeyField}" style="width:90%" value='0' /></td>
                <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
            </tr>
        </script>
        <div style="float:right">            
            <table id="tblInfoOutstandingTransfer" runat="server" style="display:none;">
                <tr>
                    <td><img height="24" src='<%= ResolveUrl("~/Libs/Images/Button/warning.png")%>' alt='' /></td>
                    <td><label class="lblInfo" id="lblInfoOutstandingBill"><%=GetLabel("Sudah Dibuat Tagihan Untuk Calon Siswa Yang Bersangkutan. Tidak Bisa Diubah") %></label></td>
                </tr>
            </table>
        </div>
        <fieldset id="fsFilterGenerate">
            <table>
                <colgroup>
                    <col style="width:150px"/>
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Calon Siswa")%></label></td>
                    <td>
                        <input type="hidden" id="hdnIsFeeder" runat="server" />
                        <input type="hidden" id="hdnProspectiveStudentID" runat="server" />
                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRegistration" ClientInstanceName="tacRegistration" MethodName="GetvRegistrationList" GetFilterExpressionFunction="onGetRegistrationFilterExpression"
                            SearchFields="ProspectiveStudentName,RegistrationNo" TextField="ProspectiveStudentName" ValueField="RegistrationID" SearchText="${ProspectiveStudentName} (<b>${RegistrationNo}</b>)" OrderByExpression="ProspectiveStudentName">
                            <ClientSideEvents ButtonSearchClick="function(){ onTacRegistrationButtonSearchClick(); }"
                                ValueChanged="function(){ onTacRegistrationValueChanged(); }" />
                        </cdx:CodeXAutoCompleteTextBox>   
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Biaya Siswa")%></label></td>
                    <td>
                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacAdmissionFeeRule" ClientInstanceName="tacAdmissionFeeRule" MethodName="GetAdmissionFeeRuleHdList" GetFilterExpressionFunction="onGetAdmissionFeeRuleFilterExpression"
                            SearchFields="AdmissionFeeRuleName" TextField="AdmissionFeeRuleName" ValueField="RegistrationID" SearchText="${AdmissionFeeRuleName}" OrderByExpression="AdmissionFeeRuleName">
                            <ClientSideEvents ButtonSearchClick="function(){ onTacAdmissionFeeRuleButtonSearchClick(); }"
                                ValueChanged="function(){ onTacAdmissionFeeRuleValueChanged(); }" />
                        </cdx:CodeXAutoCompleteTextBox>   
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><%=GetLabel("Beasiswa") %></td>
                    <td>
                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeScholarship" ID="ddeScholarship"
                            Width="300px" runat="server" EnableAnimation="False">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dxcp:ASPxCallbackPanel ID="cbpScholarship" runat="server" Width="100%" ClientInstanceName="cbpScholarship"
                                    ShowLoadingPanel="false" OnCallback="cbpScholarship_Callback">
                                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                        EndCallback="function(s,e){ onCbpScholarshipEndCallback(s); }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server">
                                            <asp:GridView ID="grdScholarship" runat="server" CssClass="grdNormal grdBorder notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdScholarship_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ScholarshipID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                    <asp:TemplateField HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkSelectAll" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIsSelected" CssClass="chkIsSelected" runat="server" />
                                                            <input type="hidden" class="hdnScholarshipName" value='<%#Eval("ScholarshipName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ScholarshipName" HeaderText="Beasiswa" HeaderStyle-HorizontalAlign="Left" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <%=GetLabel("Data Tidak Tersedia")%>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>    
                            </DropDownWindowTemplate>
                        </dxe:ASPxDropDownEdit>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Cara Pembayaran") %></label></td>
                    <td><dxe:ASPxComboBox ID="cboPaymentType" ClientInstanceName="cboPaymentType" runat="server" Width="150px" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">&nbsp;</td>
                    <td><input type="button" id="btnGenerate" value='<%=GetLabel("Generate") %>' /></td>
                </tr>
            </table>
        </fieldset>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <asp:Repeater ID="rptAdmissionComp" runat="server" OnItemDataBound="rptAdmissionComp_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblAdmissionFee">
                                    <colgroup>
                                        <col style="width:170px"/>
                                        <col style="width:60px"/>
                                        <col style="width:20px"/>
                                        <col style="width:100px"/>
                                    </colgroup>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="tdLabel"><%#Eval("StudentFeeCompTypeName") %></td>
                                    <td>&nbsp;</td>
                                    <td>:</td>
                                    <td align="right"><input type="text" value='<%#Eval("TotalAmount") %>' class="txtCurrency txtAdmissionFeeCompAmount" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><%=GetLabel("Periode Dibayar") %></td>
                                    <td>&nbsp;</td>
                                    <td>:</td>
                                    <td align="right"><input type="text" value='<%#Eval("NoOfRegistrationPaymentPeriod") %>' readonly="readonly" class="number txtNoOfRegistrationPaymentPeriod" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><%=GetLabel("Total (Sebelum Diskon)")%></td>
                                    <td>&nbsp;</td>
                                    <td>:</td>
                                    <td align="right"><input type="text" value='<%#Eval("TotalPaymentAmount") %>' readonly="readonly" class="txtCurrency txtAdmissionFeeCompGrossTransactionAmount" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><%=GetLabel("Diskon") %></td>
                                    <td><asp:TextBox id="txtDiscountPercentage" runat="server" CssClass="number txtDiscountPercentage" Width="30px" /> [%]</td>
                                    <td>:</td>
                                    <td align="right"><asp:TextBox id="txtTotalDiscountAmount" runat="server" CssClass="txtCurrency txtTotalDiscountAmount" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><%=GetLabel("Total (Setelah Diskon)") %></td>
                                    <td>&nbsp;</td>
                                    <td>:</td>
                                    <td align="right"><asp:TextBox id="txtAdmissionFeeCompTransactionAmount" runat="server" CssClass="txtCurrency txtAdmissionFeeCompTransactionAmount" ReadOnly="true" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID") %>' gcadmissionpaymentperiod='<%#Eval("GCAdmissionPaymentPeriod") %>' />
                                        <div id="containerTableFee" runat="server">
                                            <asp:Repeater ID="rptViewDt" runat="server">
                                                <HeaderTemplate>
                                                    <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                        <colgroup>
                                                            <col />
                                                            <col style="width:200px"/>
                                                            <col style="width:150px" />
                                                            <col style="width:150px" />
                                                            <col style="width:17px" />
                                                        </colgroup>
                                                        <tr>
                                                            <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jumlah Bayar [%]") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                            <th>&nbsp;</th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="trDetail trDetail<%# DataBinder.Eval(Container.Parent.Parent.Parent, "DataItem.StudentFeeCompTypeID")%> ">
                                                        <td class="keyField"><%#:Eval("StudentFeeDtID") %></td>
                                                        <td align="center"><%#Eval("DisplayOrder") %></td>
                                                        <td align="center"><input type="text" class="txtDueDate datepicker required" validationgroup="mpEntry" value='<%#Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                        <td align="center"><input type="text" class="txtTransactionAmountInPercentage number required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("TransactionAmount") %>' /></td>
                                                        <td align="center"><input type="text" class="txtTransactionAmount txtCurrency required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("LineAmount") %>' /></td>
                                                        <td><div <%#(Container.ItemIndex + 1).ToString() != "1" ? "style='float:right;'" : "style='display:none;'" %>  class="divDeleteEntryDt divDetailDelete"></div></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                        <tr class="trFooter">
                                                            <td align="right" colspan="2"><%=GetLabel("Total") %></td>
                                                            <td>&nbsp;</td>
                                                            <td align="center"><input type="text" class="txtTotalTransactionAmount txtCurrency" readonly="readonly" style="width:90%" /></td>
                                                        </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            </table>
                                            <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                                                <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("Tambah Data")%></span>
                                            </div>
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
    </div>
</asp:Content>