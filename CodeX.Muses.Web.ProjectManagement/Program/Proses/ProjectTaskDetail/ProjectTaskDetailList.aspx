<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProjectTaskDetailList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskDetailList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');
            setProjectTaskRemarks();
            
            $('#btnRefresh').click(function () {
                showLoadingPanel();
                cbpView.PerformCallback('refresh');
            });
        })

        $('.lblProjectTaskName').die('click');
        $('.lblProjectTaskName').live('click', function () {
            var url = ResolveUrl('~/Program/Proses/ProjectTaskDetail/ProjectTaskLogCtl.ascx');
            var id = $(this).closest('tr').find('.keyField').html();
            openUserControlPopup(url, id, 'Log', 900, 500);
        });

        $('.lblAssignName').die('click');
        $('.lblAssignName').live('click', function () {
            var url = ResolveUrl('~/Program/Proses/ProjectTaskDetail/ProjectTaskDetailAssignCtl.ascx');
            var id = $(this).closest('tr').find('.keyField').html();
            openUserControlPopup(url, id, 'Assignee', 900, 500);
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
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
                setProjectTaskRemarks();
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        //#region Employee
        window.onGetEmployeeFilterExpression = function () {
            var filterExpression = "<%=OnGetEmployeeFilterExpression() %>";
            if ($('#<%=hdnLstEmployeeID.ClientID %>').val() != ""){
                filterExpression += " AND EmployeeID IN (" + $('#<%=hdnLstEmployeeID.ClientID %>').val() + ")";
            }
            return filterExpression;
        }

        function onTacEmployeeCoordinatorButtonSearchClick() {
            var filterExpression = onGetEmployeeFilterExpression();
            openSearchDialog('employee', filterExpression, function (value) {
                var filterExpression = "EmployeeCode = '" + value + "'";
                Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        tacEmployee.setValue(result.EmployeeID);
                        tacEmployee.setText(result.EmployeeName);
                        //cbpView.PerformCallback('refresh');
                    }
                    else {
                        tacEmployee.setValue('');
                        tacEmployee.setText('');
                    }
                });
            });
        }

        function onTacEmployeeCoordinatorValueChanged() {
            var id = tacEmployee.getValue();
            if (id != '') {
                var filterExpression = "EmployeeID = " + id;
                Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                    //cbpView.PerformCallback('refresh');
                });
            }
        }
        //#endregion

        function setProjectTaskRemarks() {
            var open = parseInt($('#<%=hdnOpen.ClientID %>').val());
            var inProgress = parseInt($('#<%=hdnInProgress.ClientID %>').val());
            var closed = parseInt($('#<%=hdnClosed.ClientID %>').val());
            var total = parseInt($('#<%=hdnTotalTask.ClientID %>').val());
            $('#tdOpen').text(open + " / " + total);
            $('#tdInProgress').text(inProgress + " / " + total);
            $('#tdClosed').text(closed + " / " + total);

            var finish = closed / total * 100;
            var pending = (open + inProgress) / total * 100;
            $('#tdFinish').text(Math.floor(finish) + "%");
            $('#tdPending').text(Math.ceil(pending) + "%");
        }
    </script>
    <table width="100%" style="margin-bottom:10px;">
        <tr>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td class="tdLabel" style="width:100px;"><%=GetLabel("Status") %></td>
                        <td>
                            <dxe:ASPxComboBox runat="server" ID="cboStatus" ClientInstanceName="cboStatus" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="width:100px;"><%=GetLabel("Karyawan") %></td>
                        <td>
                            <input type="hidden" id="hdnEmployeeID" value="" runat="server" />
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacEmployee" ClientInstanceName="tacEmployee" MethodName="GetvEmployeeList" GetFilterExpressionFunction="onGetEmployeeFilterExpression"
                                SearchFields="EmployeeName,EmployeeCode" TextField="EmployeeName" ValueField="EmployeeID" SearchText="${EmployeeName} (<b>${EmployeeCode}</b>)" OrderByExpression="EmployeeName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacEmployeeCoordinatorButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacEmployeeCoordinatorValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><%=GetLabel("Tanggal")%></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col />
                                    <col width="30px" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStartDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    <td style="text-align:center">s/d</td>
                                    <td><asp:TextBox ID="txtEndDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="button" value="Refresh" id="btnRefresh" /></td>
                    </tr>
                </table>
            </td>
            <td valign="top" align="right">
                <div style="width:400px;height:80px; background-color:#F4F4F4; text-align:left; padding:3px;" id="divKeterangan" runat="server">
                    <div style="font-weight:bold;">Keterangan :</div>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="50%" />
                            <col />
                        </colgroup>
                        <tr>
                            <td valign="top">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="100px;"/>
                                        <col width="7px;"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td>Open</td>
                                        <td align="center">:</td>
                                        <td id="tdOpen"></td>
                                    </tr>
                                    <tr>
                                        <td>In Progress</td>
                                        <td align="center">:</td>
                                        <td id="tdInProgress"></td>
                                    </tr>
                                    <tr>
                                        <td>Closed</td>
                                        <td align="center">:</td>
                                        <td id="tdClosed"></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="50%"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td align="center" style="font-weight:bold;">Finish</td>
                                        <td align="center" style="font-weight:bold;">Pending</td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size:x-large;" id="tdFinish"></td>
                                        <td align="center" style="font-size:x-large;" id="tdPending"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <input type="hidden" id="hdnID" runat="server" value="" />
        <input type="hidden" id="hdnLstTeamDtID" runat="server" value="" />
        <input type="hidden" id="hdnLstEmployeeID" runat="server" value="" />
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnTotalTask" runat="server" />
                        <input type="hidden" id="hdnOpen" runat="server" />
                        <input type="hidden" id="hdnInProgress" runat="server" />
                        <input type="hidden" id="hdnClosed" runat="server" />
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ProjectTaskID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="ProjectTaskCode" HeaderText="Kode" HeaderStyle-Width="70px"/>
                                <asp:TemplateField HeaderText="Nama">
                                    <ItemTemplate>
                                        <label runat="server" id="lblProjectTaskName"><%#Eval("ProjectTaskName")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" HeaderStyle-Width="200px"/>
                                <asp:BoundField DataField="Position" HeaderText="Posisi" HeaderStyle-Width="150px"/>
                                <asp:BoundField DataField="StartDateInString" HeaderText="Mulai" HeaderStyle-Width="100px"/>
                                <asp:BoundField DataField="EndDateInString" HeaderText="Deadline" HeaderStyle-Width="100px"/>
                                <asp:TemplateField HeaderText="Assign" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <label runat="server" id="lblAssignName"><%#Eval("CustomAssignName")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Keterangan" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <%#Eval("CustomRemarks")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <div id="divStatus" runat="server" style="width:100%;">
                                            <%#Eval("ProjectTaskStatus")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("No Data To Display")%>
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