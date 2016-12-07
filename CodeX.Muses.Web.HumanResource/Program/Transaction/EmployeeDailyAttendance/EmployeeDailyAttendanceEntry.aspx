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
                cbpProcess.PerformCallback('saveFile');
                $('#<%=txtDate.ClientID %>').change();
            });

            $('#<%=FileUpload1.ClientID %>').change(function () {
                readURL(this);
            });

            $('#<%=txtDate.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });
            setDatePicker('<%=txtDate.ClientID %>');

        });

        $('.grdAttendance .btnSave').live('click', function (evt) {
            if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                $tr = $(this).closest('tr');

                $('#<%=hdnID.ClientID %>').val($tr.find('.keyField').html());
                $('#<%=hdnScheduleStart.ClientID %>').val($tr.find('.txtScheduleStartTime').val());
                $('#<%=hdnScheduleEnd.ClientID %>').val($tr.find('.txtScheduleEndTime').val());
                $('#<%=hdnStartTime.ClientID %>').val($tr.find('.txtStartTime').val());
                $('#<%=hdnEndTime.ClientID %>').val($tr.find('.txtEndTime').val());
                $('#<%=hdnNoOfWorkTimeHour.ClientID %>').val($tr.find('.txtNoOfWorkTimeHour').val());
                $('#<%=hdnDailyRenumerationMultiplyBy.ClientID %>').val($tr.find('.txtDailyRenumerationMultiplyBy').val());
                $('#<%=hdnOvertimeProposalStartTime.ClientID %>').val($tr.find('.txtOvertimeProposalStartTime').val());
                $('#<%=hdnOvertimeProposalEndTime.ClientID %>').val($tr.find('.txtOvertimeProposalEndTime').val());
                $('#<%=hdnOvertimeProposalTotalHour.ClientID %>').val($tr.find('.txtOvertimeProposalTotalHour').val());
                $('#<%=hdnNoOfOvertimeHour.ClientID %>').val($tr.find('.txtNoOfOvertimeHour').val());
                $('#<%=hdnStatus.ClientID %>').val($tr.find('.ddlAttendanceStatus option:selected').val());
                //alert($('#<%=hdnScheduleStart.ClientID %>').val());
                cbpProcess.PerformCallback('save');
            }
        });

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

        $('.lblEmployee').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + "|" + $('#<%=txtDate.ClientID %>').val();
            var url = ResolveUrl("~/Program/Transaction/EmployeeDailyAttendance/EmployeeDailyAttendanceDetailsCtl.ascx");
            //alert(id);
            openUserControlPopup(url, id, 'Finger Print', 600, 500);
        });
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
                                        <th rowspan ="2" class="thCenter" ><%=GetLabel("Nama Karyawan") %></th>
                                        <th colspan ="2" class="thCenter" ><%=GetLabel("Jadwal") %></th>
                                        <th colspan ="4" class="thCenter" ><%=GetLabel("Absen") %></th>
                                        <th colspan ="3" class="thCenter" ><%=GetLabel("Pengajuan Lembur") %></th>
                                        <th class="thCenter" ><%=GetLabel("Realisasi Lembur") %></th>
                                        <th rowspan ="2" class="thCenter" style="width : 100px;"><%=GetLabel("Status Kehadiran") %></th>
                                        <th rowspan ="2" class="thCenter" style="width : 20px"></th> 
                                    </tr>
                                    <tr>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Masuk") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Pulang") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Masuk") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Pulang") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Total Jam") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Pengali Harian") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Start") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Selesai") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Total Jam") %></th>
                                        <th class="thCenter" style="width : 60px;"><%=GetLabel("Total Jam") %></th>
                                    </tr>
                                    <asp:Repeater ID="rptView" runat="server"  OnItemDataBound="rptView_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%#Eval("EmployeeID") %></td>
                                                <td><label class="lblEmployee lblLink"><%#Eval("EmployeeName")%></label></td>
                                                <td align="center"><input type="text" class="txtScheduleStartTime time" value='<%#Eval("ScheduleStartTime")%>' style="width:100%" readonly="readonly" /></td>
                                                <td align="center"><input type="text" class="txtScheduleEndTime time" value='<%#Eval("ScheduleEndTime")%>' style="width:100%" readonly="readonly" /></td>
                                                <td align="center"><input type="text" class="txtStartTime time" value='<%#Eval("StartTime")%>' style="width:100%" /></td>
                                                <td align="center"><input type="text" class="txtEndTime time" value='<%#Eval("EndTime")%>' style="width:100%" /></td>
                                                <td align="center"><input type="text" class="txtNoOfWorkTimeHour number" value='<%#Eval("NoOfWorkTimeHour")%>' style="width:100%" /></td>
                                                <td align="center"><input type="text" class="txtDailyRenumerationMultiplyBy number" value='<%#Eval("DailyRenumerationMultiplyBy")%>' style="width:100%" /></td>
                                                <td align="center"><input type="text" class="txtOvertimeProposalStartTime time" value='<%#Eval("OvertimeProposalStartTime")%>' style="width:100%" readonly="readonly" /></td>
                                                <td align="center"><input type="text" class="txtOvertimeProposalEndTime time" value='<%#Eval("OvertimeProposalEndTime")%>' style="width:100%" readonly="readonly" /></td>
                                                <td align="center"><input type="text" class="txtOvertimeProposalTotalHour number" value='<%#Eval("OvertimeProposalTotalHour")%>' style="width:100%" /></td>
                                                <td align="center"><input type="text" class="txtNoOfOvertimeHour number" value='<%#Eval("NoOfOvertimeHour")%>' style="width:100%" /></td>
                                                <td><asp:DropDownList ID="ddlAttendanceStatus" CssClass="ddlAttendanceStatus" runat="server" Width="100%" /></td>
                                                <td><input type="button" class="btnSave btnWhite" value='<%=GetLabel("Save") %>'/></td>
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
