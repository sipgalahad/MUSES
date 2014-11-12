<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemPlanningEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanelHQ.Program.ItemPlanningEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        //#region Supplier
        function getSupplierFilterExpression() {
            var filterExpression = "<%=OnGetFilterExpressionSupplier() %>";
            return filterExpression;
        }

        $('#lblSupplier.lblLink').live('click', function () {
            openSearchDialog('businesspartners', getSupplierFilterExpression(), function (value) {
                $('#<%=txtSupplierCode.ClientID %>').val(value);
                onTxtSupplierCodeChanged(value);
            });
        });

        $('#<%=txtSupplierCode.ClientID %>').live('change', function () {
            onTxtSupplierCodeChanged($(this).val());
        });

        function onTxtSupplierCodeChanged(value) {
            var filterExpression = getSupplierFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
            Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnSupplierID.ClientID %>').val(result.BusinessPartnerID);
                    $('#<%=txtSupplierName.ClientID %>').val(result.BusinessPartnerName);
                }
                else {
                    $('#<%=hdnSupplierID.ClientID %>').val('');
                    $('#<%=txtSupplierCode.ClientID %>').val('');
                    $('#<%=txtSupplierName.ClientID %>').val('');
                }
            });
        }
        //#endregion
    });
</script>

<input type="hidden" id="hdnItemPlanningID" value="" runat="server" />
<input type="hidden" id="hdnItemID" value="" runat="server" />
<table class="tblContentArea">
    <colgroup>
        <col style="width:100%"/>
    </colgroup>
    <tr>            
        <td style="padding:5px;vertical-align:top">
            <fieldset id="fsEntryPopup" style="margin:0"> 
                <table class="tblEntryContent" >
                    <colgroup>
                        <col style="width:160px"/>
                        <col style="width:120px"/>
                        <col style="width:20px"/>
                        <col style="width:120px"/>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Item")%></label></td>
                        <td colspan="4"><asp:TextBox ID="txtItemName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>  
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Site")%></label></td>
                        <td colspan="3"><asp:TextBox ID="txtSiteName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr> 
                    <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblSupplier"><%=GetLabel("Supplier")%></label></td>
                        <td colspan="4">
                            <input type="hidden" value="" runat="server" id="hdnSupplierID" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtSupplierCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtSupplierName" ReadOnly="true" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Purchase Unit")%></label></td>
                        <td colspan="3"><dxe:ASPxComboBox ID="cboPurchaseUnit" Width="100%" runat="server" /></td>
                    </tr> 
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Lead Time")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtLeadTime" Width="50px" CssClass="number" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tolerance Qty")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtTolerance" Width="100%" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Safety Time")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtSafetyTime" Width="50px" CssClass="number" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Time Fence")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtTimeFence" Width="50px" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Safety Stock")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtSafetyStock" Width="100%" CssClass="number" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Standard Price")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtBasePrice" Width="100%" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Min Order Qty")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtMinOrderQty" Width="100%" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Max Order Qty")%></label></td>
                        <td class="tdLabel"><asp:TextBox ID="txtMaxOrderQty" Width="100%" CssClass="number" runat="server" /></td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>

