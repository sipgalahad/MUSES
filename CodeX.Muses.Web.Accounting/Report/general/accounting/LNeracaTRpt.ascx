<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LNeracaTRpt.ascx.cs" Inherits="CodeX.Muses.Web.Accounting.Report.LNeracaTRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        h1 { font-weight: bold !important; font-size: 12pt; margin-bottom: 0.5cm }
        h2 { font-weight: bold !important; font-size: 10pt; margin-bottom: 0.5cm; margin-top: -0.5cm; }
        th { font-weight: bold !important; }
        .trGrandTotal *  { font-weight: bold !important; }
        .trGrandTotal td  { border-top: 1px solid; border-bottom: 1px solid }
    </style>
    <center>
        <h1>Laporan Neraca (bentuk T)</h1>
        <h2 id="subHeaderText" runat="server"></h2>
    </center>
</div>

<div id="divReportBody" runat="server">
    <table class="tblReport" cellpadding="0" cellspacing="0" style="width: 100%">
        <thead>
            <tr class="tblHeader">
                <th style="width: 12%; text-align: left">Kode Perkiraan</th>
                <th style="width: 30%; text-align: left">Nama Perkiraan</th>
                <th style="width: 8%; text-align: right; padding-right: 20px; border-right: 1px solid black;">Jumlah</th>
                <th style="width: 12%; text-align: left">Kode Perkiraan</th>
                <th style="width: 30%; text-align: left">Nama Perkiraan</th>
                <th style="width: 8%; text-align: right; padding-right: 20px;">Jumlah</th>
            </tr>
        </thead>
        <tbody class="reportBody">
            <asp:Repeater ID="rptView" runat="server">
                <ItemTemplate>
                    <tr class="trReportBody <%#Eval("AdditionalClassName")%>">
                        <td><div style="margin-left:<%#Eval("AktivaAccountLevel")%>0px;"><%#Eval("AktivaGLAccountNo")%></div></td>
                        <td><div style="margin-left:<%#Eval("AktivaAccountLevel")%>0px;"><%#Eval("AktivaGLAccountName")%></div></td>
                        <td style="border-right: 1px solid black; padding-right: 20px;" align="right"><%#Eval("AktivaBalanceEND", "{0:N}")%></td>
                        <td><div style="margin-left:<%#Eval("PasivaAccountLevel")%>0px;"><%#Eval("PasivaGLAccountNo")%></div></td>
                        <td><div style="margin-left:<%#Eval("PasivaAccountLevel")%>0px;"><%#Eval("PasivaGLAccountName")%></div></td>
                        <td align="right" style="padding-right: 20px;"><%#Eval("PasivaBalanceEND", "{0:N}")%></td>
                    </tr>                
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>
