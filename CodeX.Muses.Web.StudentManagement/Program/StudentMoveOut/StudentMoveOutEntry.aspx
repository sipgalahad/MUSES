<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentMoveOutEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentMoveOutEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtMoveOutDate.ClientID %>');
        }

        //#region Student
        window.onGetStudentFilterExpression = function () {
            var filterExpression = "<%=OnGetStudentFilterExpression() %>";
            return filterExpression;
        }

        function onTacStudentButtonSearchClick() {
            openSearchDialog('student', onGetStudentFilterExpression(), function (value) {
                var filterExpression = onGetStudentFilterExpression() + " AND StudentCode = '" + value + "'";
                Methods.getObject('GetStudentList', filterExpression, function (result) {
                    if (result != null) {
                        tacStudent.setValue(result.StudentID);
                        tacStudent.setText(result.StudentName);
                        entityToControlStudent(result.StudentID);
                    }
                    else {
                        tacStudent.setValue('');
                        tacStudent.setText('');
                    }
                });
            });

        }

        function onTacStudentValueChanged() {
            var id = tacStudent.getValue();
            if (id != '') {
                entityToControlStudent(id);
            }
        }

        function entityToControlStudent(id) {
            var filterExpression = "StudentID = " + id;
            Methods.getObject('GetStudentMoveOutList', filterExpression, function (result1) {
                if (result1 != null) 
                    onLoadObject(id);
            });
        }
        //#endregion

        function onCboGCMoveOutReasonChanged() {
            if (cboGCMoveOutReason.GetValue() == '<%=OnGetStudentMoveOutReasonOther() %>')
                $('#<%=txtMoveOutReason.ClientID %>').show();
            else
                $('#<%=txtMoveOutReason.ClientID %>').hide();
        }
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <table>
        <colgroup>
            <col style="width: 180px" />
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Siswa")%></label></td>
            <td colspan="2">
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacStudent" ClientInstanceName="tacStudent" MethodName="GetStudentList" GetFilterExpressionFunction="onGetStudentFilterExpression"
                    SearchFields="StudentName,StudentCode" TextField="StudentName" ValueField="StudentID" SearchText="${StudentName} (<b>${StudentCode}</b>)" OrderByExpression="StudentName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacStudentButtonSearchClick(); }"
                        ValueChanged="function(){ onTacStudentValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal Efektif") %></label></td>
            <td colspan="2"><asp:TextBox runat="server" ID="txtMoveOutDate" CssClass="datepicker" Width="120px" /></td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Alasan") %></label></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboGCMoveOutReason" ClientInstanceName="cboGCMoveOutReason" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){onCboGCMoveOutReasonChanged()}" />
                </dxe:ASPxComboBox>
            </td>
            <td><asp:TextBox runat="server" ID="txtMoveOutReason" Width="200px" Style="display:none" /></td>
        </tr>
        <tr>
            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Catatan")%></label></td>
            <td colspan="2"><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="400px" /></td>
        </tr>
    </table>
</asp:Content>