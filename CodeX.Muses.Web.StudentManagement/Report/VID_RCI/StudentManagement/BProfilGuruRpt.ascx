<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BProfilGuruRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BProfilGuruRpt" %>

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
        .tblRapor        { border-right:1px solid; border-bottom: 1px solid;}        
        .tdScore { width:50px; }
        table tr td {padding:3px;}
    </style>
    <div style="text-align:center" id="divRBHeader" runat="server">
        <h1>PROFIL {TeacherName}</h1>
    </div>
    <table class='tblReport' style='width:100%;margin-top: 15px' cellpadding='0' cellspacing='0'>
        <tbody class="reportBody">
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">A. KEPRIBADIAN</h3>
                    <h3 style="font-weight:bold;" id="divPersonalityType" runat="server">Tipe Kepribadian : {PersonalityType}</h3>
                    <div style="padding-left:10px;" id="divPersonal" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1">
                            <colgroup>
                                <col width="20px;" />
                                <col />
                                <col width="50px" />
                                <col width="80px" />
                                <col width="50px" />
                                <col width="50px" />
                                <col width="50px" />
                                <col width="50px" />
                                <col width="50px" />
                            </colgroup>
                            <tr>
                                <td align="center">No</td>
                                <td align="center">Nama</td>
                                <td colspan="2" align="center" class="number">IQ</td>
                                <td align="center">D</td>
                                <td align="center">K</td>
                                <td align="center">L</td>
                                <td align="center">T</td>
                                <td align="center">Kons</td>
                            </tr>
                            <tr>
                                <td>1.</td>
                                <td>{TeacherName}</td>
                                <td align="right">{IQ}</td>
                                <td align="right">{IQInPercentage}</td>
                                <td align="right">{Drive}</td>
                                <td align="right">{Komunikasi}</td>
                                <td align="right">{Loyalitas}</td>
                                <td align="right">{Ketelitian}</td>
                                <td align="right">{Konsistensi}</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td valign="top">
                    <div style="padding-left:10px;" id="divPersonalDesc" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1" width="100%" style="white-space: normal; text-align: justify;">
                            <colgroup>
                                <col width="50%" />
                                <col />
                            </colgroup>
                            <tr>
                                <td>KEKUATAN</td>
                                <td>KELEMAHAN</td>
                            </tr>
                            <tr>
                                <td valign="top">{Adventages}</td>
                                <td valign="top">{Weakness}</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptReportBody" OnItemDataBound="rptReportBody_ItemDataBound">
                <ItemTemplate>
                    <tr class="trReportBody">
                        <td valign="top">
                            <h3 style="font-weight:bold; display:none;" id="divHeader" runat="server" >B. Kompetensi Pedagogik & Profesional</h3>
                            <div style="padding-left:10px;" id="div1" runat="server">
                               <table  cellpadding='0' cellspacing='0' border="1" width="100%" style="margin-bottom:20px;" >
                                    <colgroup>
                                        <col width="3px;"/>
                                        <col />
                                        <col width="50px"/>
                                        <col width="100px"/>
                                    </colgroup>
                                    <tr class="trReportBody">
                                        <td valign="top">No.</td>
                                        <td style="font-weight:bold;"><%#Eval("Value")%></td>
                                        <td align="center">%</td>
                                        <td align="center">Mutu</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptGroupItem">
                                        <ItemTemplate>
                                            <tr class="trReportBody">
                                                <td valign="top" align="right" class="number"><%# Container.ItemIndex + 1 %></td>
                                                <td valign="top" style="white-space:normal;"><%#Eval("TeacherProfileItemName") %></td>
                                                <td valign="top" align="center" class="number"><%# Eval("QualityPercentage").ToString() == "0" ? (Convert.ToDecimal(Eval("Score")) / Convert.ToDecimal(Eval("DynamicQualityPercentage")) * 100).ToString("N") : (Convert.ToDecimal(Eval("Score")) / Convert.ToDecimal(Eval("QualityPercentage")) * 100).ToString("N") %></td>
                                                <td valign="top" ><%#GetMutu(Eval("QualityPercentage").ToString() == "0" ? (Convert.ToDecimal(Eval("Score")) / Convert.ToDecimal(Eval("DynamicQualityPercentage")) * 100) : (Convert.ToDecimal(Eval("Score")) / Convert.ToDecimal(Eval("QualityPercentage")) * 100)) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="trReportBody" runat="server" id="trMutu">
                                        <td></td>
                                        <td>Mutu Pencapaian</td>
                                        <td runat="server" id="tdFinalScore" align="center"></td>
                                        <td runat="server" id="tdQualityScore"></td>
                                    </tr>
                                </table> 
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">D. PRESENSI : {FromDate}-{ToDate}</h3>
                    <div style="padding-left:10px;" id="div2" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1">
                            <colgroup>
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                            </colgroup>
                            <tr>
                                <td>He = .....</td>
                                <td align="center">Kehadiran</td>
                                <td align="center">Sakit</td>
                                <td align="center">Izin</td>
                                <td align="center">Alpha</td>
                            </tr>
                            <tr>
                                <td>JML HARI</td>
                                <td>{hrKehadiran}</td>
                                <td>{hrSakit}</td>
                                <td>{hrIzin}</td>
                                <td>{hrAlpha}</td>
                            </tr>
                            <tr>
                                <td>%</td>
                                <td>{prsnKehadiran}</td>
                                <td>{prsnhrSakit}</td>
                                <td>{prsnhrIzin}</td>
                                <td>{prsnhrAlpha}</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td>
                    <h3 style="font-weight:bold;">E. CATATAN KEPALA SEKOLAH/PIMPINAN LANGSUNG TERKAIT SIKAP/PERILAKU/KINERJA YBS</h3>
                    <div style="padding-left:10px;" id="div3" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1" width="100%">
                            <colgroup>
                                <col width="3px" />
                                <col width="120px" />
                                <col />
                            </colgroup>
                            <tr>
                                <td align="center">No.</td>
                                <td align="center">Tgl Kejadian</td>
                                <td align="center">SIKAP/PERILAKU/KINERJA</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td>
                    <h3 style="font-weight:bold;">F. PROGRAM PENGEMBANGAN DIRI YANG AKAN SAYA LAKUKAN</h3>
                    <div style="padding-left:10px;" id="div4" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1" width="100%">
                            <colgroup>
                                <col width="3px" />
                                <col />
                                <col width="60px"/>
                                <col width="60px"/>
                            </colgroup>
                            <tr>
                                <td align="center" rowspan="2">No.</td>
                                <td align="center" rowspan="2">PROGRAM</td>
                                <td align="center" colspan="2">JADWAL</td>
                            </tr>
                            <tr>
                                <td align="center">Mulai</td>
                                <td align="center">Selesai</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td>
                    <h3 style="font-weight:bold;">G. PROGRAM PENGEMBANGAN DIRI YANG PERLU DILAKUKAN GURU YBS MENURUT KEPALA SEKOLAH/PIMPINAN LANGSUNG</h3>
                    <div style="padding-left:10px;" id="div5" runat="server">
                        <table cellpadding='0' cellspacing='0' border="1" width="100%">
                            <colgroup>
                                <col width="3px" />
                                <col />
                                <col width="60px"/>
                                <col width="60px"/>
                            </colgroup>
                            <tr>
                                <td align="center" rowspan="2">No.</td>
                                <td align="center" rowspan="2">PROGRAM</td>
                                <td align="center" colspan="2">JADWAL</td>
                            </tr>
                            <tr>
                                <td align="center">Mulai</td>
                                <td align="center">Selesai</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
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

