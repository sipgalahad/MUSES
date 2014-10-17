<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentHistoryInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentHistoryInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            registerViewListClickHandler();
            registerCollapseExpandHandler();
            if ($('#ulMeetingViewList li').length > 0)
                $('#ulMeetingViewList li:eq(0)').click();
        });

        function registerViewListClickHandler() {
            $('#ulMeetingViewList li').click(function (e) {
                if (!$(this).hasClass('selected')) {
                    var id = $(this).find('.hdnClassSubjectTaskID').val();
                    $('#<%=hdnClassSubjectTaskID.ClientID %>').val(id);
                    $('#ulMeetingViewList li.selected').removeClass('selected');
                    $(this).addClass('selected');
                    cbpMeetingDetail.PerformCallback();
                }
            });
        }
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnClassSubjectTaskID" runat="server" />
    <style type="text/css">
        #ulMeetingViewList .divMeetingDate        { float: left; width: 66px; margin: 3px 10px 0 0; background-color: #6BBD46; padding: 3px 10px; font-size: 20px; color: White; vertical-align: middle; text-align: center; }
        #ulMeetingViewList li                          { padding: 5px 3px; cursor: pointer; list-style-type:none; margin-bottom: 1px; }
        #ulMeetingViewList li.selected                 { background-color: #D5D5D5; }
        #ulMeetingViewList li:hover                    { background-color: #BCBCBC; }
        #ulMeetingViewList                             { margin: 0; padding: 0; }
        #ulMeetingViewList .tdMeetingDetail       { padding-left: 5px; }
    
        h4                                                  { color: #013EDD; }
    </style>
    <table style="width:100%">
        <colgroup>
            <col style="width:300px"/>
        </colgroup>
        <tr>
            <td valign="top">
                <ul id="ulMeetingViewList">      
                    <li>
                        <div style="font-size: 24px; font-weight: 100;"><%=GetLabel("Kelas X-1") %></div>
                        <div style="font-size: 12px;"><%=GetLabel("Semester 1") %></div>
                    </li>      
                    <li>
                        <div style="font-size: 24px; font-weight: 100;"><%=GetLabel("Kelas X-1")%></div>
                        <div style="font-size: 12px;"><%=GetLabel("Semester 2") %></div>
                    </li>    
                    <li>
                        <div style="font-size: 24px; font-weight: 100;"><%=GetLabel("Kelas XI-A1") %></div>
                        <div style="font-size: 12px;"><%=GetLabel("Semester 1") %></div>
                    </li>    
                    <li>
                        <div style="font-size: 24px; font-weight: 100;"><%=GetLabel("Kelas XI-A1")%></div>
                        <div style="font-size: 12px;"><%=GetLabel("Semester 2") %></div>
                    </li>  
                </ul>
            </td>
            <td valign="top">
                <h4><%=GetLabel("Nilai")%></h4>       
                <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                    <tr>
                        <th><%=GetLabel("Mata Pelajaran") %></th>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Nilai") %></th>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Matematika")%></td>
                        <td align="center"><label class="lblLink lblMark">90</label></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Fisika") %></td>
                        <td align="center"><label class="lblLink lblMark">80</label></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Bahasa Indonesia") %></td>
                        <td align="center"><label class="lblLink lblMark">95</label></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Bahasa Inggris") %></td>
                        <td align="center"><label class="lblLink lblMark">92</label></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Kimia") %></td>
                        <td align="center"><label class="lblLink lblMark">89</label></td>
                    </tr>
                    <tr>
                        <td><%=GetLabel("Biologi") %></td>
                        <td align="center"><label class="lblLink lblMark">90</label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>