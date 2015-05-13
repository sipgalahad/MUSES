<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassSubjectExtracurricularEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassSubjectExtracurricularEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onCboClassTypeValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var id = cboClass.GetValue() + '|' + entity.SubjectID + '|' + entity.PeriodClassTypeSubjectID;
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSubjectExtracurricular/ClassSubjectExtracurricularDtEntryCtl.ascx");
            openUserControlPopup(url, id, 'Detil Guru', 1150, 500);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <table>
        <colgroup>
            <col style="width: 150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Kelas") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboClass" ClientInstanceName="cboClass" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboClassTypeValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
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
                                <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" HeaderStyle-Width="250px" />
                                <asp:BoundField DataField="TeacherName" HeaderText="Pembina"/>
                                 <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SubjectID") %>" bindingfield="SubjectID" />
                                        <input type="hidden" value="<%#Eval("PeriodClassTypeSubjectID") %>" bindingfield="PeriodClassTypeSubjectID" />
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
</asp:Content>