<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="MyRProjectSummaryList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.MyRProjectSummaryList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
    </script>
    <style>
        b               { color: Maroon; }
        h5              { margin: 0; font-weight: bold; font-size: 16px !important; }
    </style>
    <h5><%=GetLabel("Tugas Baru") %></h5>
    <table cellspacing="0" cellpadding="0">
        <colgroup>
            <col style="width:20px"/>
            <col style="width:800px"/> 
        </colgroup>
        <asp:Repeater ID="rptNewTask" runat="server">
            <ItemTemplate>
                <tr>
                    <td>-</td>
                    <td>dari <%#Eval("CreatedByName") %> : <label class="lblLink"><%#Eval("ProjectTaskName") %></label>. Deadline <b><%#Eval("EndDate","{0:dd-MMM-yyyy}") %></b>. (Project <label class="lblLink"><%#Eval("ProjectName") %></label>, Kelompok Tugas <label class="lblLink"><%#Eval("ProjectTaskGroupName") %></label>)</td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
    <h5><%=GetLabel("Tugas Pending") %></h5>
</asp:Content>