<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChiefComplaintEntryCtl.ascx.cs" 
    Inherits="QIS.Medinfras.Web.EMR.Program.PatientPage.ChiefComplaintEntryCtl" %>

<script type="text/javascript" id="dxss_chiefcomplaintentryctl">
    setDatePicker('<%=txtObservationDate.ClientID %>');

    $(function () {
        $('.ddlChief').change(function () {
            $tr = $(this).closest('tr');
            $txt = $tr.find('input');
            if ($(this).val().indexOf('^999') > -1)
                $txt.show();
            else
                $txt.hide();
        });

        $('.ddlChief').each(function () {
            $(this).change();
        });
    });
</script>
<div class="pageTitle"><%=GetLabel("Chief Complaint")%></div>
<div style="height:310px;overflow-y:scroll;">
    <input type="hidden" runat="server" id="hdnID" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top;">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:150px"/>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Date")%> - <%=GetLabel("Time")%></label></td>
                        <td><asp:TextBox ID="txtObservationDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                        <td><asp:TextBox ID="txtObservationTime" Width="80px" CssClass="time" runat="server" Style="text-align:center" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Chief Complaint")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtChiefComplaint" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Location")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtLocation" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Onset")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlOnset" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtOnset" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provocation")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlProvocation" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtProvocation" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Quality")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlQuality" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtQuality" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Severity")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlSeverity" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtSeverity" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Time")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlTime" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtTime" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Relieved By")%></label></td>
                        <td><asp:DropDownList CssClass="ddlChief" ID="ddlRelievedBy" Width="100%" runat="server" /></td>
                        <td><asp:TextBox ID="txtRelievedBy" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
