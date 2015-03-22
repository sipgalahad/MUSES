<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentProgressRuleDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.StudentProgressRuleDtEntryCtl" %>

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
            $('#<%=txtRemarks.ClientID %>').val(''); 
            $('#<%=txtDisplayOrder.ClientID %>').val('');
            $('#<%=txtStudentProgressRuleDtName.ClientID %>').val('');
            $('#<%=chkIsFromPassingGrade.ClientID %>').prop('checked', false);
            $('#<%=chkIsToPassingGrade.ClientID %>').prop('checked', false);
            $('#<%=txtFromValue.ClientID %>').val('');
            $('#<%=txtToValue.ClientID %>').val('');
            $('#<%=chkIsFromPassingGrade.ClientID %>').change();
            $('#<%=chkIsToPassingGrade.ClientID %>').change();
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
                cboGrade.SetValue(entity.GCGrade);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.StudentProgressRuleDtID);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $('#<%=chkIsFromPassingGrade.ClientID %>').prop('checked', entity.IsFromPassingGrade == 'True');
        $('#<%=chkIsToPassingGrade.ClientID %>').prop('checked', entity.IsToPassingGrade == 'True');
        $('#<%=txtFromValue.ClientID %>').val(entity.FromValue);
        $('#<%=txtToValue.ClientID %>').val(entity.ToValue);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
        $('#<%=txtStudentProgressRuleDtName.ClientID %>').val(entity.StudentProgressRuleDtName);
        $('#<%=chkIsFromPassingGrade.ClientID %>').change();
        $('#<%=chkIsToPassingGrade.ClientID %>').change();
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

    $('#<%=chkIsFromPassingGrade.ClientID %>').change(function () {
        if ($(this).is(':checked')) {
            $('#<%=txtFromValue.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtFromValue.ClientID %>').val('');
        }
        else
            $('#<%=txtFromValue.ClientID %>').removeAttr('readonly');
    });

    $('#<%=chkIsToPassingGrade.ClientID %>').change(function () {
        if ($(this).is(':checked')) {
            $('#<%=txtToValue.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtToValue.ClientID %>').val('');
        }
        else 
            $('#<%=txtToValue.ClientID %>').removeAttr('readonly');
    });
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama")%></label></td>
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
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kriteria") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtStudentProgressRuleDtName" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan")%></label></td>
                        <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="80px" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Nilai")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0"> 
                                <tr>
                                    <td valign="top" align="center">
                                        <asp:TextBox ID="txtFromValue" runat="server" Width="80px" CssClass="number" /><br />
                                        <asp:CheckBox ID="chkIsFromPassingGrade" runat="server" /><%=GetLabel("KKM")%>
                                    </td>
                                    <td valign="top"> - </td>
                                    <td valign="top" align="center">
                                        <asp:TextBox ID="txtToValue" runat="server" Width="80px" CssClass="number" /><br />
                                        <asp:CheckBox ID="chkIsToPassingGrade" runat="server" /><%=GetLabel("KKM") %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><label class="lblMandatory"><%=GetLabel("Deskripsi") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" Width="400px" TextMode="MultiLine" Rows="3" /></td>
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
                            <asp:BoundField DataField="StudentProgressRuleDtName" HeaderText="Kriteria" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="cfValue" HeaderText="Nilai" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Remarks" HeaderText="Deskripsi" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("StudentProgressRuleDtID") %>" bindingfield="StudentProgressRuleDtID" />
                                    <input type="hidden" value="<%#Eval("StudentProgressRuleDtName") %>" bindingfield="StudentProgressRuleDtName" />
                                    <input type="hidden" value="<%#Eval("FromValue") %>" bindingfield="FromValue" />
                                    <input type="hidden" value="<%#Eval("IsFromPassingGrade") %>" bindingfield="IsFromPassingGrade" />
                                    <input type="hidden" value="<%#Eval("ToValue") %>" bindingfield="ToValue" />
                                    <input type="hidden" value="<%#Eval("IsToPassingGrade") %>" bindingfield="IsToPassingGrade" />
                                    <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
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

