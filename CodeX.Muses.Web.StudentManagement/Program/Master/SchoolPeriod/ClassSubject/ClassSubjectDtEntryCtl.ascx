<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassSubjectDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassSubjectDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_drugslogisticsquickpicksctl">
    function addItemFilterRow() {
        $trHeader = $('#<%=grdView.ClientID %> tr:eq(0)');
        $trFilter = $("<tr><td></td><td></td><td></td><td></td></tr>");

        $input = $("<input type='text' id='txtFilterItemCode' style='width:100%;height:20px' />").val($('#<%=hdnFilterItemCode.ClientID %>').val());
        $trFilter.find('td').eq(1).append($input);
        $input = $("<input type='text' id='txtFilterItemName' style='width:100%;height:20px' />").val($('#<%=hdnFilterItemName.ClientID %>').val());
        $trFilter.find('td').eq(2).append($input);
        $trFilter.insertAfter($trHeader);
    }

    $('#txtFilterItemCode').live('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            getCheckedMember();
            $('#<%=hdnFilterItemCode.ClientID %>').val($(this).val());
            e.preventDefault();
            cbpPopup.PerformCallback('refresh');
        }
    });

    $('#txtFilterItemName').live('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            getCheckedMember();
            $('#<%=hdnFilterItemName.ClientID %>').val($(this).val());
            e.preventDefault();
            cbpPopup.PerformCallback('refresh');
        }
    });

    $(function () {
        hideLoadingPanel();
        addItemFilterRow();
    });

    function onBeforeSaveRecord(errMessage) {
        if (IsValid(null, 'fsDrugsQuickPicks', 'mpDrugsQuickPicks')) {
            getCheckedMember();
            if ($('#<%=hdnSelectedMember.ClientID %>').val() != '')
                return true;
            else {
                errMessage.text = 'Please Select Item First';
                return false;
            }
        }
        return false;
    }

    function getCheckedMember() {
        var lstSelectedMember = [];
        var lstSelectedMemberQty = [];
        var lstSelectedIsMainTeacher = [];
        var lstSelectedAssistantTeacher = [];
        var result = '';

        var totalQty = 0;
        $('#tblSelectedItem .trSelectedItem').each(function () {
            var key = $(this).find('.keyField').val();
            var qty = parseFloat($(this).find('.txtQty').val());
            var isMainRole = $(this).find('.chkIsMainTeacher input').is(':checked') ? '1' : '0';
            var assistantTeacherID = $(this).find('.ddlAssistantTeacher').val();
            lstSelectedMember.push(key);
            lstSelectedMemberQty.push(qty);
            lstSelectedIsMainTeacher.push(isMainRole);
            lstSelectedAssistantTeacher.push(assistantTeacherID);

            totalQty += qty;
        });

        $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
        $('#<%=hdnSelectedMemberQty.ClientID %>').val(lstSelectedMemberQty.join(','));
        $('#<%=hdnSelectedIsMainTeacher.ClientID %>').val(lstSelectedIsMainTeacher.join(','));
        $('#<%=hdnSelectedAssistantTeacher.ClientID %>').val(lstSelectedAssistantTeacher.join(','));
        
        var NoMeetingHoursInWeek = parseFloat($('#<%=txtNumberMeetingInHours.ClientID %>').val());
        if (NoMeetingHoursInWeek != totalQty)
            return false;
        return true;
        return false;
    }

    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');

    $(function () {
        setPaging($("#pagingPopup"), pageCount, function (page) {
            getCheckedMember();
            cbpPopup.PerformCallback('changepage|' + page);
        });
    });

    function onCbpPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedMember();
                cbpPopup.PerformCallback('changepage|' + page);
            });
        }
        addItemFilterRow();
    }
    //#endregion

    $('#<%=grdView.ClientID %> .chkIsSelected input').die('change');
    $('#<%=grdView.ClientID %> .chkIsSelected input').live('change', function () {
        if ($(this).is(':checked')) {
            $selectedTr = $(this).closest('tr');

            $newTr = $('#tmplSelectedTestItem').html();
            $newTr = $newTr.replace(/\$\{TeacherCode}/g, $selectedTr.find('.tdTeacherCode').html());
            $newTr = $newTr.replace(/\$\{TeacherName}/g, $selectedTr.find('.tdTeacherName').html());
            $newTr = $newTr.replace(/\$\{TeacherID}/g, $selectedTr.find('.keyField').html());
            $newTr = $($newTr);
            $newTr.insertBefore($('#trFooter'));
        }
        else {
            var id = $(this).closest('tr').find('.keyField').html();
            $('#tblSelectedItem tr').each(function () {
                if ($(this).find('.keyField').val() == id) {
                    $(this).remove();
                    if ($(this).find('.chkIsMainTeacher input').is(':checked')) {
                        if ($('#tblSelectedItem .trSelectedItem').length > 0)
                            $('#tblSelectedItem .trSelectedItem:eq(0)').find('.chkIsMainTeacher input').prop('checked', true);
                    }
                }
            });
        }
    });

    $('.chkIsMainTeacher input').die('change');
    $('.chkIsMainTeacher input').live('change', function () {
        if ($(this).is(':checked')) {
            $('#tblSelectedItem .trSelectedItem .chkIsMainTeacher input:checked').each(function () {
                $(this).prop('checked', false);
            });
            $(this).prop('checked', true);
        }
        else {
            $('#tblSelectedItem .trSelectedItem:eq(0)').find('.chkIsMainTeacher input').prop('checked', true);
        }
    });

    $('#tblSelectedItem .chkIsSelected2').die('change');
    $('#tblSelectedItem .chkIsSelected2').live('change', function () {
        if ($(this).is(':checked')) {
            $selectedTr = $(this).closest('tr');
            var id = $selectedTr.find('.keyField').val();
            var isFound = false;
            $('#<%=grdView.ClientID %> tr').each(function () {
                if (id == $(this).find('.keyField').html()) {
                    $(this).find('.chkIsSelected').find('input').prop('checked', false);
                    isFound = true;
                }
            });
            if (!isFound) {
                var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
                lstSelectedMember.splice(lstSelectedMember.indexOf(id), 1);
                $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            }
            $tr = $(this).closest('tr');
            $tr.remove();
            if ($tr.find('.chkIsMainTeacher input').is(':checked')) {
                if ($('#tblSelectedItem .trSelectedItem').length > 0)
                    $('#tblSelectedItem .trSelectedItem:eq(0)').find('.chkIsMainTeacher input').prop('checked', true);
            }
        }
    });

    function onBeforeSaveRecord(errMessage) {
        if (IsValid(null, 'fsTrxPopup', 'mpTrxPopup')) {
            if (getCheckedMember()) {
                if ($('#<%=hdnSelectedMember.ClientID %>').val() != '')
                    return true;
                else {
                    errMessage.text = 'Please Select Item First';
                    return false;
                }
            }
            else
                errMessage.text = 'Jumlah Jam Pertemuan Harus Sama Dengan ' + $('#<%=txtNumberMeetingInHours.ClientID %>').val();
        }
        return false;
    }
</script>
<div style="padding:10px; max-height: 400px; overflow-y: auto;">
    <script id="tmplSelectedTestItem" type="text/x-jquery-tmpl">
        <tr class="trSelectedItem">
            <td align="center">
                <input type="checkbox" class="chkIsSelected2" />
                <input type="hidden" class="keyField" value='${TeacherID}' />
            </td>
            <td class="tdTeacherCode">${TeacherCode}</td>
            <td>${TeacherName}</td>
            <td><asp:DropDownList ID="ddlAssistantTeacher" Width="150px" CssClass="ddlAssistantTeacher" runat="server" /></td>
            <td><input type="text" validationgroup="mpTrxPopup" class="txtQty number min" min="1" value="1" style="width:60px" /></td>
            <td align="center"><asp:CheckBox ID="chkIsMainTeacher" CssClass="chkIsMainTeacher" runat="server"/></td>
        </tr>
    </script>
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnSelectedIsMainTeacher" runat="server" value="" />
    <input type="hidden" id="hdnSchoolClassID" runat="server" value="" />
    <input type="hidden" id="hdnSubjectID" runat="server" value="" />
    <input type="hidden" id="hdnCurriculumSubjectGroupID" runat="server" value="" />
    <input type="hidden" id="hdnPeriodClassTypeSubjectID" runat="server" value="" />
    <input type="hidden" id="hdnParam" runat="server" value="" />
    <input type="hidden" id="hdnFilterItemCode" runat="server" />
    <input type="hidden" id="hdnFilterItemName" runat="server" />
    <input type="hidden" id="hdnSelectedMemberQty" runat="server" value="" />
    <input type="hidden" id="hdnSelectedAssistantTeacher" runat="server" value="" />

    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>

            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pelajaran")%></label></td>
            <td><asp:TextBox ID="txtSubjectName" ReadOnly="true" Width="200px" runat="server" /></td>
        </tr> 
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jumlah Jam Pertemuan")%></label></td>
            <td><asp:TextBox ID="txtNumberMeetingInHours" ReadOnly="true" CssClass="number" Width="60px" runat="server" /></td>
        </tr> 
    </table>
    <table style="width:100%">
        <colgroup>
            <col style="width:40%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Tersedia")%></h4>
                <dxcp:ASPxCallbackPanel ID="cbpPopup" runat="server" Width="100%" ClientInstanceName="cbpPopup"
                    ShowLoadingPanel="false" OnCallback="cbpPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel();}"
                        EndCallback="function(s,e){ onCbpPopupEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="TeacherID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField"/>
                                        <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TeacherCode" HeaderText="Kode" ItemStyle-CssClass="tdTeacherCode" HeaderStyle-Width="50px" />
                                        <asp:BoundField DataField="TeacherName" HeaderText="Nama" ItemStyle-CssClass="tdTeacherName" />
                                        <asp:TemplateField HeaderStyle-CssClass="thRight" HeaderText="Jumlah Jam Mengajar" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <div id="divSlotNum" runat="server" />
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
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Dipilih")%></h4>
                <fieldset id="fsTrxPopup">
                    <table id="tblSelectedItem" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                        <tr id="trHeader2">
                            <th style="width:40px">&nbsp;</th>
                            <th align="center" style="width:50px"><%=GetLabel("Kode")%></th> 
                            <th align="center"><%=GetLabel("Nama")%></th> 
                            <th align="center"style="width:150px"><%=GetLabel("Guru 2")%></th> 
                            <th align="center"style="width:60px"><%=GetLabel("Jumlah")%></th> 
                            <th style="width:80px" class="thCenter"><%=GetLabel("Guru Utama")%></th> 
                        </tr>
                        <asp:Repeater ID="rptSelected" runat="server" OnItemDataBound="rptSelected_ItemDataBound">
                            <ItemTemplate>
                                <tr class="trSelectedItem">
                                    <td align="center">
                                        <input type="checkbox" class="chkIsSelected2" />
                                        <input type="hidden" class="keyField" value='<%#Eval("TeacherID") %>' />
                                    </td>
                                    <td class="tdTeacherCode"><%#Eval("TeacherCode") %></td>
                                    <td><%#Eval("TeacherName") %></td>
                                    <td><asp:DropDownList ID="ddlAssistantTeacher" Width="150px" CssClass="ddlAssistantTeacher" runat="server" /></td>
                                    <td><input type="text" validationgroup="mpTrxPopup" class="txtQty number min" min="1" value='<%#Eval("NoMeetingHoursInWeek") %>' style="width:60px" /></td>
                                    <td align="center"><asp:CheckBox ID="chkIsMainTeacher" CssClass="chkIsMainTeacher" runat="server" Checked='<%#Eval("IsMainTeacher") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr id="trFooter"></tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</div>