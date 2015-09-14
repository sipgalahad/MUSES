<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="BankUploadedFile.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.BankUploadedFile" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#btnUploadFile').click(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=FileUpload1.ClientID %>').change(function () {
                readURL(this);
            });
        })

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=hdnUploadedFile1.ClientID %>').val(e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function onCbpPopupProcess() {
            hideLoadingPanel();
            //cbpView.PerformCallback('refresh');
            pcRightPanelContent.Hide();
        }

        //#region Bank
        function onGetBankFilterExpression() {
            var filterExpression = "SiteID = '<%=GetSiteID() %>'";
            Methods.getObject('GetvSiteList', filterExpression, function (result) {
                var display = result.DisplayPath.split('/');
                var temp = "";
                for (var i = 1; i < display.length - 1; i++) {
                    if (temp != "") temp += ",";
                    temp += "'" + display[i] + "'";
                }
                filterExpression = "SiteID IN (" + temp + ")";
            })
            return filterExpression;
        }

        function onTacBankButtonSearchClick() {
            openSearchDialog('bank', onGetBankFilterExpression(), function (value) {
                var filterExpression = onGetBankFilterExpression() + " AND BankCode = '" + value + "'";
                Methods.getObject('GetvBankList', filterExpression, function (result) {
                    if (result != null) {
                        tacBank.setValue(result.BankID);
                        tacBank.setText(result.BankName);
                    }
                    else {
                        tacBank.setValue('');
                        tacBank.setText('');
                    }
                    onTacBankValueChanged();
                });
            });

        }

        function onTacBankValueChanged() {
            //cbpView.PerformCallback('refresh');
        }
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

            if (s.cpErrorMessage != '')
                showToast('Error', s.cpErrorMessage);
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

        $('.lblStudent').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var temp = id.split('|');
            if (temp[0] != '0') {
                var url = ResolveUrl('~/Program/ARInvoice/ARInvoiceStudent/StudentPageLauncher.aspx?id=' + temp[0]);
                openWindowPopup(url, 'Student', '1300', '650');
            }
            else {
                var url = ResolveUrl('~/Program/ARInvoice/ARInvoiceProspectiveStudent/ProspectiveStudentPageLauncher.aspx?id=' + temp[1]);
                openWindowPopup(url, 'ProspectiveStudent', '1300', '650');
            }
        });

    </script>
    <style type="text/css">
        .grdSelected .highlighted td       { background-color: #F54F49; }
    </style>
    <div>
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bank")%></label></td>
                <td>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacBank" ClientInstanceName="tacBank" MethodName="GetvBankList" GetFilterExpressionFunction="onGetBankFilterExpression"
                        SearchFields="BankName,BankCode" TextField="BankName" ValueField="BankID" SearchText="${BankName} (<b>${BankCode}</b>)" OrderByExpression="BankName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacBankButtonSearchClick(); }"
                            ValueChanged="function(){ onTacBankValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="hidden" id="hdnFileName" runat="server" value="" />
                    <input type="hidden" id="hdnUploadedFile1" runat="server" value="" />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <input type="button" id="btnUploadFile" value="Upload" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <div style="position: relative;">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="overflow-y: scroll;">
                                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="cfStudentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="NBS" HeaderText="No. Bank" HeaderStyle-Width="120px" />
                                                <asp:TemplateField HeaderText="Calon Siswa / Siswa">
                                                    <ItemTemplate>
                                                        <label class="lblLink lblStudent"><%#Eval("StudentName") %></label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PaymentDate" HeaderText="Tanggal" HeaderStyle-Width="120px" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="120px"  />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <%=GetLabel("No Data To Display")%>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>  
                        <div class="containerPaging">
                            <div class="divInformationNumEntries" id="informationNumEntries"></div>
                            <div class="wrapperPaging">
                                <div id="paging"></div>
                            </div>
                        </div> 
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
