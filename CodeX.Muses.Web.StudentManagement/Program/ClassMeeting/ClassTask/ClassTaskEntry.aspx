<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassTaskEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskEntry" %>

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
                if (IsValid(null, 'fsMark', 'mpMark')) {
                    var GCSubjectMarkType = $('#<%=hdnGCSubjectMarkType.ClientID %>').val();
                    var result = '';
                    $('.grdStudent tr.trStudent').each(function () {
                        var studentID = $(this).find('.keyField').html();
                        var mark = '';
                        switch (GCSubjectMarkType) {
                            case '<%=OnGetSubjectMarkTypeNumber() %>': mark = $(this).find('.txtMark').val(); break;
                            case '<%=OnGetSubjectMarkTypeOption() %>':
                                var idx = $(this).find('.hdnItemIndex').val();
                                var cboStudentMarkOption = eval('cboStudentMarkOption' + idx);
                                if (cboStudentMarkOption.GetValue() != null && cboStudentMarkOption.GetValue() != '0')
                                    mark = cboStudentMarkOption.GetValue(); break;
                            case '<%=OnGetSubjectMarkTypeText() %>': mark = $(this).find('.txtStudentMarkDescription').val(); break;
                        }

                        if (result != '')
                            result += '|';
                        result += studentID + ',' + mark;
                    });
                    $('#<%=hdnListSaveValue.ClientID %>').val(result);
                    onCustomButtonClick('save');
                }
            });

            $('#btnGenerate').click(function () {
                $('#<%=btnExport.ClientID%>').click();
            });

            registerViewListClickHandler();
        });

        function registerViewListClickHandler() {
            $('#ulMeetingViewList li').click(function (e) {
                if (!$(this).hasClass('selected')) {
                    var id = $(this).find('.hdnClassSubjectTaskID').val();
                    $('#<%=hdnClassSubjectTaskID.ClientID %>').val(id);
                    $('#<%=hdnGCSubjectMarkType.ClientID %>').val($(this).find('.hdnGCMarkType').val());
                    $('#<%=hdnMarkTypeID.ClientID %>').val($(this).find('.hdnMarkTypeID').val());
                    $('#<%=hdnMinValue.ClientID %>').val($(this).find('.hdnMinValue').val());
                    $('#<%=hdnMaxValue.ClientID %>').val($(this).find('.hdnMaxValue').val());
                    $('#ulMeetingViewList li.selected').removeClass('selected');
                    $(this).addClass('selected');
                    cbpMeetingDetail.PerformCallback();
                }
            });
        }

        function onHistoryInit() {
            if ($('#ulMeetingViewList li').length > 0)
                $('#ulMeetingViewList li:eq(0)').click();

            $('#lblAddData').click(function () {
                var url = ResolveUrl("~/Program/ClassMeeting/ClassTask/ClassTaskEntryCtl.ascx");
                openUserControlPopup(url, '', 'Entri Tugas', 900, 550);                
            });
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setStudentImage();
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            registerViewListClickHandler();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
                $('#ulMeetingViewList li:eq(0)').click();
            }
            else
                $('#ulMeetingViewList li:eq(0)').click();
        }
        //#endregion

        function onCbpMeetingDetailEndCallback(s) {
            setStudentImage();
            registerCollapseExpandHandler();
            hideLoadingPanel();
        }

        function onAfterSaveAddRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }

        function onAfterSaveEditRecordEntryPopup() {
            cbpView.PerformCallback('refresh');
        }

        $('.divEdit').live('click', function () {
            $li = $(this).parent();
            var id = $li.find('.hdnClassSubjectTaskID').val();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTask/ClassTaskEntryCtl.ascx");
            openUserControlPopup(url, id, 'Entri Tugas', 900, 550);
        });

        $('.divRemedial').live('click', function () {
            $li = $(this).parent();
            var id = $li.find('.hdnClassSubjectTaskID').val() + '|' + $('#<%=txtPassingGrade.ClientID %>').val();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTask/ClassTaskRemedialEntryCtl.ascx");
            openUserControlPopup(url, id, 'Remidi', 900, 550);
        });

        function onCboFilterTaskTypeValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        $('.txtMark').live('keydown', function (e) {
            $tr = $(this).closest('tr');
            var rowIndex = $('.trStudent').index($tr);
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 40) { //down
                if (rowIndex < $('.trStudent').length - 1) {
                    rowIndex++;
                    $('.trStudent:eq(' + rowIndex + ')').find('.txtMark').focus();
                }
            }
            else if (code == 38) { //up
                if (rowIndex > 0) {
                    rowIndex--;
                    $('.trStudent:eq(' + rowIndex + ')').find('.txtMark').focus();
                }
            }
        });
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnClassSubjectTaskID" runat="server" />
    <input type="hidden" id="hdnSchoolClassID" runat="server" />
    <input type="hidden" id="hdnMarkTypeID" runat="server" />
    <input type="hidden" id="hdnGCSubjectMarkType" runat="server" />
    <input type="hidden" id="hdnMinValue" runat="server" />
    <input type="hidden" id="hdnMaxValue" runat="server" />
    <style type="text/css">
        #ulMeetingViewList .divMeetingDate        { float: left; width: 66px; margin: 3px 10px 0 0; background-color: #6BBD46; padding: 3px 10px; font-size: 20px; color: White; vertical-align: middle; text-align: center; }
        #ulMeetingViewList li                          { padding: 5px 3px; cursor: pointer; list-style-type:none; margin-bottom: 1px; }
        #ulMeetingViewList li.selected                 { background-color: #D5D5D5; }
        #ulMeetingViewList li:hover                    { background-color: #BCBCBC; }
        #ulMeetingViewList                             { margin: 0; padding: 0; }
        #ulMeetingViewList .tdMeetingDetail       { padding-left: 5px; }
        .divDetailEdit:hover                      { text-decoration:underline; }
        h4                                                  { color: #013EDD; }
    </style>
    <div style="display:none;">
        <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
        <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
    </div>
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
            <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tipe Tugas") %></td>
            <td>
                <dxe:ASPxComboBox ID="cboFilterTaskType" ClientInstanceName="cboFilterTaskType" runat="server" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboFilterTaskTypeValueChanged(); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <table style="width:100%">
        <colgroup>
            <col style="width:500px"/>
        </colgroup>
        <tr>
            <td valign="top">
                <label class="lblLink" id="lblAddData"><%=GetLabel("Tambah Data")%></label>
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:Repeater ID="rptMeetingView" runat="server">
                                <HeaderTemplate>
                                    <ul id="ulMeetingViewList">                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit divEdit"><%=GetLabel("Edit")%></div>
                                        <div style='float:right;margin-right:10px;color:Red;' class="divDetailEdit divRemedial"><%=GetLabel("Remidi")%></div>
                                        <input type="hidden" value='<%# Eval("ClassSubjectTaskID") %>' class="hdnClassSubjectTaskID" />
                                        <input type="hidden" value='<%# Eval("GCMarkType") %>' class="hdnGCMarkType" />
                                        <input type="hidden" value='<%# Eval("MarkTypeID") %>' class="hdnMarkTypeID" />
                                        <input type="hidden" value='<%# Eval("MinValue") %>' class="hdnMinValue" />
                                        <input type="hidden" value='<%# Eval("MaxValue") %>' class="hdnMaxValue" />
                                        <div class="divMeetingDate"><%# Eval("TaskDate", "{0:dd MMM}")%><br /><%# Eval("TaskDate", "{0:yyyy}")%></div>
                                        <div style="font-size: 24px; font-weight: 100;"><%#Eval("Topic") %> (<%#Eval("ClassTaskCode")%>)</div>
                                        <div style="font-size: 12px;"><%#Eval("CurriculumMarkTypeDtName") %> (<%#Eval("CurriculumMarkTypeName")%>)<br /><%#Eval("StartTime") %> - <%#Eval("EndTime") %></div>
                                    </li>                        
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>  
                <div class="containerPaging">
                    <div class="divInformationNumEntries" id="informationNumEntries"></div>
                    <div class="wrapperPaging">
                        <div id="paging"></div>
                    </div>
                </div> 
            </td>
            <td valign="top">
                <h4><%=GetLabel("Nilai")%></h4>    
                <input type="button" id="btnGenerate" style="margin:5px 0;" value='<%=GetLabel("Export") %>' />
                <dxcp:ASPxCallbackPanel ID="cbpMeetingDetail" runat="server" Width="100%" ClientInstanceName="cbpMeetingDetail"
                    ShowLoadingPanel="false" OnCallback="cbpMeetingDetail_Callback">
                    <ClientSideEvents Init="function(s,e){ onHistoryInit(); }" BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e) { onCbpMeetingDetailEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="height: 315px; overflow-y: scroll; overflow-x: hidden; font-size: 12px;">
                                <div class="containerTblEntryContent">
                                    <fieldset id="fsMark">
                                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                                            <tr>
                                                <th><%=GetLabel("Siswa") %></th>
                                                <th class="thCenter" style="width:85px"><%=GetLabel("Nilai") %></th>
                                            </tr>
                                            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="trStudent">
                                                        <td class="keyField"><%#Eval("StudentID") %></td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 35px;">
                                                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                                        <div class="gridCircle divStudentImage"></div>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("StudentName") %>
                                                                        <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="center">
                                                            <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                            <div id="divMark" runat="server">
                                                                <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtMark min max" Text="" Width="60px" />
                                                                &nbsp;<b id="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                                            </div>
                                                            <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="100px" runat="server" />
                                                            <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkDescription" Text="" Width="390px" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
                <b style="color:Red;">R*</b> : Remidi
            </td>
        </tr>
    </table>
</asp:Content>