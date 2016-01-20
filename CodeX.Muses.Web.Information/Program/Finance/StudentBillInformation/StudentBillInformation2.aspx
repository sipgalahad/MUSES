<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="StudentBillInformation2.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentBillInformation2" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btnRefresh').click(function () {
                onRefreshGridView();
            });
        })

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                setNumEntriesText($('#informationNumEntries'), rowCount, 1, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
        }
        //#endregion

        function onRefreshGridView() {
            $('#<%=hdnFilterExpressionQuickSearch.ClientID %>').val(txtSearchView.GenerateFilterExpression());
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
                setTimeout(function () {
                    s.SetFocus();
                }, 0);
            }, 0);
        }

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
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
        }
        //#endregion

        function onCboSiteValueChanged() {
            $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
            $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNowFilterExpression() %>";
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
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
        }

        function onCboViewTypeValueChanged() {
            $('#<%=hdnViewTypeID.ClientID %>').val(cboViewType.GetValue());
            $('#<%=hdnViewTypeName.ClientID %>').val(cboViewType.GetText());
        }

        $('.lblDetail.lblLink').live('click', function () {
            $tr = $(this).closest('tr');
            var studentID = $tr.find('.hdnStudentID').val() + '|' + cboViewType.GetValue();
            var url = ResolveUrl("~/Program/Finance/StudentBillInformation/StudentBillInformationDtCtl.ascx");
            openUserControlPopup(url, studentID, 'Detail Information', 700, 550);
        });

        $('.lblPrint.lblLink').live('click', function () {
            $tr = $(this).closest('tr');
            var studentID = $tr.find('.hdnStudentID').val();
            openReportViewer("FN-00001", studentID);
        });

    </script>
    <input type="hidden" value="" id="hdnSiteID" runat="server" />
    <input type="hidden" value="" id="hdnSiteName" runat="server" />
    <input type="hidden" value="" id="hdnViewTypeID" runat="server" />
    <input type="hidden" value="" id="hdnViewTypeName" runat="server" />
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <table style="width:50%">
                        <colgroup>
                            <col style="width:150px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
                            <td>
                                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                                </cdx:CodeXAutoCompleteTextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Search Filter")%></label></td>
                            <td>
                                <cdx:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView"
	                                Width="300px" Watermark="Search">
	                                <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
	                                <IntellisenseHints>
		                                <cdx:QISIntellisenseHint Text="Nama" FieldName="StudentName" />
		                                <cdx:QISIntellisenseHint Text="NIS" FieldName="StudentCode" />
	                                </IntellisenseHints>
                                </cdx:QISIntellisenseTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tipe Tagihan") %></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboViewType" ClientInstanceName="cboViewType" Width="200px">
                                    <ClientSideEvents Init="function(s,e){ onCboViewTypeValueChanged(); }"  ValueChanged="function(s,e){ onCboViewTypeValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>                        
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox runat="server" Text="Belum Dibayar" ID="chkNotPaid" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <div style="position: relative;">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <input type="hidden" value="" id="hdnMovementDate" runat="server" />
                                    <asp:Panel runat="server" ID="pnlGridView" CssClass="pnlContainerGrid" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:auto;">
                                        <table cellpadding="0" cellspacing="0" border="1" rules="all" class="grdSelected grdBorder">
                                            <colgroup>
                                                <col style="width:120px"/>
                                                <col/>
                                                <col style="width:150px"/>
                                                <col style="width:120px"/>
                                                <col style="width:120px"/>
                                                <col style="width:120px"/>
                                                <col style="width:120px"/>
                                                <col style="width:70px"/>
                                            </colgroup>
                                            <tr>
                                                <th rowspan="2" class="thCenter"><%=GetLabel("NBS") %></th>
                                                <th rowspan="2" class="thCenter"><%=GetLabel("Nama") %></th>
                                                <th rowspan="2" class="thCenter"><%=GetLabel("Kelas") %></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Jenis Pembayaran") %></th>
                                                <th rowspan="2" class="thCenter"><%=GetLabel("Total") %></th>
                                                <th rowspan="2" class="thCenter" id="thPrint" runat="server">Print</th>
                                            </tr>
                                            <tr>
                                                <th class="thCenter"><%=GetLabel("Uang Sekolah") %></th>
                                                <th class="thCenter"><%=GetLabel("Uang Kegiatan") %></th>
                                                <th class="thCenter"><%=GetLabel("Uang Pembangunan") %></th>
                                            </tr>
                                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%#Eval("StudentCode") %>
                                                        <input type="hidden" class="hdnStudentID" value='<%#Eval("StudentID") %>' />
                                                    </td>
                                                    <td><%#Eval("StudentName") %></td>
                                                    <td><%#Eval("SchoolClassCode") %></td>
                                                    <td align="right"><div id="divUsek" runat="server"></div></td>
                                                    <td align="right"><div id="divKeg" runat="server"></div></td>
                                                    <td align="right"><div id="divPemb" runat="server"></div></td>
                                                    <td align="right"><label class='lblDetail lblLink' id="lblClaimedAmount" runat="server"></label></td>
                                                    <td align="center" id="tdPrint" runat="server"><label class='lblPrint lblLink'>Print</label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            <tr style="display:none" id="trFooter" runat="server">
                                                <td colspan="3" style="font-weight:bold;" align="right"><%=GetLabel("Total") %></td>
                                                <td align="right"><div id="divTotalUsek" runat="server"></div></td>
                                                <td align="right"><div id="divTotalKeg" runat="server"></div></td>
                                                <td align="right"><div id="divTotalPemb" runat="server"></div></td>
                                                <td align="right"><div id="divTotalAll" runat="server"></div></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                        <div class="containerPaging">
                            <div class="divInformationNumEntries" id="informationNumEntries"></div>
                            <div class="wrapperPaging">
                                <div id="paging">
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

