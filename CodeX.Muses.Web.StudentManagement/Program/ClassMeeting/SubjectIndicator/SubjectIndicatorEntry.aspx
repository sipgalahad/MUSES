<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectIndicatorEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectIndicatorEntry" %>
    
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
            $('#divTransactionAdd').click(function () {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtSubjectCurriculumSyllabusName.ClientID %>').val('');
                $('#entryContainer').show();
            });

            $('#btnClose').click(function () {
                $('#entryContainer').hide();
            });

            $('#btnCommit').click(function () {
                if (IsValid(null, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        });

        //#region Edit
        $('#<%=grdView.ClientID %> .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectCurriculumSyllabusID);
            $('#<%=txtSubjectCurriculumSyllabusName.ClientID %>').val(entity.SubjectCurriculumSyllabusName);

            $('#entryContainer').show();
        });
        //#endregion

        //#region Delete
        $deletedTr = null;
        $('#<%=grdView.ClientID %> .divDetailDelete').live('click', function () {
            $deletedTr = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($deletedTr);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.SubjectCurriculumSyllabusID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });
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

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }
    </script>  
    <input type="hidden" id="hdnSubjectCurriculumID" runat="server" />
    <input type="hidden" id="hdnCurriculumSyllabusIndicatorID" runat="server" />
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Add Data")%></span><br />
        <div id="entryContainer" class="entryDetailContainer" style="display: none">
            <input type="hidden" value="" id="hdnEntryID" runat="server" />   
            <fieldset id="fsTrx" style="margin:0"> 
                <table style="width:100%">
                    <colgroup>
                        <col style="width:50%"/>
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table style="width:100%">
                                <colgroup>
                                    <col style="width:150px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Indikator")%></label></td>
                                    <td><asp:TextBox ID="txtSubjectCurriculumSyllabusName" runat="server" Width="300px" TextMode="MultiLine" Rows="2" />
                                    </td>
                                </tr>    
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnCommit" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnClose" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>        
        </div>                                  
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e) { hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="SubjectCurriculumSyllabusID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="SubjectCurriculumSyllabusName" HeaderText="Indikator" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="hidden" value="<%#Eval("SubjectCurriculumSyllabusID") %>" bindingfield="SubjectCurriculumSyllabusID" />
                                    <input type="hidden" value="<%#Eval("SubjectCurriculumSyllabusName") %>" bindingfield="SubjectCurriculumSyllabusName" />
                                    <div style="float:right;" class="divDetailDelete"></div>
                                    <div style="float:right;margin-right: 10px;" class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>