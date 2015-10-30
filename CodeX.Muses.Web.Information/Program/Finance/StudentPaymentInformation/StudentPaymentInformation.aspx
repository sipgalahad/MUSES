<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master" AutoEventWireup="true" 
    CodeBehind="StudentPaymentInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentPaymentInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtTransactionDate.ClientID %>');

            $('#btnRefresh').click(function () {
                onRefreshGridView();
            });
        })

        //#region Paging
        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }
        //#endregion

        function onRefreshGridView() {
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
                setTimeout(function () {
                    s.SetFocus();
                }, 0);
            }, 0);
        }

        function onCboSiteValueChanged() {
            $('#<%=hdnSiteID.ClientID %>').val(cboSite.GetValue());
            $('#<%=hdnSiteName.ClientID %>').val(cboSite.GetText());
        }

    </script>
    <input type="hidden" value="" id="hdnSiteID" runat="server" />
    <input type="hidden" value="" id="hdnSiteName" runat="server" />
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <table style="width:50%">
                        <colgroup>
                            <col style="width:150px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><%=GetLabel("Tanggal") %></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 1px; width: 145px"><asp:TextBox ID="txtTransactionDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px; vertical-align: top">
                    <div style="position: relative;">
                        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <input type="hidden" value="" id="hdnMovementDate" runat="server" />
                                    <asp:Panel runat="server" ID="pnlGridView" CssClass="pnlContainerGrid" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;height:380px;overflow-y:auto;">
                                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" border="1" rules="all" class="grdSelected grdBorder">
                                                    <colgroup>
                                                        <col style="width:120px"/>
                                                        <col/>
                                                        <col style="width:150px"/>
                                                        <col style="width:120px"/>
                                                        <col style="width:120px"/>
                                                        <col style="width:120px"/>
                                                        <col style="width:120px"/>
                                                        <col style="width:120px"/>
                                                    </colgroup>
                                                    <tr>
                                                        <th rowspan="2" class="thCenter"><%=GetLabel("NBS") %></th>
                                                        <th rowspan="2" class="thCenter"><%=GetLabel("Nama") %></th>
                                                        <th rowspan="2" class="thCenter"><%=GetLabel("Kelas") %></th>
                                                        <th colspan="4" class="thCenter"><%=GetLabel("Jenis Pembayaran") %></th>
                                                        <th rowspan="2" class="thCenter"><%=GetLabel("Total") %></th>
                                                    </tr>
                                                    <tr>
                                                        <th class="thCenter"><%=GetLabel("Uang Sekolah") %></th>
                                                        <th class="thCenter"><%=GetLabel("Uang Kegiatan") %></th>
                                                        <th class="thCenter"><%=GetLabel("Uang Pembangunan") %></th>
                                                        <th class="thCenter"><%=GetLabel("Denda") %></th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("cfStudentCode") %></td>
                                                    <td><%#Eval("cfStudentName") %></td>
                                                    <td><%#Eval("cfSchoolClassCode") %></td>
                                                    <td align="right"><div id="divUsek" runat="server"></div></td>
                                                    <td align="right"><div id="divKeg" runat="server"></div></td>
                                                    <td align="right"><div id="divPemb" runat="server"></div></td>
                                                    <td align="right"><div id="divDenda" runat="server"></div></td>
                                                    <td align="right"><div id="divTotal" runat="server"></div></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            <tr>
                                                <td colspan="3" style="font-weight:bold;" align="right"><%=GetLabel("Total") %></td>
                                                <td align="right"><div id="divTotalUsek" runat="server"></div></td>
                                                <td align="right"><div id="divTotalKeg" runat="server"></div></td>
                                                <td align="right"><div id="divTotalPemb" runat="server"></div></td>
                                                <td align="right"><div id="divTotalDenda" runat="server"></div></td>
                                                <td align="right"><div id="divTotalAll" runat="server"></div></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
