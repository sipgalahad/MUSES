<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="MyRProjectSummaryList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.MyRProjectSummaryList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">

        $('.lblProjectTask').die('click');
        $('.lblProjectTask').live('click', function () {
            $tr = $(this).closest('tr');
            var id = $tr.find('.keyField').html() + '|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnIsVerifiedTask').val();
            var url = ResolveUrl('~/Program/Process/RProjectSummary/MyRProjectTaskDtEntryCtl.ascx');
            openUserControlPopup(url, id, 'Detil Tugas', 1200, 500);
        });
        $('.lblProjectTaskGroup').die('click');
        $('.lblProjectTaskGroup').live('click', function () {
            $tr = $(this).closest('tr');
            var id = $tr.find('.hdnProjectTaskGroupID').val() + '|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnProjectID').val() + '|1|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnProjectOrganizationIDDisplayPath').val() + '|' + $tr.find('.hdnIsVerifiedTask').val();
            var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectStatus/RProjectTaskDtEntryCtl.ascx');
            openUserControlPopup(url, id, 'Detil Tugas', 1200, 500);
        });
        $('.lblProject').die('click');
        $('.lblProject').live('click', function () {
            var id = $(this).closest('tr').find('.hdnProjectID').val() + '|1';
            var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'Project Status' + id, '1300', '650');
        });
    </script>
    <style>
        b               { color: Maroon; }
        h5              { margin: 0; font-weight: bold; font-size: 16px !important; }
    </style>
    <h4><%=GetLabel("Tugas Baru") %></h4>
    <asp:GridView ID="grdNewTask" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
        <Columns>
            <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
            <asp:TemplateField>
                <HeaderTemplate>                                              
                    <%=GetLabel("Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTask"><%#Eval("ProjectTaskName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Project")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="hidden" class="hdnIsVerifiedTask" value='0' />
                    <input type="hidden" class="hdnProjectTaskGroupID" value='<%#Eval("ProjectTaskGroupID") %>' />
                    <input type="hidden" id="hdnProjectOrganizationID" runat="server" class="hdnProjectOrganizationID" />
                    <input type="hidden" id="hdnProjectOrganizationIDDisplayPath" runat="server" class="hdnProjectOrganizationIDDisplayPath" />
                    <input type="hidden" class="hdnProjectID" value='<%#Eval("ProjectID") %>' />
                    <label class="lblLink lblProject"><%#Eval("ProjectName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Kelompok Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTaskGroup"><%#Eval("ProjectTaskGroupName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Jabatan")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divPosition" runat="server"></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" >
                <HeaderTemplate>                                              
                    <%=GetLabel("Tenggat Waktu")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("EndDate", "{0:dd-MMM-yyyy}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedByName" HeaderText="Dibuat Oleh"  HeaderStyle-Width="200px"/>
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("Data Tidak Tersedia")%>
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <h4><%=GetLabel("Tugas Pending") %></h4>
    <asp:GridView ID="grdOldTask" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
        <Columns>
            <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
            <asp:TemplateField>
                <HeaderTemplate>                                              
                    <%=GetLabel("Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTask"><%#Eval("ProjectTaskName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Project")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="hidden" class="hdnIsVerifiedTask" value='0' />
                    <input type="hidden" class="hdnProjectTaskGroupID" value='<%#Eval("ProjectTaskGroupID") %>' />
                    <input type="hidden" id="hdnProjectOrganizationID" runat="server" class="hdnProjectOrganizationID" />
                    <input type="hidden" id="hdnProjectOrganizationIDDisplayPath" runat="server" class="hdnProjectOrganizationIDDisplayPath" />
                    <input type="hidden" class="hdnProjectID" value='<%#Eval("ProjectID") %>' />
                    <label class="lblLink lblProject"><%#Eval("ProjectName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Kelompok Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTaskGroup"><%#Eval("ProjectTaskGroupName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="150px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Jabatan")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divPosition" runat="server"></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" >
                <HeaderTemplate>                                              
                    <%=GetLabel("Tenggat Waktu")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("EndDate", "{0:dd-MMM-yyyy}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Dibuat Tanggal"  HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreatedByName" HeaderText="Dibuat Oleh"  HeaderStyle-Width="200px"/>
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("Data Tidak Tersedia")%>
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <h4><%=GetLabel("Butuh Verifikasi") %></h4>
    <asp:GridView ID="grdNeedVerification" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
        <Columns>
            <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
            <asp:TemplateField>
                <HeaderTemplate>                                              
                    <%=GetLabel("Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTask"><%#Eval("ProjectTaskName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Project")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="hidden" class="hdnIsVerifiedTask" value='1' />
                    <input type="hidden" class="hdnProjectTaskGroupID" value='<%#Eval("ProjectTaskGroupID") %>' />
                    <input type="hidden" id="hdnProjectOrganizationID" runat="server" class="hdnProjectOrganizationID" />
                    <input type="hidden" id="hdnProjectOrganizationIDDisplayPath" runat="server" class="hdnProjectOrganizationIDDisplayPath" />
                    <input type="hidden" class="hdnProjectID" value='<%#Eval("ProjectID") %>' />
                    <label class="lblLink lblProject"><%#Eval("ProjectName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="200px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Kelompok Tugas")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="lblLink lblProjectTaskGroup"><%#Eval("ProjectTaskGroupName") %></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="150px">
                <HeaderTemplate>                                              
                    <%=GetLabel("Jabatan")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divPosition" runat="server"></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" >
                <HeaderTemplate>                                              
                    <%=GetLabel("Tenggat Waktu")%>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("EndDate", "{0:dd-MMM-yyyy}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="OrganizationCoordinatorName" HeaderText="Tugas Untuk" HeaderStyle-Width="200px"/>
        </Columns>
        <EmptyDataTemplate>
            <%=GetLabel("Data Tidak Tersedia")%>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>