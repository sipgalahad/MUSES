<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="SubjectCurriculumSyllabusInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.SubjectCurriculumSyllabusInformation" %>

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
            setTimeout(function () {
                if (tacSubjectCurriculum.getValue() != '') {
                    onRefreshGridView();
                }
            }, 100);

            $('#<%=cboSchoolPeriodSection.ClientID %>').change(function () {
                onRefreshGridView();
            });
        });

        //#region edit and delete
        $('.divDetailEdit').live('click', function () {
            $li = $(this).closest('li');

            var parentID = $li.find('.hdnParentID').val();
            var curriculumDtID = $li.find('.cboCurriculumSyllabusID option:selected').val();
            var subjectCurriculumID = tacSubjectCurriculum.getValue();

            $row = $(this).closest('tr');
            var id = $('#<%=hdnSubjectID.ClientID %>').val() + '|' + subjectCurriculumID + '|' + curriculumDtID + '|' + parentID + '|' + $('#<%=hdnIsPerSchoolPeriodSection.ClientID %>').val() + '|' + $('#<%=cboSchoolPeriodSection.ClientID %> option:selected').val() + '|' + $row.find('.hdnSubjectCurriculumSyllabusID').val(); 
            var url = ResolveUrl("~/Program/ClassMeeting/SubjectCurriculumSyllabus/SubjectCurriculumSyllabusInformationDtCtl.ascx");
            openUserControlPopup(url, id, 'View Detail', 700, 400);
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
                    }
                    else {
                        tacSubjectCurriculum.setValue('');
                        tacSubjectCurriculum.setText('');
                    }
                    entityToControlSubjectCurriculum(result);
                });
            });
        }

        function onTacSubjectCurriculumValueChanged() {
            var id = tacSubjectCurriculum.getValue();
            if (id != '') {
                var filterExpression = "SubjectCurriculumID = '" + id + "'";
                Methods.getObject('GetSubjectCurriculumList', filterExpression, function (result) {
                    entityToControlSubjectCurriculum(result);
                });
            }
            else {
                $('#<%=hdnCurriculumID.ClientID %>').val('');
                $('#<%=trSchoolPeriodSection.ClientID %>').attr('style', 'display:none');
                onRefreshGridView();
            }
        }

        function entityToControlSubjectCurriculum(result) {
            if (result != null) {
                $('#<%=hdnCurriculumID.ClientID %>').val(result.CurriculumID);
                $('#<%=hdnIsPerSchoolPeriodSection.ClientID %>').val(result.IsSyllabusPerSchoolPeriodSection ? '1' : '0');
                if (result.IsSyllabusPerSchoolPeriodSection) {
                    $('#<%=trSchoolPeriodSection.ClientID %>').removeAttr('style');
                    var filterExpression = 'CurriculumID = ' + result.CurriculumID + ' AND IsDeleted = 0';
                    Methods.getListObject('GetCurriculumSchoolPeriodSectionList', filterExpression, function (result1) {
                        for (var i = 0; i < result1.length; ++i) {
                            $option = $("<option value='" + result1[i].CurriculumSchoolPeriodSectionID + "'>" + result1[i].CurriculumSchoolPeriodSectionName + "</option>");
                            $('#<%=cboSchoolPeriodSection.ClientID %>').append($option);
                        }
                        onRefreshGridView();
                    });
                }
                else {
                    $('#<%=trSchoolPeriodSection.ClientID %>').attr('style', 'display:none');
                    onRefreshGridView();
                }
            }
            else {
                $('#<%=hdnCurriculumID.ClientID %>').val('');
                $('#<%=trSchoolPeriodSection.ClientID %>').attr('style', 'display:none');
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
            var parentID = $li.find('.hdnCurriculumSyllabusID').val();
            if (parentID == "")
                filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID IS NULL AND IsDeleted = 0';
            else
                filterExpression = 'CurriculumID = ' + $('#<%=hdnCurriculumID.ClientID %>').val() + ' AND ParentID = ' + parentID + ' AND IsDeleted = 0';
            Methods.getListObject('GetCurriculumSyllabusList', filterExpression, function (result) {
                for (var i = 0; i < result.length; ++i) {
                    var isUsingCode = '1';
                    if (!result[i].IsUsingCode)
                        isUsingCode = '0';
                    $option = $("<option value='" + result[i].CurriculumSyllabusID + "' isusingcode='" + isUsingCode + "'>" + result[i].CurriculumSyllabusName + "</option>");
                    $panel.find('.cboCurriculumSyllabusID').append($option);
                }
                $panel.find('.cboCurriculumSyllabusID').change(function () {
                    $li = $(this).closest('li');
                    var idx = $('#ulContainerSubjectCurriculum li').index($li);
                    $('#ulContainerSubjectCurriculum li:gt(' + idx + ')').remove();
                    refreshGridCurriculumSyllabus($li);
                });
                refreshGridCurriculumSyllabus($li);
            });
        }

        function onAfterSaveRecordSubjectCurriculumSyllabus() {
            refreshGridCurriculumSyllabus($li);
        }

        function refreshGridCurriculumSyllabus($li) {
            $opt = $li.find('.cboCurriculumSyllabusID option:selected');
            var id = $opt.val();
            var isUsingCode = $opt.attr('isusingcode');

            $tbl = $li.find('.tblSubjectCurriculumSyllabus');
            $tbl.find('tr:gt(0)').each(function () {
                $(this).remove();
            });

            if (isUsingCode == '1')
                $tbl.find('.thCode').attr('style', 'width: 80px');
            else
                $tbl.find('.thCode').attr('style', 'display:none');

            var parentID = $li.find('.hdnParentID').val();
            var filterExpression = "";
            if (parentID == "") {
                if ($('#<%=hdnIsPerSchoolPeriodSection.ClientID %>').val() == '0')
                    filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumSyllabusID = " + id + " AND ParentID IS NULL AND IsDeleted = 0";
                else
                    filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumSyllabusID = " + id + " AND CurriculumSchoolPeriodSectionID = " + $('#<%=cboSchoolPeriodSection.ClientID %> option:selected').val() + " AND ParentID IS NULL AND IsDeleted = 0";
            }
            else {
                if ($('#<%=hdnIsPerSchoolPeriodSection.ClientID %>').val() == '0')
                    filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumSyllabusID = " + id + " AND ParentID = " + parentID + " AND IsDeleted = 0";
                else
                    filterExpression = "SubjectID = " + $('#<%=hdnSubjectID.ClientID %>').val() + " AND CurriculumSyllabusID = " + id + " AND CurriculumSchoolPeriodSectionID = " + $('#<%=cboSchoolPeriodSection.ClientID %> option:selected').val() + " AND ParentID = " + parentID + " AND IsDeleted = 0";
            }
            Methods.getListObject('GetvSubjectCurriculumSyllabusList', filterExpression, function (result) {
                $("#tmplListSubjectCurriculumSyllabus").tmpl(result).appendTo($tbl);

                if (isUsingCode == '0') {
                    $tbl.find('tr:gt(0)').each(function () {
                        $(this).find('.tdCode').attr('style', 'display:none');
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
                            var idx = $('#ulContainerSubjectCurriculum li').index($li) + 1;
                            if (idx == $('#ulContainerSubjectCurriculum li').length) {
                                $('#ulContainerSubjectCurriculum').append('<li>');
                            }

                            $('#ulContainerSubjectCurriculum li:gt(' + idx + ')').remove();

                            $li = $('#ulContainerSubjectCurriculum li:eq(' + idx + ')');
                            $li.html('');
                            $panel = $($('#tmplSubjectCurriculum').html());
                            $li.append($panel);

                            $li.find('.hdnCurriculumSyllabusID').val(id);

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
        #ulContainerSubjectCurriculum li                        { padding: 5px; width: 450px; height: 400px; border-right: 1px solid #EAEAEA; display: inline-table; list-style-type: none; white-space: normal; }
        #ulContainerSubjectCurriculum li:first-child            { border-left: 1px solid #EAEAEA; }
        
        .tblSubjectCurriculumSyllabus             { border-collapse:collapse; table-layout:fixed; width: 440px; }
        .tdName div,         
        .tdReference div         { -ms-word-break: keep-all;word-break: keep-all;-webkit-hyphens: auto;-moz-hyphens: auto;hyphens: auto;max-width: 380px; }
    </style>
    <input type="hidden" id="hdnSubjectID" runat="server" />
    <input type="hidden" id="hdnEntryID" runat="server" />
    <input type="hidden" id="hdnIsPerSchoolPeriodSection" runat="server" />
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
            <tr id="trSchoolPeriodSection" runat="server" style="display:none">
                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Semester")%></label></td>
                <td>
                    <select id="cboSchoolPeriodSection" runat="server" style="width:200px"></select>
                </td>
            </tr> 
        </table>
    </fieldset>
    <script id="tmplListSubjectCurriculumSyllabus" type="text/x-jquery-tmpl">
        <tr class="trSubjectCurriculumSyllabus">
            <td class="tdCode"><div>${SubjectCurriculumSyllabusCode}</div></td>
            <td class="tdName"><div>${SubjectCurriculumSyllabusName}</div></td>
            <td>
                <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("View")%></div>
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
        <input type="hidden" class="hdnCurriculumSyllabusID" value=""/>
        <table class="tblSubjectCurriculumSyllabus grdSelected" rules="all">
            <tr>
                <th class="thCode" style="width: 80px">Kode</th>
                <th>Teks</th>
                <th style="width: 80px"></th>
            </tr>
        </table>
        <br style="clear: both" />
    </script>
    <ul id="ulContainerSubjectCurriculum">
        <li>&nbsp;</li>
    </ul>
</asp:Content>