<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPRProjectPageTrx.master" AutoEventWireup="true" 
    CodeBehind="RTimelineList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RTimelineList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
    </style>
        
    <script type="text/javascript">
        var currScroll = 1;
        var rowNumber = 15;
        var isRefresh = true;
        var dateTime = '';
        $(function () {
            refresh();
            $('div .divTimeline').scroll(function () {
                if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                    if (isRefresh) {
                        currScroll++;
                        addData();
                    }
                }
            });
        });

        function refresh() {
            var filterExpression = "HistoryID IN (SELECT HistoryID FROM (SELECT ROW_NUMBER() OVER (ORDER BY HistoryID DESC) AS 'RowNumber',HistoryID FROM RActivityHistory) a WHERE a.RowNumber BETWEEN " + (((currScroll - 1) * rowNumber) + 1) + " AND " + (currScroll * rowNumber) + ")";
            Methods.getListObject('GetvRActivityHistoryList', filterExpression, function (result) {
                if (result != null) {
                    if (result.length == 0) isRefresh = false;
                    for (var i = 0; i < result.length; i++) {
                        var remarks = result[i].CustomRemarks;
                        var user = result[i].Username;
                        var time = result[i].CreatedDateInDateTime;
                        dateTime = result[0].CreatedDateInDateTime;
                        $newTr = $('#tmplSelectedTestItem').html();
                        $newTr = $newTr.replace(/\$\{LogTime}/g, time);
                        $newTr = $newTr.replace(/\$\{Username}/g, user);
                        $newTr = $newTr.replace(/\$\{CustomRemarks}/g, remarks);
                        $('#tblTimeline').append($newTr);
                    }
                }
            });
        }
        
        function addData() {
            var filterExpression = "HistoryID IN (SELECT HistoryID FROM (SELECT ROW_NUMBER() OVER (ORDER BY HistoryID DESC) AS 'RowNumber',HistoryID FROM RActivityHistory WHERE CreatedDate <= '" + dateTime + "') a WHERE a.RowNumber BETWEEN " + (((currScroll - 1) * rowNumber) + 1) + " AND " + (currScroll * rowNumber) + ")";
            Methods.getListObject('GetRvActivityHistoryList', filterExpression, function (result) {
                if (result != null) {
                    if (result.length == 0) isRefresh = false;
                    for (var i = 0; i < result.length; i++) {
                        var remarks = result[i].CustomRemarks;
                        var user = result[i].Username;
                        var time = result[i].CreatedDateInDateTime;
                        $newTr = $('#tmplSelectedTestItem').html();
                        $newTr = $newTr.replace(/\$\{LogTime}/g, time);
                        $newTr = $newTr.replace(/\$\{Username}/g, user);
                        $newTr = $newTr.replace(/\$\{CustomRemarks}/g, remarks);
                        $('#tblTimeline').append($newTr);
                    }
                }
            });
        }
    </script>
    <script id="tmplSelectedTestItem" type="text/x-jquery-tmpl">
        <tr class="trActivityLog">
            <td>
                <div class="divActivityLog">
                    <label style="font-weight:bold;">${Username}</label><br/>
                    <label style="font-size:smaller;">${LogTime}</label><br/>
                    ${CustomRemarks}<br/>
                </div>
            </td>
        </tr>
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="divTimeline" id="divTimeline" style="height:500px; width:110%; overflow-y:auto; overflow-x:hidden;">
        <div style="width:50px; height:50px; position: fixed; top: 200px; right: 5px; display:none">
        </div>
        <table cellpadding="0" cellspacing="0" width="500px" id="tblTimeline" style="margin-left:400px; ">
            <tr>
                <td align="center"><label style="font-weight:bold; font-size:medium"><%=GetLabel("Timeline") %></label></td>
            </tr>
        </table>
    </div> 
</asp:Content>