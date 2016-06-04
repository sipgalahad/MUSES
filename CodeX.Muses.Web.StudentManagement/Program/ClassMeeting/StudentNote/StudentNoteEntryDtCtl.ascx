<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentNoteEntryDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentNoteEntryDtCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_generatebilldtctl">
    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');
    var rowCount = parseInt('<%=RowCount %>');
    var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
    var currPage = parseInt('<%=CurrPage %>');
    $(function () {
        setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
        setPaging($("#pagingPopup"), pageCount, function (page) {
            cbpViewPopup2.PerformCallback('changepage|' + page);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
        }, null, currPage);
    });

    function onCbpViewPopup2EndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            var rowCount = parseInt(param[2]);
            setNumEntriesText($('#informationNumEntriesPopup'), rowCount, currPage, rowCountPerPage);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                cbpViewPopup2.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntriesPopup'), rowCount, page, rowCountPerPage);
            });
        }
    }
    //#endregion

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();
    }

    $(function () {
        $('#divTransactionAddPopup').click(function (evt) {
            $('#<%=hdnEntryID.ClientID %>').val('');
            cboNoteCategory.SetValue('');
            cboNoteRate.SetValue('');
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

        registerCollapseExpandHandler();
    });

    //#region edit and delete
    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation('Are You Sure Want To Delete?', function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.StudentNoteID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);

        $('#<%=hdnEntryID.ClientID %>').val(entity.StudentNoteID);
        cboNoteCategory.SetValue(entity.GCNoteCategory);
        cboNoteRate.SetValue(entity.GCNoteRate);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        $('#entryDetailContainerPopup').show();
    });

    //#endregion

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#entryDetailContainerPopup').hide();
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

    $('#btnRefreshPopup').click(function () {
        cbpViewPopup2.PerformCallback('refresh');
    });
</script>
<input type="hidden" id="hdnStudentID" runat="server" />
<table class="tblEntryContent" style="width:70%">
    <colgroup>
        <col style="width:160px"/>
        <col/>
    </colgroup>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Siswa")%></label></td>
        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
    </tr> 
</table>

<h4 class="h4expanded"><%=GetLabel("Catatan Pertemuan Hari Ini") %></h4>
<div class="containerTblEntryContent">
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:160px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kategori")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboNoteCategory" ClientInstanceName="cboNoteCategory" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboNoteRate" ClientInstanceName="cboNoteRate" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
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
            <dx:PanelContent ID="PanelContent2" runat="server">
                <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="StudentNoteID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="NoteDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteTime" HeaderText="Jam" HeaderStyle-Width="80px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteCategory" HeaderText="Kategori" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="NoteRate" HeaderText="Nilai" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style='float:right;' class="divDetailDelete"></div>
                                <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                <input type="hidden" value="<%#Eval("StudentNoteID") %>" bindingfield="StudentNoteID" />                            
                                <input type="hidden" value="<%#Eval("GCNoteCategory") %>" bindingfield="GCNoteCategory" />
                                <input type="hidden" value="<%#Eval("GCNoteRate") %>" bindingfield="GCNoteRate" />
                                <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
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
</div>
<br />
<h4 class="h4expanded"><%=GetLabel("Catatan Pertemuan Sebelumnya") %></h4>
<div class="containerTblEntryContent">
    <table cellspacing="0">
        <colgroup>
            <col style="width: 150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kategori")%></label></td>
            <td><dxe:ASPxComboBox ID="cboFilterNoteCategory" Width="200px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai")%></label></td>
            <td><dxe:ASPxComboBox ID="cboFilterNoteRate" Width="200px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"></td>
            <td><input type="button" id="btnRefreshPopup" value='<%=GetLabel("Refresh") %>' /></td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup2" runat="server" Width="100%" ClientInstanceName="cbpViewPopup2"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup2_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpViewPopup2EndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                    <Columns>
                        <asp:BoundField DataField="StudentNoteID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="NoteDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteTime" HeaderText="Jam" HeaderStyle-Width="80px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NoteCategory" HeaderText="Kategori" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="NoteRate" HeaderText="Nilai" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="Remarks" HeaderText="Catatan" />
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
</div>

<dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
    ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>