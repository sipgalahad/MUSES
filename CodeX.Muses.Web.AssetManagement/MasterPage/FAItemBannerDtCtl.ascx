<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAItemBannerDtCtl.ascx.cs" 
    Inherits="CodeX.Web.AssetManagement.MasterPage.FAItemBannerDtCtl" %>

<script type="text/javascript" id="dxss_patientbannerdtctl">
    $(function () {
        $('#divImageHeaderBanner').hide();
    });
</script>


<input type="hidden" id="hdnTitleText" runat="server" />
<h4><%=GetLabel("Data Asset")%></h4>
<img src="" id="imgPatientImage" style="display:none" runat="server" />
<ul class="ulHeaderBannerDetailInfo">
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divCode" runat="server"></div></div>
            <h6 style="background-color: #E1B700;"><%=GetLabel("Kode")%></h6>
        </center>
    </li>
</ul>
<br />