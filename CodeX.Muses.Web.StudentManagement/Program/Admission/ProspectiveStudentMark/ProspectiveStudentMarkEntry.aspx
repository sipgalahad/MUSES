<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentMarkEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentMarkEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.grdStudent tr:gt(3)').each(function () {
                    var studentID = $(this).find('.keyField').html();
                    var tempResult = '';
                    $(this).find('.txtMark').each(function () {
                        var mark = $(this).val();
                        var remarks = $(this).closest('td').next('td').find('.txtStudentMarkRemarks').val();
                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += mark + ';' + remarks;
                    });
                    if (result != '')
                        result += '|';
                    result += studentID + ',' + tempResult;
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });
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

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            addTableHeader();
        }
        //#endregion

        function addItemFilterRow() {
            $trFilter = $("<tr><td></td><td></td><td colspan='10'></td></tr>");

            $inputCode = $("<input type='text' id='txtFilterCode' style='width:99%;height:20px' />").val($('#<%=hdnFilterCode.ClientID %>').val());
            $inputName = $("<input type='text' id='txtFilterName' style='width:99%;height:20px' />").val($('#<%=hdnFilterName.ClientID %>').val());
            $trFilter.find('td').eq(0).append($inputCode);
            $trFilter.find('td').eq(1).append($inputName);
            if ($('#tblView tbody tr').length > 0) {
                $trHeader = $('#tblView tbody tr:eq(0)');
                $trFilter.insertBefore($trHeader);
            }
            else {
                $('#tblView tbody').append($trFilter);
            }
        }

        $('#txtFilterCode').live('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                $('#<%=hdnFilterCode.ClientID %>').val($(this).val());
                $('#<%=hdnFilterName.ClientID %>').val($('#txtFilterName').val());
                e.preventDefault();
                cbpView.PerformCallback('refresh');
            }
        });

        $('#txtFilterName').live('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                $('#<%=hdnFilterName.ClientID %>').val($(this).val());
                $('#<%=hdnFilterCode.ClientID %>').val($('#txtFilterCode').val());
                e.preventDefault();
                cbpView.PerformCallback('refresh');
            }
        });

        $(function () {
            addTableHeader();
        });

        function addTableHeader() {
            $('#tblView thead').html($('#tblView1 thead').html());
            addItemFilterRow();
        }
    </script>
    <style type="text/css">
        .grdStudent th b        { color: Red; }
    </style>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnFilterCode" runat="server" />
    <input type="hidden" id="hdnFilterName" runat="server" />
    <table id="tblView1" rules="all" style="display:none">
        <thead>
            <tr>
                <th rowspan="3" style="width:100px"><%=GetLabel("No Pendaftaran") %></th>
                <th rowspan="3"><%=GetLabel("Calon Siswa") %></th>
                <th id="thMarkHeader" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" colspan="2">
                            <%#Eval("SelectionName")%> (<b><%#Eval("FinalMarkPercentage")%>%</b>)
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader2" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:60px"><%=GetLabel("Nilai") %></th>
                        <th class="thCenter" style="width:200px"><%=GetLabel("Keterangan") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </thead>
    </table>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <table rules="all" id="tblView" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                            <thead>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%#Eval("RegistrationID")%></td>
                                            <td><%#Eval("RegistrationNo")%></td>
                                            <td><%#Eval("ProspectiveStudentName") %></td>
                                            <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtMark" Text="" Width="95%" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtStudentMarkRemarks" runat="server" CssClass="txtStudentMarkRemarks" Text="" Width="95%" />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>                            
                            </tbody>
                        </table>
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