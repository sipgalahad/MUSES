<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderCopyCtl.ascx.cs" 
    Inherits="CodeX.Ottimo.Web.Inventory.Program.PurchaseOrderCopyCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_purchaserequestqtyonorderctl">
    //#region Supplier
    function getSupplierFilterExpression() {
        var filterExpression = "<%=OnGetFilterExpressionSupplier() %>";
        return filterExpression;
    }

    $('#<%=lblSupplier.ClientID %>.lblLink').click(function () {
        openSearchDialog('businesspartners', getSupplierFilterExpression(), function (value) {
            $('#<%=txtSupplierCode.ClientID %>').val(value);
            onTxtSupplierChanged(value);
        });
    });

    $('#<%=txtSupplierCode.ClientID %>').change(function () {
        onTxtSupplierChanged($(this).val());
    });

    function onTxtSupplierChanged(value) {
        var filterExpression = getSupplierFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
        Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                cboTerm.SetValue(result.TermID);
            }
            else {
                $('#<%=hdnSupplierID.ClientID %>').val('');
                $('#<%=txtSupplierCode.ClientID %>').val('');
                $('#<%=txtSupplierName.ClientID %>').val('');
                cboTerm.SetSelectedIndex(0);
            }
        });
    }
    //#endregion
</script>
<input type="hidden" id="hdnPurchaseOrderID" runat="server" />
<div style="height:140px; overflow-y:auto;overflow-x: hidden">
    <table class="tblEntryContent" style="width: 100%">
        <colgroup>
            <col style="width: 30%" />
            <col />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblSupplier" runat="server"><%=GetLabel("Supplier/Penyedia")%></label></td>
            <td>
                <input type="hidden" value="" id="hdnSupplierID" runat="server" />
                <table cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width: 30%" />
                        <col style="width: 3px" />
                        <col style="width: 250px" />
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtSupplierCode" CssClass="required" ValidationGroup="mpEntry" Width="100%" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Waktu Pembayaran")%></label></td>
            <td><dxe:ASPxComboBox ID="cboTerm" ClientInstanceName="cboTerm" Width="300px" runat="server" /></td>
        </tr>
    </table>
</div>