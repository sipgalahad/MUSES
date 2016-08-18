<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporMidSemesterSMP2006Rpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporMidSemesterSMP2006Rpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
         .lblHeader {font-weight:bold;}
    </style>
    <div style="text-align:center">
        <h1 style="font-weight: bold;">LAPORAN MID SEMESTER</h1>
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
                            <center>
                                <div style="border:1px solid black; padding:2px;display: inline-block; width: 85%; margin-bottom: 18px;">
                                    <table width="100%" style="border:1px solid">
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
                                </div>
                                <br />
                            </center>
                            <center>
                                <div style="border:1px solid black; padding:2px;display: inline-block">
                                    <table class="tblRapor" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td rowspan="2" align="center" class="lblHeader" style="width:20px;">No.</td>
                                            <td rowspan="2" align="center" class="lblHeader" style="width:250px;">Mata Pelajaran</td>
                                            <td rowspan="2" align="center" class="lblHeader" style="width:50px;">KKM</td>
                                            <td id="tdHeaderUlangan" runat="server" align="center" class="lblHeader">ULANGAN HARIAN</td>
                                            <td id="tdHeaderTugas" runat="server" align="center" class="lblHeader">TUGAS</td>
                                            <td rowspan="2" align="center" style="width:50px;" class="lblHeader">UTS</td>
                                            <td rowspan="2" align="center" style="width:50px;" class="lblHeader">Rata2</td>
                                        </tr>
                                        <tr>
                                            <asp:Repeater runat="server" ID="rptUlanganHeader">
                                                <ItemTemplate>
                                                    <td class="tdScore" align="center" style="width:50px;"><%#: Container.DataItem.ToString() %></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater runat="server" ID="rptTugasHeader">
                                                <ItemTemplate>
                                                    <td class="tdScore" align="center" style="width:50px;"><%#: Container.DataItem.ToString() %></td>
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
                                                    <asp:Repeater runat="server" ID="rptTugasDetail">
                                                        <ItemTemplate>
                                                            <td align="right"><%#: Container.DataItem.ToString() %></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td runat="server" id="tdDetailUTS" align="right" style="width:50px;"></td>
                                                    <td runat="server" id="tdDetailAverage" align="right" style="width:50px;"></td>
                                                </tr>
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

