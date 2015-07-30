<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="CustomerEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.CustomerEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            //#region Province
            function onGetSCProvinceFilterExpression() {
                var filterExpression = "<%=OnGetSCProvinceFilterExpression() %>";
                return filterExpression;
            }

            $('#lblProvince.lblLink').click(function () {
                openSearchDialog('stdcode', onGetSCProvinceFilterExpression(), function (value) {
                    $('#<%=txtProvinceCode.ClientID %>').val(value);
                    onTxtProvinceCodeChanged(value);
                });
            });

            $('#<%=txtProvinceCode.ClientID %>').change(function () {
                onTxtProvinceCodeChanged($(this).val());
            });

            function onTxtProvinceCodeChanged(value) {
                var filterExpression = onGetSCProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=txtProvinceName.ClientID %>').val(result.StandardCodeName);
                    else {
                        $('#<%=txtProvinceCode.ClientID %>').val('');
                        $('#<%=txtProvinceName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Zip Code
            $('#lblZipCode.lblLink').click(function () {
                openSearchDialog('zipcodes', 'IsDeleted = 0', function (value) {
                    $('#<%=txtZipCode.ClientID %>').val(value);
                    onTxtZipCodeChanged(value);
                });
            });

            $('#<%=txtZipCode.ClientID %>').change(function () {
                onTxtZipCodeChanged($(this).val());
            });

            function onTxtZipCodeChanged(value) {
                var filterExpression = "ZipCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvZipCodesList', filterExpression, function (result) {
                    if (result != null){
                        $('#<%=hdnZipCode.ClientID %>').val(result.ID);
                        $('#<%=txtCity.ClientID %>').val(result.City);
                        $('#<%=txtCounty.ClientID %>').val(result.County);
                        $('#<%=txtDistrict.ClientID %>').val(result.District);
                        $('#<%=txtCity.ClientID %>').val(result.City);
                        $('#<%=txtProvinceCode.ClientID %>').val(result.GCProvince.split('^')[1]);
                        $('#<%=txtProvinceName.ClientID %>').val(result.Province);
                    }
                    else {
                        $('#<%=hdnZipCode.ClientID %>').val('');
                        $('#<%=txtZipCode.ClientID %>').val('');
                        $('#<%=txtCity.ClientID %>').val('');
                        $('#<%=txtCounty.ClientID %>').val('');
                        $('#<%=txtDistrict.ClientID %>').val('');
                        $('#<%=txtCity.ClientID %>').val('');
                        $('#<%=txtProvinceCode.ClientID %>').val('');
                        $('#<%=txtProvinceName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Customer Bill To
            function onGetCustomerFilterExpression() {
                return "<%=OnGetCustomerFilterExpression() %>";
            }

            $('#lblCustomerBillTo.lblLink').click(function () {
                openSearchDialog('businesspartners', onGetCustomerFilterExpression(), function (value) {
                    $('#<%=txtCustomerBillToCode.ClientID %>').val(value);
                    onTxtCustomerBillToCodeChanged(value);
                });
            });

            $('#<%=txtCustomerBillToCode.ClientID %>').change(function () {
                onTxtCustomerBillToCodeChanged($(this).val());
            });

            function onTxtCustomerBillToCodeChanged(value) {
                var filterExpression = onGetCustomerFilterExpression() + " AND BusinessPartnerCode = '" + value + "'";
                Methods.getObject('GetBusinessPartnersList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnCustomerBillToID.ClientID %>').val(result.BusinessPartnerID);
                        $('#<%=txtCustomerBillToName.ClientID %>').val(result.BusinessPartnerName);
                    }
                    else {
                        $('#<%=hdnCustomerBillToID.ClientID %>').val('');
                        $('#<%=txtCustomerBillToCode.ClientID %>').val('');
                        $('#<%=txtCustomerBillToName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            registerCollapseExpandHandler();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnAddressPrefix" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Informasi Umum")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Instansi")%></label></td>
                            <td><uc1:MasterCodingCtl ID="ctlEntityCode" runat="server" /> </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Instansi")%></label></td>
                            <td><asp:TextBox ID="txtCustomerName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Singkat")%></label></td>
                            <td><asp:TextBox ID="txtShortName" Width="200px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Contact Person")%></label></td>
                            <td><asp:TextBox ID="txtContactPerson" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Informasi Instansi")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Base Site")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboSite" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nomor PKP")%></label></td>
                            <td><asp:TextBox ID="txtVATRegistrationNo" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jangka Waktu Pembayaran")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTerm" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Instansi")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboCustomerType" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblCustomerBillTo"><%=GetLabel("Tagihan Instansi")%></label></td>
                            <td>
                                <input type="hidden" value="" runat="server" id="hdnCustomerBillToID" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:100px"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtCustomerBillToCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtCustomerBillToName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Status Instansi")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:50%"/>
                        </colgroup>
                        <tr>
                            <td valign="top" style="display:none">
                                <table>
                                    <colgroup>
                                        <col style="width:10px"/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:CheckBox ID="chkIsDummy" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Dummy")%></td>
                                    </tr>
                                    <tr>
                                        <td><asp:CheckBox ID="chkIsCreditHold" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Pemegang Kredit")%></td>
                                    </tr>
                                    <tr>
                                        <td><asp:CheckBox ID="chkIsHasContract" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Perusahaan Kerjasama")%></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <colgroup>
                                        <col style="width:10px"/>
                                    </colgroup>
                                     <tr style="display:none">
                                        <td><asp:CheckBox ID="chkIsUsingDunningLetter" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Using Dunning Letter")%></td>
                                    </tr>
                                    <tr style="display:none">
                                        <td><asp:CheckBox ID="chkIsTaxable" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("PKP")%></td>
                                    </tr>
                                    <tr>
                                        <td><asp:CheckBox ID="chkIsBlacklist" Width="100%" runat="server" /></td>
                                        <td><%=GetLabel("Blacklist")%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
               
            </td>
            <td style="padding:5px;vertical-align:top">                
                <h4 class="h4expanded"><%=GetLabel("Alamat")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Jalan")%></label></td>
                            <td><asp:TextBox ID="txtAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblZipCode"><%=GetLabel("Kode Pos")%></label></td>
                            <td>
                                <input type="hidden" runat="server" id="hdnZipCode" value="" />
                                <asp:TextBox ID="txtZipCode" Width="100%" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelurahan / Desa")%></label></td>
                            <td><asp:TextBox ID="txtCounty" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kecamatan")%></label></td>
                            <td><asp:TextBox ID="txtDistrict" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kota")%></label></td>
                            <td><asp:TextBox ID="txtCity" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblProvince"><%=GetLabel("Provinsi")%></label></td>
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:100px"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtProvinceCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtProvinceName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Telepon")%></label></td>
                            <td><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Informasi Lainnya")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="pnlCustomAttribute" runat="server">
                    <h4 class="h4expanded"><%=GetLabel("Atribut")%></h4>
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
