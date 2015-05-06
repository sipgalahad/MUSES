<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectCurriculumMeetingPlanDtCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectCurriculumMeetingPlanDtCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        setTimeout(function () {
            onRefreshGridViewGroup();
            onRefreshGridViewDetail();
        }, 100);
    });

    //#region Grd Group
    function onRefreshGridViewGroup() {
        $('#ulContainerSubjectCurriculumGroup li:gt(0)').remove();
        $li = $('#ulContainerSubjectCurriculumGroup li:eq(0)');
        $li.html('');
        $panel = $($('#tmplSubjectCurriculumGroup').html());
        $li.append($panel);
        fillSubjectCurriculumMeetingPlanListGroup(0);
    }

    function fillSubjectCurriculumMeetingPlanListGroup(idx) {
        $li = $('#ulContainerSubjectCurriculumGroup li:eq(' + idx + ')');
        var filterExpression = "";
        var parentID = $li.find('.hdnCurriculumMeetingPlanID').val();
        if (parentID == "")
            filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID IS NULL AND IsDeleted = 0';
        else
            filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID = ' + parentID + ' AND IsDeleted = 0';
        Methods.getListObject('GetCurriculumMeetingPlanList', filterExpression, function (result) {
            for (var i = 0; i < result.length; ++i) {
                var isUsingCode = '1';
                if (!result[i].IsUsingCode)
                    isUsingCode = '0';

                var referenceID = '0';
                if (result[i].CurriculumSyllabusReferenceID != null)
                    referenceID = result[i].CurriculumSyllabusReferenceID;

                $option = $("<option value='" + result[i].CurriculumMeetingPlanID + "' isusingcode='" + isUsingCode + "' referenceid='" + referenceID + "'>" + result[i].CurriculumMeetingPlanName + "</option>");
                $panel.find('.cboCurriculumMeetingPlanID').append($option);
            }
            $panel.find('.cboCurriculumMeetingPlanID').change(function () {
                $li = $(this).closest('li');
                var idx = $('#ulContainerSubjectCurriculumGroup li').index($li);
                $('#ulContainerSubjectCurriculumGroup li:gt(' + idx + ')').remove();
                refreshGridCurriculumMeetingPlanGroup($li);
            });
            refreshGridCurriculumMeetingPlanGroup($li);
        });
    }

    function refreshGridCurriculumMeetingPlanGroup($li) {
        $opt = $li.find('.cboCurriculumMeetingPlanID option:selected');
        var id = $opt.val();
        var isUsingCode = $opt.attr('isusingcode');
        var referenceID = $opt.attr('referenceid');

        $tbl = $li.find('.tblSubjectCurriculumMeetingPlan');
        $tbl.find('tr:gt(0)').each(function () {
            $(this).remove();
        });

        if (isUsingCode == '1')
            $tbl.find('.thCode').attr('style', 'width: 80px');
        else
            $tbl.find('.thCode').attr('style', 'display:none');

        var parentID = $li.find('.hdnParentID').val();
        var filterExpression = "";
        var idx = $('#ulContainerSubjectCurriculumGroup li').index($li);
        if (idx == 0) 
            filterExpression = "SubjectCurriculumMeetingPlanID = " + $('#<%=hdnParentSubjectCurriculumMeetingPlanID.ClientID %>').val(); 
        else {
            if (parentID == "")
                filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumMeetingPlanID = " + id + " AND ParentID IS NULL AND IsDeleted = 0";
            else
                filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumMeetingPlanID = " + id + " AND ParentID = " + parentID + " AND IsDeleted = 0";
        }
        Methods.getListObject('GetvSubjectCurriculumMeetingPlanList', filterExpression, function (result) {
            $("#tmplListSubjectCurriculumMeetingPlanGroup").tmpl(result).appendTo($tbl);

            if (isUsingCode == '0') {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdCode').attr('style', 'display:none');
                });
            }
            if (referenceID == '0') {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdReference').attr('style', 'display:none');
                });
            }
            else {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdName').attr('style', 'display:none');
                });
            }
            $tbl.find('tr:gt(0)').click(function (e) {
                var className = $(e.target).attr('class');
                if (className == 'divGroupDelete' || className == 'divGroupEdit')
                    return;
                if (!$(this).hasClass('selected')) {
                    $tbl = $(this).parent();

                    $tbl.find('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                    if ($(this).find('.hdnIsHeader').val() == 'true') {
                        $li = $(this).closest('li');
                        var idx = $('#ulContainerSubjectCurriculumGroup li').index($li) + 1;
                        if (idx == $('#ulContainerSubjectCurriculumGroup li').length) {
                            $('#ulContainerSubjectCurriculumGroup').append('<li>');
                        }

                        $('#ulContainerSubjectCurriculumGroup li:gt(' + idx + ')').remove();

                        $li = $('#ulContainerSubjectCurriculumGroup li:eq(' + idx + ')');
                        $li.html('');
                        $panel = $($('#tmplSubjectCurriculumGroup').html());
                        $li.append($panel);

                        $li.find('.hdnCurriculumMeetingPlanID').val(id);

                        $li.find('.hdnParentID').val($(this).find('.hdnSubjectCurriculumMeetingPlanID').val());

                        fillSubjectCurriculumMeetingPlanListGroup(idx);
                    }
                }
            });
        });
    }
    //#endregion

    //#region Grd Detail
    function onRefreshGridViewDetail() {
        $('#ulContainerSubjectCurriculumDetail li:gt(0)').remove();
        $li = $('#ulContainerSubjectCurriculumDetail li:eq(0)');
        $li.html('');
        $panel = $($('#tmplSubjectCurriculumDetail').html());
        $li.append($panel);

        fillSubjectCurriculumMeetingPlanListDetail(0);
    }

    function fillSubjectCurriculumMeetingPlanListDetail(idx) {
        $li = $('#ulContainerSubjectCurriculumDetail li:eq(' + idx + ')');
        var filterExpression = "";
        var parentID = '';
        if (idx == 0)
            parentID = $('#<%=hdnCurriculumMeetingPlanID.ClientID %>').val();
        else
            parentID = $li.find('.hdnCurriculumMeetingPlanID').val();
        if (parentID == "")
            filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID IS NULL AND IsDeleted = 0';
        else
            filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID = ' + parentID + ' AND IsDeleted = 0';
        Methods.getListObject('GetCurriculumMeetingPlanList', filterExpression, function (result) {
            for (var i = 0; i < result.length; ++i) {
                var isUsingCode = '1';
                if (!result[i].IsUsingCode)
                    isUsingCode = '0';

                var referenceID = '0';
                if (result[i].CurriculumSyllabusReferenceID != null)
                    referenceID = result[i].CurriculumSyllabusReferenceID;

                $option = $("<option value='" + result[i].CurriculumMeetingPlanID + "' isusingcode='" + isUsingCode + "' referenceid='" + referenceID + "'>" + result[i].CurriculumMeetingPlanName + "</option>");
                $panel.find('.cboCurriculumMeetingPlanID').append($option);
            }
            $panel.find('.cboCurriculumMeetingPlanID').change(function () {
                $li = $(this).closest('li');
                var idx = $('#ulContainerSubjectCurriculumDetail li').index($li);
                $('#ulContainerSubjectCurriculumDetail li:gt(' + idx + ')').remove();
                refreshGridCurriculumMeetingPlanDetail($li);
            });
            refreshGridCurriculumMeetingPlanDetail($li);
        });
    }

    function refreshGridCurriculumMeetingPlanDetail($li) {
        $opt = $li.find('.cboCurriculumMeetingPlanID option:selected');
        var id = $opt.val();
        var isUsingCode = $opt.attr('isusingcode');
        var referenceID = $opt.attr('referenceid');

        $tbl = $li.find('.tblSubjectCurriculumMeetingPlan');
        $tbl.find('tr:gt(0)').each(function () {
            $(this).remove();
        });

        if (isUsingCode == '1')
            $tbl.find('.thCode').attr('style', 'width: 80px');
        else
            $tbl.find('.thCode').attr('style', 'display:none');

        var parentID = $li.find('.hdnParentID').val();
        var filterExpression = "";
        if (parentID == "")
            filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumMeetingPlanID = " + id + " AND ParentID IS NULL AND IsDeleted = 0";
        else
            filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumMeetingPlanID = " + id + " AND ParentID = " + parentID + " AND IsDeleted = 0";
        Methods.getListObject('GetvSubjectCurriculumMeetingPlanList', filterExpression, function (result) {
            $("#tmplListSubjectCurriculumMeetingPlanDetail").tmpl(result).appendTo($tbl);

            if (isUsingCode == '0') {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdCode').attr('style', 'display:none');
                });
            }
            if (referenceID == '0') {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdReference').attr('style', 'display:none');
                });
            }
            else {
                $tbl.find('tr:gt(0)').each(function () {
                    $(this).find('.tdName').attr('style', 'display:none');
                });
            }
            $tbl.find('tr:gt(0)').click(function (e) {
                var className = $(e.target).attr('class');
                if (className == 'divDetailDelete' || className == 'divDetailEdit')
                    return;
                if (!$(this).hasClass('selected')) {
                    $tbl = $(this).parent();

                    $tbl.find('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                    if ($(this).find('.hdnIsHeader').val() == 'true') {
                        $li = $(this).closest('li');
                        var idx = $('#ulContainerSubjectCurriculumDetail li').index($li) + 1;
                        if (idx == $('#ulContainerSubjectCurriculumDetail li').length) {
                            $('#ulContainerSubjectCurriculumDetail').append('<li>');
                        }

                        $('#ulContainerSubjectCurriculumDetail li:gt(' + idx + ')').remove();

                        $li = $('#ulContainerSubjectCurriculumDetail li:eq(' + idx + ')');
                        $li.html('');
                        $panel = $($('#tmplSubjectCurriculumDetail').html());
                        $li.append($panel);

                        $li.find('.hdnCurriculumMeetingPlanID').val(id);

                        $li.find('.hdnParentID').val($(this).find('.hdnSubjectCurriculumMeetingPlanID').val());

                        fillSubjectCurriculumMeetingPlanListDetail(idx);
                    }
                }
            });
        });
    }
    //#endregion
</script>
<style type="text/css">
    #ulContainerSubjectCurriculumDetail                           { position: absolute; left: 10px; right: 10px; margin: 0; padding: 0; height: 200px;white-space: nowrap; overflow-x: scroll; }
    #ulContainerSubjectCurriculumDetail li                        { padding: 5px; width: 350px; height: 200px; border-right: 1px solid #EAEAEA; display: inline-table; list-style-type: none; }
    #ulContainerSubjectCurriculumDetail li:first-child            { border-left: 1px solid #EAEAEA; }
    
    #ulContainerSubjectCurriculumGroup                           { position: absolute; left: 10px; right: 10px; margin: 0; padding: 0; height: 200px;white-space: nowrap; overflow-x: scroll; }
    #ulContainerSubjectCurriculumGroup li                        { padding: 5px; width: 350px; height: 200px; border-right: 1px solid #EAEAEA; display: inline-table; list-style-type: none; }
    #ulContainerSubjectCurriculumGroup li:first-child            { border-left: 1px solid #EAEAEA; }    
    
    .tblSubjectCurriculumMeetingPlan             { border-collapse:collapse; table-layout:fixed; width: 350px; }
    .tdName div, .tdReference div               { -ms-word-break: break-all;word-break: break-all;-webkit-hyphens: auto;-moz-hyphens: auto;hyphens: auto;max-width: 370px; white-space: nowrap }
</style>

<div style="overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumID" value="" runat="server" />
    <input type="hidden" id="hdnSubjectID" value="" runat="server" />
    <input type="hidden" id="hdnCurriculumMeetingPlanID" value="" runat="server" />
    <input type="hidden" id="hdnParentSubjectCurriculumMeetingPlanID" value="" runat="server" />
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:200px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Pertemuan Ke")%></label></td>
            <td><asp:TextBox ID="txtMeetingNo" ReadOnly="true" Width="200px" runat="server" /></td>
        </tr> 
    </table>
    <div id="divGroup" runat="server">
        <h4 style="color: Maroon; font-weight: bold;"><%=GetLabel("Kelompok") %></h4>
        <script id="tmplListSubjectCurriculumMeetingPlanGroup" type="text/x-jquery-tmpl">
            <tr class="trSubjectCurriculumMeetingPlan">
                <td class="tdCode"><div>${SubjectCurriculumMeetingPlanCode}</div></td>
                <td class="tdName"><div>${SubjectCurriculumMeetingPlanName}</div></td>
                <td class="tdReference"><div>${ReferenceName}</div></td>
                <td style="display:none">
                    <input type="hidden" class="hdnSubjectCurriculumMeetingPlanID" value="${SubjectCurriculumMeetingPlanID}"/>
                    <input type="hidden" class="hdnIsHeader" value="${IsHeader}"/>
                </td>
            </tr>
        </script>
        <script id="tmplSubjectCurriculumGroup" type="text/x-jquery-tmpl">
            <table class="tblEntryContent" style="width:70%">
                <colgroup>
                    <col style="width:100px"/>
                    <col/>
                </colgroup>
                <tr>
                    <td class="tdLabel">Tipe</td>
                    <td><select class="cboCurriculumMeetingPlanID" style="width:200px;"></select></td>
                </tr>
            </table>
            <input type="hidden" class="hdnParentID" value=""/>
            <input type="hidden" class="hdnCurriculumMeetingPlanID" value=""/>
            <table class="tblSubjectCurriculumMeetingPlan grdSelected" rules="all">
                <tr>
                    <th class="thCode" style="width: 80px">Kode</th>
                    <th>Teks</th>
                </tr>
            </table>
            <br style="clear: both" />
        </script>
        <div style="position: relative; height: 200px">
            <ul id="ulContainerSubjectCurriculumGroup">
                <li>&nbsp;</li>
            </ul>
        </div>
    </div>
    <br style="clear: both" />
    <h4 style="color: Maroon; font-weight: bold;"><%=GetLabel("Detil") %></h4>
    <script id="tmplListSubjectCurriculumMeetingPlanDetail" type="text/x-jquery-tmpl">
        <tr class="trSubjectCurriculumMeetingPlan">
            <td class="tdCode"><div>${SubjectCurriculumMeetingPlanCode}</div></td>
            <td class="tdName"><div>${SubjectCurriculumMeetingPlanName}</div></td>
            <td class="tdReference"><div>${ReferenceName}</div></td>
            <td style="display:none">
                <input type="hidden" class="hdnSubjectCurriculumMeetingPlanID" value="${SubjectCurriculumMeetingPlanID}"/>
                <input type="hidden" class="hdnIsHeader" value="${IsHeader}"/>
            </td>
        </tr>
    </script>
    <script id="tmplSubjectCurriculumDetail" type="text/x-jquery-tmpl">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:100px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel">Tipe</td>
                <td><select class="cboCurriculumMeetingPlanID" style="width:200px;"></select></td>
            </tr>
        </table>
        <input type="hidden" class="hdnParentID" value=""/>
        <input type="hidden" class="hdnCurriculumMeetingPlanID" value=""/>
        <table class="tblSubjectCurriculumMeetingPlan grdSelected" rules="all">
            <tr>
                <th class="thCode" style="width: 80px">Kode</th>
                <th>Teks</th>
            </tr>
        </table>
        <br style="clear: both" />
    </script>
    <div style="position: relative; height: 200px">
        <ul id="ulContainerSubjectCurriculumDetail">
            <li>&nbsp;</li>
        </ul>
    </div>
</div>

