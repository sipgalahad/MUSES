<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="UpdateRenumerationCompEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.UpdateRenumerationCompEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divQuickPicks').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divQuickPicks').hide();
            }

            setDatePicker('<%=txtStartEffectiveDate.ClientID %>');
            $('#<%=txtStartEffectiveDate.ClientID %>').datepicker('option', 'minDate', '0');
            setDatePicker('<%=txtTransactionDate.ClientID %>');
            $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'minDate', '0');



            $('#<%=chkIsUseFormula.ClientID %>').change(function () {
                if (this.checked) {
                    $('#<%=txtAmount.ClientID %>').val('0').trigger('changeValue');
                    $('#<%=txtAmount.ClientID %>').attr('readonly', 'readonly');
                }
                else
                    $('#<%=txtAmount.ClientID %>').removeAttr('readonly');
            });

            //#region Transaction No
            function onGetRenumerationPositionFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblTransactionNo.lblLink').click(function () { 
                openSearchDialog('transrenumerationcomphd', onGetRenumerationPositionFilterExpression(), function (value) {
                    $('#<%=txtTransactionNo.ClientID %>').val(value);
                    onTxtTransactionNoChanged(value);
                });
            });

            $('#<%=txtTransactionNo.ClientID %>').change(function () {
                onTxtTransactionNoChanged($(this).val());
            });

            function onTxtTransactionNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
            onCboRenumerationAmountSourceValueChanged();
        }
        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (TransactionID == '' || TransactionID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "TransactionID = " + TransactionID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

        //#region Renumeration Comp
        function onGetRenumerationCompFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

        function onTacRenumerationCompSearchClick() {
            openSearchDialog('renumerationcomp', onGetRenumerationCompFilterExpression(), function (value) {
                var filterExpression = onGetRenumerationCompFilterExpression() + " AND RenumerationCompCode = '" + value + "'";
                Methods.getObject('GetvRenumerationCompList', filterExpression, function (result) {
                    if (result != null) {
                        tacRenumerationComp.setValue(result.RenumerationCompID);
                        tacRenumerationComp.setText(result.RenumerationCompName);
                    }
                    else {
                        tacRenumerationComp.setValue('');
                        tacRenumerationComp.setText('');
                    }
                    cbpView.PerformCallback('refresh');
                });
            });
        }

        function onTacRenumerationCompValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region From Renumeration Comp
        function onGetFromRenumerationCompFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

        function onTacFromRenumerationCompSearchClick() {
            openSearchDialog('renumerationcomp', onGetFromRenumerationCompFilterExpression(), function (value) {
                var filterExpression = onGetFromRenumerationCompFilterExpression() + " AND RenumerationCompCode = '" + value + "'";
                Methods.getObject('GetvRenumerationCompList', filterExpression, function (result) {
                    if (result != null) {
                        tacFromRenumerationComp.setValue(result.RenumerationCompID);
                        tacFromRenumerationComp.setText(result.RenumerationCompName);
                    }
                    else {
                        tacFromRenumerationComp.setValue('');
                        tacFromRenumerationComp.setText('');
                    }
                });
            });
        }

        function onTacFromRenumerationCompValueChanged() {
            var id = tacFromRenumerationComp.getValue();
            if (id != '') {
            }
        }
        //#endregion

        function onBeforeSaveRecord(errMessage) {
            var result = "";
            $('.tblView tr:gt(0)').each(function () {
                if (result != '')
                    result += '|';
                result += $(this).find('.hdnComp1Name').val() + ';' + $(this).find('.hdnComp1').val() + ';' + $(this).find('.hdnComp2Name').val() + ';' + $(this).find('.hdnComp2').val() + ';' + $(this).find('.txtValue').attr('hiddenVal');
            });
            $('#<%=hdnListSaveValue.ClientID %>').val(result);
            return true;
        }

        function onCboRenumerationAmountSourceValueChanged() {
            if (cboRenumerationAmountSource.GetValue() == '<%=OnGetRenumerationSourceAmountFixed() %>') {
                $('#<%=trRenumerationComp.ClientID %>').attr('style', 'display:none');
                $('#<%=trPercentage.ClientID %>').attr('style', 'display:none');
                if ($('#<%=hdnIsApplyToAll.ClientID %>').val() == '0')
                    $('#<%=trAmount.ClientID %>').removeAttr('style');
                else
                    $('#<%=trPercentage.ClientID %>').attr('style', 'display:none');
            }
            else if (cboRenumerationAmountSource.GetValue() == '<%=OnGetRenumerationSourceAmountRenumerationCompPercentage() %>') {
                $('#<%=trAmount.ClientID %>').attr('style', 'display:none');
                if ($('#<%=hdnIsApplyToAll.ClientID %>').val() == '0')
                    $('#<%=trPercentage.ClientID %>').removeAttr('style');
                else
                    $('#<%=trPercentage.ClientID %>').attr('style', 'display:none');
                $('#<%=trRenumerationComp.ClientID %>').removeAttr('style');
            }
            else {
                $('#<%=trAmount.ClientID %>').attr('style', 'display:none');
                $('#<%=trPercentage.ClientID %>').attr('style', 'display:none');
                $('#<%=trRenumerationComp.ClientID %>').removeAttr('style');
            }
        }

        function onCbpViewEndCallback() {
            hideLoadingPanel();
            onCboRenumerationAmountSourceValueChanged();
            $('.tblView .txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }
    </script>    
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnTransactionDtID" runat="server" />
    <input type="hidden" value="" id="hdnListSaveValue" runat="server" />
    <input type="hidden" value="" id="hdnIsEditable" runat="server" />

    <div style="height: 550px; overflow-y: auto; overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 50%" />
                <col style="width: 50%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />  
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblTransactionNo" ><%=GetLabel("No. Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionNo" Width="150px"  runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Dimasukkan")%></td>
                            <td><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Berlaku")%></td>
                            <td><asp:TextBox ID="txtStartEffectiveDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Komp Renumerasi")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRenumerationComp" ClientInstanceName="tacRenumerationComp" MethodName="GetvRenumerationHdList" GetFilterExpressionFunction="onGetRenumerationCompFilterExpression"
                                    SearchFields="RenumerationCompName,RenumerationCompID" TextField="RenumerationName" ValueField="RenumerationID" SearchText="${RenumerationCompName} (<b>${RenumerationCompCode}</b>)" OrderByExpression="RenumerationCompName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacRenumerationCompSearchClick(); }"
                                        ValueChanged="function(){ onTacRenumerationCompValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber Nilai")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboRenumerationAmountSource" ClientInstanceName="cboRenumerationAmountSource" Width="200px" runat="server" >
                                    <ClientSideEvents ValueChanged="function(s,e){ onCboRenumerationAmountSourceValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr id="trRenumerationComp" style="display: none;" runat="server">
                            <td class="tdLabel"><label class="lblMandatory" id="lblEmployee"><%=GetLabel("Dari Komp Renumerasi")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacFromRenumerationComp" ClientInstanceName="tacFromRenumerationComp" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetFromRenumerationCompFilterExpression"
                                    SearchFields="RenumerationCompName,RenumerationCompID" TextField="RenumerationCompName" ValueField="RenumerationCompID" SearchText="${RenumerationCompName} (<b>${RenumerationCompCode}</b>)" OrderByExpression="RenumerationCompName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacFromRenumerationCompSearchClick(); }"
                                        ValueChanged="function(){ onTacFromRenumerationCompValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr id="trAmount" style="display: none;" runat="server">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Amount")%></label></td>
                            <td><asp:TextBox ID="txtAmount" CssClass="txtCurrency" Width="120px" runat="server" /></td>
                        </tr>
                        <tr id="trPercentage" style="display: none;" runat="server">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Persen")%></label></td>
                            <td><asp:TextBox ID="txtPercentage" CssClass="txtCurrency" Width="80px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />  
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"></td>
                            <td><asp:Checkbox runat="server" ID="chkIsAllowChange" Text="Is Allow Changed"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"></td>
                            <td><asp:Checkbox runat="server" ID="chkIsApplyWhenLeave" Text="Cuti"/></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"></td>
                            <td><asp:CheckBox runat="server" ID="chkIsUseFormula" Text="Is Use Formula" /></td>
                        </tr>
                       <tr>
                            <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <input type="hidden" id="hdnIsApplyToAll" runat="server" />
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative;">
                                    <table class="tblTransactionEntryResult tblView" width="100%" rules="all">
                                        <tr>
                                            <th id="thComp1" runat="server"></th>
                                            <th id="thComp2" runat="server"></th>
                                            <th><%=GetLabel("Nilai Renumerasi") %></th>
                                        </tr>
                                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <input type="hidden" class="hdnComp1" value='<%#Eval("Comp1ID") %>' />
                                                        <input type="hidden" class="hdnComp1Name" value='<%#Eval("Comp1Name") %>' />
                                                        <%#Eval("Comp1") %>
                                                    </td>
                                                    <td>
                                                        <input type="hidden" class="hdnComp2" value='<%#Eval("Comp2ID") %>' />
                                                        <input type="hidden" class="hdnComp2Name" value='<%#Eval("Comp2Name") %>' />
                                                        <%#Eval("Comp2") %>
                                                    </td>
                                                    <td><asp:TextBox ID="txtValue" runat="server" CssClass="txtValue txtCurrency" Width="100%" ValidationGroup="mpEntry" /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
