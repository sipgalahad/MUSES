<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyRProjectTaskDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ProjectManagement.Program.MyRProjectTaskDtEntryCtl" %>

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
        $('#btnSavePopup').click(function (evt) {
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
                cbpViewPopup.PerformCallback('refresh');
            }
        }
    }

    //#region Log
    $(function () {
        setDatePicker('<%=txtLogDate.ClientID %>');
        $('#divTransactionAddPopup2').click(function () {
            $('#<%=hdnEntry2ID.ClientID %>').val('');
            var currentDate = new Date();
            var h = currentDate.getHours();
            var mnt = currentDate.getMinutes();
            var d = currentDate.getDate();
            var m = currentDate.getMonth() + 1;
            var y = currentDate.getFullYear();
            $('#<%=txtLogDate.ClientID %>').val('' + (d <= 9 ? '0' + d : d) + '-' + (m <= 9 ? '0' + m : m) + '-' + y);
            $('#<%=txtLogTime.ClientID %>').val('' + (h <= 9 ? '0' + h : h) + ':' + (mnt <= 9 ? '0' + mnt : mnt));
            $('#<%=txtLogText.ClientID %>').val('');

            $('#entryDetailContainerPopup2').show();
        });

        $('#btnCancelPopup2').click(function () {
            $('#entryDetailContainerPopup2').hide();
        });

        $('#btnSavePopup2').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup2', 'mpTrxPopup')) {
                cbpProcessPopup2.PerformCallback('save');
            }
        });

        setTimeout(function () {
            setDdeFilterStatusText();
            cbpViewPopup.PerformCallback('refresh');
        }, 500);
    });

    $('#<%=grdView2.ClientID %> .divDetailDelete').die('click');
    $('#<%=grdView2.ClientID %> .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                $('#<%=hdnEntry2ID.ClientID %>').val(entity.ProjectTaskLogID);
                cbpProcessPopup2.PerformCallback('delete');
            }
        });
    });

    $('#<%=grdView2.ClientID %> .divDetailEdit').die('click');
    $('#<%=grdView2.ClientID %> .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntry2ID.ClientID %>').val(entity.ProjectTaskLogID);
        $('#<%=txtLogText.ClientID %>').val(entity.LogText);
        $('#<%=txtLogDate.ClientID %>').val(entity.LogDate);
        $('#<%=txtLogTime.ClientID %>').val(entity.LogTime);

        $('#entryDetailContainerPopup2').show();
    });

    function onCbpProcesPopup2EndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup2').click();
                cbpViewPopup2.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup2.PerformCallback('refresh');
        }
    }
    //#endregion

    $('.lblDownload').die('click');
    $('.lblDownload').live('click', function () {
        document.location = $(this).closest('tr').find('.hdnDownloadedFile').val();
    });

    //#region Upload
    $(function () {
        $('#divTransactionAddPopup3').click(function () {
            $('#<%=hdnEntry3ID.ClientID %>').val('');
            $('#FileUpload').val('');
            $('#<%=hdnUploadedFile.ClientID %>').val('');
            $('#<%=txtFileName.ClientID %>').val('');
            $('#<%=txtFileRemarks.ClientID %>').val(''); 
            $('#entryDetailContainerPopup3').show();
        });

        $('#btnCancelPopup3').click(function () {
            $('#entryDetailContainerPopup3').hide();
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

        $('#btnSavePopup3').click(function () {
            cbpProcessPopup3.PerformCallback('save');
        });
    });

    function onCbpProcesPopup3EndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup3').click();
                $('#FileUpload').val('');
                $('#<%=hdnUploadedFile.ClientID %>').val('');
                cbpViewPopup3.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup3.PerformCallback('refresh');
        }
    }
    //#endregion
</script>

<style type="text/css">
    .tr003 td, .nts003      { background-color: #40CF4E; }
    .tr002 td, .nts002      { background-color: #40A7CF; }
    .tr001 td, .nts001      { background-color: #EB6A7D; }
    
    .grdTask .selected      { border: 1px solid Red; }
    .grdTask .selected td   { border-top: 1px solid Red; border-bottom: 1px solid Red; }
</style>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnProjectOrganizationID" value="" runat="server" />
    <input type="hidden" id="hdnPosition" value="" runat="server" />
    <input type="hidden" id="hdnProjectTaskID" value="" runat="server" />
    <input type="hidden" id="hdnProjectID" value="" runat="server" />
    <input type="hidden" id="hdnIsVerifiedTask" value="" runat="server" />
    <script id="tmplEntityDt" type="text/x-jquery-tmpl">
        <tr class="trOrganizationDt">
            <td>&nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div id="Organization${idx}" class="tacOrganization">
                                <div>
                                    <div class="containerAutoComplete">
                                        <input type="hidden" class="hdnAutoCompleteValue"/>
                                        <input type="hidden" class="hdnAutoCompleteText"/>
                                        <input type="hidden" class="hdnIsRequired" value="1"/>
                                        <input type="hidden" class="hdnValidationGroup" value="mpDrugsQuickPicks"/>
                                        <input type="text" class="required txtAutoComplete" validationgroup="mpTrxPopup" style="width:145px"/>
                                        <input type="button" class="btnAutoCompleteSearchMore btnSearch"/>
                                        <div class="divListAutoCompleteResultBox">
                                            <div class="divListAutoCompleteResult">
                                            </div>
                                        </div>
                                    </div>
                                    <script class="tmpltAutoComplete" type="text/x-jquery-tmpl">
                                        <div>
                                            ${ProjectOrganizationName}
                                            <input type='hidden' value='${ProjectOrganizationName}' class='hdnAutoCompleteRowText'/>
                                            <input type='hidden' value='${ProjectOrganizationID}' class='hdnAutoCompleteRowValue'/>
                                        </div>
                                    </script1>
                                </div>
                            </div>
                        </td>
                        <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
                    </tr>
                </table>
            </td>
        </tr>
    </script>
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tugas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Posisi")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtPosition" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Status")%></label></td>
            <td><dxe:ASPxComboBox runat="server" ID="cboStatus" ClientInstanceName="cboStatus" Width="200px" /></td>
        </tr>
        <tr id="trIsVerified" runat="server">
            <td>&nbsp;</td>
            <td><asp:CheckBox ID="chkIsVerified" runat="server" /><%=GetLabel("Verified") %></td>
        </tr>
        <tr id="trSaveEntryPopup">
            <td> 
                <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
            </td>
        </tr>
    </table>
    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
            ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
    
    <table style="width:100%">
        <tr>
            <td style="width:50%; vertical-align: top" >
                <h4><%=GetLabel("Log") %></h4>
                 <div class="containerTblEntryContent">
                     <div class="divTransactionEntry" id="divTransactionEntry2">   
                        <span id="divTransactionAddPopup2" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                        <div id="entryDetailContainerPopup2" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrxPopup2" style="margin:0"> 
                                <input type="hidden" id="hdnEntry2ID" runat="server" value="" />
                                <table id="tblEntry">
                                    <colgroup>
                                        <col style="width:150px"/>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tanggal")%></label></td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td><asp:TextBox ID="txtLogDate" Width="120px" runat="server" CssClass="datepicker" /></td>
                                                    <td style="width:10px; text-align:center">&nbsp;</td>
                                                    <td><asp:TextBox ID="txtLogTime" CssClass="thCenter" Width="70px" runat="server"/></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr valign="top" style="padding-top: 5px">
                                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                        <td><asp:TextBox runat="server" ID="txtLogText" TextMode="MultiLine" Rows="3" Width="300px" /></td>
                                    </tr>
                                    <tr id="trSaveEntry">
                                        <td></td>
                                        <td> 
                                            <input type="button" id="btnSavePopup2" class="btnWhite" value="Commit"/>
                                            <input type="button" id="btnCancelPopup2" class="btnWhite" value="Cancel"/>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <dxcp:ASPxCallbackPanel ID="cbpViewPopup2" runat="server" Width="100%" ClientInstanceName="cbpViewPopup2"
                        ShowLoadingPanel="false" OnCallback="cbpViewPopup2_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="Panel1" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView2" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectTaskLogID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:BoundField DataField="LogDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Tanggal" HeaderStyle-Width="120px" />
                                            <asp:BoundField DataField="LogTime" HeaderText="Waktu" HeaderStyle-Width="70px" />
                                            <asp:BoundField DataField="LogText" HeaderText="Keterangan" />
                                            <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="150px" />
                                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="divDetailDelete" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;'" %>></div>
                                                    <div class="divDetailEdit" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;margin-right:10px;'" %>><%=GetLabel("Edit")%></div>
                                                    <input type="hidden" value="<%#Eval("ProjectTaskLogID") %>" bindingfield="ProjectTaskLogID" />
                                                    <input type="hidden" value="<%#Eval("LogDate", "{0:dd-MM-yyyy}") %>" bindingfield="LogDate" />
                                                    <input type="hidden" value="<%#Eval("LogTime") %>" bindingfield="LogTime" />
                                                    <input type="hidden" value="<%#Eval("LogText") %>" bindingfield="LogText" />
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
                    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup2" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup2"
                        ShowLoadingPanel="false" OnCallback="cbpProcessPopup2_Callback">
                        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopup2EndCallback(s); }" />
                    </dxcp:ASPxCallbackPanel>
                </div>
            </td>
            <td style="vertical-align: top">
                 <h4><%=GetLabel("File") %></h4>
                 <div class="containerTblEntryContent">
                     <div class="divTransactionEntry" id="divTransactionEntry3"> 
                        <span id="divTransactionAddPopup3" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
                        <div id="entryDetailContainerPopup3" class="entryDetailContainer" style="display: none">
                            <fieldset id="fsTrxPopup3" style="margin:0"> 
                                <input type="hidden" id="hdnEntry3ID" runat="server" value="" />
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
                                            <input type="button" id="btnSavePopup3" class="btnWhite" value="Commit"/>
                                            <input type="button" id="btnCancelPopup3" class="btnWhite" value="Cancel"/>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <dxcp:ASPxCallbackPanel ID="cbpViewPopup3" runat="server" Width="100%" ClientInstanceName="cbpViewPopup3"
                        ShowLoadingPanel="false" OnCallback="cbpViewPopup3_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                                <asp:Panel runat="server" ID="Panel2" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView3" runat="server" CssClass="tblTransactionEntryResult" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView3_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectTaskFileID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField HeaderText="Nama" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <label class="lblDownload lblLink"><%#Eval("FileName") %></label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Keterangan" />
                                            <asp:BoundField DataField="CreatedByName" HeaderText="Pembuat" HeaderStyle-Width="180px" />
                                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="divDetailDelete" <%#Eval("CreatedBy").ToString() != OnGetUserID() ? "style='display:none'" : "style='float:right;'" %>></div>
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
                    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup3" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup3"
                        ShowLoadingPanel="false" OnCallback="cbpProcessPopup3_Callback">
                        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopup3EndCallback(s); }" />
                    </dxcp:ASPxCallbackPanel>
                </div>
            </td>
        </tr>
    </table>
</div>

