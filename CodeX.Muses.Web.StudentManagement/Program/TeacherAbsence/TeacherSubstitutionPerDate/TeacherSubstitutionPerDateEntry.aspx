<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPTeacherAbsencePageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="TeacherSubstitutionPerDateEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherSubstitutionPerDateEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var id = cboClass.GetValue() + '|' + entity.SubjectID + '|' + entity.PeriodClassTypeSubjectID;
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSubject/ClassSubjectDtEntryCtl.ascx");
            openUserControlPopup(url, id, 'Detil Guru', 1000, 500);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
            cbpClassSubject.PerformCallback('refresh');
        }

        $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
            $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');

            $tdRemaining = $(this).find('.tdRemaining');
            if ($tdRemaining.html() != '0') {

                $(this).addClass('selected');

                var entity = rowToObject($(this));

                $('#hdnSelectedTrTeacherID').val(entity.TeacherID);
                $('#hdnSelectedTrTeacherName').val(entity.TeacherName);

                $('#tdSelectedTeacher').html("<label class='lblLink' id='lblTeacher'>" + entity.TeacherName + "</label>");
                pcClassSubject.Hide();
            }
        });

        $('.tblSchedule tr.T001 td.tdHtmlText').live('click', function (e) {
            if (e.target !== this) {
                if ($(e.target).attr('class') != 'bTeacherName')
                    return;
            }
            if ($('#hdnSelectedTrTeacherID').val() != '') {
                $bTeacherName = $(this).find('.bTeacherName');
                if ($bTeacherName.length) {
                    $(this).parent().find('.tdTeacherID').html($('#hdnSelectedTrTeacherID').val());
                    $bTeacherName.html($('#hdnSelectedTrTeacherName').val());
                }
            }
        });

        $('.divDetailDelete').live('click', function (e) {
            $tr = $(this).closest('tr');
            $tr.find('.tdHtmlText').find('.bTeacherName').html($('#<%=hdnTeacherName.ClientID %>').val());
            $tr.find('.tdTeacherID').html('');
        });

        var isChangePage = false;
        function onBeforeChangePage() {
            isChangePage = true;
            $('#<%=btnSave.ClientID %>').click();
        }

        function onAfterCustomClickSuccess() {
            if (isChangePage)
                goToNextPage();
        }

        $(function () {
            setDatePicker('<%=txtSchoolDate.ClientID %>');
            $('#<%=txtSchoolDate.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                var lstClassScheduleID = [];
                var lstTeacherID = [];
                var lstTeacherSubstitutionID = [];
                $('.tblSchedule tr.T001').each(function () {
                    $tr = $(this);
                    var teacherID = $tr.find('.tdTeacherID').html();
                    if (teacherID != '') {
                        var classScheduleID = $tr.find('.tdValue').html();
                        var teacherSubstitutionID = $tr.find('.tdTeacherSubstitutionID').html();

                        lstClassScheduleID.push(classScheduleID);
                        lstTeacherID.push(teacherID);
                        lstTeacherSubstitutionID.push(teacherSubstitutionID);
                    }
                });
                $('#<%=hdnLstClassScheduleID.ClientID %>').val(lstClassScheduleID.join(','));
                $('#<%=hdnLstTeacherID.ClientID %>').val(lstTeacherID.join(','));
                $('#<%=hdnLstTeacherSubstitutionID.ClientID %>').val(lstTeacherSubstitutionID.join(','));

                onCustomButtonClick('save');
            });

            $('#lblChangeTeacher').click(function () {
                pcClassSubject.Show();
            });
        });

        $('#lblTeacher.lblLink').live('click', function () {
            var teacherID = $('#hdnSelectedTrTeacherID').val();
            if (teacherID != '') {
                var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSchedule/TeacherScheduleDtCtl.ascx");
                openUserControlPopup(url, teacherID, 'Jadwal Guru', 1250, 550);
            }
        });
    </script>
    <input type="hidden" runat="server" id="hdnLstClassScheduleID" />
    <input type="hidden" runat="server" id="hdnLstTeacherID" />
    <input type="hidden" runat="server" id="hdnLstTeacherSubstitutionID" />
    <input type="hidden" runat="server" id="hdnTeacherID" />
    <input type="hidden" runat="server" id="hdnTeacherName" />
    <input type="hidden" runat="server" id="hdnSchoolPeriodID" />

    <input type="hidden" id="hdnSelectedTrTeacherID" value="" />
    <input type="hidden" id="hdnSelectedTrTeacherName" value="" />
    <table>
        <colgroup>
            <col style="width:120px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
            <td><asp:TextBox ID="txtSchoolDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
    </table>
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 56px; cursor: pointer; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #2FD933; }
        .tblSchedule tr.T001 b              { color: Red; font-weight: normal; }
    </style>

    <table style="float:right; border: 1px solid black">
        <colgroup>
            <col style="width:150px" />
            <col style="width:350px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblLink" id="lblChangeTeacher" style="font-weight: bold;"><%=GetLabel("Guru") %></label> :</td>
            <td id="tdSelectedTeacher">&nbsp;</td>
        </tr>
    </table>
    <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
    <asp:Repeater ID="rptRemarks" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><div class='nts<%#Eval("cfStandardCodeID") %>' style="width: 20px; height: 20px; border: 1px solid black;"></div></td>
                <td><%#Eval("StandardCodeName") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <table style="width:100%">
                            <tr>
                                <td valign="top" id="tdSchoolDay1" runat="server">
                                    <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                                    <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">1</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay2" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">2</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay3" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">3</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay4" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">4</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay5" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">5</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top" id="tdSchoolDay6" runat="server"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherSubstitutionID" id="tdTeacherSubstitutionID" runat="server"></td>
                                                <td style="display:none;" class="tdTeacherID" id="tdTeacherID" runat="server"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">6</td>
                                                <td class="tdHtmlText" id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>

    <dxpc:ASPxPopupControl ID="pcClassSubject" runat="server" ClientInstanceName="pcClassSubject" EnableHierarchyRecreation="True"
        FooterText="" HeaderText="Daftar Guru" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="800px"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Height="450px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div style="height:390px; overflow-y:scroll">
                    <dxcp:ASPxCallbackPanel ID="cbpClassSubject" runat="server" Width="100%" ClientInstanceName="cbpClassSubject"
                        ShowLoadingPanel="false" OnCallback="cbpClassSubject_Callback">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdView"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="TeacherID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderText="Guru">
                                                <ItemTemplate>
                                                    <%#Eval("TeacherName")%>
                                                    <input type="hidden" value="<%#Eval("TeacherID") %>" bindingfield="TeacherID" />
                                                    <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
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
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>   
</asp:Content>