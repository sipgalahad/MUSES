<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolClassPageTrx.master" AutoEventWireup="true" 
    CodeBehind="SchoolClassStudentNoteInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassStudentNoteInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setStudentImage();
        });

        $('.lblNoteRate.lblLink').live('click', function () {
            var category = cboNoteCategory.GetValue();
            if (category == null)
                category = '';
            var id = $(this).closest('tr').find('.hdnStudentID').val() + '|' + category + '|' + $(this).parent().find('.hdnNoteRate').val();
            var url = ResolveUrl("~/Program/SchoolClass/SchoolClassStudentNoteInformation/SchoolClassStudentNoteViewDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Catatan', 800, 550);
        });

        function onCboNoteCategoryValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        function onCbpViewEndCallback() {
            hideLoadingPanel();
            setStudentImage();
        }
    </script>
    <input type="hidden" id="hdnSchoolClassID" runat="server" />
    <table cellspacing="0">
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kategori")%></label></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboNoteCategory" ClientInstanceName="cboNoteCategory" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboNoteCategoryValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div style="width:1250px; overflow-x: auto;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent" id="tblView">
                            <tr>
                                <th><%=GetLabel("Siswa") %></th>
                                <asp:Repeater ID="rptNoteRateHeader" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width:60px;"><%#Eval("TagProperty") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="trStudent">
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 35px;">
                                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                        <div class="gridCircle divStudentImage"></div>
                                                    </td>
                                                    <td>
                                                        <input type="hidden" class="hdnStudentID" value='<%#Eval("StudentID") %>' />
                                                        <label><%#Eval("StudentName") %></label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <asp:Repeater ID="rptNoteRate" runat="server" OnItemDataBound="rptNoteRate_ItemDataBound">
                                            <ItemTemplate>
                                                <td class="thCenter">
                                                    <input type="hidden" class="hdnNoteRate" value='<%#Eval("StandardCodeID") %>' />
                                                    <label id="divStudentNoteRateCount" runat="server" class="lblNoteRate lblLink"></label>
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