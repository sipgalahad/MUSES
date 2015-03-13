<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassScheduleExtracurricularEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassScheduleExtracurricularEntry" %>

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

        $('.btnSave').live('click', function () {
            $tr = $(this).closest('tr');
            $txtStartTime = $tr.find('.txtStartTime');
            $txtEndTime = $tr.find('.txtEndTime');
            if ($txtStartTime.valid() && $txtEndTime.valid()) {
                var idx = $tr.find('.hdnItemIndex').val();
                var cboDayNumber = eval('cboDayNumber' + idx);
                var cboRoom = eval('cboRoom' + idx);
                var classSubjectID = $tr.find('.hdnClassSubjectID').val();
                var schoolClassID = $tr.find('.hdnSchoolClassID').val();
                var startTime = $txtStartTime.val();
                var endTime = $txtEndTime.val();
                var roomID = 0;
                if (cboRoom.GetValue() != null)
                    roomID = cboRoom.GetValue();
                $('#<%=hdnSaveValue.ClientID %>').val(classSubjectID + '|' + schoolClassID + '|' + cboDayNumber.GetValue() + '|' + roomID + '|' + startTime + '|' + endTime);
                onCustomButtonClick('save');
            }
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var id = cboClass.GetValue() + '|' + entity.SubjectID + '|' + entity.PeriodClassTypeSubjectID;
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSubject/ClassSubjectDtEntryCtl.ascx");
            openUserControlPopup(url, id, 'Detil Guru', 1150, 500);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" OnRowDataBound="grdView_RowDataBound"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="SubjectName" HeaderText="Ekskul" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="SchoolClassName" HeaderText="Kelas" HeaderStyle-Width="150px"/>
                                <asp:BoundField DataField="TeacherName" HeaderText="Pembina" />
                                <asp:TemplateField HeaderText="Hari" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <dxe:ASPxComboBox ID="cboDayNumber" runat="server" Width="90%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jam" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td><asp:TextBox ID="txtStartTime" CssClass="time required txtStartTime" runat="server" Width="60px" /></td>
                                                <td style="width:3px">-</td>
                                                <td><asp:TextBox ID="txtEndTime" CssClass="time required txtEndTime" runat="server" Width="60px" /></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ruangan" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <dxe:ASPxComboBox ID="cboRoom" runat="server" Width="90%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' />
                                        <input type="hidden" class="hdnClassSubjectID" value='<%#Eval("ClassSubjectID") %>' />
                                        <input type="hidden" class="hdnSchoolClassID" value='<%#Eval("SchoolClassID") %>' />
                                        <input type="button" id="btnSave" class="btnSave" value="Simpan" runat="server" />
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