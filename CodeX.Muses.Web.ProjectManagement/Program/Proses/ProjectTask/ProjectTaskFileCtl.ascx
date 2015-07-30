<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTaskFileCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.ProjectTaskFileCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#FileUpload').change(function (evt) {
            var files = evt.target.files;
            var temp = {};
            var tempArr = [];
            temp["ListData"] = tempArr;

            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var reader = new FileReader();

                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (evt) {
                        var arr = {};
                        arr['filename'] = theFile.name;
                        arr['data'] = [];
                        var text = evt.target.result;
                        for (var s = 0; s < text.length; s++) {
                            arr['data'].push(text.charCodeAt(s));
                        }
                        tempArr.push(arr);
                        var json = JSON.stringify(temp);
                        $('#<%=hdnUploadedFile.ClientID %>').val(json);
                    };
                })(file);
                reader.readAsBinaryString(file);
            }
        })
    });

    $('#btnUploadFile').die('click');
    $('#btnUploadFile').live('click', function () {
        cbpProcessPopup.PerformCallback('upload');
    });

    $('.lblDownload').die('click');
    $('.lblDownload').live('click', function () {
        var id = $(this).closest('tr').find('.keyField').html();
        $('#<%=hdnEntryID.ClientID %>').val(id);
        $('#<%=btnExport.ClientID%>').click();
    });

    $('.divDetailPopupDelete').die('click');
    $('.divDetailPopupDelete').live('click', function () {
        var id = $(this).closest('tr').find('.keyField').html();
        $('#<%=hdnEntryID.ClientID %>').val(id);
        cbpProcessPopup.PerformCallback('delete');
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();
        var param = s.cpResult.split('|');
        if (param[0] == 'upload') {
            if (param[1] == 'fail')
                showToast('Upload Failed', 'Error Message : ' + param[2]);
            else {
                $('#FileUpload').val('');
                $('#<%=hdnUploadedFile.ClientID %>').val('');
                cbpViewPopup.PerformCallback('refresh');
            }
        } else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Upload Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }

</script>

<div style="overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnEntryID" value="" runat="server" />
    <input type="hidden" id="hdnTeamDtID" value="" runat="server" />
    <input type="hidden" id="hdnEmployeeSave" value="" runat="server" />
    <div style="display:none;">
        <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
    </div>
    <table class="tblEntryContent" style="width:100%">
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtProjectTaskName" ReadOnly="true" Width="300px" runat="server" /></td>
        </tr> 
    </table>         
    <div class="divTransactionEntry">   
        <table style="width:100%">
            <colgroup>
                <col width="85px" />
                <col />
            </colgroup>
            <tr>
                <td></td>
                <td>
                    <input type="hidden" id="hdnUploadedFile" runat="server" value="" />
                    <input type="file" id="FileUpload" name="FileUpload" multiple />
                    <input type="button" id="btnUploadFile" value="Upload" />
                </td>
            </tr>
        </table>    
    </div>
    <div style="height:400px; overflow:auto;">
        <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
            ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ hideLoadingPanel(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                        <asp:GridView ID="grdPopupView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="FileID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="Filename" HeaderText="File"/>
                                <asp:BoundField DataField="Uploader" HeaderText="Uploader" HeaderStyle-Width="200px"/>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="lblLink lblDownload"><%=GetLabel("Download") %></label>
                                        <div style='float:right;' class="divDetailPopupDelete">X</div>
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
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

