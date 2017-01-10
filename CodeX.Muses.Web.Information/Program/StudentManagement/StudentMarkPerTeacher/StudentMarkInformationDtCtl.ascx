<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentMarkInformationDtCtl.ascx.cs"
    Inherits="CodeX.Muses.Web.Information.Program.StudentMarkInformationDtCtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
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

    $(function () {
        setStudentImage();
    });
</script>
<style type="text/css">
    .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
</style>
<div style="overflow-y: auto; overflow-x: auto; max-height: 400px; max-width: 1000px;">
    <input type="hidden" id="hdnClassSubjectID" runat="server" />
    <input type="hidden" id="hdnPeriodSection" runat="server" />
    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
        <tr>
            <th rowspan="2"><%=GetLabel("Siswa") %></th>
            <th id="thMark" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
        </tr>
        <tr>
            <asp:Repeater ID="rptHeader" runat="server">
                <ItemTemplate>
                    <th class="thCenter" style="width:90px">
                        <%#Eval("cfClassTaskCode")%><br />
                        <%#Eval("TaskDate", "{0:dd-MMM-yyyy}")%>
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="keyField"><%#Eval("StudentID") %></td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 35px;">
                                    <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                    <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                    <div class="gridCircle divStudentImage"></div>
                                </td>
                                <td>
                                    <%#Eval("StudentName") %>
                                    <input type="hidden" id="Hidden1" class="hdnAttendance" runat="server" value="" />
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                    </td>
                    <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                        <ItemTemplate>
                            <td align="center">
                                <div id="divStudentMark" runat="server"></div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>