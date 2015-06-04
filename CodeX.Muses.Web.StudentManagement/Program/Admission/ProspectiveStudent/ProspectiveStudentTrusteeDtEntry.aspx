<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentTrusteeDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentTrusteeDtEntry" %>

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
            $('#btnNext').click(function () {
                parent.OnNextButtonClick();
            });

            $('#btnPrev').click(function () {
                parent.OnPrevButtonClick();
            });

            setDatePicker('<%=txtDOB.ClientID %>');

            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                cboFamilyRelation.SetValue('');
                cboTitle.SetValue('');
                $('#<%=txtFirstName.ClientID %>').val('');
                $('#<%=txtMiddleName.ClientID %>').val('');
                $('#<%=txtLastName.ClientID %>').val('');
                $('#<%=txtBirthPlace.ClientID %>').val('');
                $('#<%=txtDOB.ClientID %>').val('');
                cboSuffix.SetValue('');
                cboNationality.SetValue('');
                cboReligion.SetValue('');
                cboEducationLevel.SetValue('');
                cboGender.SetValue('');
                $('#entryDetailContainer').show();
            });

            $('#<%=chkIsTrusteeAddressSameWithStudent.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    $('#<%=txtTrusteeAddress.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtTrusteeCounty.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtTrusteeDistrict.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtTrusteeCity.ClientID %>').attr('readonly', 'readonly');
                    tacTrusteeProvince.setEnabled(false);
                    tacTrusteeZipCode.setEnabled(false);
                    $('#<%=txtTrusteeTelephoneNo.ClientID %>').attr('readonly', 'readonly');

                    $('#<%=txtTrusteeAddress.ClientID %>').val($('#<%=hdnStudentStreet.ClientID %>').val());
                    $('#<%=txtTrusteeCounty.ClientID %>').val($('#<%=hdnStudentCounty.ClientID %>').val());
                    $('#<%=txtTrusteeDistrict.ClientID %>').val($('#<%=hdnStudentDistrict.ClientID %>').val());
                    $('#<%=txtTrusteeCity.ClientID %>').val($('#<%=txtTrusteeCity.ClientID %>').val());
                    $('#<%=txtTrusteeTelephoneNo.ClientID %>').val($('#<%=hdnStudentTelephoneNo.ClientID %>').val());
                    tacTrusteeProvince.setValue($('#<%=hdnStudentGCProvince.ClientID %>').val());
                    tacTrusteeProvince.setText($('#<%=hdnStudentProvince.ClientID %>').val());
                    tacTrusteeZipCode.setValue($('#<%=hdnStudentZipCodeID.ClientID %>').val());
                    tacTrusteeZipCode.setText($('#<%=hdnStudentZipCode.ClientID %>').val());
                }
                else {
                    $('#<%=txtTrusteeAddress.ClientID %>').removeAttr('readonly');
                    $('#<%=txtTrusteeCounty.ClientID %>').removeAttr('readonly');
                    $('#<%=txtTrusteeDistrict.ClientID %>').removeAttr('readonly');
                    $('#<%=txtTrusteeCity.ClientID %>').removeAttr('readonly');
                    tacTrusteeProvince.setEnabled(true);
                    tacTrusteeZipCode.setEnabled(true);
                    $('#<%=txtTrusteeTelephoneNo.ClientID %>').removeAttr('readonly');
                }
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodAdmissionID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.FamilyID);
            cboFamilyRelation.SetValue(entity.GCFamilyRelation);
            cboTitle.SetValue(entity.GCTitle);
            $('#<%=txtFirstName.ClientID %>').val(entity.FirstName);
            $('#<%=txtMiddleName.ClientID %>').val(entity.MiddleName);
            $('#<%=txtLastName.ClientID %>').val(entity.LastName);
            $('#<%=txtBirthPlace.ClientID %>').val(entity.CityOfBirth);
            $('#<%=txtDOB.ClientID %>').val(entity.DateOfBirthInDatePickerFormat); 
            cboSuffix.SetValue(entity.GCSuffix);
            cboNationality.SetValue(entity.GCNationality);
            cboReligion.SetValue(entity.GCReligion);
            cboEducationLevel.SetValue(entity.GCEducationLevel);
            cboGender.SetValue(entity.GCGender);
            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        //#region Trustee Province
        function onTacTrusteeProvinceButtonSearchClick() {
            openSearchDialog('stdcode', onGetProvinceFilterExpression(), function (value) {
                var filterExpression = onGetProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null) {
                        tacTrusteeProvince.setValue(result.cfStandardCodeID);
                        tacTrusteeProvince.setText(result.StandardCodeName);
                    }
                    else {
                        tacTrusteeProvince.setValue('');
                        tacTrusteeProvince.setText('');
                    }
                });
            });
        }

        function onTacTrusteeProvinceValueChanged() {
        }
        //#endregion

        //#region Trustee ZipCode
        function onTacTrusteeZipCodeButtonSearchClick() {
            openSearchDialog('zipcodes', onGetZipCodeFilterExpression(), function (value) {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacTrusteeZipCode.setValue(result.ID);
                        tacTrusteeZipCode.setText(result.ZipCode);
                        entityToControlZipCodeTrustee(result);
                    }
                    else {
                        tacTrusteeZipCode.setValue('');
                        tacTrusteeZipCode.setText('');
                        entityToControlZipCodeTrustee(result);
                    }
                });
            });
        }

        function entityToControlZipCodeTrustee(result) {
            $('#<%=txtTrusteeAddress.ClientID %>').val(result.StreetName);
            $('#<%=txtTrusteeCounty.ClientID %>').val(result.County);
            $('#<%=txtTrusteeDistrict.ClientID %>').val(result.District);
            $('#<%=txtTrusteeCity.ClientID %>').val(result.City);
            var filterExpression = "StandardCodeID = '" + result.GCProvince + "'";
            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    tacTrusteeProvince.setValue(result1.cfStandardCodeID);
                    tacTrusteeProvince.setText(result1.StandardCodeName);
                }
                else {
                    tacTrusteeProvince.setValue('');
                    tacTrusteeProvince.setText('');
                }
            });
        }

        function onTacTrusteeZipCodeValueChanged() {
            var id = tacTrusteeZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ID = '" + id + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    entityToControlZipCodeTrustee(result);
                });
            }
        }
        //#endregion

        //#region TrusteeOffice Province
        function onTacTrusteeOfficeProvinceButtonSearchClick() {
            openSearchDialog('stdcode', onGetProvinceFilterExpression(), function (value) {
                var filterExpression = onGetProvinceFilterExpression() + " AND StandardCodeID LIKE '%^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null) {
                        tacTrusteeOfficeProvince.setValue(result.cfStandardCodeID);
                        tacTrusteeOfficeProvince.setText(result.StandardCodeName);
                    }
                    else {
                        tacTrusteeOfficeProvince.setValue('');
                        tacTrusteeOfficeProvince.setText('');
                    }
                });
            });
        }

        function onTacTrusteeOfficeProvinceValueChanged() {
        }
        //#endregion

        //#region TrusteeOffice ZipCode
        function onTacTrusteeOfficeZipCodeButtonSearchClick() {
            openSearchDialog('zipcodes', onGetZipCodeFilterExpression(), function (value) {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        tacTrusteeOfficeZipCode.setValue(result.ID);
                        tacTrusteeOfficeZipCode.setText(result.ZipCode);
                        entityToControlZipCodeTrusteeOffice(result);
                    }
                    else {
                        tacTrusteeOfficeZipCode.setValue('');
                        tacTrusteeOfficeZipCode.setText('');
                        entityToControlZipCodeTrusteeOffice(result);
                    }
                });
            });
        }

        function entityToControlZipCodeTrusteeOffice(result) {
            $('#<%=txtTrusteeOfficeAddress.ClientID %>').val(result.StreetName);
            $('#<%=txtTrusteeOfficeCounty.ClientID %>').val(result.County);
            $('#<%=txtTrusteeOfficeDistrict.ClientID %>').val(result.District);
            $('#<%=txtTrusteeOfficeCity.ClientID %>').val(result.City);
            var filterExpression = "StandardCodeID = '" + result.GCProvince + "'";
            Methods.getObject('GetStandardCodeList', filterExpression, function (result1) {
                if (result1 != null) {
                    tacTrusteeOfficeProvince.setValue(result1.cfStandardCodeID);
                    tacTrusteeOfficeProvince.setText(result1.StandardCodeName);
                }
                else {
                    tacTrusteeOfficeProvince.setValue('');
                    tacTrusteeOfficeProvince.setText('');
                }
            });
        }

        function onTacTrusteeOfficeZipCodeValueChanged() {
            var id = tacTrusteeOfficeZipCode.getValue();
            if (id != '') {
                var filterExpression = onGetZipCodeFilterExpression() + " AND ID = '" + id + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    entityToControlZipCodeTrusteeOffice(result);
                });
            }
        }
        //#endregion
    </script>
    <input type="hidden" runat="server" id="hdnID" />
    <input type="hidden" id="hdnHomeAddressPrefix" runat="server" value="" />
    <input type="hidden" id="hdnOfficeAddressPrefix" runat="server" value="" />
    <input type="hidden" id="hdnStudentAddressID" runat="server" value="0" />
    <input type="hidden" id="hdnStudentStreet" runat="server" value="0" />
    <input type="hidden" id="hdnStudentCounty" runat="server" value="0" />
    <input type="hidden" id="hdnStudentDistrict" runat="server" value="0" />
    <input type="hidden" id="hdnStudentCity" runat="server" value="0" />
    <input type="hidden" id="hdnStudentGCProvince" runat="server" value="0" />
    <input type="hidden" id="hdnStudentProvince" runat="server" value="0" />
    <input type="hidden" id="hdnStudentZipCodeID" runat="server" value="0" />
    <input type="hidden" id="hdnStudentZipCode" runat="server" value="0" />
    <input type="hidden" id="hdnStudentTelephoneNo" runat="server" value="0" />
    <div style="height: 410px; overflow-y:auto">
        <div class="divTransactionEntry">
            <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
            <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                <fieldset id="fsTrx" style="margin: 0">
                    <input type="hidden" value="" id="hdnEntryID" runat="server" />
                    <table style="width: 100%">
                        <colgroup>
                            <col style="width: 50%" />
                        </colgroup>
                        <tr>
                            <td valign="top">
                                <table>
                                    <colgroup>
                                        <col style="width: 180px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hubungan Keluarga")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboFamilyRelation" ClientInstanceName="cboFamilyRelation" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Depan")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboTitle" ClientInstanceName="cboTitle" Width="100%" runat="server" /></td>
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
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelar Belakang")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboSuffix" ClientInstanceName="cboSuffix" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jenis Kelamin")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboGender" ClientInstanceName="cboGender" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <colgroup>
                                        <col style="width: 180px" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tempat Lahir")%></label></td>
                                        <td><asp:TextBox ID="txtBirthPlace" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Lahir")%></label></td>
                                        <td><asp:TextBox ID="txtDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kewarganegaraan")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboNationality" ClientInstanceName="cboNationality" Width="120px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Agama")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboReligion" ClientInstanceName="cboReligion" Width="120px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendidikan Terakhir")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboEducationLevel" ClientInstanceName="cboEducationLevel" Width="120px" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <h4><%=GetLabel("Alamat Rumah")%></h4>
                                <table class="tblEntryContent" style="width:100%">
                                    <colgroup>
                                        <col style="width:180px"/>
                                    </colgroup>                             
                                    <tr>
                                        <td class="tdLabel">&nbsp;</td>
                                        <td><asp:CheckBox ID="chkIsTrusteeAddressSameWithStudent" runat="server" /> <%=GetLabel("Sama Dengan Siswa") %></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Alamat Rumah")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Desa / Kelurahan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeCounty" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kecamatan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeDistrict" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kota")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeCity" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provinsi")%></label></td>
                                        <td>
                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTrusteeProvince" ClientInstanceName="tacTrusteeProvince" MethodName="GetStandardCodeList" GetFilterExpressionFunction="onGetProvinceFilterExpression"
                                                SearchFields="StandardCodeName" TextField="StandardCodeName" ValueField="StandardCodeID" SearchText="${StandardCodeName} (<b>${cfStandardCodeID}</b>)" OrderByExpression="StandardCodeName">
                                                <ClientSideEvents ButtonSearchClick="function(){ onTacTrusteeProvinceButtonSearchClick(); }"
                                                    ValueChanged="function(){ onTacTrusteeProvinceValueChanged(); }" />
                                            </cdx:CodeXAutoCompleteTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Pos")%></label></td>
                                        <td>
                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTrusteeZipCode" ClientInstanceName="tacTrusteeZipCode" MethodName="GetZipCodesList" GetFilterExpressionFunction="onGetZipCodeFilterExpression"
                                                SearchFields="ZipCode" TextField="ZipCode" ValueField="ID" SearchText="${ZipCode}" OrderByExpression="ZipCode">
                                                <ClientSideEvents ButtonSearchClick="function(){ onTacTrusteeZipCodeButtonSearchClick(); }"
                                                    ValueChanged="function(){ onTacTrusteeZipCodeValueChanged(); }" />
                                            </cdx:CodeXAutoCompleteTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telepon Rumah")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeTelephoneNo" Width="100%" runat="server" /></td>
                                    </tr>     
                                </table>
                            </td>
                            <td>
                                <h4><%=GetLabel("Pekerjaan") %></h4>
                                <table class="tblEntryContent" style="width:100%">
                                    <colgroup>
                                        <col style="width:180px"/>
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kantor")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeJobOffice" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pekerjaan")%></label></td>
                                        <td><dxe:ASPxComboBox ID="cboTrusteeGCJob" Width="120px" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jabatan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOccupation" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pendapatan Bulanan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeSalary" CssClass="txtCurrency" Width="100px" runat="server" /></td>
                                    </tr>                       
                                    <tr>
                                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Alamat Kantor")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOfficeAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Desa / Kelurahan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOfficeCounty" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kecamatan")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOfficeDistrict" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kota")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOfficeCity" Width="100%" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Provinsi")%></label></td>
                                        <td>
                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTrusteeOfficeProvince" ClientInstanceName="tacTrusteeOfficeProvince" MethodName="GetStandardCodeList" GetFilterExpressionFunction="onGetProvinceFilterExpression"
                                                SearchFields="StandardCodeName" TextField="StandardCodeName" ValueField="StandardCodeID" SearchText="${StandardCodeName} (<b>${cfStandardCodeID}</b>)" OrderByExpression="StandardCodeName">
                                                <ClientSideEvents ButtonSearchClick="function(){ onTacTrusteeOfficeProvinceButtonSearchClick(); }"
                                                    ValueChanged="function(){ onTacTrusteeOfficeProvinceValueChanged(); }" />
                                            </cdx:CodeXAutoCompleteTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kode Pos")%></label></td>
                                        <td>
                                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTrusteeOfficeZipCode" ClientInstanceName="tacTrusteeOfficeZipCode" MethodName="GetZipCodesList" GetFilterExpressionFunction="onGetZipCodeFilterExpression"
                                                SearchFields="ZipCode" TextField="ZipCode" ValueField="ID" SearchText="${ZipCode}" OrderByExpression="ZipCode">
                                                <ClientSideEvents ButtonSearchClick="function(){ onTacTrusteeOfficeZipCodeButtonSearchClick(); }"
                                                    ValueChanged="function(){ onTacTrusteeOfficeZipCodeValueChanged(); }" />
                                            </cdx:CodeXAutoCompleteTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telepon Kantor")%></label></td>
                                        <td><asp:TextBox ID="txtTrusteeOfficeTelephoneNo" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td> 
                                <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                    EndCallback="function(s,e){ hideLoadingPanel(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                            position: relative; font-size: 0.95em;">
                            <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                <Columns>
                                    <asp:BoundField DataField="FamilyID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                    <asp:BoundField DataField="FamilyRelation" HeaderText="Relasi" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="FamilyName" HeaderText="Nama"/>
                                    <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div style='float:right;' class="divDetailDelete"></div>
                                            <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                            <input type="hidden" value="<%#Eval("FamilyID") %>" bindingfield="FamilyID" />
                                            <input type="hidden" value="<%#Eval("GCFamilyRelation") %>" bindingfield="GCFamilyRelation" />
                                            <input type="hidden" value="<%#Eval("GCTitle") %>" bindingfield="GCTitle" />
                                            <input type="hidden" value="<%#Eval("FirstName") %>" bindingfield="FirstName" />
                                            <input type="hidden" value="<%#Eval("MiddleName") %>" bindingfield="MiddleName" />
                                            <input type="hidden" value="<%#Eval("LastName") %>" bindingfield="LastName" />
                                            <input type="hidden" value="<%#Eval("GCSuffix") %>" bindingfield="GCSuffix" />
                                            <input type="hidden" value="<%#Eval("GCNationality") %>" bindingfield="GCNationality" />
                                            <input type="hidden" value="<%#Eval("GCReligion") %>" bindingfield="GCReligion" />
                                            <input type="hidden" value="<%#Eval("GCEducationLevel") %>" bindingfield="GCEducationLevel" />
                                            <input type="hidden" value="<%#Eval("CityOfBirth") %>" bindingfield="CityOfBirth" />
                                            <input type="hidden" value="<%#Eval("DateOfBirthInDatePickerFormat") %>" bindingfield="DateOfBirthInDatePickerFormat" />
                                            <input type="hidden" value="<%#Eval("GCGender") %>" bindingfield="GCGender" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <%=GetLabel("No Data To Display")%>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </dx:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </div>
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
    <br />
    <input type="button" id="btnNext" value="Next" style="float:right" />
    <input type="button" id="btnPrev" value="Prev" />
</asp:Content>