<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCurriculumPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="CurriculumMarkTypeEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumMarkTypeEntry" %>

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
                $('#<%=txtCurriculumMarkTypeName.ClientID %>').val('');
                cboTaskMarkType.SetValue('');
                cboFinalMarkType.SetValue('');
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
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
                    $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumMarkTypeID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumMarkTypeID);
            $('#<%=txtCurriculumMarkTypeName.ClientID %>').val(entity.CurriculumMarkTypeName);
            cboTaskMarkType.SetValue(entity.GCTaskMarkType);
            cboFinalMarkType.SetValue(entity.GCFinalMarkType);

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

        $('.lnkDetail a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Curriculum/CurriculumMarkType/CurriculumMarkTypeDtEntryCtl.ascx");
            openUserControlPopup(url, entity.CurriculumMarkTypeID, 'Detil', 800, 550);
        });
    </script>
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
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtCurriculumMarkTypeName" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Format Nilai Tugas")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboTaskMarkType" ClientInstanceName="cboTaskMarkType" runat="server" Width="100px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Format Nilai Rapor")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFinalMarkType" ClientInstanceName="cboFinalMarkType" runat="server" Width="100px" /></td>
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
                                <asp:BoundField DataField="CurriculumMarkTypeName" HeaderText="Nama" />
                                <asp:BoundField DataField="TaskMarkType" HeaderText="Format Nilai Tugas" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="FinalMarkType" HeaderText="Format Nilai Akhir" HeaderStyle-Width="150px" />
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("CurriculumMarkTypeID") %>" bindingfield="CurriculumMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("CurriculumMarkTypeName") %>" bindingfield="CurriculumMarkTypeName" />
                                        <input type="hidden" value="<%#Eval("GCTaskMarkType") %>" bindingfield="GCTaskMarkType" />
                                        <input type="hidden" value="<%#Eval("GCFinalMarkType") %>" bindingfield="GCFinalMarkType" />
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