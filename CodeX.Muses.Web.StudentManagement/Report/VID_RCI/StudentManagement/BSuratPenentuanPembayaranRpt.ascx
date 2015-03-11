<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSuratPenentuanPembayaranRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BResepRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
    </style>
    <center>
        <div style="text-decoration: underline; margin-top:10px; font-weight:bold">PENERIMAAN SISWA BARU {Periode}</div>
        <div style="margin-top:10px; font-weight:bold">Formulir Penentuan Pembayaran</div>
    </center>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        #divReportBody table tr td { padding: 1px;}
        #divReportBody div { margin-bottom:10px; }
        .lblDataHeader { font-weight:bold; }
        .lblDataSiswa { padding-left:10px !important; }
        .PaymentDt tr td { border:1px solid; }
        .PaymentDt td { padding:3px !important; }
    </style>
    <div id="divDataSiswa" runat="server">
        <table cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="120px;" />
                <col width="3px;" />
                <col />
            </colgroup>
            <tr>
                <td colspan="3" class="lblDataHeader">Data Siswa Baru</td>
            </tr>
            <tr>
                <td class="lblDataSiswa">No. Bank Siswa</td>
                <td align="center">:</td>
                <td>{ProspectiveStudentCode}</td>
            </tr>
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
                <td>:</td>
                <td>{Class}</td>
            </tr>
        </table>
    </div> 
    <div>
        <table cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="120px;" />
                <col width="3px;" />
                <col width="120px;" />
            </colgroup>
            <tr>
                <td colspan="3" class="lblDataHeader">Biaya Sekolah</td>
            </tr>
            <asp:Repeater runat="server" ID="rptStudentFeeComp">
                <ItemTemplate>
                    <tr>
                        <td class="lblDataSiswa"><%#:Eval("StudentFeeCompTypeName") %></td>
                        <td align="center">:</td>
                        <td align="right">Rp. <%#:Eval("TotalAmount","{0:N}") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td class="lblDataSiswa">Jumlah</td>
                <td align="center">:</td>
                <td id="tdTotalLineAmount" runat="server" align="right" style="border-top:1px solid;">Rp. {TotalLineAmount}</td>
            </tr>
        </table>
    </div>
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" class="lblDataHeader">Pembayaran</td>
            </tr>
            <asp:Repeater runat="server" ID="rptPayment" OnItemDataBound="rptPayment_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="lblDataSiswa"><%#:Eval("StudentFeeCompTypeName") %></td>
                    </tr>
                    <tr>
                        <td class="lblDataSiswa">
                            <table width="100%" class="PaymentDt" style="border:1px solid;" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="30px;" />
                                    <col width="120px" />
                                    <col width="120px" />
                                    <col width="120px" />
                                    <col width="120px" />
                                </colgroup>
                                <tr>
                                    <td>No.</td>
                                    <td align="center" style="font-weight:bold">Jatuh Tempo</td>
                                    <td align="right" style="font-weight:bold">Jumlah Bayar</td>
                                    <td align="right" style="font-weight:bold">Diskon</td>
                                    <td align="right" style="font-weight:bold">Total</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rptPaymentDt">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#:Eval("DisplayOrder") %></td>
                                            <td align="center"><%#:Eval("PaymentDateInString") %></td>
                                            <td align="right">Rp. <%#:Eval("TotalPaymentAmount","{0:N}") %></td>
                                            <td align="right">Rp. <%#:Eval("TotalDiscountAmount","{0:N}") %></td>
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
        </table>
    </div>
</div>

<div id="divPageFooter" runat="server">
    <style type="text/css">
        .pageFooter         { border-top: 0px solid; font-size: 8pt !important; }
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
