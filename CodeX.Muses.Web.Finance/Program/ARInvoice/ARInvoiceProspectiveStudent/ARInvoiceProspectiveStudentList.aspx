<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="ARInvoiceProspectiveStudentList.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.ARInvoiceProspectiveStudentList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            Methods.checkImageError('imgStudentImage', 'student', 'hdnStudentGender');
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            Methods.checkImageError('imgStudentImage', 'student', 'hdnStudentGender');
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        $('.lnkDetail a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl('~/Program/ARInvoice/ARInvoiceProspectiveStudent/ProspectiveStudentPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'ProspectiveStudent', '1300', '650');
        });

        function onCboSchoolPeriodValueChanged() {
            tacPeriodAdmission.SetValue('');
            tacPeriodAdmission.SetText('');
        }

        //#region Period Admission
        function onGetPeriodAdmissionFilterExpression() {
            var filterExpression = "<%=OnGetPeriodAdmissionFilterExpression() %>";
            filterExpression += " AND SchoolPeriodID = " + tacSchoolPeriod.getValue();
            return filterExpression;
        }

        function onTacPeriodAdmissionButtonSearchClick() {
            openSearchDialog('periodadmission', onGetPeriodAdmissionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodAdmissionFilterExpression() + " AND PeriodAdmissionID = '" + value + "'";
                Methods.getObject('GetPeriodAdmissionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodAdmission.setValue(result.PeriodAdmissionID);
                        tacPeriodAdmission.setText(result.PeriodAdmissionName);
                    }
                    else {
                        tacPeriodAdmission.setValue('');
                        tacPeriodAdmission.setText('');
                    }
                    cbpView.PerformCallback('refresh');
                });
            });
        }

        function onTacPeriodAdmissionValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

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
            cbpView.PerformCallback('refresh');
        }
        //#endregion
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <table>
            <colgroup>
                <col style="width:120px"/>
            </colgroup>
            <tr>
                <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
                <td>
                    <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                        <ClientSideEvents ValueChanged="function(s,e) { onCboSiteValueChanged(s); }" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                        SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                            ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Gelombang")%></label></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodAdmission" ClientInstanceName="tacPeriodAdmission" MethodName="GetPeriodAdmissionList" GetFilterExpressionFunction="onGetPeriodAdmissionFilterExpression"
                        SearchFields="PeriodAdmissionName,PeriodAdmissionCode" TextField="PeriodAdmissionName" ValueField="PeriodAdmissionID" SearchText="${PeriodAdmissionName} (<b>${PeriodAdmissionCode}</b>)" OrderByExpression="PeriodAdmissionName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodAdmissionButtonSearchClick(); }"
                            ValueChanged="function(){ onTacPeriodAdmissionValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
        </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="overflow-y: scroll;">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="ProspectiveStudentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="600px">
                                    <HeaderTemplate><%=GetLabel("Informasi Siswa")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding:3px">
                                            <img class="imgStudentImage" src='<%#Eval("ProspectiveStudentImageUrl") %>' alt="" height="55px" width="40px" style="float:left;margin-right: 10px;" />
                                            <div><%# Eval("ProspectiveStudentName") %></div>
                                            <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                            <table cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col style="width:100px"/>
                                                    <col style="width:10px"/>
                                                    <col style="width:250px"/>
                                                    <col style="width:50px"/>
                                                    <col style="width:10px"/>
                                                    <col style="width:70px"/>
                                                    <col style="width:60px"/>
                                                    <col style="width:10px"/>
                                                </colgroup>
                                                <tr>
                                                    <td style="text-align:right;font-size:0.9em;font-style:italic"><%=GetLabel("Nama Panggilan")%></td>
                                                    <td>&nbsp;</td>
                                                    <td><%# Eval("PreferredName")%></td>
                                                    <td style="text-align:right;font-size:0.9em;font-style:italic"><%=GetLabel("NIS")%></td>
                                                    <td>&nbsp;</td>
                                                    <td><%# Eval("ProspectiveStudentCode")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;font-size:0.9em;font-style:italic"><%=GetLabel("Tanggal Lahir")%></td>
                                                    <td>&nbsp;</td>
                                                    <td><%# Eval("DateOfBirthInString")%></td>
                                                    <td style="text-align:right;font-size:0.9em;font-style:italic"><%=GetLabel("Umur")%></td>
                                                    <td>&nbsp;</td>
                                                    <td><%# Eval("StudentAge")%></td>
                                                </tr>
                                            </table>                                                                                    
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate><%=GetLabel("Informasi Kontak")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding:3px">
                                            <div><%# Eval("HomeAddress")%></div>
                                            <img src='<%= ResolveUrl("~/Libs/Images/homephone.png")%>' alt='' style="float:left;" /><div style="margin-left:30px"><%# Eval("PhoneNo1")%>&nbsp;</div>
                                            <img src='<%= ResolveUrl("~/Libs/Images/mobilephone.png")%>' alt='' style="float:left;" /><div style="margin-left:30px"><%# Eval("MobilePhoneNo1")%>&nbsp;</div>                                                  
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Detil" Text="Detil" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkDetail" HeaderStyle-Width="120px" HeaderStyle-CssClass="thCenter" />
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("Data Tidak Tersedia")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>