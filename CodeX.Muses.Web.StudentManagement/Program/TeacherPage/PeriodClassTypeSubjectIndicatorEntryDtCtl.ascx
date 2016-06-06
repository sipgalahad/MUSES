<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeriodClassTypeSubjectIndicatorEntryDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodClassTypeSubjectIndicatorEntryDtCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<script type="text/javascript" id="dxss_generatebilldtctl">
    function onBeforeSaveRecord(errMessage) {
        var result = '';
        $('#<%=grdView.ClientID %> > tbody > tr:gt(0)').each(function () {
            $tr = $(this);
            var indicatorID = $tr.find('.keyField').html();
            var idx = $tr.find('.hdnItemIndex').val();
            var cboPeriodSection = eval('cboPeriodSection' + idx);

            var periodSection = '';
            if (cboPeriodSection.GetValue() != null)
                periodSection = cboPeriodSection.GetValue();
            if (result != '')
                result += '|';
            result += indicatorID + ';' + periodSection;
        });
        $('#<%=hdnLstSaveValue.ClientID %>').val(result);
        return true;
    }
</script>
<input type="hidden" id="hdnPeriodClassTypeSubjectID" runat="server" />
<input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
<input type="hidden" id="hdnLstSaveValue" runat="server" />
<div style="height:440px; overflow-y:scroll">
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Kelas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText2" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText3" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>

    <div class="containerTblEntryContent">
        <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
            ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e) { hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent2" runat="server">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SubjectCurriculumSyllabusID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="SubjectCurriculumSyllabusName" HeaderText="Indicator" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderText="Semester">
                                <ItemTemplate>
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                    <dxe:ASPxComboBox ID="cboPeriodSection" runat="server" Width="100%" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntriesPopup"></div>
            <div class="wrapperPaging">
                <div id="pagingPopup"></div>
            </div>
        </div> 
    </div>
</div>