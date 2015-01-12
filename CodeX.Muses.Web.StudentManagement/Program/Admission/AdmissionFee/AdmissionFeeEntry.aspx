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
                tacAdmissionFeeRule.setValue(result.AdmissionFeeRuleID);
                tacAdmissionFeeRule.setText(result.AdmissionFeeRuleName);
                cboPaymentType.SetValue(result.PaymentID);
                if (result.AdmissionFeeRuleID > 0) {
                    cbpView.PerformCallback('refresh');
                }
            }
            else {
                $('#<%=hdnIsFeeder.ClientID %>').val('0');
                tacAdmissionFeeRule.setValue('');
                tacAdmissionFeeRule.setText('');
                cboPaymentType.SetValue('');
            }
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

        $('.txtPaymentAmount').live('change', function () {
            $(this).blur();

            $tr = $(this).closest('tr');
            var paymentAmount = parseFloat($tr.find('.txtPaymentAmount').attr('hiddenVal'));
            var discountAmount = parseFloat($tr.find('.txtDiscountAmount').attr('hiddenVal'));
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
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    var isAllowSave = true;
                    $('.txtTotalPaymentAmount').each(function () {
                        var totalPaymentAmount = parseFloat($(this).attr('hiddenVal'));
                        var totalPaymentAmount1 = parseFloat($(this).closest('.tblView').prev('.hdnTotalAmount').val());
                        if (totalPaymentAmount != totalPaymentAmount1) {
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
                $tbl = $(this).next().next('.tblView');
                var lstTemp = '';
                $tbl.find('.trDetail').each(function () {
                    if (lstTemp != '')
                        lstTemp += ',';
                    var paymentDate = $(this).find('.txtPaymentDate').val();
                    var paymentAmount = $(this).find('.txtPaymentAmount').attr('hiddenVal');
                    var discountAmount = $(this).find('.txtDiscountAmount').attr('hiddenVal');
                    var lineAmount = $(this).find('.txtLineAmount').attr('hiddenVal');
                    lstTemp += paymentDate + '^' + paymentAmount + '^' + discountAmount + '^' + lineAmount;
                });
                if (lstSaveValue != '')
                    lstSaveValue += '|';
                lstSaveValue += admissionFeeCompID + ';' + lstTemp;
            });
            $('#<%=hdnSaveValue.ClientID %>').val(lstSaveValue);
        }
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnSchoolPeriodID" value="0" runat="server" />
    <input type="hidden" id="hdnSaveValue" value="0" runat="server" />
    <div>
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
                <td><dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="150px" /></td>
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
                                    <td align="right"><b style="color:Red"><%#Eval("TotalAmount", "{0:N}") %></b></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID") %>' />
                                        <input type="hidden" class="hdnTotalAmount" value='<%#Eval("TotalAmount") %>' />
                                        <asp:Repeater ID="rptViewDt" runat="server">
                                            <HeaderTemplate>
                                                <table rules="all" class="grdSelected tblView">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width:200px"/>
                                                        <col style="width:150px" />
                                                        <col style="width:150px" />
                                                        <col style="width:150px" />
                                                    </colgroup>
                                                    <tr>
                                                        <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                        <th class="thCenter"><%=GetLabel("Tanggal Pembayaran") %></th>
                                                        <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                        <th class="thCenter"><%=GetLabel("Total") %></th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="trDetail">
                                                    <td align="center"><%#Eval("DisplayOrder") %></td>
                                                    <td align="center"><input type="text" class="txtPaymentDate datepicker required" validationgroup="mpEntry" value='<%#Eval("PaymentDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                    <td align="center"><input type="text" class="txtPaymentAmount txtCurrency required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("TotalPaymentAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtDiscountAmount txtCurrency required" validationgroup="mpEntry" style="width:90%" value='<%#Eval("TotalDiscountAmount") %>' /></td>
                                                    <td align="center"><input type="text" class="txtLineAmount txtCurrency required" validationgroup="mpEntry" readonly="readonly" style="width:90%" value='<%#Eval("LineAmount") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                    <tr class="trFooter">
                                                        <td align="right" colspan="2"><%=GetLabel("Total") %></td>
                                                        <td align="center"><input type="text" class="txtTotalPaymentAmount txtCurrency" readonly="readonly" style="width:90%" /></td>
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