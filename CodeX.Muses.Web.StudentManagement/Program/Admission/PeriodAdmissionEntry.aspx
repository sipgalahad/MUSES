<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="PeriodAdmissionEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodAdmissionEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');
            setDatePicker('<%=txtRegistrationStartDate.ClientID %>');
            setDatePicker('<%=txtRegistrationEndDate.ClientID %>');
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" value="" />
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
                        <td class="tdLabel"><label><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtPeriodAdmissionCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtPeriodAdmissionName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Seleksi")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:145px" />
                                    <col style="width:5px" />
                                    <col style="width:145px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                                    <td align="center">-</td>
                                    <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Pendaftaran")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:145px" />
                                    <col style="width:5px" />
                                    <col style="width:145px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtRegistrationStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                                    <td align="center">-</td>
                                    <td><asp:TextBox ID="txtRegistrationEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
