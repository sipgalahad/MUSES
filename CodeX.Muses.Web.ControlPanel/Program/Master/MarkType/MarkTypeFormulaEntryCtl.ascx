<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarkTypeFormulaEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.MarkTypeFormulaEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtMinValue.ClientID %>').val('');
            $('#<%=txtMaxValue.ClientID %>').val('');
            $('#<%=hdnFromMarkTypeDtID.ClientID %>').val('');
            $('#<%=hdnToMarkTypeDtID.ClientID %>').val('');
            tacFromMarkTypeDt.setValue('');
            tacFromMarkTypeDt.setText('');
            tacToMarkTypeDt.setValue('');
            tacToMarkTypeDt.setText('');
            $('#<%=txtRemarks.ClientID %>').val('');

            $('#entryDetailContainerPopup').show();
        });

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
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.MarkTypeFormulaID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.MarkTypeFormulaID);
        $('#<%=txtMinValue.ClientID %>').val(entity.MinValue);
        $('#<%=txtMaxValue.ClientID %>').val(entity.MaxValue);
        $('#<%=hdnFromMarkTypeDtID.ClientID %>').val(entity.FromMarkTypeDtID);
        $('#<%=hdnToMarkTypeDtID.ClientID %>').val(entity.ToMarkTypeDtID);
        tacFromMarkTypeDt.setValue(entity.FromMarkTypeDtID);
        tacFromMarkTypeDt.setText(entity.FromMarkTypeDtName);
        tacToMarkTypeDt.setValue(entity.ToMarkTypeDtID);
        tacToMarkTypeDt.setText(entity.ToMarkTypeDtName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        $('#entryDetailContainerPopup').show();
    });

    //#region From Mark Type
    window.onGetFromMarkTypeDtFilterExpression = function() {
        var filterExpression = "MarkTypeID = " + cboFromMarkType.GetValue().split('|')[0] + " AND IsDeleted = 0";
        return filterExpression;
    }

    function onTacFromMarkTypeDtButtonSearchClick() {
        openSearchDialog('marktypedt', onGetFromMarkTypeDtFilterExpression(), function (value) {
            var filterExpression = onGetFromMarkTypeDtFilterExpression() + " AND MarkTypeDtID = '" + value + "'";
            Methods.getObject('GetMarkTypeDtList', filterExpression, function (result) {
                if (result != null) {
                    tacFromMarkTypeDt.setValue(result.MarkTypeDtID);
                    tacFromMarkTypeDt.setText(result.MarkTypeDtName);
                    $('#<%=hdnFromMarkTypeDtID.ClientID %>').val(result.MarkTypeDtID);
                }
                else {
                    tacFromMarkTypeDt.setValue('');
                    tacFromMarkTypeDt.setText('');
                    $('#<%=hdnFromMarkTypeDtID.ClientID %>').val('');
                }
            });
        });
    }

    function onTacFromMarkTypeDtValueChanged() {
        $('#<%=hdnFromMarkTypeDtID.ClientID %>').val(tacFromMarkTypeDt.getValue());
    }
    //#endregion

    //#region To Mark Type
    window.onGetToMarkTypeDtFilterExpression = function () {
        var filterExpression = "MarkTypeID = " + $('#<%=hdnID.ClientID %>').val() + " AND IsDeleted = 0";
        return filterExpression;
    }

    function onTacToMarkTypeDtButtonSearchClick() {
        openSearchDialog('marktypedt', onGetToMarkTypeDtFilterExpression(), function (value) {
            var filterExpression = onGetToMarkTypeDtFilterExpression() + " AND MarkTypeDtID = '" + value + "'";
            Methods.getObject('GetMarkTypeDtList', filterExpression, function (result) {
                if (result != null) {
                    tacToMarkTypeDt.setValue(result.MarkTypeDtID);
                    tacToMarkTypeDt.setText(result.MarkTypeDtName);
                    $('#<%=hdnToMarkTypeDtID.ClientID %>').val(result.MarkTypeDtID);

                }
                else {
                    tacToMarkTypeDt.setValue('');
                    tacToMarkTypeDt.setText('');
                    $('#<%=hdnToMarkTypeDtID.ClientID %>').val('');
                }
            });
        });
    }

    function onTacToMarkTypeDtValueChanged() {
        $('#<%=hdnToMarkTypeDtID.ClientID %>').val(tacToMarkTypeDt.getValue());
    }
    //#endregion

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

    function onCboFromMarkTypeValueChanged() {
        var GCMarkType = cboFromMarkType.GetValue().split('|')[1];
        $('#<%=hdnGCMarkType.ClientID %>').val(GCMarkType);
        if (GCMarkType == "<%=GetMarkTypeNumber() %>") {
            $('#trMark').removeAttr('style');
            $('#trOption').attr('style', 'display:none');
        }
        else {
            $('#trOption').removeAttr('style');
            $('#trMark').attr('style', 'display:none');
        }
        cbpViewPopup.PerformCallback('refresh');
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dari Tipe")%></label></td>
            <td colspan="2">
                <input type="hidden" id="hdnGCMarkType" value="" runat="server" />
                <dxe:ASPxComboBox ID="cboFromMarkType" ClientInstanceName="cboFromMarkType" Width="100%" runat="server">
                    <ClientSideEvents Init="function(s,e){ onCboFromMarkTypeValueChanged() }" 
                        ValueChanged="function(s,e){ onCboFromMarkTypeValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr id="trMark">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Dari Nilai")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:80px"/>
                                    <col style="width:25px"/>
                                    <col style="width:80px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtMinValue" runat="server" CssClass="number" Width="100%" /></td>
                                    <td align="center">s/d</td>
                                    <td><asp:TextBox ID="txtMaxValue" runat="server" CssClass="number" Width="100%" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trOption">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Dari Nilai")%></label></td>
                        <td>      
                            <input type="hidden" id="hdnFromMarkTypeDtID" runat="server" />      
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacFromMarkTypeDt" ClientInstanceName="tacFromMarkTypeDt" MethodName="GetMarkTypeDtList" GetFilterExpressionFunction="onGetFromMarkTypeDtFilterExpression"
                                SearchFields="MarkTypeDtName" TextField="MarkTypeDtName" ValueField="MarkTypeDtID" SearchText="${MarkTypeDtName}" OrderByExpression="MarkTypeDtName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacFromMarkTypeDtButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacFromMarkTypeDtValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Akhir")%></label></td>
                        <td>      
                            <input type="hidden" id="hdnToMarkTypeDtID" runat="server" />      
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacToMarkTypeDt" ClientInstanceName="tacToMarkTypeDt" MethodName="GetMarkTypeDtList" GetFilterExpressionFunction="onGetToMarkTypeDtFilterExpression"
                                SearchFields="MarkTypeDtName" TextField="MarkTypeDtName" ValueField="MarkTypeDtID" SearchText="${MarkTypeDtName}" OrderByExpression="MarkTypeDtName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacToMarkTypeDtButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacToMarkTypeDtValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" runat="server" Width="300px" TextMode="MultiLine" Rows="2" /></td>
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
                            <asp:BoundField DataField="cfFromValue" HeaderText="Dari Nilai" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="ToMarkTypeDtName" HeaderText="Nilai Akhir" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("MarkTypeFormulaID") %>" bindingfield="MarkTypeFormulaID" />
                                    <input type="hidden" value="<%#Eval("MinValue") %>" bindingfield="MinValue" />
                                    <input type="hidden" value="<%#Eval("MaxValue") %>" bindingfield="MaxValue" />
                                    <input type="hidden" value="<%#Eval("FromMarkTypeDtID") %>" bindingfield="FromMarkTypeDtID" />
                                    <input type="hidden" value="<%#Eval("FromMarkTypeDtName") %>" bindingfield="FromMarkTypeDtName" />
                                    <input type="hidden" value="<%#Eval("ToMarkTypeDtID") %>" bindingfield="ToMarkTypeDtID" />
                                    <input type="hidden" value="<%#Eval("ToMarkTypeDtName") %>" bindingfield="ToMarkTypeDtName" />
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

