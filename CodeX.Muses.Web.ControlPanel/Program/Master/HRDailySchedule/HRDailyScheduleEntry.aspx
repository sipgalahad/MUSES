<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="HRDailyScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.HRDailyScheduleEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><uc1:MasterCodingCtl ID="ctlEntityCode" runat="server" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtDailyScheduleName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Lama Jam Kerja")%></label></td>
                        <td><asp:TextBox ID="txtNoOfWorkHours" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h4>Jam  Mulai</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam")%></label></td>
                        <td><asp:TextBox CssClass="time" ID="txtFromHour" Width="80px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tenggang")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0" >
                                <tr>
                                    <td><asp:TextBox CssClass="time" ID="txtStartGraceTimeArrive" Width="80px" runat="server" /></td>
                                    <td align="center" style="width:10px;"><label ><%=GetLabel(" - ")%></label></td>
                                    <td><asp:TextBox CssClass="time" ID="txtEndGraceTimeArrive" Width="80px" runat="server" /></td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                   <tr>
                        <td colspan="2">
                            <h4>Jam  Pulang</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam")%></label></td>
                        <td><asp:TextBox CssClass="time" ID="txtToHour" Width="80px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tenggang")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0" >
                                <tr>
                                    <td><asp:TextBox CssClass="time" ID="txtStartGraceTimeDepart" Width="80px" runat="server" /></td>
                                    <td align="center" style="width:10px;"><label ><%=GetLabel(" - ")%></label></td>
                                    <td><asp:TextBox CssClass="time" ID="txtEndGraceTimeDepart" Width="80px" runat="server" /></td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
