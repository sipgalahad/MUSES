<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurriculumFinalMarkFormulaDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumFinalMarkFormulaDtEntryCtl" %>

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
            $('#<%=txtFinalMarkPercentage.ClientID %>').val(''); 
            $('#<%=txtDisplayOrder.ClientID %>').val('');
            $('#<%=txtCurriculumFinalMarkFormulaDtName.ClientID %>').val('');

            $('#<%=hdnLstMarkTypeID.ClientID %>').val('');
            ddeMarkType.SetText('');
            $('.chkMarkType input:checked').each(function () {
                $(this).prop('checked', false);
            });

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
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumFinalMarkFormulaDtID);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $('#<%=txtFinalMarkPercentage.ClientID %>').val(entity.FinalMarkPercentage);
        $('#<%=txtCurriculumFinalMarkFormulaDtName.ClientID %>').val(entity.CurriculumFinalMarkFormulaDtName);

        $('.chkMarkType input:checked').each(function () {
            $(this).prop('checked', false);
        });

        var lstMarkTypeID = entity.ListMarkTypeID.split(',');
        for (var i = 0; i < lstMarkTypeID.length; ++i) {
            $('.chkMarkType').each(function () {
                if ($(this).attr('marktypeid') == lstMarkTypeID[i])
                    $(this).find('input').prop('checked', true);
            });
        }

        setDdeMarkTypeText();

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

    $('.chkMarkType input').change(function () {
        setDdeMarkTypeText();
    });

    function setDdeMarkTypeText() {
        var lstMarkTypeID = '';
        var lstMarkTypeName = '';
        $('.chkMarkType input:checked').each(function () {
            if (lstMarkTypeName != '') {
                lstMarkTypeName += ', ';
                lstMarkTypeID += ',';
            }
            lstMarkTypeID += $(this).parent().attr('marktypeid');
            lstMarkTypeName += $(this).parent().attr('marktypename');
        });
        $('#<%=hdnLstMarkTypeID.ClientID %>').val(lstMarkTypeID);
        ddeMarkType.SetText(lstMarkTypeName);
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnLstMarkTypeID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Formula")%></label></td>
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtCurriculumFinalMarkFormulaDtName" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan")%></label></td>
                        <td><asp:TextBox ID="txtDisplayOrder" runat="server" Width="80px" CssClass="number" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tugas")%></label></td>
                        <td>
                            <dxe:ASPxDropDownEdit ClientInstanceName="ddeMarkType" ID="ddeMarkType"
                                Width="300px" runat="server" EnableAnimation="False">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <asp:Repeater ID="rptMarkType" runat="server" OnItemDataBound="rptMarkType_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMarkType" CssClass="chkMarkType" runat="server"  /> <%#Eval("CurriculumMarkTypeDtName") %><br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </DropDownWindowTemplate>
                            </dxe:ASPxDropDownEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("[%] Bobot Nilai")%></label></td>
                        <td><asp:TextBox ID="txtFinalMarkPercentage" runat="server" Width="80px" CssClass="number" /></td>
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
                            <asp:BoundField DataField="CurriculumFinalMarkFormulaDtName" HeaderText="Nama" />
                            <asp:BoundField DataField="ListMarkTypeName" HeaderText="Tipe Tugas" HeaderStyle-Width="300px" />
                            <asp:BoundField DataField="FinalMarkPercentage" HeaderText="[%] Bobot Nilai Akhir" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("CurriculumFinalMarkFormulaDtID") %>" bindingfield="CurriculumFinalMarkFormulaDtID" />
                                    <input type="hidden" value="<%#Eval("CurriculumFinalMarkFormulaDtName") %>" bindingfield="CurriculumFinalMarkFormulaDtName" />
                                    <input type="hidden" value="<%#Eval("ListMarkTypeID") %>" bindingfield="ListMarkTypeID" />
                                    <input type="hidden" value="<%#Eval("ListMarkTypeName") %>" bindingfield="ListMarkTypeName" />
                                    <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                    <input type="hidden" value="<%#Eval("FinalMarkPercentage") %>" bindingfield="FinalMarkPercentage" />
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

