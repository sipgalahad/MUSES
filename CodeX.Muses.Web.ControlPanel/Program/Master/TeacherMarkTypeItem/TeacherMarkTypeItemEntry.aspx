<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="TeacherMarkTypeItemEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.TeacherMarkTypeItemEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region TeacherMarkTypeGroup
        function onGetTeacherMarkTypeGroupFilterExpression() {
            var filterExpression = "<%=OnGetTeacherMarkTypeGroupFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherMarkTypeGroupButtonSearchClick() {
            openSearchDialog('teachermarktypegroup', onGetTeacherMarkTypeGroupFilterExpression(), function (value) {
                var filterExpression = onGetTeacherMarkTypeGroupFilterExpression() + " AND TeacherMarkTypeGroupID = " + value;
                Methods.getObject('GetvTeacherMarkTypeGroupList', filterExpression, function (result) {
                    if (result != null) {
                        tacTeacherMarkTypeGroup.setValue(result.TeacherMarkTypeGroupID);
                        tacTeacherMarkTypeGroup.setText(result.TeacherMarkTypeGroupName);
                    }
                    else {
                        tacTeacherMarkTypeGroup.setValue('');
                        tacTeacherMarkTypeGroup.setText('');
                    }
                });
            });
        }
        //#endregion
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnStartDate" runat="server" value="" />
    <input type="hidden" id="hdnEndDate" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col width="50%"/>
            <col width="50%"/>
        </colgroup>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Dimensi")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacherMarkTypeGroup" ClientInstanceName="tacTeacherMarkTypeGroup" MethodName="GetvTeacherMarkTypeGroupList" GetFilterExpressionFunction="onGetTeacherMarkTypeGroupFilterExpression"
                                SearchFields="TeacherMarkTypeGroupName" TextField="TeacherMarkTypeGroupName" ValueField="TeacherMarkTypeGroupID" SearchText="${TeacherMarkTypeGroupName}" OrderByExpression="TeacherMarkTypeGroupName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherMarkTypeGroupButtonSearchClick(); }" />
                            </cdx:CodeXAutoCompleteTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Sub Dimensi")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtTeacherMarkTypeItemName" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nilai Akhir")%></label></td>
                        <td><asp:TextBox runat="server" ID="txtFinalMark" CssClass="number" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
