<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolClassPageTrx.master" AutoEventWireup="true" EnableEventValidation="false"  
    CodeBehind="SchoolClassMarkInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassMarkInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setStudentImage();
        });

        function onCboSubjectValueChanged() {
            $('#<%=hdnSubject.ClientID %>').val(cboSubject.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCbpViewEndCallback() {
            setStudentImage();
            hideLoadingPanel();
        }
    </script>
    <table cellspacing="0">
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td>    
                <input type="hidden" id="hdnSubject" runat="server" />
                <dxe:ASPxComboBox runat="server" ID="cboSubject" ClientInstanceName="cboSubject" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboSubjectValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView">
                    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                        <tr>
                            <th rowspan="2"><%=GetLabel("Siswa") %></th>
                            <th id="thMark" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                        </tr>
                        <tr>
                            <asp:Repeater ID="rptHeader" runat="server">
                                <ItemTemplate>
                                    <th class="thCenter" style="width:90px">
                                        <%#Eval("cfClassTaskCode")%>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td class="keyField"><%#Eval("StudentID") %></td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 35px;">
                                                    <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                    <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                    <div class="gridCircle divStudentImage"></div>
                                                </td>
                                                <td>
                                                    <%#Eval("StudentName") %>
                                                    <input type="hidden" id="Hidden1" class="hdnAttendance" runat="server" value="" />
                                                </td>
                                            </tr>
                                        </table>
                                        <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                                    </td>
                                    <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="center">
                                                <div id="divStudentMark" runat="server"></div>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>  

    
    <div style="width:1250px; overflow-x: auto; display: none">
        <dxcp:ASPxCallbackPanel ID="cbpPrint" runat="server" Width="100%" ClientInstanceName="cbpPrint"
            ShowLoadingPanel="false">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent2" runat="server">
                    <asp:Panel runat="server" ID="pnlPrint">
                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent" border="1">
                            <tr>
                                <th rowspan="2" style="width:80px"><%=GetLabel("NIS") %></th>
                                <th rowspan="2"><%=GetLabel("Siswa") %></th>
                                <th id="thMark2" runat="server" class="thCenter">NILAI</th>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptHeader2" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width:90px">
                                            <%#Eval("cfClassTaskCode")%>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <asp:Repeater ID="rptStudent2" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("StudentCode") %></td>
                                        <td><%#Eval("StudentName") %></td>
                                        <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                                            <ItemTemplate>
                                                <td align="center">
                                                    <div id="divStudentMark" runat="server"></div>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>  
    </div>
</asp:Content>