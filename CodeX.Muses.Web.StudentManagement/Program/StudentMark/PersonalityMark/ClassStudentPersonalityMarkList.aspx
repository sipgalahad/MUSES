<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ClassStudentPersonalityMarkList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassStudentPersonalityMarkList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                var lstClassSubjectID = '';
                $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                    if (result != '') {
                        result += '|';
                        lstClassSubjectID += ',';
                    }
                    lstClassSubjectID += $(this).find('.keyField').html();
                    result += $(this).find('.keyField').html() + ';' + $(this).find('.ddlMark').val() + ';' + $(this).find('.txtMarkDescription').val();
                });
                $('#<%=hdnSaveValue.ClientID %>').val(result);
                $('#<%=hdnLstClassSubjectID.ClientID %>').val(lstClassSubjectID);
                onCustomButtonClick('save');
            });
        });

        $('.lblNoteRate.lblLink').live('click', function () {
            var id = $(this).closest('tr').find('.hdnNoteCategory').val() + '|' + $(this).parent().find('.hdnNoteRate').val();
            var url = ResolveUrl("~/Program/StudentMark/PersonalityMark/ClassStudentNoteViewDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Catatan', 1100, 550);
        });

        function onCbpViewEndCallback() {
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassSubjectID" runat="server" />
    <input type="hidden" id="hdnCurriculumMarkTypeID" runat="server" />
    <input type="hidden" id="hdnMarkTypeID" runat="server" />
    <input type="hidden" id="hdnID" value="" runat="server" />  
    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
    <Columns>
        <asp:BoundField DataField="ClassSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
        <asp:BoundField DataField="SubjectName" HeaderText="Aspek" ItemStyle-CssClass="tdSubjectName" />
        <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" HeaderText="Predikat" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:DropDownList ID="ddlMark" Width="100%" runat="server" CssClass="ddlMark" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-Width="700px" HeaderStyle-CssClass="thCenter" HeaderText="Keterangan" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:TextBox ID="txtMarkDescription" Width="100%" runat="server" CssClass="txtMarkDescription" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <%=GetLabel("No Data To Display")%>
    </EmptyDataTemplate>
</asp:GridView>

    <br />
    <h4><%=GetLabel("Catatan Individu") %></h4>
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
                                <th><%=GetLabel("Kategori") %></th>
                                <asp:Repeater ID="rptNoteRateHeader" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width:60px;"><%#Eval("TagProperty") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <asp:Repeater ID="rptNoteCategory" runat="server" OnItemDataBound="rptNoteCategory_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <label><%#Eval("StandardCodeName") %></label>
                                            <input type="hidden" class="hdnNoteCategory" value='<%#Eval("StandardCodeID") %>' />
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