<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporSemesterGanjilSMP2006Rpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporSemesterGanjilSMP2006Rpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
    <div style="text-align:center">
        <h1>LAPORAN HASIL BELAJAR PESERTA DIDIK</h1>
    </div>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:2px 3px;}   
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
                                                <td>Nama</td>
                                                <td>:</td>
                                                <td id="tdStudentName" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>NIS / NISN</td>
                                                <td>:</td>
                                                <td id="tdNIS" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Nama Sekolah</td>
                                                <td>:</td>
                                                <td id="tdSchoolName" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Alamat</td>
                                                <td>:</td>
                                                <td id="tdSchoolAddress" runat="server"></td>
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
                                                <td>:</td>
                                                <td id="tdClass" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Semester</td>
                                                <td>:</td>
                                                <td id="tdSemester" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td>Tahun Pelajaran</td>
                                                <td>:</td>
                                                <td id="tdSchoolPeriod" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <div style="border:1px solid black; padding:2px;display: inline-block">
                                    <table class="tblRapor" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td rowspan="2" align="center" class="lblHeader" style="width:50px;">No.</td>
                                            <td rowspan="2" align="center" class="lblHeader" style="width:220px;">Mata Pelajaran</td>
                                            <td rowspan="2" align="center" style="width:50px;" class="lblHeader">KKM</td>
                                            <td colspan="2" align="center" class="lblHeader">Nilai</td>
                                            <td rowspan="2" align="center" style="width:150px;" class="lblHeader">Deskripsi<br />Kemajuan<br />Belajar</td>
                                        </tr>
                                        <tr>
                                            <td class="tdScore lblHeader" align="center" style="width:50px;">Angka</td>
                                            <td class="lblHeader" align="center" style="width:150px;">Huruf</td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center" id="tdItemIndex" runat="server"><%# Container.ItemIndex + 1 %></td>
                                                    <td>
                                                        <%#:Eval("CurriculumReportDtName") %>
                                                        <div id="divSubjectDt" runat="server"></div>
                                                    </td>
                                                    <td align="center"><b style="font-weight: bold;"><%#:Eval("PassingGrade","{0:N0}") %></b></td>
                                                    <td align="center" runat="server" id="tdTheory"></td>
                                                    <td align="center" runat="server" id="tdTxtTheory"></td>
                                                    <td align="center" runat="server" id="tdTxtDescription"></td>
                                                </tr>
                                                
                                                <asp:Repeater runat="server" ID="rptSubject2" OnItemDataBound="rptSubject_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#:Eval("CurriculumReportDtName") %>
                                                                <div id="divSubjectDt" runat="server"></div>
                                                            </td>
                                                            <td align="center"><b style="font-weight: bold;"><%#:Eval("PassingGrade","{0:N0}") %></b></td>
                                                            <td align="center" runat="server" id="tdTheory"></td>
                                                            <td align="center" runat="server" id="tdTxtTheory"></td>
                                                            <td align="center" runat="server" id="tdTxtDescription"></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <br />
                                <br />
                                <table cellpadding="0" style="width:100%">
                                    <td valign="top" align="center" style="width:50%">
                                        <div style="border:1px solid black; padding:2px;display: inline-block">
                                            <table class="tblRapor" cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col width="250px;" />
                                                    <col width="10px;" />
                                                    <col width="50px;" />
                                                </colgroup>
                                                <tr>
                                                    <td align="center" colspan="3">Akhlak dan Kepribadian</td>
                                                </tr>
                                                <asp:Repeater runat="server" ID="rptPersonality" OnItemDataBound="rptPersonality_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#:Eval("SubjectName") %></td>
                                                            <td align="center">:</td>
                                                            <td align="center" runat="server" id="tdPersonalityScore" style="white-space:pre-wrap ; word-wrap:break-word;"></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                    <td valign="top" align="center">  
                                        <div style="border:1px solid black; padding:2px;display: inline-block">                                      
                                            <table class="tblRapor" cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col width="120px;" />
                                                    <col width="10px;" />
                                                    <col width="50px;" />
                                                    <col width="80px;" />
                                                </colgroup>
                                                <tr>
                                                    <td style="text-align:center;" colspan="4">Ketidakhadiran</td>
                                                </tr>
                                                <tr>
                                                    <td>1. Sakit</td>
                                                    <td align="center">:</td>
                                                    <td runat="server" id="tdSick" align="center"></td>
                                                    <td align="center">Hari</td>
                                                </tr>
                                                <tr>
                                                    <td>2. Izin</td>
                                                    <td align="center">:</td>
                                                    <td runat="server" id="tdPermit" align="center"></td>
                                                    <td align="center">Hari</td>
                                                </tr>
                                                <tr>
                                                    <td>3. Tanpa Keterangan</td>
                                                    <td align="center">:</td>
                                                    <td runat="server" id="tdAlpha" align="center"></td>
                                                    <td align="center">Hari</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr class="trReportBody" style="display:none">
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
                    <tr class="trReportBody" style="display:none">
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
            <td align="center">Mengetahui</td>
            <td align="center">{City}, {Date.Now}</td>
        </tr>
        <tr>
            <td align="center">Orang Tua/Wali</td>
            <td align="center">Wali Kelas</td>
        </tr>
        <tr style="height:80px; vertical-align:bottom;">
            <td align="center">.....................................</td>
            <td align="center" style="font-weight: bold">{WaliKelas}</td>
        </tr>
    </table>
</div>

