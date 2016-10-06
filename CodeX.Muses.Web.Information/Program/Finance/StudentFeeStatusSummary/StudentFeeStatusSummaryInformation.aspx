<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="StudentFeeStatusSummaryInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentFeeStatusSummaryInformation" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        //#region School Period
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "'";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName); 
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText(''); 
                    }
                    onTacSchoolPeriodValueChanged();
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
        }
        //#endregion

        $(function () {
            $('#btnRefresh').click(function () {
                $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
                $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
                $('#<%=hdnSchoolPeriodID.ClientID %>').val(tacSchoolPeriod.getValue()); 
                $('#<%=hdnSelectedYear.ClientID %>').val(cboYear.GetValue());
                $('#<%=hdnSelectedMonth.ClientID %>').val(cboMonth.GetValue());

                cbpView.PerformCallback('refresh');
            });
        });

        function onCbpViewEndCallback(s) {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.grdView').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            hideLoadingPanel();
        }

        $('.lblStudentCount').live('click', function () {
            var id = $(this).closest('tr').find('.hdnSchoolClassID').val();
            openStudentCountDetail(id, '');
        });

        $('.lblStudentPaidCount').live('click', function () {
            var id = $(this).closest('tr').find('.hdnSchoolClassID').val();
            openStudentCountDetail(id, '1');
        });

        $('.lblStudentNotPaidCount').live('click', function () {
            var id = $(this).closest('tr').find('.hdnSchoolClassID').val();
            openStudentCountDetail(id, '0');
        });

        function openStudentCountDetail(schoolClassID, type) {
            var id = schoolClassID + '|' + $('#<%=hdnSelectedMonth.ClientID %>').val() + '|' + $('#<%=hdnSelectedYear.ClientID %>').val() + '|' + type;
            var url = ResolveUrl("~/Program/Finance/StudentFeeStatusSummary/StudentFeeStatusSummaryDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Siswa', 700, 550);
        }
    </script>
    <input type="hidden" id="hdnSiteID" runat="server" />
    <input type="hidden" id="hdnSiteName" runat="server" />
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" />
    <input type="hidden" id="hdnSelectedYear" runat="server" />
    <input type="hidden" id="hdnSelectedMonth" runat="server" />
    <input type="hidden" id="hdnExportControl" runat="server" />
    <div>
        <table>
            <colgroup>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><%=GetLabel("Site") %></td>
                <td colspan="2"><dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px" /></td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
                <td colspan="2">
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                        SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                            ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><%=GetLabel("Periode")%></td>
                <td><dxe:ASPxComboBox ID="cboYear" Width="80px" ClientInstanceName="cboYear" runat="server" HorizontalAlign="Center" /></td>
                <td><dxe:ASPxComboBox ID="cboMonth" Width="120px" ClientInstanceName="cboMonth" runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><input type="button" id="btnRefresh" value='<%=GetLabel("Refresh") %>' /></td>
            </tr>
        </table>
    </div>
    <div>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <div id="divContainerView">
                            <table class="grdView notAllowSelect grdBorder" rules="all">
                                <tr>
                                    <th rowspan="2"><%=GetLabel("Kelas") %></th>
                                    <th rowspan="2" class="thCenter" style="width:100px;"><%=GetLabel("Jmlh Siswa") %></th>
                                    <th colspan="7" class="thCenter"><%=GetLabel("UANG SEKOLAH") %></th>
                                </tr>
                                <tr>
                                    <th class="thCenter" style="width:100px"><%=GetLabel("Uang Sekolah") %></th>
                                    <th class="thCenter" colspan="2"><%=GetLabel("Sudah Bayar") %></th>
                                    <th class="thCenter" style="width:50px"><%=GetLabel("%") %></th>
                                    <th class="thCenter" colspan="2" style="width:100px"><%=GetLabel("Belum Bayar") %></th>
                                    <th class="thCenter" style="width:50px"><%=GetLabel("%") %></th>
                                </tr>
                                <asp:Repeater ID="rptView" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="hidden" class="hdnSchoolClassID" value='<%#Eval("SchoolClassID") %>' />
                                                <%#Eval("SchoolClassCode") %>
                                            </td>
                                            <td align="center"><label class='lblLink lblStudentCount'><%#Eval("StudentCount") %></label></td>
                                            <td align="right"><%#Eval("StudentAmount", "{0:N}") %></td>
                                            <td align="right" style="width:150px;"><%#Eval("StudentPaidAmount", "{0:N}") %></td>
                                            <td align="right" style="width:50px;"><label class='lblLink lblStudentPaidCount'><%#Eval("StudentPaidCount") %></label></td> 
                                            <td align="right"><%#Eval("StudentPaidCountPercentage", "{0:N1}")%></td>
                                            <td align="right" style="width:150px;"><%#Eval("StudentNotPaidAmount", "{0:N}") %></td>
                                            <td align="right" style="width:50px;"><label class='lblLink lblStudentNotPaidCount'><%#Eval("StudentNotPaidCount") %></label></td>
                                            <td align="right"><%#Eval("StudentNotPaidCountPercentage", "{0:N1}")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td style="font-weight:bold"><%=GetLabel("JUMLAH") %></td>
                                    <td align="center"><div style="font-weight:bold" id="divTotalStudentCount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentAmount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentPaidAmount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentPaidCount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentPaidCountPercentage" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentNotPaidAmount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentNotPaidCount" runat="server"></div></td>
                                    <td align="right"><div style="font-weight:bold" id="divTotalStudentNotPaidCountPercentage" runat="server"></div></td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
