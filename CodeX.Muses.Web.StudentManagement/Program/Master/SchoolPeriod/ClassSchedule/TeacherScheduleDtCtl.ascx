<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeacherScheduleDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherScheduleDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <style type="text/css">
        .tblScheduleTeacher                        { width: 100%; }
        .tblScheduleTeacher td                     { text-align: center; }
        .tblScheduleTeacher tr td                  { border: 1px solid #333; }
        .tblScheduleTeacher tr.T001                { height: 60px; cursor: pointer; }
        .tblScheduleTeacher tr.T001 b              { color: Red; }
        .tblScheduleTeacher tr.T001 td, .nts001    { background-color: #2FD933; }
    </style>
    <input type="hidden" id="Hidden1" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtTeacherCode" ReadOnly="true" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtTeacherName" ReadOnly="true" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
                        <table class="tblScheduleTeacher" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='T<%#Eval("cfDailyScheduleType") %>'>
                            <td style="display:none" id="tdClassSubjectID" runat="server" class="tdClassSubjectID"></td>
                            <td style="display:none" id="tdClassScheduleID" runat="server" class="tdClassScheduleID"></td>
                            <td id="tdHtmlText" runat="server"><%#Eval("StartTime") %> - <%#Eval("EndTime") %></td>
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
    <table style="width:100%">
        <colgroup>
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td valign="top">
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
            </td>
            <td valign="top" style="padding-left:300px;">
                <div style="font-weight: bold;"><%=GetLabel("Jumlah Jam Mengajar") %> : <span style="color: Red;" id="spnNumSlot" runat="server"></span></div>
            </td>
        </tr>
    </table>
</div>

