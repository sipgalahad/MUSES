<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPMain.master" AutoEventWireup="true" 
    CodeBehind="StudentClassInformation.aspx.cs" Inherits="CodeX.Muses.Web.Mobile.Program.StudentClassInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <style type="text/css">
        #tblBill tr th, #tblBill tr td          { border: 1px solid #EAEAEA; font-size: 16px; padding: 2px 5px; word-wrap:break-word; }
        #tblBill tr th                          { background-color: #AAA; }
        #tblBill                                { table-layout: fixed; }
    </style>
    <input type="hidden" runat="server" id="hdnStudentID" />
    <div style="padding: 5px;" runat="server" id="divBill">
        <div style="font-size:16px;">Status Kehadiran : <span id="spnAttendanceStatus" runat="server">-</span></div>
        <table cellpadding="0" cellspacing="0" id="tblBill">
            <colgroup>
                <col style="width:100px"/>
                <col style="width:200px"/>
                <col style="width:200px"/>
                <col style="width:80px"/>
            </colgroup>
            <tr>
                <th class="thLeft">Mata Pelajaran</th>
                <th>Catatan</th>
                <th>Catatan Pertemuan Selanjutnya</th>
                <th>Kehadiran</th>
            </tr>
            <asp:Repeater ID="rptClassMeeting" runat="server" OnItemDataBound="rptClassMeeting_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("SubjectName") %></td>
                        <td><%#Eval("Remarks") %></td>
                        <td><%#Eval("NextMeetingRemarks")%></td>
                        <td><div id="divAttendanceStatus" runat="server">-</div></td>
                    </tr>    
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>