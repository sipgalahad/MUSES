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
        .tblProfilGuruItem tr td {padding:3px;}
    </style>
    <table class='tblReport' style='width:100%; margin-top: 15px;' cellpadding='0' cellspacing='0'>
        <tbody class="reportBody">
            <asp:Repeater runat="server" ID="rptMainBody" OnItemDataBound="rptMainBody_ItemDataBound">
                <ItemTemplate>
                    <tr class="trReportBody">
                        <td align="center"><h1>PROFIL <%#Eval("TeacherName") %></h1></td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <div style="padding-left:10px;" id="divEmploymentStatus" runat="server">
                                <table cellpadding='0' cellspacing='0' width="100%" class="tblProfilGuruItem">
                                    <colgroup>
                                        <col width="25%" />
                                        <col width="25%" />
                                        <col width="25%" />
                                        <col width="25%" />
                                    </colgroup>
                                    <tr>
                                        <td>Mulai Dinas : {HiredDate}</td>
                                        <td>Pensiun : {TerminatedDate}</td>
                                        <td>Status Kerja :</td>
                                        <td>Fungsi :</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <h3 style="font-weight:bold;">A. KEPRIBADIAN</h3>
                            <h3 style="font-weight:bold;" id="divPersonalityType" runat="server">Tipe Kepribadian : {PersonalityType}</h3>
                            <div style="padding-left:10px;" id="divPersonal" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" class="tblProfilGuruItem">
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
                                        <td align="center">{IQInPercentage}</td>
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
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <div style="padding-left:10px;" id="divPersonalDesc" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" width="100%" style="white-space: normal; text-align: justify;" class="tblProfilGuruItem">
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
                            <br />
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptReportBody" OnItemDataBound="rptReportBody_ItemDataBound">
                        <ItemTemplate>
                            <tr class="trReportBody">
                                <td><h3 style="font-weight:bold; display:none;" id="divHeader" runat="server" >B. Kompetensi Pedagogik & Profesional</h3></td>
                            </tr>
                            <tr class="trReportBody">
                                <td valign="top">
                                    <div style="padding-left:10px;" id="div1" runat="server">
                                       <table  cellpadding='0' cellspacing='0' border="1" width="100%" class="tblProfilGuruItem">
                                            <colgroup>
                                                <col width="3px;"/>
                                                <col />
                                                <col width="50px"/>
                                                <col width="100px"/>
                                            </colgroup>
                                            <tr class="trReportBody">
                                                <td valign="top">No.</td>
                                                <td style="font-weight:bold;"><%#Eval("Value")%></td>
                                                <td align="center" id="tdHeaderPercentage" runat="server">%</td>
                                                <td align="center" id="tdHeaderMutu" runat="server">Mutu</td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptGroupItem">
                                                <ItemTemplate>
                                                    <tr class="trReportBody">
                                                        <td valign="top" align="right" class="number"><%# Container.ItemIndex + 1 %></td>
                                                        <td valign="top" style="white-space:normal;"><%#Eval("TeacherProfileItemName") %></td>
                                                        <td valign="top" align="center" class="number" id="tdPercentage" runat="server"></td>
                                                        <td valign="top" id="tdMutu" runat="server"></td>
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
                                    <br />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="trReportBody">
                        <td><h3 style="font-weight:bold;">D. PRESENSI : </h3></td>
                    </tr>
                    <tr class="trReportBody">
                        <td valign="top">
                            <div style="padding-left:10px;" id="div2" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" class="tblProfilGuruItem">
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
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>%</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td><h3 style="font-weight:bold;">E. CATATAN KEPALA SEKOLAH/PIMPINAN LANGSUNG TERKAIT SIKAP/PERILAKU/KINERJA YBS</h3></td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div style="padding-left:10px;" id="div3" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" width="100%" class="tblProfilGuruItem">
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
                            <br />
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td><h3 style="font-weight:bold;">F. PROGRAM PENGEMBANGAN DIRI YANG AKAN SAYA LAKUKAN</h3></td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div style="padding-left:10px; max-height: 170px;" id="div4" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" width="100%" class="tblProfilGuruItem">
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
                            <br />
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td><h3 style="font-weight:bold;">G. PROGRAM PENGEMBANGAN DIRI YANG PERLU DILAKUKAN GURU YBS MENURUT KEPALA SEKOLAH/PIMPINAN LANGSUNG</h3></td>
                    </tr>
                    <tr class="trReportBody" id="trTest">
                        <td>
                            <div style="padding-left:10px;" id="div5" runat="server">
                                <table cellpadding='0' cellspacing='0' border="1" width="100%" class="tblProfilGuruItem">
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
                            <table cellpadding='0' cellspacing='0' width="100%" class="tblProfilGuruItem">
                                <colgroup>
                                    <col width="50%" />
                                    <col width="50%" />
                                </colgroup>
                                <tr>
                                    <td align="center"></td>
                                    <td align="center">..................... .....-.....-.....</td>
                                </tr>
                                <tr>
                                    <td align="center">Kepala Sekolah</td>
                                    <td align="center">Guru Yang Bersangkutan</td>
                                </tr>
                                <tr>
                                    <td style="height:70px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="center">(________________________________)</td>
                                    <td align="center">(________________________________)</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="trReportBody">
                        <td>
                            <div style="height:200px; background-color:White;"></div>
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

