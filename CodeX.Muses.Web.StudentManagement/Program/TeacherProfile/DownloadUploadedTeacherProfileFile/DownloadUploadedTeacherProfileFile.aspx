<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="DownloadUploadedTeacherProfileFile.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.DownloadUploadedTeacherProfileFile" %>

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
            pcRightPanelContent.Hide();
        }

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
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

    </script>
    <div>
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <table class="tblEntryContent" style="width: 50%">
            <colgroup>
                <col style="width: 30%" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Grade")%></label></td>
                <td>
                    <dxe:ASPxComboBox runat="server" ID="cboGrade" />
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
                                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                            <Columns>
                                                <asp:BoundField DataField="NIK" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="NIK" HeaderText="No. Karyawan" HeaderStyle-Width="120px" />
                                                <asp:BoundField DataField="Name" HeaderText="Nama" />
                                                <asp:BoundField DataField="Talent" HeaderText="Talenta" HeaderStyle-Width="120px" />
                                                <asp:BoundField DataField="IQ" HeaderText="IQ" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Drive" HeaderText="D" HeaderStyle-Width="120px"  />
                                                <asp:BoundField DataField="Komunikasi" HeaderText="K" HeaderStyle-Width="120px"  />
                                                <asp:BoundField DataField="Loyalitas" HeaderText="L" HeaderStyle-Width="120px"  />
                                                <asp:BoundField DataField="Teliti" HeaderText="T" HeaderStyle-Width="120px"  />
                                                <asp:BoundField DataField="Konsistensi" HeaderText="K" HeaderStyle-Width="120px"  />
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