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
            calculateAll();
            $('.txtItemMark').change(function () {
                var $tr = $(this).closest('tr');
                $id = $tr.find('.hdnMarkTypeGroupID');
                var hdnFinalItemMark = parseInt($tr.find('.hdnFinalItemMark').val());
                var hdnGroupFinalMarkPercentage = parseInt($tr.find('.hdnGroupFinalMarkPercentage').val());

                while (typeof $id.val() === 'undefined') {
                    $tr = $tr.prev();
                    $id = $tr.find('.hdnMarkTypeGroupID');
                    hdnFinalItemMark = parseInt($tr.find('.hdnFinalItemMark').val());
                    hdnGroupFinalMarkPercentage = parseInt($tr.find('.hdnGroupFinalMarkPercentage').val());
                    $tdGroupMark = $tr.find('.tdGroupMark');
                }

                $tr = $(this).closest('tr');
                $tdGroupMark = $tr.find('.tdGroupMark');
                while ($tdGroupMark.html() == null) {
                    $tr = $tr.next();
                    $tdGroupMark = $tr.find('.tdGroupMark');
                }
                var total = 0;
                var count = 0;
                var totalConvertion = 0;
                $('.score' + $id.val()).each(function () {
                    var bobot = parseInt($(this).closest('td').prev().prev().html());
                    var score = parseInt($(this).val())
                    var conv = parseFloat(score / 100 * bobot);
                    $convertion = $(this).closest('td').next().next().find('.txtConvertion' + $id.val());
                    $convertion.val(conv.toFixed(2));
                    total += score;
                    totalConvertion += conv;
                    count++;
                });

                $('.txtTotalConvertion' + $id.val()).val(totalConvertion.toFixed(2));
                $('.txtTotalItemMark' + $id.val()).val(parseFloat(total / count).toFixed(2));
                var groupMark = parseFloat(totalConvertion / hdnFinalItemMark * hdnGroupFinalMarkPercentage).toFixed(2);
                $tdGroupMark.html(groupMark);
                calculateAll();
            });

            $('#btnGenerate').click(function () {
                cbpProcess.PerformCallback('generate');
            });
        });

        function calculateAll() {
            var totalAllItemMark = 0;
            var totalAllConvertion = 0;
            var totalGroupMark = 0;
            var countGroup = 0;
            $('.txtTotalItemMark').each(function () {
                totalAllItemMark += parseFloat($(this).val());
                countGroup++;
            })

            $('.txtTotalConvertion').each(function () {
                totalAllConvertion += parseFloat($(this).val());
            })

            $('.tdGroupMark').each(function () {
                totalGroupMark += parseFloat($(this).html());
            })

            $('#<%=txtTotalAllItemMark.ClientID %>').val(parseFloat(totalAllItemMark / countGroup).toFixed(2));
            $('#<%=txtTotalAllConvertion.ClientID %>').val(parseFloat(totalAllConvertion / countGroup).toFixed(2));
            $('.tdTotalGroupMark').html(parseFloat(totalGroupMark).toFixed(2));
            
        }

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
    <input type="hidden" runat="server" id="hdnSelectedValue" />
    <table>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Guru")%></label></td>
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
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <input type="hidden" value="" id="hdnStartDate" runat="server" />
                        <table width="100%" class="tblTeacherMark" cellpadding="0" cellspacing="1">
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
                                            <input type="hidden" runat="server" class="hdnMarkTypeGroupID" value='<%#:Eval("TeacherMarkTypeGroupID") %>' />
                                            <input type="hidden" runat="server" class="hdnGroupFinalMarkPercentage" value='<%#:Eval("FinalMarkPercentage") %>' />
                                            <input type="hidden" runat="server" id="hdnFinalItemMark" class="hdnFinalItemMark" value="" />
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
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
                                        <td><asp:TextBox runat="server" ID="txtItemMark" CssClass="txtItemMark number" Width="70px" Text="" /></td>
                                        <td><asp:TextBox runat="server" ID="txtItemMarkInString" CssClass="txtItemMarkInString" ReadOnly="true" Width="70px" /></td>
                                        <td><asp:TextBox runat="server" ID="txtConvertion" ReadOnly="true" Text="0" CssClass='number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" CssClass="txtRemarks" Width="200px" Text='' /></td>
                                        <td id="tdNote" runat="server"></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptTeacherMarkItem" OnItemDataBound="rptTeacherMarkItem_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" id="tdItemFinalMarkPercentage" runat="server"><%#:Eval("FinalMarkPercentage") %></td>
                                                <td><%#:Eval("TeacherMarkTypeItemName") %></td>                        
                                                <td><asp:TextBox runat="server" ID="txtItemMark" CssClass="txtItemMark number" Width="70px" Text='<%#:Eval("Mark") %>' /></td>
                                                <td><asp:TextBox runat="server" CssClass="txtItemMarkInString" ReadOnly="true" Width="70px" Text='<%#:Eval("MarkInString") %>' /></td>
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
                                                    <td align="center" class="tdGroupMark" style="font-weight:bold;"><%#:Eval("Mark") %></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td runat="server" id="tdTotalItemFinalMark" align="center"></td>
                                        <td><b><%:GetLabel("PENCAPAIAN MUTU") %> <%#:Eval("TeacherMarkTypeGroupName") %></b></td>
                                        <td><asp:TextBox runat="server" id="txtTotalItemMark" ReadOnly="true" Text="0" CssClass='txtTotalItemMark number' Width="70px" /></td>
                                        <td><asp:TextBox runat="server" CssClass="txtTotalItemMarkInString" ReadOnly="true" Width="70px"/></td>
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