<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="RevenuePeriodEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.RevenuePeriodEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
    <%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            $('#<%=txtStartDate.ClientID %>').datepicker();
            setDatePicker('<%=txtEndDate.ClientID %>');
            $('#<%=txtEndDate.ClientID %>').datepicker();
        }
    </script>
    
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                        <td><asp:TextBox ID="txtStartDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Berakhir")%></label></td>
                        <td><asp:TextBox ID="txtEndDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
