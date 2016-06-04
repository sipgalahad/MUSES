<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="StudentNoteEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentNoteEntry" %>

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

        $('.lblStudent').live('click', function () {
            var id = $(this).closest('table').parent().closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/StudentNote/StudentNoteEntryDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Catatan Individu', 900, 550);  
        });

        function onCbpViewEndCallback(s) {
            setStudentImage();
            hideLoadingPanel();
        }

        function onAfterPopupControlClosing() {
            cbpView.PerformCallback('refresh');
        }
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <style type="text/css">
        h4                                                  { color: #013EDD; }
    </style>     
    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpViewEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <div style="height: 415px; overflow-y: scroll; overflow-x: hidden; font-size: 12px;">
                    <div class="containerTblEntryContent">
                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                            <tr>
                                <th><%=GetLabel("Siswa") %></th>
                                <th class="thCenter" style="width:700px"><%=GetLabel("Catatan") %></th>
                            </tr>
                            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="trStudent">
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
                                                        <label class="lblLink lblStudent"><%#Eval("StudentName") %></label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtStudentNote" runat="server" CssClass="txtStudentNote" ReadOnly="true" Text="" Width="95%" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</asp:Content>