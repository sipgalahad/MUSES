<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="RenumerationCompEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.RenumerationCompEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="~/Libs/Controls/MasterCodingCtl.ascx" TagName="MasterCodingCtl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onCboRenumerationCompTypeValueChanged() {
            if (cboRenumerationCompType.GetValue() == "<%=OnGetRenumerationCompTypeDeduction() %>") {
                $('#trAddRenumerationCompSource').attr('style', 'display:none');
                $('#tblEntity .trEntityDt').each(function () {
                    $(this).remove();
                });
            }
            else
                $('#trAddRenumerationCompSource').removeAttr('style');
        }

        function onBeforeSaveRecord(errMessage) {
            var result = '';
            $('#tblEntity .trEntityDt').each(function () {
                if (result != '')
                    result += '|';
                result += $(this).find('.ddlRenumerationCompSource').val() + ';' + $(this).find('.ddlPerformanceIndicator').val();
            });
            $('#<%=hdnLstSaveValue.ClientID %>').val(result);
            return true;
        }

        function onLoad() {
            $('#divEntryDtAdd').click(function () {
                $newTr = $('#tmplEntityDt').html().replace('script1', 'script').replace('script1', 'script');
                $newTr = $($newTr);
                $newTr.insertBefore($('#trSaveEntryPopup'));
            });
            if (!getIsAdd()) {
                var filterExpression = "RenumerationCompID = " + $('#<%=hdnID.ClientID %>').val();
                Methods.getListObject('GetRenumerationCompSourceList', filterExpression, function (result) {
                    for (var i = 0; i < result.length; ++i) {
                        $('#divEntryDtAdd').click();
                        $newTr = $('#tblEntity .trEntityDt').last();
                        $newTr.find('.ddlRenumerationCompSource').val(result[i].GCRenumerationCompSource);
                        $newTr.find('.ddlRenumerationCompSource').change();
                        $newTr.find('.ddlPerformanceIndicator').val(result[i].PerformanceIndicatorID);
                    }
                });
            }

            $('#<%=chkIsApllyToAll.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    $('#trAddRenumerationCompSource').attr('style', 'display:none');
                    $('#tblEntity .trEntityDt').each(function () {
                        $(this).remove();
                    });
                }
                else
                    $('#trAddRenumerationCompSource').removeAttr('style');

            });
        }

        $('.ddlRenumerationCompSource').live('change', function () {
            if ($(this).val() == '<%=OnGetRenumerationCompSourcePerformanceIndicator() %>')
                $(this).closest('tr').find('.ddlPerformanceIndicator').show();
            else
                $(this).closest('tr').find('.ddlPerformanceIndicator').hide();
        });

        function onProspectiveStudentIDValueChanged($s) {
            $tacTr = $s.closest('tr');
            if ($s.val() != '') {
                //var trIdx = $('.trJournalEntry').index($tacTr);
                //if (trIdx == $('.trJournalEntry').length - 1)
                //    addEntityRowPrescription();
            }
        }

        $('.divDeleteEntryDt').live('click', function () {
            $tr = $(this).closest('tr');
            $tr.remove();
        });
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnLstSaveValue" runat="server" value="" />
    <script id="tmplEntityDt" type="text/x-jquery-tmpl">
        <tr class="trEntityDt">
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber")%></label></td>
            <td><asp:DropDownList runat="server" Width="200px" ID="ddlRenumerationCompSource" validationgroup="mpEntry" CssClass="ddlRenumerationCompSource required"/></td>
            <td><asp:DropDownList runat="server" Width="200px" ID="ddlPerformanceIndicator" validationgroup="mpEntry" CssClass="ddlPerformanceIndicator required" Style="display:none"/></td>
            <td><div style='float:right;' class="divDeleteEntryDt divDetailDelete"></div></td>
        </tr>
    </script>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" id="tblEntity" style="width:50%">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Kode")%></label></td>
                        <td colspan="2"><uc1:MasterCodingCtl ID="ctlEntityCode" runat="server" /> </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtRenumerationCompName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe")%></label></td>
                        <td colspan="2">
                            <dxe:ASPxComboBox ID="cboRenumerationCompType" ClientInstanceName="cboRenumerationCompType" Width="200px" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){ onCboRenumerationCompTypeValueChanged(); }" 
                                    Init="function(s,e){ onCboRenumerationCompTypeValueChanged(); }"/>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Apply Semua")%></label></td>
                        <td colspan="2"><asp:CheckBox ID="chkIsApllyToAll" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top; padding-top: 5px;" class="tdLabel"><label class="lblRemarks"><%=GetLabel("Catatan")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Rows="2" runat="server" /></td>
                    </tr>
                    <tr id="trAddRenumerationCompSource" >
                        <td>&nbsp;</td>
                        <td><span class="divAdd" id="divEntryDtAdd"><%=GetLabel("Tambah Sumber")%></span><br /></td>
                    </tr>
                    <tr id="trSaveEntryPopup">
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Sumber")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboRenumerationCompSource" Width="200px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
