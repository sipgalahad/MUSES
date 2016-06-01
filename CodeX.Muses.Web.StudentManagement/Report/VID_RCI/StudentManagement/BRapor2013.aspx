<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BRapor2013.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.VID_RCI.StudentManagement.BRapor2013" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align:center;">
            <h1 style="font-size: 16pt; font-weight: bold; line-height: 25px;">RAPOR<br/>SEKOLAH MENENGAH ATAS<br/>(SMA)</h1>
            <div>img src=~/Libs/Images/Client/logo_ricci.png posX=248 posY=600 </div>
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            <table>
                <tr style="height:15px; line-height: 10px;">
                    <td>
                        <h2 style="font-size: 16pt; line-height: 20px;">Nama Peserta Didik</h2>            
                    </td>
                </tr>
            </table>
            
            <table>
                <tr>
                    <td></td>
                    <td colspan="8">
                        <table border="1" style="width: 80%">
                            <tr style="height:15px; line-height: 15px;">
                                <td><div style="font-size: 16pt;font-weight:bold;" id="divStudentName" runat="server"></div></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            
            <table>
                <tr style="height:15px; line-height: 10px;">
                    <td>
                        <h2 style="font-size: 16pt; line-height: 20px;">NIS / NISN</h2>            
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td></td>
                    <td colspan="8">
                        <table border="1" style="width: 80%">
                            <tr style="height:15px; line-height: 15px;">
                                <td><div style="font-size: 16pt;font-weight:bold;" id="divStudentNIS" runat="server"></div></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>
            </table>
            
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            <div style="font-weight:bold; font-size: 16pt; line-height: 25px;">
                KEMENTERIAN PENDIDIKAN DAN KEBUDAYAAN<br/>REPUBLIK INDONESIA
            </div>
        </div>

        <div>\p</div>

        <div style="text-align:center;">
            <h1 style="font-size: 16pt; font-weight: bold; line-height: 25px;">RAPOR<br/>SEKOLAH MENENGAH ATAS<br/>(SMA)</h1>
        </div>
        <br /><br />
        <table class="tblSchoolData" style="font-size:10pt;">
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Nama Sekolah</td>
                <td align="center">:</td>
                <td id="tdSchoolName" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">NPSN / NSS</td>
                <td align="center">:</td>
                <td id="tdSchoolNPSN" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3" valign="top">Alamat Sekolah</td>
                <td valign="top" align="center">:</td>
                <td id="tdSchoolAddress" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Kelurahan</td>
                <td align="center">:</td>
                <td id="tdSchoolKelurahan" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Kecamatan</td>
                <td align="center">:</td>
                <td id="tdSchoolKecamatan" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Kabupaten/Kota</td>
                <td align="center">:</td>
                <td id="tdSchoolCity" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Provinsi</td>
                <td align="center">:</td>
                <td id="tdSchoolProvince" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Website Sekolah</td>
                <td align="center">:</td>
                <td id="tdSchoolWebsite" runat="server" colspan="10"></td>
            </tr>
            <tr style="height:20px; line-height:20px;">
                <td colspan="3">Email Sekolah</td>
                <td align="center">:</td>
                <td id="tdSchoolEmail" runat="server" colspan="10"></td>
            </tr>
        </table>

        <div>\p</div>

        <div style="text-align:center;">
            <h1 style="font-size: 16pt; font-weight: bold; line-height: 25px;">IDENTITAS PESERTA DIDIK</h1>
        </div>

        <div>\p</div>

        <table style="font-size:9pt">
            <tr style="height:6px; line-height:6px;">
                <td colspan="5">Nama Sekolah</td><td>:</td><td colspan="8" id="tdHeaderSchoolName1" runat="server"></td><td></td>
                <td colspan="5">Kelas</td><td>:</td><td colspan="8" id="tdHeaderSchoolClassName1" runat="server"></td>
            </tr>
            <tr style="height:6px; line-height:6px;">
                <td colspan="5">Alamat Sekolah</td><td>:</td><td colspan="8" id="tdHeaderSchoolAddress1" runat="server"></td><td></td>
                <td colspan="5">Semester</td><td>:</td><td colspan="8" id="td" runat="server"></td>
            </tr>
            <tr style="height:6px; line-height:6px;">
                <td colspan="5"></td><td></td><td colspan="8" id="tdHeaderSchoolAddressLine21" runat="server"></td><td></td>
                <td colspan="5">Tahun Pelajaran</td><td>:</td><td colspan="8" id="tdHeaderSchoolPeriod1" runat="server"></td>
            </tr>
            <tr style="height:6px; line-height:6px;">
                <td colspan="5">Nama</td><td>:</td><td colspan="8" id="tdHeaderStudentName1" runat="server"></td><td></td>
                <td colspan="5"></td><td></td><td colspan="8"></td>
            </tr>
            <tr style="height:6px; line-height:6px;">
                <td colspan="5">NIS / NISN</td><td>:</td><td colspan="8" id="tdHeaderStudentCode1" runat="server"></td><td></td>
                <td colspan="5"></td><td></td><td colspan="8"></td>
            </tr>
        </table>
        <br />
        <div style="text-align:center;">
            <h1 style="font-size: 9pt; font-weight: bold; line-height: 25px;">CAPAIAN HASIL BELAJAR</h1>
        </div>
    </div>
    </form>
</body>
</html>
