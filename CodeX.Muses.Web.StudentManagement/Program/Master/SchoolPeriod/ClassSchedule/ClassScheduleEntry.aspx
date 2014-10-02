<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassScheduleEntry" %>

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
            var url = ResolveUrl("~/Program/Master/SchoolPeriod/ClassSubject/ClassSubjectDtEntryCtl.ascx");
            openUserControlPopup(url, id, 'Detil Guru', 1000, 500);
        });

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }

        $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
            $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');

            $tdRemaining = $(this).find('td.tdRemaining');
            if ($tdRemaining.html() != '0') {

                $(this).addClass('selected');

                var entity = rowToObject($(this));

                $('#hdnSelectedTrText').val(entity.SubjectName + '<br/>' + entity.TeacherName);
                $('#hdnSelectedTrValue').val(entity.ClassSubjectID);
                $('#hdnSelectedTrRoomID').val(entity.RoomID);
                $('#hdnSelectedTrRoomName').val(entity.RoomName);

                $('#tdSelectedSubject').html(entity.SubjectName);
                $('#tdSelectedTeacher').html(entity.TeacherName);
            }
        });

        $('.tblSchedule tr.T001 td.tdHtmlText').live('click', function (e) {
            if (e.target !== this)
                return;
            var text = $('#hdnSelectedTrText').val();
            if (text != '') {
                $tr = $(this).parent();
                if ($tr.find('.tdValue').html() != '') {
                    $tr.find('.divDetailDelete').click();
                }
                $(this).html('<div style="float:right" class="divDetailDelete"></div>' + text + '<br/><label class="lblLink">' + $('#hdnSelectedTrRoomName').val() + '</label>');

                $tr.find('.tdValue').html($('#hdnSelectedTrValue').val());
                $tr.find('.tdRoomID').html($('#hdnSelectedTrRoomID').val());

                $tdRemaining = $('#<%=grdView.ClientID %> tr.selected td.tdRemaining');
                var remaining = parseFloat($tdRemaining.html());
                $tdRemaining.html(remaining - 1);
                if (remaining < 2) {
                    $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                    $('#hdnSelectedTrText').val('');
                    $('#hdnSelectedTrValue').val('');
                    $('#hdnSelectedTrRoomID').val('');
                    $('#hdnSelectedTrRoomName').val('');

                    $('#tdSelectedSubject').html('');
                    $('#tdSelectedTeacher').html('');
                }
            }
        });

        $('.divDetailDelete').live('click', function (e) {
            $tr = $(this).closest('tr');
            $tr.find('.tdHtmlText').html($tr.find('.tdDefaultHtml').html());
            $tdValue = $tr.find('.tdValue');
            var classSubjectID = $tdValue.html();
            $tdValue.html('');

            var isFound = false;
            $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                if (!isFound) {
                    var entity = rowToObject($(this));
                    if (entity.ClassSubjectID == classSubjectID) {
                        $tdRemaining = $(this).find('td.tdRemaining');
                        var remaining = parseFloat($tdRemaining.html());
                        $tdRemaining.html(remaining + 1);
                        isFound = true;
                    }
                }
            });
        });
    </script>
    <table>
        <tr>
            <td><%=GetLabel("Kelas") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboClass" ClientInstanceName="cboClass" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboClassTypeValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdnSelectedTrText" value="" />
    <input type="hidden" id="hdnSelectedTrValue" value="" />
    <input type="hidden" id="hdnSelectedTrRoomID" value="" />
    <input type="hidden" id="hdnSelectedTrRoomName" value="" />
    <style type="text/css">
        .tblSchedule                        { width: 100%; }
        .tblSchedule td                     { text-align: center; }
        .tblSchedule tr td                  { border: 1px solid #333; }
        .tblSchedule tr.T001                { height: 56px; }
        .tblSchedule tr.T001 td, .nts001    { background-color: #2FD933; }
    </style>

    <table style="float:right; border: 1px solid black">
        <colgroup>
            <col style="width:150px" />
            <col style="width:150px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><div style="font-weight: bold;"><%=GetLabel("Dipilih") %> :</div></td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Mata Pelajaran") %> :</td>
            <td id="tdSelectedSubject">&nbsp;</td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Guru") %> :</td>
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
                            <colgroup>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                                <col style="width:15%"/>
                            </colgroup>
                            <tr>
                                <td valign="top">
                                    <h4 style="text-align: center"><%=GetLabel("Senin") %></h4>
                                    <asp:Repeater ID="rptDay1" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">1</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">2</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">3</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">4</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">5</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue"></td>
                                                <td style="display:none;" class="tdRoomID"></td>
                                                <td style="display:none;" class="tdHoursIndex"><%#Eval("HoursIndex") %></td>
                                                <td style="display:none;" class="tdDayNumber">6</td>
                                                <td class="tdHtmlText"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderText="Mata Pelajaran" HeaderStyle-Width="250px">
                                    <ItemTemplate>
                                        <%#Eval("SubjectName")%>
                                        <input type="hidden" value="<%#Eval("ClassSubjectID") %>" bindingfield="ClassSubjectID" />
                                        <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
                                        <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                        <input type="hidden" value="<%#Eval("RoomName") %>" bindingfield="RoomName" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TeacherName" HeaderText="Guru"/>
                                <asp:BoundField DataField="NoMeetingHoursInWeek" HeaderText="Jumlah Jam Pertemuan" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoMeetingHoursInWeek" HeaderText="Sisa Jam Pertemuan" ItemStyle-CssClass="tdRemaining" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
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