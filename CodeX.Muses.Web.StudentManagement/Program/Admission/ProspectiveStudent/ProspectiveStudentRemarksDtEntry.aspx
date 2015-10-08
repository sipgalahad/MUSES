<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentRemarksDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentRemarksDtEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPFrame" runat="server">
    <script type="text/javascript">
        $(function () {
            registerCollapseExpandHandler();

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
    </script>
    <input type="hidden" id="hdnID" runat="server" />
    <div style="height: 410px; overflow-y:auto">
        <fieldset id="fsMPEntry">            
            <table class="tblContentArea" >
                <colgroup>
                    <col style="width:50%"/>
                </colgroup>
                <tr>
                    <td style="padding:5px;vertical-align:top" rowspan="2">
                        <h4 class="h4expanded"><%=GetLabel("Informasi Pribadi")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:220px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Bakat / Minat") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtTalentOrInterest" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsFatherless" Text="Yatim" />
                                        <asp:CheckBox runat="server" ID="chkIsMotherless" Text="Piatu" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Bahasa Sehari-hari")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboLanguage" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" valign="top" style="padding-top:4px">
                                        <label class="lblNormal"><%=GetLabel("Selera Makan pada Waktu")%></label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col width="320px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <colgroup>
                                                            <col width="75px" />
                                                            <col />
                                                        </colgroup>
                                                        <tr>
                                                            <td><label class="lblNormal"><%=GetLabel("Pagi")%></label></td>
                                                            <td><dxe:ASPxComboBox ID="cboAppetiteAtBreakfast" Width="120px" runat="server" /></td>    
                                                        </tr>
                                                        <tr>
                                                            <td><label class="lblNormal"><%=GetLabel("Siang")%></label></td>
                                                            <td><dxe:ASPxComboBox ID="cboAppetiteAtLunch" Width="120px" runat="server" /></td>    
                                                        </tr>
                                                        <tr>
                                                            <td><label class="lblNormal"><%=GetLabel("Malam")%></label></td>
                                                            <td><dxe:ASPxComboBox ID="cboAppetiteAtDinner" Width="120px" runat="server" /></td>    
                                                        </tr>
                                                        <tr>
                                                            <td><label class="lblNormal"><%=GetLabel("yang Lain")%></label></td>
                                                            <td><dxe:ASPxComboBox ID="cboAppetiteAtOtherTime" Width="120px" runat="server" /></td>    
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" valign="top" style="padding-top:2px">
                                        <label class="lblNormal"><%=GetLabel("Kebiasaan Tidur Pada Umumnya")%></label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col width="220px" />
                                                <col width="220px" />
                                            </colgroup>
                                            <tr>
                                                <td><label><%=GetLabel("Tidur Malam")%></label> <asp:TextBox ID="txtSleepingAtNight" Width="80px" runat="server" /></td>
                                                <td><label><%=GetLabel("Bangun Pagi")%></label> <asp:TextBox ID="txtWakeUp" Width="80px" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:CheckBox runat="server" ID="chkSleepingAtRandomTime" Text="Tidur dan Bangun pada Waktu yang Tidak Menentu" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Disusui Oleh Ibunya/wanita lain selama")%></label></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtBreastfedDuration" Width="80px" Text="0" CssClass="number" runat="server" /> Bulan</td>
                                                <td><asp:CheckBox runat="server" ID="chkIsBreastfed" Text="Disusui" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Makanan Tambahan yang Diberikan Setelah Umur 3 bulan")%></label></td>
                                    <td><asp:TextBox ID="txtAdditionalFood" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Alat Transportasi")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboTransportToSchool" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsFailInSchool" Text="Pernah Tidak Naik Kelas" />
                                        <asp:TextBox ID="txtGradeFail" Width="80px" CssClass="number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="tdLabel"><label class="lblNormal"><%=GetLabel("Alasan Mendaftar")%></label></td>
                                    <td><asp:TextBox ID="txtReasonRegister" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                                </tr>
                            </table>
                        </div>

                        <h4 class="h4expanded"><%=GetLabel("Informasi Medis")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:220px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Waktu Buang Air pada Umumnya")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboUrinateStatus" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Golongan Darah")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboBloodType" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keadaan dalam Kandungan")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboStateInWomb" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keadaan pada Waktu Lahir")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboStateAtBirth" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keadaan pada saat Masuk Taman Kanak-kanak") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtStateEnterKinderGarten" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:CheckBox runat="server" ID="chkIsDisablity" Text="Cacat" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Riwayat Penyakit") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtMedicalHistory" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="padding:5px;vertical-align:top" rowspan="2">
                        <h4 class="h4expanded"><%=GetLabel("Informasi Lingkungan")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col width="50%"/>
                                    <col />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tempat Tinggal")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboHouseStatus" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Penghuni Dewasa")%></label></td>
                                    <td><asp:TextBox ID="txtHouseHolderAdult" Width="80px" CssClass="number" Text="0" runat="server" /> Orang</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Penghuni Anak-anak")%></label></td>
                                    <td><asp:TextBox ID="txtHouseHolderChild" Width="80px" CssClass="number" Text="0" runat="server" /> Orang</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hubungan dengan Ayah")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboRealtionshipWithFather" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hubungan dengan Ibu")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboRealtionshipWithMother" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Hubungan dengan Saudara")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboRealtionshipWithBrother" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:CheckBox runat="server" ID="chkIsPlaygroundInHouse" Text="Terdapat Halaman Tempat Bermain" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kesempatan Bergaul")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboChanceToHangout" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jarak Rumah Ke Sekolah")%></label></td>
                                    <td><asp:TextBox ID="txtHomeDistance" Width="80px" CssClass="number" runat="server" /> [km]</td>
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