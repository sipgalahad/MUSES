<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPFAItemPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="FAItemMovementEntry.aspx.cs" Inherits="Codex.Muses.Web.AssetManagement.Program.FAItemMovementEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtMovementDate.ClientID %>');

            //#region Location
            function onGetLocationFilterExpression() {
                return "IsDeleted = 0";
            }

            $('#lblLocation.lblLink').click(function () {
                openSearchDialog('falocation', onGetLocationFilterExpression(), function (value) {
                    $('#<%=txtToLocationCode.ClientID %>').val(value);
                    ontxtToLocationCodeChanged(value);
                });
            });

            $('#<%=txtToLocationCode.ClientID %>').change(function () {
                ontxtToLocationCodeChanged($(this).val());
            });

            function ontxtToLocationCodeChanged(value) {
                var filterExpression = onGetLocationFilterExpression() + " AND FALocationCode = '" + value + "'";
                Methods.getObject('GetFALocationList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=txtToLocationName.ClientID %>').val(result.FALocationName);
                        $('#<%=hdnToLocationID.ClientID %>').val(result.FALocationID);
                    }
                    else {
                        $('#<%=txtToLocationCode.ClientID %>').val('');
                        $('#<%=txtToLocationName.ClientID %>').val('');
                        $('#<%=hdnToLocationID.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#divTransactionAdd').live('click', function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtMovementNo.ClientID %>').val('');
                $('#<%=txtMovementDate.ClientID %>').val('');
                $('#<%=hdnFromLocationID.ClientID %>').val('');
                $('#<%=hdnToLocationID.ClientID %>').val('');
                $('#<%=txtToLocationCode.ClientID %>').val('');
                $('#<%=txtToLocationName.ClientID %>').val('');
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

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        }

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
            $('#<%=hdnEntryID.ClientID %>').val(entity.MovementID);
            $('#<%=txtMovementNo.ClientID %>').val(entity.MovementNo);
            $('#<%=txtMovementDate.ClientID %>').val(entity.MovementDateInDatePickerFormat);
            $('#<%=hdnFromLocationID.ClientID %>').val(entity.FromFALocationID);
            $('#<%=hdnToLocationID.ClientID %>').val(entity.ToFALocationID);
            $('#<%=txtToLocationCode.ClientID %>').val(entity.ToLocationCode);
            $('#<%=txtToLocationName.ClientID %>').val(entity.ToLocationName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            $('#entryDetailContainer').show();
        });

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });

            }
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    //var OrderID = s.cpOrderID;
                    //onAfterSaveRecordDtSuccess(OrderID);
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
    <input type="hidden" id="hdnFromLocationID" runat="server" value="" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
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
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Pemindahan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtMovementNo" ReadOnly="true" Width="150px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
                                    <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtMovementDate" Width="120px" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblLocation"><%=GetLabel("Lokasi") %></label></td>
                                    <td>
                                        <input type="hidden" runat="server" id="hdnToLocationID" />
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:30%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox runat="server" ID="txtToLocationCode" Width="100%" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox runat="server" ID="txtToLocationName" ReadOnly="true" Width="100%" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="100%" /></td>
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
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="MovementID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="MovementDateInString" HeaderText="Tgl. Mutasi" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="FromLocationName" HeaderText="Dari Lokasi" />
                                <asp:BoundField DataField="ToLocationName" HeaderText="Kepada Lokasi" HeaderStyle-Width="250px" />
                                <asp:BoundField DataField="LastUpdatedByName" HeaderText="Petugas" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;<%#Eval("IsAllowEditItem").ToString() == "False" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("MovementID") %>" bindingfield="MovementID" />
                                    <input type="hidden" value="<%#Eval("MovementNo") %>" bindingfield="MovementNo" />
                                    <input type="hidden" value="<%#Eval("MovementDateInDatePickerFormat") %>" bindingfield="MovementDateInDatePickerFormat" />
                                    <input type="hidden" value="<%#Eval("FromFALocationID") %>" bindingfield="FromFALocationID" />
                                    <input type="hidden" value="<%#Eval("ToFALocationID") %>" bindingfield="ToFALocationID" />
                                    <input type="hidden" value="<%#Eval("ToLocationCode") %>" bindingfield="ToLocationCode" />
                                    <input type="hidden" value="<%#Eval("ToLocationName") %>" bindingfield="ToLocationName" />
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
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging">
                </div>
            </div>
        </div>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
    
</asp:Content>
