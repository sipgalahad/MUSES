<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="StudentDailyAttendanceEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentDailyAttendanceEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            setStudentImage();

            setDatePicker('<%=txtSchoolDate.ClientID %>');

            $('#<%=txtSchoolDate.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.grdStudent tr.trStudent').each(function () {
                    var studentID = $(this).find('.keyField').html();
                    var selected = $(this).find("input[type='radio']:checked");
                    var attendanceStatus = '';
                    if (selected.length > 0)
                        attendanceStatus = selected.val();
                    if (result != '')
                        result += '|';
                    result += studentID + ',' + attendanceStatus + ',' + $(this).find('.txtRemarks').val();
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });
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
            $('.grdStudent tr.trStudent').each(function () {
                var hdnAttendance = $(this).find(".hdnAttendance").val();
                if (hdnAttendance != '') {
                    $rdo = $(this).find("input[type='radio'][value='" + hdnAttendance + "']");
                    $rdo.attr('checked', true);
                }
            });

            $("input[type='radio'][name='rdoAttendanceAll']").live('change', function () {
                var selectedValue = $(this).val();
                $('.grdStudent tr:gt(1)').each(function () {
                    $rdo = $(this).find("input[type='radio'][value='" + selectedValue + "']");
                    $rdo.attr('checked', true);
                });
            });

            $('.rdoAttendanceDt').live('change', function () {
                $rdo = $("input[type='radio'][name='rdoAttendanceAll']:checked");
                if ($rdo != null)
                    $rdo.attr('checked', false);
            });
            setStudentImage();
            hideLoadingPanel();
        }

        function setStudentImage() {
            setTimeout(function () {
                var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
                var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");

                $('.imgStudentImage').each(function () {
                    $divStudentImage = $(this).parent().find('.divStudentImage');
                    $divStudentImage.attr('style', "background-image:url('" + this.src + "')");
                    $(this).error(function () {
                        var gender = $(this).parent().find('.hdnStudentGender').val();
                        if (gender == '0003^F')
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlF + "')");
                        else
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlM + "')");
                    }).attr('src', this.src);
                });
            }, 0);
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
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + cboSchoolPeriod.GetValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
            cbpView.PerformCallback('refresh');
        }
        //#endregion
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
    </style>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
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
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Semester")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacPeriodSection" ClientInstanceName="tacPeriodSection" MethodName="GetPeriodSectionList" GetFilterExpressionFunction="onGetPeriodSectionFilterExpression"
                    SearchFields="PeriodSectionName,PeriodSectionCode" TextField="PeriodSectionName" ValueField="PeriodSectionID" SearchText="${PeriodSectionName} (<b>${PeriodSectionCode}</b>)" OrderByExpression="PeriodSectionName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacPeriodSectionButtonSearchClick(); }"
                        ValueChanged="function(){ onTacPeriodSectionValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tanggal")%></label></td>
            <td><asp:TextBox ID="txtSchoolDate" runat="server" CssClass="datepicker" Width="120px" /></td>
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
                        <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                            <tr>
                                <th rowspan="2"><%=GetLabel("Siswa") %></th>
                                <th id="thHeaderAttendance" runat="server" class="thCenter"><%=GetLabel("STATUS KEHADIRAN") %></th>
                                <th rowspan="2" style="width:200px"><%=GetLabel("Keterangan") %></th>
                            </tr>
                            <tr>
                                <asp:Repeater ID="rptHeader" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width:100px">
                                            <%#Eval("StandardCodeName") %><br />
                                            <input type="radio" name="rdoAttendanceAll" value='<%#Eval("StandardCodeID") %>' />
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
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
                                                        <%#Eval("StudentName") %>
                                                        <input type="hidden" id="hdnAttendance" class="hdnAttendance" runat="server" value="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <asp:Repeater ID="rptStudentAttendance" runat="server">
                                            <ItemTemplate>
                                                <td align="center">
                                                    <input type="radio" class="rdoAttendanceDt" name="rdoAttendance<%# ((RepeaterItem)Container.Parent.Parent).ItemIndex %>" value='<%#Eval("StandardCodeID") %>' />
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td><asp:TextBox ID="txtRemarks" CssClass="txtRemarks" runat="server" Width="100%" /></td>
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