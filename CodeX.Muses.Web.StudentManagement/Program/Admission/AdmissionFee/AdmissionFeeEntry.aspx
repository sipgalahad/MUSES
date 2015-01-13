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

                if (result.GCRegistrationStatus == "<%=OnGetRegistrationStatusAccepted() %>") {
                    $('#<%=tblInfoOutstandingTransfer.ClientID %>').hide();
                    $('#<%=btnGenerateAR.ClientID %>').show();
                    $('#<%=btnSave.ClientID %>').show();
                    $('#<%=btnVoid.ClientID %>').hide();
                }
                else {
                    $('#<%=tblInfoOutstandingTransfer.ClientID %>').show();
                    $('#<%=btnGenerateAR.ClientID %>').hide();
                    $('#<%=btnSave.ClientID %>').hide();
                    $('#<%=btnVoid.ClientID %>').show();
                }
                tacAdmissionFeeRule.setValue(result.AdmissionFeeRuleID);
                tacAdmissionFeeRule.setText(result.AdmissionFeeRuleName);
                cboPaymentType.SetValue(result.PaymentID);
                $('#<%=hdnAdmissionFeeRuleID.ClientID %>').val(result.AdmissionFeeRuleID);
            }
            else {
                $('#<%=hdnIsFeeder.ClientID %>').val('0');
                $('#<%=tblInfoOutstandingTransfer.ClientID %>').hide();
                $('#<%=btnGenerateAR.ClientID %>').show();
                $('#<%=btnSave.ClientID %>').show();
                $('#<%=btnVoid.ClientID %>').hide();
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
            $('.txtTotalPaymentAmount').each(function () {
                $tbl = $(this).closest('.tblView');
                var totalPayment = 0;
                var discountPayment = 0;
                var lineAmount = 0;
                $tbl.find('tr.trDetail').each(function () {
                    totalPayment += parseFloat($(this).find('.txtPaymentAmount').attr('hiddenVal'));
                    discountPayment += parseFloat($(this).find('.txtDiscountAmount').attr('hiddenVal'));
                    lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
                });
                $tbl.find('.txtTotalPaymentAmount').val(totalPayment).trigger('changeValue');
                $tbl.find('.txtTotalDiscountAmount').val(discountPayment).trigger('changeValue');
                $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
            });
        }

        $('.txtDiscountAmount').live('change', function () {
            $(this).blur();

            $tr = $(this).closest('tr');
            var paymentAmount = parseFloat($tr.find('.txtPaymentAmount').attr('hiddenVal'));
            var discountAmount = parseFloat($tr.find('.txtDiscountAmount').attr('hiddenVal'));
            $tr.find('.txtLineAmount').val(paymentAmount - discountAmount).trigger('changeValue');

            var discountPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                discountPayment += parseFloat($(this).find('.txtDiscountAmount').attr('hiddenVal'));
                lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalDiscountAmount').val(discountPayment).trigger('changeValue');
            $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
        });

        function onCbpViewEndCallback(s) {
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            $('.txtPaymentDate').each(function () {
                setDatePickerElement($(this));
            });
            calculateTotal();
            hideLoadingPanel();
        }

        $(function () {
            $('#btnGenerate').click(function () {
                cbpView.PerformCallback('refresh|0');
            });

            $('#<%=btnGenerateAR.ClientID %>').click(function () {
                onCustomButtonClick('generateAR');
            });
            $('#<%=btnVoid.ClientID %>').click(function () {
                onCustomButtonClick('void');
            });
            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    var isAllowSave = true;
                    $('.txtTotalPaymentAmount').each(function () {
                        var totalPaymentAmount = parseFloat($(this).attr('hiddenVal'));
                        var totalPaymentAmount1 = parseFloat($(this).closest('.tblView').parent().closest('tr').prev().find('.txtAdmissionFeeCompAmount').attr('hiddenVal'));
                        if (totalPaymentAmount != totalPaymentAmount1) {
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
                        onCustomButtonClick('save');
                    }
                }
            });
        });

        function getSaveValue() {
            var lstSaveValue = '';
            $('.hdnAdmissionFeeCompID').each(function () {
                var admissionFeeCompID = $(this).val();
                var admissionFeeCompValue = $(this).closest('tr').prev().find('.txtAdmissionFeeCompAmount').attr('hiddenVal');
                $tbl = $(this).next('.tblView');
                var lstTemp = '';
                $tbl.find('.trDetail').each(function () {
                    if (lstTemp != '')
                        lstTemp += ',';
                    var paymentDate = $(this).find('.txtPaymentDate').val();
                    var paymentAmountInPercentage = $(this).find('.txtPaymentAmountInPercentage').val();
                    var paymentAmount = $(this).find('.txtPaymentAmount').attr('hiddenVal');
                    var discountAmountInPercentage = $(this).find('.txtDiscountAmountInPercentage').val();
                    var discountAmount = $(this).find('.txtDiscountAmount').attr('hiddenVal');
                    var lineAmount = $(this).find('.txtLineAmount').attr('hiddenVal');
                    lstTemp += paymentDate + '^' + paymentAmountInPercentage + '^' + paymentAmount + '^' + discountAmountInPercentage + '^' + discountAmount + '^' + lineAmount;
                });
                if (lstSaveValue != '')
                    lstSaveValue += '|';
                lstSaveValue += admissionFeeCompID + ';' + admissionFeeCompValue + ';' + lstTemp;
            });
            $('#<%=hdnSaveValue.ClientID %>').val(lstSaveValue);
        }

        $('.txtAdmissionFeeCompAmount').live('change', function () {
            $(this).blur();
            var admissionFeeCompAmount = parseFloat($(this).attr('hiddenVal'));
            $tbl = $(this).closest('tr').next().find('.tblView');
            $tbl.find('.trDetail').each(function () {
                var paymentAmountInPercentage = parseFloat($(this).find('.txtPaymentAmountInPercentage').val());
                var paymentAmount = admissionFeeCompAmount * paymentAmountInPercentage / 100;
                var discountAmountInPercentage = parseFloat($(this).find('.txtDiscountAmountInPercentage').val());
                var discountAmount = paymentAmount * discountAmountInPercentage / 100;
                $(this).find('.txtPaymentAmount').val(paymentAmount).trigger('changeValue');
                $(this).find('.txtDiscountAmount').val(discountAmount).trigger('changeValue');
            });
        });

        $('.txtPaymentAmountInPercentage').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var admissionFeeCompAmount = parseFloat($tr.parent().closest('tr').prev().find('.txtAdmissionFeeCompAmount').attr('hiddenVal'));

            var paymentAmountInPercentage = parseFloat($(this).val());
            var paymentAmount = admissionFeeCompAmount * paymentAmountInPercentage / 100;
            var discountAmountInPercentage = parseFloat($tr.find('.txtDiscountAmountInPercentage').val());
            var discountAmount = paymentAmount * discountAmountInPercentage / 100;
            $tr.find('.txtPaymentAmount').val(paymentAmount).trigger('changeValue');
            $tr.find('.txtDiscountAmount').val(discountAmount).trigger('changeValue');
            $tr.find('.txtLineAmount').val(paymentAmount - discountAmount).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtPaymentAmount').attr('hiddenVal'));
                lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalPaymentAmount').val(totalPayment).trigger('changeValue');
            $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
        });
        $('.txtPaymentAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var admissionFeeCompAmount = parseFloat($tr.parent().closest('tr').prev().find('.txtAdmissionFeeCompAmount').attr('hiddenVal'));

            var paymentAmount = parseFloat($(this).attr('hiddenVal'));
            var paymentAmountInPercentage = paymentAmount * 100 / admissionFeeCompAmount;
            var discountAmountInPercentage = parseFloat($tr.find('.txtDiscountAmountInPercentage').val());
            var discountAmount = paymentAmount * discountAmountInPercentage / 100;
            $tr.find('.txtPaymentAmountInPercentage').val(paymentAmountInPercentage).trigger('changeValue');
            $tr.find('.txtDiscountAmount').val(discountAmount).trigger('changeValue');
            $tr.find('.txtLineAmount').val(paymentAmount - discountAmount).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtPaymentAmount').attr('hiddenVal'));
                lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalPaymentAmount').val(totalPayment).trigger('changeValue');
            $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
        });

        $('.txtDiscountAmountInPercentage').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var paymentAmount = parseFloat($tr.find('.txtPaymentAmount').attr('hiddenVal'));
            var discountAmountInPercentage = parseFloat($tr.find('.txtDiscountAmountInPercentage').val());
            var discountAmount = paymentAmount * discountAmountInPercentage / 100;
            $tr.find('.txtDiscountAmount').val(discountAmount).trigger('changeValue');
            $tr.find('.txtLineAmount').val(paymentAmount - discountAmount).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtPaymentAmount').attr('hiddenVal'));
                lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalPaymentAmount').val(totalPayment).trigger('changeValue');
            $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
        });

        $('.txtDiscountAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var paymentAmount = parseFloat($tr.find('.txtPaymentAmount').attr('hiddenVal'));
            var discountAmount = parseFloat($tr.find('.txtDiscountAmount').attr('hiddenVal'));
            var discountAmountInPercentage = discountAmount * 100 / paymentAmount;
            $tr.find('.txtDiscountAmountInPercentage').val(discountAmountInPercentage).trigger('changeValue');
            $tr.find('.txtLineAmount').val(paymentAmount - discountAmount).trigger('changeValue');

            var totalPayment = 0;
            var lineAmount = 0;
            $tbl = $(this).closest('.tblView');
            $tbl.find('tr.trDetail').each(function () {
                totalPayment += parseFloat($(this).find('.txtPaymentAmount').attr('hiddenVal'));
                lineAmount += parseFloat($(this).find('.txtLineAmount').attr('hiddenVal'));
            });
            $tbl.find('.txtTotalPaymentAmount').val(totalPayment).trigger('changeValue');
            $tbl.find('.txtTotalAmount').val(lineAmount).trigger('changeValue');
        });

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
            else
                hideLoadingPanel();
        }
        //#endregion
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnAdmissionFeeRuleID" value="0" runat="server" />
    <input type="hidden" id="hdnSchoolPeriodID" value="0" runat="server" />
    <input type="hidden" id="hdnSaveValue" value="0" runat="server" />
    <input type="hidden" id="hdnLstScholarshipID" value="0" runat="server" />
    <div>
        <div style="float:right">            
            <table id="tblInfoOutstandingTransfer" runat="server" style="display:none;">
                <tr>
                    <td><img height="24" src='<%= ResolveUrl("~/Libs/Images/Button/warning.png")%>' alt='' /></td>
                    <td><label class="lblInfo" id="lblInfoOutstandingBill"><%=GetLabel("Sudah Dibuat Tagihan Untuk Calon Siswa Yang Bersangkutan. Tidak Bisa Diubah") %></label></td>
                </tr>
            </table>
        </div>
        <table>
            <colgroup>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Calon Siswa")%></label></td>
                <td>
                    <input type="hidden" id="hdnIsFeeder" runat="server" />
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRegistration" ClientInstanceName="tacRegistration" MethodName="GetvRegistrationList" GetFilterExpressionFunction="onGetRegistrationFilterExpression"
                        SearchFields="ProspectiveStudentName,RegistrationNo" TextField="ProspectiveStudentName" ValueField="RegistrationID" SearchText="${ProspectiveStudentName} (<b>${RegistrationNo}</b>)" OrderByExpression="ProspectiveStudentName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacRegistrationButtonSearchClick(); }"
                            ValueChanged="function(){ onTacRegistrationValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Biaya Siswa")%></label></td>
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
                <td class="tdLabel"><%=GetLabel("Cara Pembayaran") %></td>
                <td><dxe:ASPxComboBox ID="cboPaymentType" ClientInstanceName="cboPaymentType" runat="server" Width="150px" /></td>
            </tr>
            <tr>
                <td class="tdLabel">&nbsp;</td>
                <td><input type="button" id="btnGenerate" value='<%=GetLabel("Generate") %>' /></td>
            </tr>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <asp:Repeater ID="rptAdmissionComp" runat="server" OnItemDataBound="rptAdmissionComp_ItemDataBound">
                            <HeaderTemplate>
                                <table>
                                    <colgroup>
                                        <col style="width:160px"/>
                                        <col style="width:20px"/>
                                        <col style="width:100px"/>
                                    </colgroup>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("AdmissionFeeCompType") %></td>
                                    <td>:</td>
                                    <td align="right"><input type="text" value='<%#Eval("TotalAmount") %>' class="txtCurrency txtAdmissionFeeCompAmount" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID") %>' />
                                        <asp:Repeater ID="rptViewDt" runat="server">
                                            <HeaderTemplate>
                                                <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width:200px"/>
                                                        <col style="width:150px" />
                                                        <col style="width:150px" />
                                                        <col style="width:150px" />
                                                    </colgroup>
                                                    <tr>
                                                        <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                        <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                        <th class="thCenter"><%=GetLabel("Jumlah Bayar [%]") %></th>
                                                        <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon [%]") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                        <th class="thCenter"><%=GetLabel("Total") %></th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="trDetail">
                                                    <td align="center"><%#Eval("DisplayOrder") %></td>
                                                    <td align="center"><input type="text" class="txtPaymentDate datepicker required" validationgroup="mpEntry" value='<%#Eval("PaymentDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                    <td align="center"><input type="text" class="txtPaymentAmountInPercentage number required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("PaymentAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtPaymentAmount txtCurrency required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("TotalPaymentAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtDiscountAmountInPercentage number required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("DiscountAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtDiscountAmount txtCurrency required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("TotalDiscountAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtLineAmount txtCurrency required" validationgroup="mpEntry" readonly="readonly" style="width:90%" value='<%#Eval("LineAmount") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                    <tr class="trFooter">
                                                        <td align="right" colspan="2"><%=GetLabel("Total") %></td>
                                                        <td>&nbsp;</td>
                                                        <td align="center"><input type="text" class="txtTotalPaymentAmount txtCurrency" readonly="readonly" style="width:90%" /></td>
                                                        <td>&nbsp;</td>
                                                        <td align="center"><input type="text" class="txtTotalDiscountAmount txtCurrency" readonly="readonly" style="width:90%" /></td>
                                                        <td align="center"><input type="text" class="txtTotalAmount txtCurrency" readonly="readonly" style="width:90%" /></td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
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