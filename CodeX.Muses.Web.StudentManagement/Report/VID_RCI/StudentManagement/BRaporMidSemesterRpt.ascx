<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BRaporMidSemesterRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BRaporMidSemesterRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
    </style>
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
    <table>
        <asp:Repeater runat="server" ID="rptSubject">
            <HeaderTemplate>
                <tr>
                    <td>Komponen</td>
                    <td>Ulangan Harian</td>
                    <td>Tugas</td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
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

