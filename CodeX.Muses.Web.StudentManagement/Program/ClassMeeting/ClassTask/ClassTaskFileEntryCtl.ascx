<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassTaskFileEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskFileEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    //#region Upload
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#FileUpload').val('');
            $('#<%=hdnUploadedFile.ClientID %>').val('');
            $('#<%=txtFileName.ClientID %>').val('');
            $('#<%=txtFileRemarks.ClientID %>').val('');
            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

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
        });

        $('#btnSavePopup').click(function () {
            cbpProcessPopup.PerformCallback('save');
        });
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                $('#FileUpload').val('');
                $('#<%=hdnUploadedFile.ClientID %>').val('');
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }
    //#endregion

    $('.lblDownload').die('click');
    $('.lblDownload').live('click', function () {
        window.open($(this).closest('tr').find('.hdnDownloadedFile').val(), '_blank');
        //document.location = $(this).closest('tr').find('.hdnDownloadedFile').val();
    });
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr>
    </table>
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width:150px"/>
                        <col />
                    </colgroup>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <input type="hidden" id="hdnUploadedFile" runat="server" value="" />
                            <input type="file" id="FileUpload" name="FileUpload" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama File") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtFileName" Width="300px" /></td>
                    </tr>
                    <tr valign="top" style="padding-top: 5px">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                        <td><asp:TextBox runat="server" ID="txtFileRemarks" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent3" runat="server">
                <asp:Panel runat="server" ID="Panel2" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ClassSubjectTaskFileID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:TemplateField HeaderText="Nama" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <label class="lblDownload lblLink"><%#Eval("FileName") %></label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                            <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="120px" />
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div class="divDetailDelete" style='float:right;'></div>
                                    <input type="hidden" id="hdnDownloadedFile" runat="server" class="hdnDownloadedFile" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>
