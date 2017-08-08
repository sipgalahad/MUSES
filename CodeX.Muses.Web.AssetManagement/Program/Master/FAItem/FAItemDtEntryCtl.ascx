<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAItemDtEntryCtl.ascx.cs" 
    Inherits="Codex.Ottimo.Web.AssetManagement.Program.FAItemDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtFixedAssetDtCode.ClientID %>').val('');
            $('#<%=txtSerialNumber.ClientID %>').val('');
            $('#<%=hdnFALocationID.ClientID %>').val('');
            $('#<%=txtFALocationCode.ClientID %>').val('');
            $('#<%=txtFALocationName.ClientID %>').val('');

            $('#entryDetailContainerPopup').show();
        });

        //#region FA Location
        function onGetFALocationFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

        $('#lblFALocation.lblLink').click(function () {
            openSearchDialog('falocation', onGetFALocationFilterExpression(), function (value) {
                $('#<%=txtFALocationCode.ClientID %>').val(value);
                onTxtFALocationCodeChanged(value);
            });
        });

        $('#<%=txtFALocationCode.ClientID %>').change(function () {
            onTxtFALocationCodeChanged($(this).val());
        });

        function onTxtFALocationCodeChanged(value) {
            var filterExpression = onGetFALocationFilterExpression() + " AND FALocationCode = '" + value + "'";
            Methods.getObject('GetFALocationList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnFALocationID.ClientID %>').val(result.FALocationID);
                    $('#<%=txtFALocationName.ClientID %>').val(result.FALocationName);
                }
                else {
                    $('#<%=hdnFALocationID.ClientID %>').val('');
                    $('#<%=txtFALocationCode.ClientID %>').val('');
                    $('#<%=txtFALocationName.ClientID %>').val('');
                }
            });
        }
        //#endregion

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
                cbpProcessPopup.PerformCallback('save');
        });
    });

    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation('Are You Sure Want To Delete?', function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.FixedAssetDtID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.FixedAssetDtID);
        $('#<%=txtFixedAssetDtCode.ClientID %>').val(entity.FixedAssetDtCode);
        $('#<%=txtSerialNumber.ClientID %>').val(entity.SerialNumber);
        $('#<%=hdnFALocationID.ClientID %>').val(entity.FALocationID);
        $('#<%=txtFALocationCode.ClientID %>').val(entity.FALocationCode);
        $('#<%=txtFALocationName.ClientID %>').val(entity.FALocationName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        $('#entryDetailContainerPopup').show();
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kamar")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtFixedAssetDtCode" CssClass="required" runat="server" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblLink lblMandatory" id="lblFALocation"><%=GetLabel("Lokasi Aktiva Tetap")%></label></td>
                        <td>
                            <input type="hidden" id="hdnFALocationID" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtFALocationCode" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtFALocationName" ReadOnly="true" Width="100%" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nomor Seri")%></label></td>
                        <td><asp:TextBox ID="txtSerialNumber" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="FixedAssetDtCode" HeaderText="Kode"/>
                            <asp:BoundField DataField="FALocationName" HeaderText="Lokasi" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="SerialNumber" HeaderText="No Seri" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("FixedAssetDtID") %>" bindingfield="FixedAssetDtID" />
                                    <input type="hidden" value="<%#Eval("FixedAssetDtCode") %>" bindingfield="FixedAssetDtCode" />
                                    <input type="hidden" value="<%#Eval("SerialNumber") %>" bindingfield="SerialNumber" />
                                    <input type="hidden" value="<%#Eval("FALocationID") %>" bindingfield="FALocationID" />
                                    <input type="hidden" value="<%#Eval("FALocationCode") %>" bindingfield="FALocationCode" />
                                    <input type="hidden" value="<%#Eval("FALocationName") %>" bindingfield="FALocationName" />
                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>
