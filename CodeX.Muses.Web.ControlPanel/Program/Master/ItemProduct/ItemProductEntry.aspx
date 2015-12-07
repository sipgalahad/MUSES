<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="ItemProductEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.ItemProductEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            //#region Item Group
            $('#lblItemGroup.lblLink').click(function () {
                var filterExpression = "GCItemType = '" + $('#<%=hdnGCItemType.ClientID %>').val() + "' AND IsDeleted = 0";
                openSearchDialog('itemgroup', filterExpression, function (value) {
                    $('#<%=txtItemGroupCode.ClientID %>').val(value);
                    onTxtItemGroupCodeChanged(value);
                });
            });

            $('#<%=txtItemGroupCode.ClientID %>').change(function () {
                onTxtItemGroupCodeChanged($(this).val());
            });

            function onTxtItemGroupCodeChanged(value) {
                var filterExpression = "ItemGroupCode = '" + value + "'";
                Methods.getObject('GetItemGroupMasterList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName1);
                    }
                    else {
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                });
            }
            //#endregion        
            //#region Product Brand
            $('#lblProductBrand.lblLink').click(function () {
                openSearchDialog('productbrand', 'IsDeleted = 0', function (value) {
                    $('#<%=txtProductBrandCode.ClientID %>').val(value);
                    onTxtProductBrandCodeChanged(value);
                });
            });

            $('#<%=txtProductBrandCode.ClientID %>').change(function () {
                onTxtProductBrandCodeChanged($(this).val());
            });

            function onTxtProductBrandCodeChanged(value) {
                var filterExpression = "ProductBrandCode = '" + value + "'";
                Methods.getObject('GetProductBrandList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnProductBrandID.ClientID %>').val(result.ProductBrandID);
                        $('#<%=txtProductBrandName.ClientID %>').val(result.ProductBrandName);
                    }
                    else {
                        $('#<%=hdnProductBrandID.ClientID %>').val('');
                        $('#<%=txtProductBrandCode.ClientID %>').val('');
                        $('#<%=txtProductBrandName.ClientID %>').val('');
                    }
                });
            }
            //#endregion
            //#region Product Line
            $('#lblProductLine.lblLink').click(function () {
                openSearchDialog('productline', 'IsDeleted = 0', function (value) {
                    $('#<%=txtProductLineCode.ClientID %>').val(value);
                    onTxtProductLineCodeChanged(value);
                });
            });

            $('#<%=txtProductLineCode.ClientID %>').change(function () {
                onTxtProductLineCodeChanged($(this).val());
            });

            function onTxtProductLineCodeChanged(value) {
                var filterExpression = "ProductLineCode = '" + value + "'";
                Methods.getObject('GetProductLineList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnProductLineID.ClientID %>').val(result.ProductLineID);
                        $('#<%=txtProductLineName.ClientID %>').val(result.ProductLineName);
                    }
                    else {
                        $('#<%=hdnProductLineID.ClientID %>').val('');
                        $('#<%=txtProductLineCode.ClientID %>').val('');
                        $('#<%=txtProductLineName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            registerCollapseExpandHandler();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnGCItemType" runat="server" value="" />   
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Informasi Item")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Item")%></label></td>
                            <td><uc1:MasterCodingCtl ID="ctlEntityCode" runat="server" /> </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Item #1")%></label></td>
                            <td><asp:TextBox ID="txtItemName1" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Item #2")%></label></td>
                            <td><asp:TextBox ID="txtItemName2" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblProductBrand"><%=GetLabel("Merek Dagang")%></label></td>
                            <td>
                                <input type="hidden" id="hdnProductBrandID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtProductBrandCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtProductBrandName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink lblMandatory" id="lblItemGroup"><%=GetLabel("Kelompok Item")%></label></td>
                            <td>
                                <input type="hidden" id="hdnItemGroupID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtItemGroupCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtItemGroupName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Informasi Persediaan")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Status Item")%></label></td>            
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">                                    
                                    <colgroup>
                                        <col style="width:130px"/>
                                        <col style="width:3px"/>
                                        <col style="width:160px"/>
                                        <col/>
                                    </colgroup>
                                   <tr>
                                       <td><asp:CheckBox ID="chkIsInventoryItem" runat="server" /><%=GetLabel("Persediaan/Stock")%></td>
                                       <td>&nbsp</td>
                                       <td><asp:CheckBox ID="chkIsProductionItem" runat="server" /><%=GetLabel("Produksi/Pengemasan Kembali")%></td>
                                   </tr>
                               </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelompok A/B/C")%></label></td>
                            <td>
                                <asp:RadioButtonList ID="rblABCClass" runat="server" RepeatDirection="Horizontal" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Satuan Kecil")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboItemUnit" Width="130px" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Cycle Count Interval")%></label></td>
                            <td><asp:TextBox ID="txtCountInterval" Width="200px" CssClass="number required" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Transaction Restriction")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTransactionRestriction" Width="100%" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Batch Control")%></label></td>
                            <td><asp:CheckBox ID="chkIsBatchControl" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><asp:CheckBox runat="server" ID="chkIsControlExpired" Text="Kontrol Tanggal Kadaluarsa" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Informasi Keuangan")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblLink" id="lblProductLine"><%=GetLabel("Product Line")%></label></td>
                            <td>
                                <input type="hidden" id="hdnProductLineID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtProductLineCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtProductLineName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Markup")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboMarkup" runat="server" Width="100%" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Harga Eceran Tertinggi (HET)")%></label></td>
                            <td><asp:TextBox ID="txtSuggestedPrice" CssClass="txtCurrency" runat="server" Width="130px" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Prioritas Harga Dari HNA")%></label></td>
                            <td><asp:CheckBox ID="chkIsUsingStandardPrice" Width="100%" runat="server" /></td>
                        </tr>
                        <tr style="display:none">
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Margin")%> (%)</label></td>
                            <td><asp:TextBox ID="txtMargin" CssClass="number" runat="server" Width="130px" /></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4collapsed"><%=GetLabel("Informasi Lain-lain")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan Lain-lain")%></label></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="pnlCustomAttribute" runat="server">
                    <h4 class="h4expanded"><%=GetLabel("Custom Attribute")%></h4>
                    <asp:Repeater ID="rptCustomAttribute" runat="server">
                        <HeaderTemplate>
                            <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:30%"/>
                                </colgroup>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%# Eval("Value") %></label></td>
                                <td>
                                    <input type="hidden" value='<%# Eval("Code") %>' runat="server" id="hdnTagFieldCode" />
                                    <asp:TextBox ID="txtTagField" Width="300px" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
