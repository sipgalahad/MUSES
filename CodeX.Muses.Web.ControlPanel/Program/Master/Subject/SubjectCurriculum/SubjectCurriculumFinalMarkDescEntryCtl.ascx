<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectCurriculumFinalMarkDescEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.SubjectCurriculumFinalMarkDescEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    function onBeforeSaveRecord() {
        var result = '';
        $('.txtDescriptionText').each(function () {
            if (result != "")
                result += '|';
            var descriptionText = $(this).val();
            var curriculumschoolperiodsectionid = $(this).attr('curriculumschoolperiodsectionid');
            var curriculummarktypeid = $(this).attr('curriculummarktypeid');
            result += curriculumschoolperiodsectionid + ';' + curriculummarktypeid + ';' + descriptionText;
        });
        $('#<%=hdnSaveValue.ClientID %>').val(result);
        return true;
    }
</script>

<input type="hidden" id="hdnSaveValue" runat="server" />
<input type="hidden" id="hdnID" runat="server" value="" />
<div>
    <table>
        <colgroup>
            <col style="width: 160px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Kurikulum")%></label></td>
            <td><asp:TextBox ID="txtSubjectCurriculumName" Width="200px" ReadOnly="true" runat="server" /></td>
        </tr>
        <asp:Repeater ID="rptMarkType" runat="server" OnItemDataBound="rptMarkType_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td colspan="2"><h4><%#Eval("CurriculumMarkTypeName") %></h4></td>
                </tr>
                <asp:Repeater ID="rptPeriodSection" runat="server" OnItemDataBound="rptPeriodSection_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%#Eval("CurriculumSchoolPeriodSectionName")%></label></td>
                            <td><asp:TextBox ID="txtDescriptionText" CssClass="txtDescriptionText" Width="200px" runat="server" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>        
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>

