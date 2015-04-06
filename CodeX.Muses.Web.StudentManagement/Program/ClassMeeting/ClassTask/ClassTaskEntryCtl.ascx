<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        setDatePicker('<%=txtTaskDate.ClientID %>');
        setDatePicker('<%=txtStartDate.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');
    });

    function onBeforeSaveRecord() {
        var result = '';
        $('.tacSubjectIndicator').each(function () {
            if (result != "")
                result += ',';
            result += $(this).find('.hdnAutoCompleteValue').val();
        });
        $('#<%=hdnSubjectIndicatorSave.ClientID %>').val(result);
        return true;
    }

    window.onGetSubjectIndicatorFilterExpression = function () {
        var filterExpression = "<%=OnGetSubjectIndicatorFilterExpression() %>";
        return filterExpression;
    }

    var idxSubjectIndicator = 0;
    var tempHelper = null;
    $(function () {
        $('#divEntryDtAdd').click(function () {
            $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
            $newTr = $newTr.replace(/\$\{idx}/g, idxSubjectIndicator);
            $newTr = $($newTr);
            $newTr.insertBefore($('#trSaveEntryPopup'));

            tempHelper = new CodeXClientAutoCompleteHelper();
            tempHelper.init("SubjectIndicator" + idxSubjectIndicator, "SubjectBasicCompetencyName,SubjectIndicatorName", "GetvSubjectIndicatorList", "", "onGetSubjectIndicatorFilterExpression", "SubjectIndicatorID");
            tempHelper.setClientSideEvents(onSubjectIndicatorIDValueChanged);
            tempHelper.initializeControl();
            idxSubjectIndicator++;
        });

        var classSubjectTaskID = $('#<%=hdnID.ClientID %>').val();
        if (classSubjectTaskID != '0') {
            var filterExpression = 'ClassSubjectTaskID = ' + classSubjectTaskID;
            Methods.getListObject('GetvClassSubjectTaskIndicatorList', filterExpression, function (result) {
                for (var i = 0; i < result.length; ++i) {
                    var entity = result[i];
                    $('#divEntryDtAdd').click();

                    $tr = $('.trSubjectIndicatorDt').last();
                    $tacSubjectIndicator = $tr.find('.tacSubjectIndicator');
                    $tacSubjectIndicator.find('.hdnAutoCompleteValue').val(entity.SubjectIndicatorID);
                    $tacSubjectIndicator.find('.hdnAutoCompleteText').val(entity.SubjectIndicatorName);
                    $tacSubjectIndicator.find('.txtAutoComplete').val(entity.SubjectIndicatorName);
                }
            });
        }
    });

    function onSubjectIndicatorIDValueChanged($s) {
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

    $('.tacSubjectIndicator .btnAutoCompleteSearchMore').die('click');
    $('.tacSubjectIndicator .btnAutoCompleteSearchMore').live('click', function () {
        if ($(this).attr('enabled') == null) {
            $tacTr = $(this).closest('tr');
            openSearchDialog('subjectindicator', onGetSubjectIndicatorFilterExpression(), function (value) {
                var filterExpression = onGetSubjectIndicatorFilterExpression() + " AND SubjectIndicatorID = '" + value + "'";
                Methods.getObject('GetvSubjectIndicatorList', filterExpression, function (result) {
                    $tacCOA = $tacTr.find('.tacSubjectIndicator');
                    if (result != null) {
                        $tacCOA.find('.hdnAutoCompleteValue').val(result.SubjectIndicatorID);
                        $tacCOA.find('.hdnAutoCompleteText').val(result.SubjectIndicatorName);
                        $tacCOA.find('.txtAutoComplete').val(result.SubjectIndicatorName);
                    }
                    else {
                        $tacCOA.find('.hdnAutoCompleteValue').val('');
                        $tacCOA.find('.hdnAutoCompleteText').val('');
                        $tacCOA.find('.txtAutoComplete').val('');
                    }
                    onSubjectIndicatorIDValueChanged($tacCOA.find('.txtAutoComplete'));
                });
                //var trIdx = $('.trPrescriptionEntry').index($tacTr);
                //if (trIdx == $('.trPrescriptionEntry').length - 1)
                //    addEntityRowPrescription();
                $tacTr = null;
            });
        }
    });
</script>

<script id="tmplEntityDt" type="text/x-jquery-tmpl">
    <tr class="trSubjectIndicatorDt">
        <td class="tdLabel"><%=GetLabel("Indikator") %></td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="SubjectIndicator${idx}" class="tacSubjectIndicator">
                            <div>
                                <div class="containerAutoComplete">
                                    <input type="hidden" class="hdnAutoCompleteValue"/>
                                    <input type="hidden" class="hdnAutoCompleteText"/>
                                    <input type="hidden" class="hdnIsRequired" value="1"/>
                                    <input type="hidden" class="hdnValidationGroup" value="mpDrugsQuickPicks"/>
                                    <input type="text" class="required txtAutoComplete" validationgroup="mpTrxPopup" style="width:400px"/>
                                    <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                                    <div class="divListAutoCompleteResultBox">
                                        <div class="divListAutoCompleteResult">
                                        </div>
                                    </div>
                                </div>
                                <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                    <div>
                                        ${SubjectIndicatorName} (<b>${SubjectIndicatorCode}</b>)
                                        <input type='hidden' value='${SubjectIndicatorName}' class='hdnAutoCompleteRowText'/>
                                        <input type='hidden' value='${SubjectIndicatorID}' class='hdnAutoCompleteRowValue'/>
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

<input type="hidden" id="hdnSubjectMatterID" runat="server" />
<input type="hidden" id="hdnSubjectIndicatorSave" runat="server" />
<input type="hidden" id="hdnID" runat="server" value="" />
<div>
    <table>
        <colgroup>
            <col style="width: 160px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
            <td><asp:TextBox ID="txtClassTaskCode" Width="100px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Topik")%></label></td>
            <td><asp:TextBox ID="txtTopic" Width="200px" runat="server" /></td>
        </tr>
        <tr id="trLessonType" runat="server">
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Pelajaran")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboLessonType" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tugas")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboTaskType" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("% Bobot Nilai")%></label></td>
            <td><asp:TextBox ID="txtFinalMarkPercentage" CssClass="number" Width="80px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Tugas")%></label></td>
            <td><asp:TextBox ID="txtTaskDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Mulai")%></label></td>
            <td>    
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
            <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
        </tr>
        <tr>
            <td colspan="2"><h4><%=GetLabel("Indikator") %></h4></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Indikator")%></span><br /></td>
        </tr>
        <tr id="trSaveEntryPopup">
        </tr>
    </table>
</div>

