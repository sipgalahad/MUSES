<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporSemester2013Rpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporSemester2013Rpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:5px 3px;}   
        .tblRapor tr td { border-top: 1px solid; border-left:1px solid; }   
        .tblRapor       { border-right:1px solid; border-bottom: 1px solid; margin-bottom:10px;}        
        
        
        .tblRapor1       { border-right:1px solid; border-bottom: 1px solid; margin-bottom:10px;}
        .tblRapor1 tr td { border-top: 1px solid; border-left:1px solid; padding:2px 0px 0px 5px; }
        
        .tblSchoolData {}
        .tblSchoolData tr td { padding:5px}
        
        .tdScore { width:50px; }
        .divReportBody { height:887px; overflow-y:auto; }
    </style>
    <table class='tblReport' style='width:100%;margin-top: 15px' cellpadding='0' cellspacing='0'>
        <tbody class='reportBody'>
            <asp:Repeater runat="server" ID="rptStudent" OnItemDataBound="rptStudent_ItemDataBound">
                <ItemTemplate>
                    <tr class="trReportBody">
                        <td>
                            <div class="divReportBody" style="text-align:center;" id="divRapor" runat="server">
                                <h1>LAPORAN<br/>CAPAIAN KOMPETENSI PESERTA DIDIK<br/>SEKOLAH MENENGAH ATAS<br/>(SMA)</h1>
                                <div style="height:200px;"></div>
                                <h2>Nama Peserta Didik</h2>
                                <div style="border:1px solid; width:500px; font-size:large; font-weight:bold; margin-left:120px; margin-bottom:7px;">{StudentName}</div>
                                <div style="border:1px solid; width:500px; font-size:small; margin-left:120px; margin-bottom:200px;">{StudentNIS}</div>
                                <div style="font-size:medium; font-weight:bold;">
                                    KEMENTERIAN PENDIDIKAN DAN KEBUDAYAAN<br/>REPUBLIK INDONESIA
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div id="divSchool" runat="server" class="divReportBody">
                                <div style="text-align:center; font-size:small; font-weight:bold; margin-bottom:50px;">
                                    LAPORAN<br/>CAPAIAN PESERTA DIDIK<br/>SEKOLAH MENENGAH ATAS<br/>(SMA)
                                </div>
                                <table cellpadding="0" cellspacing="0" class="tblSchoolData">
                                    <tr>
                                        <td>Nama Sekolah</td>
                                        <td align="center">:</td>
                                        <td>{SchoolName}</td>
                                    </tr>
                                    <tr>
                                        <td>NPSN / NSS</td>
                                        <td align="center">:</td>
                                        <td>{SchoolNPSN}</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Alamat Sekolah</td>
                                        <td valign="top" align="center">:</td>
                                        <td>{SchoolAddress}</td>
                                    </tr>
                                    <tr>
                                        <td>Kelurahan</td>
                                        <td align="center">:</td>
                                        <td>{SchoolKelurahan}</td>
                                    </tr>
                                    <tr>
                                        <td>Kecamatan</td>
                                        <td align="center">:</td>
                                        <td>{SchoolKecamatan}</td>
                                    </tr>
                                    <tr>
                                        <td>Kabupaten/Kota</td>
                                        <td align="center">:</td>
                                        <td>{SchoolCity}</td>
                                    </tr>
                                    <tr>
                                        <td>Provinsi</td>
                                        <td align="center">:</td>
                                        <td>{SchoolProvince}</td>
                                    </tr>
                                    <tr>
                                        <td>Website Sekolah</td>
                                        <td align="center">:</td>
                                        <td>{SchoolWebsite}</td>
                                    </tr>
                                    <tr>
                                        <td>Email Sekolah</td>
                                        <td align="center">:</td>
                                        <td>{SchoolEmail}</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div style="text-align:center">
                                <h3 style="font-weight:bold;">KETERANGAN TENTANG DIRI PESERTA DIDIK</h3>
                            </div>
                            <div id="divPersonal" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="20px" />
                                        <col width="120px" />
                                        <col width="10px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td>1.</td>
                                        <td>Nama Peserta Didik (Lengkap)</td>
                                        <td align="center">:</td>
                                        <td>{StudentName}</td>
                                    </tr>
                                    <tr>
                                        <td>2.</td>
                                        <td>NIS / NISN</td>
                                        <td align="center">:</td>
                                        <td>{NIS}</td>
                                    </tr>
                                    <tr>
                                        <td>3.</td>
                                        <td>Tempat Tanggal Lahir</td>
                                        <td align="center">:</td>
                                        <td>{DOB}</td>
                                    </tr>
                                    <tr>
                                        <td>4.</td>
                                        <td>Jenis Kelamin</td>
                                        <td align="center">:</td>
                                        <td>{Gender}</td>
                                    </tr>
                                    <tr>
                                        <td>5.</td>
                                        <td>Agama</td>
                                        <td align="center">:</td>
                                        <td>{Religion}</td>
                                    </tr>
                                    <tr>
                                        <td>6.</td>
                                        <td>Status dalam Keluarga</td>
                                        <td align="center">:</td>
                                        <td>{Status}</td>
                                    </tr>
                                    <tr>
                                        <td>7.</td>
                                        <td>Anak ke</td>
                                        <td align="center">:</td>
                                        <td>{1 dari - Saudara}</td>
                                    </tr>
                                    <tr>
                                        <td>8.</td>
                                        <td>Alamat Peserta didik</td>
                                        <td align="center">:</td>
                                        <td>{Address}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td align="center">:</td>
                                        <td>{City}</td>
                                    </tr>
                                    <tr>
                                        <td>9.</td>
                                        <td>Nomor Telepon Rumah</td>
                                        <td align="center">:</td>
                                        <td>{PhoneNo}</td>
                                    </tr>
                                    <tr>
                                        <td>10.</td>
                                        <td>Sekolah Asal</td>
                                        <td align="center">:</td>
                                        <td>{PastSchool}</td>
                                    </tr>
                                    <tr>
                                        <td>11.</td>
                                        <td>Diterima di sekolah ini</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Di kelas</td>
                                        <td align="center">:</td>
                                        <td>{Grade}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Pada tanggal</td>
                                        <td align="center">:</td>
                                        <td>{AcceptedDate}</td>
                                    </tr>
                                    <tr>
                                        <td>12.</td>
                                        <td>Nama Orang tua</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a. Ayah</td>
                                        <td align="center">:</td>
                                        <td>{FatherName}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b. Ibu</td>
                                        <td align="center">:</td>
                                        <td>{MotherName}</td>
                                    </tr>
                                    <tr>
                                        <td>13.</td>
                                        <td>Alamat Orang Tua</td>
                                        <td align="center">:</td>
                                        <td>{ParentAddress}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td align="center">:</td>
                                        <td>{ParentCity}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Nomor Telepon Rumah</td>
                                        <td align="center">:</td>
                                        <td>{ParentPhoneNo}</td>
                                    </tr>
                                    <tr>
                                        <td>14.</td>
                                        <td>Pekerjaan Orang Tua</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a. Ayah</td>
                                        <td align="center">:</td>
                                        <td>{FatherJob}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b. Ibu</td>
                                        <td align="center">:</td>
                                        <td>{MotherJob}</td>
                                    </tr>
                                    <tr>
                                        <td>15.</td>
                                        <td>Nama Wali Peserta Didik</td>
                                        <td align="center">:</td>
                                        <td>{WaliName}</td>
                                    </tr>
                                    <tr>
                                        <td>16.</td>
                                        <td>Alamat Wali Peserta Didik</td>
                                        <td align="center">:</td>
                                        <td>{WaliAddress}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td align="center">:</td>
                                        <td>{WaliCity}</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>Nomor Telepon Rumah</td>
                                        <td align="center">:</td>
                                        <td>{WaliPhoneNo}</td>
                                    </tr>
                                    <tr>
                                        <td>17.</td>
                                        <td>Pekerjaan Wali Peserta Didik</td>
                                        <td align="center">:</td>
                                        <td>{WaliJob}</td>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="0" cellspacing="0" style="margin-top:10px;">
                                    <colgroup>
                                        <col width="50%" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td valign="top" align="center">
                                            <div style="width:90px; height:120px; border:1px solid;">
                                            
                                            </div>
                                        </td>
                                        <td valign="top" align="center">
                                            <table>
                                                <tr><td>{FooterDate.Now}</td></tr>
                                                <tr><td>Kepala Sekolah,</td></tr>
                                                <tr><td style="height:50px;"></td></tr>
                                                <tr><td>{Headmaster}</td></tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td style="height:151.5px" valign="top">
                            <table width="100%">
                                <colgroup>
                                    <col width="50%" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td valign="top" align="left">
                                        <table width="100%">
                                            <colgroup>
                                                <col width="120px" />
                                                <col width="3px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolName" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Alamat Sekolah</td>
                                                <td valign="top" align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolAddress" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdStudentName" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nomor Induk / NISN</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdNIS" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <table >
                                            <colgroup>
                                                <col width="120px" />
                                                <col width="3px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td>Kelas</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdClass" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Semester</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdPeriodSection" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolPeriod" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div class="divReportBody">
                                <h3 style="font-weight:bold;">CAPAIAN KOMPETENSI</h3>
                                <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td rowspan="2" align="center" class="lblHeader">No.</td>
                                        <td rowspan="2" align="center" class="lblHeader">Mata Pelajaran</td>
                                        <td colspan="2" align="center" class="lblHeader">Pengetahuan</td>
                                        <td colspan="2" align="center" class="lblHeader">Keterampilan</td>
                                        <td colspan="2" class="tdScore lblHeader" align="center">Sikap Spiritual dan Sosial</td>
                                    </tr>
                                    <tr>
                                        <td class="tdScore lblHeader" align="center">Angka</td>
                                        <td class="lblHeader" align="center">Predikat</td>
                                        <td class="tdScore lblHeader" align="center">Angka</td>
                                        <td class="lblHeader" align="center">Predikat</td>
                                        <td class="tdScore lblHeader" align="center">Dalam Mapel</td>
                                        <td class="lblHeader" align="center">Antar Mapel</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptCurriculumSubjectGroupName" OnItemDataBound="rptCurriculumSubjectGroupName_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td <%# Container.ItemIndex == 0 ? "colspan='8'":"colspan='7'" %> style="font-weight:bold;"><%#:Eval("CurriculumSubjectGroupName") %></td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                        <td runat="server" id="tdTxtSubjectName"></td>
                                                        <td align="right" runat="server" id="tdTheory"></td>
                                                        <td runat="server" id="tdTxtTheory"></td>
                                                        <td align="right" runat="server" id="tdPractice"></td>
                                                        <td runat='server' id="tdTxtPractice"></td>
                                                        <td align="right" runat="server" id="tdAffective"></td>
                                                        <td runat="server" id="tdAttitude" ></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <div class="divReportBody">
                                <table width="100%" cellpadding="0" cellspacing="0" class="tblRapor">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="lblHeader" align="center">No</td>
                                        <td class="lblHeader" align="center">Ekstrakurikuler</td>
                                        <td class="lblHeader" align="center">Keikutsertaan dalam Kegiatan</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptEskul" OnItemDataBound="rptEskul_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                <td style="padding-left:10px;"><%#:Eval("SubjectName") %></td>
                                                <td runat="server" id="tdEskul"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr style="display:none;">
                                        <td class="lblHeader" align="center">B</td>
                                        <td class="lblHeader">Kegiatan dalam Organisasi/Kegiatan Sekolah</td>
                                        <td></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptOrganization">
                                        <ItemTemplate>
                                            <tr style="display:none;">
                                                <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                <td style="padding-left:10px;"><%#:Eval("Code") %></td>
                                                <td><%#:Eval("Value") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0" style="display:none;">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="lblHeader" align="center">No</td>
                                        <td class="lblHeader" align="center">Aspek yang dinilai</td>
                                        <td class="lblHeader" align="center">Keterangan</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptPersonality">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td runat="server" id="tdPersonalityScore"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="100px;" />
                                        <col width="20px" />
                                        <col width="100px;" />
                                    </colgroup>
                                    <tr>
                                        <td style="text-align:center; font-weight:bold; border:1px solid;" colspan="3">Ketidakhadiran</td>
                                    </tr>
                                    <tr>
                                        <td style="border-left:1px solid; padding-left:5px;">Sakit</td>
                                        <td align="center">:</td>
                                        <td runat="server" id="tdSick" align="center" style="border-right:1px solid;"></td>
                                    </tr>
                                    <tr>
                                        <td style="border-left:1px solid; padding-left:5px;">Izin</td>
                                        <td align="center">:</td>
                                        <td runat="server" id="tdPermit" align="center" style="border-right:1px solid;"></td>
                                    </tr>
                                    <tr>
                                        <td style="border-left:1px solid; border-bottom:1px solid; padding-left:5px;">Tanpa Keterangan</td>
                                        <td align="center" style="border-bottom:1px solid;">:</td>
                                        <td style="border-right:1px solid; border-bottom:1px solid;" runat="server" id="tdAlpha" align="center"></td>
                                    </tr>
                                </table>
                                <div style="margin-top:600px;">
                                    <table width="100%">
                                        <colgroup>
                                            <col width="33%" />
                                            <col width="33%" />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td>Mengetahui</td>
                                            <td colspan="2" id="tdFooterDateNow" runat="server">{City}, {Date.Now}</td>
                                        </tr>
                                        <tr>
                                            <td>Orang Tua/Wali,</td>
                                            <td>Wali Kelas</td>
                                        </tr>
                                        <tr style="height:100px; vertical-align:bottom;">
                                            <td id="tdFooterStudentParent" runat="server">{StudentParent}</td>
                                            <td id="tdFooterWali" runat="server">{WaliKelas}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <table width="100%">
                                <colgroup>
                                    <col width="50%" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td valign="top" align="left">
                                        <table width="100%">
                                            <colgroup>
                                                <col width="120px" />
                                                <col width="3px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolName1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Alamat Sekolah</td>
                                                <td valign="top" align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolAddress1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdStudentName1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nomor Induk / NISN</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdNIS1" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <colgroup>
                                                <col width="120px" />
                                                <col width="3px" />
                                                <col />
                                            </colgroup>
                                            <tr>
                                                <td>Kelas</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdClass1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Semester</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdPeriodSection1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td align="center">:</td>
                                                <td class="lblHeader" id="tdSchoolPeriod1" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div class="divReportBody">
                                <h3 style="font-weight:bold;">DESKRIPSI KOMPETENSI</h3>
                                <table width="100%" cellpadding="0" cellspacing="0" class="tblRapor1">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="lblHeader" align="center">Mata Pelajaran</td>
                                        <td class="lblHeader" align="center">Kompetensi</td>
                                        <td class="lblHeader" align="center">Catatan</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptCurriculumSubjectGroupName1" OnItemDataBound="rptCurriculumSubjectGroupName1_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="font-weight:bold;" colspan="3"><%#:Eval("CurriculumSubjectGroupName") %></td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptSubjectKompetensi" OnItemDataBound="rptSubjectKompetensi_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td rowspan="3" valign="top"><%#:Eval("SubjectName") %></td>
                                                        <td>Pengetahuan</td>
                                                        <td runat="server" id="tdTeoriKompetensi"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Keterampilan</td>
                                                        <td runat="server" id="tdPraktikKompetensi"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sikap Spiritual dan Sosial</td>
                                                        <td runat="server" id="tdSikapKompetensi"></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div>
                                    <table width="100%">
                                        <colgroup>
                                            <col width="33%" />
                                            <col width="33%" />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td>Mengetahui</td>
                                            <td id="tdFooterDateNow1" runat="server" colspan="2">{City}, {Date.Now}</td>
                                        </tr>
                                        <tr>
                                            <td>Orang Tua/Wali,</td>
                                            <td>Wali Kelas</td>
                                        </tr>
                                        <tr style="height:100px; vertical-align:bottom;">
                                            <td id="tdFooterStudentParent1" runat="server">{StudentParent}</td>
                                            <td id="tdFooterWali1" runat="server">{WaliKelas}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div style="text-align:center">
                                <h3 style="font-weight:bold;">CATATAN PRESTASI YANG PERNAH DICAPAI</h3>
                            </div>
                            <table>
                                <tr>
                                    <td>Nama Peserta Didik</td>
                                    <td>:</td>
                                    <td runat="server" id="tdAchStudentName"></td>
                                </tr>
                                <tr>
                                    <td>Nama Sekolah</td>
                                    <td>:</td>
                                    <td runat="server" id="tdAchSchoolName"></td>
                                </tr>
                                <tr>
                                    <td>Nomor Induk / NISN</td>
                                    <td>:</td>
                                    <td runat="server" id="tdAchNIS"></td>
                                </tr>
                            </table>
                            <table class="tblRapor" cellpadding="0" cellspacing="0" border="1">
                                <colgroup>
                                    <col width="20px"/>
                                    <col width="300px"/>
                                </colgroup>
                                <tr>
                                    <td>No</td>
                                    <td>Prestasi Yang Pernah Dicapai</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rptAchievement">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%#Eval("AchievementName") %></td>
                                        </tr>    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>

<div id="divPageFooter" runat="server">
    <style type="text/css">
        .pageFooter         { border-top: 0px solid; font-size: 8pt !important; margin-bottom: 50px; }
        .pageFooter *       { font-size: 8pt !important; }
        .letterFooter       { width:150px; text-align:center}
    </style>
</div>

