<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPCurriculumPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="CurriculumSubjectEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CurriculumSubjectEntry" %>

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
                tacSubject.setValue('');
                tacSubject.setText('');

                $('#<%=hdnLstClassTypeID.ClientID %>').val('');
                ddeClassType.SetText('');
                $('.chkClassType input:checked').each(function () {
                    $(this).prop('checked', false);
                });

                $('#<%=hdnLstMarkTypeID.ClientID %>').val('');
                ddeMarkType.SetText('');
                $('.chkMarkType input:checked').each(function () {
                    $(this).prop('checked', false);
                });

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
                    $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumSubjectID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.CurriculumSubjectID);
            tacSubject.setValue(entity.SubjectID);
            tacSubject.setText(entity.SubjectName);

            $('.chkClassType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            var lstClassTypeID = entity.ListCurriculumClassTypeID.split(',');
            for (var i = 0; i < lstClassTypeID.length; ++i) {
                $('.chkClassType').each(function () {
                    if ($(this).attr('classtypeid') == lstClassTypeID[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeClassTypeText();

            $('.chkMarkType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            var lstMarkTypeID = entity.ListCurriculumMarkTypeID.split(',');
            for (var i = 0; i < lstMarkTypeID.length; ++i) {
                $('.chkMarkType').each(function () {
                    if ($(this).attr('marktypeid') == lstMarkTypeID[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeMarkTypeText();

            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Subject
        function onGetSubjectFilterExpression() {
            var filterExpression = "<%=OnGetSubjectFilterExpression() %>";
            return filterExpression;
        }

        function onTacSubjectButtonSearchClick() {
            openSearchDialog('subject', onGetSubjectFilterExpression(), function (value) {
                var filterExpression = onGetSubjectFilterExpression() + " AND SubjectCode = '" + value + "'";
                Methods.getObject('GetSubjectList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubject.setValue(result.SubjectID);
                        tacSubject.setText(result.SubjectName);
                    }
                    else {
                        tacSubject.setValue('');
                        tacSubject.setText('');
                    }
                });
            });

        }

        function onTacSubjectValueChanged() {
        }
        //#endregion

        //#region Class Type
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
        //#endregion

        //#region Mark Type
        $('.chkMarkType input').live('change', function () {
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
    <input type="hidden" id="hdnGCClassStudyType" value="" runat="server" />
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    <input type="hidden" id="hdnLstMarkTypeID" value="" runat="server" />
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
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mata Pelajaran")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubject" ClientInstanceName="tacSubject" MethodName="GetvSubjectClassTypeList" GetFilterExpressionFunction="onGetSubjectFilterExpression"
                                            SearchFields="SubjectName,SubjectCode" TextField="SubjectName" ValueField="SubjectID" SearchText="${SubjectName} (<b>${SubjectCode}</b>)" OrderByExpression="SubjectName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacSubjectValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                                    <td colspan="5">
                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeClassType" ID="ddeClassType"
                                            Width="300px" runat="server" EnableAnimation="False">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkClassType" CssClass="chkClassType" runat="server"  /> <%#Eval("CurriculumClassTypeName")%><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </DropDownWindowTemplate>
                                        </dxe:ASPxDropDownEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Nilai")%></label></td>
                                    <td colspan="5">
                                        <dxe:ASPxDropDownEdit ClientInstanceName="ddeMarkType" ID="ddeMarkType"
                                            Width="300px" runat="server" EnableAnimation="False">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <asp:Repeater ID="rptMarkType" runat="server" OnItemDataBound="rptMarkType_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMarkType" CssClass="chkMarkType" runat="server"  /> <%#Eval("CurriculumMarkTypeName")%><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </DropDownWindowTemplate>
                                        </dxe:ASPxDropDownEdit>
                                    </td>
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
                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" />
                                <asp:BoundField DataField="ListCurriculumClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="460px" />
                                <asp:BoundField DataField="ListCurriculumMarkTypeName" HeaderText="Tipe Nilai" HeaderStyle-Width="250px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("CurriculumSubjectID") %>" bindingfield="CurriculumSubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectID") %>" bindingfield="SubjectID" />
                                        <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                        <input type="hidden" value="<%#Eval("ListCurriculumClassTypeID") %>" bindingfield="ListCurriculumClassTypeID" />
                                        <input type="hidden" value="<%#Eval("ListCurriculumClassTypeName") %>" bindingfield="ListCurriculumClassTypeName" />
                                        <input type="hidden" value="<%#Eval("ListCurriculumMarkTypeID") %>" bindingfield="ListCurriculumMarkTypeID" />
                                        <input type="hidden" value="<%#Eval("ListCurriculumMarkTypeName") %>" bindingfield="ListCurriculumMarkTypeName" />
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