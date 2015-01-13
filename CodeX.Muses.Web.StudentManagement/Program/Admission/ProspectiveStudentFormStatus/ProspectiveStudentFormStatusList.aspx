<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPPeriodAdmissionPageTrx.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentFormStatusList.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentFormStatusList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        //#region Registration
        function onGetRegistrationFilterExpression() {
            var filterExpression = "<%=OnGetRegistrationFilterExpression() %>";
            return filterExpression;
        }

        function onTacRegistrationButtonSearchClick() {
            openSearchDialog('registration', onGetRegistrationFilterExpression(), function (value) {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    if (result != null) {
                        tacRegistration.setValue(result.RegistrationID);
                        tacRegistration.setText(result.ProspectiveStudentName);
                        entityToControlRegistration(result);
                    }
                    else {
                        tacRegistration.setValue('');
                        tacRegistration.setText('');
                        entityToControlRegistration(result);
                    }
                });
            });
        }

        function onTacRegistrationValueChanged() {
            var id = tacRegistration.getValue();
            if (id != '') {
                var filterExpression = onGetRegistrationFilterExpression() + " AND RegistrationNo = '" + value + "'";
                Methods.getObject('GetvRegistrationList', filterExpression, function (result) {
                    entityToControlRegistration(result);
                });
            }
        }

        function entityToControlRegistration(result) {
            if (result != null)
                $('#<%=hdnProspectiveStudentID.ClientID %>').val(result.ProspectiveStudentID);
            else
                $('#<%=hdnProspectiveStudentID.ClientID %>').val('0');
            cbpView.PerformCallback('refresh');
        }
        //#endregion

        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var param = "";
                $('.chkIsExist input:checked').each(function () {
                    var id = $(this).closest('tr').find('.keyField').html();
                    $isComplete = $(this).closest('tr').find('.chkIsCompleted').find('input');
                    var remarks = $(this).closest('tr').find('.txtRemarks').val();
                    
                    if (param != '') {
                        param += '|';
                    }
                    param += id + ',';
                    if ($isComplete.is(':checked'))
                        param += '1';
                    else
                        param += '0';
                    param += ',' + remarks;

                });
                $('#<%=hdnSelectedValue.ClientID %>').val(param);
                cbpProcess.PerformCallback('save');
            })
            
            $('.chkIsExist input').each(function () {
                $(this).change();
            });
        });
        
        $('.chkIsExist input').live('change', function () {
            $chkHdID = $(this).closest('tr').find('.chkIsCompleted').find('input');
            if ($(this).is(':checked')) {
                $chkHdID.removeAttr("disabled");
            }
            else {
                $chkHdID.attr("disabled", true);
                $chkHdID.prop('checked', false);
            }
        });
        
        function onCbpProcessEndCallback(s) {
            $('#<%=hdnID.ClientID %>').val('');
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
            else {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }

            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server"/>
    <input type="hidden" id="hdnSelectedValue" runat="server"/>
    <div class="divTransactionEntry">
        <table>
            <colgroup>
                <col style="width:120px"/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Calon Siswa")%></label></td>
                <td>
                    <input type="hidden" id="hdnProspectiveStudentID" runat="server"/>
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRegistration" ClientInstanceName="tacRegistration" MethodName="GetvRegistrationList" GetFilterExpressionFunction="onGetRegistrationFilterExpression"
                        SearchFields="ProspectiveStudentName,RegistrationNo" TextField="ProspectiveStudentName" ValueField="RegistrationID" SearchText="${ProspectiveStudentName} (<b>${RegistrationNo}</b>)" OrderByExpression="ProspectiveStudentName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacRegistrationButtonSearchClick(); }"
                            ValueChanged="function(){ onTacRegistrationValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" 
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="FormID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="FormCode" HeaderText="Kode" HeaderStyle-Width="180px" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="FormName" HeaderText="Formulir"/>
                                <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Ada">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsExist" CssClass="chkIsExist" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Terisi">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsCompleted" CssClass="chkIsCompleted" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderText="Catatan">                                               
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" CssClass="txtRemarks" Width="120px" runat="server" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>