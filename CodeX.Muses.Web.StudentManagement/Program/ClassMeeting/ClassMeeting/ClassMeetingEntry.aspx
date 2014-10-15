<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassMeetingEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassMeetingEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtMeetingDate.ClientID %>');

            $('#<%=btnSave.ClientID %>').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry'))
                    onCustomButtonClick('save');
            });
        });

        //#region Room
        function onGetRoomFilterExpression() {
            var filterExpression = "<%=OnGetRoomFilterExpression() %>";
            return filterExpression;
        }

        function onTacRoomButtonSearchClick() {
            openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
                var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
                Methods.getObject('GetRoomList', filterExpression, function (result) {
                    if (result != null) {
                        tacRoom.setValue(result.RoomID);
                        tacRoom.setText(result.RoomName);
                    }
                    else {
                        tacRoom.setValue('');
                        tacRoom.setText('');
                    }
                });
            });

        }

        function onTacRoomValueChanged() {
        }
        //#endregion

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetTeacherList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeacher.setValue(result.TeacherID);
                        tacTeacher.setText(result.TeacherName);
                    }
                    else {
                        tacTeacher.setValue('');
                        tacTeacher.setText('');
                    }
                });
            });

        }

        function onTacTeacherValueChanged() {
        }
        //#endregion
    </script>

    <table style="width:100%">
        <colgroup>
            <col style="width:130px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal") %></label></td>
            <td><asp:TextBox ID="txtMeetingDate" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam") %></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:80px" />
                        <col style="width:10px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>    
                        <td align="center"><%=GetLabel("s/d") %></td>
                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Ruangan")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRoom" ClientInstanceName="tacRoom" MethodName="GetRoomList" GetFilterExpressionFunction="onGetRoomFilterExpression"
                    SearchFields="RoomName,RoomID" TextField="RoomName" ValueField="RoomID" SearchText="${RoomName} (<b>${RoomID}</b>)" OrderByExpression="RoomName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacRoomButtonSearchClick(); }"
                        ValueChanged="function(){ onTacRoomValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan")%></label></td>
            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan Pertemuan Berikutnya")%></label></td>
            <td><asp:TextBox ID="txtNextMeetingRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="5" /></td>
        </tr>
    </table>
</asp:Content>