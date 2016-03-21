<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPRProjectPageTrx.master" AutoEventWireup="true" 
    CodeBehind="RProjectEvaluationEntry.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.RProjectEvaluationEntry" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEvaluationSave" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtLogDate.ClientID %>');

            $('#<%=btnEvaluationSave.ClientID %>').click(function () {
                onCustomButtonClick('save');
            });

            $('#divTransactionAddPopup').click(function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                var currentDate = new Date();
                var h = currentDate.getHours();
                var mnt = currentDate.getMinutes();
                var d = currentDate.getDate();
                var m = currentDate.getMonth() + 1;
                var y = currentDate.getFullYear();
                $('#<%=txtLogDate.ClientID %>').val('' + (d <= 9 ? '0' + d : d) + '-' + (m <= 9 ? '0' + m : m) + '-' + y);
                $('#<%=txtLogTime.ClientID %>').val('' + (h <= 9 ? '0' + h : h) + ':' + (mnt <= 9 ? '0' + mnt : mnt));
                $('#<%=txtLogText.ClientID %>').val('');
                $('#entryDetailContainerPopup').show();
            });

            $('#btnCancelPopup').click(function () {
                $('#entryDetailContainerPopup').hide();
            });

            $('#btnSavePopup').click(function (evt) {
                if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                    cbpProcessPopup.PerformCallback('save');
                }
            });
        })

        $('#<%=grdView.ClientID %> .divDetailDelete').die('click');
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectLogID);
                    cbpProcessPopup.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').die('click');
        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ProjectLogID);
            $('#<%=txtLogDate.ClientID %>').val(entity.LogDate);
            $('#<%=txtLogTime.ClientID %>').val(entity.LogTime);
            $('#<%=txtLogText.ClientID %>').val(entity.LogText);
            $('#entryDetailContainerPopup').show();
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
                setProjectTaskLogText();
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAddPopup').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="divTransactionEntry">   
        <table width="50%">
            <tr>
                <td><label class="lblNormal" style="font-weight:bold;"><%=GetLabel("Indikator Kinerja")%></label></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtProjectIndicator" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
            </tr>
            <tr>
                <td><label class="lblNormal" style="font-weight:bold;"><%=GetLabel("Target / Sasaran")%></label></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtProjectTarget" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
            </tr>
            <tr>
                <td><label class="lblNormal" style="font-weight:bold;"><%=GetLabel("Pencapaian Target Indikator Kinerja")%></label></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtProjectAchievment" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
            </tr>
        </table>
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table id="tblEntryPopup">
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:TextBox ID="txtLogDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                    <td style="width:10px; text-align:center">&nbsp;</td>
                                    <td><asp:TextBox ID="txtLogTime" CssClass="thCenter" Width="70px" runat="server"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtLogText" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr id="trSaveEntryPopup">
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
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
                            <asp:BoundField DataField="LogDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="LogTime" HeaderText="Waktu" HeaderStyle-Width="70px" />
                            <asp:BoundField DataField="LogText" HeaderText="Keterangan" />
                            <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div class="divDetailDelete" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;'" %>></div>
                                    <div class="divDetailEdit" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;margin-right:10px;'" %>><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("LogDate", "{0:dd-MM-yyyy}") %>" bindingfield="LogDate" />
                                    <input type="hidden" value="<%#Eval("LogTime") %>" bindingfield="LogTime" />
                                    <input type="hidden" value="<%#Eval("LogText") %>" bindingfield="LogText" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>