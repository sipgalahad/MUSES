<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPFrame.master" AutoEventWireup="true" 
    CodeBehind="ProspectiveStudentRemarksDtEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentRemarksDtEntry" %>

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

            $('#btnNext').click(function () {
                if (IsValid(null, 'fsMPEntry', 'mpEntry'))
                    cbpMPEntryProcess.PerformCallback('save');
            });

            $('#btnPrev').click(function () {
                parent.OnPrevButtonClick();
            });
        });

        function onAfterSaveSuccess(retval) {
            parent.OnNextButtonClick();
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
                        <h4 class="h4expanded"><%=GetLabel("Keterangan Lain")%></h4>
                        <div class="containerTblEntryContent">
                            <table class="tblEntryContent" style="width:100%">
                                <colgroup>
                                    <col style="width:180px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Golongan Darah")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboBloodType" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Bahasa Sehari-hari")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboLanguage" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jarak Rumah Ke Sekolah")%></label></td>
                                    <td><asp:TextBox ID="txtHomeDistance" Width="80px" CssClass="number" runat="server" /> [km]</td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Riwayat Penyakit") %></label></td>
                                    <td><asp:TextBox runat="server" ID="txtMedicalHistory" TextMode="MultiLine" Rows="2" Width="300px" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <input type="button" id="btnNext" value="Next" style="float:right" />
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