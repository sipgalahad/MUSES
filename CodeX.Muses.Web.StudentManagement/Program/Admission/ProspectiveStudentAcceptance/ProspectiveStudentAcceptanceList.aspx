<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentAcceptanceList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentAcceptanceList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Process")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var param = "";
                $('.chkIsAccepted input:checked').each(function () {
                    var id = $(this).closest('tr').find('.keyField').html();

                    if (param != '') {
                        param += ',';
                    }
                    param += id;
                });
                if (param == "")
                    showToast('Warning', 'Silakan Pilih Calon Siswa Terlebih Dahulu');
                else {
                    $('#<%=hdnSelectedValue.ClientID %>').val(param);
                    cbpProcess.PerformCallback('save');
                }
            });

            $('#<%=chkFilterIsPaid.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            setDatePicker('<%=txtAcceptedDate.ClientID %>');
        })

        $('.chkAcceptAll input').live('click', function () {
            var value = $(this).is(':checked');
            $('#<%=grdView.ClientID %> .chkIsAccepted input').each(function () {
                if ($(this).is(':enabled')) $(this).prop("checked", value);
            });
        });

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Process Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <input type="hidden" id="hdnSelectedValue" runat="server" />
    <input type="hidden" id="hdnEntryID" runat="server"/>
    <asp:CheckBox runat="server" ID="chkFilterIsPaid" /> <%=GetLabel("Tampilkan Hanya Yang Lunas") %><br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:120" class="tdLabel"><%=GetLabel("Tanggal Diterima") %></td>
            <td><asp:TextBox ID="txtAcceptedDate" runat="server" Width="120px" CssClass="datepicker" /></td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" OnRowDataBound="grdView_RowDataBound"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="text-align:center">
                                            <asp:CheckBox runat="server" ID="chkAcceptAll" CssClass="chkAcceptAll" />
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsAccepted" CssClass="chkIsAccepted" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RegistrationID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="ProspectiveStudentCode" HeaderText="NBS" HeaderStyle-Width="180px" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ProspectiveStudentName" HeaderText="Nama Calon Siswa"/>
                                <asp:BoundField DataField="TotalClaimedAmount" HeaderText="Total Tagihan" HeaderStyle-Width="150px" DataFormatString="{0:N}" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPaymentAmount" HeaderText="Total Bayar" DataFormatString="{0:N}" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="RemainingAmount" HeaderText="Sisa" DataFormatString="{0:N}" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Lunas">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsPaid" CssClass="chkIsPaid" Enabled="false" />
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
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>