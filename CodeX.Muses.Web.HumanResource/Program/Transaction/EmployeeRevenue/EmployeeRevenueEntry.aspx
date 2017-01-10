<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="EmployeeRevenueEntry.aspx.cs" Inherits="CodeX.Muses.Web.HumanResource.Program.EmployeeRevenueEntry" %>

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

            $('#btnRefresh').click(function () {
                cbpProcess.PerformCallback('process');
            });

//            $('#</%=FileUpload1.ClientID %>').change(function () {
//                readURL(this);
//            });

//            $('#</%=txtStartDate.ClientID %>').change(function () {
//                cbpView.PerformCallback('refresh');
//            });
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

        });

        

//        function readURL(input) {
//            if (input.files && input.files[0]) {
//                var reader = new FileReader();
//                reader.onload = function (e) {
//                    $('#</%=hdnUploadedFile1.ClientID %>').val(e.target.result);
//                }
//                reader.readAsDataURL(input.files[0]);
//            }
//        }

        function onCbpProcessEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'process') {
                if (param[1] == 'fail')
                    showToast('Process Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
            else if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
            }
        }

        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('.grdStockDetail tr:eq(2)').click();

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('.grdStockDetail tr:eq(2)').click();
        }

        
    </script>
    <style type="text/css">
        .grdSelected .highlighted td       { background-color: #F54F49; }
    </style>
    <div>
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <input type="hidden" id="hdnScheduleStart" runat="server" value="" />
        <input type="hidden" id="hdnScheduleEnd" runat="server" value="" />
        <input type="hidden" id="hdnStartTime" runat="server" value="" />
        <input type="hidden" id="hdnEndTime" runat="server" value="" />
        <input type="hidden" id="hdnNoOfWorkTimeHour" runat="server" value="" />
        <input type="hidden" id="hdnDailyRenumerationMultiplyBy" runat="server" value="" />
        <input type="hidden" id="hdnOvertimeProposalStartTime" runat="server" value="" />
        <input type="hidden" id="hdnOvertimeProposalEndTime" runat="server" value="" />
        <input type="hidden" id="hdnOvertimeProposalTotalHour" runat="server" value="" />
        <input type="hidden" id="hdnNoOfOvertimeHour" runat="server" value="" />
        <input type="hidden" id="hdnStatus" runat="server" value="" />
        <table class="tblEntryContent" style="width:100%;">
            <colgroup>
                <col style="width: 150px" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Start Date")%></label></td>
                <td><asp:TextBox ID="txtStartDate" runat="server" Width="120px" CssClass="datepicker" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("End Date")%></label></td>
                <td><asp:TextBox ID="txtEndDate" runat="server" Width="120px" CssClass="datepicker" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><label></label></td>
                <td><input type="button" id="btnRefresh" class="btnRefresh" value="Process" /></td>
            </tr>
            <%--<tr>
                <td></td>
                <td>
                    <input type="hidden" id="hdnFileName" runat="server" value="" />
                    <input type="hidden" id="hdnUploadedFile1" runat="server" value="" />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <input type="button" id="btnUploadFile" value="Upload" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView" ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                             <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                position: relative; font-size: 0.95em;">
                                <input type="hidden" id="Hidden1" value="" runat="server" />
                                <table id="tblView" class="grdAttendance grdSelected grdBorder" rules="all" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th  class="thCenter" ><%=GetLabel("Nama Karyawan") %></th>
                                    </tr>
                                    <asp:Repeater ID="rptView" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%#Eval("EmployeeID") %></td>
                                                <td><label class="lblEmployee lblLink"><%#Eval("EmployeeName")%></label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="imgLoadingGrdView" id="containerImgLoadingView">
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
                <div class="containerPaging">
                    <div class="divInformationNumEntries" id="informationNumEntries"></div>
                    <div class="wrapperPaging">
                        <div id="paging">
                        </div>
                    </div>
                </div>
            </td>
            </tr>
        </table>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>

</asp:Content>
