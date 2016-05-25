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
        var cbpView = null;
        $('.lblProjectTask').die('click');
        $('.lblProjectTask').live('click', function () {
            $tr = $(this).closest('tr');
            var tblClass = $tr.closest('table');
            if (tblClass.hasClass('grdView1')) cbpView = cbpView1;
            if (tblClass.hasClass('grdView2')) cbpView = cbpView2;
            if (tblClass.hasClass('grdView3')) cbpView = cbpView3;

            var id = $tr.find('.keyField').html() + '|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnIsVerifiedTask').val();
            var url = ResolveUrl('~/Program/Process/RProjectSummary/MyRProjectTaskDtEntryCtl.ascx');
            openUserControlPopup(url, id, 'Detil Tugas', 1200, 500);
        });
        $('.lblProjectTaskGroup').die('click');
        $('.lblProjectTaskGroup').live('click', function () {
            $tr = $(this).closest('tr');
            var tblClass = $tr.closest('table');
            if (tblClass.hasClass('grdView1')) cbpView = cbpView1;
            if (tblClass.hasClass('grdView2')) cbpView = cbpView2;
            if (tblClass.hasClass('grdView3')) cbpView = cbpView3;

            var id = $tr.find('.hdnProjectTaskGroupID').val() + '|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnProjectID').val() + '|1|' + $tr.find('.hdnProjectOrganizationID').val() + '|' + $tr.find('.hdnProjectOrganizationIDDisplayPath').val() + '|' + $tr.find('.hdnIsVerifiedTask').val();
            var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectStatus/RProjectTaskDtEntryCtl.ascx');
            openUserControlPopup(url, id, 'Detil Tugas', 1200, 500);
        });
        $('.lblProject').die('click');
        $('.lblProject').live('click', function () {
            var id = $(this).closest('tr').find('.hdnProjectID').val() + '|1';
            var tblClass = $tr.closest('table');
            if (tblClass.hasClass('grdView1')) cbpView = cbpView1;
            if (tblClass.hasClass('grdView2')) cbpView = cbpView2;
            if (tblClass.hasClass('grdView3')) cbpView = cbpView3;

            var url = ResolveUrl('~/Program/Process/RProjectPage/RProjectPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'Project Status' + id, '1300', '650');
        });

        function onAfterPopupControlClosing() {
            cbpView.PerformCallback('refresh');
        }

        //#region Paging 1
        var pageCount1 = parseInt('<%=PageCount1 %>');
        var rowCount1 = parseInt('<%=RowCount1 %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries1'), rowCount1, 1, rowCountPerPage);
            setPaging($("#paging1"), pageCount1, function (page) {
                cbpView1.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries1'), rowCount1, page, rowCountPerPage);
            });
        });

        function onCbpView1EndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                setNumEntriesText($('#informationNumEntries1'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging1"), pageCount, function (page) {
                    cbpView1.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries1'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        //#region Paging 2
        var pageCount2 = parseInt('<%=PageCount2 %>');
        var rowCount2 = parseInt('<%=RowCount2 %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries2'), rowCount2, 1, rowCountPerPage);
            setPaging($("#paging2"), pageCount2, function (page) {
                cbpView2.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries2'), rowCount2, page, rowCountPerPage);
            });
        });

        function onCbpView2EndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                setNumEntriesText($('#informationNumEntries2'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging2"), pageCount, function (page) {
                    cbpView2.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries2'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        //#region Paging 3
        var pageCount3 = parseInt('<%=PageCount3 %>');
        var rowCount3 = parseInt('<%=RowCount3 %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries3'), rowCount3, 1, rowCountPerPage);
            setPaging($("#paging3"), pageCount3, function (page) {
                cbpView3.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries3'), rowCount3, page, rowCountPerPage);
            });
        });

        function onCbpView3EndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                setNumEntriesText($('#informationNumEntries3'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging3"), pageCount, function (page) {
                    cbpView3.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries3'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion
    </script>
    <style>
        b               { color: Maroon; }
        h5              { margin: 0; font-weight: bold; font-size: 16px !important; }
    </style>
    <h4><%=GetLabel("Tugas Baru") %></h4>
    <dxcp:ASPxCallbackPanel ID="cbpView1" runat="server" Width="100%" ClientInstanceName="cbpView1"
        ShowLoadingPanel="false" OnCallback="cbpView1_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpView1EndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView">
                    <asp:GridView ID="grdNewTask" runat="server" CssClass="grdSelected grdView1" AutoGenerateColumns="false" 
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
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>    
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntries1"></div>
        <div class="wrapperPaging">
            <div id="paging1"></div>
        </div>
    </div> 
    <br />
    <h4><%=GetLabel("Tugas Pending") %></h4>
    <dxcp:ASPxCallbackPanel ID="cbpView2" runat="server" Width="100%" ClientInstanceName="cbpView2"
        ShowLoadingPanel="false" OnCallback="cbpView2_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpView2EndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server">
                <asp:Panel runat="server" ID="Panel1">
                    <asp:GridView ID="grdOldTask" runat="server" CssClass="grdSelected grdView2" AutoGenerateColumns="false" 
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
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>     
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntries2"></div>
        <div class="wrapperPaging">
            <div id="paging2"></div>
        </div>
    </div> 
    <br />
    <h4><%=GetLabel("Butuh Verifikasi") %></h4>
    <dxcp:ASPxCallbackPanel ID="cbpView3" runat="server" Width="100%" ClientInstanceName="cbpView3"
        ShowLoadingPanel="false" OnCallback="cbpView3_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpView3EndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent3" runat="server">
                <asp:Panel runat="server" ID="Panel2">
                    <asp:GridView ID="grdNeedVerification" runat="server" CssClass="grdSelected grdView3" AutoGenerateColumns="false" 
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
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>     
    <div class="containerPaging">
        <div class="divInformationNumEntries" id="informationNumEntries3"></div>
        <div class="wrapperPaging">
            <div id="paging3"></div>
        </div>
    </div> 
</asp:Content>