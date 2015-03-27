<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectMatterEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectMatterEntry" %>

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
                $('#<%=txtSubjectMatterCode.ClientID %>').val('');
                $('#<%=txtSubjectMatterName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');

                $('#<%=hdnLstClassTypeID.ClientID %>').val('');
                ddeClassType.SetText('');
                $('.chkClassType input:checked').each(function () {
                    $(this).prop('checked', false);
                });

                $('.txtSummaryName').each(function () {
                    $(this).val('');
                });

                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    getSaveValue();
                    cbpProcess.PerformCallback('save');
                }
            });
        });

        function getSaveValue() {
            var lstSaveValue = '';
            $('.txtSummaryName').each(function () {
                if (lstSaveValue != '')
                    lstSaveValue += '|';
                var summaryName = $(this).val();
                $tr = $(this).closest('tr');
                var GCPeriodSection = $tr.find('.hdnGCPeriodSection').val();
                lstSaveValue += GCPeriodSection + ';' + summaryName;
            });
            $('#<%=hdnLstPeriodSectionSummary.ClientID %>').val(lstSaveValue);
        }

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectMatterID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectMatterID);
            $('#<%=txtSubjectMatterCode.ClientID %>').val(entity.SubjectMatterCode);
            $('#<%=txtSubjectMatterName.ClientID %>').val(entity.SubjectMatterName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            $('.chkClassType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            var lstClassTypeID = entity.ListClassTypeID.split(',');
            for (var i = 0; i < lstClassTypeID.length; ++i) {
                $('.chkClassType').each(function () {
                    if ($(this).attr('classtypeid') == lstClassTypeID[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeClassTypeText();
            
            $('.txtSummaryName').each(function () {
                $(this).val('');
            });
            var filterExpression = 'SubjectMatterID = ' + entity.SubjectMatterID;
            Methods.getListObject('GetSubjectCompetencyStandardSummaryList', filterExpression, function (result) {
                for (var i = 0; i < result.length; ++i) {
                    $('.txtSummaryName').each(function () {
                        $tr = $(this).closest('tr');
                        var GCPeriodSection = $tr.find('.hdnGCPeriodSection').val();
                        if(GCPeriodSection == result[i].GCPeriodSection)
                            $(this).val(result[i].SummaryName);
                    });
                }
            });

            $('#entryDetailContainer').show();
        });

        //#endregion

        $('.chkClassType input').change(function () {
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

        function onCboFilterValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        $('.lnkCompetencyStandard a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectMatter/SubjectCompetencyStandardEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectMatterID, 'Standar Kompetensi', 800, 550);
        });

        $('.lnkSubjectMatterDt a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectMatter/SubjectMatterDtEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectMatterID, 'Detil Pertemuan', 800, 550);
        });
    </script>
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    <input type="hidden" id="hdnLstPeriodSectionSummary" value="" runat="server" />
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
                                    <td><asp:TextBox ID="txtSubjectMatterCode" runat="server" Width="100px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtSubjectMatterName" runat="server" Width="200px" /></td>
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
                                                        <asp:CheckBox ID="chkClassType" CssClass="chkClassType" runat="server"  /> <%#Eval("ClassTypeName") %><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </DropDownWindowTemplate>
                                        </dxe:ASPxDropDownEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><h4><%=GetLabel("Pokok Standar Kompetensi") %></h4></td>
                                </tr>
                                <asp:Repeater ID="rptPeriodSection" runat="server" OnItemDataBound="rptPeriodSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel">
                                                <input type="hidden" class="hdnGCPeriodSection" value='<%#Eval("StandardCodeID")%>' />
                                                <label class="lblMandatory"><%#Eval("StandardCodeName")%></label>
                                            </td>
                                            <td><asp:TextBox ID="txtSummaryName" CssClass="txtSummaryName" Width="100%" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
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
                                <asp:BoundField DataField="SubjectMatterCode" HeaderText="Kode" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="SubjectMatterName" HeaderText="Nama" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="ListClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="180px" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                <asp:HyperLinkField HeaderText="Standar Kompetensi" Text="Standar Kompetensi" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkCompetencyStandard" HeaderStyle-Width="160px" />
                                <asp:HyperLinkField HeaderText="Detil Pertemuan" Text="Detil Pertemuan" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkSubjectMatterDt" HeaderStyle-Width="120px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SubjectMatterID") %>" bindingfield="SubjectMatterID" />
                                        <input type="hidden" value="<%#Eval("SubjectMatterCode") %>" bindingfield="SubjectMatterCode" />
                                        <input type="hidden" value="<%#Eval("SubjectMatterName") %>" bindingfield="SubjectMatterName" />
                                        <input type="hidden" value="<%#Eval("CompetencyStandard") %>" bindingfield="CompetencyStandard" />
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