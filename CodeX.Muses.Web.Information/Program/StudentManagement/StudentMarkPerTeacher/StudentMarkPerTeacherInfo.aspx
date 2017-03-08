<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentMarkPerTeacherInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentMarkPerTeacherInfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtDateFrom.ClientID %>');
            setDatePicker('<%=txtDateTo.ClientID %>');

            $('#<%=txtDateFrom.ClientID %>').change(function () {
                cbpTeacher.PerformCallback('refresh');
                $('#tdDetail').attr('style', 'display:none');
            });
            $('#<%=txtDateTo.ClientID %>').change(function () {
                cbpTeacher.PerformCallback('refresh');
                $('#tdDetail').attr('style', 'display:none');
            });
        });

        function onCboSiteValueChanged() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNowFilterExpression() %>";
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                if (result != null) {
                    tacSchoolPeriod.setValue(result.SchoolPeriodID);
                    tacSchoolPeriod.setText(result.SchoolPeriodName);
                }
                else {
                    tacSchoolPeriod.setValue('');
                    tacSchoolPeriod.setText('');
                }
                onTacSchoolPeriodValueChanged();
            });
        }

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }

        //#region School Period
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "'";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                    }
                    onTacSchoolPeriodValueChanged();
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');

            var filterExpression = "SchoolPeriodID = '" + tacSchoolPeriod.getValue() + "' AND <%=OnGetPeriodSectionNowFilterExpression() %>";
            Methods.getObject('GetPeriodSectionList', filterExpression, function (result) {
                if (result != null) {
                    tacPeriodSection.setValue(result.PeriodSectionID);
                    tacPeriodSection.setText(result.PeriodSectionName);
                }
                else {
                    tacPeriodSection.setValue('');
                    tacPeriodSection.setText('');
                }
                onTacPeriodSectionValueChanged();
            });
        }
        //#endregion

        //#region Period Section
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND <%=OnGetPeriodSectionFilterExpression() %>";
            return filterExpression;
        }

        function onTacPeriodSectionButtonSearchClick() {
            openSearchDialog('periodsection', onGetPeriodSectionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodSectionFilterExpression() + " AND PeriodSectionCode = '" + value + "'";
                Methods.getObject('GetPeriodSectionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodSection.setValue(result.PeriodSectionID);
                        tacPeriodSection.setText(result.PeriodSectionName);
                    }
                    else {
                        tacPeriodSection.setValue('');
                        tacPeriodSection.setText('');
                    }
                    onTacPeriodSectionValueChanged();
                });
            });

        }

        function onTacPeriodSectionValueChanged() {
            cbpTeacher.PerformCallback('refresh');
            $('#tdDetail').attr('style', 'display:none');
        }
        //#endregion

         $('.lblTaskCount').live('click', function () {
             $tr = $(this).closest('tr');
             var classSubjectID = $tr.find('.keyField').html();
             var id = tacPeriodSection.getValue() + '|' + classSubjectID;

             var url = ResolveUrl("~/Program/StudentManagement/StudentMarkPerTeacher/StudentMarkInformationDtCtl.ascx");
             openUserControlPopup(url, id, 'Detail Tugas', 1200, 550);
         }); 

         $('.lblMeetingCount').live('click', function () {
             $tr = $(this).closest('tr');
             var classSubjectID = $tr.find('.keyField').html();
             var id = tacPeriodSection.getValue() + '|' + classSubjectID;

             var url = ResolveUrl("~/Program/StudentManagement/StudentMarkPerTeacher/ClassMeetingHistoryDtCtl.ascx");
             openUserControlPopup(url, id, 'Detail Pertemuan', 1200, 550);
         }); 

         $('.lblBelowPassingGradeCount').live('click', function () {
             $tr = $(this).closest('tr');
             var itemID = tacTeacher.getValue() + '|' + $tr.find('.keyField').html();

             var url = ResolveUrl("~/Program/StudentManagement/StudentMarkPerTeacher/StudentMarkPerTeacherInfoDtCtl.ascx");
             openUserControlPopup(url, itemID, 'Detail Informasi', 1200, 550);
         });

         $('#<%=grdTeacher.ClientID %> tr:gt(0)').live('click', function () {
             $('#<%=hdnTeacherID.ClientID %>').val($(this).find('.keyField').html());
             $('#<%=grdTeacher.ClientID %> tr.selected').removeClass('selected');
             $(this).addClass('selected');
             cbpView.PerformCallback('refresh');
             $('#tdDetail').removeAttr('style');
         });
    </script>
    <input type="hidden" id="hdnTeacherID" runat="server" />
    <table>
        <colgroup>
            <col style="width: 120px" />
        </colgroup>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetSchoolPeriodList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                    SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Tanggal") %></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateFrom" Width="120px" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateTo" Width="120px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="position: relative;">
        <table style="width:100%">
            <tr>
                <td style="vertical-align:top; width:400px">
                    <dxcp:ASPxCallbackPanel ID="cbpTeacher" runat="server" Width="100%" ClientInstanceName="cbpTeacher"
                        ShowLoadingPanel="false" OnCallback="cbpTeacher_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" CssClass="pnlContainerGrid">
                                    <asp:GridView ID="grdTeacher" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdTeacher_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="TeacherID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="TeacherName" HeaderText="Nama Guru"/>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Pertemuan" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <div id="divMeetingCount" runat="server"></div>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("Data Tidak Tersedia")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>  
                </td>
                <td style="vertical-align:top;">
                    <div id="tdDetail">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ClassSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="SchoolClassName" HeaderText="Nama Kelas"/>
                                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" HeaderStyle-Width="250px" />
                                                <asp:TemplateField HeaderStyle-Width="120px" HeaderText="Jmlh Pertemuan" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <label class="lblLink lblMeetingCount"><div id="divMeetingCount" runat="server"></div></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderStyle-Width="120px" HeaderText="Jmlh Penilaian" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <label class="lblLink lblTaskCount"><div id="divTaskCount" runat="server"></div></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="< KKM" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <label class="lblLink lblBelowPassingGradeCount"><div id="divBelowPassingGradeCount" runat="server"></div></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Jmlh Siswa" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div id="divStudentCount" runat="server"></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <%=GetLabel("Data Tidak Tersedia")%>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>    
                        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
                            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                        </div>
                        <div class="containerPaging">
                            <div class="divInformationNumEntries" id="informationNumEntries"></div>
                            <div class="wrapperPaging">
                                <div id="paging"></div>
                            </div>
                        </div> 
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
