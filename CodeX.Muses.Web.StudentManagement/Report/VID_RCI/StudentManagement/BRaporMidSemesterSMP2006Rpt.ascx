<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporMidSemesterSMP2006Rpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporMidSemesterSMP2006Rpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
         .lblHeader {font-weight:bold;}
    </style>
    <div style="text-align:center">
        <h1>LAPORAN HASIL BELAJAR TENGAH SEMESTER GENAP</h1>
    </div>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:3px;}
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
                                        <td rowspan="4" align="center" class="lblHeader">No.</td>
                                        <td rowspan="4" align="center" class="lblHeader">Komponen</td>
                                        <td rowspan="4" align="center" style="width:50px;" class="lblHeader">KKM</td>
                                        <td id="tdHeaderHasil" runat="server" align="center" class="lblHeader">Hasil Belajar Siswa</td>
                                    </tr>
                                    <tr>
                                        <td id="tdHeaderNilai" runat="server" align="center" class="lblHeader">Nilai</td>
                                        <td rowspan="3" class="tdScore lblHeader" align="center">Sikap</td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="tdUlangan" align="center" class="lblHeader">Kognitif</td>
                                        <td rowspan="2" class="tdScore lblHeader" align="center">UTS</td>
                                        <td runat="server" id="tdTugas" align="center" class="lblHeader">Tugas</td>
                                        <td align="center" id="tdPsikomotorik" runat="server" class="lblHeader">Psikomotorik</td>
                                    </tr>
                                    <tr>
                                        <asp:Repeater runat="server" ID="rptUlanganHeader">
                                            <ItemTemplate>
                                                <td class="tdScore" align="center"><%#: Container.DataItem.ToString() %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="rptTugasHeader">
                                            <ItemTemplate>
                                                <td class="tdScore" align="center"><%#: Container.DataItem.ToString() %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="rptPsikomotorikHeader">
                                            <ItemTemplate>
                                                <td class="tdScore" align="center"><%#: Container.DataItem.ToString() %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td align="right"><%#:Eval("PassingGrade","{0:N}") %></td>
                                                <asp:Repeater runat="server" ID="rptUlanganDetail">
                                                    <ItemTemplate>
                                                        <td align="right"><%#: Container.DataItem.ToString() %></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td runat="server" id="tdDetailUTS" align="right" style="width:50px;"></td>
                                                <asp:Repeater runat="server" ID="rptTugasDetail">
                                                    <ItemTemplate>
                                                        <td align="right"><%#: Container.DataItem.ToString() %></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="rptPsikomotorikDetail">
                                                    <ItemTemplate>
                                                        <td align="right"><%#: Container.DataItem.ToString() %></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td runat="server" id="tdDetailSikap" align="right" style="width:50px;"></td>
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
                                <h4 style=" font-weight:bold;">Keterlambatan</h4>
                                <table class="tblRapor" width="100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="50px;" />
                                    </colgroup>
                                    <tr>
                                        <td></td>
                                        <td>0 Kali</td>
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
            <col width="50%" />
            <col />
        </colgroup>
        <tr>
            <td>
                <table width="50%">
                    <tr><td align="center">Orang Tua/Wali</td></tr>
                    <tr><td align="center">Peserta Didik</td></tr>
                    <tr style="height:100px; vertical-align:bottom;"><td align="center">.....................................</td></tr>
                </table>
            </td>
            <td align="right">
                <table width="50%">
                    <tr><td align="center">{City}, {Date.Now}</td></tr>
                    <tr><td align="center">Wali Kelas</td></tr>
                    <tr style="height:100px; vertical-align:bottom;"><td align="center">{WaliKelas}</td></tr>
                </table>
            </td>
        </tr>
    </table>
</div>

