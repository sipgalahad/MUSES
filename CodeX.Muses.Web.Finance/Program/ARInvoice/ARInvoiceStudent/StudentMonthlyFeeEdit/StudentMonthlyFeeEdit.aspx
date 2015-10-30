<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentMonthlyFeeEdit.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.StudentMonthlyFeeEdit" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnGenerate" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#<%=btnGenerate.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    var param = "";
                    var lstStudentFeeCompID = "";
                    var lstStudentFeeID = "";
                    $('.hdnStudentFeeCompID').each(function () {
                        var studentFeeCompID = $(this).val();
                        var totalAmount = $(this).closest('tr').prev().find('.txtTotalAmount').attr('hiddenVal');
                        var tempResult = '';
                        $(this).closest('td').find('.txtDueDate').each(function () {
                            if ($(this).attr('readonly') == null) {
                                $tr = $(this).closest('tr');
                                var studentFeeID = $tr.find('.keyField').html();
                                var amount = $tr.find('.txtAmount').attr('hiddenVal');
                                var discountPercentage = $tr.find('.txtDiscountPercentage').attr('hiddenVal');
                                var totalDiscount = $tr.find('.txtTotalDiscount').attr('hiddenVal');
                                var businessPartnerID = $tr.find('.ddlBusinessPartner').val();
                                var studentAmount = $tr.find('.txtStudentAmount').attr('hiddenVal');
                                var payerAmount = $tr.find('.txtPayerAmount').attr('hiddenVal');
                                var isGeneratePayerAmount = $tr.find('.chkIsGeneratePayerAmount input').is(':checked') ? '1' : '0';
                                if (tempResult != '') {
                                    tempResult += '^';
                                    lstStudentFeeID += ',';
                                }
                                tempResult += studentFeeID + ',' + $(this).val() + ',' + amount + ',' + discountPercentage + ',' + totalDiscount + ',' + businessPartnerID + ',' + studentAmount + ',' + payerAmount + ',' + isGeneratePayerAmount;
                                lstStudentFeeID += studentFeeID;
                            }
                        });

                        if (param != '') {
                            param += '|';
                            lstStudentFeeCompID += ',';
                        }
                        param += studentFeeCompID + ';' + totalAmount + ';' + tempResult;
                        lstStudentFeeCompID += studentFeeCompID;
                    });
                    $('#<%=hdnLstStudentFeeCompID.ClientID %>').val(lstStudentFeeCompID);
                    $('#<%=hdnLstStudentFeeID.ClientID %>').val(lstStudentFeeID);
                    $('#<%=hdnSaveValue.ClientID %>').val(param);
                    onCustomButtonClick('save');
                }
            });
        });

        $('.txtTotalAmount').live('change', function () {
            $(this).blur();
            var totalAmount = $(this).attr('hiddenVal');
            $(this).closest('tr').next().find('.txtAmount').each(function () {
                if ($(this).attr('readonly') == null) {
                    $(this).val(totalAmount).trigger('changeValue');
                    $(this).change();
                }
            });
        });

        $('.txtAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var discountPercentage = parseFloat($tr.find('.txtDiscountPercentage').attr('hiddenVal'));
            var totalAmount = parseFloat($(this).attr('hiddenVal'));
            var discountTotal = discountPercentage * totalAmount / 100;
            $tr.find('.txtTotalDiscount').val(discountTotal).trigger('changeValue');
            calculateTotalStudentAmount($tr);
        });

        $('.txtDiscountPercentage').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var totalAmount = parseFloat($tr.find('.txtAmount').attr('hiddenVal'));
            var discountPercentage = parseFloat($(this).attr('hiddenVal'));
            var discountTotal = discountPercentage * totalAmount / 100;
            $tr.find('.txtTotalDiscount').val(discountTotal).trigger('changeValue');
            calculateTotalStudentAmount($tr);
        });

        $('.txtTotalDiscount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var totalAmount = parseFloat($tr.find('.txtAmount').attr('hiddenVal'));
            var discountTotal = parseFloat($(this).attr('hiddenVal'));
            var discountPercentage = discountTotal * 100 / totalAmount;
            $tr.find('.txtDiscountPercentage').val(discountPercentage).trigger('changeValue');
            calculateTotalStudentAmount($tr);
        });

        $('.ddlBusinessPartner').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            if ($(this).val() == '0') {
                $tr.find('.txtStudentAmount').attr('readonly', 'readonly');
                $tr.find('.txtPayerAmount').attr('readonly', 'readonly');
                $tr.find('.txtPayerAmount').val('0').trigger('changeValue');
                $tr.find('.txtPayerAmount').change();
            }
            else {
                $tr.find('.txtStudentAmount').removeAttr('readonly');
                $tr.find('.txtPayerAmount').removeAttr('readonly');
            }

            if ($(this).val() == '0')
                $(this).closest('tr').find('.chkIsGeneratePayerAmount input').prop('checked', false);
            else {
                var lstCustomer = $('#<%=hdnLstCustomer.ClientID %>').val().split('|');
                for (var i = 0; i < lstCustomer.length; ++i) {
                    var temp = lstCustomer[i].split(';');
                    if (temp[0] == $(this).val()) {
                        $(this).closest('tr').find('.chkIsGeneratePayerAmount input').prop('checked', temp[1] == '1');
                        break;
                    }
                }
            }
        });

        function calculateTotalStudentAmount($tr) {
            var totalAmount = parseFloat($tr.find('.txtAmount').attr('hiddenVal'));
            var totalDiscount = parseFloat($tr.find('.txtTotalDiscount').attr('hiddenVal'));
            var lineAmount = totalAmount - totalDiscount;
            var payerAmount = parseFloat($tr.find('.txtPayerAmount').attr('hiddenVal'));
            if (lineAmount < payerAmount)
                payerAmount = lineAmount;
            if (payerAmount < 0)
                payerAmount = 0;

            var studentAmount = lineAmount - payerAmount;
            $tr.find('.txtPayerAmount').val(payerAmount).trigger('changeValue');
            $tr.find('.txtStudentAmount').val(studentAmount).trigger('changeValue');                        
        }

        $('.txtStudentAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var totalAmount = parseFloat($tr.find('.txtAmount').attr('hiddenVal'));
            var studentAmount = parseFloat($tr.find('.txtStudentAmount').attr('hiddenVal'));
            var totalDiscount = parseFloat($tr.find('.txtTotalDiscount').attr('hiddenVal'));
            var payerAmount = totalAmount - totalDiscount - studentAmount;
            $tr.find('.txtPayerAmount').val(payerAmount).trigger('changeValue');
        }); 

        $('.txtPayerAmount').live('change', function () {
            $(this).blur();
            $tr = $(this).closest('tr');
            var totalAmount = parseFloat($tr.find('.txtAmount').attr('hiddenVal'));
            var payerAmount = parseFloat($tr.find('.txtPayerAmount').attr('hiddenVal'));
            var totalDiscount = parseFloat($tr.find('.txtTotalDiscount').attr('hiddenVal'));
            var studentAmount = totalAmount - totalDiscount - payerAmount;
            $tr.find('.txtStudentAmount').val(studentAmount).trigger('changeValue');
        }); 

        //#region SchoolPeriod
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "<%=OnGetSchoolPeriodFilterExpression() %>";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodCode);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                        $('#<%=hdnSchoolPeriodID.ClientID %>').val(result.SchoolPeriodID);
                        cbpView.PerformCallback('refresh');
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                        $('#<%=hdnSchoolPeriodID.ClientID %>').val('');
                        cbpView.PerformCallback('refresh');
                    }
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
            var id = tacStudent.getValue();
            if (id != '') {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnSchoolPeriodID.ClientID %>').val(result.SchoolPeriodID)
                        cbpView.PerformCallback('refresh');
                    }
                    
                });
            }
        }
        //#endregion

        function onCbpViewEndCallback(s) {
            $('.txtDueDate').each(function () {
                if ($(this).attr('readonly') == null) {
                    $(this).attr('placeholder', 'dd-MM-yyyy');
                    setDatePickerElement($(this));
                }
            });
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnLstCustomer" runat="server" />
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeCompID" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeID" runat="server" />
    <input type="hidden" id="hdnSiteID" runat="server" />
    <div>
        <table width="100%">
            <colgroup>
                    <col style="width:150px"/>
                </colgroup>
                <tr>
                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Ajaran")%></label></td>
                    <td>
                        <input type="hidden" id="hdnSchoolPeriodID" runat="server" value="0" />
                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolPeriodList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                            SearchFields="SchoolPeriodName" TextField="SchoolPeriodName" ValueField="SchoolPeriodCode" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                            <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                                ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                        </cdx:CodeXAutoCompleteTextBox>   
                    </td>
                </tr>
        </table>
    </div>
    <div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <input type="hidden" value="" />
                        <table class="tblStudentFeeComp">
                            <colgroup>
                                <col width="250px"/>
                                <col width="3px"/>
                                <col width="900px"/>
                            </colgroup>
                            <asp:Repeater runat="server" ID="rptStudentFeeComp" OnItemDataBound="rptStudentFeeComp_ItemDataBound">
                                <ItemTemplate>                                        
                                    <tr id="trDataHeader" runat="server">
                                        <td><%#:Eval("StudentFeeCompTypeName") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="txtTotalAmount txtCurrency" Width="120px" /></td>
                                    </tr>  
                                    <tr id="trDataDetail" runat="server">
                                        <td colspan="5">
                                            <input type="hidden" class="hdnStudentFeeCompTypeName" value='<%#:Eval("StudentFeeCompTypeName") %>' />
                                            <input type="hidden" class="hdnStudentFeeCompID" runat="server" value='<%#:Eval("StudentFeeCompID") %>' />
                                            <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                <asp:Repeater runat="server" ID="rptStudentFee" OnItemDataBound="rptStudentFee_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th style="width:200px" class="thCenter" rowspan="2"><%=GetLabel("Periode") %></th>
                                                            <th style="width:150px" class="thCenter" rowspan="2"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th style="width:150px" class="thCenter" rowspan="2"><%=GetLabel("Jumlah Bayar") %></th>
                                                            <th class="thCenter" colspan="2"><%=GetLabel("Diskon") %></th>
                                                            <th style="width:150px" class="thCenter" rowspan="2"><%=GetLabel("Siswa") %></th>
                                                            <th class="thCenter" colspan="2"><%=GetLabel("Pembayar") %></th>
                                                            <th style="width:40px" class="thCenter" rowspan="2"><%=GetLabel("Bayar") %></th>
                                                            <th style="width:40px" class="thCenter" rowspan="2"><%=GetLabel("Generate Tagihan PSE") %></th>
                                                        </tr>
                                                        <tr>
                                                            <th style="width:80px" class="thCenter"><%=GetLabel("[%]") %></th>
                                                            <th style="width:120px" class="thCenter"><%=GetLabel("Total") %></th>
                                                            <th style="width:120px" class="thCenter"><%=GetLabel("Pembayar") %></th>
                                                            <th style="width:120px" class="thCenter"><%=GetLabel("Jumlah") %></th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="trDetail">
                                                            <td class="keyField"><%#:Eval("StudentFeeID") %></td>
                                                            <td align="center"><%#:Eval("PaymentPeriod") %></td>
                                                            <td align="center"><input type="text" id="txtDueDate" <%#Eval("IsPaid").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtDueDate datepicker required" value='<%#:Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" id="txtAmount" <%#Eval("IsPaid").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtAmount txtCurrency required" value='<%#:Eval("TransactionAmount") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" id="txtDiscountPercentage" <%#Eval("IsPaid").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtDiscountPercentage txtCurrency required" value='<%#:Eval("DiscountAmount") %>' style="width:80px" /></td>
                                                            <td align="center"><input type="text" id="txtTotalDiscount" <%#Eval("IsPaid").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtTotalDiscount txtCurrency required" value='<%#:Eval("TotalDiscountAmount") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" id="txtStudentAmount" <%#Eval("IsPaid").ToString() == "True" || Eval("BusinessPartnerID").ToString() == "0" ? "readonly='readonly'" : "" %> class="txtStudentAmount txtCurrency required" value='<%#:Eval("TotalStudentAmount") %>' style="width:120px" /></td>
                                                            <td align="center"><asp:DropDownList ID="ddlBusinessPartner" runat="server" CssClass="ddlBusinessPartner" Style="width:120px" /> </td>
                                                            <td align="center"><input type="text" id="txtPayerAmount" <%#Eval("IsPaid").ToString() == "True" || Eval("BusinessPartnerID").ToString() == "0" ? "readonly='readonly'" : "" %> class="txtPayerAmount txtCurrency required" value='<%#:Eval("PayerAmount") %>' style="width:120px" /></td>
                                                            <td align="center"><asp:CheckBox ID="chkIsPaid" runat="server" Enabled="false" Checked='<%#Eval("IsPaid") %>' /></td>
                                                            <td align="center"><asp:CheckBox ID="chkIsGeneratePayerAmount" runat="server" CssClass="chkIsGeneratePayerAmount" Checked='<%#Eval("IsGeneratePayerAmount") %>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
