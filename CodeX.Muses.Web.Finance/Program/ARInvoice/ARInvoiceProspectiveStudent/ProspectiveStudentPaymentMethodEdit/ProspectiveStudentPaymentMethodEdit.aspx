<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPProspectiveStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentPaymentMethodEdit.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.ProspectiveStudentPaymentMethodEdit" %>

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
                    var isAllowSave = true;
                    $('.txtRemainingAmount').each(function () {
                        if (isAllowSave) {
                            var total = parseFloat($(this).attr('hiddenVal'));
                            $tr = $(this).closest('tr').next().next().next();

                            var totalPayerAmount = parseFloat($tr.prev().find('.txtPayerAmount').attr('hiddenVal'));
                            var studentFeeCompType = $tr.find('.hdnStudentFeeCompTypeName').val();
                            var totalPayment = parseFloat($tr.find('.txtTotalPayment').attr('hiddenVal'));
                            if ((total - totalPayerAmount) != totalPayment) {
                                isAllowSave = false;
                                showToast('Warning', 'Total ' + studentFeeCompType + ' Tidak Sama');
                            }
                        }
                    });
                    if (isAllowSave) {
                        var param = "";
                        var lstStudentFeeID = "";
                        $('.hdnStudentFeeID').each(function () {
                            var studentFeeID = $(this).val();
                            var totalAmount = $(this).closest('tr').prev().prev().prev().prev().prev().find('.txtTotalAmount').attr('hiddenVal');
                            var payerAmount = $(this).closest('tr').prev().find('.txtPayerAmount').attr('hiddenVal');
                            var customerID = $(this).closest('tr').prev().prev().find('.ddlCustomer').val();
                            var tempResult = '';
                            var count = 0;
                            $('.txtDueDate' + studentFeeID).each(function () {
                                $tr = $(this).closest('tr');
                                var studentFeeDtID = $tr.find('.keyField').html();
                                var paymentAmount = parseFloat($tr.find('.txtPaymentAmount').attr('hiddenVal'));
                                if (tempResult != '')
                                    tempResult += '^';
                                tempResult += studentFeeDtID + ',' + $(this).val() + ',' + paymentAmount;
                                count++;
                            });

                            if (param != '') {
                                param += '|';
                                lstStudentFeeID += ',';
                            }
                            param += studentFeeID + ';' + totalAmount + ';' + customerID + ';' + payerAmount + ';' + tempResult;
                            lstStudentFeeID += studentFeeID;
                        });
                        $('#<%=hdnLstStudentFeeID.ClientID %>').val(lstStudentFeeID);
                        $('#<%=hdnSaveValue.ClientID %>').val(param);
                        onCustomButtonClick('save');
                    }
                }
            });

            onCbpViewEndCallback();
        });

        //#region Student
        function onGetStudentFilterExpression() {
            var filterExpression = "<%=OnGetStudentFilterExpression() %>";
            return filterExpression;
        }

        function onTacStudentButtonSearchClick() {
            openSearchDialog('student', onGetStudentFilterExpression(), function (value) {
                var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
                Methods.getObject('GetStudentList', filterExpression, function (result) {
                    if (result != null) {
                        tacStudent.setValue(result.StudentID);
                        tacStudent.setText(result.StudentName);
                        cbpView.PerformCallback('refresh');
                    }
                    else {
                        tacStudent.setValue('');
                        tacStudent.setText('');
                        cbpView.PerformCallback('refresh');
                    }
                });
            });

        }

        function onTacStudentValueChanged() {
            var id = tacStudent.getValue();
            if (id != '') {
                var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
                Methods.getObject('GetStudentList', filterExpression, function (result) {
                    cbpView.PerformCallback('refresh');
                });
            }
        }
        //#endregion

        $('#lblEntryPopupAddData').live('click', function () {
            $tr = $(this).closest('tr').find('.tblView tr').next('tr');
            var className = $tr.attr('class');
            addEntryDt(className);
        });

        function addEntryDt(className) {
            var rowCount = parseInt($tr.closest('.tblView').find('.' + className).last().find('td:eq(1)').html()) + 1;
            $newTr = $($('#tmplEntityDt').html());
            $newTr.addClass(className);
            $newTr.insertAfter($('.tblView').find('.' + className).last());

            var keyField = className.replace("trDetail","");
            var text = $newTr.html();
            text = text.replace('{DisplayOrder}', rowCount);
            text = text.replace('{KeyField}', keyField);
            text = text.replace('{KeyField}', keyField);
            $newTr.html(text);

            $('.txtDueDate').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                setDatePickerElement($(this));
            });

            calculatePaymentAmount(keyField);
            calculateTotalPayment();
        }

        function calculatePaymentAmount(keyField) {
            var count = $('.txtPaymentAmount' + keyField).length;
            var totalAmount = parseFloat($('.txtRemainingAmount' + keyField).attr('hiddenVal'));
            $('.txtPaymentAmount' + keyField).each(function () {
                $(this).val(totalAmount / count).trigger('changeValue');
            });
        }

        function calculateTotalPayment() {
            $('.txtTotalPayment').each(function () {
                $row = $(this).closest('tr').parent().parent().parent();
                var keyField = $row.find('.hdnStudentFeeID').val();
                var totalAmount = 0;
                $('.txtPaymentAmount' + keyField).each(function () {
                    totalAmount += parseFloat($(this).attr('hiddenVal'));
                });

                $(this).val(totalAmount).trigger('changeValue');
            });
        }

        $('.txtPaymentAmount').live('change', function () {
            $(this).trigger('changeValue');
            calculateTotalPayment();
        });

        $('.divDetailDelete').live('click', function () {
            $row1 = $(this).closest('tr').parent().parent().parent();
            $row = $(this).closest('tr');
            $row.remove();

            var keyField = $row1.find('.hdnStudentFeeID').val();
            calculatePaymentAmount(keyField);
            calculateTotalPayment();
        });

        function onCbpViewEndCallback() {
            $('.txtDueDate').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                setDatePickerElement($(this));
            });
            $('.txtPaymentAmount').each(function () {
                $(this).trigger('changeValue');
            });

            calculateTotalPayment();
            hideLoadingPanel();
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    cbpView.PerformCallback('refresh');
                }
            }
        }

        $('.txtTotalAmount').live('change', function () {
            $(this).blur();
            var totalAmount = parseFloat($(this).attr('hiddenVal'));
            var totalPaymentAmount = parseFloat($(this).closest('tr').next().find('.txtTotalPaymentAmount').attr('hiddenVal'));
            $(this).closest('tr').next().next().find('.txtRemainingAmount').val(totalAmount - totalPaymentAmount).trigger('changeValue');

            var studentFeeID = $(this).closest('tr').next().next().next().find('.hdnStudentFeeID').val();

            calculatePaymentAmount(studentFeeID);
            calculateTotalPayment();
        });
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeID" runat="server" />
    <div>
        <script id="tmplEntityDt" type="text/x-jquery-tmpl">
            <tr>
                <td class="keyField">0</td>
                <td align="center">{DisplayOrder}</td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtDueDate datepicker required txtDueDate{KeyField}" value='' style="width:120px" /></td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtPaymentAmount txtCurrency required txtPaymentAmount{KeyField}" style="width:90%" value='0' /></td>
                <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
            </tr>
        </script>
    </div>
    <div>
        <input type="hidden" id="hdnParam" runat="server" />
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
                                <col width="300px"/>
                            </colgroup>
                            <asp:Repeater runat="server" ID="rptStudentFeeComp" OnItemDataBound="rptStudentFeeComp_ItemDataBound">
                                <ItemTemplate>                                     
                                    <tr id="trDataHeader" runat="server">
                                        <td><%#:Eval("cfStudentFeeCompTypeName") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="txtTotalAmount txtCurrency" Width="120px" /></td>
                                    </tr>                                     
                                    <tr id="trDataHeader1" runat="server">
                                        <td><%=GetLabel("Sudah Dibayar") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalPaymentAmount" runat="server" ReadOnly="true" CssClass="txtTotalPaymentAmount txtCurrency" Width="120px" /></td>
                                    </tr>                                   
                                    <tr id="trDataHeader2" runat="server">
                                        <td><%=GetLabel("Sisa") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtRemainingAmount" runat="server" ReadOnly="true" Width="120px" /></td>
                                    </tr>                                
                                    <tr id="trDataHeader3" runat="server">
                                        <td><%=GetLabel("Pemberi Beasiswa") %></td>
                                        <td>:</td>
                                        <td><asp:DropDownList ID="ddlCustomer" CssClass="ddlCustomer" runat="server" Width="150px" /></td>
                                    </tr>
                                    <tr id="trDataHeader4" runat="server">
                                        <td><%=GetLabel("Nominal") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtPayerAmount" runat="server" CssClass="txtPayerAmount txtCurrency" Width="120px" /></td>
                                    </tr>
                                    <tr id="trDataDetail" runat="server">
                                        <td colspan="3">
                                            <input type="hidden" class="hdnStudentFeeCompTypeName" value='<%#:Eval("cfStudentFeeCompTypeName") %>' />
                                            <input id="Hidden1" type="hidden" class="hdnStudentFeeID" runat="server" value='<%#:Eval("StudentFeeID") %>' />
                                            <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                <asp:Repeater runat="server" ID="rptStudentFee">
                                                    <HeaderTemplate>
                                                        <colgroup>
                                                            <col style="width:3px"/>
                                                            <col style="width:200px"/>
                                                            <col style="width:150px" />
                                                            <col style="width:17px" />
                                                        </colgroup>
                                                        <tr>
                                                            <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                            <th>&nbsp;</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="trDetail<%#:Eval("StudentFeeID") %>">
                                                            <td class="keyField"><%#:Eval("StudentFeeDtID") %></td>
                                                            <td align="center"><%#:Eval("DisplayOrder") %></td>
                                                            <td align="center"><input type="text" id="txtDueDate" <%#Eval("IsClosed").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtDueDate datepicker required txtDueDate<%#:Eval("IsClosed").ToString() == "True" ?  "" : Eval("StudentFeeID") %>" value='<%#:Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" <%#Eval("IsClosed").ToString() == "True" ? "readonly='readonly'" : "" %>  class='txtPaymentAmount txtCurrency required txtPaymentAmount<%#:Eval("IsClosed").ToString() == "True" ?  "" : Eval("StudentFeeID").ToString() %>' style="width:90%" value='<%#:Eval("StudentAmount") %>' /></td>
                                                            <td><div <%#(Container.ItemIndex + 1).ToString() != "1" ? "style='float:right;'" : "style='display:none;'" %>  class="divDeleteEntryDt divDetailDelete"></div></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td colspan="2">Total</td>
                                                            <td align="center"><input type="text" class="txtTotalPayment txtCurrency" readonly="readonly" runat="server" style="width:90%" /></td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                                                <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("Tambah Data")%></span>
                                            </div>
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
