<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ChangeStudentPhotoEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ChangeStudentPhotoEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview.ClientID %>').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $(function () {
            $("#imgInp").change(function () {
                readURL(this);
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                var bgImg = new Image;
                bgImg.src = $('#<%=imgPreview.ClientID %>').attr('src');
                bgImg.onload = function () {
                    var canvas = document.createElement('canvas');
                    canvas.setAttribute('width', $('#<%=imgPreview.ClientID %>').width());
                    canvas.setAttribute('height', $('#<%=imgPreview.ClientID %>').height());
                    var ctx = canvas.getContext("2d");

                    ctx.drawImage(bgImg, 0, 0, $('#<%=imgPreview.ClientID %>').width(), $('#<%=imgPreview.ClientID %>').height());

                    var image = canvas.toDataURL("image/png");
                    image = image.replace('data:image/png;base64,', '');
                    image = image.replace('data:image/jpeg;base64,', '');
                    image = image.replace('data:image/gif;base64,', '');
                    $('#<%=hdnImageData.ClientID %>').val(image);
                    onCustomButtonClick('save');
                }

            });

            var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
            var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");
            $('#<%=imgPreview.ClientID %>').each(function () {
                $(this).error(function () {
                    var gender = $('#<%=hdnGender.ClientID %>').val();
                    if (gender == '0003^F')
                        $('#<%=imgPreview.ClientID %>').attr('src', imgUrlF);
                    else
                        $('#<%=imgPreview.ClientID %>').attr('src', imgUrlM);
                }).attr('src', this.src);
            });
        });
    </script>  
    <input type="hidden" id="hdnStudentCode" value="" runat="server" />
    <input type="hidden" id="hdnImageData" value="" runat="server" />
    <input type="hidden" id="hdnGender" value="" runat="server" />
    <input type='file' id="imgInp" /> <br />
    <div style="border:1px solid black; width: 140px; height: 180px;display: table-cell; vertical-align: middle; text-align: center; box-shadow: 5px 10px 15px #888888;">
        <img id="imgPreview" runat="server" src="#" alt="your image" style="max-width:140px; max-height: 180px;" />
    </div>
</asp:Content>