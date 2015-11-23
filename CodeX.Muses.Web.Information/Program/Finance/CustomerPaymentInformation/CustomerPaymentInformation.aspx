<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="CustomerPaymentInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.CustomerPaymentInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnRefresh').click(function () {
                onRefreshGridView();
            });
        })

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }
        //#endregion

        function onRefreshGridView() {
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
                setTimeout(function () {
                    s.SetFocus();
                }, 0);
            }, 0);
        }

        //#region AR Receiving
        function onGetARReceivingFilterExpression() {
            var filterExpression = "BusinessPartnerID = '" + cboCustomer.GetValue() + "' AND GCTransactionStatus != 'X121^999'";
            return filterExpression;
        }

        function onTacARReceivingButtonSearchClick() {
            openSearchDialog('arreceivinghd', onGetARReceivingFilterExpression(), function (value) {
                var filterExpression = onGetARReceivingFilterExpression() + " AND ARReceivingNo = '" + value + "'";
                Methods.getObject('GetvARReceivingHdList', filterExpression, function (result) {
                    if (result != null) {
                        tacARReceiving.setValue(result.ARReceivingID);
                        tacARReceiving.setText(result.ARReceivingNo);
                        $('#<%=hdnReceivingDate.ClientID %>').val(result.ReceivingDateInDatePickerFormat);
                    }
                    else {
                        tacARReceiving.setValue('');
                        tacARReceiving.setText('');
                    }
                    onTacARReceivingValueChanged();
                });
            });

        }

        function onTacARReceivingValueChanged() {
        }
        //#endregion

        function onCboCustomerValueChanged() {
            $('#<%=hdnCustomerID.ClientID %>').val(cboCustomer.GetValue());
            $('#<%=hdnCustomerName.ClientID %>').val(cboCustomer.GetText());
        }

    </script>
    <input type="hidden" value="" id="hdnCustomerID" runat="server" />
    <input type="hidden" value="" id="hdnCustomerName" runat="server" />
    <input type="hidden" value="" id="hdnReceivingDate" runat="server" />
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <table style="width:50%">
                        <colgroup>
                            <col style="width:200px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Pemberi Bantuan Dana") %></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboCustomer" ClientInstanceName="cboCustomer" Width="200px">
                                    <ClientSideEvents Init="function(s,e){ onCboCustomerValueChanged(); }"  ValueChanged="function(s,e){ onCboCustomerValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tanggal Pembayaran") %></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacARReceiving" ClientInstanceName="tacARReceiving" MethodName="GetvARReceivingHdList" GetFilterExpressionFunction="onGetARReceivingHdFilterExpression"
                                    SearchFields="ARReceivingNo" TextField="ARReceivingNo" ValueField="ARReceivingID" SearchText="${ARReceivingNo}" OrderByExpression="ARReceivingNo">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacARReceivingButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacARReceivingValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <div style="position: relative;">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <input type="hidden" value="" id="hdnMovementDate" runat="server" />
                                    <asp:Panel runat="server" ID="pnlGridView" CssClass="pnlContainerGrid" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:auto;">
                                        <table cellpadding="0" cellspacing="0" border="1" rules="all" class="grdSelected grdBorder">
                                            <tr>
                                                <th style="width:120px" rowspan="2" class="thCenter"><%=GetLabel("NBS") %></th>
                                                <th rowspan="2" class="thCenter"><%=GetLabel("Nama") %></th>
                                                <th style="width:150px" rowspan="2" class="thCenter"><%=GetLabel("Kelas") %></th>
                                                <th class="thCenter" runat="server" id="thReceivingMonth"><%=GetLabel("Bulan") %></th>
                                                <th style="width:120px" rowspan="2" class="thCenter"><%=GetLabel("Total") %></th>
                                            </tr>
                                            <tr>
                                                <asp:Repeater ID="rptReceivingMonth" runat="server">
                                                    <ItemTemplate>
                                                        <th class="thCenter" style="width:120px"><%#Eval("cfStudentFeeCompTypeName")%></th>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("PayedStudentCode") %></td>
                                                    <td><%#Eval("PayedStudentName")%></td>
                                                    <td><%#Eval("PayedSchoolClassCode")%></td>
                                                    <asp:Repeater ID="rptReceivingMonth" runat="server" OnItemDataBound="rptReceivingMonth_ItemDataBound">
                                                        <ItemTemplate>
                                                            <td align="right"><div id="divPaymentAmount" runat="server"></div></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td align="right"><div id="divTotalPayment" runat="server"></div></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            <tr>
                                                <td colspan="3" style="font-weight:bold;" align="right"><%=GetLabel("Total") %></td>
                                                <asp:Repeater ID="rptReceivingMonthTotal" runat="server" OnItemDataBound="rptReceivingMonthTotal_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right"><div id="divTotal" runat="server"></div></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td align="right"><div id="divTotalAll" runat="server"></div></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
