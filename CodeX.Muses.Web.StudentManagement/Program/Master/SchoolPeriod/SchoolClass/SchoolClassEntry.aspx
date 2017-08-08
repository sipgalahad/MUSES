<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SchoolClassEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SchoolClassEntry" %>

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
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtSchoolClassCode.ClientID %>').val('');
                $('#<%=txtSchoolClassName.ClientID %>').val('');
                tacRoom.setValue('');
                tacRoom.setText('');
                tacTeacher.setValue('');
                tacTeacher.setText('');
                $('#<%=txtMaxStudent.ClientID %>').val($('#<%=hdnMaxStudent.ClientID %>').val());
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region edit and delete
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.SchoolClassID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.SchoolClassID);
            $('#<%=txtSchoolClassCode.ClientID %>').val(entity.SchoolClassCode);
            $('#<%=txtSchoolClassName.ClientID %>').val(entity.SchoolClassName);
            tacRoom.setValue(entity.RoomID);
            tacRoom.setText(entity.RoomName);
            tacTeacher.setValue(entity.TeacherID);
            tacTeacher.setText(entity.TeacherName);
            $('#<%=txtMaxStudent.ClientID %>').val(entity.MaxStudent);
            $('#entryDetailContainer').show();
        });

        //#endregion

        //#region Room
        function onGetRoomFilterExpression() {
            var filterExpression = "<%=OnGetRoomFilterExpression() %>";
            return filterExpression;
        }

        function onTacRoomButtonSearchClick() {
            openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
                var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
                Methods.getObject('GetRoomList', filterExpression, function (result) {
                    if (result != null) {
                        tacRoom.setValue(result.RoomID);
                        tacRoom.setText(result.RoomName);
                    }
                    else {
                        tacRoom.setValue('');
                        tacRoom.setText('');
                    }
                });
            });

        }

        function onTacRoomValueChanged() {
        }
        //#endregion

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

        function onCboClassTypeValueChanged(s) {
            $('#btnCancel').click();
            cbpView.PerformCallback('refresh');
        }

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
    </script>
    <input type="hidden" id="hdnMaxStudent" value="" runat="server" />
    <input type="hidden" id="hdnSiteID" value="" runat="server" />
    <table>
        <colgroup>
            <col style="width: 150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><%=GetLabel("Tipe Kelas") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboClassType" ClientInstanceName="cboClassType" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboClassTypeValueChanged(s); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
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
                                    <td><asp:TextBox ID="txtSchoolClassCode" Width="100px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label><%=GetLabel("Nama")%></label></td>
                                    <td><asp:TextBox ID="txtSchoolClassName" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Ruangan")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacRoom" ClientInstanceName="tacRoom" MethodName="GetRoomList" GetFilterExpressionFunction="onGetRoomFilterExpression"
                                            SearchFields="RoomName,RoomID" TextField="RoomName" ValueField="RoomID" SearchText="${RoomName} (<b>${RoomID}</b>)" OrderByExpression="RoomName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacRoomButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacRoomValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Wali Kelas")%></label></td>
                                    <td>
                                        <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacTeacher" ClientInstanceName="tacTeacher" MethodName="GetvTeacherList" GetFilterExpressionFunction="onGetTeacherFilterExpression"
                                            SearchFields="TeacherName,TeacherCode" TextField="TeacherName" ValueField="TeacherID" SearchText="${TeacherName} (<b>${TeacherCode}</b>)" OrderByExpression="TeacherName">
                                            <ClientSideEvents ButtonSearchClick="function(){ onTacTeacherButtonSearchClick(); }"
                                                ValueChanged="function(){ onTacTeacherValueChanged(); }" />
                                        </cdx:CodeXAutoCompleteTextBox>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kapasitas Siswa")%></label></td>
                                    <td><asp:TextBox ID="txtMaxStudent" CssClass="number" Width="120px" runat="server" /></td>
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
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="SchoolClassID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="SchoolClassCode" HeaderText="Kode Kelas" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="SchoolClassName" HeaderText="Nama Kelas"/>
                                <asp:BoundField DataField="RoomName" HeaderText="Ruangan" HeaderStyle-Width="230px" />
                                <asp:BoundField DataField="TeacherName" HeaderText="Wali Kelas" HeaderStyle-Width="300px" />
                                <asp:BoundField DataField="MaxStudent" HeaderText="Kapasitas Siswa" HeaderStyle-Width="150px" HeaderStyle-CssClass="thRight" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("SchoolClassID") %>" bindingfield="SchoolClassID" />
                                        <input type="hidden" value="<%#Eval("SchoolClassCode") %>" bindingfield="SchoolClassCode" />
                                        <input type="hidden" value="<%#Eval("SchoolClassName") %>" bindingfield="SchoolClassName" />
                                        <input type="hidden" value="<%#Eval("RoomID") %>" bindingfield="RoomID" />
                                        <input type="hidden" value="<%#Eval("RoomName") %>" bindingfield="RoomName" />
                                        <input type="hidden" value="<%#Eval("TeacherID") %>" bindingfield="TeacherID" />
                                        <input type="hidden" value="<%#Eval("TeacherName") %>" bindingfield="TeacherName" />
                                        <input type="hidden" value="<%#Eval("MaxStudent") %>" bindingfield="MaxStudent" />
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
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>