<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentFamilyDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentFamilyDtEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPFrame" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnNext').click(function () {
                parent.OnNextButtonClick();
            });

            $('#btnPrev').click(function () {
                parent.OnPrevButtonClick();
            });

            setDatePicker('<%=txtDOB.ClientID %>');

            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                cboFamilyRelation.SetValue('');
                cboTitle.SetValue('');
                $('#<%=txtFirstName.ClientID %>').val('');
                $('#<%=txtMiddleName.ClientID %>').val('');
                $('#<%=txtLastName.ClientID %>').val('');
                $('#<%=txtBirthPlace.ClientID %>').val('');
                $('#<%=txtDOB.ClientID %>').val('');
                cboSuffix.SetValue('');
                cboNationality.SetValue('');
                cboReligion.SetValue('');
                cboEducationLevel.SetValue('');
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
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodAdmissionID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.FamilyID);
            cboFamilyRelation.SetValue(entity.GCFamilyRelation);
            cboTitle.SetValue(entity.GCTitle);
            $('#<%=txtFirstName.ClientID %>').val(entity.FirstName);
            $('#<%=txtMiddleName.ClientID %>').val(entity.MiddleName);
            $('#<%=txtLastName.ClientID %>').val(entity.LastName);
            $('#<%=txtBirthPlace.ClientID %>').val(entity.CityOfBirth);
            $('#<%=txtDOB.ClientID %>').val(entity.DateOfBirthInDatePickerFormat); 
            cboSuffix.SetValue(entity.GCSuffix);
            cboNationality.SetValue(entity.GCNationality);
            cboReligion.SetValue(entity.GCReligion);
            cboEducationLevel.SetValue(entity.GCEducationLevel);
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
    <input type="hidden" runat="server" id="hdnID" />
    <div style="height: 410px; overflow-y:auto">
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
                                        <col style="width: 160px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hubungan Keluarga")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboFamilyRelation" ClientInstanceName="cboFamilyRelation" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboTitle" ClientInstanceName="cboTitle" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Depan")%></label></td>
                                        <td>
                                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col style="width:49%"/>
                                                    <col style="width:3px"/>
                                                    <col/>
                                                </colgroup>
                                                <tr>
                                                    <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                                    <td>&nbsp;</td>
                                                    <td><asp:TextBox ID="txtMiddleName" Width="100%" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Belakang")%></label></td>
                                        <td><asp:TextBox ID="txtLastName" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboSuffix" ClientInstanceName="cboSuffix" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <colgroup>
                                        <col style="width: 160px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tempat Lahir")%></label></td>
                                        <td><asp:TextBox ID="txtBirthPlace" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Lahir")%></label></td>
                                        <td><asp:TextBox ID="txtDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kewarganegaraan")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboNationality" ClientInstanceName="cboNationality" Width="120px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Agama")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboReligion" ClientInstanceName="cboReligion" Width="120px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendidikan Terakhir")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboEducationLevel" ClientInstanceName="cboEducationLevel" Width="120px" runat="server" /></td>
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
                                    <asp:BoundField DataField="FamilyID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                    <asp:BoundField DataField="FamilyRelation" HeaderText="Relasi" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="FamilyName" HeaderText="Nama"/>
                                    <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div style='float:right;' class="divDetailDelete"></div>
                                            <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                            <input type="hidden" value="<%#Eval("FamilyID") %>" bindingfield="FamilyID" />
                                            <input type="hidden" value="<%#Eval("GCFamilyRelation") %>" bindingfield="GCFamilyRelation" />
                                            <input type="hidden" value="<%#Eval("GCTitle") %>" bindingfield="GCTitle" />
                                            <input type="hidden" value="<%#Eval("FirstName") %>" bindingfield="FirstName" />
                                            <input type="hidden" value="<%#Eval("MiddleName") %>" bindingfield="MiddleName" />
                                            <input type="hidden" value="<%#Eval("LastName") %>" bindingfield="LastName" />
                                            <input type="hidden" value="<%#Eval("GCSuffix") %>" bindingfield="GCSuffix" />
                                            <input type="hidden" value="<%#Eval("GCNationality") %>" bindingfield="GCNationality" />
                                            <input type="hidden" value="<%#Eval("GCReligion") %>" bindingfield="GCReligion" />
                                            <input type="hidden" value="<%#Eval("GCEducationLevel") %>" bindingfield="GCEducationLevel" />
                                            <input type="hidden" value="<%#Eval("CityOfBirth") %>" bindingfield="CityOfBirth" />
                                            <input type="hidden" value="<%#Eval("DateOfBirthInDatePickerFormat") %>" bindingfield="DateOfBirthInDatePickerFormat" />
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
    </div>
    <br />
    <input type="button" id="btnNext" value="Next" style="float:right" />
    <input type="button" id="btnPrev" value="Prev" />
</asp:Content>