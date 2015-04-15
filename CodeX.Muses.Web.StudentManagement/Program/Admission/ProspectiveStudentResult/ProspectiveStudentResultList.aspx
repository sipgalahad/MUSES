<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentResultList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentResultList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnAccept" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Terima")%></div></li>
    <li id="btnReject" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Tolak")%></div></li>
    <li id="btnOpen" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/redo.png")%>' alt="" /><div><%=GetLabel("Batal Proses")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $('#chkSelectAll').die('change');
        $('#chkSelectAll').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $chk = $(this).find('input');
                $chk.prop('checked', isChecked);
            });
        });

        $(function () {
            $('#<%=btnAccept.ClientID %>').click(function () {
                //if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                if (getCheckedMember()) {
                    if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                        showToast('Warning', 'Please Select Item First');
                    }
                    else {
                        onCustomButtonClick('accept');
                        //alert($('#<%=hdnLstGCMajor.ClientID %>').val());
                    }
                }
                //}
            });
            $('#<%=btnReject.ClientID %>').click(function () {
                //if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                    showToast('Warning', 'Please Select Item First');
                }
                else {
                    onCustomButtonClick('reject');
                }
                //}
            });
            $('#<%=btnOpen.ClientID %>').click(function () {
                //if (IsValid(null, 'fsMPEntry', 'mpEntry')) {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '') {
                    showToast('Warning', 'Please Select Item First');
                }
                else {
                    onCustomButtonClick('open');
                }
                //}
            });
        });

        function onAfterCustomClickSuccess() {
            cbpView.PerformCallback('refresh');
        }

        function getCheckedMember() {
            var isAllowSave = true;
            var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
            var lstGCMajor = $('#<%=hdnLstGCMajor.ClientID %>').val().split(',');
            var result = '';
            $('#tblView .chkIsSelected input').each(function () {
                $tr = $(this).closest('tr');
                if ($(this).is(':checked')) {
                    var key = $tr.find('.keyField').html();
                    if (lstSelectedMember.indexOf(key) < 0) {
                        var GCMajor = '';
                        var idx = $tr.find('.hdnItemIndex').val();
                        var cboGCMajor = eval('cboGCMajor' + idx);
                        cboGCMajor.Validate();
                        if (cboGCMajor.GetValue() != null && cboGCMajor.GetValue() != '')
                            GCMajor = cboGCMajor.GetValue();
                        lstSelectedMember.push(key);
                        lstGCMajor.push(GCMajor);
                        if (GCMajor == '')
                            isAllowSave = false;
                    }
                }
                else {
                    var key = $tr.find('.keyField').html();
                    if (lstSelectedMember.indexOf(key) > -1) {
                        var idx = lstSelectedMember.indexOf(key);
                        lstSelectedMember.splice(idx, 1);
                        lstGCMajor.splice(idx, 1);
                    }
                }
            });
            $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            $('#<%=hdnLstGCMajor.ClientID %>').val(lstGCMajor.join(','));
            return isAllowSave;
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
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
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            addTableHeader();
        }
        //#endregion

        function addItemFilterRow() {
            $trFilter = $("<tr><td></td><td></td><td></td><td colspan='10'></td></tr>");

            $inputCode = $("<input type='text' id='txtFilterCode' style='width:99%;height:20px' />").val($('#<%=hdnFilterCode.ClientID %>').val());
            $inputName = $("<input type='text' id='txtFilterName' style='width:99%;height:20px' />").val($('#<%=hdnFilterName.ClientID %>').val());
            $trFilter.find('td').eq(1).append($inputCode);
            $trFilter.find('td').eq(2).append($inputName);
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
                getCheckedMember();
                cbpView.PerformCallback('refresh');
            }
        });

        $('#txtFilterName').live('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                $('#<%=hdnFilterName.ClientID %>').val($(this).val());
                $('#<%=hdnFilterCode.ClientID %>').val($('#txtFilterCode').val());
                e.preventDefault();
                getCheckedMember();
                cbpView.PerformCallback('refresh');
            }
        });

        function onCboProspectiveStudentStatusValueChanged() {
            getCheckedMember();
            cbpView.PerformCallback('refresh');
        }

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
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" />
    <input type="hidden" id="hdnSelectedMember" runat="server" />
    <input type="hidden" id="hdnLstGCMajor" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnFilterCode" runat="server" />
    <input type="hidden" id="hdnFilterName" runat="server" />
    <div style="float:right">
        <table>
            <colgroup>
                <col style="width:100px"/>
                <col style="width:150px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><%=GetLabel("Status") %></td>
                <td>
                    <dxe:ASPxComboBox ID="cboProspectiveStudentStatus" runat="server" Width="150px">
                        <ClientSideEvents ValueChanged="function(s,e) { onCboProspectiveStudentStatusValueChanged() }" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
    </div>
    <br style="clear:both;" />
    <table id="tblView1" rules="all" style="display:none">
        <thead>
            <tr>
                <th rowspan="3" style="width:40px" class="thCenter"><input id="chkSelectAll" type="checkbox" /></th>
                <th rowspan="3" style="width:100px"><%=GetLabel("No Pendaftaran") %></th>
                <th rowspan="3"><%=GetLabel("Calon Siswa") %></th>
                <th id="thMarkHeader" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                <th rowspan="3" style="width:60px" class="thCenter"><%=GetLabel("Nilai Akhir") %></th>
                <th rowspan="3" style="width:60px" class="thCenter"><%=GetLabel("Kelas") %></th>
                <th rowspan="3" style="width:100px" class="thCenter"><%=GetLabel("Jurusan") %></th>
                <th rowspan="3" style="width:180px"><%=GetLabel("Status") %></th>
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
                        <th class="thCenter" style="width:50px"><%=GetLabel("Nilai") %></th>
                        <th class="thCenter" style="width:160px"><%=GetLabel("Keterangan") %></th>
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
                                            <td align="center"><asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" /></td>
                                            <td class="keyField"><%#Eval("RegistrationID")%></td>
                                            <td><%#Eval("RegistrationNo")%></td>
                                            <td><%#Eval("ProspectiveStudentName") %></td>
                                            <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                                <ItemTemplate>
                                                    <td align="center">
                                                        <div id="divStudentMark" runat="server"></div>
                                                    </td>
                                                    <td>
                                                        <div id="divStudentMarkRemarks" runat="server"></div>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td align="center"><%#Eval("FinalMark") %></td>
                                            <td><%#Eval("Grade") %></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                <dxe:ASPxComboBox ID="cboGCMajor" runat="server" Width="100px" />
                                            </td>
                                            <td><%#Eval("RegistrationStatus") %></td>
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