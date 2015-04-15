<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="StudentMarkLedgerEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentMarkLedgerEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
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
            $temp = $(".grdStudent").parent().clone();
            $temp.find('.grdStudent').attr('border', '1');
            $('#<%=hdnExportData.ClientID %>').val($temp.html());
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
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
            cbpSubject.PerformCallback('refresh');
        }

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
                $('#<%=hdnClassName.ClientID %>').val(entity.SchoolClassName);
                $('#<%=hdnGCMajor.ClientID %>').val(entity.GCMajor);
                $('#<%=hdnGCGrade.ClientID %>').val(entity.GCGrade);
                $('#<%=hdnNextGCGrade.ClientID %>').val(entity.NextGCGrade);
                $('#<%=hdnNextGrade.ClientID %>').val(entity.NextGrade);
            }
            else {
                $('#<%=hdnClassName.ClientID %>').val(''); 
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
    <input type="hidden" runat="server" id="hdnExportData" />
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
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <input type="hidden" id="hdnClassName" runat="server" />
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
                                    <th rowspan="3" colspan="2"><%=GetLabel("Siswa") %></th>
                                    <asp:Repeater ID="rptColHeaderLevel1" runat="server">
                                        <ItemTemplate>
                                            <th class="thCenter" colspan="6" id="tdSubjectName" runat="server"><%#Eval("SubjectName") %></th>    
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel2" runat="server" OnItemDataBound="rptColHeaderLevel2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Repeater ID="rptColHeaderLevel2Dt" runat="server">
                                                <ItemTemplate>
                                                    <th class="thCenter" colspan="3" id="tdPeriodSection" runat="server"><%#Eval("PeriodSection") %></th>  
                                                </ItemTemplate>
                                            </asp:Repeater>        
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rptColHeaderLevel3" runat="server">
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
                                            <td style="width:40px" align="center">
                                                <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                                <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                                <div class="gridCircle divStudentImage"></div>
                                            </td>
                                            <td style="width:340px"><%#Eval("StudentName") %></td>
                                            <asp:Repeater ID="rptStudentSubject" runat="server" OnItemDataBound="rptStudentSubject_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Repeater ID="rptStudentSubjectPeriodSection" runat="server" OnItemDataBound="rptStudentSubjectPeriodSection_ItemDataBound">
                                                        <ItemTemplate>
                                                            <td align="center" id="tdTheoryMark" runat="server"></td>
                                                            <td align="center" id="tdPracticeMark" runat="server"></td>
                                                            <td align="center" id="tdAffectiveMark" runat="server"></td>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
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
    </div>
</asp:Content>