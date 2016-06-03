<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentMarkPerIndicatorDtViewCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentMarkPerIndicatorDtViewCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    
</script>

<input type="hidden" id="hdnStudentID" runat="server" value="" />
<input type="hidden" id="hdnCurriculumMarkTypeID" runat="server" value="" />
<input type="hidden" id="hdnSummaryType" runat="server" />
<div style="overflow-y: scroll; height: 440px">
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Siswa")%></label></td>
            <td><asp:TextBox ID="txtHeaderName" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>  
    </table>
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="2" style="width:20px" class="thCenter">No.</th>
            <th rowspan="2">Indikator</th>
            <th id="tdClassTask" runat="server" class="thCenter">Tugas</th>
            <th rowspan="2" style="width:80px;" class="thCenter" id="thFinalMarkHeader" runat="server"></th>
        </tr>
        <tr>            
            <asp:Repeater ID="rptClassTaskHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width: 80px">
                        <input type="hidden" class="hdnClassSubjectTaskID" value='<%#Eval("ClassSubjectTaskID") %>' />
                        <label><%#Eval("ClassTaskCode")%></label><br />
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="center"><%# Container.ItemIndex + 1 %></td>
                    <td><%#Eval("SubjectIndicatorName")%></td>
                    <asp:Repeater ID="rptClassTask" runat="server" OnItemDataBound="rptClassTask_ItemDataBound">
                        <ItemTemplate>
                            <td class="thCenter">
                                <div id="divClassTask" runat="server"></div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <td id="tdFinalMark" runat="server" align="center"></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>

