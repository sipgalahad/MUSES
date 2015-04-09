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
                                if (tempResult != '') {
                                    tempResult += '^';
                                    lstStudentFeeID += ',';
                                }
                                tempResult += studentFeeID + ',' + $(this).val();
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
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeCompID" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeID" runat="server" />
    <div>
        <script id="tmplEntityDt" type="text/x-jquery-tmpl">
            <tr>
                <td class="keyField">0</td>
                <td align="center">{DisplayOrder}</td>
                <td align="center"><input type="text" validationgroup="mpEntry" id="txtDueDate" class="txtDueDate datepicker required txtDueDate{KeyField}" value='' style="width:120px" /></td>
                <td align="center"><input type="text" validationgroup="mpEntry" class="txtPaymentAmount txtCurrency required txtPaymentAmount{KeyField}" style="width:90%" value='0' /></td>
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
                                        <td><%#:Eval("StudentFeeCompTypeName") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="txtTotalAmount txtCurrency" Width="120px" /></td>
                                    </tr>  
                                    <tr id="trDataDetail" runat="server">
                                        <td colspan="3">
                                            <input type="hidden" class="hdnStudentFeeCompTypeName" value='<%#:Eval("StudentFeeCompTypeName") %>' />
                                            <input type="hidden" class="hdnStudentFeeCompID" runat="server" value='<%#:Eval("StudentFeeCompID") %>' />
                                            <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                <asp:Repeater runat="server" ID="rptStudentFee">
                                                    <HeaderTemplate>
                                                        <colgroup>
                                                            <col style="width:200px"/>
                                                            <col style="width:150px" />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <th class="thCenter"><%=GetLabel("Periode") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="trDetail">
                                                            <td class="keyField"><%#:Eval("StudentFeeID") %></td>
                                                            <td align="center"><%#:Eval("PaymentPeriod") %></td>
                                                            <td align="center"><input type="text" id="txtDueDate" <%#Eval("IsPaid").ToString() == "True" ? "readonly='readonly'" : "" %> class="txtDueDate datepicker required" value='<%#:Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                            <td align="center"><asp:CheckBox ID="chkIsPaid" runat="server" Enabled="false" Checked='<%#Eval("IsPaid") %>' /></td>
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
