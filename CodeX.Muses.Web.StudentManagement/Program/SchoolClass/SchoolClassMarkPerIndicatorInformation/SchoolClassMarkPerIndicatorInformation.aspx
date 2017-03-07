<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolClassPageTrx.master" AutoEventWireup="true" EnableEventValidation="false"  
    CodeBehind="SchoolClassMarkPerIndicatorInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassMarkPerIndicatorInformation" %>

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

        $('.lblStudent.lblLink').live('click', function () {
            var id = $(this).parent().find('.hdnStudentID').val() + '|' + cboLessonType.GetValue() + '|' + $('#<%=hdnSummaryType.ClientID %>').val() + '|' + cboSubject.GetValue();
            var url = ResolveUrl("~/Program/SchoolClass/SchoolClassMarkPerIndicatorInformation/SchoolClassMarkPerIndicatorDtViewCtl.ascx");
            openUserControlPopup(url, id, 'Detil Nilai', 800, 550);
        });

        function onCboLessonTypeValueChanged() {
            $('#<%=hdnLessonType.ClientID %>').val(cboLessonType.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCboSummaryTypeValueChanged() {
            $('#<%=hdnCboSummaryType.ClientID %>').val(cboSummaryType.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCboSubjectValueChanged() {
            $('#<%=hdnSubject.ClientID %>').val(cboSubject.GetValue());
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
    <div style="width:1250px; overflow-x: auto;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView">
                        <table cellspacing="0">
                            <tr>
                                <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
                                <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Pelajaran")%></label></td>
                                <td>    
                                    <input type="hidden" id="hdnLessonType" runat="server" />
                                    <dxe:ASPxComboBox runat="server" ID="cboLessonType" ClientInstanceName="cboLessonType" Width="200px">
                                        <ClientSideEvents ValueChanged="function(s,e){ onCboLessonTypeValueChanged() }" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Nilai")%></label></td>
                                <td>    
                                    <input type="hidden" id="hdnCboSummaryType" runat="server" />
                                    <dxe:ASPxComboBox runat="server" ID="cboSummaryType" ClientInstanceName="cboSummaryType" Width="200px">
                                        <ClientSideEvents ValueChanged="function(s,e){ onCboSummaryTypeValueChanged() }" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
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
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <div style="display:none">
                        <asp:Panel runat="server" ID="pnlPrint">
                            <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent" border="1">
                                <tr>
                                    <th style="width:80px"><%=GetLabel("NIS") %></th>
                                    <th><%=GetLabel("Siswa") %></th>
                                    <asp:Repeater ID="rptSubjectIndicatorHeader2" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter thSubjectIndicator" style="width:60px;"><%# Container.ItemIndex + 1 %>
                                                <div style="width:100%; position: relative;">
                                                    <div class="divSubjectIndicatorName" style="position: absolute; right: 0; width: 150px; height: 30px; background-color: #FFF !important; border: 1px solid #AAA;"><%#Eval("SubjectIndicatorName") %></div>
                                                </div>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptStudent2" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="trStudent">
                                            <td><%#Eval("StudentCode") %></td>
                                            <td><%#Eval("StudentName") %></td>
                                            <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
                                                <ItemTemplate>
                                                    <td class="thCenter">
                                                        <div id="divStudentMark" runat="server"></div>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:Panel>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>  
    </div>
</asp:Content>