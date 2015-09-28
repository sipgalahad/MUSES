<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSuratPenagihanPiutangRpt.ascx.cs" Inherits="CodeX.Muses.Web.Finance.Report.BSuratPenagihanPiutangRpt" %>

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
    <table cellpadding="0" cellspacing="0">
        <colgroup>
            <col width="120px"/>
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

    Kepada Yth,<br/>
    Bapak / Ibu orang tua murid : <br/>
    Nama :<br/>
    Unit :<br/>
    Kelas / NBS :<br/>
    Di Tempat<br/>
    <br/>
    Dengan hormat,<br/>
    Melalui surat ini kami mengharapkan bantuan dari Bapak/Ibu. Memperhatikan dan<br/>
    berdasarkan data pembayaran keuangan dari sistem UBP (Unified Bill Payment) yang ada <br/>
    di Bank Mandiri, per tanggal surat ini kami belum mendapatkan informasi pembayaran uang sbb :<br/>
    <table cellpadding="0" cellspacing="0" border="1">
        <colgroup>
            <col width="300px" />
            <col width="100px" />
            <col width="150px" />
        </colgroup>    
        <tr>
            <td>Piutang</td>
            <td align="right">Jumlah</td>
            <td>Keterangan</td>
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
    Maka bagi siswa yang belum membayar, kami harap untuk segera menyelesaikan<br/>
    pembayaran tersebut di atas ke Cabang Bank Mandiri terdekat, dan bagi siswa<br/>
    yang ternyata sudah membayar kami mohon maaf dan mohon bantuannya untuk segera<br/>
    menginformasikan ke bagian administrasi keuangan untuk memperbaiki catatan<br/>
    administrasi kami.<br/>
    <br/>
    Demikian surat ini kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan<br/>
    terima kasih.<br/>
</div>

<div id="divPageFooter" runat="server">
<style type="text/css">
    .pageFooter         { border-top: 0px solid; font-size: 8pt !important; margin-bottom: 50px; }
    .pageFooter *       { font-size: 8pt !important; }
    .letterFooter       { width:150px; text-align:center}
</style>
    <table width="100%" cellpadding="0" cellspacing="0">    
        <colgroup>
            <col width="50%" />
            <col width="50%" />
        </colgroup>
        <tr>
            <td align="center">Mengetahui,</td>
            <td align="center">Hormat kami,</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Agustinus Awal S.Pd</td>
            <td>Agnes Kus Handayani</td>
        </tr>
        <tr>
            <td>Kepala SMP Ricci II</td>
            <td>Adm. Keu. Ricci II</td>
        </tr>
    </table>
    Catatan :
    Untuk informasi pembayaran ke :<br/>
        - Ibu Agnes atau Bapak Agung<br/>
          di sekertariat Ricci II Telp. 7361674, 7355891.
</div>