<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="StudentFeeInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentFeeInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
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

        //#region School Period
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
            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            hideLoadingPanel();
        }
    </script>
    <table style="width: 100%">
    </table>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstStudentFeeID" runat="server" />
    <div>
        <table width="100%">
            <colgroup>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Siswa")%></label></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacStudent" ClientInstanceName="tacStudent" MethodName="GetStudentList" GetFilterExpressionFunction="onGetStudentFilterExpression"
                        SearchFields="StudentName,StudentCode" TextField="StudentName" ValueField="StudentID" SearchText="${StudentName} (<b>${StudentCode}</b>)" OrderByExpression="StudentName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacStudentButtonSearchClick(); }"
                            ValueChanged="function(){ onTacStudentValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
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
                                    <tr>
                                        <td colspan="3"><hr /></td>
                                    </tr>                        
                                    <tr id="trDataHeader" runat="server">
                                        <td style="color: Red; font-weight: bold;"><%#:Eval("cfStudentFeeCompTypeName") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalAmount" runat="server" ReadOnly="true" CssClass="txtTotalAmount txtCurrency" Width="120px" /></td>
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
                                    <tr id="trDataDetail" runat="server">
                                        <td colspan="3">
                                            <input type="hidden" class="hdnStudentFeeCompTypeName" value='<%#:Eval("cfStudentFeeCompTypeName") %>' />
                                            <input type="hidden" class="hdnStudentFeeID" runat="server" value='<%#:Eval("StudentFeeID") %>' />
                                            <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                <asp:Repeater runat="server" ID="rptStudentFee">
                                                    <HeaderTemplate>
                                                        <colgroup>
                                                            <col style="width:3px"/>
                                                            <col style="width:200px"/>
                                                            <col style="width:150px" />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <th class="thCenter"><%=GetLabel("Pembayaran Ke") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jumlah Bayar") %></th>
                                                            <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="trDetail<%#:Eval("StudentFeeID") %>">
                                                            <td class="keyField"><%#:Eval("StudentFeeDtID") %></td>
                                                            <td align="center"><%#:Eval("DisplayOrder") %></td>
                                                            <td align="center"><input type="text" id="txtDueDate" readonly='readonly' class="txtDueDate datepicker required txtDueDate<%#:Eval("IsClosed").ToString() == "True" ?  "" : Eval("StudentFeeID") %>" value='<%#:Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" readonly='readonly'  class='txtPaymentAmount txtCurrency required txtPaymentAmount<%#:Eval("IsClosed").ToString() == "True" ?  "" : Eval("StudentFeeID").ToString() %>' style="width:90%" value='<%#:Eval("StudentAmount") %>' /></td>
                                                            <td align="center"><asp:CheckBox ID="chkIsPaid" runat="server" Enabled="false" Checked='<%#Eval("IsPaid") %>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater runat="server" ID="rptStudentFeeComp2" OnItemDataBound="rptStudentFeeComp2_ItemDataBound">
                                <ItemTemplate>                
                                    <tr>
                                        <td colspan="3"><hr /></td>
                                    </tr>                                         
                                    <tr id="trDataHeader" runat="server">
                                        <td style="color: Red; font-weight: bold;"><%#:Eval("StudentFeeCompTypeName") %></td>
                                        <td>:</td>
                                        <td><asp:TextBox ID="txtTotalAmount" ReadOnly="true" runat="server" CssClass="txtTotalAmount txtCurrency" Width="120px" /></td>
                                    </tr>  
                                    <tr id="trDataDetail" runat="server">
                                        <td colspan="3">
                                            <input type="hidden" class="hdnStudentFeeCompTypeName" value='<%#:Eval("StudentFeeCompTypeName") %>' />
                                            <input id="Hidden1" type="hidden" class="hdnStudentFeeCompID" runat="server" value='<%#:Eval("StudentFeeCompID") %>' />
                                            <table rules="all" class="grdNormal grdBorder notAllowSelect tblView">
                                                <asp:Repeater runat="server" ID="rptStudentFee">
                                                    <HeaderTemplate>
                                                        <colgroup>
                                                            <col style="width:200px"/>
                                                            <col style="width:150px" />
                                                            <col style="width:150px" />
                                                            <col style="width:80px" />
                                                        </colgroup>
                                                        <tr>
                                                            <th class="thCenter"><%=GetLabel("Periode") %></th>
                                                            <th class="thCenter"><%=GetLabel("Jatuh Tempo") %></th>
                                                            <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                            <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="trDetail">
                                                            <td class="keyField"><%#:Eval("StudentFeeID") %></td>
                                                            <td align="center"><%#:Eval("PaymentPeriod") %></td>
                                                            <td align="center"><input type="text" id="txtDueDate" readonly='readonly' class="txtDueDate datepicker required" value='<%#:Eval("DueDate","{0:dd-MM-yyyy}") %>' style="width:120px" /></td>
                                                            <td align="center"><input type="text" id="txtDiscountAmount" readonly='readonly' class="txtDiscountAmount txtCurrency required" value='<%#:Eval("TotalDiscountAmount","{0:N}") %>' style="width:120px" /></td>
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
