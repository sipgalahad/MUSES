<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentSurveyDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentSurveyDtEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPFrame" runat="server">
    <script type="text/javascript">
        $(function () {
            registerCollapseExpandHandler();

            $('#btnFinish').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry'))
                    cbpMPEntryProcess.PerformCallback('save');
            });

            $('#btnPrev').click(function () {
                parent.OnPrevButtonClick();
            });
        });

        function onAfterSaveSuccess(retval) {
            parent.OnFinishButtonClick();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" />
    <div style="height: 410px; overflow-y:auto">
        <fieldset id="fsMPEntry">            
            <table class="tblContentArea" >
                <colgroup>
                    <col style="width:50%"/>
                </colgroup>
                <tr>
                    <td style="padding:5px;vertical-align:top" rowspan="2">
                        <h4 class="h4expanded"><%=GetLabel("Survei")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Sumber Informasi")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboInformationSource" Width="120px" runat="server" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <input type="button" id="btnFinish" value="Finish" style="float:right" />
    <input type="button" id="btnPrev" value="Prev" />

    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpMPEntryProcess" runat="server" Width="100%" ClientInstanceName="cbpMPEntryProcess"
            ShowLoadingPanel="false" OnCallback="cbpMPEntryProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){
                showLoadingPanel();
            }" EndCallback="function(s,e){
                var result = s.cpResult.split('|');
                if(result[0] == 'save'){
                    if(result[1] == 'success'){
                        onAfterSaveSuccess(result[2]);
                    }
                    else
                        if(result[2] != '')
                            showToast('Save Failed', 'Error Message : ' + result[2]);
                        else
                            showToast('Save Failed', '');
                }
                hideLoadingPanel();
            }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>