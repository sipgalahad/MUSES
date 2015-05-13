<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectCurriculumList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectCurriculumList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) 
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        var isEdit = false;
        var entity = null;
        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            isEdit = true;
            $row = $(this).closest('tr');
            entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectCurriculumID);
            cboCurriculum.SetValue(entity.CurriculumID);
            $('#<%=txtSubjectCurriculumName.ClientID %>').val(entity.SubjectCurriculumName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            $('.chkClassType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            cbpClassType.PerformCallback('refresh');
        });

        //#endregion

        function onCbpClassTypeEndCallback() {
            if (isEdit) {
                isEdit = false;

                var lstClassTypeID = entity.ListClassTypeID.split(',');
                for (var i = 0; i < lstClassTypeID.length; ++i) {
                    $('.chkClassType').each(function () {
                        if ($(this).attr('classtypeid') == lstClassTypeID[i])
                            $(this).find('input').prop('checked', true);
                    });
                }
                entity = null;
                setDdeClassTypeText();
                $('#entryDetailContainer').show();
            }
            hideLoadingPanel();
        }

        function onCboCurriculumValueChanged() {
            $('#<%=hdnLstClassTypeID.ClientID %>').val('');
            ddeClassType.SetText('');
            cbpClassType.PerformCallback('refresh');
        }

        $('.chkClassType input').live('change', function () {
            setDdeClassTypeText();
        });

        function setDdeClassTypeText() {
            var lstClassTypeID = '';
            var lstClassTypeName = '';
            $('.chkClassType input:checked').each(function () {
                if (lstClassTypeName != '') {
                    lstClassTypeName += ', ';
                    lstClassTypeID += ',';
                }
                lstClassTypeID += $(this).parent().attr('classtypeid');
                lstClassTypeName += $(this).parent().attr('classtypename');
            });
            $('#<%=hdnLstClassTypeID.ClientID %>').val(lstClassTypeID);
            ddeClassType.SetText(lstClassTypeName);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#entryDetailContainer').hide();
                    cbpView.PerformCallback('refresh');
                }
            }
        }

        function onCboFilterValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        $('.lnkFinalMarkDesc a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectCurriculum/SubjectCurriculumFinalMarkDescEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectCurriculumID, 'Deskripsi Rapor', 450, 350);
        });
    </script>
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    <div class="divTransactionEntry">
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
                                    <td><asp:TextBox ID="txtSubjectCurriculumName" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kurikulum")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cboCurriculum" ClientEnabled="false" ClientInstanceName="cboCurriculum" runat="server" Width="200px">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboCurriculumValueChanged(); }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                                    <td colspan="5">
                                        <dxcp:ASPxCallbackPanel ID="cbpClassType" runat="server" Width="100%" ClientInstanceName="cbpClassType"
                                            ShowLoadingPanel="false" OnCallback="cbpClassType_Callback">
                                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                                EndCallback="function(s,e){ onCbpClassTypeEndCallback(); }" />
                                            <PanelCollection>
                                                <dx:PanelContent ID="PanelContent2" runat="server">
                                                    <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto;
                                                        position: relative; font-size: 0.95em;">
                                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeClassType" ID="ddeClassType"
                                                            Width="300px" runat="server" EnableAnimation="False">
                                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                                            <DropDownWindowTemplate>
                                                                <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkClassType" CssClass="chkClassType" runat="server"  /> <%#Eval("CurriculumClassTypeName") %><br />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </DropDownWindowTemplate>
                                                        </dxe:ASPxDropDownEdit>
                                                    </asp:Panel>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
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
                                <asp:BoundField DataField="SubjectCurriculumName" HeaderText="Nama" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="CurriculumName" HeaderText="Kurikulum" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="ListClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" ItemStyle-CssClass="lnkFinalMarkDesc" HeaderText="Deskripsi Rapor" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <a <%# Eval("IsFinalMarkDesriptionPerSection").ToString() == "False" ? "style='display:none'" : ""%>>Deskripsi Rapor</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumID") %>" bindingfield="SubjectCurriculumID" />
                                        <input type="hidden" value="<%#Eval("SubjectCurriculumName") %>" bindingfield="SubjectCurriculumName" />
                                        <input type="hidden" value="<%#Eval("CurriculumID") %>" bindingfield="CurriculumID" />
                                        <input type="hidden" value="<%#Eval("CurriculumName") %>" bindingfield="CurriculumName" />
                                        <input type="hidden" value="<%#Eval("ListClassTypeID") %>" bindingfield="ListClassTypeID" />
                                        <input type="hidden" value="<%#Eval("ListClassTypeName") %>" bindingfield="ListClassTypeName" />
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