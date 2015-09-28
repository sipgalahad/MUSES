<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LPenerimaanBankMandiriRpt.ascx.cs" Inherits="CodeX.Muses.Web.Finance.Report.LPenerimaanBankMandiriRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
    </style>
    <div style="border:1px solid; float: right; height: 60px; width: 100px; vertical-align: middle; line-height: 20px; font-weight: bold; text-align: center;">
        NBS
        <div style="font-size: 20px">{ProspectiveStudentCode}</div>
    </div>
    <center style="margin-left: 100px;">
        <h1>PENERIMAAN SISWA BARU {Periode}</h1>
        <h2>Formulir Penentuan Pembayaran</h2>
    </center>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        #divReportBody table tr td { padding: 1px;}
        #divReportBody div { margin-bottom:10px; }
        .lblDataHeader { font-weight:bold; }
        .lblDataSiswa { padding-left:10px !important; }
        .tblPaymentDt tr td { border-top: 1px solid; border-left:1px solid; }
        .tblPaymentDt td { padding:3px !important; }
        .tblPaymentDt        { border-right:1px solid; border-bottom: 1px solid;}    
        
    </style>
    <div id="divDataSiswa" runat="server">
        <table cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="120px;" />
                <col width="10px;" />
                <col />
            </colgroup>
            <tr>
                <td class="lblDataSiswa">Nama Lengkap</td>
                <td align="center">:</td>
                <td>{ProspectiveStudentName}</td>
            </tr>
            <tr>
                <td class="lblDataSiswa">Alamat Lengkap</td>
                <td align="center">:</td>
                <td>{Address}</td>
            </tr>
            <tr>
                <td class="lblDataSiswa">No. Telpon / Hp</td>
                <td align="center">:</td>
                <td>{PhoneNo}</td>
            </tr>
            <tr>
                <td class="lblDataSiswa">Kelas</td>
                <td align="center">:</td>
                <td>{Class}</td>
            </tr>
        </table>
    </div> 
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="120px;" />
                <col width="3px;" />
                <col width="120px;" />
                <col />
            </colgroup>
            <tr>
                <td colspan="3" class="lblDataHeader" style="font-size: 1.1em">Biaya Sekolah</td>
            </tr>
            <tr>
                <td colspan="4" class="lblDataSiswa">
                    <table width="100%" class="tblPaymentDt" cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="300px" />
                            <col width="300px" />
                            <col width="300px" />
                            <col width="300px" />
                        </colgroup>
                        <tr>
                            <td style="font-weight:bold">Tipe Pembayaran</td>
                            <td align="right" style="font-weight:bold">Jumlah Bayar</td>
                            <td align="right" style="font-weight:bold">Diskon</td>
                            <td align="right" style="font-weight:bold">Total</td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptStudentFeeComp">
                            <ItemTemplate>
                                <tr>
                                    <td><%#:Eval("StudentFeeCompTypeName") %></td>
                                    <td align="right">Rp. <%#:Eval("TransactionAmount","{0:N}") %></td>
                                    <td align="right">Rp. <%#:Eval("TotalDiscountAmount","{0:N}") %></td>
                                    <td align="right">Rp. <%#:Eval("LineAmount","{0:N}") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td style="font-weight:bold">Jumlah</td>
                            <td></td>
                            <td></td>
                            <td id="tdTotalLineAmount" runat="server" align="right" style="font-weight:bold">Rp. {TotalLineAmount}</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" class="lblDataHeader" style="font-size: 1.1em">Pembayaran</td>
            </tr>
            <asp:Repeater runat="server" ID="rptPayment" OnItemDataBound="rptPayment_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="lblDataSiswa"><%#:Eval("StudentFeeCompTypeName") %></td>
                    </tr>
                    <tr>
                        <td class="lblDataSiswa">
                            <table width="100%" class="tblPaymentDt" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="30px;" />
                                    <col width="300px" />
                                    <col width="300px" />
                                </colgroup>
                                <tr>
                                    <td align="center" style="font-weight:bold">No.</td>
                                    <td align="center" style="font-weight:bold">Jatuh Tempo</td>
                                    <td align="right" style="font-weight:bold">Jumlah Bayar</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rptPaymentDt">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center"><%#:Eval("DisplayOrder") %></td>
                                            <td align="center"><%#:Eval("DueDateInString") %></td>
                                            <td align="right">Rp. <%#:Eval("LineAmount","{0:N}") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="3" class="lblDataHeader" style="font-size: 1.1em">Catatan: </td>
            </tr>
            <tr>
                <td colspan="3">- Uang Pembangunan sudah lunas tgl 10 Juni 2014</td>
            </tr>
            <tr>
                <td colspan="3">- Pembayaran melalui Bank Mandiri paling lambat tgl 10 setiap bulannya</td>
            </tr>
        </table>
    </div>
</div>

<div id="divPageFooter" runat="server">
    <style type="text/css">
        .pageFooter         { border-top: 0px solid; font-size: 8pt !important; margin-bottom: 50px; }
        .pageFooter *       { font-size: 8pt !important; }
        .letterFooter       { width:150px; text-align:center}
    </style>
    <table width="100%">
        <tr align="center">
            <td></td>
            <td>{City}, {DateNow}</td>
        </tr>
        <tr align="center">
            <td>Panitia</td>
            <td>Disetujui oleh,</td>
        </tr>
        <tr align="center">
            <td>Penerimaan Siswa Baru</td>
            <td>Orang Tua / Wali Siswa</td>
        </tr>
        <tr>
            <td colspan="2" style="height:50px;">&nbsp;</td>
        </tr>
        <tr align="center">
            <td>..........................</td>
            <td>..........................</td>
        </tr>
    </table>
</div>
