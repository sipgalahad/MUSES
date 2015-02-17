<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="TeacherMarkGroupEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherMarkGroupEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('.txtMark').change(function () {
                var $tr = $(this).closest('tr');
                $id = $tr.find('.hdnMarkTypeGroupID');
                var hdnFinalItemMark = parseInt($tr.find('.hdnFinalItemMark').val());
                var hdnFinalGroupMarkPercentage = parseInt($tr.find('.hdnFinalGroupMarkPercentage').val());

                while (typeof $id.val() === 'undefined') {
                    $tr = $tr.prev();
                    $id = $tr.find('.hdnMarkTypeGroupID');
                    hdnFinalItemMark = parseInt($tr.find('.hdnFinalItemMark').val());
                    hdnFinalGroupMarkPercentage = parseInt($tr.find('.hdnFinalGroupMarkPercentage').val());
                    $tdGroupFinalMark = $tr.find('.tdGroupFinalMark');
                }

                $tr = $(this).closest('tr');
                $tdGroupFinalMark = $tr.find('.tdGroupFinalMark');
                while ($tdGroupFinalMark.html() == null) {
                    $tr = $tr.next();
                    $tdGroupFinalMark = $tr.find('.tdGroupFinalMark');
                }
                var total = 0;
                var count = 0;
                var totalConvertion = 0;
                $('.score' + $id.val()).each(function () {
                    var bobot = parseInt($(this).closest('td').prev().prev().html());
                    var score = parseInt($(this).val())
                    var conv = parseFloat(score / 100 * bobot);
                    $convertion = $(this).closest('td').next().next().find('.txtConvertion' + $id.val());
                    $convertion.val(conv);
                    total += score;
                    totalConvertion += conv;
                    count++;
                });
                $('.txtTotalConvertion' + $id.val()).val(totalConvertion);
                $('.txtTotalMark' + $id.val()).val(parseFloat(total / count).toFixed(2));
                var groupFinalMark = parseFloat(totalConvertion / hdnFinalItemMark * hdnFinalGroupMarkPercentage).toFixed(2);
                $tdGroupFinalMark.html("<b>"+groupFinalMark+"</b>");
            });
        });

        $('.lnkProcess').die('click');
        $('.lnkProcess').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            //var param = cboSchoolPeriod.GetValue() + '|' + tacPeriodSection.getValue() + '|' + tacTeacher.getValue();
            var param = id + '|' + tacTeacher.getValue();
            var url = ResolveUrl('~/Program/TeacherMark/TeacherMarkEntryCtl.ascx?id=' + param);
            //openWindowPopup(url, 'Penilaian Guru' + id, '1300', '650');
            
        });

        

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }

        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpView.PerformCallback('refresh');
        }

        //#region Period Section
        function onGetPeriodSectionFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND <%=OnGetPeriodSectionFilterExpression() %>";
            return filterExpression;
        }

        function onTacPeriodSectionButtonSearchClick() {
            openSearchDialog('periodsection', onGetPeriodSectionFilterExpression(), function (value) {
                var filterExpression = onGetPeriodSectionFilterExpression() + " AND PeriodSectionCode = '" + value + "'";
                Methods.getObject('GetPeriodSectionList', filterExpression, function (result) {
                    if (result != null) {
                        tacPeriodSection.setValue(result.PeriodSectionID);
                        tacPeriodSection.setText(result.PeriodSectionName);
                    }
                    else {
                        tacPeriodSection.setValue('');
                        tacPeriodSection.setText('');
                    }
                    onTacPeriodSectionValueChanged();
                });
            });

        }

        function onTacPeriodSectionValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('employee', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND EmployeeCode = '" + value + "'";
                Methods.getObject('GetEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeacher.setValue(result.EmployeeID);
                        tacTeacher.setText(result.FullName);
                    }
                    else {
                        tacTeacher.setValue('');
                        tacTeacher.setText('');
                    }
                });
            });

        }
        //#endregion
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
    </style>
    <input type="hidden" runat="server" id="hdnSelectedValue" />
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                    SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Guru")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetEmployeeList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="Name" TextField="Name" ValueField="EmployeeID" SearchText="${Name} (<b>${EmployeeCode}</b>)" OrderByExpression="Name">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTeacherSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
    </table>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <table width="100%" border="1">
                            <colgroup>
                                <col width="200px"/>
                                <col width="50px"/>
                                <col />
                                <col width="70px"/>
                                <col width="70px"/>
                                <col width="70px"/>
                                <col width="200px"/>
                                <col width="70px"/>
                            </colgroup>
                            <thead>
                                <tr>
                                    <th><%=GetLabel("Dimensi Bobot & Bobot") %></th>
                                    <th><%=GetLabel("Bobot") %></th>
                                    <th><%=GetLabel("Sub Dimensi") %></th>
                                    <th><%=GetLabel("Skoring") %></th>
                                    <th><%=GetLabel("Mutu") %></th>
                                    <th><%=GetLabel("Konversi = Skoring / 100 * Bobot") %></th>
                                    <th><%=GetLabel("Catatan") %></th>
                                    <th><%=GetLabel("Ket") %></th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="rptTeacerMarkGroup" OnItemDataBound="rptTeacerMarkGroup_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td id="tdTeacherMarkTypeGroupName" runat="server">
                                            <input type="hidden" runat="server" class="hdnMarkTypeGroupID" value='<%#:Eval("TeacherMarkTypeGroupID") %>' />
                                            <input type="hidden" runat="server" class="hdnFinalGroupMarkPercentage" value='<%#:Eval("FinalMarkPercentage") %>' />
                                            <input type="hidden" runat="server" id="hdnFinalItemMark" class="hdnFinalItemMark" value="" />
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col width="80%"/>
                                                    <col />
                                                </colgroup>
                                                <tr>
                                                    <td><b><%#:Eval("TeacherMarkTypeGroupName") %></b></td>
                                                    <td align="center"><b><%#:Eval("FinalMarkPercentage") %></b></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" id="tdItemFinalMarkPercentage" runat="server"></td>
                                        <td runat="server" id="tdTeacherMarkTypeItemName"></td>
                                        <td><asp:TextBox runat="server" ID="txtMark" CssClass="txtMark number" Width="70px" Text="0" /></td>
                                        <td><asp:TextBox runat="server" CssClass="txtMarkInString" Width="70px" /></td>
                                        <td><asp:TextBox runat="server" id="txtConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" CssClass="txtRemarks" Width="200px" /></td>
                                        <td id="tdNote" runat="server"></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptTeacherMarkItem" OnItemDataBound="rptTeacherMarkItem_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" id="tdItemFinalMarkPercentage" runat="server"><%#:Eval("FinalMarkPercentage") %></td>
                                                <td><%#:Eval("TeacherMarkTypeItemName") %></td>                        
                                                <td><asp:TextBox runat="server" ID="txtMark" CssClass="txtMark number" Width="70px" Text="0" /></td>
                                                <td><asp:TextBox runat="server" CssClass="txtMarkInString" Width="70px"/></td>
                                                <td><asp:TextBox runat="server" id="txtConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                                <td><asp:TextBox runat="server" CssClass="txtRemarks" Width="200px" /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <colgroup>
                                                    <col width="80%"/>
                                                    <col />
                                                </colgroup>
                                                <tr>
                                                    <td><b><%:GetLabel("KONTRIBUSI PERAN :")%></b></td>
                                                    <td align="center" class="tdGroupFinalMark"><b>0</b></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td runat="server" id="tdTotalItemFinalMark" align="center"></td>
                                        <td><b><%:GetLabel("PENCAPAIAN MUTU") %> <%#:Eval("TeacherMarkTypeGroupName") %></b></td>
                                        <td><asp:TextBox runat="server" id="txtTotalMark" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" CssClass="txtTotalMarkInString" ReadOnly="true" Width="70px"/></td>
                                        <td><asp:TextBox runat="server" id="txtTotalConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                        <td></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>