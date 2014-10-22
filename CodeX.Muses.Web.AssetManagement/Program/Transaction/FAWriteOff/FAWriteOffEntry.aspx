<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/FixedAssetPage/MPBaseFixedAssetPageTrx.master" AutoEventWireup="true" 
    CodeBehind="FAWriteOffEntry.aspx.cs" Inherits="QIS.Medinfras.Web.Accounting.Program.FAWriteOffEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Program/FixedAsset/FixedAssetToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhMPPatientPageNavigationPane" runat="server">
    <uc1:ToolbarCtl ID="ctlToolbar" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtFAWriteOffDate.ClientID %>');

            function getFAWriteOffDateFilterExpression(value) {
                var date = value.split('-');
                var filterExpression = "YEAR(DepreciationDate) = " + date[2] + " AND MONTH(DepreciationDate) = " + date[1];
                return filterExpression;
            }

            $('#<%=txtFAWriteOffDate.ClientID %>').change(function () {
                Methods.getObject('GetvFADepreciationList', getFAWriteOffDateFilterExpression($(this).val()), function (result) {
                    if (result != null) {
                        $('#<%=txtAssetValue.ClientID %>').val(result.ProcurementAmount).trigger('changeValue');
                        $('#<%=txtWriteOffAmount.ClientID %>').val(result.AssetValue).trigger('changeValue');
                        $('#<%=txtSelisih.ClientID %>').val(result.TotalDepreciationAmount).trigger('changeValue');
                        calculate();
                    }
                });
            });

            $('#<%=txtAssetValue.ClientID %>').change(function () {
                calculate();
            });

            $('#<%=txtWriteOffAmount.ClientID %>').change(function () {
                calculate();
            });
        }

        

        function calculate() {
            var assetValue = parseFloat($('#<%=txtAssetValue.ClientID %>').attr('hiddenVal'));
            var writeOffAmount = parseFloat($('#<%=txtWriteOffAmount.ClientID %>').attr('hiddenVal'));
            var selisih = assetValue - writeOffAmount;
            $('#<%=txtSelisih.ClientID %>').val(selisih).trigger('changeValue');
        }
    </script>
    <input type="hidden" id="hdnFAWriteOffID" runat="server" value="" />
    <input type="hidden" id="hdnFixedAssetID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Pemusnahan Aktiva Tetap")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col width="50%"/>
            <col />
        </colgroup>
        <tr>
            <td>
                <table width="100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("No. Pemusnahan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtFAWriteOffNo" Width="120px" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Pemusnahan") %></label></td>
                        <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtFAWriteOffDate" Width="120px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Pemusnahan") %></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboAssetWriteOffType" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Cara Penjualan") %></label></td>
                        <td><dxe:ASPxComboBox runat="server" ID="cboAssetSalesType" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nilai Buku") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtAssetValue" Width="120px" CssClass="txtCurrency" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Pemusnahan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtWriteOffAmount" Width="120px" CssClass="txtCurrency" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Selisih") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtSelisih" Width="120px" CssClass="txtCurrency" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" Width="220px" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
            <td valign ="top">
                <div style="margin-left:200px; padding:5px; border-color:Red; border-style:solid; border-width:1px">
                    <font color="red">PERHATIAN !!</font><br/>
                    Data pemusnahan tidak dapat dibatal posting.<br/>
                    Harap pastikan data pemusnahan yang Anda masukkan sudah benar.
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
