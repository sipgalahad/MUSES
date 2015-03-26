<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporMidSemesterRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporMidSemesterRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
    </style>
    <table width="50%">
        <tr>
            <td valign="top">
                <table width="100%">
                    <colgroup>
                        <col width="120px" />
                        <col width="3px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td>Nama Peserta Didik</td>
                        <td>:</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Nomor Induk</td>
                        <td>:</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Nama Sekolah</td>
                        <td>:</td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="100%">
                    <colgroup>
                        <col width="120px" />
                        <col width="3px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td>Kelas / Semester</td>
                        <td>:</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Tahun Pelajaran</td>
                        <td>:</td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        #tblRapor tr td { border:1px solid; padding:3px;}
    </style>
    <table width="100%" style="border:1px solid;" id="tblRapor" cellpadding="0" cellspacing="0">
        <tr>
            <td rowspan="2" align="center">No.</td>
            <td rowspan="2" align="center">Komponen</td>
            <td rowspan="2" align="center" style="width:50px;">KKM</td>
            <td runat="server" id="tdUlangan" align="center">Ulangan Harian</td>
            <td runat="server" id="tdTugas" align="center">Tugas</td>
            <td rowspan="2" align="center">UTS</td>
            <td rowspan="2" align="center">Nilai Akhir</td>
        </tr>
        <tr>
            <asp:Repeater runat="server" ID="rptUlanganHeader">
                <ItemTemplate>
                    <td style="width:50px;" align="center"><%#: Container.DataItem.ToString() %></td>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ID="rptTugasHeader">
                <ItemTemplate>
                    <td style="width:50px;" align="center"><%#: Container.DataItem.ToString() %></td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="center" style="width:50px;"><%# Container.ItemIndex + 1 %></td>
                    <td><%#:Eval("SubjectName") %></td>
                    <td align="right"><%#:Eval("PassingGrade") %></td>
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
                    <td runat="server" id="tdFinalScore" align="right" style="width:50px;"></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>

<div id="divPageFooter" runat="server">
    <style type="text/css">
        .pageFooter         { border-top: 0px solid; font-size: 8pt !important; }
        .pageFooter *       { font-size: 8pt !important; }
        .letterFooter       { width:150px; text-align:center}
    </style>
</div>

