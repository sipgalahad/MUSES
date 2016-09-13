<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskRemedialEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskRemedialEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        setDatePicker('<%=txtTaskDate.ClientID %>');
        setDatePicker('<%=txtStartDate.ClientID %>');
        setDatePicker('<%=txtEndDate.ClientID %>');

        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtDisplayOrder.ClientID %>').val('1');
            $('#<%=txtTaskDate.ClientID %>').val('<%=OnGetDateNow() %>');
            $('#<%=txtStartDate.ClientID %>').val('<%=OnGetDateNow() %>');
            $('#<%=txtEndDate.ClientID %>').val('<%=OnGetDateNow() %>');
            $('#<%=txtStartTime.ClientID %>').val('<%=OnGetTimeNow() %>');
            $('#<%=txtEndTime.ClientID %>').val('<%=OnGetTimeNow() %>');
            $('#<%=txtRemarks.ClientID %>').val('');

            $('#entryDetailContainerPopup').show();
        });
        setStudentImage();

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup'))
                cbpProcessPopup.PerformCallback('save');
        });

        $('#<%=chkFilterUnderPassingGrade.ClientID %>').change(function () {
            cbpViewPopup.PerformCallback('refresh');
        });

        $('#btnSaveRemedialMark').click(function () {
            var result = '';
            var lstStudentID = '';
            var lstRemedialID = '';
            $('.trRemedialDt').each(function () {
                var temp = '';
                $(this).find('.txtMark').each(function () {
                    if ($(this).val() != '-') {
                        if (temp != '') {
                            temp += '^';
                            lstRemedialID += ',';
                        }
                        var remedialID = $(this).attr('ClassSubjectTaskRemedialID');
                        temp += $(this).attr('ClassSubjectTaskRemedialID') + ',' + $(this).val();
                        lstRemedialID += remedialID;
                    }
                });
                if (temp != '') {
                    if (result != '') {
                        result += '|';
                        lstStudentID += ',';
                    }
                    var studentID = $(this).find('.keyField').html();
                    result += studentID + ';' + $(this).find('.txtOriginalMark').val() + ';' + $(this).find('.txtFinalMark').val() + ';' + temp;
                    lstStudentID += studentID;
                }
            });
            $('#<%=hdnSaveValue.ClientID %>').val(result);
            $('#<%=hdnListStudentID.ClientID %>').val(lstStudentID);
            $('#<%=hdnListRemedialID.ClientID %>').val(lstRemedialID);
            
            cbpProcessPopup.PerformCallback('savemark');
        });
    });

    $('.divDetailDelete').die('click');
    $('.divDetailDelete').live('click', function () {
        $row = $(this).closest('th');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntryID.ClientID %>').val(entity.ClassSubjectTaskRemedialID);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('.lblRemedialDisplayOrder').die('click');
    $('.lblRemedialDisplayOrder').live('click', function () {
        $row = $(this).closest('th');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.ClassSubjectTaskRemedialID);
        $('#<%=txtDisplayOrder.ClientID %>').val(entity.DisplayOrder);
        $('#<%=txtTaskDate.ClientID %>').val(entity.TaskDate);
        $('#<%=txtStartDate.ClientID %>').val(entity.StartDate);
        $('#<%=txtEndDate.ClientID %>').val(entity.EndDate);
        $('#<%=txtStartTime.ClientID %>').val(entity.StartTime);
        $('#<%=txtEndTime.ClientID %>').val(entity.EndTime);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

        $('#entryDetailContainerPopup').show();
    });

    $('.txtMark').die('change');
    $('.txtMark').live('change', function () {
        if ($('.chkIsSetPassingGrade input').is(':checked')) {
            var passingGrade = parseFloat($('#<%=txtPassingGrade.ClientID %>').val());
            var value = parseFloat($(this).val());
            var finalMark = parseFloat($(this).closest('tr').find('.txtFinalMark').val());
            if (value > passingGrade) 
                $(this).closest('tr').find('.txtFinalMark').val(passingGrade);
            else if (finalMark < value)
                $(this).closest('tr').find('.txtFinalMark').val(value);
        }
    });

    $('.chkIsSetPassingGrade').die('change');
    $('.chkIsSetPassingGrade').live('change', function () {
        if ($(this).find('input').is(':checked')) {
            var passingGrade = parseFloat($('#<%=txtPassingGrade.ClientID %>').val());
            $('.trRemedialDt').each(function () {
                var isSetPassingGrade = false;
                var maxValue = parseFloat($(this).find('.txtOriginalMark').val());
                $(this).find('.txtMark').each(function () {
                    if ($(this).val() != '-') {
                        var value = parseFloat($(this).val());
                        if (value >= passingGrade)
                            isSetPassingGrade = true;
                        if (value > maxValue)
                            maxValue = value;
                    }
                });
                if (isSetPassingGrade)
                    $(this).find('.txtFinalMark').val(passingGrade);
                else
                    $(this).find('.txtFinalMark').val(maxValue);
            });
        }
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#entryDetailContainerPopup').hide();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        if (param[0] == 'savemark') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                cbpMeetingDetail.PerformCallback('refresh');
                pcRightPanelContent.Hide();
            }            
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnSchoolClassID" value="" runat="server" />
    <input type="hidden" id="hdnSaveValue" value="" runat="server" />
    <input type="hidden" id="hdnListStudentID" value="" runat="server" />
    <input type="hidden" id="hdnListRemedialID" value="" runat="server" />
    <input type="hidden" id="hdnPassingGrade" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("KKM")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtPassingGrade" ReadOnly="true" Width="80px" CssClass="number" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:160px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Urutan")%></label></td>
                        <td><asp:TextBox ID="txtDisplayOrder" CssClass="number" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Tugas")%></label></td>
                        <td><asp:TextBox ID="txtTaskDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Mulai")%></label></td>
                        <td>    
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:145px" />
                                    <col style="width:5px" />
                                    <col style="width:80px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                                    <td align="center"></td>
                                    <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal / Jam Selesai")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:145px" />
                                    <col style="width:5px" />
                                    <col style="width:80px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                                    <td align="center"></td>
                                    <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <asp:CheckBox runat="server" ID="chkFilterUnderPassingGrade" Checked="true" /> <%=GetLabel("Tampilkan Hanya Yang Di Bawah KKM") %>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ setStudentImage();hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <table rules="all" cellspacing="0" style="width:100%" class="grdBorder grdSelected grdStudent">
                        <tr>
                            <th rowspan="2"><%=GetLabel("Siswa") %></th>
                            <th colspan="10" class="thCenter"><%=GetLabel("NILAI") %></th>
                        </tr>
                        <tr>
                            <th class="thCenter" style="width:80px"><%=GetLabel("Nilai Awal") %></th>
                            <asp:Repeater ID="rptHeader" runat="server">
                                <ItemTemplate>
                                    <th class="thCenter" style="width:80px">
                                        <div class="divDetailDelete" style="float:right;"></div>
                                        <label class="lblLink lblRemedialDisplayOrder" style="margin-right: -15px;">R<%#Eval("DisplayOrder") %></label>
                                        <input type="hidden" value="<%#Eval("ClassSubjectTaskRemedialID") %>" bindingfield="ClassSubjectTaskRemedialID" />
                                        <input type="hidden" value="<%#Eval("DisplayOrder") %>" bindingfield="DisplayOrder" />
                                        <input type="hidden" value="<%#Eval("TaskDate", "{0:dd-MM-yyyy}") %>" bindingfield="TaskDate" />
                                        <input type="hidden" value="<%#Eval("StartDate", "{0:dd-MM-yyyy}") %>" bindingfield="StartDate" />
                                        <input type="hidden" value="<%#Eval("EndDate", "{0:dd-MM-yyyy}") %>" bindingfield="EndDate" />
                                        <input type="hidden" value="<%#Eval("StartTime") %>" bindingfield="StartTime" />
                                        <input type="hidden" value="<%#Eval("EndTime") %>" bindingfield="EndTime" />
                                        <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <th class="thCenter" style="width:80px">
                                <%=GetLabel("Nilai Akhir") %><br />
                                <asp:CheckBox ID="chkIsSetPassingGrade" CssClass="chkIsSetPassingGrade" runat="server" /> <%=GetLabel("= KKM") %>
                            </th>
                        </tr>
                        <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                            <ItemTemplate>
                                <tr class="trRemedialDt">
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
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center"><asp:TextBox ID="txtOriginalMark" CssClass="number txtOriginalMark" Text="-" Width="100%" runat="server" /></td>
                                    <asp:Repeater ID="rptClassSubjectRemedial" runat="server" OnItemDataBound="rptClassSubjectRemedial_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="center">
                                                <asp:TextBox ID="txtMark" CssClass="number txtMark" Text="-" Width="100%" runat="server" />
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td align="center"><asp:TextBox ID="txtFinalMark" CssClass="number txtFinalMark" Text="-" Width="100%" runat="server" /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <br />
    <input type="button" id="btnSaveRemedialMark" value='<%=GetLabel("Simpan") %>' />
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>
