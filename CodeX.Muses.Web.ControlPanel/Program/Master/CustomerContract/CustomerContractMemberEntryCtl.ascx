<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerContractMemberEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.CustomerContractMemberEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            tacCoverageType.setValue('');
            tacCoverageType.setText('');
            $('#<%=hdnCoverageTypeID.ClientID %>').val('');

            idxStudent = 0;
            $('.trStudentDt').each(function () {
                $(this).remove();
            });

            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                var result = '';
                $('.tacStudent').each(function () {
                    if (result != "")
                        result += ',';
                    result += $(this).find('.hdnAutoCompleteValue').val();
                });
                $('#<%=hdnStudentSave.ClientID %>').val(result);
                cbpProcessPopup.PerformCallback('save');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                cboGrade.SetValue(entity.GCGrade);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.CoverageTypeID);
        tacCoverageType.setValue(entity.CoverageTypeID);
        tacCoverageType.setText(entity.CoverageTypeName);
        $('#<%=hdnCoverageTypeID.ClientID %>').val(entity.CoverageTypeID);

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

        $('#entryDetailContainerPopup').show();
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }

    //#region Coverage Type
    window.onGetCoverageTypeFilterExpression = function () {
        var filterExpression = "IsDeleted = 0";
        return filterExpression;
    }

    function onTacCoverageTypeButtonSearchClick() {
        openSearchDialog('coveragetype', onGetCoverageTypeFilterExpression(), function (value) {
            var filterExpression = onGetCoverageTypeFilterExpression() + " AND CoverageTypeCode = '" + value + "'";
            Methods.getObject('GetCoverageTypeList', filterExpression, function (result) {
                if (result != null) {
                    tacCoverageType.setValue(result.CoverageTypeID);
                    tacCoverageType.setText(result.CoverageTypeName);
                    entityToControlCoverageType(result);
                }
                else {
                    tacCoverageType.setValue('');
                    tacCoverageType.setText('');
                    entityToControlCoverageType(null);
                }
            });
        });

    }

    function onTacCoverageTypeValueChanged() {
        var id = tacCoverageType.getValue();
        if (id != '') {
            var filterExpression = "CoverageTypeID = " + value;
            Methods.getObject('GetCoverageTypeList', filterExpression, function (result) {
                entityToControlCoverageType(result);
            });
        }
    }

    function entityToControlCoverageType(result) {
        if (result != null)
            $('#<%=hdnCoverageTypeID.ClientID %>').val(result.CoverageTypeID);        
        else
            $('#<%=hdnCoverageTypeID.ClientID %>').val('');
    }
    //#endregion

    var idxStudent = 0;
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
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
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
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Contract")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table id="tblEntryPopup">
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Coverage")%></label></td>
                        <td>
                            <input type="hidden" id="hdnCoverageTypeID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacCoverageType" ClientInstanceName="tacCoverageType" MethodName="GetCoverageTypeList" GetFilterExpressionFunction="onGetStudentFilterExpression"
                                SearchFields="CoverageTypeName,CoverageTypeCode" TextField="CoverageTypeName" ValueField="CoverageTypeID" SearchText="${CoverageTypeName} (<b>${CoverageTypeCode}</b>)" OrderByExpression="CoverageTypeName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacCoverageTypeButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacCoverageTypeValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Member")%></span><br /></td>
                    </tr>
                    <tr id="trSaveEntryPopup">
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="CoverageTypeName" HeaderText="Koordinator" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="ListStudentName" HeaderText="Member" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("CoverageTypeID") %>" bindingfield="CoverageTypeID" />
                                    <input type="hidden" value="<%#Eval("CoverageTypeName") %>" bindingfield="CoverageTypeName" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

