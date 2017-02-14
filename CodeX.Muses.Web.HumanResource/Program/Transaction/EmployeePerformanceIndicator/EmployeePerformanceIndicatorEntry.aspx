<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master"
    AutoEventWireup="true" CodeBehind="EmployeePerformanceIndicatorEntry.aspx.cs" Inherits="CodeX.Muses.Web.HumanResource.Program.EmployeePerformanceIndicatorEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView2();
            grd.init('grdStockDetail', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

//            $('#</%=txtJobLevel.ClientID %>').change(function () {
//                cbpView.PerformCallback('refresh');
//            });

            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            });

        });

        $('.grdStockDetail .btnSave').live('click', function (evt) {
            if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                $tr = $(this).closest('tr');

                $('#<%=hdnID.ClientID %>').val($tr.find('.keyField').html());
                $('#<%=hdnPerformanceID.ClientID %>').val($tr.find('.hdnPerformanceIndicatorID').val());
                $('#<%=hdnInput.ClientID %>').val($tr.find('.txtInput').val());
                cbpProcess.PerformCallback('save');
              
                
            }
        });

       

        function onCbpProcessEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
//            if (param[0] == 'upload') {
//                if (param[1] == 'fail')
//                    showToast('Upload Failed', 'Error Message : ' + param[2]);
//                else
//                    cbpView.PerformCallback('refresh');
//            }
           if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
            }
        }


        $('.lblFormula').live('click', function () {
            //var id = $(this).closest('tr').find('.keyField').html();
            $td = $(this).closest('td');
            var hdnOp = $td.find('.hdnOrganizationPositionID').val();
            var hdnRenum = $td.find('.hdnPeriodTransID').val();
            var hdnRenumCompID = $td.find('.hdnPeriodCompID').val();
            var id = "jl|" + hdnOp + "|" + hdnRenumCompID + "|" + hdnRenum;
            var url = ResolveUrl("~/Program/HumanResource/EmployeePeriod/PeriodInformationDtCtl.ascx");
            openUserControlPopup(url, id, 'Period Formula', 600, 500);
        });

        //#region Paging
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
        //#endregion

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGrid();
            }, 0);
        }

        function onCboTypeValueChanged() {
            onRefreshGrid();
        }

        function onCboDepartmentValueChanged() {
            onRefreshGrid();
        }

        $('.lblDetail').live('click', function () {
            $tr = $(this).closest('tr');
            var itemID = $tr.find('.keyField').html();

            var url = ResolveUrl("~/Program/Inventory/StockDetailInfoDtCtl.ascx");
            openUserControlPopup(url, itemID, 'Detail Information', 1200, 550);
        });

        //#region Period
        function onGetPeriodFilterExpression() {
            var filterExpression = " isDeleted =  0 ";
            return filterExpression;
        }

        function onTacPeriodSearchClick() {
            openSearchDialog('revenueperiod', onGetPeriodFilterExpression(), function (value) {
                var filterExpression = onGetPeriodFilterExpression() + " AND RevenuePeriodID = '" + value + "'";
                Methods.getObject('GetRevenuePeriodList', filterExpression, function (result) {
                    if (result != null) {
                        //var tempStartDate = setDatePicker(result.StartDate);
                        //var tempEndDate = setDatePicker(result.EndDate);
                        $('#<%=hdnPeriod.ClientID %>').val(result.RevenuePeriodID);
                        tacPeriod.setValue(result.RevenuePeriodID);
                        tacPeriod.setText(result.StartDate + "-" + result.EndDate);
                    }
                    else {
                        $('#<%=hdnPeriod.ClientID %>').val(result.PeriodID);
                        tacPeriod.setValue('');
                        tacPeriod.setText('');
                    }
                });
            });
        }

        function onTacPeriodValueChanged() {
        }
        //#endregion

    </script>
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnOrganizationPositionID" runat="server" />
    <input type="hidden" value="" id="hdnInput" runat="server" />
    <input type="hidden" value="" id="hdnPerformanceID" runat="server" />
    <input type="hidden" value="" id="hdnPeriod" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <table class="tblEntryContent" style="width:550px;">
                    <colgroup>
                        <col style="width: 150px" />
                        <col style="width: 400px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Nama Karyawan")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtNamaKaryawan" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Periode")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriod" ClientInstanceName="tacPeriod" MethodName="GetRevenuePeriodList" GetFilterExpressionFunction="onGetPeriodFilterExpression"
                                            SearchFields="StartDate,EndDate,RevenuePeriodID" TextField="StartDate" ValueField="RevenuePeriodID" SearchText="${StartDate} (<b>${EndDate}</b> (<b>${RevenuePeriodID}</b>)" OrderByExpression="StartDate">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSearchClick(); }"
                                                ValueChanged="function(){ onTacPeriodValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Karyawan")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboEmployeeType" ClientInstanceName="cboEmployeeType" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label></label></td>
                        <td><input type="button" id="btnRefresh" class="btnRefresh" value="Search" /></td>
                    </tr>
                </table>
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
                                <input type="hidden" id="hdnFilterExpression" value="" runat="server" />
                                <table id="tblView" class="grdStockDetail grdSelected grdBorder" rules="all" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th><%=GetLabel("Karyawan") %></th>
                                        <asp:Repeater ID="rptPerfomanceIndicatorHd" runat="server">
                                            <ItemTemplate>
                                                <th class="thCenter" style="width:80px">
                                                    <%#Eval("PerformanceIndicatorName")%>
                                                </th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <th class="thRight" style="width:80px"> 
                                            <%#Eval(" ")%>
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="keyField"><%#Eval("EmployeeID")%></td>
                                                <td><label class="lblEmployee lblLink"><%#Eval("EmployeeName")%></label></td>
                                                <asp:Repeater ID="rptCompDt" runat="server" OnItemDataBound="rptCompDt_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right">
                                                            <input type="hidden" class="hdnPerformanceIndicatorID" value='<%#Eval("PerformanceIndicatorID") %>' />
                                                            <asp:TextBox runat="server" ID="txtInput" class="txtInput number" Width="80px" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <td><input type="button" class="btnSave btnWhite"  value='<%=GetLabel("Save") %>'/></td>
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
                <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
                    ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpProcessEndCallback(s); }" />
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
