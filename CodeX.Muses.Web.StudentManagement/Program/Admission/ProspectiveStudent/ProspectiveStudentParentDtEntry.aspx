<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentParentDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentParentDtEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPFrame" runat="server">
    <script type="text/javascript">
        $(function () {
            registerCollapseExpandHandler();

            setDatePicker('<%=txtFatherDOB.ClientID %>');
            setDatePicker('<%=txtMotherDOB.ClientID %>');

            $('#btnNext').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry'))
                    cbpMPEntryProcess.PerformCallback('save');
            });

            $('#btnPrev').click(function () {
                parent.OnPrevButtonClick();
            });
        });

        function onAfterSaveSuccess(retval) {
            parent.OnNextButtonClick();
        }
        
        function onGetProvinceFilterExpression() {
            var filterExpression = "<%=OnGetProvinceFilterExpression() %>";
            return filterExpression;
        }
        function onGetZipCodeFilterExpression() {
            var filterExpression = "IsDeleted = 0";
            return filterExpression;
        }
        
        //#region Father Province
        function onTacFatherProvinceButtonSearchClick() {
            openSearchDialog('stdcode', onGetProvinceFilterExpression(), function (value) {
                var filterExpression = onGetProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null) {
                        tacFatherProvince.setValue(result.cfStandardCodeID);
                        tacFatherProvince.setText(result.StandardCodeName);
                    }
                    else {
                        tacFatherProvince.setValue('');
                        tacFatherProvince.setText('');
                    }
                });
            });
        }

        function onTacFatherProvinceValueChanged() {
        }
        //#endregion

        //#region Father ZipCode
        function onTacFatherZipCodeButtonSearchClick() {
            openSearchDialog('zipcodes', onGetZipCodeFilterExpression(), function (value) {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacFatherZipCode.setValue(result.ID);
                        tacFatherZipCode.setText(result.ZipCode);
                        entityToControlZipCodeFather(result);
                    }
                    else {
                        tacFatherZipCode.setValue('');
                        tacFatherZipCode.setText('');
                        entityToControlZipCodeFather(result);
                    }
                });
            });
        }

        function entityToControlZipCodeFather(result) {
            $('#<%=txtFatherAddress.ClientID %>').val(result.StreetName);
            $('#<%=txtFatherCounty.ClientID %>').val(result.County);
            $('#<%=txtFatherDistrict.ClientID %>').val(result.District);
            $('#<%=txtFatherCity.ClientID %>').val(result.City);
            var filterExpression = "StandardCodeID = '" + result.GCProvince + "'";
            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    tacFatherProvince.setValue(result1.cfStandardCodeID);
                    tacFatherProvince.setText(result1.StandardCodeName);
                }
                else {
                    tacFatherProvince.setValue('');
                    tacFatherProvince.setText('');
                }
            });
        }

        function onTacFatherZipCodeValueChanged() {
            var id = tacFatherZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ID = '" + id + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    entityToControlZipCodeFather(result);
                });
            }
        }
        //#endregion

        //#region Mother Province
        function onTacMotherProvinceButtonSearchClick() {
            openSearchDialog('stdcode', onGetProvinceFilterExpression(), function (value) {
                var filterExpression = onGetProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null) {
                        tacMotherProvince.setValue(result.cfStandardCodeID);
                        tacMotherProvince.setText(result.StandardCodeName);
                    }
                    else {
                        tacMotherProvince.setValue('');
                        tacMotherProvince.setText('');
                    }
                });
            });
        }

        function onTacMotherProvinceValueChanged() {
        }
        //#endregion

        //#region Mother ZipCode
        function onTacMotherZipCodeButtonSearchClick() {
            openSearchDialog('zipcodes', onGetZipCodeFilterExpression(), function (value) {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacMotherZipCode.setValue(result.ID);
                        tacMotherZipCode.setText(result.ZipCode);
                        entityToControlZipCodeMother(result);
                    }
                    else {
                        tacMotherZipCode.setValue('');
                        tacMotherZipCode.setText('');
                        entityToControlZipCodeMother(result);
                    }
                });
            });
        }

        function entityToControlZipCodeMother(result) {
            $('#<%=txtMotherAddress.ClientID %>').val(result.StreetName);
            $('#<%=txtMotherCounty.ClientID %>').val(result.County);
            $('#<%=txtMotherDistrict.ClientID %>').val(result.District);
            $('#<%=txtMotherCity.ClientID %>').val(result.City);
            var filterExpression = "StandardCodeID = '" + result.GCProvince + "'";
            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    tacMotherProvince.setValue(result1.cfStandardCodeID);
                    tacMotherProvince.setText(result1.StandardCodeName);
                }
                else {
                    tacMotherProvince.setValue('');
                    tacMotherProvince.setText('');
                }
            });
        }

        function onTacMotherZipCodeValueChanged() {
            var id = tacMotherZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ID = '" + id + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    entityToControlZipCodeMother(result);
                });
            }
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnAddressPrefix" runat="server" value="" />
    <input type="hidden" id="hdnIsAdd" runat="server" value="0" />
    <div style="height: 405px; overflow-y:auto">
        <fieldset id="fsMPEntry">            
            <table class="tblContentArea" >
                <colgroup>
                    <col style="width:50%"/>
                </colgroup>
                <tr>
                    <td style="padding:5px;vertical-align:top">
                        <h4 class="h4expanded"><%=GetLabel("Data Ayah")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherTitle" ClientInstanceName="cboFatherTitle" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Depan")%></label></td>
                                    <td>
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:49%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtFatherFirstName" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtFatherMiddleName" Width="100%" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Belakang")%></label></td>
                                    <td><asp:TextBox ID="txtFatherLastName" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherSuffix" ClientInstanceName="cboFatherSuffix" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tempat Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtFatherBirthPlace" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtFatherDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kewarganegaraan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherNationality" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Agama")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherReligion" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendidikan Terakhir")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherEducationLevel" Width="120px" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                        
                        <h4 class="h4expanded"><%=GetLabel("Pekerjaan Ayah")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtFatherJobOffice" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pekerjaan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboFatherGCJob" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jabatan")%></label></td>
                                    <td><asp:TextBox ID="txtFatherOccupation" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendapatan Bulanan")%></label></td>
                                    <td><asp:TextBox ID="txtFatherSalary" CssClass="txtCurrency" Width="100px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Alamat Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtFatherAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Desa / Kelurahan")%></label></td>
                                    <td><asp:TextBox ID="txtFatherCounty" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kecamatan")%></label></td>
                                    <td><asp:TextBox ID="txtFatherDistrict" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kota")%></label></td>
                                    <td><asp:TextBox ID="txtFatherCity" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provinsi")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacFatherProvince" ClientInstanceName="tacFatherProvince" MethodName="GetStandardCodeList" GetFilterExpressionFunction="onGetProvinceFilterExpression"
                                            SearchFields="StandardCodeName" TextField="StandardCodeName" ValueField="StandardCodeID" SearchText="${StandardCodeName} (<b>${cfStandardCodeID}</b>)" OrderByExpression="StandardCodeName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacFatherProvinceButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacFatherProvinceValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Pos")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacFatherZipCode" ClientInstanceName="tacFatherZipCode" MethodName="GetZipCodesList" GetFilterExpressionFunction="onGetZipCodeFilterExpression"
                                            SearchFields="ZipCode" TextField="ZipCode" ValueField="ID" SearchText="${ZipCode}" OrderByExpression="ZipCode">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacFatherZipCodeButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacFatherZipCodeValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telepon Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtFatherTelephoneNo" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="padding:5px;vertical-align:top">
                        <h4 class="h4expanded"><%=GetLabel("Data Ibu")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherTitle" ClientInstanceName="cboMotherTitle" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Depan")%></label></td>
                                    <td>
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:49%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtMotherFirstName" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtMotherMiddleName" Width="100%" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Belakang")%></label></td>
                                    <td><asp:TextBox ID="txtMotherLastName" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherSuffix" ClientInstanceName="cboMotherSuffix" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tempat Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtMotherBirthPlace" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtMotherDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kewarganegaraan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherNationality" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Agama")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherReligion" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendidikan Terakhir")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherEducationLevel" Width="120px" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                        
                        <h4 class="h4expanded"><%=GetLabel("Pekerjaan Ibu")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtMotherJobOffice" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pekerjaan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMotherGCJob" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jabatan")%></label></td>
                                    <td><asp:TextBox ID="txtMotherOccupation" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendapatan Bulanan")%></label></td>
                                    <td><asp:TextBox ID="txtMotherSalary" CssClass="txtCurrency" Width="100px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Alamat Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtMotherAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Desa / Kelurahan")%></label></td>
                                    <td><asp:TextBox ID="txtMotherCounty" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kecamatan")%></label></td>
                                    <td><asp:TextBox ID="txtMotherDistrict" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kota")%></label></td>
                                    <td><asp:TextBox ID="txtMotherCity" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provinsi")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacMotherProvince" ClientInstanceName="tacMotherProvince" MethodName="GetStandardCodeList" GetFilterExpressionFunction="onGetProvinceFilterExpression"
                                            SearchFields="StandardCodeName" TextField="StandardCodeName" ValueField="StandardCodeID" SearchText="${StandardCodeName} (<b>${cfStandardCodeID}</b>)" OrderByExpression="StandardCodeName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacMotherProvinceButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacMotherProvinceValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Pos")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacMotherZipCode" ClientInstanceName="tacMotherZipCode" MethodName="GetZipCodesList" GetFilterExpressionFunction="onGetZipCodeFilterExpression"
                                            SearchFields="ZipCode" TextField="ZipCode" ValueField="ID" SearchText="${ZipCode}" OrderByExpression="ZipCode">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacMotherZipCodeButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacMotherZipCodeValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telepon Kantor")%></label></td>
                                    <td><asp:TextBox ID="txtMotherTelephoneNo" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <input type="button" id="btnNext" value="Next" style="float:right" />
    <input type="button" id="btnPrev" value="Prev" />

    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpMPEntryProcess" runat="server" Width="100%" ClientInstanceName="cbpMPEntryProcess"
            ShowLoadingPanel="false" OnCallback="cbpMPEntryProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){
                showLoadingPanel();
            }" EndCallback="function(s,e){
                var result = s.cpResult.split('|');
                if(result[0] == 'save'){
                    if(result[1] == 'success'){
                        onAfterSaveSuccess(result[2]);
                    }
                    else
                        if(result[2] != '')
                            showToast('Save Failed', 'Error Message : ' + result[2]);
                        else
                            showToast('Save Failed', '');
                }
                hideLoadingPanel();
            }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>