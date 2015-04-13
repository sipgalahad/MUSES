<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="TeacherEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.TeacherEntry" %>

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
            setDatePicker('<%=txtHiredDate.ClientID %>');
            setDatePicker('<%=txtTerminatedDate.ClientID %>');            
        });
        //#region Province
        function onGetProvinceFilterExpression() {
            var filterExpression = "<%=OnGetProvinceFilterExpression() %>";
            return filterExpression;
        }

        function onTacProvinceButtonSearchClick() {
            openSearchDialog('stdcode', onGetProvinceFilterExpression(), function (value) {
                var filterExpression = onGetProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null) {
                        tacProvince.setValue(result.cfStandardCodeID);
                        tacProvince.setText(result.StandardCodeName);
                    }
                    else {
                        tacProvince.setValue('');
                        tacProvince.setText('');
                    }
                });
            });
        }

        function onTacProvinceValueChanged() {
        }
        //#endregion

        //#region ZipCode
        function onGetZipCodeFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

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
            var filterExpression = "StandardCodeID = '" + result.GCProvince + "'";
            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    tacProvince.setValue(result1.cfStandardCodeID);
                    tacProvince.setText(result1.StandardCodeName);
                }
                else {
                    tacProvince.setValue('');
                    tacProvince.setText('');
                }
            });
        }

        function onTacZipCodeValueChanged() {
            var id = tacZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ID = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    entityToControlZipCode(result);
                });
            }
        }
        //#endregion

        //#region Room
        function onGetRoomFilterExpression() {
            var filterExpression = "<%=OnGetRoomFilterExpression() %>";
            return filterExpression;
        }

        function onTacRoomButtonSearchClick() {
            openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
                var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
                Methods.getObject('GetRoomList', filterExpression, function (result) {
                    if (result != null) {
                        tacRoom.setValue(result.RoomID);
                        tacRoom.setText(result.RoomName);
                    }
                    else {
                        tacRoom.setValue('');
                        tacRoom.setText('');
                    }
                });
            });

        }

        function onTacRoomValueChanged() {
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnAddressPrefix" runat="server" value="" />
    <table class="tblContentArea" >
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top" rowspan="2">
                <h4 class="h4expanded"><%=GetLabel("Data Guru")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode Karyawan")%></label></td>
                            <td><asp:TextBox ID="txtTeacherCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Inisial")%></label></td>
                            <td><asp:TextBox ID="txtInitial" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Sapaan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCSalutation" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCTitle" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Depan")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                        <td style="width: 5px"></td>
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCSuffix" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Ruangan")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRoom" ClientInstanceName="tacRoom" MethodName="GetRoomList" GetFilterExpressionFunction="onGetRoomFilterExpression"
                                    SearchFields="RoomName,RoomCode" TextField="RoomName" ValueField="RoomID" SearchText="${RoomName} (<b>${RoomCode}</b>)" OrderByExpression="RoomName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacRoomButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacRoomValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Kelamin")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGender" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Alamat")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:180px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Alamat")%></label></td>
                            <td><asp:TextBox ID="txtAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Desa / Kelurahan")%></label></td>
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provinsi")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacProvince" ClientInstanceName="tacProvince" MethodName="GetStandardCodeList" GetFilterExpressionFunction="onGetProvinceFilterExpression"
                                    SearchFields="StandardCodeName" TextField="StandardCodeName" ValueField="StandardCodeID" SearchText="${StandardCodeName} (<b>${cfStandardCodeID}</b>)" OrderByExpression="StandardCodeName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacProvinceButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacProvinceValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Pos")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacZipCode" ClientInstanceName="tacZipCode" MethodName="GetZipCodesList" GetFilterExpressionFunction="onGetZipCodeFilterExpression"
                                    SearchFields="ZipCode" TextField="ZipCode" ValueField="ID" SearchText="${ZipCode}" OrderByExpression="ZipCode">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacZipCodeButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacZipCodeValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telepon")%></label></td>
                            <td><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top" rowspan="2">
                <h4 class="h4expanded"><%=GetLabel("Data Kontak")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
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
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Ext. Kantor")%></label></td>
                            <td><asp:TextBox ID="txtOfficeExtension" Width="220px" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Data Karyawan")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bagian")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCDepartment" ClientInstanceName="cboGCDepartment" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jabatan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCOccupation" ClientInstanceName="cboGCOccupation" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Golongan")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCOccupationLevel" ClientInstanceName="cboGCOccupationLevel" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Diterima")%></label></td>
                            <td><asp:TextBox ID="txtHiredDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Terakhir Bekerja")%></label></td>
                            <td><asp:TextBox ID="txtTerminatedDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("NPWP")%></label></td>
                            <td><asp:TextBox ID="txtVATRegistrationNo" Width="220px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGCEmployeeStatus" ClientInstanceName="cboGCEmployeeStatus" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                        
                <h4 class="h4expanded"><%=GetLabel("Informasi Lain")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("File Foto")%></label></td>
                            <td><asp:TextBox ID="txtPictureFileName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
