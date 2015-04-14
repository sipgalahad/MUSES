<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="GradePromotionEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.GradePromotionEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnPromote" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Naik")%></div></li>
    <li id="btnReject" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Tidak Naik")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnPromote.ClientID %>').click(function () {
                var param = "";
                var lstStudentID = '';
                $('.chkIsSelected input:checked').each(function () {
                    $tr = $(this).closest('tr');
                    var id = $tr.find('.keyField').html();
                    if (param != '') {
                        param += '|';
                        lstStudentID += ',';
                    }
                    var idx = $tr.find('.hdnItemIndex').val();

                    var GCMajor = '';
                    var cboGCMajor = eval('cboGCMajor' + idx);
                    if (cboGCMajor.GetValue() != null && cboGCMajor.GetValue() != '')
                        GCMajor = cboGCMajor.GetValue();

                    param += id + ';' + GCMajor;
                    lstStudentID += id;
                });
                $('#<%=hdnSelectedValue.ClientID %>').val(param);
                $('#<%=hdnLstStudentID.ClientID %>').val(lstStudentID);
                cbpProcess.PerformCallback('promote');
            })

            $('#<%=btnReject.ClientID %>').click(function () {
                var param = "";
                $('.chkIsSelected input:checked').each(function () {
                    var id = $(this).closest('tr').find('.keyField').html();

                    if (param != '') {
                        param += ',';
                    }
                    param += id;
                });
                $('#<%=hdnLstStudentID.ClientID %>').val(param);
                
                cbpProcess.PerformCallback('reject');
            })

            $('#btnRefresh').click(function () {
                cbpView.PerformCallback('refresh');
            })

            setStudentImage();
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
            $('.grdStudent').width(parseInt(s.cpTableWidth));
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

        $('.lblFinalMark').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + '|' + tacSchoolClass.getValue() + '|' + tacPeriodSection.getValue();
            var url = ResolveUrl("~/Program/StudentMark/StudentFinalMarkDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Nilai', 800, 550);       
        });

        function onCboSchoolPeriodValueChanged(s) {
            tacPeriodSection.setValue('');
            tacPeriodSection.setText('');
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpSubject.PerformCallback('refresh');
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
                        entityToControlClass(result);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                        entityToControlClass(null);
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
            var schoolClassID = tacSchoolClass.getValue();
            if (schoolClassID != '') {
                var filterExpression = "SchoolClassID = " + schoolClassID;
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    entityToControlClass(result);
                });
            }
        }

        function entityToControlClass(entity) {
            if (entity != null) {
                $('#<%=hdnGCMajor.ClientID %>').val(entity.GCMajor);
                $('#<%=hdnGCGrade.ClientID %>').val(entity.GCGrade);
                $('#<%=hdnNextGCGrade.ClientID %>').val(entity.NextGCGrade);
                $('#<%=hdnNextGrade.ClientID %>').val(entity.NextGrade); 
            }
            else {
                $('#<%=hdnGCMajor.ClientID %>').val('');
                $('#<%=hdnGCGrade.ClientID %>').val('');
                $('#<%=hdnNextGCGrade.ClientID %>').val('');
                $('#<%=hdnNextGrade.ClientID %>').val('');
            }
            setTimeout(function () {
                cbpSubject.PerformCallback('refresh');
            }, 100);
        }
        //#endregion

        $('.chkSelectAllSubject input').live('change', function () {
            var isChecked = $(this).is(':checked');
            $('.chkSubject input').each(function () {
                $(this).prop('checked', isChecked);
            });
            setDdeSubjectText();
        });

        $('.chkSubject input').live('change', function () {
            setDdeSubjectText();
        });

        function setDdeSubjectText() {
            var lstID = '';
            var lstName = '';
            $('.chkSubject input:checked').each(function () {
                if (lstName != '') {
                    lstName += ', ';
                    lstID += ',';
                }
                lstID += $(this).parent().attr('id');
                lstName += $(this).parent().attr('name');
            });
            $('#<%=hdnLstSubjectID.ClientID %>').val(lstID);
            ddeSubject.SetText(lstName);
        }

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
        .belowpassinggrade                  { color: Red !important; }
    </style>
    <input type="hidden" runat="server" id="hdnSelectedValue" />
    <input type="hidden" runat="server" id="hdnLstStudentID" />
    <input type="hidden" runat="server" id="hdnLstSubjectID" />
    <input type="hidden" runat="server" id="hdnNextSchoolPeriod" />
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
                <input type="hidden" id="hdnGCGrade" runat="server" />
                <input type="hidden" id="hdnNextGCGrade" runat="server" />
                <input type="hidden" id="hdnNextGrade" runat="server" />
                <input type="hidden" id="hdnGCMajor" runat="server" />
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mata Pelajaran")%></label></td>
            <td>
                <dxe:ASPxDropDownEdit ClientInstanceName="ddeSubject" ID="ddeSubject"
                    Width="300px" runat="server" EnableAnimation="False">
                    <DropDownWindowStyle BackColor="#EDEDED" />
                    <DropDownWindowTemplate>
                        <dxcp:ASPxCallbackPanel ID="cbpSubject" runat="server" Width="100%" ClientInstanceName="cbpSubject"
                            ShowLoadingPanel="false" OnCallback="cbpSubject_Callback">
                            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                                EndCallback="function(s,e){ hideLoadingPanel(); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="max-height:200px">
                                        <asp:GridView ID="grdSubject" runat="server" CssClass="grdBorder grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdSubject_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="30px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAllSubject" CssClass="chkSelectAllSubject" runat="server"  />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSubject" CssClass="chkSubject" runat="server"  /> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ClassSubjectID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                                <asp:BoundField DataField="SubjectName" HeaderText="Mata Pelajaran" />
                                                <asp:BoundField DataField="SubjectType" HeaderText="Tipe Pelajaran" HeaderStyle-Width="120px" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <%=GetLabel("No Data To Display")%>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </DropDownWindowTemplate>
                </dxe:ASPxDropDownEdit>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"></td>
            <td><asp:CheckBox ID="chkIsOnlyFinalMark" runat="server" /> <%=GetLabel("Tampilkan Hanya NPK") %></td>
        </tr>
        <tr>
            <td class="tdLabel"></td>
            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
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
                        <div style="width:1320px; overflow-x: auto;" id="divContainerTable" runat="server">
                            <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent" id="tblView">
                                <tr>
                                    <th rowspan="3" style="width:40px"></th>
                                    <th rowspan="3" colspan="2"><%=GetLabel("Siswa") %></th>
                                    <asp:Repeater ID="rptColHeaderLevel1" runat="server" OnItemDataBound="rptColHeaderLevel1_ItemDataBound">
                                        <ItemTemplate>
                                            <th class="thCenter" colspan="7" id="tdSubjectName" runat="server"><%#Eval("SubjectName") %></th>    
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th rowspan="3" style="width:100px"><%=GetLabel("Naik Ke Kelas") %></th>
                                    <th rowspan="3" style="width:120px"><%=GetLabel("Jurusan") %></th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel2" runat="server" OnItemDataBound="rptColHeaderLevel2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptColHeaderLevel2Dt" runat="server" OnItemDataBound="rptColHeaderLevel2Dt_ItemDataBound">
                                                <ItemTemplate>
                                                    <th class="thCenter" colspan="3" id="tdPeriodSection" runat="server"><%#Eval("PeriodSection") %></th>  
                                                </ItemTemplate>
                                            </asp:Repeater>        
                                            <th class="thCenter" rowspan="2" style="width:60px" id="tdFinalMark" runat="server"><%=GetLabel("NPK") %></th>  
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel3" runat="server" OnItemDataBound="rptColHeaderLevel3_ItemDataBound">
                                        <ItemTemplate>
                                            <th class="thCenter" style="width:60px" id="tdTheory" runat="server"><%=GetLabel("Peng") %></th>
                                            <th class="thCenter" style="width:60px" id="tdPractice" runat="server"><%=GetLabel("Prak") %></th>
                                            <th class="thCenter" style="width:60px" id="tdAffective" runat="server"><%=GetLabel("Sik") %></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="keyField"><%#Eval("StudentID")%></td>
                                            <td align="center"><asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" /></td>
                                            <td style="width:40px" align="center">
                                                <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                <div class="gridCircle divStudentImage"></div>
                                            </td>
                                            <td><%#Eval("StudentName") %></td>
                                            <asp:Repeater ID="rptStudentSubject" runat="server" OnItemDataBound="rptStudentSubject_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Repeater ID="rptStudentSubjectPeriodSection" runat="server" OnItemDataBound="rptStudentSubjectPeriodSection_ItemDataBound">
                                                        <ItemTemplate>
                                                            <td align="center" id="tdTheoryMark" runat="server"></td>
                                                            <td align="center" id="tdPracticeMark" runat="server"></td>
                                                            <td align="center" id="tdAffectiveMark" runat="server"></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <td align="center" id="tdFinalMark" runat="server"></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <td><div runat="server" id="divNextGrade"></div></td>
                                            <td>
                                                <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                <dxe:ASPxComboBox ID="cboGCMajor" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
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