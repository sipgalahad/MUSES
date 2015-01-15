<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassSubjectBannerDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.MasterPage.ClassSubjectBannerDtCtl" %>

<script type="text/javascript" id="dxss_patientbannerdtctl">
    $(function () {
        $('#divImageHeaderBanner').hide();
    });

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

<style type="text/css">
    .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
</style>

<input type="hidden" id="hdnTitleText" runat="server" />
<h4><%=GetLabel("Data Pasien")%></h4>
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
<br />