<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentStatisticInfo.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentStatisticInfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPMain" runat="server">
    <script type="text/javascript">
        function onCboSchoolPeriodValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

        $(function () {
            onCbpViewEndCallback();
        });

        function onCbpViewEndCallback() {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.divClassTypeInformation').each(function () {
                $(this).attr('border', '1');
            });
            $tempDiv.find('.divClassTypeMutationInformation').each(function () {
                $(this).attr('border', '1');
            });
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            $('#<%=hdnExportTitle.ClientID %>').val($('.hdnTempExportTitle').val());
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnExportControl" runat="server" />
    <input type="hidden" id="hdnExportTitle" runat="server" />
    <table>
        <colgroup>
            <col style="width: 120px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry" style="width:1250px; height: 450px; overflow: scroll;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <input type="hidden" id="hdnTempExportTitle" class="hdnTempExportTitle" runat="server" />
                        <div id="divContainerView">
                            <asp:Repeater ID="rptPeriod" runat="server" OnItemDataBound="rptPeriod_ItemDataBound">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" style="margin-bottom: 10px; width: 1700px;">
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" rules="all" class="grdSelected grdBorder divClassTypeInformation">
                                                <tr>
                                                    <th rowspan="3" style="width: 30px" class="thCenter"><%=GetLabel("NO") %></th>
                                                    <th rowspan="3" style="width: 80px"><%=GetLabel("KELAS") %></th>
                                                    <th id="thPeriodName" runat="server" class="thCenter"></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 60px" class="thCenter" rowspan="2"><%=GetLabel("JML KLS") %></th>
                                                    <th class="thCenter" colspan="3"><%=GetLabel("JML SISWA") %></th>
                                                    <th class="thCenter" id="thReligion" runat="server"><%=GetLabel("AGAMA") %></th>
                                                </tr>
                                                <tr>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>

                                                    <asp:Repeater ID="rptReligion" runat="server">
                                                        <ItemTemplate>
                                                            <th class="thCenter" style="width: 40px"><%#Eval("TagProperty") %></th>    
                                                        </ItemTemplate>
                                                    </asp:Repeater>                                               
                                                </tr>
                                                <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%#Eval("CurriculumClassTypeName") %></td>
                                                            <td align="center"><%#Eval("NoOfClass") %></td>
                                                            <td id="tdStudentCount" runat="server" align="center"></td>
                                                            <td id="tdStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdStudentFemaleCount" runat="server" align="center"></td>
                                                            <asp:Repeater ID="rptStudentReligion" runat="server" OnItemDataBound="rptStudentReligion_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td id="tdReligion" runat="server" align="center"></td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>               
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="2"><b><%=GetLabel("JUMLAH") %></b></td>
                                                    <td align="center"><b id="bClassCount" runat="server"></b></td>
                                                    <td align="center"><b id="bTotalStudentCount" runat="server"></b></td>
                                                    <td align="center"><b id="bTotalMaleCount" runat="server"></b></td>
                                                    <td align="center"><b id="bTotalFemaleCount" runat="server"></b></td>

                                                    <asp:Repeater ID="rptReligionTotal" runat="server" OnItemDataBound="rptReligionTotal_ItemDataBound">
                                                        <ItemTemplate>
                                                            <td align="center"><b id="bTotalReligionCount" runat="server"></b></td>    
                                                        </ItemTemplate>
                                                    </asp:Repeater>                                               
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width:20px"></td>
                                        <td valign="top">
                                            <div style="height:20px; padding-top: 15px;"><%=GetLabel("CATATAN MUTASI SISWA :")%></div>
                                            <table cellpadding="0" cellspacing="0" rules="all" class="grdSelected grdBorder divClassTypeMutationInformation">
                                                <tr>
                                                    <th rowspan="2" style="width: 30px" class="thCenter"><%=GetLabel("NO") %></th>
                                                    <th rowspan="2" style="width: 80px"><%=GetLabel("KELAS") %></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JML AWAL BLN")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("MUTASI MASUK")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("MUTASI KELUAR")%></th>
                                                    <th colspan="3" class="thCenter"><%=GetLabel("JML AKHIR BLN")%></th>
                                                    <th rowspan="2" style="width: 200px"><%=GetLabel("ALASAN MUTASI") %></th>
                                                </tr>
                                                <tr>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>

                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>

                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>

                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("JML") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("L") %></th>
                                                    <th class="thCenter" style="width: 40px"><%=GetLabel("P") %></th>
                                                </tr>
                                                <asp:Repeater ID="rptClassType2" runat="server" OnItemDataBound="rptClassType2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="center"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%#Eval("CurriculumClassTypeName") %></td>
                                                            <td id="tdBeginStudentCount" runat="server" align="center"></td>
                                                            <td id="tdBeginStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdBeginStudentFemaleCount" runat="server" align="center"></td>

                                                            <td id="tdInStudentCount" runat="server" align="center"></td>
                                                            <td id="tdInStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdInStudentFemaleCount" runat="server" align="center"></td>

                                                            <td id="tdOutStudentCount" runat="server" align="center"></td>
                                                            <td id="tdOutStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdOutStudentFemaleCount" runat="server" align="center"></td>

                                                            <td id="tdEndStudentCount" runat="server" align="center"></td>
                                                            <td id="tdEndStudentMaleCount" runat="server" align="center"></td>
                                                            <td id="tdEndStudentFemaleCount" runat="server" align="center"></td>
                                                            
                                                            <td id="tdStudentMoveOutReason" runat="server"></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </table>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
