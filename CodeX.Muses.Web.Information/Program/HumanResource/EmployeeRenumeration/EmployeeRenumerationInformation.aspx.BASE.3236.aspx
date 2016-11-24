<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="EmployeeRenumerationInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.EmployeeRenumerationInformation" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView2();
            grd.init('grdStockDetail', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            //            setDatePicker('</%=txtDateFrom.ClientID %>');
            //            setDatePicker('</%=txtDateTo.ClientID %>');

            //            $('#</%=txtDateFrom.ClientID %>').change(function () {
            //                cbpView.PerformCallback('refresh');
            //            });
            //            $('#</%=txtDateTo.ClientID %>').change(function () {
            //                cbpView.PerformCallback('refresh');
            //            });

            cboOrganizationDepartment.SetValue('');

            $('#<%=txtEmployeeName.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=txtNIK.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });


            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            });

        });


        $('.lblFormula').live('click', function () {
            //var id = $(this).closest('tr').find('.keyField').html();
            $td = $(this).closest('td');
            var hdnEmp = $td.find('.hdnEmployeePositionTransID').val();
            var hdnRenum = $td.find('.hdnRenumerationTransID').val();
            var hdnEmpID = $td.find('.hdnEmployeeID').val();
            var hdnRenumCompID = $td.find('.hdnRenumerationCompID').val();
            var id = "";
            if (hdnEmp != "" && hdnEmp != "0") {
                id = hdnEmpID + "|" + hdnRenumCompID + "|" + hdnEmp;
                var url = ResolveUrl("~/Program/HumanResource/EmployeeRenumeration/EmployeeRenumerationInformationCtl.ascx");
                openUserControlPopup(url, id, 'Renumeration Formula', 600, 500);
            }
            else {
                id = "emp|" + hdnEmpID + "|" + hdnRenumCompID + "|" + hdnRenum;
                var url = ResolveUrl("~/Program/HumanResource/EmployeeRenumeration/RenumerationInformationDtCtl.ascx");
                openUserControlPopup(url, id, 'Renumeration Formula', 600, 500);
            }
        });
        

        //#region Location
        function getLocationFilterExpression() {
            var filterExpression = "<%=OnGetLocationFilterExpression() %>";
            return filterExpression;
        }

//        $('#lblLocation.lblLink').live('click', function () {
//            openSearchDialog('locationroleuser', getLocationFilterExpression(), function (value) {
//                $('#</%=txtLocationCode.ClientID %>').val(value);
//                onTxtLocationCodeChanged(value);
//            });
//        });

//        $('#</%=txtLocationCode.ClientID %>').live('change', function () {
//            onTxtLocationCodeChanged($(this).val());
//        });

//        function onTxtLocationCodeChanged(value) {
//            var filterExpression = getLocationFilterExpression() + "LocationCode = '" + value + "'";
//            Methods.getObject('GetLocationUserAccessList', filterExpression, function (result) {
//                if (result != null) {
//                    $('#</%=hdnLocationID.ClientID %>').val(result.LocationID);
//                    $('#</%=txtLocationName.ClientID %>').val(result.LocationName);
//                }
//                else {
//                    $('#</%=hdnLocationID.ClientID %>').val('');
//                    $('#</%=txtLocationCode.ClientID %>').val('');
//                    $('#</%=txtLocationName.ClientID %>').val('');
//                }
//            });

//            cbpView.PerformCallback('refresh');
//        }
        //#endregion

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

        //#region Organization Position
        function onGetOrganizationPositionFilterExpression() {
            var departmentID = cboOrganizationDepartment.GetValue();
            var filterExpression = " OrganizationDepartmentID = " + departmentID +" ";
            return filterExpression;
        }

        function onTacOrganizationPositionIDSearchClick() {
            openSearchDialog('OrganizationPosition', onGetOrganizationPositionFilterExpression(), function (value) {
                var filterExpression = onGetOrganizationPositionFilterExpression() + " AND OrganizationPositionID = '" + value + "' AND IsDeleted = 0 ";
                Methods.getObject('GetvOrganizationPositionList', filterExpression, function (result) {
                    if (result != null) {
                        tacOrganizationPositionID.setValue(result.OrganizationPositionID);
                        tacOrganizationPositionID.setText(result.OrganizationPositionName);
                    }
                    else {
                        tacOrganizationPositionID.setValue('');
                        tacOrganizationPositionID.setText('');
                    }
                });
            });
        }

        function onTacOrganizationPositionIDValueChanged() {

        }
        //#endregion
    </script>
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" value="" id="hdnOrganizationPositionID" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <table class="tblEntryContent" style="width:550px;">
                    <colgroup>
                        <col style="width: 150px" />
                        <col style="width: 400px" />
                    </colgroup>
                   <%-- <tr>
                        <td class="tdLabel"><label class="lblLink" id="lblLocation"><%=GetLabel("Lokasi") %></label></td>
                        <td>
                            <input type="hidden" id="hdnLocationID" runat="server" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="100px" />
                                    <col width="3px" />
                                    <col width="250px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox runat="server" ID="txtLocationCode" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtLocationName" Width="100%" Enabled="false" /></td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td class="tdLabel"><label><%=GetLabel("Tanggal") %></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateFrom" Width="120px" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox runat="server" CssClass="datepicker" ID="txtDateTo" Width="120px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("NIK")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtNIK" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Nama Karyawan")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtEmployeeName" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Department")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboOrganizationDepartment" ClientInstanceName="cboOrganizationDepartment" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal" id="lblPosition"><%=GetLabel("Jabatan")%></label></td>
                            <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacOrganizationPositionID" ClientInstanceName="tacOrganizationPositionID" MethodName="GetvOrganizationPositionList" GetFilterExpressionFunction="onGetOrganizationPositionFilterExpression"
                                SearchFields="OrganizationPositionName,OrganizationPositionID" TextField="OrganizationPositionName" ValueField="OrganizationPositionID" SearchText="${OrganizationPositionName} (<b>${PositionLevel}</b>)" OrderByExpression="OrganizationPositionName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacOrganizationPositionIDSearchClick(); }"
                                    ValueChanged="function(){ onTacOrganizationPositionIDValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
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
                                <table id="tblView" class="grdStockDetail grdSelected">
                                    <tr>
                                        <th style="width:100px" ><%=GetLabel("NIK Karyawan") %></th>
                                        <th><%=GetLabel("Nama Karyawan") %></th>
                                        <asp:Repeater ID="rptCompHd" runat="server">
                                            <ItemTemplate>
                                                <th class="thCenter" style="width:150px">
                                                    <%#Eval("RenumerationCompName") %>
                                                    <br />(<%#Eval("RenumerationCompType") %>)
                                                </th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("EmployeeCode") %></td>
                                                <td><%#Eval("EmployeeName") %></td>
                                                <asp:Repeater ID="rptCompDt" runat="server" OnItemDataBound="rptCompDt_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="right">
                                                            <input type="hidden" id="hdnEmployeeID" runat="server" class="hdnEmployeeID"/>
                                                            <input type="hidden" id="hdnRenumerationCompID" runat="server" class="hdnRenumerationCompID"/>
                                                            <input type="hidden" id="hdnEmployeePositionTransID" runat="server" class="hdnEmployeePositionTransID"/>
                                                            <input type="hidden" id="hdnRenumerationTransID" runat="server" class="hdnRenumerationTransID" />
                                                            <div id="divAmount" runat="server"></div>
                                                            <label id="lblFormula" style="display:none" runat="server" class="lblLink lblFormula"><%=GetLabel("Formula") %></label>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                               <%-- <asp:ListView runat="server" ID="lvwView">
                                    <EmptyDataTemplate>
                                        <table id="tblView" runat="server" class="grdStockDetail grdSelected" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                <th rowspan="2" style="width:60px"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Stok Awal")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Masuk")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Keluar")%></th>
                                                <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Stok Akhir")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Pembelian")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Pemakaian")%></th> 
                                            </tr>
                                            
                                            <tr class="trEmpty">
                                                <td colspan="25">
                                                    <%=GetLabel("No Data To Display")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="tblView" runat="server" class="grdStockDetail grdBorder grdSelected" cellspacing="0" rules="all" >
                                            <tr>  
                                                <th class="keyField" rowspan="2">&nbsp;</th>
                                                <th rowspan="2"><%=GetLabel("Nama Item")%></th>
                                                <th rowspan="2" style="width:60px"><%=GetLabel("Satuan")%></th>
                                                <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Stok Awal")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Masuk")%></th>
                                                <th colspan="3" class="thCenter"><%=GetLabel("Keluar")%></th>
                                                <th rowspan="2" class="thCenter" style="width:70px"><%=GetLabel("Stok Akhir")%></th>
                                            </tr>
                                            <tr>
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Pembelian")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 

                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Distribusi")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Penyesuaian")%></th> 
                                                <th style="width:70px" class="thCenter" align="right"><%=GetLabel("Pemakaian")%></th> 
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder" ></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%# Eval("ItemID")%></td>
                                            <td><label class="lblLink lblDetail"><%# Eval("ItemName1")%></label></td>
                                            <td><%# Eval("ItemUnit")%></td>
                                            <td align="right"><%# Eval("IN_QuantityBEGIN", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("IN_PurchaseReceive", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("IN_Distribution", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("IN_Adjustment", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("OUT_Distribution", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("OUT_Adjustment", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("OUT_Consumption", "{0:N2}")%></td>
                                            <td align="right"><%# Eval("QuantityEND", "{0:N2}")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>--%>
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
</asp:Content>
