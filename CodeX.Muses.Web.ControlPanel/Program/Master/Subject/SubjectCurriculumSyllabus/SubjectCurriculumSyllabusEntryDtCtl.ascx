<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectCurriculumSyllabusEntryDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectCurriculumSyllabusEntryDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<script type="text/javascript" id="dxss_itembomentryctl">
    $('#btnEntryPopupCancel').click(function () {
        pcRightPanelContent.Hide();
    });

    $('#btnEntryPopupSaveNew').click(function (evt) {
        if (IsValid(evt, 'fsEntryPopup', 'mpEntryPopup'))
            cbpEntryPopupView.PerformCallback('savenew');
        return false;
    });

    $('#btnEntryPopupSaveClose').click(function (evt) {
        if (IsValid(evt, 'fsEntryPopup', 'mpEntryPopup'))
            cbpEntryPopupView.PerformCallback('saveclose');
        return false;
    });

    function onCbpEntryPopupViewEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'saveclose') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                onAfterSaveRecordSubjectCurriculumSyllabus();
                pcRightPanelContent.Hide();
            }
        }
        else {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                onAfterSaveRecordSubjectCurriculumSyllabus();
                $('#<%=txtSubjectCurriculumSyllabusCode.ClientID %>').val('');
                $('#<%=txtSubjectCurriculumSyllabusName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
                $('#<%=hdnIsAdd.ClientID %>').val('1');
                cboReferenceID.SetValue('');
                $('#containerPopupEntryData').show();
                if ($('#<%=hdnIsUsingCode.ClientID %>').val() == '1')
                    $('#<%=txtSubjectCurriculumSyllabusCode.ClientID %>').focus();
                else
                    $('#<%=txtSubjectCurriculumSyllabusName.ClientID %>').focus();
            }
        }
        hideLoadingPanel();
    }
</script>
<div>
    <input type="hidden" id="hdnSubjectCurriculumID" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumID" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumSyllabusID" value="" runat="server" />
    <input type="hidden" id="hdnSubjectCurriculumSyllabusID" value="" runat="server" />
    <input type="hidden" id="hdnParentID" value="" runat="server" />
    <input type="hidden" id="hdnIsUsingCode" value="" runat="server" />
    <input type="hidden" id="hdnIsPerSchoolPeriodSection" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumSchoolPeriodSectionID" value="" runat="server" />
    <input type="hidden" id="hdnIsAdd" value="" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 100%" />
        </colgroup>
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <div id="containerPopupEntryData">
                    <fieldset id="fsEntryPopup" style="margin: 0">
                        <table style="width: 100%" cellpadding="0" cellspacing="1">
                            <colgroup>
                                <col style="width: 125px" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe")%></label></td>
                                <td><asp:TextBox ID="txtType" runat="server" Width="200px" ReadOnly="true" /></td>
                            </tr>
                            <tr id="trSchoolPeriodSection" runat="server">
                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
                                <td><asp:TextBox ID="txtSchoolPeriodSectionName" runat="server" Width="200px" ReadOnly="true" /></td>
                            </tr>
                            <tr id="trCode" runat="server">
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                                <td><asp:TextBox ID="txtSubjectCurriculumSyllabusCode" runat="server" Width="100px" /></td>
                            </tr>
                            <tr id="trCodeStandardCode" runat="server">
                                <td class="tdLabel"><label class="lblMandatory" id="lblStandardCode" runat="server"></label></td>
                                <td><dxe:ASPxComboBox ID="cboStandardCode" ClientInstanceName="cboStandardCode" runat="server" Width="200px"  /></td>
                            </tr>
                            <tr id="trMainCompetency" runat="server">
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kompetensi Inti") %></label></td>
                                <td><dxe:ASPxComboBox ID="cboMainCompetency" ClientInstanceName="cboMainCompetency" runat="server" Width="200px"  /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px; vertical-align: top" class="tdLabel"><label class="lblMandatory"><%=GetLabel("Teks")%></label></td>
                                <td><asp:TextBox ID="txtSubjectCurriculumSyllabusName" runat="server" Width="450px" TextMode="MultiLine" Rows="5" /></td>
                            </tr>
                            <tr id="trReferenceID" runat="server">
                                <td class="tdLabel"><label class="lblMandatory" id="lblReference" runat="server"></label></td>
                                <td><dxe:ASPxComboBox ID="cboReferenceID" ClientInstanceName="cboReferenceID" runat="server" Width="200px"  /></td>
                            </tr>
                            <tr id="trIsAllowTask" runat="server">
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Include Dalam Tugas") %></label></td>
                                <td><asp:CheckBox ID="chkIsAllowTask" runat="server" /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px; vertical-align: top" class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                <td><asp:TextBox ID="txtRemarks" runat="server" Width="450px" TextMode="MultiLine" Rows="5" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <table>
                                        <tr>
                                            <td><input type="button" id="btnEntryPopupSaveNew" value='<%= GetLabel("Save & New")%>' /></td>
                                            <td><input type="button" id="btnEntryPopupSaveClose" value='<%= GetLabel("Save & Close")%>' /></td>
                                            <td><input type="button" id="btnEntryPopupCancel" value='<%= GetLabel("Cancel")%>' /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none">
        <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
            ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</div>
