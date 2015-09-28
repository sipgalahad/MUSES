<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSuratPenagihanPiutangRpt.ascx.cs" Inherits="CodeX.Muses.Web.Finance.Report.BSuratPenagihanPiutangRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
    <div style="height:100px;"></div>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:5px 3px;}   
        .tblRapor tr td { border-top: 1px solid; border-left:1px solid; }   
        .tblRapor        { border-right:1px solid; border-bottom: 1px solid;}        
        .tdScore { width:50px; }
        table tr td {padding:3px;}
    </style>
    <table cellpadding="0" cellspacing="0">
        <colgroup>
            <col width="70px"/>
            <col width="3px" />
            <col width="220px"/>
        </colgroup>
        <tr>
            <td>Nomor</td>
            <td align="center">:</td>
            <td></td>
        </tr>
        <tr>
            <td>Lampiran</td>
            <td align="center">:</td>
            <td></td>
        </tr>
        <tr>
            <td>Hal</td>
            <td align="center">:</td>
            <td>Surat Klarifikasi Pembayaran</td>
        </tr>
    </table>
    <br/>
    Kepada Yth,<br/>
    Bapak / Ibu orang tua murid : <br/>
    <div runat="server" id="divStudent">
        <table cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="70px"/>
                <col width="3px" />
                <col width="220px"/>
            </colgroup>
            <tr>
                <td>Nama</td>
                <td align="center">:</td>
                <td>{StudentName}</td>
            </tr>
            <tr>
                <td>Unit</td>
                <td align="center">:</td>
                <td>{Grade}</td>
            </tr>
            <tr>
                <td>Kelas / NBS </td>
                <td align="center">:</td>
                <td>{Class}</td>
            </tr>
        </table>
    </div>
    Di Tempat<br/>
    <br/>
    Dengan hormat,<br/>
    Melalui surat ini kami mengharapkan bantuan dari Bapak/Ibu. Memperhatikan dan berdasarkan data pembayaran keuangan dari sistem UBP<br/>
    (Unified Bill Payment) yang ada di Bank Mandiri, per tanggal surat ini kami belum mendapatkan informasi pembayaran uang sbb :
    <table cellpadding="0" cellspacing="0" border="1" width="95%">
        <colgroup>
            <col width="150px" />
            <col width="120px" />
            <col />
        </colgroup>    
        <tr>
            <td align="center">Pembayaran</td>
            <td align="center">Jumlah Rp.</td>
            <td align="center">Keterangan</td>
        </tr>
        <asp:Repeater runat="server" ID="rptPiutang">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("GroupName") %></td>
                    <td align="right"><%#Eval("TotalAmount","{0:N}") %></td>
                    <td><%#Eval("Remarks") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    Maka bagi siswa yang belum membayar, kami harap untuk segera menyelesaikan pembayaran tersebut di atas ke Cabang Bank Mandiri terdekat,<br/>
    dan bagi siswa yang ternyata sudah membayar kami mohon maaf dan mohon bantuannya untuk segera menginformasikan ke bagian administrasi<br/>
    keuangan untuk memperbaiki catatan administrasi kami.<br/>
    <br/>
    Demikian surat ini kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terima kasih.<br/>
</div>

<div id="divPageFooter" runat="server">
<style type="text/css">
    .pageFooter         { border-top: 0px solid; font-size: 8pt !important; margin-bottom: 50px; }
    .pageFooter *       { font-size: 8pt !important; }
    .letterFooter       { width:150px; text-align:center}
</style>
    <table width="100%" cellpadding="0" cellspacing="0">    
        <colgroup>
            <col width="70%" />
            <col width="30%" />
        </colgroup>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr><td>Mengetahui,</td></tr>
                    <tr style="height:70px;"><td></td></tr>
                    <tr><td>{HeadMaster}</td></tr>
                    <tr><td>Kepala {SiteName}</td></tr>
                </table>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr><td>Hormat kami,</td></tr>
                    <tr style="height:70px;"><td></td></tr>
                    <tr><td>{FinanceManager}</td></tr>
                    <tr><td>Adm. Keu. {SiteName}</td></tr>
                </table>
            </td>
        </tr>
        
    </table>
    <table>
        <colgroup>
            <col width="20px" />
            <col />
        </colgroup>
        <tr>
            <td colspan="2">Catatan :</td>
        </tr>
        <tr>
            <td colspan="2">Untuk informasi pembayaran ke :</td>
        </tr>
        <tr>
            <td align="right" valign="top">-</td>
            <td>Ibu Agnes atau Bapak Agung<br/> di sekertariat {SiteName} Telp. {PhoneNo}.</td>
        </tr>
    </table>
        
</div>