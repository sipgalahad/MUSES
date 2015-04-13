<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="TeacherAbsenceEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.TeacherAbsenceEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#<%=chkIsFullDay.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    $('#<%=txtStartTime.ClientID %>').val('');
                    $('#<%=txtEndTime.ClientID %>').val('');
                    $('#<%=txtStartTime.ClientID %>').attr('readonly', 'readonly');
                    $('#<%=txtEndTime.ClientID %>').attr('readonly', 'readonly');
                }
                else {
                    $('#<%=txtStartTime.ClientID %>').removeAttr('readonly');
                    $('#<%=txtEndTime.ClientID %>').removeAttr('readonly');
                }
            });

            $('#<%=chkIsFullDay.ClientID %>').change();

            setTimeout(function () {
                onCboAbsenceReasonValueChanged();
            }, 100);
        }

        function onCboAbsenceReasonValueChanged() {
            if (cboAbsenceReason.GetValue() != '<%=OnGetAbsenceReasonOther() %>') {
                $('#<%=txtOtherAbsenceReason.ClientID %>').hide();
                $('#<%=txtOtherAbsenceReason.ClientID %>').val('');
            }
            else
                $('#<%=txtOtherAbsenceReason.ClientID %>').show();
        }

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetvTeacherList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeacher.setValue(result.TeacherID);
                        tacTeacher.setText(result.TeacherName);
                    }
                    else {
                        tacTeacher.setValue('');
                        tacTeacher.setText('');
                    }
                });
            });

        }

        function onTacTeacherValueChanged() {
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSchoolPeriodID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetvTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                                SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:145px" />
                                    <col style="width:5px" />
                                    <col style="width:145px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>    
                                    <td align="center">-</td>
                                    <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Full Day")%></label></td>
                        <td><asp:CheckBox ID="chkIsFullDay" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Jam")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:80px" />
                                    <col style="width:5px" />
                                    <col style="width:80px" />
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>    
                                    <td align="center">-</td>
                                    <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Alasan")%></label></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:150px" />
                                    <col style="width:5px" />
                                    <col style="width:150px" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="cboAbsenceReason" ClientInstanceName="cboAbsenceReason" Width="150px" runat="server">
                                            <ClientSideEvents ValueChanged="function(s,e){ onCboAbsenceReasonValueChanged(); }" />
                                        </dxe:ASPxComboBox>
                                    </td>    
                                    <td align="center">&nbsp;</td>
                                    <td><asp:TextBox ID="txtOtherAbsenceReason" Width="150px" runat="server" /></td>
                                </tr>
                            </table>   
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
