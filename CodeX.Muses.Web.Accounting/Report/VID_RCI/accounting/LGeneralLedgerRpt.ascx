<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LGeneralLedgerRpt.ascx.cs" Inherits="CodeX.Muses.Web.Accounting.Report.LGeneralLedgerRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
        .page { padding: 0.2cm 0.7cm; }
        *   { font-weight: 100; }
         @media print { }
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
    <div style="text-align:center">
        <h1>LAPORAN BUKU BESAR</h1>
    </div>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblReport thead tr td { border-width:1px 0px 1px 0px; border-style:solid; }
         .tdTotal{ border-width:1px 0px 1px 0px; border-style:solid; }
    </style>
    <table class='tblReport' style='width:100%; margin-top: 15px' cellpadding='0' cellspacing='0' >
        <asp:Repeater runat="server" ID="rptPeriod" OnItemDataBound="rptPeriod_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td runat="server" id="tdKetPeriod"></td>
                </tr>
                <tr>
                    <td>
                        <table class='tblReport' style='width:100%; margin-top: 15px' cellpadding='0' cellspacing='0' >
                            <colgroup>
                                <col width="120px;"/>
                                <col />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                            </colgroup>
                            <thead>
                                <tr>
                                    <td>Kode Perkiraan</td>
                                    <td>Nama Perkiraan</td>
                                    <td align="right">Saldo Awal</td>
                                    <td align="right">Mutasi Debet</td>
                                    <td align="right">Mutasi Kredit</td>
                                    <td align="right">Saldo Akhir</td>
                                </tr>
                            </thead>
                            <tbody class='reportBody'>
                                <asp:Repeater runat="server" ID="rptGLBalance" OnItemDataBound="rptGLBalance_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="trReportBody">
                                            <td><%#:Eval("GLAccountNo") %></td>
                                            <td><%#:Eval("GLAccountName") %></td>
                                            <td runat="server" id="tdBalanceBEGIN"  align="right"></td>
                                            <td runat="server" id="tdBalanceDEBIT"  align="right"></td>
                                            <td runat="server" id="tdBalanceCREDIT"  align="right"></td>
                                            <td runat="server" id="tdBalanceEND"  align="right"></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td></td>
                                    <td style="font-weight:bold;">Total</td>
                                    <td runat="server" id="tdTotalBalanceBEGIN" align="right" class="tdTotal">{TotalBalanceBEGIN}</td>
                                    <td runat="server" id="tdTotalBalanceDEBIT" align="right" class="tdTotal">{TotalBalanceDEBIT}</td>
                                    <td runat="server" id="tdTotalBalanceCREDIT" align="right" class="tdTotal">{TotalBalanceCREDIT}</td>
                                    <td runat="server" id="tdTotalBalanceEND" align="right" class="tdTotal">{TotalBalanceEND}</td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>

