<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCurriculumPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="CurriculumSyllabusEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumSyllabusEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtCurriculumSyllabusName.ClientID %>').val('');
                cboCurriculumSyllabusType.SetValue('');
                $('#<%=txtDisplayOrder.ClientID %>').val('');
                tacParent.setValue('');
                tacParent.setText('');
                tacReference.setValue('');
                tacReference.setText('');
                $('#<%=chkIsHeader.ClientID %>').prop('checked', false);
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

        //#region Parent
        function onGetParentFilterExpression() {
            var filterExpression = "<%=OnGetParentFilterExpression() %>";
            return filterExpression;
        }

        function onTacParentButtonSearchClick() {
            openSearchDialog('curriculumsyllabus', onGetParentFilterExpression(), function (value) {
                var filterExpression = onGetParentFilterExpression() + " AND CurriculumSyllabusID = '" + value + "'";
                Methods.getObject('GetCurriculumSyllabusList', filterExpression, function (result) {
                    if (result != null) {
                        tacParent.setValue(result.CurriculumSyllabusID);
                        tacParent.setText(result.CurriculumSyllabusName);
                    }
                    else {
                        tacParent.setValue('');
                        tacParent.setText('');
                    }
                });
            });

        }

        function onTacParentValueChanged() {
        }
        //#endregion

        //#region Reference
        function onGetReferenceFilterExpression() {
            var filterExpression = "<%=OnGetReferenceFilterExpression() %>";
            return filterExpression;
        }

        function onTacReferenceButtonSearchClick() {
            openSearchDialog('curriculumsyllabus', onGetReferenceFilterExpression(), function (value) {
                var filterExpression = onGetReferenceFilterExpression() + " AND CurriculumSyllabusID = '" + value + "'";
                Methods.getObject('GetCurriculumSyllabusList', filterExpression, function (result) {
                    if (result != null) {
                        tacReference.setValue(result.CurriculumSyllabusID);
                        tacReference.setText(result.CurriculumSyllabusName);
                    }
                    else {
                        tacReference.setValue('');
                        tacReference.setText('');
                    }
                });
            });

        }

        function onTacReferenceValueChanged() {
        }
        //#endregion

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumSyllabusID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumSyllabusID);
            $('#<%=txtCurriculumSyllabusName.ClientID %>').val(entity.CurriculumSyllabusName);
            cboCurriculumSyllabusType.SetValue(entity.GCCurriculumSyllabusType);
            $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
            tacParent.setValue(entity.ParentID);
            tacParent.setText(entity.ParentName);
            tacReference.setValue(entity.ReferenceID);
            tacReference.setText(entity.ReferenceName);
            $('#<%=chkIsHeader.ClientID %>').prop('checked', entity.IsHeader == 'True');

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
                                    <td><asp:TextBox ID="txtCurriculumSyllabusName" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboCurriculumSyllabusType" ClientInstanceName="cboCurriculumSyllabusType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Induk")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacParent" ClientInstanceName="tacParent" MethodName="GetCurriculumSyllabusList" GetFilterExpressionFunction="onGetParentFilterExpression"
                                            SearchFields="CurriculumSyllabusName" TextField="CurriculumSyllabusName" ValueField="CurriculumSyllabusID" SearchText="${CurriculumSyllabusName}" OrderByExpression="CurriculumSyllabusName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacParentButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacParentValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Reference")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacReference" ClientInstanceName="tacReference" MethodName="GetCurriculumSyllabusList" GetFilterExpressionFunction="onGetReferenceFilterExpression"
                                            SearchFields="CurriculumSyllabusName" TextField="CurriculumSyllabusName" ValueField="CurriculumSyllabusID" SearchText="${CurriculumSyllabusName}" OrderByExpression="CurriculumSyllabusName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacReferenceButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacReferenceValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan")%></label></td>
                                    <td><asp:TextBox ID="txtDisplayOrder" CssClass="number" runat="server" Width="80px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Induk")%></label></td>
                                    <td><asp:CheckBox ID="chkIsHeader" runat="server" /></td>
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
                                <asp:TemplateField HeaderStyle-Width="200px" >
                                    <HeaderTemplate>
                                        <div style="padding-left:3px">
                                            <%=GetLabel("Nama")%>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style='margin-left:<%# Eval("Level") %>0px;'><%# Eval("CurriculumSyllabusName") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CurriculumSyllabusType" HeaderText="Tipe" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("CurriculumSyllabusID") %>" bindingfield="CurriculumSyllabusID" />
                                        <input type="hidden" value="<%#Eval("CurriculumSyllabusName") %>" bindingfield="CurriculumSyllabusName" />
                                        <input type="hidden" value="<%#Eval("ParentID") %>" bindingfield="ParentID" />
                                        <input type="hidden" value="<%#Eval("ParentName") %>" bindingfield="ParentName" />
                                        <input type="hidden" value="<%#Eval("ReferenceID") %>" bindingfield="ReferenceID" />
                                        <input type="hidden" value="<%#Eval("ReferenceName") %>" bindingfield="ReferenceName" />
                                        <input type="hidden" value="<%#Eval("GCCurriculumSyllabusType") %>" bindingfield="GCCurriculumSyllabusType" />
                                        <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                        <input type="hidden" value="<%#Eval("IsHeader") %>" bindingfield="IsHeader" />
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