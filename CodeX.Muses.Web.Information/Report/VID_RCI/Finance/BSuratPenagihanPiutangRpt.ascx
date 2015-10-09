<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSuratPenagihanPiutangRpt.ascx.cs" Inherits="CodeX.Muses.Web.Information.Report.BSuratPenagihanPiutangRpt" %>

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
    <%--405--%>
    <div style="height:15px;"></div>
    <div id="divPiutang" runat="server">
        <table cellpadding="0" cellspacing="0" style="margin-left:130px;">
            <colgroup>
                <col width="67px"/>
                <col />
            </colgroup>
            <tr style="height:142px;">
                <td></td>
                <td valign="top" style="padding-top:20px;">{No}</td>
            </tr>
            <tr style="height:25px;">
                <td></td>
                <td>{StudentName}</td>
            </tr>
            <tr style="height:25px;">
                <td></td>
                <td>{Grade}</td>
            </tr>
            <tr style="height:24px;">
                <td></td>
                <td>{Class}</td>
            </tr>
        </table>
        <div style="height:155px;"></div>
        <table cellpadding="0" cellspacing="0" style="margin-left:130px;">
            <colgroup>
                <col width="155px"/>
                <col width="120px" />
                <col width="" />
            </colgroup>
            <tr style="height:37px;">
                <td></td>
                <td align="right">{Usek}</td>
                <td></td>
            </tr>
            <tr style="height:37px">
                <td></td>
                <td align="right">{Kegiatan}</td>
                <td></td>
            </tr>
            <tr style="height:37px">
                <td></td>
                <td align="right">{Pembangunan}</td>
                <td></td>
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
</div>