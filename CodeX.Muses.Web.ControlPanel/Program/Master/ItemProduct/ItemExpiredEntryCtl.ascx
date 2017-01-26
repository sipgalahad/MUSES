<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemExpiredEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.ItemExpiredEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_generatebilldtctl">
    $(function () {
        setDatePicker('<%=txtExpiredDate.ClientID %>');

        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnIsAdd.ClientID %>').val('1');
            $('#<%=txtBatchNumber.ClientID %>').removeAttr('readonly');
            $('#<%=txtBatchNumber.ClientID %>').val('');
            $('#<%=chkIsEmpty.ClientID %>').prop('checked', false);
            $('#<%=txtExpiredDate.ClientID %>').val('');

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
    
    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    var rowCount = parseInt('<%=RowCount %>');
    var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
    var currPage = parseInt('<%=CurrPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
        setPaging($("#pagingPopup"), pageCount, function (page) {
            cbpViewPopup.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
        }, null, currPage);
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpViewPopup.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion

    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation('Are You Sure Want To Delete?', function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=txtBatchNumber.ClientID %>').val(entity.BatchNumber);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnIsAdd.ClientID %>').val('0');
        $('#<%=txtBatchNumber.ClientID %>').attr('readonly', 'readonly');
        $('#<%=txtBatchNumber.ClientID %>').val(entity.BatchNumber);
        $('#<%=chkIsEmpty.ClientID %>').prop('checked', entity.IsEmpty == 'True');
        $('#<%=txtExpiredDate.ClientID %>').val(entity.ExpiredDateInDatePickerFormat);

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
       
<div style="height:440px; overflow-y:auto; overflow-x:hidden"> 
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Item")%></label></td>
            <td><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="200px" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><%=GetLabel("Tipe Tampilan") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboViewType" ClientInstanceName="cboViewType" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){
                        cbpViewPopup.PerformCallback('refresh');
                    }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>

    <input type="hidden" id="hdnID" value="" runat="server" />        
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnIsAdd" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Batch Number")%></label></td>
                        <td><asp:TextBox ID="txtBatchNumber" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Expired Date")%></label></td>
                        <td><asp:TextBox ID="txtExpiredDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kosong")%></label></td>
                        <td><asp:CheckBox ID="chkIsEmpty" runat="server" /></td>
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
            EndCallback="function(s,e) { onCbpViewPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="BatchNumber" HeaderText="No. Batch" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="ExpiredDateInString" HeaderText="Tanggal Expired" />
                        <asp:CheckBoxField DataField="IsEmpty" HeaderText="Kosong" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style='float:right;' class="divDetailDelete"></div>
                                <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                <input type="hidden" value="<%#Eval("BatchNumber") %>" bindingfield="BatchNumber" />
                                <input type="hidden" value="<%#Eval("ExpiredDateInDatePickerFormat") %>" bindingfield="ExpiredDateInDatePickerFormat" />
                                <input type="hidden" value="<%#Eval("IsEmpty") %>" bindingfield="IsEmpty" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <%=GetLabel("No Data To Display")%>
                    </EmptyDataTemplate>
                </asp:GridView>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
        <div class="wrapperPaging">
            <div id="pagingPopup"></div>
        </div>
    </div> 
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>