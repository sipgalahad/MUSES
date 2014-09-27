<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MPEntryPopupCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Program.MPEntryPopupCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

    <input type="hidden" runat="server" id="hdnIsAdd" value="1" />
    <div style="padding:5px 0;">  
        <script type="text/javascript" id="dxss_mpentrypopupctl">
            window.setEntryPopupIsAdd = function (isAdd) {
                if (isAdd)
                    $('#<%=hdnIsAdd.ClientID %>').val('1');
                else
                    $('#<%=hdnIsAdd.ClientID %>').val('0');
            }
            window.getEntryPopupIsAdd = function () {
                return ($('#<%=hdnIsAdd.ClientID %>').val() == '1');
            }

            $(function () {
                $('#<%=btnMPEntryPopupSave.ClientID %>').show();
                /*if ($('#<%=hdnIsAdd.ClientID %>').val() == '1') {
                $('#btnMPEntryPopupNew').show();
                }*/

                $('#<%=btnMPEntryPopupNew.ClientID %>').click(function () {
                    cbpMPEntryPopupContent.PerformCallback('new');
                });
                $('#<%=btnMPEntryPopupSave.ClientID %>').click(function (evt) {
                    var errMessage = { text: "" };
                    var isAllowSave = true;
                    if (typeof onBeforeSaveRecord != 'undefined')
                        isAllowSave = onBeforeSaveRecord(errMessage);
                    if (isAllowSave) {
                        if (IsValid(evt, 'fsMPEntryPopup', 'mpEntryPopup'))
                            cbpMPEntryPopupProcess.PerformCallback('save');
                    }
                    else
                        showToast('Warning', errMessage.text);
                });

            });
        </script>
        <dxcp:ASPxCallbackPanel ID="cbpMPEntryPopupContent" runat="server" Width="100%" ClientInstanceName="cbpMPEntryPopupContent"
            ShowLoadingPanel="false" OnCallback="cbpMPEntryPopupContent_Callback">
            <ClientSideEvents BeginCallback="function(s,e){
                showLoadingPanel();
            }" EndCallback="function(s,e){
                setEntryPopupIsAdd(true);
                hideLoadingPanel();
            }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server"> 
                    <fieldset id="fsMPEntryPopup">  
                        <asp:Panel ID="pnlEntryPopup" runat="server" />  
                    </fieldset>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>

        <div style="text-align:right; padding-right: 10px;">
            <input type="button" style="display:none" runat="server" id="btnMPEntryPopupNew" value="New" />
            <input type="button" style="display:none" runat="server" id="btnMPEntryPopupSave" value="Save" />
        </div>

        <dxcp:ASPxCallbackPanel ID="cbpMPEntryPopupProcess" runat="server" Width="100%" ClientInstanceName="cbpMPEntryPopupProcess"
            ShowLoadingPanel="false" OnCallback="cbpMPEntryPopupProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){
                showLoadingPanel();
            }" EndCallback="function(s,e){
                var result = s.cpResult.split('|');
                if(result[0] == 'saveadd' || result[0] == 'saveedit'){
                    if(result[1] == 'success'){
                        var param = s.cpRetval;
                        
                        hideLoadingPanel();
                        if(result[0] == 'saveadd' && typeof onAfterSaveAddRecordEntryPopup != 'undefined')
                            onAfterSaveAddRecordEntryPopup(param);
                        if(result[0] == 'saveedit' && typeof onAfterSaveEditRecordEntryPopup != 'undefined')
                            onAfterSaveEditRecordEntryPopup(param);

                        var isAdd = false;
                        if(result[0] == 'saveadd')
                            isAdd = true;

                        if(typeof onGetEntryPopupReturnValue != 'undefined' && typeof onAfterSaveRightPanelContent != 'undefined')
                            onAfterSaveRightPanelContent($('#hdnRightPanelContentCode').val(), onGetEntryPopupReturnValue(), isAdd);
                        pcRightPanelContent.Hide();
                    }
                    else {
                        if(result[2] != '')
                            showToast('Save Failed', 'Error Message : ' + result[2]);
                        else
                            showToast('Save Failed', '');
                        hideLoadingPanel();
                    }
                }
            }" />
        </dxcp:ASPxCallbackPanel>
    </div>

