<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierBannerDtCtl.ascx.cs" 
    Inherits="CodeX.Web.Finance.MasterPage.SupplierBannerDtCtl" %>

<script type="text/javascript" id="dxss_patientbannerdtctl">
    $(function () {
        $('#divImageHeaderBanner').hide();
    });
</script>

<input type="hidden" id="hdnTitleText" runat="server" />
<h4><%=GetLabel("Data Supplier")%></h4>
<img src="" id="imgPatientImage" style="display:none" runat="server" />
<ul class="ulHeaderBannerDetailInfo">
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divBusinessPartnerCode" runat="server"></div></div>
            <h6 style="background-color: #E1B700;"><%=GetLabel("Kode")%></h6>
        </center>
    </li>
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divContactPerson" runat="server"></div></div>
            <h6 style="background-color: #FF2E12"><%=GetLabel("Contact Person")%></h6>
        </center>
    </li>
    <li style="width: 190px">
        <center>
            <div class="containerTile" style="font-size: 24px;"><div id="divPhoneNo" runat="server"></div></div>
            <h6 style="background-color: #77B900"><%=GetLabel("No Telp")%></h6>
        </center>
    </li>
    <li style="width: 210px">
        <center>
            <div class="containerTile" style="font-size: 13px;"><div id="divAddress" runat="server"></div></div>
            <h6 style="background-color: #AA40FF"><%=GetLabel("Alamat")%></h6>
        </center>
    </li>
</ul>
<h4><%=GetLabel("Informasi Kunjungan")%></h4>
<ul class="ulHeaderBannerDetailInfo">
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divBank" runat="server"></div></div>
            <h6 style="background-color: #77B900"><%=GetLabel("Bank")%></h6>
        </center>
    </li>
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divBankReferenceNo" runat="server"></div></div>
            <h6 style="background-color: #AA40FF"><%=GetLabel("No Rekening")%></h6>
        </center>
    </li>
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divBankAccountHolder" runat="server"></div></div>
            <h6 style="background-color: #77B900"><%=GetLabel("Nama Pemegang")%></h6>
        </center>
    </li>
<br />