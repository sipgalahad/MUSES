<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolClassPageTrx.master" AutoEventWireup="true" EnableEventValidation="false"  
    CodeBehind="SchoolClassMarkPerIndicatorAllInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassMarkPerIndicatorAllInformation" %>

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

        function onCboLessonTypeValueChanged() {
            $('#<%=hdnLessonType.ClientID %>').val(cboLessonType.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCboSubjectValueChanged() {
            $('#<%=hdnSubject.ClientID %>').val(cboSubject.GetValue());
            cbpView.PerformCallback('refresh');
        }

        function onCbpViewEndCallback() {
            setStudentImage();
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" />
    <style type="text/css">
        .thSubjectIndicator .divSubjectIndicatorName        { display: none; }
        .thSubjectIndicator:hover .divSubjectIndicatorName  { display: block; }
        .thSubjectIndicator                                 { cursor: pointer; }
    </style>
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
                <dx:PanelContent ID="PanelContent2" runat="server">
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
                        </table>

                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                            <tr>
                                <th rowspan="3"><%=GetLabel("Siswa") %></th>
                                <th id="thMark" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptSubjectIndicatorHeader" runat="server" OnItemDataBound="rptSubjectIndicatorHeader_ItemDataBound">
                                    <ItemTemplate>
                                        <th id="thSubjectIndicator" runat="server" class="thCenter thSubjectIndicator" style="width:60px;"><%# Container.ItemIndex + 1 %>
                                            <div style="width:100%; position: relative;">
                                                <div class="divSubjectIndicatorName" style="position: absolute; right: 0; width: 150px; height: 30px; background-color: #FFF !important; border: 1px solid #AAA;"><%#Eval("SubjectIndicatorName") %></div>
                                            </div>
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptSubjectIndicatorHeader2" runat="server" OnItemDataBound="rptSubjectIndicatorHeader2_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptClassTaskHeader" runat="server">
                                            <ItemTemplate>
                                                <th class="thCenter" style="width:90px">
                                                    <%#Eval("cfClassTaskCode")%>
                                                </th>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                                        <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td align="center">
                                                            <div id="divStudentMark" runat="server"></div>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
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
                                    <th rowspan="3" style="width:80px"><%=GetLabel("NIK") %></th>
                                    <th rowspan="3"><%=GetLabel("Siswa") %></th>
                                    <th id="th1" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptSubjectIndicatorHeaderPrint" runat="server" OnItemDataBound="rptSubjectIndicatorHeader_ItemDataBound">
                                        <ItemTemplate>
                                            <th id="thSubjectIndicator" runat="server" class="thCenter thSubjectIndicator" style="width:60px;"><%# Container.ItemIndex + 1 %>
                                                <div style="width:100%; position: relative;">
                                                    <div class="divSubjectIndicatorName" style="position: absolute; right: 0; width: 150px; height: 30px; background-color: #FFF !important; border: 1px solid #AAA;"><%#Eval("SubjectIndicatorName") %></div>
                                                </div>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptSubjectIndicatorHeaderPrint2" runat="server" OnItemDataBound="rptSubjectIndicatorHeader2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptClassTaskHeader" runat="server">
                                                <ItemTemplate>
                                                    <th class="thCenter" style="width:90px">
                                                        <%#Eval("cfClassTaskCode")%>
                                                    </th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptStudentPrint" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("StudentCode") %></td>
                                            <td><%#Eval("StudentName") %></td>
                                            <asp:Repeater ID="rptSubjectIndicator" runat="server" OnItemDataBound="rptSubjectIndicator_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                                        <ItemTemplate>
                                                            <td align="center">
                                                                <div id="divStudentMark" runat="server"></div>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
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