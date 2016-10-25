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
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Teacher
         function onGetTeacherFilterExpression() {
             var filterExpression = "TeacherID IN (SELECT TeacherID FROM TeacherSubject WHERE SiteID = '" + cboSite.GetValue() + "') AND IsDeleted = 0";
             return filterExpression;
         }

         function onTacTeacherButtonSearchClick() {
             openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                 var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                 Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                     if (result != null) {
                         tacTeacher.setValue(result.TeacherID);
                         tacTeacher.setText(result.TeacherName);
                     }
                     else {
                         tacTeacher.setValue('');
                         tacTeacher.setText('');
                     }
                     onTacTeacherValueChanged();
                 });
             });

         }

         function onTacTeacherValueChanged() {
             cbpView.PerformCallback('refresh');
         }
         //#endregion

         $('.lblBelowPassingGradeCount').live('click', function () {
             $tr = $(this).closest('tr');
             var itemID = tacTeacher.getValue() + '|' + $tr.find('.keyField').html();

             var url = ResolveUrl("~/Program/StudentManagement/StudentMarkPerTeacher/StudentMarkPerTeacherInfoDtCtl.ascx");
             openUserControlPopup(url, itemID, 'Detail Informasi', 1200, 550);
         });
    </script>
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Guru")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
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
    </table>
    <div style="position: relative;">
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
                                <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Jmlh Penilaian" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <div id="divTaskCount" runat="server"></div>
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
</asp:Content>
