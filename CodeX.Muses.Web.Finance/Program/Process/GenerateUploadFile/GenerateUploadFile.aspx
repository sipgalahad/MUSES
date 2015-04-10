<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="GenerateUploadFile.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.GenerateUploadFile" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#btnGenerate').click(function () {
                $('#<%=btnExport.ClientID%>').click();
            });
        })

        function setStartEndPeriod() {
            var pad = "00";
            var date = new Date();
            var firstDay = new Date(cboYear.GetValue(), cboMonth.GetValue() - 1, 1);
            var lastDay = new Date(cboYear.GetValue(), cboMonth.GetValue(), 0);
            var fpMonth = pad.substring(0, pad.length - (firstDay.getMonth() + 1).toString().length) + (firstDay.getMonth() + 1).toString();
            var epMonth = pad.substring(0, pad.length - (lastDay.getMonth() + 1).toString().length) + (lastDay.getMonth() + 1).toString();
            var endDate = lastDay.getDate() + '-' + epMonth + '-' + lastDay.getFullYear();
            var firstDate = '0' + firstDay.getDate() + '-' + fpMonth + '-' + firstDay.getFullYear();
            $('#<%=txtStartDate.ClientID %>').val(firstDate);
            $('#<%=txtEndDate.ClientID %>').val(endDate);
        }
    </script>
    <div>
        <div style="display:none;">
            <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
            <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
        </div>
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bank")%></label></td>
                <td><dxe:ASPxComboBox ID="cboBank" ClientInstanceName="cboBank" Width="120px" runat="server" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Bulan") %></td>
                <td style="padding-right: 1px; width: 140px">
                    <table cellpadding="0" cellspacing="0" >
                        <colgroup>
                            <col width="120px" />
                            <col width="70px" />
                            <col width="120px" />
                        </colgroup>
                        <tr>
                            <td class="tdMonth">
                                <dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" Width="120px">
                                    <ClientSideEvents ValueChanged="function(s,e){setStartEndPeriod()}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tahun")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboYear" runat="server" ClientInstanceName="cboYear" Width="120px" >
                                    <ClientSideEvents ValueChanged="function(s,e){setStartEndPeriod()}" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Periode") %></td>
                <td>
                    <table cellpadding="0" cellspacing="0" >
                        <colgroup>
                            <col width="100px" />
                            <col width="30px" />
                            <col width="100px" />
                        </colgroup>
                        <tr>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtStartDate" Width="100px" CssClass="datepicker" runat="server" /></td>
                            <td>s/d</td>
                            <td style="padding-right: 1px; width: 140px"><asp:TextBox ID="txtEndDate" Width="100px" CssClass="datepicker" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td><input type="button" id="btnGenerate" value="Generate" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
