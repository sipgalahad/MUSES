<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RProjectBannerDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.MasterPage.RProjectBannerDtCtl" %>

<script type="text/javascript" id="dxss_patientbannerdtctl">
    $(function () {
        $('#divImageHeaderBanner').hide();
    });
</script>

<input type="hidden" id="hdnTitleText" runat="server" />
<h4><%=GetLabel("Detil Project")%></h4>
<img src="" id="imgPatientImage" style="display:none" runat="server" />
<ul class="ulHeaderBannerDetailInfo">
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divCode" runat="server"></div></div>
            <h6 style="background-color: #E1B700;"><%=GetLabel("Kode")%></h6>
        </center>
    </li>
    <li style="width: 250px">
        <center>
            <div class="containerTile" style="font-size:16px;"><div id="divDate" runat="server"></div></div>
            <h6 style="background-color: #FF2E12"><%=GetLabel("Tanggal")%></h6>
        </center>
    </li>
</ul>
<br />