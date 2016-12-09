<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPMain.master" AutoEventWireup="true" 
    CodeBehind="StudentBillInformation.aspx.cs" Inherits="CodeX.Muses.Web.Mobile.Program.StudentBillInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <style type="text/css">
        #tblBill tr th, #tblBill tr td          { border: 1px solid #EAEAEA; font-size: 16px; padding: 2px 5px; }
        #tblBill tr th                          { background-color: #AAA; }
    </style>
    <input type="hidden" runat="server" id="hdnStudentID" />
    <div style="padding: 5px;" runat="server" id="divBill">
        <div style="font-size:16px;">Anda Mempunyai Tagihan Sebesar : </div>
        <table cellpadding="0" cellspacing="0" id="tblBill">
            <colgroup>
                <col style="width:250px"/>
                <col style="width:180px"/>
            </colgroup>
            <tr>
                <th class="thLeft">Komponen</th>
                <th class="thRight">Tagihan</th>
            </tr>
            <tr>
                <td>Uang Pembangunan</td>
                <td align="right"><div id="divUpemb" runat="server"></div></td>
            </tr>
            <tr>
                <td>Uang Kegiatan</td>
                <td align="right"><div id="divUkeg" runat="server"></div></td>
            </tr>
            <tr>
                <td>Uang Sekolah</td>
                <td align="right"><div id="divUsek" runat="server"></div></td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px;" runat="server" id="divEmptyBill" >
        <div style="font-size:16px;">Anda Tidak Mempunyai Tagihan</div>
    </div>
</asp:Content>