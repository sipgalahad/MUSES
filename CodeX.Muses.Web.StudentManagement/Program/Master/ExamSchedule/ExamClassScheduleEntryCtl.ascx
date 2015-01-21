<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamClassScheduleEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ExamClassScheduleEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
    });

    //#region Room
    function onGetRoomFilterExpression() {
        var filterExpression = "<%=OnGetRoomFilterExpression() %>";
        return filterExpression;
    }

    $td = null;
    $('.lblRoom.lblLink').die('click');
    $('.lblRoom.lblLink').live('click', function () {
        $td = $(this).parent();
        openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
            onTxtRoomChanged(value);
        });
    });

    function onTxtRoomChanged(value) {
        var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
        Methods.getObject('GetRoomList', filterExpression, function (result) {
            if (result != null) {
                $td.find('.hdnRoomID').val(result.RoomID);
                $td.find('.lblRoom').html(result.RoomName);
            }
            else {
                $td.find('.hdnRoomID').val('0');
                $td.find('.lblRoom').html('Pilih Ruangan');
            }
        });
    }
    //#endregion

    //#region Employee
    function onGetEmployeeFilterExpression() {
        var filterExpression = "<%=OnGetEmployeeFilterExpression() %>";
        return filterExpression;
    }

    $td = null;
    $('.lblEmployee.lblLink').die('click');
    $('.lblEmployee.lblLink').live('click', function () {
        $td = $(this).parent();
        openSearchDialog('employee', onGetEmployeeFilterExpression(), function (value) {
            onTxtEmployeeChanged(value);
        });
    });

    function onTxtEmployeeChanged(value) {
        var filterExpression = onGetEmployeeFilterExpression() + " AND EmployeeCode = '" + value + "'";
        Methods.getObject('GetEmployeeList', filterExpression, function (result) {
            if (result != null) {
                $td.find('.hdnEmployeeID').val(result.EmployeeID);
                $td.find('.lblEmployee').html(result.FullName);
            }
            else {
                $td.find('.hdnEmployeeID').val('0');
                $td.find('.lblEmployee').html('Pilih Pengawas');
            }
        });
    }
    //#endregion

    function onBeforeSaveRecord(errMessage) {
        var result = '';
        var lstID = '';
        $('#grdClassSchedule tr:gt(1)').each(function () {
            var tempResult = '';
            $(this).find('.hdnClassID').each(function () {
                if (tempResult != '')
                    tempResult += ';';
                var classID = $(this).val();
                var roomID = $(this).parent().find('.hdnRoomID').val();
                var emplyoeeID = $(this).parent().next().find('.hdnEmployeeID').val();
                if (emplyoeeID != '0')
                    tempResult += classID + ',' + roomID + ',' + emplyoeeID;
            });

            if (result != '') {
                result += '|';
                lstID += ',';
            }
            var subjectID = $(this).find('.keyField').html();
            result += subjectID + '^' + tempResult;
            lstID += subjectID;
        });
        $('#<%=hdnSaveValue.ClientID %>').val(result);
        $('#<%=hdnListID.ClientID %>').val(lstID);
        return true;
    } 
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnListID" value="" runat="server" />
    <input type="hidden" id="hdnSaveValue" value="" runat="server" />
    <input type="hidden" id="hdnID" value="" runat="server" />   
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Kelas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
    <table id="grdClassSchedule" class="grdSelected grdBorder" rules="all">
        <tr>
            <th class="keyField"></th>
            <th rowspan="2"><%=GetLabel("Mata Pelajaran") %></th>
            <th rowspan="2" class="thCenter" style="width:100px"><%=GetLabel("Tanggal") %></th>
            <th rowspan="2" class="thCenter" style="width:90px"><%=GetLabel("Jam") %></th>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th colspan="2" class="thCenter"><%#Eval("SchoolClassName") %></th>
                </ItemTemplate>            
            </asp:Repeater>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeaderDt" runat="server">
                <ItemTemplate>
                    <th style="width:80px" class="thCenter"><%=GetLabel("Ruangan") %></th>
                    <th style="width:150px" class="thCenter"><%=GetLabel("Pengawas") %></th>
                </ItemTemplate>            
            </asp:Repeater>
        </tr>
    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
        <ItemTemplate>
            <tr>
                <td class="keyField"><%#Eval("ExamScheduleDtID") %></td>
                <td><%#Eval("SubjectName")%></td>
                <td align="center"><%#Eval("ExamDate", "{0:dd-MMM-yyyy}")%></td>
                <td align="center"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
                <asp:Repeater ID="rptViewDt" runat="server" OnItemDataBound="rptViewDt_ItemDataBound">
                    <ItemTemplate>
                        <td align="center">
                            <input type="hidden" value='<%#Eval("SchoolClassID") %>' class="hdnClassID" />
                            <input type="hidden" class="hdnRoomID" id="hdnRoomID" runat="server" />
                            <label class="lblLink lblRoom" id="lblRoom" runat="server" ></label>
                        </td>
                        <td align="center">
                            <input type="hidden" class="hdnEmployeeID" id="hdnEmployeeID" runat="server" />
                            <label class="lblLink lblEmployee" id="lblEmployee" runat="server"></label>                        
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
</div>