 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPProjectManagementPageTrx.master" AutoEventWireup="true" 
    CodeBehind="UseOfBudgetList.aspx.cs" Inherits="CodeX.Muses.Web.ProjectManagement.Program.UseOfBudgetList" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <style type="text/css">
        .trActivityLog  {height:50px;}
        .divActivityLog { width:99%; background-color:#EEEEEE; border-radius:10px; padding:3px; margin-bottom:7px;}
    </style>
        
    <script type="text/javascript">
        $('.btnSave').die('click');
        $('.btnSave').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var idx = entity.ItemIndex;
            $('#<%=hdnID.ClientID %>').val(entity.BudgetID);
            var usedAmount = $('#txtUsedAmount' + idx).attr('hiddenVal');
            $('#<%=hdnUsedAmount.ClientID %>').val(usedAmount);

            var amount = $row.find('.hdnAmount').val();
            if ($('#<%=hdnUsedAmount.ClientID %>').val() > amount)
                cbpProcess.PerformCallback('save');
        });

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
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
    <input type="hidden" id="hdnUsedAmount" runat="server" value="" />
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="BudgetID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="BudgetCode" HeaderText="Kode" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="BudgetName" HeaderText="Nama" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Position" HeaderText="Bagian" HeaderStyle-Width="170px"  HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Remarks" HeaderText="Keterangan" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ProposedAmount" HeaderText="Dianggarkan" HeaderStyle-Width="70px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                <asp:BoundField DataField="RealizationAmount" HeaderText="Direalisasikan" HeaderStyle-Width="70px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                <asp:TemplateField HeaderText="Digunakan" HeaderStyle-Width="70px" HeaderStyle-CssClass="thRight">
                                    <ItemTemplate>
                                        <input type="hidden" class="hdnAmount" id="hdnAmount" runat="server" value="0" />
                                        <input type="text" class="txtCurrency" id="txtUsedAmount<%# Container.DataItemIndex %>" style="width:100%" value="<%#:Eval("UsedAmount") %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <input type="button" value="Simpan" class="btnSave" id="btnSave" runat="server" />
                                        <input type="hidden" class="hdnItemIndex" value='<%# Container.DataItemIndex %>' bindingfield="ItemIndex" />
                                        <input type="hidden" value="<%#Eval("BudgetID") %>" bindingfield="BudgetID" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("Data Tidak Tersedia")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>