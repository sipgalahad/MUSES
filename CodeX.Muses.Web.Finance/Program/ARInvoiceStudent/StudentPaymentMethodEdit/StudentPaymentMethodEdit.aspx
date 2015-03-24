<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentPaymentMethodEdit.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.StudentPaymentMethodEdit" %>

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
                var param = "";
                $('.hdnKeyField').each(function () {
                    var keyField = $(this).val();
                    var count = 0;
                    $('.tpd' + keyField).each(function () {
                        var paymentAmount = parseFloat($('.pa' + keyField).eq(count).attr('hiddenVal'));
                        param += keyField + ";" + $(this).val() + ";" + paymentAmount + "|";
                        count++;
                    })
                });
                $('#<%=hdnParam.ClientID %>').val(param);
                cbpProcess.PerformCallback('save');
            });
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

        $('#lblEntryPopupAddData').live('click', function () {
            $tr = $(this).closest('tr').find('.tblView tr').next('tr');
            var className = $tr.attr('class');
            addEntryDt(className);
        });

        function addEntryDt(className) {

            var rowCount = $('.' + className).length + 1;
            $newTr = $($('#tmplEntityDt').html());
            $newTr.addClass(className);
            $newTr.insertAfter($('.tblView').find('.' + className).last());

            var keyField = className.replace("trDetail","");
            var text = $newTr.html();
            text = text.replace('{DisplayOrder}', rowCount);
            text = text.replace('{KeyField}', keyField);
            text = text.replace('{KeyField}', keyField);
            $newTr.html(text);
            
            var count = 1;
            $('.txtPaymentDate').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                $(this).attr('id', 'txtPaymentDate' + count);
                setDatePickerElement($(this));
                count++;
            });

            calculatePaymentAmount(keyField);
            calculateTotalPayment();
        }

        function calculatePaymentAmount(keyField) {
            var count = $('.pa' + keyField).length;
            var totalAmount = parseFloat($('.hdnTotalAmount' + keyField).val());
            $('.pa' + keyField).each(function () {
                $(this).val(totalAmount / count).trigger('changeValue');
            });
        }

        function calculateTotalPayment() {
            $('.txtTotalPayment').each(function () {
                $row = $(this).closest('tr').parent().parent().parent();
                var keyField = $row.find('.hdnKeyField').val();

                var totalAmount = 0;
                $('.pa' + keyField).each(function () {
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

            var keyField = $row1.find('.hdnKeyField').val();
            calculatePaymentAmount(keyField);
            calculateTotalPayment();
        });

        function onCbpViewEndCallback(s) {
            var count = 1;
            $('.txtPaymentDate').each(function () {
                $(this).attr('placeholder', 'dd-MM-yyyy');
                $(this).attr('id', 'txtPaymentDate' + count);
                setDatePickerElement($(this));
                count++;
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
    </script>
    <div>
        <script id="tmplEntityDt" type="text/x-jquery-tmpl">
            <tr>
                <td align="center">{DisplayOrder}</td>
                <td align="center"><input type="text" id="txtPaymentDate" class="txtPaymentDate datepicker required tpd{KeyField}" value='' style="width:120px" /></td>
                <td align="center"><input type="text" class="txtPaymentAmount txtCurrency required pa{KeyField}" style="width:90%" value='0' /></td>
                <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
            </tr>
        </script>
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
                                <col width="150px"/>
                                <col width="3px"/>
                                <col width="300px"/>
                            </colgroup>
                            <asp:Repeater runat="server" ID="rptStudentFeeComp" OnItemDataBound="rptStudentFeeComp_ItemDataBound">
                                <ItemTemplate>                                        
                                        <tr id="trDataHeader" runat="server">
                                            <td>Sisa <%#:Eval("StudentFeeCompTypeName") %></td>
                                            <td>:</td>
                                            <td id="tdTotalAmount" runat="server"></td>
                                        </tr>
                                        <tr id="trDataDetail" runat="server">
                                            <td colspan="3">
                                                <input type="hidden" class="hdnKeyField" runat="server" value='<%#:Eval("StudentFeeCompID") %>' />
                                                <input type="hidden" id="hdnTotalAmount" runat="server" />
                                                <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                    <asp:Repeater runat="server" ID="rptStudentFee">
                                                        <HeaderTemplate>
                                                            <colgroup>
                                                                <col style="width:3px"/>
                                                                <col style="width:200px"/>
                                                                <col style="width:150px" />
                                                                <col style="width:3px" />
                                                            </colgroup>
                                                            <tr>
                                                                <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                                <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                                <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="trDetail<%#:Eval("StudentFeeCompID") %>">
                                                                <td align="center"><%#:Eval("DisplayOrder") %></td>
                                                                <td align="center"><input type="text" id="txtPaymentDate" <%#Eval("GCTransactionStatus").ToString() == "X121^004" ? "readonly='readonly'" : "" %> class="txtPaymentDate datepicker required tpd<%#:Eval("GCTransactionStatus").ToString() == "X121^004" ?  "" : Eval("StudentFeeCompID") %>" value='<%#:Eval("PaymentDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                                <td align="center"><input type="text" <%#Eval("GCTransactionStatus").ToString() == "X121^004" ? "readonly='readonly'" : "" %>  class='txtPaymentAmount txtCurrency required pa<%#:Eval("GCTransactionStatus").ToString() == "X121^004" ?  "" : Eval("StudentFeeCompID").ToString() %>' style="width:90%" value='<%#:Eval("TotalPaymentAmount") %>' /></td>
                                                                <td><div <%#(Container.ItemIndex + 1).ToString() != "1" ? "style='float:right;'" : "style='display:none;'" %>  class="divDeleteEntryDt divDetailDelete"></div></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr>
                                                                <td colspan="2">Total</td>
                                                                <td align="center"><input type="text" class="txtTotalPayment txtCurrency " readonly="readonly" runat="server" style="width:90%" /></td>
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
    <div>
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }"/>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
