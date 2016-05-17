<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPRProjectPageTrx.master" AutoEventWireup="true" 
    CodeBehind="RProjectStatusList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectStatusList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=chkIsShowAllGroup.ClientID %>').change(function () {
                cbpView2.PerformCallback();
            });

            $('#divTransactionAdd').click(function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtProjectTaskGroupName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
                $('#entryDetailContainer').show();
            });

            $('#divTransactionCopy').click(function () {
                var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectStatus/RProjectTaskGroupCopyEntryCtl.ascx');
                var id = $('#<%=hdnProjectOrganizationID.ClientID %>').val();
                openUserControlPopup(url, id, 'Copy Kelompok Tugas', 1100, 400);
            }); 

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        $('.lblTask').live('click', function () {
            var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectStatus/RProjectTaskDtEntryCtl.ascx');
            var id = $(this).closest('tr').find('.keyField').html() + '|' + $('#<%=hdnProjectOrganizationID.ClientID %>').val();
            openUserControlPopup(url, id, 'Detil Tugas', 1200, 500);
        });

        $('#<%=grdView2.ClientID %> .divDetailDelete').die('click');
        $('#<%=grdView2.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskGroupID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView2.ClientID %> .divDetailEdit').die('click');
        $('#<%=grdView2.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectTaskGroupID);
            $('#<%=txtProjectTaskGroupName.ClientID %>').val(entity.ProjectTaskGroupName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            $('#entryDetailContainer').show();
        });

        $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
            if ($(this).find('.hdnIsAllowAccess').val() == '1') {
                $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnProjectOrganizationID.ClientID %>').val($(this).find('.keyField').html());
                cbpView2.PerformCallback('refresh');
                $('#divContainerProjectTaskGroup').show();
            }
        });

        function onAfterPopupControlClosing() {
            cbpView.PerformCallback('refresh');
        }

        $(function () {
            $('#<%=grdView.ClientID %> tr:eq(1)').click();
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                if ($('#<%=hdnProjectOrganizationID.ClientID %>').val() == $(this).find('.keyField').html()) {
                    $(this).addClass('selected');
                }
            });
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
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
    </script>

    <input type="hidden" id="hdnProjectOrganizationID" runat="server" />
    <input type="hidden" id="hdnMyProjectOrganizationID" runat="server" />
    <input type="hidden" id="hdnMyProjectOrganizationIDDisplayPath" runat="server" />
    <table style="width:100%">
        <tr>
            <td style="width:50%; vertical-align: top" >
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectOrganizationID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:TemplateField HeaderStyle-Width="200px" >
                                            <HeaderTemplate>
                                                <div style="padding-left:3px">
                                                    <%=GetLabel("Jabatan / Posisi")%>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style='margin-left:<%# Eval("Level") %>0px;'><%# Eval("Position") %></div>
                                                <input type="hidden" runat="server" id="hdnIsAllowAccess" class="hdnIsAllowAccess" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeCoordinatorName" HeaderText="Koordinator" HeaderStyle-Width="200px" />
                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" >
                                            <HeaderTemplate>                                              
                                                <%=GetLabel("Persentase")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="divPercentage" runat="server"><%# Eval("Position") %></div>
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
            </td>
            <td style="width:50%; vertical-align: top" >
                <div id="divContainerProjectTaskGroup" style="display:none">
                    <asp:CheckBox ID="chkIsShowAllGroup" runat="server" Checked="false" Text="Tampilkan Semua Kelompok Tugas" />
                    <div class="divTransactionEntry">   
                        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                        <span id="divTransactionCopy" class="divAdd" style="margin-left: 40px;"><%=GetLabel("Copy Data")%></span><br />
                        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrx" style="margin:0"> 
                                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                                <table id="tblEntry">
                                    <colgroup>
                                        <col style="width:150px"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelompok Tugas") %></label></td>
                                        <td><asp:TextBox runat="server" ID="txtProjectTaskGroupName" Width="300px" /></td>
                                    </tr>
                                    <tr valign="top" style="padding-top: 5px">
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                                    </tr>
                                    <tr id="trSaveEntry">
                                        <td> 
                                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <dxcp:ASPxCallbackPanel ID="cbpView2" runat="server" Width="100%" ClientInstanceName="cbpView2"
                        ShowLoadingPanel="false" OnCallback="cbpView2_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView2_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectTaskGroupID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="ProjectTaskGroupName" HeaderText="Kelompok Tugas" />
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Task" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <label class="lblLink lblTask"><%=GetLabel("Task") %></label>
                                                </ItemTemplate>
                                            </asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="Persentase" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div id="divPercentage" runat="server"></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("ProjectTaskGroupID") %>" bindingfield="ProjectTaskGroupID" />
                                                    <input type="hidden" value="<%#Eval("ProjectTaskGroupName") %>" bindingfield="ProjectTaskGroupName" />
                                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
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
                    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
                        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
                        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
                    </dxcp:ASPxCallbackPanel>
                </div>
            </td>
        </tr>
    </table>
    
</asp:Content>