<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="OrganizationEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.OrganizationEntry" %>

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
                $('#<%=txtOrganizationCode.ClientID %>').val('');
                $('#<%=txtOrganizationName.ClientID %>').val('');
                $('#<%=chkIsAllStudentAsMember.ClientID %>').prop('checked', false);
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
                    $('#<%=hdnEntryID.ClientID %>').val(entity.OrganizationID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.OrganizationID);
            $('#<%=txtOrganizationCode.ClientID %>').val(entity.OrganizationCode);
            $('#<%=txtOrganizationName.ClientID %>').val(entity.OrganizationName);
            $('#<%=chkIsAllStudentAsMember.ClientID %>').prop('checked', entity.IsAllStudentAsMember == "True");
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

        $('.lblDetail').live('click', function () {
            var url = ResolveUrl('~/Program/Master/SchoolPeriod/Organization/OrganizationDtEntryCtl.ascx');
            var id = $(this).closest('tr').find('.keyField').html();
            openUserControlPopup(url, id, 'Kepengurusan', 900, 500);
        });
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
                                    <col style="width: 200px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Kode")%></label></td>
                                    <td><asp:TextBox ID="txtOrganizationCode" Width="100px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtOrganizationName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Semua Siswa Sbg Anggota")%></label></td>
                                    <td><asp:CheckBox ID="chkIsAllStudentAsMember" runat="server" /></td>
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
                                <asp:BoundField DataField="OrganizationID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="OrganizationCode" HeaderText="Kode" HeaderStyle-Width="100px"/>
                                <asp:BoundField DataField="OrganizationName" HeaderText="Nama" HeaderStyle-Width="200px"/>
                                <asp:CheckBoxField DataField="IsAllStudentAsMember" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderText="Semua Siswa Sbg Anggota" HeaderStyle-Width="200px"/>
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan"/>                                
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Kepengurusan" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="lblDetail lblLink"><%=GetLabel("Kepengurusan") %></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("OrganizationID") %>" bindingfield="OrganizationID" />
                                        <input type="hidden" value="<%#Eval("OrganizationCode") %>" bindingfield="OrganizationCode" />
                                        <input type="hidden" value="<%#Eval("OrganizationName") %>" bindingfield="OrganizationName" />
                                        <input type="hidden" value="<%#Eval("IsAllStudentAsMember") %>" bindingfield="IsAllStudentAsMember" />
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