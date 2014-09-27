<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPPatientPageListEntry.master" AutoEventWireup="true" 
    CodeBehind="CurrentMedicationList.aspx.cs" Inherits="QIS.Medinfras.Web.EMR.Program.CurrentMedicationList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhScript" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=grdView.ClientID %> tr:gt(0):not(.trEmpty)').live('click', function () {
                $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnID.ClientID %>').val($(this).find('.keyField').html());
            });
            $('#<%=grdView.ClientID %> tr:eq(1)').click();
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });
        });

        function onCbpViewEndCallback(s) {
            $('#containerImgLoadingView').hide();

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
        }
        //#endregion

        function onSelectedRowChanged(value) {
            var idx = $('#<%=grdView.ClientID %> tr').index($('#<%=grdView.ClientID %> tr.selected'));
            idx += value;
            if (idx < 1)
                idx = 1;
            if (idx == $('#<%=grdView.ClientID %> tr').length)
                idx--;
            $('#<%=grdView.ClientID %> tr:eq(' + idx + ')').click();
        }

        function getSelectedRow() {
            return $('#<%=grdView.ClientID %> tr.selected');
        }

        //#region Entity To Control
        function entityToControl(entity) {
            $('#<%=ddlAllergenType.ClientID %>').focus();
            if (entity != null) {
                var year = entity.KnownDate.substring(0, 4);
                var month = entity.KnownDate.substring(4, 6);
                var date = entity.KnownDate.substring(6);

                $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
                $('#<%=ddlAllergenType.ClientID %>').val(entity.GCAllergenType);
                $('#<%=ddlFindingSource.ClientID %>').val(entity.GCAllergenSource);
                $('#<%=ddlSeverity.ClientID %>').val(entity.GCAllergySeverity);
                $('#<%=txtReaction.ClientID %>').val(entity.Reaction);
                $('#<%=txtAllergenName.ClientID %>').val(entity.Allergen);

                $('#<%=ddlYear.ClientID %>').val(year);
                $('#<%=ddlMonth.ClientID %>').val(parseInt(month));
                $('#<%=ddlDate.ClientID %>').val(parseInt(date));
            }
            else {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=ddlAllergenType.ClientID %>').val('');
                $('#<%=ddlFindingSource.ClientID %>').val('');
                $('#<%=ddlSeverity.ClientID %>').val('');
                $('#<%=txtReaction.ClientID %>').val('');
                $('#<%=txtAllergenName.ClientID %>').val('');

                $('#<%=ddlYear.ClientID %>').val('');
                $('#<%=ddlMonth.ClientID %>').val('');
                $('#<%=ddlDate.ClientID %>').val('');
            }
        }
        //#endregion
    </script>
</asp:Content>

<asp:Content ID="ctnEntry" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" value="" id="hdnEntryID" runat="server" />
    <table style="width:100%" class="tblEntryDetail">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td>
                <table style="width:100%">
                    <colgroup>
                        <col style="width:100px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Allergen Type")%></label></td>
                        <td><asp:DropDownList runat="server" ID="ddlAllergenType" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Allergen Name")%></label></td>
                        <td><asp:TextBox ID="txtAllergenName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Finding Source")%></label></td>
                        <td><asp:DropDownList runat="server" ID="ddlFindingSource" Width="300px" /></td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width:100%">
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Known Date")%></label></td>
                        <td>
                            <table>
                                <colgroup>
                                    <col style="width:80px"/>
                                    <col style="width:5px"/>
                                    <col style="width:100px"/>
                                    <col style="width:5px"/>
                                    <col style="width:80px"/>
                                </colgroup>
                                <tr>
                                    <td><asp:DropDownList runat="server" ID="ddlYear" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:DropDownList runat="server" ID="ddlMonth" Width="100%" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:DropDownList runat="server" ID="ddlDate" Width="100%" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Severity")%></label></td>
                        <td><asp:DropDownList runat="server" ID="ddlSeverity" Width="100px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Reaction")%></label></td>
                        <td><asp:TextBox ID="txtReaction" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="ctnList" ContentPlaceHolderID="plhList" runat="server">
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingView').show(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:300px">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdPatientPage" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-CssClass="hiddenColumn" ItemStyle-CssClass="hiddenColumn" >
                                    <ItemTemplate>
                                        <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                        <input type="hidden" value="<%#Eval("Allergen") %>" bindingfield="Allergen" />
                                        <input type="hidden" value="<%#Eval("GCAllergenType") %>" bindingfield="GCAllergenType" />
                                        <input type="hidden" value="<%#Eval("GCAllergySource") %>" bindingfield="GCAllergySource" />
                                        <input type="hidden" value="<%#Eval("GCAllergySeverity") %>" bindingfield="GCAllergySeverity" />
                                        <input type="hidden" value="<%#Eval("KnownDate") %>" bindingfield="KnownDate" />
                                        <input type="hidden" value="<%#Eval("Reaction") %>" bindingfield="Reaction" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Allergen" HeaderText="Allergen Name" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="AllergySource" HeaderText="Finding Source" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="DisplayDate" HeaderText="Known Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="AllergySeverity" HeaderText="Severity" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="Reaction" HeaderText="Reaction" />
                            </Columns>
                            <EmptyDataTemplate>
                                No Data To Display
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
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>
