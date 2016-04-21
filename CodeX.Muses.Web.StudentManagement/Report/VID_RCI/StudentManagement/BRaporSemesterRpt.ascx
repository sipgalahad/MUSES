<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporSemesterRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporSemesterRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
    <div style="text-align:center">
        <h1>LAPORAN HASIL BELAJAR SEMESTER GENAP</h1>
    </div>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:5px 3px;}   
        .tblRapor tr td { border-top: 1px solid; border-left:1px solid; }   
        .tblRapor        { border-right:1px solid; border-bottom: 1px solid;}        
        .tdScore { width:50px; }
    </style>
    <table class='tblReport' style='width:100%;margin-top: 15px' cellpadding='0' cellspacing='0'>
        <tbody class='reportBody'>
            <asp:Repeater runat="server" ID="rptStudent" OnItemDataBound="rptStudent_ItemDataBound">
                <ItemTemplate>
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
                                                <td>Nama Peserta Didik</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdStudentName" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nomor Induk</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdNIS" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolName" runat="server"></td>
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
                                                <td>Kelas / Semester</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdClass" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolPeriod" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td rowspan="3" align="center" class="lblHeader">No.</td>
                                        <td rowspan="3" align="center" class="lblHeader">Komponen</td>
                                        <td rowspan="3" align="center" style="width:50px;" class="lblHeader">KKM</td>
                                        <td colspan="5" align="center" class="lblHeader">Hasil Belajar Siswa</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" class="lblHeader">Pengetahuan</td>
                                        <td colspan="2" align="center" class="lblHeader">Praktik</td>
                                        <td rowspan="2" class="tdScore lblHeader" align="center">Sikap</td>
                                    </tr>
                                    <tr>
                                        <td class="tdScore lblHeader" align="center">Angka</td>
                                        <td class="lblHeader" align="center" style="width:150px;">Huruf</td>
                                        <td class="tdScore lblHeader" align="center">Angka</td>
                                        <td class="lblHeader" align="center" style="width:150px;">Huruf</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td align="right"><%#:Eval("PassingGrade","{0:N}") %></td>
                                                <td align="right" runat="server" id="tdTheory"></td>
                                                <td runat="server" id="tdTxtTheory"></td>
                                                <td align="right" runat="server" id="tdPractice"></td>
                                                <td runat="server" id="tdTxtPractice"></td>
                                                <td align="right" runat="server" id="tdAffective"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div style="height:400px;"></div>
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
                                                <td>Nama Peserta Didik</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdStudentName1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nomor Induk</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdNIS1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolName1" runat="server"></td>
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
                                                <td>Kelas / Semester</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdClass1" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolPeriod1" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="lblHeader" align="center">No</td>
                                        <td class="lblHeader" align="center">Komponen</td>
                                        <td class="lblHeader" align="center">Ketercapaian Kompetensi</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptSubjectKompetnsi" OnItemDataBound="rptSubjectKompetnsi_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td runat="server" id="tdKompetensi"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div style="height:400px;"></div>
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
                                                <td>Nama Peserta Didik</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdStudentName2" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nomor Induk</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdNIS2" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolName2" runat="server"></td>
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
                                                <td>Kelas / Semester</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdClass2" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td>:</td>
                                                <td class="lblHeader" id="tdSchoolPeriod2" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <h4 style="font-weight:bold;">Pengembangan diri</h4>
                                <table width="100%" cellpadding="0" cellspacing="0" class="tblRapor">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="lblHeader" align="center">No</td>
                                        <td class="lblHeader" align="center">Jenis Kegiatan</td>
                                        <td class="lblHeader" align="center">Keterangan</td>
                                    </tr>
                                    <tr>
                                        <td class="lblHeader" align="center">A</td>
                                        <td class="lblHeader">Kegiatan Ekstrakulikuler</td>
                                        <td></td>
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
                                    <tr>
                                        <td class="lblHeader" align="center">B</td>
                                        <td class="lblHeader">Kegiatan dalam Organisasi/Kegiatan Sekolah</td>
                                        <td></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptOrganization">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                <td style="padding-left:10px;"><%#:Eval("Code") %></td>
                                                <td><%#:Eval("Value") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <h4 style="font-weight:bold;">Akhlak Mulia dan Kepribadian</h4>
                                <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0">
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
                                    <asp:Repeater runat="server" ID="rptPersonality" OnItemDataBound="rptPersonality_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td runat="server" id="tdPersonalityScore" style="white-space:pre-wrap ; word-wrap:break-word;"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <h4 style="font-weight:bold;">Ketidakhadiran</h4>
                                <table class="tblRapor" width="100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="300px" />
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td style="text-align:center; font-weight:bold;">No</td>
                                        <td style="text-align:center; font-weight:bold;">Alasan Ketidakhadiran</td>
                                        <td style="text-align:center; font-weight:bold;">Keterangan</td>
                                    </tr>
                                    <tr>
                                        <td align="center">1</td>
                                        <td>Sakit</td>
                                        <td runat="server" id="tdSick" align="center"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">2</td>
                                        <td>Izin</td>
                                        <td runat="server" id="tdPermit" align="center"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">3</td>
                                        <td>Tanpa Keterangan</td>
                                        <td runat="server" id="tdAlpha" align="center"></td>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="0" cellspacing="0" style="margin-top:20px; border:1px solid;">
                                    <tr>
                                        <td class="lblHeader">Catatan Wali Kelas :</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 80px; vertical-align:top;white-space:pre-wrap ; word-wrap:break-word;" id="tdStudentRemarks" runat="server"></td>
                                    </tr>
                                </table>
                            </div>
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
    <table width="100%">
        <colgroup>
            <col width="33%" />
            <col width="33%" />
            <col />
        </colgroup>
        <tr>
            <td>&nbsp;</td>
            <td colspan="2" align="center">{City}, {Date.Now}</td>
        </tr>
        <tr>
            <td align="center">Orang Tua/Wali</td>
            <td align="center">Wali Kelas</td>
            <td align="center">Kepala Sekolah</td>
        </tr>
        <tr>
            <td align="center">Peserta Didik</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height:100px; vertical-align:bottom;">
            <td align="center">.....................................</td>
            <td align="center">{WaliKelas}</td>
            <td align="center">{Headmaster}</td>
        </tr>
    </table>
</div>

