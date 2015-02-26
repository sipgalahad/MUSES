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

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            calculateAll();

            $('#btnGenerate').click(function () {
                cbpProcess.PerformCallback('generate');
            });

            $('#btnSave').click(function () {
                var lstValue = "";
                var countGroup = 0;
                $('.hdnMarkTypeGroupID').each(function () {
                    if (lstValue != "") lstValue += "|";
                    var id = $(this).val();
                    var temp = "";
                    var countItem = 0;
                    $('.score' + id).each(function () {
                        if (temp != "") temp += ";";
                        temp += $('.hdnItemMarkKeyField' + id).eq(countItem).val() + "," + parseInt($(this).val()) + "," + $('.txtItemMarkInString' + id).eq(countItem).val() + "," + $('.txtRemarks'+id).eq(countItem).val();
                        countItem++;
                    })
                    lstValue += $('.hdnTeacherMarkGroupID').eq(countGroup).val() + "," + parseInt($('.tdGroupMark' + id).html()) + ";" + temp;
                    countGroup++;
                });
                $('#<%=hdnListValue.ClientID %>').val(lstValue);
                cbpProcess.PerformCallback('update');
            });
        });

        $('.txtItemMark').die('change');
        $('.txtItemMark').live('change',function () {
            calculateAll();
        });

        function calculateAll() {
            var totalAllItemMark = 0;
            var totalAllConvertion = 0;
            var totalGroupMark = 0;
            var countGroup = 0;

            $('.hdnMarkTypeGroupID').each(function () {
                var id = $(this).val();
                var totalItemMark = 0;
                var totalConvertion = 0;
                var totalBobot = 0;
                var countItem = 0;

                $('.score' + id).each(function () {
                    totalItemMark += parseFloat($(this).val());
                    var bobot = parseInt($('.tdItemFinalMarkPercentage' + id).eq(countItem).html());
                    totalBobot += bobot;

                    var grade = checkGrade($(this).val());
                    $('.txtItemMarkInString' + id).eq(countItem).val(grade);

                    $('.txtConvertion' + id).eq(countItem).val(parseFloat($(this).val() / 100 * bobot).toFixed(2));
                    totalConvertion += parseFloat($('.txtConvertion' + id).eq(countItem).val());
                    countItem++;
                })
                totalAllItemMark += (totalItemMark / countItem);
                totalAllConvertion += totalConvertion;

                var score = parseFloat(totalItemMark / countItem)
                $('.txtTotalItemMark' + id).val(score.toFixed(2));

                var grade = checkGrade(score);
                $('.txtTotalItemMarkInString' + id).val(grade);

                $('.txtTotalConvertion' + id).val(parseFloat(totalConvertion).toFixed(2));

                bobotGroup = parseInt($('.tdGroupFinalMarkPercentage' + id).html());
                var groupMark = parseFloat(score / totalBobot * bobotGroup);
                $('.tdGroupMark' + id).html(groupMark.toFixed(2));
                totalGroupMark += groupMark;
                countGroup++;
            });
            if (countGroup == 0) countGroup = 1;
            $('#<%=txtTotalAllItemMark.ClientID %>').val(parseFloat(totalAllItemMark / countGroup).toFixed(2));
            var scr = parseFloat(totalAllItemMark / countGroup).toFixed(2);
            var finalGrade = checkGrade(scr);
            $('#<%=txtTotalAllItemMarkInString.ClientID %>').val(finalGrade);
            $('#<%=txtTotalAllConvertion.ClientID %>').val(parseFloat(totalAllConvertion / countGroup).toFixed(2));
            $('.tdTotalGroupMark').html(parseFloat(totalGroupMark).toFixed(2));
            var temp = parseInt(totalGroupMark) + ";" + finalGrade;
            $('#<%=hdnTotalGroupMark.ClientID %>').val(temp);
        }

        function checkGrade(value) {
            var temp = "<%=OnGetScoreGrade() %>";
            var score = temp.split('|')
            
            for (var i = 0; i < 12; i++) {
                var val = score[i].split(',');
                if (parseFloat(value) > parseFloat(val[0])) return val[1];
            }
            
            var val = score[12].split(',');
            return val[1];
        }

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
            calculateAll();
            var id = $('#<%=hdnTeacherMarkID.ClientID %>').val();
            if (id != "") $('#btnGenerate').hide();
            else $('#btnGenerate').show();
            hideLoadingPanel();
        }

        function onCboSchoolPeriodValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

        function onCboMonthValueChanged(s) {
            cbpView.PerformCallback('refresh');
        }

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
                    cbpView.PerformCallback('refresh');
                });
            });

        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'generate') {
                if (param[1] == 'fail')
                    showToast('Generate Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            } else if (param[0] == 'update') {
                if (param[1] == 'fail')
                    showToast('Update Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
        
        
        .tblTeacherMark thead{ background-color:#EEEEEE; }
        .tblTeacherMark tr td {border: 1px solid #EEEEEE; }
        .tblTeacherMark table tr td {border: 0px; }
        
    </style>
    <input type="hidden" runat="server" id="hdnListValue" />
    
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><label class="lblMandatory"><%=GetLabel("Tahun Ajaran") %></label></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { showLoadingPanel();onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Bulan")%></label></td>
            <td>
                <dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth">
                    <ClientSideEvents ValueChanged="function(s,e){ showLoadingPanel();onCboMonthValueChanged(s);}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetEmployeeList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                    SearchFields="Name" TextField="Name" ValueField="EmployeeID" SearchText="${Name} (<b>${EmployeeCode}</b>)" OrderByExpression="Name">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                        ValueChanged="function(){ onTacTeacherSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="button" id="btnGenerate" value="Generate" /></td>
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
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height: 390px;">
                        <input type="hidden" value="" id="hdnStartDate" runat="server" />
                        <input type="hidden" runat="server" id="hdnTeacherMarkID" />
                        <input type="hidden" runat="server" id="hdnPeriodSectionID" />
                        <input type="hidden" id="hdnTotalGroupMark" runat="server" value="" />
                        <table width="100%" class="grdBorder grdSelected" cellpadding="0" cellspacing="0" border="0">
                            <colgroup>
                                <col width="250px"/>
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
                                            <input type="hidden" runat="server" class="hdnTeacherMarkGroupID" value='<%#:Eval("TeacherMarkGroupID") %>' />
                                            <input type="hidden" runat="server" class="hdnMarkTypeGroupID" value='<%#:Eval("TeacherMarkTypeGroupID") %>' />
                                            <input type="hidden" runat="server" id="hdnFinalItemMark" class="hdnFinalItemMark" value="" />
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <colgroup>
                                                    <col width="80%"/>
                                                    <col />
                                                </colgroup>
                                                <tr>
                                                    <td><b><%#:Eval("TeacherMarkTypeGroupName") %></b></td>
                                                    <td align="center" id="tdGroupFinalMarkPercentage" runat="server" class="tdGroupFinalMarkPercentage" style="font-weight:bold;"><%#:Eval("FinalMarkPercentage") %></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" id="tdItemFinalMarkPercentage" runat="server"></td>
                                        <td runat="server" id="tdTeacherMarkTypeItemName"></td>
                                        <td>
                                            <input type="hidden" id="hdnItemMarkKeyField" runat="server" value="" />
                                            <asp:TextBox runat="server" ID="txtItemMark" CssClass="txtItemMark number" Width="70px" Text="" />
                                        </td>
                                        <td><asp:TextBox runat="server" ID="txtItemMarkInString" CssClass="txtItemMarkInString" ReadOnly="true" Width="70px" /></td>
                                        <td><asp:TextBox runat="server" ID="txtConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" ID="txtRemarks" CssClass="txtRemarks" Width="200px" Text='' /></td>
                                        <td id="tdNote" runat="server"></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptTeacherMarkItem" OnItemDataBound="rptTeacherMarkItem_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" id="tdItemFinalMarkPercentage" runat="server"><%#:Eval("FinalMarkPercentage") %></td>
                                                <td><%#:Eval("TeacherMarkTypeItemName") %></td>                        
                                                <td>
                                                    <input type="hidden" id="hdnItemMarkKeyField" runat="server" value='<%#:Eval("TeacherMarkItemID") %>' />
                                                    <asp:TextBox runat="server" ID="txtItemMark" CssClass="txtItemMark number" Width="70px" Text='<%#:Eval("Mark") %>' />
                                                </td>
                                                <td><asp:TextBox runat="server" ID="txtItemMarkInString" CssClass="txtItemMarkInString" ReadOnly="true" Width="70px" Text='<%#:Eval("MarkInString") %>' /></td>
                                                <td><asp:TextBox runat="server" ID="txtConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                                <td><asp:TextBox runat="server" ID="txtRemarks" CssClass="txtRemarks" Width="200px" Text='<%#:Eval("Remarks") %>' /></td>
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
                                                    <td align="center" class='tdGroupMark tdGroupMark<%#:Eval("TeacherMarkTypeGroupID") %>' style="font-weight:bold;"><%#:Eval("Mark") %></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td runat="server" id="tdTotalItemFinalMark" align="center"></td>
                                        <td><b><%:GetLabel("PENCAPAIAN MUTU") %> <%#:Eval("TeacherMarkTypeGroupName") %></b></td>
                                        <td><asp:TextBox runat="server" id="txtTotalItemMark" ReadOnly="true" Text="0" CssClass='txtTotalItemMark number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" id="txtTotalItemMarkInString" ReadOnly="true" CssClass="txtTotalItemMarkInString" Width="70px"/></td>
                                        <td><asp:TextBox runat="server" id="txtTotalConvertion" ReadOnly="true" Text="0" CssClass='txtTotalConvertion number' Width="70px" /></td>
                                        <td></td>
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
                                            <td><b><%:GetLabel("KONTRIBUSI BOBOT PERAN :")%></b></td>
                                            <td align="center" class="tdTotalGroupMark" style="font-weight:bold;"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="tdTotalAllItemFinalMark" align="center" style="font-weight:bold;" runat="server"></td>
                                <td><b><%:GetLabel("CAPAIAN KINERJA MUTU MANAJEMEN") %></b></td>
                                <td><asp:TextBox runat="server" id="txtTotalAllItemMark" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                <td><asp:TextBox ID="txtTotalAllItemMarkInString" runat="server" CssClass="txtTotalAllItemMarkInString" ReadOnly="true" Width="70px"/></td>
                                <td><asp:TextBox runat="server" id="txtTotalAllConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
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
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel> 
    </div>
</asp:Content>