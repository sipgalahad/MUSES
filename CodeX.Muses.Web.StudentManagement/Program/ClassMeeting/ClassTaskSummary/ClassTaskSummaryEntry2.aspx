<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassTaskSummaryEntry2.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskSummaryEntry2" %>

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
            var GCSubjectMarkType = $('#<%=hdnGCSubjectMarkType.ClientID %>').val();
            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.txtFinalMarkPercentageTheory').each(function () {
                    var classTaskID = $(this).parent().find('.hdnClassSubjectTaskID').val();
                    var value = parseFloat($(this).val());
                    if (result != '')
                        result += '|';
                    result += classTaskID + ',' + value;
                });
                $('.txtFinalMarkPercentagePractice').each(function () {
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
                    var tempResult = '';
                    var cboStudentProgressRule = eval('cboStudentProgressRule' + idx);
                    var studentProgressRuleDtID = cboStudentProgressRule.GetValue();
                    if (studentProgressRuleDtID == null || studentProgressRuleDtID == "0")
                        studentProgressRuleDtID = "";

                    $(this).find('.txtStudentMarkTheory').each(function () {
                        var value = '';
                        var positiontag = $(this).attr('positiontag');
                        switch (GCSubjectMarkType) {
                            case '<%=OnGetSubjectMarkTypeNumber() %>': value = $(this).val(); break;
                            case '<%=OnGetSubjectMarkTypeOption() %>':
                                var cboStudentMarkOption = eval('cboStudentMarkOptionTheory' + positiontag);
                                if (cboStudentMarkOption.GetValue() != null && cboStudentMarkOption.GetValue() != '0')
                                    value = cboStudentMarkOption.GetValue(); break;
                            case '<%=OnGetSubjectMarkTypeText() %>': value = $(this).parent().find('.txtStudentMarkTheoryDescription').val(); break;
                        }

                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += value;
                    });

                    var tempResult2 = '';
                    $(this).find('.txtFinalStudentMarkTheoryGroup').each(function () {
                        var value = $(this).val();
                        if (tempResult2 != '')
                            tempResult2 += ',';
                        tempResult2 += $(this).attr('formuladtid') + ')' + value;
                    });

                    $(this).find('.txtStudentMarkPractice').each(function () {
                        var value = '';
                        var positiontag = $(this).attr('positiontag');
                        switch (GCSubjectMarkType) {
                            case '<%=OnGetSubjectMarkTypeNumber() %>': value = $(this).val(); break;
                            case '<%=OnGetSubjectMarkTypeOption() %>':
                                var cboStudentMarkOption = eval('cboStudentMarkOptionPractice' + positiontag);
                                if (cboStudentMarkOption.GetValue() != null)
                                    value = cboStudentMarkOption.GetValue(); break;
                            case '<%=OnGetSubjectMarkTypeText() %>': value = $(this).parent().find('.txtStudentMarkPracticeDescription').val(); break;
                        }

                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += value;
                    });

                    $(this).find('.txtFinalStudentMarkPracticeGroup').each(function () {
                        var value = $(this).val();
                        if (tempResult2 != '')
                            tempResult2 += ',';
                        tempResult2 += $(this).attr('formuladtid') + ')' + value;
                    });
                    if (result != '')
                        result += '|';
                    result += $(this).find('.keyField').html() + '*' + $(this).find('.txtFinalStudentMarkTheory').val() + '*' + $(this).find('.txtFinalStudentMarkPractice').val() + '*' + $(this).find('.txtAffectiveMark').val() + '*' + $(this).find('.txtAffectiveDescription').val() + '*' + studentProgressRuleDtID + '*' + $(this).find('.txtProgressDescription').val() + '*' + tempResult + '*' + tempResult2;
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
                setTotalPercentageTheory();
                setTotalPercentagePractice();
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
        var lstFinalMarkPercentageTheory = [];
        function setTotalPercentageTheory() {
            $('.txtFinalMarkPercentageTheory').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentageTheory.push(value);
            });
            $('.txtAverageFinalMarkPercentageTheory').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $('.txtFinalMarkPercentageTheory[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    totalGroup += value;
                });
                $(this).val(totalGroup);
            });
            $('.trDetail').each(function () {
                setStudentGroupMarkTheory($(this));
            });
        }

        $('.txtFinalMarkPercentageTheory').live('change', function () {
            var idx = $(this).index('.txtFinalMarkPercentageTheory');
            lstFinalMarkPercentageTheory[idx] = parseFloat($(this).val());
            setTotalPercentageTheory();
        });

        $('.txtStudentMarkTheory').live('change', function () {
            setStudentGroupMarkTheory($(this).closest('.trDetail'));
        });

        function setStudentGroupMarkTheory($tr) {
            var ctr = 0;

            $tr.find('.txtAverageStudentMarkTheoryGroup').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $tr.find('.txtStudentMarkTheory[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    var ctr = $tr.find('.txtStudentMarkTheory').index($(this));
                    totalGroup += value * lstFinalMarkPercentageTheory[ctr] / 100;
                });

                $(this).val(totalGroup);
                if (!isOnLoad) {
                    $txtFinal = $(this).parent().next().find('.txtFinalStudentMarkTheoryGroup');
                    $txtFinal.val(totalGroup);
                    $txtFinal.change();
                }
            });
            setStudentFinalMarkTheory($tr);
        }

        function setStudentFinalMarkTheory($tr) {
            var total = 0;
            $tr.find('.txtFinalStudentMarkTheoryGroup').each(function () {
                var formulaPercentage = parseFloat($(this).attr('formulapercentage'));
                total += $(this).val() * formulaPercentage / 100;
            });

            $tr.find('.txtTotalStudentMarkTheory').val(total);
            if (!isOnLoad) {
                $tr.find('.txtFinalStudentMarkTheory').val(total);
                $tr.find('.txtFinalStudentMarkTheory').change();
            }
        }

        $('.txtFinalStudentMarkTheoryGroup').live('change', function () {
            $tr = $(this).closest('.trDetail');
            setStudentFinalMarkTheory($tr);
        });
        //#endregion

        //#region Practice
        var lstFinalMarkPercentagePractice = [];
        function setTotalPercentagePractice() {
            $('.txtFinalMarkPercentagePractice').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentagePractice.push(value);
            });
            $('.txtAverageFinalMarkPercentagePractice').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $('.txtFinalMarkPercentagePractice[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    totalGroup += value;
                });
                $(this).val(totalGroup);
            });
            $('.trDetail').each(function () {
                setStudentGroupMarkPractice($(this));
            });
        }

        $('.txtFinalMarkPercentagePractice').live('change', function () {
            var idx = $(this).index('.txtFinalMarkPercentagePractice');
            lstFinalMarkPercentagePractice[idx] = parseFloat($(this).val());
            setTotalPercentagePractice();
        });

        $('.txtStudentMarkPractice').live('change', function () {
            setStudentGroupMarkPractice($(this).closest('.trDetail'));
        });

        function setStudentGroupMarkPractice($tr) {
            var ctr = 0;

            $tr.find('.txtAverageStudentMarkPracticeGroup').each(function () {
                var formulaDtID = $(this).attr('formuladtid');
                var totalGroup = 0;
                $tr.find('.txtStudentMarkPractice[formuladtid="' + formulaDtID + '"]').each(function () {
                    var value = 0;
                    if ($(this).val() != "-" && $(this).val() != "")
                        value = parseFloat($(this).val());
                    var ctr = $tr.find('.txtStudentMarkPractice').index($(this));
                    totalGroup += value * lstFinalMarkPercentagePractice[ctr] / 100;
                });

                $(this).val(totalGroup);
                if (!isOnLoad) {
                    $txtFinal = $(this).parent().next().find('.txtFinalStudentMarkPracticeGroup');
                    $txtFinal.val(totalGroup);
                    $txtFinal.change();
                }
            });
            setStudentFinalMarkPractice($tr);
        }

        function setStudentFinalMarkPractice($tr) {
            var total = 0;
            $tr.find('.txtFinalStudentMarkPracticeGroup').each(function () {
                var formulaPercentage = parseFloat($(this).attr('formulapercentage'));
                total += $(this).val() * formulaPercentage / 100;
            });

            $tr.find('.txtTotalStudentMarkPractice').val(total);
            if (!isOnLoad) {
                $tr.find('.txtFinalStudentMarkPractice').val(total);
                $tr.find('.txtFinalStudentMarkPractice').change();
            }
        }

        $('.txtFinalStudentMarkPracticeGroup').live('change', function () {
            $tr = $(this).closest('.trDetail');
            setStudentFinalMarkPractice($tr);
        });
        //#endregion

        //#region Progress Description
        $('.txtFinalStudentMarkTheory').live('change', function () {
            var value = parseFloat($(this).val());
            var idx = $(this).attr('itemindex');
            $tr = $('.trDetail:eq(' + idx + ')');
            var cboStudentProgressRule = eval('cboStudentProgressRule' + idx);
            var lstProgress = $('#<%=hdnListProgress.ClientID %>').val().split('|');
            for (var i = 0; i < lstProgress.length; ++i) {
                var temp = lstProgress[i].split(';');
                if (value >= parseFloat(temp[1]) && value <= parseFloat(temp[2])) {
                    var studentName = $tr.find('.hdnPreferredName').val();
                    $tr.find('.txtProgressDescription').val(temp[3].replace('{NamaSiswa}', studentName));
                    cboStudentProgressRule.SetValue(temp[0]);
                }
            }
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
    <input type="hidden" id="hdnListSaveHeaderValue" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnListProgress" runat="server" />
    <input type="hidden" id="hdnIsMainTeacher" runat="server" />
    <input type="hidden" id="hdnGCClassStudyType" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnGCSubjectMarkType" runat="server" />
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
                <th id="thTheory" runat="server" class="thCenter"><%=GetLabel("KOGNITIF / PENGETAHUAN") %></th>
                <th id="thPractice" runat="server" class="thCenter"><%=GetLabel("PSIKOMOTORIK / PRAKTEK") %></th>
                <th id="thAffective" runat="server" colspan="2" class="thCenter"><%=GetLabel("Afektif") %></th>
                <th colspan="2" class="thCenter"><%=GetLabel("Deskripsi Kemajuan Bljr") %></th>
            </tr>
            <tr> 
                <asp:Repeater ID="rptHeaderTheoryTaskGroup" runat="server" OnItemDataBound="rptHeaderTheoryTaskGroup_ItemDataBound">
                    <ItemTemplate>
                        <th class="thCenter" id="thHeaderTheoryTaskGroup" runat="server">
                            <%#Eval("TheoryFinalMarkFormulaDtName")%> <br /><%#Eval("TheoryFinalMarkPercentage")%> [%]
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <th id="thFinalReadonlyMarkTheory" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter">
                    <%=GetLabel("Total") %><br />
                    <span id="spnTotalTheoryPercentage" runat="server"></span> [%]
                </th>
                <th id="thFinalMarkTheory" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter"><%=GetLabel("Nilai Rapor") %></th>
                <asp:Repeater ID="rptHeaderPracticeTaskGroup" runat="server" OnItemDataBound="rptHeaderPracticeTaskGroup_ItemDataBound">
                    <ItemTemplate>
                        <th class="thCenter" id="thHeaderPracticeTaskGroup" runat="server">
                            <%#Eval("PracticeFinalMarkFormulaDtName")%> <br /><%#Eval("PracticeFinalMarkPercentage")%> [%]
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <th id="thFinalReadonlyMarkPractice" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter">
                    <%=GetLabel("Total") %><br />
                    <span id="spnTotalPracticePercentage" runat="server"></span> [%]
                </th>
                <th id="thFinalMarkPractice" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter"><%=GetLabel("Nilai Rapor")%></th>
                <th id="thAffectiveMark" runat="server" class="thCenter" rowspan="2" style="width:40px"><%=GetLabel("Nilai") %></th>
                <th id="thAffectiveDescription" runat="server" class="thCenter" rowspan="2" style="width:200px"><%=GetLabel("Deskripsi") %></th>
                <th class="thCenter" rowspan="2" style="width:80px"><%=GetLabel("Kriteria") %></th>
                <th class="thCenter" rowspan="2" style="width:200px"><%=GetLabel("Deskripsi") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeaderTheoryGroup" runat="server" OnItemDataBound="rptHeaderTheoryGroup_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater ID="rptHeaderTheory" runat="server">
                            <ItemTemplate>
                                <th class="thCenter" style="width:90px">
                                    <%#Eval("ClassTaskCode")%><br />
                                    <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                                    <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" formuladtid='<%#Eval("TheoryFinalMarkFormulaDtID") %>' class="number txtFinalMarkPercentageTheory" />[%]
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                        <th id="thAverageMarkTheory" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                            <%=GetLabel("Rata-Rata") %><br />
                            <input type="text" class="txtAverageFinalMarkPercentageTheory number" formuladtid='<%#Eval("TheoryFinalMarkFormulaDtID") %>' readonly="readonly" style="width:30px" class="number" />[%]
                        </th>
                        <th id="thFinalMarkTheory" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                            <%=GetLabel("Nilai") %><br />
                        </th>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Repeater ID="rptHeaderPracticeGroup" runat="server" OnItemDataBound="rptHeaderPracticeGroup_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater ID="rptHeaderPractice" runat="server">
                            <ItemTemplate>
                                <th class="thCenter" style="width:90px">
                                    <%#Eval("ClassTaskCode")%><br />
                                    <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                                    <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" formuladtid='<%#Eval("PracticeFinalMarkFormulaDtID") %>' class="number txtFinalMarkPercentagePractice" />[%]
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                        <th id="thAverageMarkPractice" runat="server" class="thCenter" style="width:65px; background-color:#B9EB33">
                            <%=GetLabel("Rata-Rata") %><br />
                            <input type="text" class="txtAverageFinalMarkPercentagePractice number" formuladtid='<%#Eval("PracticeFinalMarkFormulaDtID") %>' readonly="readonly" style="width:30px" class="number" />[%]
                        </th>
                        <th id="thFinalMarkPractice" runat="server" class="thCenter" style="width:65px; background-color:#B9EB33">
                            <%=GetLabel("Nilai") %><br />
                        </th>
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
                        <asp:Repeater ID="rptStudentMarkTheoryGroup" runat="server" OnItemDataBound="rptStudentMarkTheoryGroup_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptStudentMarkTheory" runat="server" OnItemDataBound="rptStudentMarkTheory_ItemDataBound">
                                    <ItemTemplate>
                                        <td align="center">
                                            <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                            <div id="divMark" runat="server">
                                                <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMarkTheory" Text="" Width="60px" />&nbsp;<b id="bIsRemedial" class="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                            </div>
                                            <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                            <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkTheoryDescription" Text="" Width="390px" />                         
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td align="center" id="tdAverageStudentMarkTheoryGroup" runat="server"><input class="txtAverageStudentMarkTheoryGroup number" formulapercentage='<%#Eval("TheoryFinalMarkPercentage") %>' formuladtid='<%#Eval("TheoryFinalMarkFormulaDtID") %>' readonly="readonly" style="width:60px" /></td>
                                <td align="center" id="tdFinalStudentMarkTheoryGroup" runat="server"><asp:TextBox ID="txtFinalStudentMarkTheoryGroup" runat="server" CssClass="number txtFinalStudentMarkTheoryGroup" Text="" Width="60px" /></td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center" id="tdTotalStudentMarkTheory" runat="server"><input class="txtTotalStudentMarkTheory number" readonly="readonly" style="width:90%" /></td>
                        <td align="center" id="tdFinalStudentMarkTheory" runat="server"><asp:TextBox ID="txtFinalStudentMarkTheory" CssClass="txtFinalStudentMarkTheory number" Text="-" runat="server" Width="90%" /></td>
                        
                        <asp:Repeater ID="rptStudentMarkPracticeGroup" runat="server" OnItemDataBound="rptStudentMarkPracticeGroup_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptStudentMarkPractice" runat="server" OnItemDataBound="rptStudentMarkPractice_ItemDataBound">
                                    <ItemTemplate>
                                        <td align="center">
                                            <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                            <div id="divMark" runat="server">
                                                <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMarkPractice" Text="" Width="60px" />&nbsp;<b id="bIsRemedial" class="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                            </div>
                                            <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                            <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkPracticeDescription" Text="" Width="390px" />                         
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td align="center" id="tdAverageStudentMarkPracticeGroup" runat="server"><input class="txtAverageStudentMarkPracticeGroup number" formulapercentage='<%#Eval("PracticeFinalMarkPercentage") %>' formuladtid='<%#Eval("PracticeFinalMarkFormulaDtID") %>' readonly="readonly" style="width:60px" /></td>
                                <td align="center" id="tdFinalStudentMarkTheoryGroup" runat="server"><asp:TextBox ID="txtFinalStudentMarkPracticeGroup" runat="server" CssClass="number txtFinalStudentMarkPracticeGroup" Text="" Width="60px" /></td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center" id="tdTotalStudentMarkPractice" runat="server"><input class="txtTotalStudentMarkPractice number" readonly="readonly" style="width:90%" /></td>
                        <td align="center" id="tdFinalStudentMarkPractice" runat="server"><asp:TextBox ID="txtFinalStudentMarkPractice" CssClass="txtFinalStudentMarkPractice number" Text="-" runat="server" Width="90%" /></td>
                        
                        <td align="center" id="tdStudentAffectiveMark" runat="server"><asp:TextBox ID="txtAffectiveMark" CssClass="txtAffectiveMark" runat="server" Width="90%" /></td>
                        <td align="center" id="tdStudentAffectiveDescription" runat="server"><asp:TextBox ID="txtAffectiveDescription" CssClass="txtAffectiveDescription" runat="server" Width="90%" /></td>
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