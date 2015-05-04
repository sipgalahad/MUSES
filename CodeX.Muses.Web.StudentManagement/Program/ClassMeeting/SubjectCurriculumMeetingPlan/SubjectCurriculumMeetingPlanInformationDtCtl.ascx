<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectCurriculumMeetingPlanInformationDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectCurriculumMeetingPlanInformationDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<script type="text/javascript" id="dxss_itembomentryctl">
</script>
<div>
    <input type="hidden" id="hdnSubjectCurriculumID" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumMeetingPlanID" value="" runat="server" />
    <input type="hidden" id="hdnSubjectCurriculumMeetingPlanID" value="" runat="server" />
    <input type="hidden" id="hdnParentID" value="" runat="server" />
    <input type="hidden" id="hdnIsUsingCode" value="" runat="server" />
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
                            <tr id="trCode" runat="server">
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                                <td><asp:TextBox ID="txtSubjectCurriculumMeetingPlanCode" ReadOnly="true" runat="server" Width="100px" /></td>
                            </tr>
                            <tr id="trName" runat="server">
                                <td style="padding-top:5px; vertical-align: top" class="tdLabel"><label class="lblMandatory"><%=GetLabel("Teks")%></label></td>
                                <td><asp:TextBox ID="txtSubjectCurriculumMeetingPlanName" ReadOnly="true" runat="server" Width="450px" TextMode="MultiLine" Rows="5" /></td>
                            </tr>
                            <tr id="trReferenceID" runat="server">
                                <td class="tdLabel"><label class="lblMandatory" id="lblReference" runat="server"></label></td>
                                <td><dxe:ASPxComboBox ID="cboReferenceID" ClientInstanceName="cboReferenceID" ClientEnabled="false" runat="server" Width="200px"  /></td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px; vertical-align: top" class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                                <td><asp:TextBox ID="txtRemarks" runat="server" Width="450px" ReadOnly="true" TextMode="MultiLine" Rows="5" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</div>
