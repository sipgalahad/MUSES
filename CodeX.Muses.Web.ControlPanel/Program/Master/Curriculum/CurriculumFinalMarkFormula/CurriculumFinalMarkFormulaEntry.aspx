<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCurriculumPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="CurriculumFinalMarkFormulaEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumFinalMarkFormulaEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtCurriculumFinalMarkFormulaCode.ClientID %>').val('');
                $('#<%=txtCurriculumFinalMarkFormulaName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
                cboMarkType.SetEnabled(false);
                cboFinalMarkSource.SetSelectedIndex(0);
                onCboFinalMarkSourceValueChanged();
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                cboMarkType.SetEnabled(true);
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumFinalMarkFormulaID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumFinalMarkFormulaID);
            $('#<%=txtCurriculumFinalMarkFormulaCode.ClientID %>').val(entity.CurriculumFinalMarkFormulaCode);
            $('#<%=txtCurriculumFinalMarkFormulaName.ClientID %>').val(entity.CurriculumFinalMarkFormulaName);
            cboFinalMarkSource.SetValue(entity.GCFinalMarkSource);
            cboSummaryType.SetValue(entity.GCSummaryType);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            onCboFinalMarkSourceValueChanged();
            cboMarkType.SetEnabled(false);
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        function onCboFinalMarkSourceValueChanged() {
            if (cboFinalMarkSource.GetValue() == '<%=OnGetFinalMarkSourceIndicator() %>')
                cboSummaryType.SetEnabled(true);
            else
                cboSummaryType.SetEnabled(false);
        }

        function onCboMarkTypeValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        $('.lnkDetail a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Curriculum/CurriculumFinalMarkFormula/CurriculumFinalMarkFormulaDtEntryCtl.ascx");
            openUserControlPopup(url, entity.CurriculumFinalMarkFormulaID, 'Detil', 800, 550);
        });
    </script>
    <fieldset id="fsFilter">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:200px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Nilai")%></label></td>
                <td>
                    <dxe:ASPxComboBox ID="cboMarkType" ClientInstanceName="cboMarkType" Width="200px" runat="server">
                        <ClientSideEvents ValueChanged="function(){ onCboMarkTypeValueChanged(); }" />
                    </dxe:ASPxComboBox>
                </td>
            </tr> 
        </table>
    </fieldset>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 160px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                                    <td><asp:TextBox ID="txtCurriculumFinalMarkFormulaCode" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtCurriculumFinalMarkFormulaName" runat="server" Width="250px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber Penilaian")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cboFinalMarkSource" ClientInstanceName="cboFinalMarkSource" runat="server" Width="250px">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboFinalMarkSourceValueChanged() }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Summary Nilai")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboSummaryType" ClientInstanceName="cboSummaryType" runat="server" Width="250px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Keterangan")%></label></td>
                                    <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" Width="400px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
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
                                <asp:BoundField DataField="CurriculumFinalMarkFormulaCode" HeaderText="Kode" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="CurriculumFinalMarkFormulaName" HeaderText="Nama" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("CurriculumFinalMarkFormulaID") %>" bindingfield="CurriculumFinalMarkFormulaID" />
                                        <input type="hidden" value="<%#Eval("CurriculumFinalMarkFormulaCode") %>" bindingfield="CurriculumFinalMarkFormulaCode" />
                                        <input type="hidden" value="<%#Eval("CurriculumFinalMarkFormulaName") %>" bindingfield="CurriculumFinalMarkFormulaName" />
                                        <input type="hidden" value="<%#Eval("GCFinalMarkSource") %>" bindingfield="GCFinalMarkSource" />
                                        <input type="hidden" value="<%#Eval("GCSummaryType") %>" bindingfield="GCSummaryType" />
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
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>