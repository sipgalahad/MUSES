<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="CreateTariffEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.CreateTariffEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtEffectiveDate.ClientID %>');
            setDatePicker('<%=txtDocumentDate.ClientID %>');

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });

            var pageCount = parseInt($('#<%=hdnPageCount.ClientID %>').val());
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });

            //#region Item Group
            $('#lblItemGroup.lblLink').click(function () {
                var filterExpression = "IsDeleted = 0";
                openSearchDialog('itemgroup', filterExpression, function (value) {
                    $('#<%=txtItemGroupCode.ClientID %>').val(value);
                    onTxtItemGroupCodeChanged(value);
                });
            });

            $('#<%=txtItemGroupCode.ClientID %>').change(function () {
                onTxtItemGroupCodeChanged($(this).val());
            });

            function onTxtItemGroupCodeChanged(value) {
                var filterExpression = "ItemGroupCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetItemGroupList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnItemGroupID.ClientID %>').val(result.ItemGroupID);
                        $('#<%=txtItemGroupName.ClientID %>').val(result.ItemGroupName);
                    }
                    else {
                        $('#<%=hdnItemGroupID.ClientID %>').val('');
                        $('#<%=txtItemGroupCode.ClientID %>').val('');
                        $('#<%=txtItemGroupName.ClientID %>').val('');
                    }
                    cbpView.PerformCallback('refresh');
                });
            }
            //#endregion

            if (!getIsAdd()) {
                $('#trItemGroup').removeAttr('style');
                $('#trQuickFilter').removeAttr('style');
            }
            else {
                $('#trItemGroup').attr('style','display:none');
                $('#trQuickFilter').attr('style','display:none');
            }
        }

        $('.txtNewTariff').live('change', function () {
            $tr = $(this).closest('tr');
            $tr.find('.btnSave').removeAttr('enabled');
        });

        function onRefreshGrid() {
            $('#<%=hdnFilterExpressionQuickSearch.ClientID %>').val(txtSearchView.GenerateFilterExpression());
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGrid();
            }, 0);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var result = s.cpResult.split('|');
            if (result[1] == 'success') {
                $tr = $btnSave.closest('tr');
                $btnSave.attr('enabled', 'false');
            }
            else {
                if (result[2] != '')
                    showToast('Save Failed', 'Error Message : ' + result[2]);
                else
                    showToast('Save Failed', '');
            }
        }

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

            $('.txtCurrency').each(function () {
                $(this).trigger('changeValue');
            });
        }
        //#endregion

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                $tr = $(this).closest('tr');
                var itemID = $tr.find('.keyField').html();
                var newTariff = $tr.find('.txtNewTariff').val();

                var param = 'save|' + itemID + '|' + newTariff;
                $btnSave = $(this);
                cbpProcess.PerformCallback(param);
            }
        });
    </script>
    <input type="hidden" id="hdnFilterExpressionQuickSearch" runat="server" value="" />
    <input type="hidden" id="hdnBookID" runat="server" value="" />
    <input type="hidden" id="hdnVATPercentage" runat="server" value="" />
    <input type="hidden" value="" id="hdnPageCount" runat="server" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width: 50%" />
        </colgroup>
        <tr>
            <td valign="top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Site")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboSite" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Document No")%></label></td>
                        <td><asp:TextBox ID="txtDocumentNo" Width="150px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Document Date")%></label></td>
                        <td><asp:TextBox ID="txtDocumentDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Effective Date")%></label></td>
                        <td><asp:TextBox ID="txtEffectiveDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tariff Scheme")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboTariffScheme" Width="150px" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Item Type")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboItemType" Width="150px" runat="server"  /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Revision No")%></label></td>
                        <td><asp:TextBox ID="txtRevisionNo" Width="100px" CssClass="number" runat="server" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:CheckBox ID="chkPPN" Width="100%" runat="server" Text="PPN" /></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trItemGroup">
                        <td class="tdLabel"><label class="lblLink" id="lblItemGroup"><%=GetLabel("Kelompok Item")%></label></td>
                        <td>
                            <input type="hidden" id="hdnItemGroupID" value="" runat="server" />
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:100px"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtItemGroupCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtItemGroupName" ReadOnly="true" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trQuickFilter">
                        <td class="tdLabel"><label><%=GetLabel("Quick Filter")%></label></td>
                        <td>
                            <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView" Width="300px" Watermark="Search">
                                <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
                                <IntellisenseHints>
                                    <qis:QISIntellisenseHint Text="Nama Item" FieldName="ItemName1" />
                                    <qis:QISIntellisenseHint Text="Kode Item" FieldName="ItemCode" />
                                </IntellisenseHints>
                            </qis:QISIntellisenseTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlGridView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:scroll;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                    OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ItemID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />                                            
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="ItemName1" HeaderText="Nama Item" />
                                        <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thRight">
                                            <HeaderTemplate>
                                                <%=GetLabel("Tarif Saat Ini") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="divCurrentTariff" runat="server" style="text-align:right"></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thRight">
                                            <HeaderTemplate>
                                                <%=GetLabel("Tarif Saat Ini + PPN") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="divCurrentTariffAfterVAT" runat="server" style="text-align:right"></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10px" />
                                        <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                            <HeaderTemplate>
                                                <%=GetLabel("Tarif Baru") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input type="text" runat="server" id="txtNewTariff" class="txtNewTariff txtCurrency" style="width:100%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                            <HeaderTemplate>
                                                <%=GetLabel("Simpan")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input type="button" id="btnSave" class="btnSave" enabled="false" value="Simpan" runat="server" />
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
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="paging"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>
    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>