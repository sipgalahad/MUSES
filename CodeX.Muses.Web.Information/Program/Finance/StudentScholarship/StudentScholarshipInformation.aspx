<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPBaseContent.master"
    AutoEventWireup="true" CodeBehind="StudentScholarshipInformation.aspx.cs" Inherits="CodeX.Muses.Web.Information.Program.StudentScholarshipInformation" %>

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

        function onCboCustomerValueChanged(s) {
            cbpView.PerformCallback('refresh');
        } 

        $(function () {
            onCbpViewEndCallback();
        });

        function onCbpViewEndCallback() {
            $tempDiv = $('<div></div>');
            $tempDiv.html($('#divContainerView').html());
            $tempDiv.find('.tblView').attr('border', '1');
            $('#<%=hdnExportControl.ClientID %>').val($tempDiv.html());
            hideLoadingPanel();
        }
    </script>
    <style type="text/css">
        .divRemarks             { height: 30px; }
    </style>
    <input type="hidden" id="hdnExportControl" runat="server" />
    <table>
        <colgroup>
            <col style="width: 180px" />
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
    <div class="divTransactionEntry">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <div id="divContainerView">
                            <table cellpadding="0" cellspacing="0" class="grdSelected grdBorder tblView">
                                <tr>
                                    <th style="width:220px" rowspan="3"><%=GetLabel("Siswa")%></th>       
                                    <th style="width:120px" rowspan="3" class="thCenter"><%=GetLabel("Tgl Mulai")%></th>
                                    <th id="thFeeComp" runat="server" class="thCenter"><%=GetLabel("Komponen") %></th> 
                                    <th rowspan="3"><%=GetLabel("Keterangan")%></th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptStudentFeeCompTypeView" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter" colspan="2"><%#Eval("StudentFeeCompTypeName")%></th>
                                        </ItemTemplate>
                                    </asp:Repeater>       
                                </tr>
                                <tr> 
                                    <asp:Repeater ID="rptStudentFeeCompTypeView2" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter" style="width:80px"><%=GetLabel("Diskon") %></th>
                                            <th class="thCenter" style="width:70px"><%=GetLabel("Frek Bayar") %></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("StudentName") %></td>
                                            <td align="center"><%#Eval("StartingDate", "{0:dd-MMM-yyyy}") %></td>
                                            <asp:Repeater ID="rptViewDt" runat="server">
                                                <ItemTemplate>
                                                    <td class="thRight"><div><%#Eval("cfDiscountAmount") %></div></td>
                                                    <td class="thRight"><%#Eval("NoOfPeriod") %></td>
                                                </ItemTemplate>
                                            </asp:Repeater>  
                                            <td><%#Eval("Remarks") %></td>   
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
