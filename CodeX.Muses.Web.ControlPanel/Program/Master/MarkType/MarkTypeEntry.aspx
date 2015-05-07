<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="MarkTypeEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.MarkTypeEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            onCboMarkTypeChanged();
        }

        function onCboMarkTypeChanged() {
            var GCMarkType = cboMarkType.GetValue();
            if (GCMarkType == "<%=GetMarkTypeNumber() %>") {
                $('#trMinValue').removeAttr('style');
                $('#trMaxValue').removeAttr('style');
            }
            else {
                $('#trMinValue').attr('style', 'display:none');
                $('#trMaxValue').attr('style', 'display:none');
            }
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
                        <col style="width:160px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td><asp:TextBox ID="txtMarkTypeCode" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtMarkTypeName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Nilai")%></label></td>
                        <td>
                            <dxe:ASPxComboBox id="cboMarkType" ClientInstanceName="cboMarkType" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboMarkTypeChanged() }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr id="trMinValue">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Min")%></label></td>
                        <td><asp:TextBox ID="txtMinValue" Width="100px" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr id="trMaxValue">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Max")%></label></td>
                        <td><asp:TextBox ID="txtMaxValue" Width="100px" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px"><label class="lblNormal"><%=GetLabel("Keterangan")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
