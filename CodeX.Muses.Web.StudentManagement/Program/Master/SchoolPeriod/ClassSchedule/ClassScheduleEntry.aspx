<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassScheduleEntry" %>

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
        function onCboClassTypeValueChanged(s) {
            cbpView.PerformCallback('refresh');
            cbpClassSubject.PerformCallback('refresh');

            $('#hdnSelectedTrText').val('');
            $('#hdnSelectedTrValue').val('');
            $('#hdnSelectedTrTeacherID').val(''); 
            $('#hdnSelectedTrRoomID').val('');
            $('#hdnSelectedTrRoomName').val('');

            $('#tdSelectedSubject').html('');
            $('#tdSelectedTeacher').html('');
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
            cbpClassSubject.PerformCallback('refresh');
        }

        $('#<%=grdView.ClientID %> tr:gt(0)').live('click', function () {
            $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');

            $tdRemaining = $(this).find('.tdRemaining');
            if ($tdRemaining.html() != '0') {

                $(this).addClass('selected');

                var entity = rowToObject($(this));

                $('#hdnSelectedTrText').val(entity.SubjectName + '<br/><b>' + entity.TeacherName + '</b>');
                $('#hdnSelectedTrValue').val(entity.ClassSubjectID);
                $('#hdnSelectedTrTeacherID').val(entity.TeacherID);
                $('#hdnSelectedTrRoomID').val(entity.RoomID);
                $('#hdnSelectedTrRoomName').val(entity.RoomName);

                $('#tdSelectedSubject').html(entity.SubjectName);
                $('#tdSelectedTeacher').html("<label class='lblLink' id='lblTeacher'>" + entity.TeacherName + "</label>");
                pcClassSubject.Hide();
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
                $(this).html('<div style="float:right" class="divDetailDelete"></div>' + text + '<br/><label class="lblLink lblRoom">' + $('#hdnSelectedTrRoomName').val() + '</label>');

                $tr.find('.tdValue').html($('#hdnSelectedTrValue').val());
                $tr.find('.tdRoomID').html($('#hdnSelectedTrRoomID').val());

                $tdRemaining = $('#<%=grdView.ClientID %> tr.selected .tdRemaining');
                var remaining = parseFloat($tdRemaining.html());
                $tdRemaining.html(remaining - 1);
                if (remaining < 2) {
                    $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                    $('#hdnSelectedTrText').val('');
                    $('#hdnSelectedTrValue').val('');
                    $('#hdnSelectedTrTeacherID').val(''); 
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
                        $tdRemaining = $(this).find('.tdRemaining');
                        var remaining = parseFloat($tdRemaining.html());
                        $tdRemaining.html(remaining + 1);
                        isFound = true;
                    }
                }
            });
        });

        //#region Room
        function onGetRoomFilterExpression() {
            var filterExpression = "<%=OnGetRoomFilterExpression() %>";
            return filterExpression;
        }

        $td = null;
        $('.lblRoom.lblLink').die('click');
        $('.lblRoom.lblLink').live('click', function () {
            $tr = $(this).closest('tr');
            openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
                onTxtRoomChanged(value);
            });
        });

        function onTxtRoomChanged(value) {
            var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
            Methods.getObject('GetRoomList', filterExpression, function (result) {
                if (result != null) {
                    $tr.find('.tdRoomID').html(result.RoomID);
                    $tr.find('.lblRoom').html(result.RoomName);
                }
                else {
                    $tr.find('.tdRoomID').html('0');
                    $tr.find('.lblRoom').html('Pilih Ruangan');
                }
            });
        }
        //#endregion

        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var lstClassSubjectID = [];
                var lstRoomID = [];
                var lstDayNumber = [];
                var lstHoursIndex = [];
                $('.tblSchedule tr.T001').each(function () {
                    $tr = $(this);
                    var classSubjectID = $tr.find('.tdValue').html();
                    if (classSubjectID != '') {
                        var roomID = $tr.find('.tdRoomID').html();
                        var dayNumber = $tr.find('.tdDayNumber').html();
                        var hoursIndex = $tr.find('.tdHoursIndex').html();

                        lstClassSubjectID.push(classSubjectID);
                        lstDayNumber.push(dayNumber);
                        lstHoursIndex.push(hoursIndex);
                        lstRoomID.push(roomID);
                    }
                });
                $('#<%=hdnLstClassSubjectID.ClientID %>').val(lstClassSubjectID.join(','));
                $('#<%=hdnLstHoursIndex.ClientID %>').val(lstHoursIndex.join(','));
                $('#<%=hdnLstRoomID.ClientID %>').val(lstRoomID.join(','));
                $('#<%=hdnLstDayNumber.ClientID %>').val(lstDayNumber.join(','));

                onCustomButtonClick('save');
            });

            $('#lblChangeClassSubject').click(function () {
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
    <input type="hidden" runat="server" id="hdnLstClassSubjectID" />
    <input type="hidden" runat="server" id="hdnLstRoomID" />
    <input type="hidden" runat="server" id="hdnLstHoursIndex" />
    <input type="hidden" runat="server" id="hdnLstDayNumber" />

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
    <input type="hidden" id="hdnSelectedTrTeacherID" value="" />
    <input type="hidden" id="hdnSelectedTrRoomID" value="" />
    <input type="hidden" id="hdnSelectedTrRoomName" value="" />
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
            <td class="tdLabel"><label class="lblLink" id="lblChangeClassSubject" style="font-weight: bold;"><%=GetLabel("Dipilih") %></label> :</td>
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
                                    <asp:Repeater ID="rptDay1" runat="server" OnItemDataBound="rptDay1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Selasa") %></h4>
                                    <asp:Repeater ID="rptDay2" runat="server" OnItemDataBound="rptDay2_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Rabu") %></h4>
                                    <asp:Repeater ID="rptDay3" runat="server" OnItemDataBound="rptDay3_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Kamis") %></h4>
                                    <asp:Repeater ID="rptDay4" runat="server" OnItemDataBound="rptDay4_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Jumat") %></h4>
                                    <asp:Repeater ID="rptDay5" runat="server" OnItemDataBound="rptDay5_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
                                <td valign="top"> 
                                    <h4 style="text-align: center"><%=GetLabel("Sabtu") %></h4>
                                    <asp:Repeater ID="rptDay6" runat="server" OnItemDataBound="rptDay6_ItemDataBound">
                                        <HeaderTemplate>
                                            <table class="tblSchedule" cellpadding="0" cellspacing="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                                                <td style="display:none;" class="tdDefaultHtml"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                                                <td style="display:none;" class="tdValue" id="tdValue" runat="server"></td>
                                                <td style="display:none;" class="tdRoomID" id="tdRoomID" runat="server"></td>
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
        FooterText="" HeaderText="Daftar Pelajaran" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="800px"
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
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdView" OnRowDataBound="grdView_RowDataBound"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="SubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderText="Mata Pelajaran" HeaderStyle-Width="250px">
                                                <ItemTemplate>
                                                    <%#Eval("SubjectName")%>
                                                    <input type="hidden" value="<%#Eval("ClassSubjectID") %>" bindingfield="ClassSubjectID" />
                                                    <input type="hidden" value="<%#Eval("TeacherID") %>" bindingfield="TeacherID" />
                                                    <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
                                                    <input type="hidden" value="<%#Eval("SubjectName") %>" bindingfield="SubjectName" />
                                                    <input type="hidden" value="<%#Eval("RoomID") %>" bindingfield="RoomID" />
                                                    <input type="hidden" value="<%#Eval("RoomName") %>" bindingfield="RoomName" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TeacherName" HeaderText="Guru"/>
                                            <asp:BoundField DataField="NoMeetingHoursInWeek" HeaderText="Jumlah Jam Pertemuan" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="Sisa Jam Pertemuan" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <div id="tdRemaining" class="tdRemaining" runat="server"></div>
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