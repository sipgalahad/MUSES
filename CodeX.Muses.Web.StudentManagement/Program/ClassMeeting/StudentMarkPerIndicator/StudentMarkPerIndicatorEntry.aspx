<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="StudentMarkPerIndicatorEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentMarkPerIndicatorEntry" %>
    
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
            setStudentImage();

            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';

                $('.trStudent').each(function () {
                    var studentID = $(this).find('.hdnStudentID').val();
                    var mark = $(this).find('.divStudentFinalMark').html();
                    var minMark = $(this).find('.hdnMinDesc').val();
                    var maxMark = $(this).find('.hdnMaxDesc').val();

                    if (result != '')
                        result += '|';
                    result += studentID + ';' + mark + ';' + minMark + ';' + maxMark;
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });
        });

        $('.lblStudent.lblLink').live('click', function () {
            var id = $(this).parent().find('.hdnStudentID').val() + '|' + cboLessonType.GetValue() + '|' + $('#<%=hdnSummaryType.ClientID %>').val();
            var url = ResolveUrl("~/Program/ClassMeeting/StudentMarkPerIndicatorInformation/StudentMarkPerIndicatorDtViewCtl.ascx");
            openUserControlPopup(url, id, 'Detil Nilai', 800, 550);
        });

        function onCboLessonTypeValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        function onCbpViewEndCallback() {
            hideLoadingPanel();
            setStudentImage();
        }
    </script>
    <style type="text/css">
        .thSubjectIndicator .divSubjectIndicatorName        { display: none; }
        .thSubjectIndicator:hover .divSubjectIndicatorName  { display: block; }
        .thSubjectIndicator                                 { cursor: pointer; }
    </style>
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <table cellspacing="0">
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
            <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pelajaran")%></label></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboLessonType" ClientInstanceName="cboLessonType" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboLessonTypeValueChanged() }" />
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
                        <input type="hidden" id="hdnSummaryType" runat="server" />
                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent" id="tblView">
                            <tr>
                                <th><%=GetLabel("Siswa") %></th>
                                <asp:Repeater ID="rptSubjectIndicatorHeader" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter thSubjectIndicator" style="width:60px;"><%# Container.ItemIndex + 1 %>
                                            <div style="width:100%; position: relative;">
                                                <div class="divSubjectIndicatorName" style="position: absolute; right: 0; width: 150px; height: 30px; background-color: #FFF !important; border: 1px solid #AAA;"><%#Eval("SubjectIndicatorName") %></div>
                                            </div>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <th class="thCenter thSubjectIndicator" class="thCenter" style="width:60px;">
                                    Rata-Rata
                                </th>
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
                                                        <label class="lblStudent lblLink"><%#Eval("StudentName") %></label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
                                            <ItemTemplate>
                                                <td class="thCenter">
                                                    <div id="divStudentMark" runat="server"></div>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td class="thCenter">
                                            <div id="divStudentFinalMark" class="divStudentFinalMark" runat="server"></div>
                                            <input type="hidden" id="hdnMinDesc" class="hdnMinDesc" runat="server" />
                                            <input type="hidden" id="hdnMaxDesc" class="hdnMaxDesc" runat="server" />
                                        </td>
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