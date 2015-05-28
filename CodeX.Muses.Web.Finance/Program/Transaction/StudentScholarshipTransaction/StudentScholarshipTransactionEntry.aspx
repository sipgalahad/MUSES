<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentScholarshipTransactionEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.StudentScholarshipTransactionEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhHeader" runat="server">
    <input type="hidden" id="hdnRowCountPerPage" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1')
                $('#divTransactionAdd').show();
            else
                $('#divTransactionAdd').hide();

            setDatePicker('<%=txtTransactionDate.ClientID %>');
            $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtStartingDate.ClientID %>'); 

            //#region Transaction No
            function onGetStudentScholarshipTransactionFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblTransactionNo.lblLink').click(function () {
                var filterExpression = onGetStudentScholarshipTransactionFilterExpression();
                openSearchDialog('studentscholarshiptransactionhd', filterExpression, function (value) {
                    $('#<%=txtTransactionNo.ClientID %>').val(value);
                    onTxtTransactionNoChanged(value);
                });
            });

            $('#<%=txtTransactionNo.ClientID %>').change(function () {
                onTxtTransactionNoChanged($(this).val());
            });

            function onTxtTransactionNoChanged(value) {
                onLoadObject(value);
            }
            //#endregion

            $('#btnScholarshipDt').click(function () {
                var id = tacScholarship.getValue();
                if (id != '') {
                    var url = ResolveUrl("~/Program/Transaction/StudentScholarshipTransaction/ScholarshipDtViewCtl.ascx");
                    openUserControlPopup(url, id, 'Detail', 1250, 500);
                }
            });

            $('#divEntryDtAdd').click(function () {
                $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
                $newTr = $newTr.replace(/\$\{idx}/g, idxStudent);
                $newTr = $($newTr);
                $newTr.insertBefore($('#trSaveEntryPopup'));

                var tempHelper = new CodeXClientAutoCompleteHelper();
                tempHelper.init("Student" + idxStudent, "StudentCode,StudentName", "GetStudentList", "", "onGetStudentFilterExpression", "StudentID");
                tempHelper.setClientSideEvents(onStudentIDValueChanged);
                tempHelper.initializeControl();
                idxStudent++;
            });

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    tacScholarship.setEnabled(true);
                    tacScholarship.setValue('');
                    tacScholarship.setText('');
                    $('#<%=hdnScholarshipID.ClientID %>').val('');

                    idxStudent = 0;
                    $('.trStudentDt').each(function () {
                        $(this).remove();
                    });

                    $('#entryDetailContainer').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    var result = '';
                    $('.tacStudent').each(function () {
                        if (result != "")
                            result += ',';
                        result += $(this).find('.hdnAutoCompleteValue').val();
                    });
                    $('#<%=hdnStudentSave.ClientID %>').val(result);
                    cbpProcess.PerformCallback('save');
                }
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

        //#region Edit & Delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ScholarshipID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ScholarshipID);
            tacScholarship.setEnabled(false);
            tacScholarship.setValue(entity.ScholarshipID);
            tacScholarship.setText(entity.ScholarshipName);
            $('#<%=hdnScholarshipID.ClientID %>').val(entity.ScholarshipID);

            idxStudent = 0;
            $('.trStudentDt').each(function () {
                $(this).remove();
            });

            var lstStudentID = entity.ListStudentID.split(',');
            var lstStudentName = entity.ListStudentName.split(', ');
            for (var i = 0; i < lstStudentID.length; ++i) {
                $('#divEntryDtAdd').click();

                $tr = $('.trStudentDt').last();
                $tacStudent = $tr.find('.tacStudent');
                $tacStudent.find('.hdnAutoCompleteValue').val(lstStudentID[i]);
                $tacStudent.find('.hdnAutoCompleteText').val(lstStudentName[i]);
                $tacStudent.find('.txtAutoComplete').val(lstStudentName[i]);
            }

            $('#entryDetailContainer').show();
        });

        //#endregion

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

        //#region Scholarship
        window.onGetScholarshipFilterExpression = function () {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

        function onTacScholarshipButtonSearchClick() {
            openSearchDialog('scholarship', onGetScholarshipFilterExpression(), function (value) {
                var filterExpression = onGetScholarshipFilterExpression() + " AND ScholarshipID = '" + value + "'";
                Methods.getObject('GetScholarshipList', filterExpression, function (result) {
                    if (result != null) {
                        tacScholarship.setValue(result.ScholarshipID);
                        tacScholarship.setText(result.ScholarshipName);
                        $('#<%=hdnScholarshipID.ClientID %>').val(result.ScholarshipID);
                    }
                    else {
                        tacScholarship.setValue('');
                        tacScholarship.setText('');
                        $('#<%=hdnScholarshipID.ClientID %>').val('');
                    }
                });
            });

        }

        function onTacScholarshipValueChanged() {
            var id = tacScholarship.getValue();
            $('#<%=hdnScholarshipID.ClientID %>').val(id);
        }
        //#endregion

        var idxStudent = 0;

        function onStudentIDValueChanged($s) {
            $tacTr = $s.closest('tr');
            if ($s.val() != '') {
                //var trIdx = $('.trJournalEntry').index($tacTr);
                //if (trIdx == $('.trJournalEntry').length - 1)
                //    addEntityRowPrescription();
            }
        }

        $('.divDeleteEntryDt').live('click', function () {
            $tr = $(this).closest('tr').parent().closest('tr');
            $tr.remove();
        });

        window.onGetStudentFilterExpression = function () {
            var filterExpression = "<%=OnGetStudentFilterExpression() %>";
            return filterExpression;
        }

        $('.tacStudent .btnAutoCompleteSearchMore').die('click');
        $('.tacStudent .btnAutoCompleteSearchMore').live('click', function () {
            $tacTr = $(this).closest('tr');
            openSearchDialog('student', onGetStudentFilterExpression(), function (value) {
                var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
                Methods.getObject('GetStudentList', filterExpression, function (result) {
                    $tacCOA = $tacTr.find('.tacStudent');
                    if (result != null) {
                        $tacCOA.find('.hdnAutoCompleteValue').val(result.StudentID);
                        $tacCOA.find('.hdnAutoCompleteText').val(result.StudentName);
                        $tacCOA.find('.txtAutoComplete').val(result.StudentName);
                    }
                    else {
                        $tacCOA.find('.hdnAutoCompleteValue').val('');
                        $tacCOA.find('.hdnAutoCompleteText').val('');
                        $tacCOA.find('.txtAutoComplete').val('');
                    }
                    onStudentIDValueChanged($tacCOA.find('.txtAutoComplete'));
                });
                //var trIdx = $('.trPrescriptionEntry').index($tacTr);
                //if (trIdx == $('.trPrescriptionEntry').length - 1)
                //    addEntityRowPrescription();
                $tacTr = null;
            });
        });

        function onAfterSaveRecordDtSuccess(TransactionID) {
            if ($('#<%=hdnTransactionID.ClientID %>').val() == '0') {
                $('#<%=hdnTransactionID.ClientID %>').val(TransactionID);
                var filterExpression = 'TransactionID = ' + TransactionID;
                Methods.getObject('GetStudentScholarshipTransactionHdList', filterExpression, function (result) {
                    $('#<%=txtTransactionNo.ClientID %>').val(result.TransactionNo);
                });
                onAfterCustomSaveSuccess();
            }
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    var adjustmentID = s.cpTransactionID;
                    onAfterSaveRecordDtSuccess(adjustmentID);
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

        $('.lnkScholarship a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Transaction/StudentScholarshipTransaction/ScholarshipDtViewCtl.ascx");
            openUserControlPopup(url, entity.ScholarshipID, 'Detail', 1250, 500);
        });
    </script>    
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" id="hdnStudentSave" value="" runat="server" />
    <script id="tmplEntityDt" type="text/x-jquery-tmpl">
        <tr class="trStudentDt">
            <td>&nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div id="Student${idx}" class="tacStudent">
                                <div>
                                    <div class="containerAutoComplete">
                                        <input type="hidden" class="hdnAutoCompleteValue"/>
                                        <input type="hidden" class="hdnAutoCompleteText"/>
                                        <input type="hidden" class="hdnIsRequired" value="1"/>
                                        <input type="hidden" class="hdnValidationGroup" value="mpDrugsQuickPicks"/>
                                        <input type="text" class="required txtAutoComplete" validationgroup="mpTrxPopup" style="width:145px"/>
                                        <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                                        <div class="divListAutoCompleteResultBox">
                                            <div class="divListAutoCompleteResult">
                                            </div>
                                        </div>
                                    </div>
                                    <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                        <div>
                                            ${StudentName} (<b>${StudentCode}</b>)
                                            <input type='hidden' value='${StudentName}' class='hdnAutoCompleteRowText'/>
                                            <input type='hidden' value='${StudentID}' class='hdnAutoCompleteRowValue'/>
                                        </div>
                                    </script1>
                                </div>
                            </div>
                        </td>
                        <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
                    </tr>
                </table>
            </td>
        </tr>
    </script>
    <div style="height: 550px; overflow-y: auto; overflow-x: hidden;">
        <table class="tblContentArea">
            <colgroup>
                <col style="width: 50%" />
                <col style="width: 50%" />
            </colgroup>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />  
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblTransactionNo"><%=GetLabel("No. Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionNo" Width="150px" ReadOnly="true" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Efektif")%></label></td>
                            <td><asp:TextBox ID="txtStartingDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 5px; vertical-align: top">
                    <table class="tblEntryContent" style="width: 100%">
                        <colgroup>
                            <col style="width: 30%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Referensi") %></label></td>
                            <td><asp:TextBox ID="txtReferenceNo" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="width: 120px; vertical-align:top; padding-top:5px; "><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="divTransactionEntry">
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrx" style="margin: 0">
                                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                                <table id="tblEntryPopup">
                                    <colgroup>
                                        <col style="width:150px"/>
                                        <col style="width:220px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Beasiswa")%></label></td>
                                        <td>
                                            <input type="hidden" id="hdnScholarshipID" value="" runat="server" />
                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacScholarship" ClientInstanceName="tacScholarship" MethodName="GetScholarshipList" GetFilterExpressionFunction="onGetScholarshipFilterExpression"
                                                SearchFields="ScholarshipName" TextField="ScholarshipName" ValueField="ScholarshipID" SearchText="${ScholarshipName}" OrderByExpression="ScholarshipName">
                                                <ClientSideEvents ButtonSearchClick="function(){ onTacScholarshipButtonSearchClick(); }"
                                                    ValueChanged="function(){ onTacScholarshipValueChanged(); }" />
                                            </cdx:CodeXAutoCompleteTextBox>   
                                        </td>
                                        <td><input type="button" id="btnScholarshipDt" class="btnMore" value="..." /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="2"><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Member")%></span><br /></td>
                                    </tr>
                                    <tr id="trSaveEntryPopup">
                                        <td> 
                                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:HyperLinkField DataTextField="ScholarshipName" HeaderText="Tipe Coverage" HeaderStyle-Width="200px" ItemStyle-CssClass="lnkScholarship" />
                                            <asp:BoundField DataField="ListStudentName" HeaderText="Member" />
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;<%# IsEditable() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;<%# IsEditable() == "0" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("ScholarshipID") %>" bindingfield="ScholarshipID" />
                                                    <input type="hidden" value="<%#Eval("ScholarshipName") %>" bindingfield="ScholarshipName" />
                                                    <input type="hidden" value="<%#Eval("ListStudentID") %>" bindingfield="ListStudentID" />
                                                    <input type="hidden" value="<%#Eval("ListStudentName") %>" bindingfield="ListStudentName" />
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
                </td>
            </tr>
        </table>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
