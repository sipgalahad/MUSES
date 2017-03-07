<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolClassBannerDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.MasterPage.SchoolClassBannerDtCtl" %>

<script type="text/javascript" id="dxss_patientbannerdtctl">
    function setStudentImage() {
        setTimeout(function () {
            var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
            var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");

            $('.imgStudentImage').each(function () {
                $divStudentImage = $(this).parent().find('.divStudentImage');
                $divStudentImage.attr('style', "background-image:url('" + this.src + "')");
                $(this).error(function () {
                    var gender = $(this).parent().find('.hdnStudentGender').val();
                    if (gender == '0003^F')
                        $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlF + "')");
                    else
                        $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlM + "')");
                }).attr('src', this.src);
            });
        }, 0);
    }
</script>

<input type="hidden" id="hdnTitleText" runat="server" />
<h4><%=GetLabel("Data Siswa")%></h4>
<img src="" id="imgPatientImage" style="display:none" runat="server" />
<ul class="ulHeaderBannerDetailInfo">
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divStudentCode" runat="server"></div></div>
            <h6 style="background-color: #E1B700;"><%=GetLabel("Kode")%></h6>
        </center>
    </li>
    <li style="width: 190px">
        <center>
            <div class="containerTile"><div id="divDateOfBirth" runat="server"></div></div>
            <h6 style="background-color: #00A3A3"><%=GetLabel("Tanggal Lahir")%></h6>
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
<br />