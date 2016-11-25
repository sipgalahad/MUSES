<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="EmployeeDailyAttendanceEntry.aspx.cs" Inherits="CodeX.Muses.Web.HumanResource.Program.EmployeeDailyAttendanceEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnUploadFile').click(function () {
                cbpProcess.PerformCallback('refresh');
            });

            $('#<%=FileUpload1.ClientID %>').change(function () {
                readURL(this);
            });

            setDatePicker('<%=txtDate.ClientID %>');
        })

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=hdnUploadedFile1.ClientID %>').val(e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function onCbpProcessEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'upload') {
                if (param[1] == 'fail')
                    showToast('Upload Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <style type="text/css">
        .grdSelected .highlighted td       { background-color: #F54F49; }
    </style>
    <div>
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                <td><asp:TextBox ID="txtDate" runat="server" Width="120px" CssClass="datepicker" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="hidden" id="hdnFileName" runat="server" value="" />
                    <input type="hidden" id="hdnUploadedFile1" runat="server" value="" />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <input type="button" id="btnUploadFile" value="Upload" />
                </td>
            </tr>
        </table>

    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>

</asp:Content>
