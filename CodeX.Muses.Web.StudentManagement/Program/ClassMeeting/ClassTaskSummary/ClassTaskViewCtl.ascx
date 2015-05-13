<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskViewCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskViewCtl" %>

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

<input type="hidden" id="hdnClassMeetingID" runat="server" />
<input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
<input type="hidden" id="hdnSubjectIndicatorSave" runat="server" />
<input type="hidden" id="hdnID" runat="server" value="" />
<div>
    <table>
        <colgroup>
            <col style="width: 160px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
            <td><asp:TextBox ID="txtClassTaskCode" ReadOnly="true" Width="100px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Topik")%></label></td>
            <td><asp:TextBox ID="txtTopic" Width="200px" ReadOnly="true" runat="server" /></td>
        </tr>
        <tr id="trLessonType" runat="server">
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Pelajaran")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboLessonType" ClientEnabled="false" ClientInstanceName="cboLessonType" Width="200px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Tugas")%></label></td>
            <td><asp:TextBox ID="txtTaskType" ReadOnly="true" Width="200px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("% Bobot Nilai")%></label></td>
            <td><asp:TextBox ID="txtFinalMarkPercentage" ReadOnly="true" CssClass="number" Width="80px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Tugas")%></label></td>
            <td><asp:TextBox ID="txtTaskDate" ReadOnly="true" CssClass="datepicker" Width="120px" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Mulai")%></label></td>
            <td>    
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtStartDate" ReadOnly="true" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtStartTime" ReadOnly="true" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Selesai")%></label></td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:145px" />
                        <col style="width:5px" />
                        <col style="width:80px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtEndDate" ReadOnly="true" CssClass="datepicker" Width="120px" runat="server" /></td>    
                        <td align="center"></td>
                        <td><asp:TextBox ID="txtEndTime" ReadOnly="true" CssClass="time" Width="80px" runat="server" /></td>
                    </tr>
                </table>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
            <td><asp:TextBox runat="server" ID="txtRemarks" ReadOnly="true" TextMode="MultiLine" Rows="2" Width="300px" /></td>
        </tr>
        <tr>
            <td colspan="2"><h4><%=GetLabel("Indikator") %></h4></td>
        </tr>
        <tr style="display:none">
            <td>&nbsp;</td>
            <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Indikator")%></span><br /></td>
        </tr>
        <asp:Repeater ID="rptIndicator" runat="server">
            <ItemTemplate>
                <tr class="trSubjectIndicatorDt">
                    <td class="tdLabel"><%=GetLabel("Indikator") %></td>
                    <td><input type="text" class="txtSubjectIndicatorName" readonly="readonly" value='<%#Eval("SubjectIndicatorName") %>' style="width:440px;"/></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>

