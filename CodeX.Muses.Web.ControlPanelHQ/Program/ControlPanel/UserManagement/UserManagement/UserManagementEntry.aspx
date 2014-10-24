<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="UserManagementEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.UserManagementEntry" %>

<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function checkConfirmPassword(value, element) {
            return value == $('#<%=txtPassword.ClientID %>').val();
        }
        function checkConfirmMobilePIN(value, element) {
            return value == $('#<%=txtMobilePIN.ClientID %>').val();
        }

        jQuery.validator.addMethod("confirmpassword", checkConfirmPassword, "");
        jQuery.validator.addMethod("confirmmobilepin", checkConfirmMobilePIN, "");

        function onLoad() {
        }

        //#region Teacher
        function onGetTeacherFilterExpression() {
            var filterExpression = "<%=OnGetTeacherFilterExpression() %>";
            return filterExpression;
        }

        function onTacTeacherButtonSearchClick() {
            openSearchDialog('teacher', onGetTeacherFilterExpression(), function (value) {
                var filterExpression = onGetTeacherFilterExpression() + " AND TeacherCode = '" + value + "'";
                Methods.getObject('GetTeacherList', filterExpression, function (result) {
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
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:0 5px;vertical-align:top">
                <h4><%=GetLabel("General Information")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("User Name")%></label></td>
                        <td><asp:TextBox ID="txtUserName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Full Name")%></label></td>
                        <td><asp:TextBox ID="txtFullName" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Email")%></label></td>
                        <td><asp:TextBox ID="txtEmail" Width="100%" CssClass="email" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Password")%></label></td>
                        <td><asp:TextBox ID="txtPassword" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Confirm Password")%></label></td>
                        <td><asp:TextBox ID="txtConfirmPassword" CssClass="confirmpassword" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile PIN")%></label></td>
                        <td><asp:TextBox ID="txtMobilePIN" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Confirm Mobile PIN")%></label></td>
                        <td><asp:TextBox ID="txtConfirmMobilePIN" CssClass="confirmmobilepin" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Security Question")%></label></td>
                        <td><asp:TextBox ID="txtSecurityQuestion" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Security Answer")%></label></td>
                        <td><asp:TextBox ID="txtSecurityAnswer" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Guru")%></label></td>
                        <td>
                            <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                                SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                                <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                                    ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                            </cdx:CodeXAutoCompleteTextBox>   
                        </td>
                    </tr>
                </table>
            </td>
            <td style="padding:0 5px;vertical-align:top">
                <asp:Panel ID="pnlCustomAttribute" runat="server">
                    <h4><%=GetLabel("Custom Attribute")%></h4>
                    <asp:Repeater ID="rptCustomAttribute" runat="server">
                        <HeaderTemplate>
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:30%"/>
                                </colgroup>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%# Eval("Value") %></label></td>
                                <td>
                                    <input type="hidden" value='<%# Eval("Code") %>' runat="server" id="hdnTagFieldCode" />
                                    <asp:TextBox ID="txtTagField" Width="300px" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
