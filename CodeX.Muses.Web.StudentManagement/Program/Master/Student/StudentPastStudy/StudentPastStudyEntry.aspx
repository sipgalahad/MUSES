<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentPastStudyEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentPastStudyEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                cboSchoolType.SetValue('');
                $('#<%=txtSchoolName.ClientID %>').val('');
                $('#<%=txtStartYear.ClientID %>').val('');
                $('#<%=txtEndYear.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
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
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodSectionID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.StudentPastStudyID);
            cboSchoolType.SetValue(entity.GCSchoolType);
            $('#<%=txtSchoolName.ClientID %>').val(entity.SchoolName);
            $('#<%=txtStartYear.ClientID %>').val(entity.StartYear);
            $('#<%=txtEndYear.ClientID %>').val(entity.EndYear);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
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
    </script>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 150px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Tipe Sekolah")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboSchoolType" ClientInstanceName="cboSchoolType" Width="200px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Nama Sekolah")%></label></td>
                                    <td><asp:TextBox ID="txtSchoolName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Mulai")%></label></td>
                                    <td><asp:TextBox ID="txtStartYear" CssClass="number" Width="80px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Selesai")%></label></td>
                                    <td><asp:TextBox ID="txtEndYear" CssClass="number" Width="80px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
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
                                <asp:BoundField DataField="StudentPastStudyID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="SchoolType" HeaderText="Tipe Sekolah" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="SchoolName" HeaderText="Nama Sekolah" HeaderStyle-Width="200px"/>
                                <asp:BoundField DataField="StartYear" HeaderText="Tanggal Mulai" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="EndYear" HeaderText="Tanggal Selesai" HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="10px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan"/>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("StudentPastStudyID") %>" bindingfield="StudentPastStudyID" />
                                        <input type="hidden" value="<%#Eval("GCSchoolType") %>" bindingfield="GCSchoolType" />
                                        <input type="hidden" value="<%#Eval("SchoolName") %>" bindingfield="SchoolName" />
                                        <input type="hidden" value="<%#Eval("StartYear") %>" bindingfield="StartYear" />
                                        <input type="hidden" value="<%#Eval("EndYear") %>" bindingfield="EndYear" />
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