<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="ProductBrandEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.ProductBrandEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            //#region Manufacturer
            $('#lblManufacturer.lblLink').click(function () {
                openSearchDialog('manufacturer', 'IsDeleted = 0', function (value) {
                    $('#<%=txtManufacturerCode.ClientID %>').val(value);
                    onTxtManufacturerCodeChanged(value);
                });
            });

            $('#<%=txtManufacturerCode.ClientID %>').change(function () {
                onTxtManufacturerCodeChanged($(this).val());
            });

            function onTxtManufacturerCodeChanged(value) {
                var filterExpression = "ManufacturerCode = '" + value + "'";
                Methods.getObject('GetManufacturerList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnManufacturerID.ClientID %>').val(result.ManufacturerID);
                        $('#<%=txtManufacturerName.ClientID %>').val(result.ManufacturerName);
                    }
                    else {
                        $('#<%=hdnManufacturerID.ClientID %>').val('');
                        $('#<%=txtManufacturerCode.ClientID %>').val('');
                        $('#<%=txtManufacturerName.ClientID %>').val('');
                    }
                });
            }
            //#endregion
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
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("ProductBrand Code")%></label></td>
                        <td><asp:TextBox ID="txtProductBrandCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("ProductBrand Name")%></label></td>
                        <td><asp:TextBox ID="txtProductBrandName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblLink lblMandatory" id="lblManufacturer"><%=GetLabel("Manufacturer")%></label></td>
                        <td>
                            <input type="hidden" id="hdnManufacturerID" value="" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtManufacturerCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtManufacturerName" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
