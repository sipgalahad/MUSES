<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="HRScheduleGroupEntry.aspx.cs" Inherits="CodeX.Muses.Web.Inventory.Program.HRScheduleGroupEntry" %>

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
    <input type="hidden" id="hdnRowCountPerPage2" runat="server" value="" />
    <input type="hidden" id="hdnRecordFilterExpression" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            $('#ulTabEmployeePosition li').click(function () {
                $('#ulTabEmployeePosition li.selected').removeAttr('class');
                $('.containerTransDt').filter(':visible').hide();
                $contentID = $(this).attr('contentid');
                $('#' + $contentID).show();
                $(this).addClass('selected');
                lastContentID = $contentID;
            });

            if ($('#<%=hdnIsEditable.ClientID %>').val() == '1') {
                $('#divTransactionAdd').show();
                $('#divQuickPicks').show();
            }
            else {
                $('#divTransactionAdd').hide();
                $('#divQuickPicks').hide();
            }

            setDatePicker('<%=txtStartEffectiveDate.ClientID %>');
            $('#<%=txtStartEffectiveDate.ClientID %>').datepicker('option', 'minDate', '0');
            setDatePicker('<%=txtTransactionDate.ClientID %>');
            $('#<%=txtTransactionDate.ClientID %>').datepicker('option', 'maxDate', '0');


            $('.lnkDetail a').live('click', function () {
                var id = $(this).closest('tr').find('.keyField').html();
                //alert(id);
                var url = ResolveUrl("~/Program/Master/UpdateEmployeePosition/UpdateEmployeePositionEntryCtl.ascx");
                openUserControlPopup(url, id, 'Renumeration Formula', 600, 500);
            });

            $("#<%=rblDate.ClientID %> input").change(function () {
                var value = $(this).val();
                if (value == 1) {
                    $("#trDay").attr('style', 'display:none');
                    $("#trDay").removeAttr('style');
                    $("#trDate").attr('style', 'display:none');
                }
                else {
                    $("#trDate").attr('style', 'display:none');
                    $("#trDate").removeAttr('style');
                    $("#trDay").attr('style', 'display:none');
                }
            });

            //#region Transaction No
            function onGetEmployeePositionFilterExpression() {
                var filterExpression = "<%=GetFilterExpression() %>";
                return filterExpression;
            }

            $('#lblTransactionNo.lblLink').click(function () {
                openSearchDialog('transemployeepositionhd', onGetEmployeePositionFilterExpression(), function (value) {
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

            $('#divTransactionAdd').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    editedLineAmount = 0;
                    tacEmployeeID.setValue('');
                    tacEmployeeID.setText('');
                    $('#entryDetailContainer').show();
                }
            });

            $('#divTemplatePicks').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                    //showLoadingPanel();
                    id = "HR";
                    var url = ResolveUrl('~/Program/Master/HRScheduleGroup/TemplateEmployeeGroupPicksCtl.ascx');
                    var transactionID = $('#<%=hdnTransRenumerationID.ClientID %>').val();
                    openUserControlPopup(url, id, 'Template Picks', 1000, 600);
                }
            });

            $('#divTransactionAdd2').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    editedLineAmount = 0;

                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $("#<%=rblDate.ClientID %> input").prop('checked', false);
                    $input = $("#<%=rblDate.ClientID %> input[value=1]");
                    $input.prop('checked', true);
                    $input.change();

                    setDatePicker('<%=txtScheduleDate.ClientID %>');
                    $('#<%=txtScheduleDate.ClientID %>').datepicker('option', 'minDate', '0');

                    cboDailySchedule.SetSelectedIndex(0);
                    $('#entryDetailContainer2').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    cbpProcess.PerformCallback('save');
                }
            });

            $('#btnCancel2').click(function () {
                $('#entryDetailContainer2').hide();
            });

            $('#btnSave2').click(function (evt) {
                if (IsValid(evt, 'fsTrx2', 'mpTrx'))
                    cbpProcess2.PerformCallback('save');
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            var rowCount = parseInt($('#<%=hdnRowCount.ClientID %>').val());
            var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });

            var pageCount2 = parseInt($('#<%=hdnPageCount2.ClientID %>').val());
            var rowCount2 = parseInt($('#<%=hdnRowCount2.ClientID %>').val());
            var rowCountPerPage2 = parseInt($('#<%=hdnRowCountPerPage2.ClientID %>').val());
            setNumEntriesText($('#informationNumEntries2'), rowCount2, 1, rowCountPerPage2);
            setPaging($("#paging2"), pageCount2, function (page) {
                cbpView2.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries2'), rowCount2, page, rowCountPerPage2);
            });
        }
        //#region  Delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    tacEmployeeID.setValue(entity.EmployeeID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        //#region detil2 Edit & Delete
        $('#<%=grdView2.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.TransactionDtID);
                    cbpProcess2.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView2.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.TransactionDtID);
            setDatePicker('<%=txtScheduleDate.ClientID %>');
            $('#<%=txtScheduleDate.ClientID %>').val(entity.ScheduleDateInDatePickerFormat);
            cboGCDay.SetValue(entity.GCDay);
            cboDailySchedule.SetValue(entity.DailyScheduleID);
            
            $input = null;
            if (entity.GCDay != '') 
                $input = $("#<%=rblDate.ClientID %> input[value=1]");
            else 
                $input = $("#<%=rblDate.ClientID %> input[value=2]");
            $input.prop('checked', true);
            $input.change();

            cboDailySchedule.SetValue(entity.DailyScheduleID);
            $('#entryDetailContainer2').show();
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

        function onCbpViewEndCallback2(s) {
            hideLoadingPanel();
            var param = s.cpResult2.split('|');
            if (param[0] == 'refresh') {
                var pageCount2 = parseInt(param[1]);
                var rowCount2 = parseInt(param[2]);

                var rowCountPerPage = parseInt($('#<%=hdnRowCountPerPage.ClientID %>').val());
                setNumEntriesText($('#informationNumEntries2'), rowCount2, 1, rowCountPerPage2);
                setPaging($("#paging2"), pageCount2, function (page) {
                    cbpView2.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries2'), rowCount2, page, rowCountPerPage2);
                });

            }
        }
        //#endregion

        //#region details
        function onAfterSaveRecordDtSuccess(TransactionID) {
            if ($('#<%=hdnTransactionID.ClientID %>').val() == '0') {
                $('#<%=hdnTransactionID.ClientID %>').val(TransactionID);
                var filterExpression = 'TransactionID = ' + TransactionID;
                Methods.getObject('GetTransEmployeePositionHdList', filterExpression, function (result) {
                    $('#<%=txtTransactionNo.ClientID %>').val(result.TransactionNo);
                    cbpView.PerformCallback('refresh');
                });
                onAfterCustomSaveSuccess();
            }
            else
                cbpView.PerformCallback('refresh');
        }

        function onAfterSaveAddRecordEntryPopup(param) {
            onAfterSaveRecordDtSuccess(param);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail') 
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    onAfterSaveRecordDtSuccess(s.cpTransactionID);
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
        //#endregion

        //#region details2
       
        function onCbpProcesEndCallback2(s) {
            hideLoadingPanel();
            var param = s.cpResult2.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    onAfterSaveRecordDtSuccess(s.cpTransactionID2);
                    $('#divTransactionAdd2').click();
                    cbpView2.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView2.PerformCallback('refresh');
            }
        }
        //#endregion

        function onBeforeRightPanelPrint(code, filterExpression, errMessage) {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var printStatus = $('#<%=hdnPrintStatus.ClientID %>').val();
            if (printStatus == 'true') {
                if (TransactionID == '' || TransactionID == '0') {
                    errMessage.text = 'Please Set Transaction First!';
                    return false;
                }
                else {
                    filterExpression.text = "TransactionID = " + TransactionID;
                    return true;
                }
            } else {
                errMessage.text = "Data Doesn't Approved or Closed";
                return false;
            }
        }

        //#region Employee
        function onGetEmployeeFilterExpression() {
            var TransactionID = $('#<%=hdnTransactionID.ClientID %>').val();
            var filterExpression = "<%=GetFilterEmployeeExpression() %>"
            filterExpression += "IsDeleted = 0 AND EmployeeID NOT IN (SELECT EmployeeID FROM HRScheduleGroupEmployee where TransactionID = " + TransactionID + ")";
            return filterExpression;
        }

        function onTacEmployeeIDSearchClick() {
            openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
                var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
                Methods.getObject('GetEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        tacEmployeeID.setValue(result.EmployeeID);
                        tacEmployeeID.setText(result.FullName);
                    }
                    else {
                        tacEmployeeID.setValue('');
                        tacEmployeeID.setText('');
                    }
                });
            });

        }

        function onTacEmployeeIDValueChanged() {
        }
        //#endregion
    </script>    
    <input type="hidden" value="" id="hdnPrintStatus" runat="server" />
    <input type="hidden" value="" id="hdnTransactionID" runat="server" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <input type="hidden" value="" id="hdnPageCount2" runat="server" />
    <input type="hidden" value="" id="hdnRowCount" runat="server" />
    <input type="hidden" value="" id="hdnRowCount2" runat="server" />
    <input type="hidden" value="1" id="hdnIsEditable" runat="server" />
    <input type="hidden" value="" id="hdnTransRenumerationID" runat="server" />

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
                            <td class="tdLabel"><label class="lblLink" id="lblTransactionNo" ><%=GetLabel("No. Transaksi")%></label></td>
                            <td><asp:TextBox ID="txtTransactionNo" Width="150px"  runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Transaksi")%></td>
                            <td><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal Dimulai")%></td>
                            <td><asp:TextBox ID="txtStartEffectiveDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                       <tr>
                            <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="containerUlTabPage">
                        <ul class="ulTabPage" id="ulTabEmployeePosition">
                            <li class="selected" contentid="containerEmployee"><%=GetLabel("Karyawan") %></li>
                            <li contentid="containerRenumeration"><%=GetLabel("Jadwal") %></li>
                        </ul>
                    </div>
                    <div id="containerEmployee" class="containerTransDt">    
                        <div class="divTransactionEntry">
                            <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                            <span id="divTemplatePicks" class="divAdd" style="margin-left: 50px;"><%=GetLabel("Template Picks")%></span>
                            <br />
                            <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                                <fieldset id="fsTrx" style="margin: 0">
                                    <table style="width: 100%">
                                        <colgroup>
                                            <col style="width: 50%" />
                                        </colgroup>
                                        <tr>
                                            <td valign="top">
                                                <table style="width: 100%">
                                                    <colgroup>
                                                        <col style="width: 150px" />
                                                    </colgroup>
                                                    <tr>
                                                        <td class="tdLabel"><label class="lblMandatory" id="lblEmployee"><%=GetLabel("Karyawan")%></label></td>
                                                        <td>
                                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacEmployeeID" ClientInstanceName="tacEmployeeID" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                                                SearchFields="EmployeeName,EmployeeID" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                                                <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeIDSearchClick(); }"
                                                                    ValueChanged="function(){ onTacEmployeeIDValueChanged(); }" />
                                                            </cdx:CodeXAutoCompleteTextBox>   
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td> 
                                                <input type="button" id="btnSave" class="btnWhite" value='<%=GetLabel("Commit") %>'/>
                                                <input type="button" id="btnCancel" class="btnWhite" value='<%=GetLabel("Cancel") %>'/>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </div>
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                        position: relative;">
                                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Nama" />
                                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style='float:right;<%#IsEditable().ToString() == "False" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                        <input type="hidden" value="<%#Eval("EmployeeID") %>" bindingfield="EmployeeID" />
                                                        <input type="hidden" value="<%#Eval("EmployeeName") %>" bindingfield="EmployeeName" />
                                                        <input type="hidden" value="<%#Eval("EmployeeCode") %>" bindingfield="EmployeeCode" />
                                                        <input type="hidden" value="<%#Eval("TransactionID") %>" bindingfield="TransactionID" />
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
                    <div id="containerRenumeration" style="display:none" class="containerTransDt">  
                        <div class="divTransactionEntry">
                            <span id="divTransactionAdd2" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                            <br />
                            <div id="entryDetailContainer2" class="entryDetailContainer" style="display: none">                                
                                <fieldset id="fsTrx2" style="margin: 0">
                                    <input type="hidden" value="" id="hdnEntryID" runat="server" />
                                    <table style="width: 100%">
                                        <colgroup>
                                            <col style="width: 50%" />
                                        </colgroup>
                                        <tr>
                                            <td valign="top">
                                                <table style="width: 100%">
                                                    <colgroup>
                                                        <col style="width: 150px" />
                                                    </colgroup>
                                                    <tr>
                                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jadwal")%></label></td>
                                                        <td><dxe:ASPxComboBox runat="server" ID="cboDailySchedule" ClientInstanceName="cboDailySchedule" Width="300px"></dxe:ASPxComboBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rblDate" CssClass="rblDate" runat="server"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Hari" Value="1" />
                                                                <asp:ListItem Text="Tanggal" Value="2" />
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trDay" style="display:none;">
                                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hari")%></label></td>
                                                        <td><dxe:ASPxComboBox runat="server" ID="cboGCDay" ClientInstanceName="cboGCDay" Width="300px"></dxe:ASPxComboBox></td>
                                                    </tr>
                                                    <tr id="trDate" style="display:none;">
                                                        <td class="tdLabel"><%=GetLabel("Tanggal")%></td>
                                                        <td><asp:TextBox ID="txtScheduleDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td> 
                                                <input type="button" id="btnSave2" class="btnWhite" value='<%=GetLabel("Commit") %>'/>
                                                <input type="button" id="btnCancel2" class="btnWhite" value='<%=GetLabel("Cancel") %>'/>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </div>
                        <dxcp:ASPxCallbackPanel ID="cbpView2" runat="server" Width="100%" ClientInstanceName="cbpView2"
                        ShowLoadingPanel="false" OnCallback="cbpView2_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ onCbpViewEndCallback2(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server">
                                    <asp:Panel runat="server" ID="pnlView2" Style="width: 100%; margin-left: auto; margin-right: auto;
                                        position: relative;">
                                        <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult"
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="TransactionDtID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="Day" HeaderText="Hari" HeaderStyle-Width="150px" />
                                                <asp:BoundField DataField="ScheduleDateInString" HeaderText="Tanggal" HeaderStyle-Width="150px"/>
                                                <asp:BoundField DataField="DailyScheduleName" HeaderText="Jadwal" />
                                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style='float:right;<%=IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailDelete"></div>
                                                        <div style='float:right;margin-right:10px;<%#IsEditable().ToString() == "0" ? "display:none" : "" %>' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                        <input type="hidden" value="<%#Eval("TransactionDtID") %>" bindingfield="TransactionDtID" />
                                                        <input type="hidden" value="<%#Eval("GCDay") %>" bindingfield="GCDay" />
                                                        <input type="hidden" value="<%#Eval("ScheduleDateInDatePickerFormat") %>" bindingfield="ScheduleDateInDatePickerFormat" />
                                                        <input type="hidden" value="<%#Eval("DailyScheduleID") %>" bindingfield="DailyScheduleID" />
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
                            <div class="divInformationNumEntries" id="informationNumEntries2"></div>
                            <div class="wrapperPaging">
                                <div id="paging2">
                                </div>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess2" runat="server" Width="100%" ClientInstanceName="cbpProcess2"
        ShowLoadingPanel="false" OnCallback="cbpProcess2_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback2(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
