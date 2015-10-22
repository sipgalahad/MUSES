<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentPaymentInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.ProspectiveStudentPaymentInformation" %>

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
        });

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

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
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

        function onCboSiteValueChanged() {
            $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
            $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
            tacSchoolPeriod.setValue('');
            tacSchoolPeriod.setText('');
        }

    </script>
    <input type="hidden" value="" id="hdnSiteID" runat="server" />
    <input type="hidden" value="" id="hdnSiteName" runat="server" />
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Search Filter")%></label></td>
                            <td>
                                <cdx:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView"
	                                Width="300px" Watermark="Search">
	                                <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
	                                <IntellisenseHints>
		                                <cdx:QISIntellisenseHint Text="Nama" FieldName="ProspectiveStudentName" />
		                                <cdx:QISIntellisenseHint Text="NBS" FieldName="ProspectiveStudentCode" />
	                                </IntellisenseHints>
                                </cdx:QISIntellisenseTextBox>
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
                                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" border="1" rules="all" class="grdSelected grdBorder">
                                                    <colgroup>
                                                        <col style="width:60px"/>
                                                        <col/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                        <col style="width:80px"/>
                                                    </colgroup>
                                                    <tr>
                                                        <th rowspan="3" class="thCenter"><%=GetLabel("NBS") %></th>
                                                        <th rowspan="3" class="thCenter"><%=GetLabel("Nama") %></th>
                                                        <th colspan="12" class="thCenter"><%=GetLabel("Jenis Pembayaran") %></th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="4" class="thCenter"><%=GetLabel("Uang Sekolah") %></th>
                                                        <th colspan="4" class="thCenter"><%=GetLabel("Uang Kegiatan") %></th>
                                                        <th colspan="4" class="thCenter"><%=GetLabel("Uang Pembangunan") %></th>
                                                    </tr>
                                                    <tr>
                                                        <th class="thCenter"><%=GetLabel("Total") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                        <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        <th class="thCenter"><%=GetLabel("Piutang") %></th>

                                                        <th class="thCenter"><%=GetLabel("Total") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                        <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        <th class="thCenter"><%=GetLabel("Piutang") %></th>

                                                        <th class="thCenter"><%=GetLabel("Total") %></th>
                                                        <th class="thCenter"><%=GetLabel("Diskon") %></th>
                                                        <th class="thCenter"><%=GetLabel("Bayar") %></th>
                                                        <th class="thCenter"><%=GetLabel("Piutang") %></th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("ProspectiveStudentCode") %></td>
                                                    <td><%#Eval("ProspectiveStudentName")%></td>
                                                    <td align="right"><div id="divUsekTotal" runat="server"></div></td>
                                                    <td align="right"><div id="divUsekDiskon" runat="server"></div></td>
                                                    <td align="right"><div id="divUsekBayar" runat="server"></div></td>
                                                    <td align="right" style="background-color: #6DDF7A"><div id="divUsekSisa" runat="server"></div></td>

                                                    <td align="right"><div id="divKegTotal" runat="server"></div></td>
                                                    <td align="right"><div id="divKegDiskon" runat="server"></div></td>
                                                    <td align="right"><div id="divKegBayar" runat="server"></div></td>
                                                    <td align="right" style="background-color: #6DDF7A"><div id="divKegSisa" runat="server"></div></td>

                                                    <td align="right"><div id="divPembTotal" runat="server"></div></td>
                                                    <td align="right"><div id="divPembDiskon" runat="server"></div></td>
                                                    <td align="right"><div id="divPembBayar" runat="server"></div></td>
                                                    <td align="right" style="background-color: #6DDF7A"><div id="divPembSisa" runat="server"></div></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            <tr>
                                                <td colspan="2" style="font-weight:bold;" align="right"><%=GetLabel("Total") %></td>
                                                <td align="right"><div id="divTotalUsek" runat="server"></div></td>
                                                <td align="right"><div id="divTotalUsekDiskon" runat="server"></div></td>
                                                <td align="right"><div id="divTotalUsekBayar" runat="server"></div></td>
                                                <td align="right" style="background-color: #6DDF7A"><div id="divTotalUsekSisa" runat="server"></div></td>

                                                <td align="right"><div id="divTotalKeg" runat="server"></div></td>
                                                <td align="right"><div id="divTotalKegDiskon" runat="server"></div></td>
                                                <td align="right"><div id="divTotalKegBayar" runat="server"></div></td>
                                                <td align="right" style="background-color: #6DDF7A"><div id="divTotalKegSisa" runat="server"></div></td>

                                                <td align="right"><div id="divTotalPemb" runat="server"></div></td>
                                                <td align="right"><div id="divTotalPembDiskon" runat="server"></div></td>
                                                <td align="right"><div id="divTotalPembBayar" runat="server"></div></td>
                                                <td align="right" style="background-color: #6DDF7A"><div id="divTotalPembSisa" runat="server"></div></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                        <div style="display:none">
                            <div class="containerPaging">
                                <div class="divInformationNumEntries" id="informationNumEntries"></div>
                                <div class="wrapperPaging">
                                    <div id="paging"></div>
                                </div>
                            </div> 
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
