<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="GenerateUploadFileStudent.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.GenerateUploadFileStudent" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnGenerate" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/download.png")%>' alt="" /><div><%=GetLabel("Download")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#<%=btnGenerate.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    var param = "";
                    $('.chkIsAccepted input:checked').each(function () {
                        var id = $(this).closest('tr').find('.keyField').html();
                        if (param != '') {
                            param += ',';
                        }
                        param += id;
                    });
                    if (param == "")
                        showToast('Warning', 'Silakan Pilih Tagihan Terlebih Dahulu');
                    else {
                        $('#<%=hdnSelectedValue.ClientID %>').val(param);
                        $('#<%=btnExport.ClientID%>').click();
                        cbpView.PerformCallback();
                    }
                }
            });

            $('.chkAcceptAll input').click(function () {
                var value = $(this).is(':checked');
                $('#<%=grdView.ClientID %> .chkIsAccepted input').each(function () {
                    if ($(this).is(':enabled')) $(this).prop("checked", value);
                });
            });
        })

        //#region Bank
        function onGetBankFilterExpression() {
            var filterExpression = "SiteID = '<%=GetSiteID() %>'";
            Methods.getObject('GetvSiteList', filterExpression, function (result) {
                var display = result.DisplayPath.split('/');
                var temp = "";
                for (var i = 1; i < display.length - 1; i++) {
                    if (temp != "") temp += ",";
                    temp += "'" + display[i] + "'";
                }
                filterExpression = "SiteID IN (" + temp + ")";
            })
            return filterExpression;
        }

        function onTacBankButtonSearchClick() {
            openSearchDialog('bank', onGetBankFilterExpression(), function (value) {
                var filterExpression = onGetBankFilterExpression() + " AND BankCode = '" + value + "'";
                Methods.getObject('GetvBankList', filterExpression, function (result) {
                    if (result != null) {
                        tacBank.setValue(result.BankID);
                        tacBank.setText(result.BankName);
                    }
                    else {
                        tacBank.setValue('');
                        tacBank.setText('');
                    }
                    onTacBankValueChanged();
                });
            });

        }

        function onTacBankValueChanged() {
            //cbpView.PerformCallback('refresh');
        }
        //#endregion

        function setStartEndPeriod() {
            var pad = "00";
            var date = new Date();
            var firstDay = new Date(cboYear.GetValue(), cboMonth.GetValue() - 1, 1);
            var lastDay = new Date(cboYear.GetValue(), cboMonth.GetValue(), 0);
            var fpMonth = pad.substring(0, pad.length - (firstDay.getMonth() + 1).toString().length) + (firstDay.getMonth() + 1).toString();
            var epMonth = pad.substring(0, pad.length - (lastDay.getMonth() + 1).toString().length) + (lastDay.getMonth() + 1).toString();
            var endDate = lastDay.getDate() + '-' + epMonth + '-' + lastDay.getFullYear();
            var firstDate = '0' + firstDay.getDate() + '-' + fpMonth + '-' + firstDay.getFullYear();
            $('#<%=txtStartDate.ClientID %>').val(firstDate);
            $('#<%=txtEndDate.ClientID %>').val(endDate);
        }
    </script>
    <input type="hidden" id="hdnSelectedValue" value="" runat="server" />
    <input type="hidden" id="hdnDepositAmount" value="" runat="server" />
    <input type="hidden" id="hdnSiteID" value="" runat="server" />
    <div>
        <div style="display:none;">
            <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
            <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
        </div>
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bank")%></label></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacBank" ClientInstanceName="tacBank" MethodName="GetvBankList" GetFilterExpressionFunction="onGetBankFilterExpression"
                        SearchFields="BankName,BankCode" TextField="BankName" ValueField="BankID" SearchText="${BankName} (<b>${BankCode}</b>)" OrderByExpression="BankName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacBankButtonSearchClick(); }"
                            ValueChanged="function(){ onTacBankValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Bulan") %></td>
                <td style="padding-right: 1px; width: 140px">
                    <table cellpadding="0" cellspacing="0" >
                        <colgroup>
                            <col width="120px" />
                            <col width="70px" />
                            <col width="120px" />
                        </colgroup>
                        <tr>
                            <td class="tdMonth">
                                <dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" Width="120px">
                                    <ClientSideEvents ValueChanged="function(s,e){setStartEndPeriod()}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tahun")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboYear" runat="server" ClientInstanceName="cboYear" Width="120px" >
                                    <ClientSideEvents ValueChanged="function(s,e){setStartEndPeriod()}" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Periode") %></td>
                <td>
                    <table cellpadding="0" cellspacing="0" >
                        <colgroup>
                            <col width="100px" />
                            <col width="30px" />
                            <col width="100px" />
                        </colgroup>
                        <tr>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtStartDate" Width="100px" CssClass="datepicker" runat="server" /></td>
                            <td>s/d</td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtEndDate" Width="100px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="divTransactionEntry">
        <hr />
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label><%=GetLabel("Deposit")%></label></td>
                <td><asp:TextBox ID="txtDepositAmount" CssClass="txtCurrency" ReadOnly="true" Width="150px" runat="server" /></td>
            </tr>
        </table>
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
                                <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="text-align:center">
                                            <asp:CheckBox runat="server" ID="chkAcceptAll" CssClass="chkAcceptAll" />
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsAccepted" CssClass="chkIsAccepted" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StudentFeeDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="cfStudentFeeCompTypeName" HeaderText="Jenis Biaya" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="DueDateInString" HeaderText="Jatuh Tempo" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="TotalStudentAmount" HeaderText="Jumlah" HeaderStyle-Width="180px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                <asp:CheckBoxField HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderText="Generate" DataField="IsProcessed" />
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
</asp:Content>
