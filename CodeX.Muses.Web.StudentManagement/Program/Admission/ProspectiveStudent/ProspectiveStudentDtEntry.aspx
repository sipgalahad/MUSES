<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentDtEntry" %>

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
            //$("#ui-datepicker-div").wrap('<div style="position:absolute;top:0px;"></div>');

            setDatePicker('<%=txtRegistrationDate.ClientID %>');
            setDatePicker('<%=txtDateOfBaptism.ClientID %>');

            //#region DOB
            $('#<%=txtDOB.ClientID %>').change(function () {
                var age = Methods.getAgeFromDatePickerFormat($(this).val());
                $('#<%=txtAgeInYear.ClientID %>').val(age.years);
                $('#<%=txtAgeInMonth.ClientID %>').val(age.months);
                $('#<%=txtAgeInDay.ClientID %>').val(age.days);
            });

            setDatePicker('<%=txtDOB.ClientID %>');
            //#endregion

            registerCollapseExpandHandler();

            $('#btnNext').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry'))
                    cbpMPEntryProcess.PerformCallback('save');
            });

            onCboReligionValueChanged();
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

        function onCboReligionValueChanged() {
            if (cboReligion.GetValue() == "<%=OnGetReligionCatholic() %>") {
                $('#<%=txtPlaceOfBaptism.ClientID %>').removeAttr('readonly');
                $('#<%=txtDateOfBaptism.ClientID %>').removeAttr('readonly');
            }
            else {
                $('#<%=txtPlaceOfBaptism.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtDateOfBaptism.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtPlaceOfBaptism.ClientID %>').val('');
                $('#<%=txtDateOfBaptism.ClientID %>').val('');
            }
        }

        function onAfterSaveSuccess(retval) {
            if ($('#<%=hdnIsAdd.ClientID %>').val() == '1')
                parent.OnSetHdnID(retval);
            parent.OnNextButtonClick();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnInitial" runat="server" value="" />
    <input type="hidden" id="hdnSchoolType" runat="server" value="" />
    <input type="hidden" id="hdnIsAdd" runat="server" value="0" />
    <input type="hidden" id="hdnAddressPrefix" runat="server" value="" />
    <div style="height: 410px; overflow-y:auto">
        <fieldset id="fsMPEntry">            
            <table class="tblContentArea" >
                <colgroup>
                    <col style="width:50%"/>
                </colgroup>
                <tr>
                    <td style="padding:5px;vertical-align:top" rowspan="2">
                        <h4 class="h4expanded"><%=GetLabel("Informasi Pendaftaran")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Pendaftaran")%></label></td>
                                    <td><asp:TextBox ID="txtRegistrationNo" ReadOnly="true" Width="150px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%> - <%=GetLabel("Waktu")%></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtRegistrationDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                                <td style="width: 5px">&nbsp;</td>
                                                <td><asp:TextBox ID="txtRegistrationTime" Width="100px" CssClass="time" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Cara Pendaftaran")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboRegistrationType" Width="120px" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                                
                        <h4 class="h4expanded"><%=GetLabel("Data Siswa")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr style="display:none">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Salutation")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboSalutation" Width="120px" runat="server" /></td>
                                </tr>
                                <tr style="display:none">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboTitle" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama Siswa")%></label></td>
                                    <td>
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:49%"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtMiddleName" Width="100%" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Belakang")%></label></td>
                                    <td><asp:TextBox ID="txtLastName" Width="100%" runat="server" /></td>
                                </tr>
                                <tr style="display:none">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboSuffix" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Panggilan")%></label></td>
                                    <td><asp:TextBox ID="txtPreferredName" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Kelamin")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboGender" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tempat Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtBirthPlace" Width="100%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Lahir")%></label></td>
                                    <td><asp:TextBox ID="txtDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Umur")%> (yyyy-MM-dd)</label></td>
                                    <td>
                                        <table style="width:100%" cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:32%"/>
                                                <col style="width:3px"/>
                                                <col style="width:32%"/>
                                                <col style="width:3px"/>
                                                <col style="width:32%"/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtAgeInYear" CssClass="number" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtAgeInMonth" CssClass="number" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtAgeInDay" CssClass="number" Width="100%" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kewarganegaraan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboNationality" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Agama")%></label></td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cboReligion" ClientInstanceName="cboReligion" Width="120px" runat="server">
                                            <ClientSideEvents ValueChanged="function(s,e) { onCboReligionValueChanged(); }" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr id="trPlaceOfBaptism" runat="server">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tempat Baptis")%></label></td>
                                    <td><asp:TextBox ID="txtPlaceOfBaptism" Width="100%" runat="server" /></td>
                                </tr>
                                <tr id="trDateOfBaptism" runat="server">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal Baptis")%></label></td>
                                    <td><asp:TextBox ID="txtDateOfBaptism" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Feeder")%></label></td>
                                    <td><asp:CheckBox ID="chkIsFeeder" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="padding:5px;vertical-align:top">
                        <h4 class="h4expanded"><%=GetLabel("Data Kontak")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
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
                        </div>
                        <h4 class="h4expanded"><%=GetLabel("Alamat")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Address")%></label></td>
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
                        <h4 class="h4expanded"><%=GetLabel("Informasi Tambahan")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr id="trGrade" runat="server">
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kelas")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboGrade" Width="120px" runat="server" /></td>
                                </tr>
                                <tr id="trMajor" runat="server">
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jurusan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboMajor" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" valign="top" style="padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
                                    <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
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