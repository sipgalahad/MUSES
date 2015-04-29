<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectCurriculumSyllabusEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectCurriculumSyllabusEntry" %>

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
        });

        $('.divTransactionAdd').live('click', function () {
            $li = $(this).closest('li');

            var curriculumDtID = $li.find('.cboCurriculumSyllabusID option:selected').val();
            var subjectCurriculumID = tacSubjectCurriculum.getValue();

            var id = 'add|' + subjectCurriculumID + '|' + curriculumDtID;
            var url = ResolveUrl("~/Program/Master/Subject/SubjectCurriculumSyllabus/SubjectCurriculumSyllabusEntryDtCtl.ascx");
            openUserControlPopup(url, id, 'Entry Data', 700, 400);
        });

        //#region edit and delete
        $('.divDetailEdit').live('click', function () {
            $li = $(this).closest('li');

            var curriculumDtID = $li.find('.cboCurriculumSyllabusID option:selected').val();
            var subjectCurriculumID = tacSubjectCurriculum.getValue();

            $row = $(this).closest('tr');
            var id = 'edit|' + subjectCurriculumID + '|' + curriculumDtID + '|' + $row.find('.hdnSubjectCurriculumSyllabusID').val();
            var url = ResolveUrl("~/Program/Master/Subject/SubjectCurriculumSyllabus/SubjectCurriculumSyllabusEntryDtCtl.ascx");
            openUserControlPopup(url, id, 'Entry Data', 700, 400);
        });

        $('.divDetailDelete').live('click', function () {
            $li = $(this).closest('li');
            $row = $(this).closest('tr');
            showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val($row.find('.hdnSubjectCurriculumSyllabusID').val());
                    cbpProcess.PerformCallback('delete');
                }
            });
        });
        //#endregion

        function onCboFilterValueChanged() {
            cbpView.PerformCallback('refresh');
        }

        //#region SubjectCurriculum
        function onGetSubjectCurriculumFilterExpression() {
            var filterExpression = "<%=OnGetSubjectCurriculumFilterExpression() %>";
            return filterExpression;
        }

        function onTacSubjectCurriculumButtonSearchClick() {
            openSearchDialog('subjectcurriculum', onGetSubjectCurriculumFilterExpression(), function (value) {
                var filterExpression = onGetSubjectCurriculumFilterExpression() + " AND SubjectCurriculumID = '" + value + "'";
                Methods.getObject('GetSubjectCurriculumList', filterExpression, function (result) {
                    if (result != null) {
                        tacSubjectCurriculum.setValue(result.SubjectCurriculumID);
                        tacSubjectCurriculum.setText(result.SubjectCurriculumName);
                        $('#<%=hdnCurriculumID.ClientID %>').val(result.CurriculumID);
                    }
                    else {
                        tacSubjectCurriculum.setValue('');
                        tacSubjectCurriculum.setText('');
                        $('#<%=hdnCurriculumID.ClientID %>').val('');
                    }
                    onRefreshGridView();
                });
            });
        }

        function onTacSubjectCurriculumValueChanged() {
            var id = tacSubjectCurriculum.getValue();
            if (id != '') {
                var filterExpression = "SubjectCurriculumID = '" + id + "'";
                Methods.getObject('GetSubjectCurriculumList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=hdnCurriculumID.ClientID %>').val(result.CurriculumID);
                    else 
                        $('#<%=hdnCurriculumID.ClientID %>').val('');
                    onRefreshGridView();
                });
            }
            else {
                $('#<%=hdnCurriculumID.ClientID %>').val('');
                onRefreshGridView();
            }
        }

        function onRefreshGridView() {
            $('#ulContainerSubjectCurriculum li:gt(0)').remove();
            $li = $('#ulContainerSubjectCurriculum li:eq(0)');
            $li.html('');
            $panel = $($('#tmplSubjectCurriculum').html());
            $li.append($panel);

            fillSubjectCurriculumSyllabusList(0);
        }

        function fillSubjectCurriculumSyllabusList(idx) {
            $li = $('#ulContainerSubjectCurriculum li:eq(' + idx + ')');
            var filterExpression = "";
            var parentID = $li.find('.hdnParentID').val();
            if (parentID == "")
                filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID IS NULL AND IsDeleted = 0';
            else
                filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID = ' + parentID + ' AND IsDeleted = 0';
            Methods.getListObject('GetCurriculumSyllabusList', filterExpression, function (result) {
                for (var i = 0; i < result.length; ++i) {
                    $option = $("<option value='" + result[i].CurriculumSyllabusID + "'>" + result[i].CurriculumSyllabusName + "</option>");
                    $panel.find('.cboCurriculumSyllabusID').append($option);
                }
                $panel.find('.cboCurriculumSyllabusID').change(function () {
                    $li = $(this).closest('li');
                    refreshGridCurriculumSyllabus($li);
                });
                refreshGridCurriculumSyllabus($li);
            });
        }

        function onAfterSaveRecordSubjectCurriculumSyllabus() {
            refreshGridCurriculumSyllabus($li);
        }

        function refreshGridCurriculumSyllabus($li) {
            var id = $li.find('.cboCurriculumSyllabusID option:selected').val();
            $tbl = $li.find('.tblSubjectCurriculumSyllabus');
            $tbl.find('tr:gt(0)').each(function () {
                $(this).remove();
            });
            var filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumSyllabusID = " + id + " AND IsDeleted = 0";
            Methods.getListObject('GetvSubjectCurriculumSyllabusList', filterExpression, function (result) {
                $("#tmplListSubjectCurriculumSyllabus").tmpl(result).appendTo($tbl);

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
                            var idx = $('#ulContainerSubjectCurriculum li').index($li) + 1;
                            if (idx == $('#ulContainerSubjectCurriculum li').length) {
                                $('#ulContainerSubjectCurriculum').append('<li>');
                            }

                            $('#ulContainerSubjectCurriculum li:gt(' + idx + ')').remove();

                            $li = $('#ulContainerSubjectCurriculum li:eq(' + idx + ')');
                            $li.html('');
                            $panel = $($('#tmplSubjectCurriculum').html());
                            $li.append($panel);

                            $li.find('.hdnParentID').val($(this).find('.hdnSubjectCurriculumSyllabusID').val());

                            fillSubjectCurriculumSyllabusList(idx);
                        }
                    }
                });
            });
        }
        //#endregion

        function onCbpProcessEndCallback(s) {
            var param = s.cpResult.split('|');
            if (param[0] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[1]);
            else {
                onAfterSaveRecordSubjectCurriculumSyllabus();
                hideLoadingPanel();
            }
        }

        $('.lnkDetail a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectBasicCompetency/SubjectBasicCompetencyDtEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectBasicCompetencyID, 'Detil', 800, 550);
        });

        $('.lnkIndicator a').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);
            var url = ResolveUrl("~/Program/Master/Subject/SubjectBasicCompetency/SubjectIndicatorEntryCtl.ascx");
            openUserControlPopup(url, entity.SubjectBasicCompetencyID, 'Indikator', 800, 550);
        });
    </script>
    <style type="text/css">
        #ulContainerSubjectCurriculum                           { position: absolute; left: 10px; right: 10px; margin: 0; padding: 0; height: 450px;white-space: nowrap; overflow-x: scroll; }
        #ulContainerSubjectCurriculum li                        { padding: 5px; width: 400px; height: 400px; border-right: 1px solid #EAEAEA; display: inline-table; list-style-type: none; }
        #ulContainerSubjectCurriculum li:first-child            { border-left: 1px solid #EAEAEA; }
        
        .tblSubjectCurriculumSyllabus             { border-collapse:collapse; table-layout:fixed; width: 390px; }
        .tblSubjectCurriculumSyllabus td:first-child div         { -ms-word-break: break-all;word-break: break-all;-webkit-hyphens: auto;-moz-hyphens: auto;hyphens: auto;max-width: 300px; white-space: nowrap }
    </style>
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnEntryID" runat="server" />
    <fieldset id="fsFilter">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:200px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Jenis Kurikulum")%></label></td>
                <td>      
                    <input type="hidden" id="hdnCurriculumID" runat="server" />      
                    <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSubjectCurriculum" ClientInstanceName="tacSubjectCurriculum" MethodName="GetSubjectCurriculumList" GetFilterExpressionFunction="onGetSubjectCurriculumFilterExpression"
                        SearchFields="SubjectCurriculumName,SubjectCurriculumID" TextField="SubjectCurriculumName" ValueField="SubjectCurriculumID" SearchText="${SubjectCurriculumName}" OrderByExpression="SubjectCurriculumName">
                        <ClientSideEvents ButtonSearchClick="function(){ onTacSubjectCurriculumButtonSearchClick(); }"
                            ValueChanged="function(){ onTacSubjectCurriculumValueChanged(); }" />
                    </cdx:CodeXAutoCompleteTextBox>   
                </td>
            </tr> 
            <tr>
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
                <td>
                    <dxe:ASPxComboBox ID="cboGCPeriodSection" ClientInstanceName="cboGCPeriodSection" Width="200px" runat="server">
                        <ClientSideEvents ValueChanged="function(){ onCboGCPeriodSectionValueChanged(); }" />
                    </dxe:ASPxComboBox>
                </td>
            </tr> 
        </table>
    </fieldset>
    <script id="tmplListSubjectCurriculumSyllabus" type="text/x-jquery-tmpl">
        <tr class="trSubjectCurriculumSyllabus">
            <td><div>${SubjectCurriculumSyllabusName}</div></td>
            <td>
                <div style='float:right;' class="divDetailDelete"></div>
                <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                <input type="hidden" class="hdnSubjectCurriculumSyllabusID" value="${SubjectCurriculumSyllabusID}"/>
                <input type="hidden" class="hdnIsHeader" value="${IsHeader}"/>
            </td>
        </tr>
    </script>
    <script id="tmplSubjectCurriculum" type="text/x-jquery-tmpl">
        <table class="tblEntryContent" style="width:70%">
            <colgroup>
                <col style="width:100px"/>
                <col/>
            </colgroup>
            <tr>
                <td class="tdLabel">Tipe</td>
                <td><select class="cboCurriculumSyllabusID" style="width:200px;"></select></td>
            </tr>
        </table>
        <input type="hidden" class="hdnParentID" value=""/>
        <div class="divTransactionEntry" style="margin-top: 5px;">
            <span class="divTransactionAdd divAdd"><%=GetLabel("Tambah Data")%></span>&nbsp;
            <br />
        </div>
        <table class="tblSubjectCurriculumSyllabus grdSelected" rules="all">
            <colgroup>
                <col />
                <col style="width: 80px" />
            </colgroup>
            <tr>
                <th>Keterangan</th>
                <th></th>
            </tr>
        </table>
        <br style="clear: both" />
    </script>
    <ul id="ulContainerSubjectCurriculum">
        <li>&nbsp;</li>
    </ul>

    <div style="display: none">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpProcessEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>