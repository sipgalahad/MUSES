<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassTaskSummaryEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskSummaryEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnSave" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/save.png")%>' alt="" /><div><%=GetLabel("Save")%></div></li>
    <li id="btnApprove" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Approve")%></div></li>
    <li id="btnReopen" runat="server" CRUDMode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/redo.png")%>' alt="" /><div><%=GetLabel("Reopen")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        var isOnLoad = true;
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.txtFinalMarkPercentage').each(function () {
                    var classTaskID = $(this).parent().find('.hdnClassSubjectTaskID').val();
                    var value = parseFloat($(this).val());
                    if (result != '')
                        result += '|';
                    result += classTaskID + ',' + value;
                });
                $('#<%=hdnListSaveHeaderValue.ClientID %>').val(result);

                result = '';
                var idx = 0;
                $('.trDetail').each(function () {
                    var cboStudentProgressRule = eval('cboStudentProgressRule' + idx);
                    var studentProgressRuleDtID = cboStudentProgressRule.GetValue();
                    if (studentProgressRuleDtID == null || studentProgressRuleDtID == "0")
                        studentProgressRuleDtID = "";

                    $tr = $(this);
                    var tempResult1 = '';
                    $(this).find('.hdnCurriculumMarkTypeID').each(function () {
                        var curriculumMarkTypeID = $(this).val();
                        $td = $(this).parent();

                        var itemIndex = $td.find('.hdnItemIndex').val();
                        var taskGCMarkType = $td.find('.hdnTaskGCMarkType').val();
                        var finalGCMarkType = $td.find('.hdnFinalGCMarkType').val();
                        var predicateGCMarkType = $td.find('.hdnPredicateGCMarkType').val();
                        $txtFinalStudentMark = $td.find('.txtFinalStudentMark');
                        var positiontag = $txtFinalStudentMark.attr('positiontag');

                        var finalMark = '';
                        switch (finalGCMarkType) {
                            case '<%=OnGetSubjectMarkTypeNumber() %>':
                                finalMark = $txtFinalStudentMark.val(); break;
                            case '<%=OnGetSubjectMarkTypeOption() %>':
                                var cboFinalStudentMarkOption = eval('cboFinalStudentMarkOption' + positiontag);
                                if (cboFinalStudentMarkOption.GetValue() != null && cboFinalStudentMarkOption.GetValue() != '0')
                                    finalMark = cboFinalStudentMarkOption.GetValue(); break;
                            case '<%=OnGetSubjectMarkTypeText() %>':
                                $txtFinalStudentMarkDescription = $td.find('.txtFinalStudentMarkDescription');
                                finalMark = $txtFinalStudentMark.val(); break;
                        }
                        var cboPredicateStudentMarkOption = eval('cboPredicateStudentMarkOption' + positiontag);
                        var predicateMark = '';
                        if (cboPredicateStudentMarkOption.GetValue() != null && cboPredicateStudentMarkOption.GetValue() != '0')
                            predicateMark = cboPredicateStudentMarkOption.GetValue();
                        var tempResult2 = '';
                        $tr.find('td[curriculummarktypeid="' + curriculumMarkTypeID + '"]').each(function () {
                            var value = '';
                            $txtStudentMark = $(this).find('.txtStudentMark');
                            var positiontag = $txtStudentMark.attr('positiontag');
                            switch (taskGCMarkType) {
                                case '<%=OnGetSubjectMarkTypeNumber() %>':
                                    value = $txtStudentMark.val(); break;
                                case '<%=OnGetSubjectMarkTypeOption() %>':
                                    var cboStudentMarkOption = eval('cboStudentMarkOption' + positiontag);
                                    if (cboStudentMarkOption.GetValue() != null && cboStudentMarkOption.GetValue() != '0')
                                        value = cboStudentMarkOption.GetValue(); break;
                                case '<%=OnGetSubjectMarkTypeText() %>': value = $(this).find('.txtStudentMarkTheoryDescription').val(); break;
                            }
                            if (tempResult2 != '')
                                tempResult2 += ',';
                            tempResult2 += value;
                        });
                        if (tempResult1 != '')
                            tempResult1 += ';';
                        tempResult1 += curriculumMarkTypeID + '(' + taskGCMarkType + '(' + finalGCMarkType + '(' + predicateGCMarkType + '(' + finalMark + '(' + predicateMark + '(' + tempResult2;
                    });

                    var tempResult2 = '';
                    $(this).find('.txtFinalStudentMarkGroup').each(function () {
                        var value = $(this).val();
                        if (tempResult2 != '')
                            tempResult2 += ',';
                        tempResult2 += $(this).attr('formuladtid') + ')' + value;
                    });
                    if (result != '')
                        result += '|';
                    result += $(this).find('.keyField').html() + '*' + tempResult1 + '*' + tempResult2;
                    idx++;
                });
                $('#<%=hdnListSaveValue.ClientID %>').val(result);
                onCustomButtonClick('save');
            });

            if ($('#<%=hdnGCTransactionStatus.ClientID %>').val() == "<%=OnGetTransactionStatusApproved() %>") {
                showWatermark('APPROVED');
            }

            $('#<%=btnApprove.ClientID %>').click(function () {
                onCustomButtonClick('approve');
            });

            $('#<%=btnReopen.ClientID %>').click(function () {
                onCustomButtonClick('reopen');
            });

            setStudentImage();

            var width = parseInt('<%=OnGetTableViewWidth() %>');
            //if (width < 1250)
            //width = 1250;
            $('#tblView').width(width);

            setTimeout(function () {
                setTotalPercentage();
                isOnLoad = false;
            }, 500);
        });

        $('.lblStudent').live('click', function () {
            var id = $(this).closest('table').parent().closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskSummary/StudentSummaryViewDtCtl.ascx");
            openUserControlPopup(url, id, 'Riwayat Siswa', 800, 450);
        });

        $('.bIsRemedial').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + '|' + $(this).attr('ClassSubjectTaskID');
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskSummary/StudentRemedialMarkViewDtCtl.ascx");
            openUserControlPopup(url, id, 'Remidi', 800, 450);
        });

        function onAfterCustomClickSuccess(type) {
            if (type == 'approve') {
                $('#<%=btnApprove.ClientID %>').hide();
                $('#<%=btnReopen.ClientID %>').show();
                $('#<%=btnSave.ClientID %>').hide();
                showWatermark('APPROVED');
            }
            else {
                $('#<%=btnApprove.ClientID %>').show();
                $('#<%=btnReopen.ClientID %>').hide();
                $('#<%=btnSave.ClientID %>').show();
                hideWatermark();
            }
        }

        //#region Theory
        var lstFinalMarkPercentage = [];
        function setTotalPercentage() {
            $('.txtFinalMarkPercentage').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentage.push(value);
            });
            $('.txtAverageFinalMarkPercentage').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $('.txtFinalMarkPercentage[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    totalGroup += value;
                });
                $(this).val(totalGroup);
            });
            $('.trDetail').each(function () {
                setStudentGroupMark($(this));
            });
        }

        $('.txtFinalMarkPercentage').live('change', function () {
            var idx = $(this).index('.txtFinalMarkPercentage');
            lstFinalMarkPercentage[idx] = parseFloat($(this).val());
            setTotalPercentage();
        });

        $('.txtStudentMark').live('change', function () {
            setStudentGroupMark($(this).closest('.trDetail'));
        });

        function setStudentGroupMark($tr) {
            var ctr = 0;

            $tr.find('.txtAverageStudentMarkGroup').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $tr.find('.txtStudentMark[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    var ctr = $tr.find('.txtStudentMark').index($(this));
                    totalGroup += value * lstFinalMarkPercentage[ctr] / 100;
                });

                $(this).val(totalGroup);
                if (!isOnLoad) {
                    $txtFinal = $(this).parent().next().find('.txtFinalStudentMarkGroup');
                    $txtFinal.val(totalGroup);
                    //$txtFinal.change();
                }
            });
            setStudentFinalMark($tr);
        }

        function setStudentFinalMark($tr) {
            $tr.find('.txtFinalStudentMark').each(function () {
                var total = 0;
                $td = $(this).parent();
                var curriculummarktypeid = $(this).attr("curriculummarktypeid");
                $tr.find('.txtFinalStudentMarkGroup[curriculummarktypeid="' + curriculummarktypeid + '"]').each(function () {
                    var formulaPercentage = parseFloat($(this).attr('formulapercentage'));
                    total += $(this).val() * formulaPercentage / 100;
                });
                $tr.find('.txtTotalStudentMark[curriculummarktypeid="' + curriculummarktypeid + '"]').val(total);
                if (!isOnLoad) {
                    var taskMarkTypeID = $td.find('.hdnTaskMarkTypeID').val();
                    var finalMarkTypeID = $td.find('.hdnFinalMarkTypeID').val();
                    var predicateMarkTypeID = $td.find('.hdnPredicateMarkTypeID').val();
                    var taskGCMarkType = $td.find('.hdnTaskGCMarkType').val();
                    var finalGCMarkType = $td.find('.hdnFinalGCMarkType').val();
                    var predicateGCMarkType = $td.find('.hdnPredicateGCMarkType').val();
                    var isAllowTask = $td.find('.hdnIsAllowTask').val() == 'True';

                    if (isAllowTask) {
                        if (taskMarkTypeID == finalMarkTypeID) {
                            $(this).val(total);
                            $(this).change();
                        }
                        else {
                            if (finalGCMarkType == '<%=OnGetSubjectMarkTypeOption() %>') {
                                var positiontag = $(this).attr('positiontag');
                                var cboFinalStudentMarkOption = eval('cboFinalStudentMarkOption' + positiontag);
                                var lstMarkTypeFormula = $('#<%=hdnListMarkTypeFormula.ClientID %>').val().split('|');
                                for (var i = 0; i < lstMarkTypeFormula.length; ++i) {
                                    var temp = lstMarkTypeFormula[i].split(';');
                                    if (temp[0] == finalMarkTypeID && temp[1] == taskMarkTypeID) {
                                        if (taskGCMarkType == '<%=OnGetSubjectMarkTypeNumber() %>') {
                                            if (total >= parseFloat(temp[2]) && total <= parseFloat(temp[3])) {
                                                cboFinalStudentMarkOption.SetValue(temp[5]);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (predicateMarkTypeID != '0') {
                            if (finalGCMarkType == '<%=OnGetSubjectMarkTypeOption() %>') {
                                var positiontag = $(this).attr('positiontag');
                                var cboFinalStudentMarkOption = eval('cboFinalStudentMarkOption' + positiontag);
                                var cboPredicateStudentMarkOption = eval('cboPredicateStudentMarkOption' + positiontag);

                                var value = cboFinalStudentMarkOption.GetValue();
                                var lstMarkTypeFormula = $('#<%=hdnListMarkTypeFormula.ClientID %>').val().split('|');
                                for (var i = 0; i < lstMarkTypeFormula.length; ++i) {
                                    var temp = lstMarkTypeFormula[i].split(';');
                                    if (temp[0] == predicateMarkTypeID && temp[1] == finalMarkTypeID) {
                                        if (finalGCMarkType == '<%=OnGetSubjectMarkTypeNumber() %>') {
                                            if (value >= parseFloat(temp[2]) && value <= parseFloat(temp[3])) {
                                                cboPredicateStudentMarkOption.SetValue(temp[5]);
                                                break;
                                            }
                                        }
                                        else if (finalGCMarkType == '<%=OnGetSubjectMarkTypeOption() %>') {
                                            if (value == temp[4]) {
                                                cboPredicateStudentMarkOption.SetValue(temp[5]);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        $('.txtFinalStudentMarkGroup').live('change', function () {
            $tr = $(this).closest('.trDetail');
            setStudentFinalMark($tr);
        });
        //#endregion

        //#region Progress Description
        $('.txtFinalStudentMark').live('change', function () {
            var value = parseFloat($(this).val());
            var idx = $(this).attr('itemindex');
            $tr = $('.trDetail:eq(' + idx + ')');
            /*var cboStudentProgressRule = eval('cboStudentProgressRule' + idx);
            var lstProgress = $('#<%=hdnListProgress.ClientID %>').val().split('|');
            for (var i = 0; i < lstProgress.length; ++i) {
                var temp = lstProgress[i].split(';');
                if (value >= parseFloat(temp[1]) && value <= parseFloat(temp[2])) {
                    var studentName = $tr.find('.hdnPreferredName').val();
                    $tr.find('.txtProgressDescription').val(temp[3].replace('{NamaSiswa}', studentName));
                    cboStudentProgressRule.SetValue(temp[0]);
                }
            }*/
        });

        function onCboStudentProgressRuleValueChanged(s, idx, studentName) {
            $tr = $('.trDetail:eq(' + idx + ')');
            var value = s.GetValue();
            var lstProgress = $('#<%=hdnListProgress.ClientID %>').val().split('|');
            for (var i = 0; i < lstProgress.length; ++i) {
                var temp = lstProgress[i].split(';');
                if (temp[0] == value) {
                    $tr.find('.txtProgressDescription').val(temp[3].replace('{NamaSiswa}', studentName));
                }
            }
        }
        //#endregion
    </script>
    <style type="text/css">
        .bIsRemedial                { cursor: pointer; }
        .bIsRemedial:hover          { text-decoration: underline; }
    </style>
    <input type="hidden" id="hdnListMarkTypeFormula" runat="server" />
    <input type="hidden" id="hdnTableWidth" runat="server" />
    <input type="hidden" id="hdnListSaveHeaderValue" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnListProgress" runat="server" />
    <input type="hidden" id="hdnIsMainTeacher" runat="server" />
    <input type="hidden" id="hdnGCClassStudyType" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnGCTransactionStatus" runat="server" />
    <input type="hidden" id="hdnCompetencyStandard" runat="server" />
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
            <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
        </tr>
    </table>
    <div style="width:1250px; overflow-x: auto;">
        <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent" id="tblView">
            <tr>
                <th rowspan="3" style="width:300px"><%=GetLabel("Siswa") %></th>
                <asp:Repeater ID="rptHeaderMarkType1" runat="server" OnItemDataBound="rptHeaderMarkType1_ItemDataBound">
                    <ItemTemplate>
                        <th class="thCenter" id="thHeader" runat="server"><%#Eval("CurriculumMarkTypeName") %></th>
                    </ItemTemplate>
                </asp:Repeater>
                <th colspan="2" class="thCenter"><%=GetLabel("Deskripsi Kemajuan Bljr") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeaderMarkType2" runat="server" OnItemDataBound="rptHeaderMarkType2_ItemDataBound">
                    <ItemTemplate> 
                        <asp:Repeater ID="rptHeaderMarkType2Dt" runat="server" OnItemDataBound="rptHeaderMarkType2Dt_ItemDataBound">
                            <ItemTemplate>
                                <th class="thCenter" id="thHeaderTaskGroup" runat="server">
                                    <%#Eval("CurriculumFinalMarkFormulaDtName")%> <br /><%#Eval("FinalMarkPercentage")%> [%]
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>                        
                        <th id="thFinalReadonlyMark" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter">
                            <%=GetLabel("Total") %><br />
                            <span id="spnTotalPercentage" runat="server"></span> [%]
                        </th>
                        <th id="thFinalMark" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter"><%=GetLabel("Nilai Rapor") %></th>
                        <th id="thPredicateMark" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter"><%=GetLabel("Predikat") %></th>
                    </ItemTemplate>
                </asp:Repeater>
                <th class="thCenter" rowspan="2" style="width:80px"><%=GetLabel("Kriteria") %></th>
                <th class="thCenter" rowspan="2" style="width:200px"><%=GetLabel("Deskripsi") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeaderMarkType3" runat="server" OnItemDataBound="rptHeaderMarkType3_ItemDataBound">
                    <ItemTemplate> 
                        <asp:Repeater ID="rptHeaderMarkType3Dt1" runat="server" OnItemDataBound="rptHeaderMarkType3Dt1_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptHeaderMarkType3Dt2" runat="server">
                                    <ItemTemplate>
                                        <th class="thCenter" style="width:90px">
                                            <%#Eval("ClassTaskCode")%><br />
                                            <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                                            <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" class="number txtFinalMarkPercentage" formuladtid='<%#DataBinder.Eval(Container,"Parent.Parent.DataItem.CurriculumFinalMarkFormulaDtID")%>' />[%]
                                        </th>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <th id="thAverageMark" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                                    <%=GetLabel("Rata-Rata") %><br />
                                    <input type="text" class="txtAverageFinalMarkPercentage number" formuladtid='<%#Eval("CurriculumFinalMarkFormulaDtID") %>' readonly="readonly" style="width:30px" class="number" />[%]
                                </th>
                                <th id="thFinalMark" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                                    <%=GetLabel("Nilai") %><br />
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                <ItemTemplate>
                    <tr class="trDetail">
                        <td class="keyField"><%#Eval("StudentID") %></td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 35px;">
                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                        <div class="gridCircle divStudentImage"></div>
                                    </td>
                                    <td>
                                        <label class="lblLink lblStudent"><%#Eval("StudentName") %></label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <asp:Repeater ID="rptStudentMarkType" runat="server" OnItemDataBound="rptStudentMarkType_ItemDataBound">
                            <ItemTemplate> 
                                <asp:Repeater ID="rptStudentMarkGroup" runat="server" OnItemDataBound="rptStudentMarkGroup_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                            <ItemTemplate>
                                                <td align="center" curriculummarktypeid='<%#DataBinder.Eval(Container,"Parent.Parent.Parent.Parent.DataItem.CurriculumMarkTypeID")%>'>
                                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                                    <div id="divMark" runat="server">
                                                        <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMark" Text="" Width="60px" />&nbsp;<b id="bIsRemedial" class="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                                    </div>
                                                    <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                                    <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkDescription" Text="" Width="390px" />                         
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td align="center" id="tdAverageStudentMarkGroup" runat="server"><input class="txtAverageStudentMarkGroup number" formulapercentage='<%#Eval("FinalMarkPercentage") %>' formuladtid='<%#Eval("CurriculumFinalMarkFormulaDtID") %>' readonly="readonly" style="width:60px" /></td>
                                        <td align="center" id="tdFinalStudentMarkGroup" runat="server"><asp:TextBox ID="txtFinalStudentMarkGroup" runat="server" CssClass="number txtFinalStudentMarkGroup" Text="" Width="60px" /></td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td align="center" id="tdTotalStudentMark" runat="server"><input class="txtTotalStudentMark number" curriculummarktypeid='<%#Eval("CurriculumMarkTypeID") %>' readonly="readonly" style="width:90%" /></td>
                                <td align="center" id="tdFinalStudentMark" runat="server">
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                    <input type="hidden" class="hdnCurriculumMarkTypeID" value='<%# Eval("CurriculumMarkTypeID") %>' />
                                    <input type="hidden" class="hdnTaskMarkTypeID" value='<%# Eval("TaskMarkTypeID") %>' />
                                    <input type="hidden" class="hdnFinalMarkTypeID" value='<%# Eval("FinalMarkTypeID") %>' />
                                    <input type="hidden" class="hdnPredicateMarkTypeID" value='<%# Eval("PredicateMarkTypeID") %>' />
                                    <input type="hidden" class="hdnTaskGCMarkType" value='<%# Eval("TaskGCMarkType") %>' />
                                    <input type="hidden" class="hdnFinalGCMarkType" value='<%# Eval("FinalGCMarkType") %>' />
                                    <input type="hidden" class="hdnPredicateGCMarkType" value='<%# Eval("PredicateGCMarkType") %>' />
                                    <input type="hidden" class="hdnIsAllowTask" value='<%# Eval("IsAllowTask") %>' />
                                    <asp:TextBox ID="txtFinalStudentMark" CssClass="txtFinalStudentMark number" Text="-" runat="server" Width="80px" />
                                    <dxe:ASPxComboBox ID="cboFinalStudentMarkOption" Width="80px" runat="server" />
                                    <asp:TextBox ID="txtFinalStudentMarkDescription" runat="server" CssClass="txtFinalStudentMarkDescription" Text="" Width="390px" />                         
                                </td>
                                <td align="center" id="tdPredicateStudentMark" runat="server">
                                    <dxe:ASPxComboBox ID="cboPredicateStudentMarkOption" Width="80px" runat="server" />                     
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center"><dxe:ASPxComboBox ID="cboStudentProgressRule" runat="server" Width="90%" /></td>
                        <td align="center">
                            <input type="hidden" class="hdnPreferredName" value='<%#Eval("PreferredName") %>' />
                            <asp:TextBox ID="txtProgressDescription" CssClass="txtProgressDescription" runat="server" Width="90%" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>