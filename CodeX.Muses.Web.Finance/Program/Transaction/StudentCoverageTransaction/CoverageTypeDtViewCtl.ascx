<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoverageTypeDtViewCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Finance.Program.CoverageTypeDtViewCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">

    $(function () {
        addTableHeader();
    });

    function addTableHeader() {
        $('#tblView thead').html($('#tblView1 thead').html());
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnStudentFeeCompTypeSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>

    <table id="tblView1" rules="all" class="tblTransactionEntryResult grdBorder" style="display:none">
        <thead>
            <tr>
                <th style="width:220px" rowspan="3"><%=GetLabel("Tipe Kelas")%></th>     
                <th style="width:120px" rowspan="3"><%=GetLabel("Nama")%></th>     
                <th id="thFeeComp" runat="server" class="thCenter"><%=GetLabel("Komponen") %></th> 
                <th rowspan="3"><%=GetLabel("Keterangan")%></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptStudentFeeCompTypeView" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" colspan="3"><%#Eval("StudentFeeCompTypeName")%></th>
                    </ItemTemplate>
                </asp:Repeater>       
            </tr>
            <tr> 
                <asp:Repeater ID="rptStudentFeeCompTypeView2" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Diskon") %></th>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Tanggung") %></th>
                        <th class="thCenter" style="width:70px"><%=GetLabel("Frek Bayar") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </thead>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ addTableHeader(); hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                        <HeaderTemplate>
                            <table id="tblView" rules="all" class="tblTransactionEntryResult grdBorder">
                                <thead>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="trDt">
                                <td><%#Eval("ListClassTypeName")%></td>
                                <td><%#Eval("CoverageTypeDtName")%></td>
                                <asp:Repeater ID="rptViewDt" runat="server">
                                    <ItemTemplate>
                                        <td class="thRight tdCompDt">
                                            <input type="hidden" class="hdnDiscountAmount" value='<%#Eval("DiscountAmount")%>' />
                                            <input type="hidden" class="hdnIsDiscountInPercentage" value='<%#Eval("IsDiscountInPercentage")%>' />
                                            <input type="hidden" class="hdnCoverageAmount" value='<%#Eval("CoverageAmount")%>' />
                                            <input type="hidden" class="hdnIsCoverageInPercentage" value='<%#Eval("IsCoverageInPercentage")%>' />
                                            <input type="hidden" class="hdnNoOfPeriod" value='<%#Eval("NoOfPeriod")%>' />
                                            <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID")%>' />
                                            <div><%#Eval("cfDiscountAmount") %></div>
                                        </td>
                                        <td class="thRight tdCompDt2">
                                            <div><%#Eval("cfCoverageAmount") %></div>
                                        </td>
                                        <td class="thRight tdCompDt3">
                                            <%#Eval("NoOfPeriod") %>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>                              
                                <td><%#Eval("Remarks")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>                                
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</div>

