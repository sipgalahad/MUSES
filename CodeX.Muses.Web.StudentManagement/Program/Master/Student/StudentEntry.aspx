<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="StudentEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            
        });

        //#region ZipCode
        function onGetZipCodeFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }
                 //onTacZipCodeButtonSearchClick 
        function onTacZipCodeButtonSearchClick() {
            openSearchDialog('zipcodes', onGetZipCodeFilterExpression(), function (value) {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacZipCode.setValue(result.ID);
                        tacZipCode.setText(result.ZipCode);
                        entityToControlZipCode(result);
                    }
                    else {
                        tacZipCode.setValue('');
                        tacZipCode.setText('');
                        entityToControlZipCode(result);
                    }
                });
            });
        }

        function entityToControlZipCode(result) {
            $('#<%=txtAddress.ClientID %>').val(result.StreetName);
            $('#<%=txtCounty.ClientID %>').val(result.County);
            $('#<%=txtDistrict.ClientID %>').val(result.District);
            $('#<%=txtCity.ClientID %>').val(result.City);
            var filterExpression = "StandardCodeID = '" + result.GCProvince+"' AND IsDeleted = 0 AND IsActive = 1";

            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    var gcProvinceCode = result1.StandardCodeID;
                    var code = gcProvinceCode.split('^');
                    $('#<%=txtProvinceCode.ClientID %>').val(code[1]);
                    $('#<%=txtProvinceName.ClientID %>').val(result1.StandardCodeName);
                }
                else {
                    $('#<%=txtProvinceCode.ClientID %>').val('');
                    $('#<%=txtProvinceName.ClientID %>').val('');
                }
            });
        }

        function onTacZipCodeValueChanged() {
            var id = tacZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacZipCode.setValue(result.ZipCode);
                        tacZipCode.setText(result.ZipCode);
                        entityToControlZipCode(result);
                    }
                    else {
                        tacZipCode.setValue('');
                        tacZipCode.setText('');
                        entityToControlZipCode(result);
                    }
                });
            }
        }
        //#endregion

    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea" >
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top" rowspan="2">
                <h4 class="h4expanded"><%=GetLabel("Data Student")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                            <td><asp:TextBox ID="txtStudentCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Salutation")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCSalutation" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Suffix")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCSuffix" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCStudentStatus" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Title")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCTitle" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Pasien")%></label></td>
                            <td>
                                <table>
                                    <tr>
                                        <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                        <td><asp:TextBox ID="txtMiddleName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Belakang")%></label></td>
                            <td><asp:TextBox ID="txtLastName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Panggilan")%></label></td>
                            <td><asp:TextBox ID="txtPreferredName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tempat Lahir")%></label></td>
                            <td><asp:TextBox ID="txtBirthPlace" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Lahir")%></label></td>
                            <td><asp:TextBox ID="txtDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kewarganegaraan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCNationality" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Grade")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCGrade" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Major")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCMajor" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("File Photo")%></label></td>
                            <td><asp:TextBox ID="txtPictureFileName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Address")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:100px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Address")%></label></td>
                        <td><asp:TextBox ID="txtAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("County")%></label></td>
                        <td><asp:TextBox ID="txtCounty" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("District")%></label></td>
                        <td><asp:TextBox ID="txtDistrict" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("City")%></label></td>
                        <td><asp:TextBox ID="txtCity" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal" id="lblProvince"><%=GetLabel("Province")%></label></td>
                        <td>
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:30%"/>
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
                        <td class="tdLabel"><label class="lblNormal" id="lblZipCode"><%=GetLabel("ZipCode")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacZipCode" ClientInstanceName="tacZipCode" MethodName="GetZipCodeList" GetFilterExpressionFunction="IsDeleted = 0"
                                SearchFields="ZipCode" TextField="ZipCode" ValueField="ZipCode" SearchText="${StreetName} (<b>${ZipCode}</b>)" OrderByExpression="StreetName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacZipCodeButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacZipCodeValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telephone")%></label></td>
                        <td><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Data Kontak")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:35%"/>
                        <col style="width:65%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Email 1")%></label></td>
                        <td><asp:TextBox ID="txtEmailAddress1" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Email 2")%></label></td>
                        <td><asp:TextBox ID="txtEmailAddress2" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Telp 1")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhoneNo1" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Telp 2")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhoneNo2" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
