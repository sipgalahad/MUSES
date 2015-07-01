<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTaskLogEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskLogEntryCtl" %>

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
        setDatePicker('<%=txtStartDate.ClientID %>');

        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtNoteName.ClientID %>').val('');
            $('#<%=txtRemarks.ClientID %>').val('');
            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                cbpProcessPopup.PerformCallback('save');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divPopupDelete').die('click');
    $('#<%=grdView.ClientID %> .divPopupDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskLogID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divPopupEdit').die('click');
    $('#<%=grdView.ClientID %> .divPopupEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskLogID);
        $('#<%=txtNoteName.ClientID %>').val(entity.NoteName);
        $('#<%=txtStartDate.ClientID %>').val(entity.NoteDateInDatePicker);
        $('#<%=txtStartTime.ClientID %>').val(entity.NoteTime);
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
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td ><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Mulai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox ID="txtPTStartDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                        <td style="width:10px; text-align:center">&nbsp;</td>
                        <td><asp:TextBox ID="txtPTStartTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox ID="txtPTEndDate" Width="120px" runat="server" CssClass="datepicker" ReadOnly="true" /></td>
                        <td style="width:10px; text-align:center">&nbsp;</td>
                        <td><asp:TextBox ID="txtPTEndTime" CssClass="thCenter" Width="70px" runat="server" ReadOnly="true"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
            <td><asp:TextBox runat="server" ID="txtPTRemarks" TextMode="MultiLine" Rows="2" Width="300px" ReadOnly="true" /></td>
        </tr>
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table id="tblEntryPopup">
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtNoteName" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:TextBox ID="txtStartDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    <td style="width:10px; text-align:center">&nbsp;</td>
                                    <td><asp:TextBox ID="txtStartTime" CssClass="thCenter" Width="70px" runat="server"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr id="trSaveEntryPopup">
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
                            <asp:BoundField DataField="NoteName" HeaderText="Nama" HeaderStyle-Width="250px" />
                            <asp:BoundField DataField="NoteDateInString" HeaderText="Tanggal" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="NoteTime" HeaderText="Waktu" HeaderStyle-Width="70px" />
                            <asp:TemplateField HeaderText="Keterangan" >
                                <ItemTemplate>
                                    <%#Eval("CustomRemarks")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divPopupDelete">X</div>
                                    <div style='float:right;margin-right:10px;' class="divPopupEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("ProjectTaskLogID") %>" bindingfield="ProjectTaskLogID" />
                                    <input type="hidden" value="<%#Eval("NoteName") %>" bindingfield="NoteName" />
                                    <input type="hidden" value="<%#Eval("NoteDateInDatePicker") %>" bindingfield="NoteDateInDatePicker" />
                                    <input type="hidden" value="<%#Eval("NoteTime") %>" bindingfield="NoteTime" />
                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                    <input type="hidden" value="<%#Eval("EmployeeID") %>" bindingfield="EmployeeID" />
                                    <input type="hidden" value="<%#Eval("EmployeeName") %>" bindingfield="EmployeeName" />
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

