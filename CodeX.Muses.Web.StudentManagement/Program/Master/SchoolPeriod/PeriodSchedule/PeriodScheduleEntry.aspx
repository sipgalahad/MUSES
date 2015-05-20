<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="PeriodScheduleEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.PeriodScheduleEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        Date.prototype.yyyymmdd = function () {
            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = this.getDate().toString();
            return yyyy + (mm[1] ? mm : "0" + mm[0]) + (dd[1] ? dd : "0" + dd[0]); // padding
        };

        var lstHoliday = [];
        var lstEntity = [];
        function setListEntitySchedule() {
            lstEntity = [];
            $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
                $row = $(this).closest('tr');
                var entity = rowToObject($row);
                lstEntity.push(entity);
            });
            var year = $('#<%=hdnYear.ClientID %>').val();
            var month = $('#<%=hdnMonth.ClientID %>').val();
            if (month != '') {
                var date = new Date();
                date.setDate(1);
                date.setMonth(month - 1);
                date.setYear(year);
                $('#calSchedule').datepicker("setDate", date);
            }
        }

        function setCalSchedule() {
            var temp = $('#<%=hdnMaxDate.ClientID %>').val().split('-');
            var maxDate = new Date(temp[0], temp[1], temp[2]);
            temp = $('#<%=hdnMinDate.ClientID %>').val().split('-');
            var minDate = new Date(temp[0], temp[1], temp[2]);

            $('#calSchedule').datepicker({
                inline: true,
                minDate: minDate,
                maxDate: maxDate,
                beforeShowDay: function (date) {
                    var theday = date.yyyymmdd();
                    for (var i = 0; i < lstEntity.length; ++i) {
                        var entity = lstEntity[i];
                        if (theday >= entity.StartDateyyyyMMdd && theday <= entity.EndDateyyyyMMdd)
                            return [true, "date" + entity.cfGCPeriodScheduleType, entity.PeriodScheduleName];
                    }

                    for (var i = 0; i < lstHoliday.length; ++i) {
                        var entity = lstHoliday[i];
                        if ((entity.IsAnnualHoliday && entity.HolidayDate == date.getDate() && entity.HolidayMonth == date.getMonth() + 1)
                           || (!entity.IsAnnualHoliday && entity.HolidayDate == date.getDate() && entity.HolidayMonth == date.getMonth() + 1 && entity.HolidayYear == date.getFullYear()))
                            return [true, "specialDate", entity.HolidayName];
                    }

                    //if (date.getDay() < 1)
                    //    return [true, "specialDate"];
                    return [true, "", "Kegiatan Belajar Mengajar"];
                },
                onChangeMonthYear: function (year, month, instance) {
                    $('#<%=hdnYear.ClientID %>').val(year);
                    $('#<%=hdnMonth.ClientID %>').val(month);
                    if (!$('#<%=chkShowAll.ClientID %>').is(':checked'))
                        cbpView.PerformCallback('refresh')
                }
            });
        }

        $(function () {
            Methods.getListObject('GetHolidayList', 'IsDeleted = 0', function (result) {
                lstHoliday = result;

                setListEntitySchedule();
                setCalSchedule();
            });
        });

        $(function () {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtPeriodScheduleCode.ClientID %>').val('');
                $('#<%=txtPeriodScheduleName.ClientID %>').val('');
                $('#<%=txtStartDate.ClientID %>').val('');
                $('#<%=txtEndDate.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
                cboScheduleType.SetValue('');
                cboCurriculumMarkTypeDt.SetValue('');

                $('#<%=hdnLstClassTypeID.ClientID %>').val('');
                ddeClassType.SetText('');
                $('.chkClassType input:checked').each(function () {
                    $(this).prop('checked', false);
                });

                onCboScheduleTypeValueChanged();
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            $('#<%=chkShowAll.ClientID %>').change(function () {
                cbpView.PerformCallback('refresh');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodScheduleID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.PeriodScheduleID);
            $('#<%=txtPeriodScheduleCode.ClientID %>').val(entity.PeriodScheduleCode);
            $('#<%=txtPeriodScheduleName.ClientID %>').val(entity.PeriodScheduleName);
            $('#<%=txtStartDate.ClientID %>').val(entity.StartDateInDatePickerFormat);
            $('#<%=txtEndDate.ClientID %>').val(entity.EndDateInDatePickerFormat);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

            $('.chkClassType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            var lstClassTypeID = entity.ListPeriodClassTypeID.split(',');
            for (var i = 0; i < lstClassTypeID.length; ++i) {
                $('.chkClassType').each(function () {
                    if ($(this).attr('classtypeid') == lstClassTypeID[i])
                        $(this).find('input').prop('checked', true);
                });
            }
            setDdeClassTypeText();

            cboScheduleType.SetValue(entity.GCPeriodScheduleType);
            cboCurriculumMarkTypeDt.SetValue(entity.CurriculumMarkTypeDtID);
            onCboScheduleTypeValueChanged();
            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Class Type
        $('.chkClassType input').live('change', function () {
            setDdeClassTypeText();
        });

        function setDdeClassTypeText() {
            var lstClassTypeID = '';
            var lstClassTypeName = '';
            $('.chkClassType input:checked').each(function () {
                if (lstClassTypeName != '') {
                    lstClassTypeName += ', ';
                    lstClassTypeID += ',';
                }
                lstClassTypeID += $(this).parent().attr('classtypeid');
                lstClassTypeName += $(this).parent().attr('classtypename');
            });
            $('#<%=hdnLstClassTypeID.ClientID %>').val(lstClassTypeID);
            ddeClassType.SetText(lstClassTypeName);
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        function onCboScheduleTypeValueChanged() {
            if (cboScheduleType.GetValue() == "<%=GetInternalUjianValue() %>")
                $('#trTaskType').removeAttr('style');
            else
                $('#trTaskType').attr('style', 'display:none');
        }
    </script>
    <style type="text/css">
        .specialDate a.ui-state-default, .specialDate a.ui-state-hover       { background: #FF1901 !important; color: White; }
        
        <asp:Repeater ID="rptDateStyle" runat="server">
            <ItemTemplate>
                .nts<%#Eval("cfStandardCodeID")%>, .date<%#Eval("cfStandardCodeID")%> a.ui-state-default, .date<%#Eval("cfStandardCodeID")%> a.ui-state-hover      { background: <%#Eval("TagProperty")%> !important; }
            </ItemTemplate>
        </asp:Repeater>
    </style>
    <input type="hidden" id="hdnMonth" runat="server" />
    <input type="hidden" id="hdnYear" runat="server" />
    <input type="hidden" id="hdnMaxDate" runat="server" />
    <input type="hidden" id="hdnMinDate" runat="server" />
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    <table style="width:100%">
        <colgroup>
            <col style="width:250px" />
        </colgroup>
        <tr>
            <td valign="top">
                <div id="calSchedule"></div>                
                <div style="font-weight: bold;"><%=GetLabel("Keterangan") %> :</div>
                <asp:Repeater ID="rptRemarks" runat="server">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><div class='nts<%#Eval("cfStandardCodeID") %>' style="width: 20px; height: 20px; border: 1px solid black;"></div></td>
                            <td><%#Eval("StandardCodeName") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
            <td valign="top">
                <div class="divTransactionEntry">
                    <asp:CheckBox runat="server" ID="chkShowAll" Text="Show All" /><br />
                    <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span>
                    <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
                        <fieldset id="fsTrx" style="margin: 0">
                            <input type="hidden" value="" id="hdnEntryID" runat="server" />
                            <table style="width: 100%">
                                <colgroup>
                                    <col style="width: 50%" />
                                </colgroup>
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <colgroup>
                                                <col style="width: 150px" />
                                            </colgroup>
                                            <tr>
                                                <td class="tdLabel"><label><%=GetLabel("Kode")%></label></td>
                                                <td><asp:TextBox ID="txtPeriodScheduleCode" Width="100px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label><%=GetLabel("Nama")%></label></td>
                                                <td><asp:TextBox ID="txtPeriodScheduleName" Width="300px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Jadwal")%></label></td>
                                                <td>
                                                    <dxe:ASPxComboBox runat="server" ID="cboScheduleType" ClientInstanceName="cboScheduleType" Width="300px">
                                                        <ClientSideEvents ValueChanged="function(s,e){ onCboScheduleTypeValueChanged(); }" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr id="trTaskType">
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Ujian")%></label></td>
                                                <td><dxe:ASPxComboBox runat="server" ID="cboCurriculumMarkTypeDt" ClientInstanceName="cboCurriculumMarkTypeDt" Width="300px" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                                                <td>
                                                    <dxe:ASPxDropDownEdit ClientInstanceName="ddeClassType" ID="ddeClassType"
                                                        Width="300px" runat="server" EnableAnimation="False">
                                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                                        <DropDownWindowTemplate>
                                                            <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkClassType" CssClass="chkClassType" runat="server"  /> <%#Eval("CurriculumClassTypeName")%><br />
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </DropDownWindowTemplate>
                                                    </dxe:ASPxDropDownEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Mulai")%></label></td>
                                                <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Selesai")%></label></td>
                                                <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                                <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td> 
                                        <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                                        <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                            EndCallback="function(s,e){ setListEntitySchedule(); hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                                    position: relative; font-size: 0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="PeriodScheduleID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="PeriodScheduleCode" HeaderText="Kode" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="PeriodScheduleName" HeaderText="Nama"/>
                                            <asp:BoundField DataField="PeriodScheduleType" HeaderText="Tipe" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="ListPeriodClassTypeName" HeaderText="Tipe Kelas" HeaderStyle-Width="200px"/>
                                            <asp:BoundField DataField="StartDateInString" HeaderText="Tanggal Mulai" HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="EndDateInString" HeaderText="Tanggal Selesai" HeaderStyle-Width="150px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div style='float:right;' class="divDetailDelete"></div>
                                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("PeriodScheduleID") %>" bindingfield="PeriodScheduleID" />
                                                    <input type="hidden" value="<%#Eval("PeriodScheduleCode") %>" bindingfield="PeriodScheduleCode" />
                                                    <input type="hidden" value="<%#Eval("PeriodScheduleName") %>" bindingfield="PeriodScheduleName" />
                                                    <input type="hidden" value="<%#Eval("StartDateInDatePickerFormat") %>" bindingfield="StartDateInDatePickerFormat" />
                                                    <input type="hidden" value="<%#Eval("EndDateInDatePickerFormat") %>" bindingfield="EndDateInDatePickerFormat" />
                                                    <input type="hidden" value="<%#Eval("GCPeriodScheduleType") %>" bindingfield="GCPeriodScheduleType" />
                                                    <input type="hidden" value="<%#Eval("CurriculumMarkTypeDtID") %>" bindingfield="CurriculumMarkTypeDtID" />
                                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                                    <input type="hidden" value="<%# Eval("StartDate", "{0:yyyyMMdd}")%>" bindingfield="StartDateyyyyMMdd" />
                                                    <input type="hidden" value="<%# Eval("EndDate", "{0:yyyyMMdd}")%>" bindingfield="EndDateyyyyMMdd" />
                                                    <input type="hidden" value="<%#Eval("ListPeriodClassTypeID") %>" bindingfield="ListPeriodClassTypeID" />
                                                    <input type="hidden" value="<%#Eval("ListPeriodClassTypeName") %>" bindingfield="ListPeriodClassTypeName" />
                                                    <input type="hidden" value="<%#Eval("cfGCPeriodScheduleType") %>" bindingfield="cfGCPeriodScheduleType" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </div>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>