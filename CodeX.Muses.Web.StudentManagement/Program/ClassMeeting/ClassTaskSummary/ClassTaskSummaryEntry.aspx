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
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=hdnUploadedFile1.ClientID %>').val(e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $('.txtStudentMark').live('keydown', function (e) {
            $tr = $(this).closest('tr');
            var rowIndex = $('.trDetail').index($tr);

            var idxTxt = $tr.find('.txtStudentMark').index($(this));

            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 40) { //down
                if (rowIndex < $('.trDetail').length - 1) {
                    rowIndex++;
                    $('.trDetail:eq(' + rowIndex + ')').find('.txtStudentMark:eq(' + idxTxt + ')').focus();
                }
            }
            else if (code == 38) { //up
                if (rowIndex > 0) {
                    rowIndex--;
                    $('.trDetail:eq(' + rowIndex + ')').find('.txtStudentMark:eq(' + idxTxt + ')').focus();
                }
            }
        });

        $(function () {
            $('#btnUploadFile').click(function () {
                cbpProcess.PerformCallback('upload');
            });

            $('#<%=FileUpload1.ClientID %>').change(function () {
                readURL(this);
            });

            $('#<%=btnSave.ClientID %>').click(function () {
                var result = '';
                $('.hdnClassSubjectTaskID').each(function () {
                    var classTaskID = $(this).val();
                    if (result != '')
                        result += '|';
                    result += classTaskID + ',0';
                });
                $('#<%=hdnListSaveHeaderValue.ClientID %>').val(result);

                result = '';
                var idx = 0;
                $('.trDetail').each(function () {
                    $tr = $(this);
                    var tempResult1 = '';
                    $(this).find('.hdnCurriculumMarkTypeID').each(function () {
                        var curriculumMarkTypeID = $(this).val();
                        $td = $(this).parent();

                        var itemIndex = $td.find('.hdnItemIndex').val();
                        var taskGCMarkType = $td.find('.hdnTaskGCMarkType').val();
                        var finalGCMarkType = $td.find('.hdnFinalGCMarkType').val();
                        var predicateGCMarkType = $td.find('.hdnPredicateGCMarkType').val();
                        var GCCompetencyDescriptionType = $td.find('.hdnGCCompetencyDescriptionType').val();
                        var competencyMark = '';
                        var competencyDesc = '';

                        $txtFinalStudentMark = $td.find('.txtFinalStudentMark');
                        var positiontag = $txtFinalStudentMark.attr('positiontag');

                        if (GCCompetencyDescriptionType == '<%=OnGetCompetencyDescriptionSemester() %>') {
                            var cboCompetencyMarkType = eval('cboCompetencyMarkType' + positiontag);
                            if (cboCompetencyMarkType.GetValue() != null && cboCompetencyMarkType.GetValue() != '0')
                                competencyMark = cboCompetencyMarkType.GetValue();
                            competencyDesc = $tr.find('.txtCompetencyDescription[positiontag="' + positiontag + '"]').val();
                        }

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
                        tempResult1 += curriculumMarkTypeID + '(' + taskGCMarkType + '(' + finalGCMarkType + '(' + predicateGCMarkType + '(' + finalMark + '(' + predicateMark + '(' + competencyMark + '(' + competencyDesc + '(' + tempResult2;
                    });
                    if (result != '')
                        result += '|';
                    result += $(this).find('.keyField').html() + '*' + tempResult1;
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
            }, 1000);
        });

        $('.lblTask.lblLink').live('click', function () {
            var id = $(this).parent().find('.hdnClassSubjectTaskID').val();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskSummary/ClassTaskViewCtl.ascx");
            openUserControlPopup(url, id, 'Detil Tugas', 800, 550);
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
            setStudentFinalMark($(this).closest('.trDetail'));
        });

        function setStudentGroupMark($tr) {
            setStudentFinalMark($tr);
        }

        function setStudentFinalMark($tr) {
            var ctr = 0;
            var total = 0;
            $tr.find('.txtStudentMark').each(function () {
                var value = 0;
                if ($(this).val() != "-" && $(this).val() != "") {
                    value = parseFloat($(this).val());
                    ctr++;
                    total += value;
                }
            });
            var val = 0;
            if (ctr > 0)
                val = total / ctr;
            $tr.find('.txtTotalStudentMark').val(val);
            if (!isOnLoad || $tr.find('.txtFinalStudentMark').val() == '-') {
                $tr.find('.txtFinalStudentMark').val(val);
                $tr.find('.txtFinalStudentMark').change();
            }
        }
        //#endregion

        //#region Progress Description
        $('.txtFinalStudentMark').live('change', function () {
            var value = parseFloat($(this).val());
            var positiontag = $(this).attr('positiontag');
            $tr = $(this).closest('tr');
            $td = $(this).parent();

            var total = parseFloat($(this).val());

            var competencyMarkTypeID = $td.find('.hdnCompetencyMarkTypeID').val();
            var finalMarkTypeID = $td.find('.hdnFinalMarkTypeID').val();
            var finalGCMarkType = $td.find('.hdnFinalGCMarkType').val();

            try {
                if ($('#<%=hdnIsAutoUpdateCompetencyDescription.ClientID %>').val() == '1') {
                    var cboCompetencyMarkType = eval('cboCompetencyMarkType' + positiontag);
                    var lstMarkTypeFormula = $('#<%=hdnListMarkTypeFormula.ClientID %>').val().split('|');
                    for (var i = 0; i < lstMarkTypeFormula.length; ++i) {
                        var temp = lstMarkTypeFormula[i].split(';');
                        if (temp[0] == competencyMarkTypeID && temp[1] == finalMarkTypeID) {
                            if (finalGCMarkType == '<%=OnGetSubjectMarkTypeNumber() %>') {
                                if (total >= parseFloat(temp[2]) && total <= parseFloat(temp[3])) {
                                    cboCompetencyMarkType.SetValue(temp[5]);
                                    break;
                                }
                            }
                        }
                    }


                    var studentName = $tr.find('.hdnPreferredName').val();
                    var value = cboCompetencyMarkType.GetValue();
                    var idx2 = parseInt(positiontag.substring(2));
                    var lstProgress = $tr.find('.hdnListProgress:eq(' + idx2 + ')').val().split('|');
                    for (var i = 0; i < lstProgress.length; ++i) {
                        var temp = lstProgress[i].split(';');
                        if (temp[0] == value) {
                            $tr.find('.txtCompetencyDescription').val(temp[1].replace('{NamaSiswa}', studentName));
                        }
                    }
                }
            }
            catch (ex) {
            }
        });

        function onCboCompetencyMarkTypeValueChanged(s, idx, idx2, studentName) {
            if ($('#<%=hdnIsAutoUpdateCompetencyDescription.ClientID %>').val() == '1') {
                $tr = $('.trDetail:eq(' + idx + ')');
                var value = s.GetValue();
                var lstProgress = $tr.find('.hdnListProgress:eq(' + idx2 + ')').val().split('|');
                for (var i = 0; i < lstProgress.length; ++i) {
                    var temp = lstProgress[i].split(';');
                    if (temp[0] == value) {
                        $tr.find('.txtCompetencyDescription').val(temp[1].replace('{NamaSiswa}', studentName));
                    }
                }
            }
        }
        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'upload') {
                if (param[1] == 'fail')
                    showToast('Import Gagal', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

    </script>
    <style type="text/css">
        .bIsRemedial                { cursor: pointer; }
        .bIsRemedial:hover          { text-decoration: underline; }
    </style>
    <input type="hidden" id="hdnListMarkTypeFormula" runat="server" />
    <input type="hidden" id="hdnTableWidth" runat="server" />
    <input type="hidden" id="hdnListSaveHeaderValue" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnIsMainTeacher" runat="server" />
    <input type="hidden" id="hdnGCClassStudyType" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnGCTransactionStatus" runat="server" />
    <input type="hidden" id="hdnIsAutoUpdateCompetencyDescription" runat="server" value="1" />
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("KKM") %></td>
            <td><asp:TextBox ID="txtPassingGrade" runat="server" Width="100px" CssClass="number" ReadOnly="true" /></td>
        </tr>
        <tr>
            <td></td>
			<td>
				<input type="hidden" id="hdnFileName" runat="server" value="" />
				<input type="hidden" id="hdnUploadedFile1" runat="server" value="" />
				<asp:FileUpload ID="FileUpload1" runat="server" />
				<input type="button" id="btnUploadFile" value="Upload" />
			</td>
        </tr>
    </table>
    <div style="width:1250px; overflow-x: auto; max-height:420px; overflow-y:auto;">
        <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent" id="tblView">
            <tr>
                <th rowspan="2" style="width:300px"><%=GetLabel("Siswa") %></th>
                <asp:Repeater ID="rptHeaderMarkType1" runat="server" OnItemDataBound="rptHeaderMarkType1_ItemDataBound">
                    <ItemTemplate>
                        <th class="thCenter" id="thHeader" runat="server"><%#Eval("CurriculumMarkTypeName") %></th>
                    </ItemTemplate>
                </asp:Repeater>
                <th class="thCenter" id="thDesc" runat="server"><%=GetLabel("Deskripsi Kompetensi") %></th>
            </tr>
            <tr style="display:none">
                <asp:Repeater ID="rptHeaderMarkType2" runat="server" OnItemDataBound="rptHeaderMarkType2_ItemDataBound">
                    <ItemTemplate>                
                        <th id="thFinalReadonlyMark" runat="server" style="width:90px; background-color: #FF8837;" class="thCenter">
                            <%=GetLabel("Total") %><br />
                            <span id="spnTotalPercentage" runat="server"></span> [%]
                        </th>
                        <th id="thFinalMark" runat="server" rowspan="2" style="background-color: #FF8837;" class="thCenter"><%=GetLabel("Nilai Rapor") %></th>
                        <th id="thPredicateMark" runat="server" rowspan="2" style="width:90px; background-color: #FF8837;" class="thCenter"><%=GetLabel("Predikat") %></th>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptHeaderMarkTypeDesc2" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" colspan="2"><%#Eval("CurriculumMarkTypeName") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeaderMarkType3" runat="server" OnItemDataBound="rptHeaderMarkType3_ItemDataBound">
                    <ItemTemplate> 
                        <asp:Repeater ID="rptHeaderMarkType3Dt2" runat="server" OnItemDataBound="rptHeaderMarkType3Dt2_ItemDataBound">
                            <ItemTemplate>
                                <th class="thCenter" runat="server" id="thTaskCode">
                                    <label class="lblTask lblLink"><%#Eval("cfClassTaskCode")%></label><br />
                                    <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                        <th id="thAverageMark" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                            <%=GetLabel("Rata-Rata") %><br />
                        </th>
                        <th id="thFinalMark" runat="server" class="thCenter" style="width:80px; background-color:#B9EB33">
                            <%=GetLabel("Nilai") %><br />
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptHeaderMarkTypeDesc3" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Kriteria") %></th>
                        <th class="thCenter" style="width:200px"><%=GetLabel("Deskripsi") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                <ItemTemplate>
                    <tr class="trDetail">
                        <td class="keyField">
                            <%#Eval("StudentID") %>
                        </td>
                        <td>
                            <input type="hidden" class="hdnPreferredName" value='<%#Eval("PreferredName") %>' />
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
                                <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                                    <ItemTemplate>
                                        <td align="center" curriculummarktypeid='<%#DataBinder.Eval(Container,"Parent.Parent.DataItem.CurriculumMarkTypeID")%>'>
                                            <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                            <div id="divMark" runat="server">
                                                <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMark" Text="" Width="60px" />&nbsp;<b id="bIsRemedial" class="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                            </div>
                                            <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                            <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkDescription" Text="" Width="390px" />                         
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td align="center" id="tdTotalStudentMark" runat="server"><input class="txtTotalStudentMark number" curriculummarktypeid='<%#Eval("CurriculumMarkTypeID") %>' readonly="readonly" style="width:90%" /></td>
                                <td align="center" id="tdFinalStudentMark" runat="server">
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                    <input type="hidden" class="hdnCurriculumMarkTypeID" value='<%# Eval("CurriculumMarkTypeID") %>' />
                                    <input type="hidden" class="hdnTaskMarkTypeID" value='<%# Eval("TaskMarkTypeID") %>' />
                                    <input type="hidden" class="hdnFinalMarkTypeID" value='<%# Eval("FinalMarkTypeID") %>' />
                                    <input type="hidden" class="hdnPredicateMarkTypeID" value='<%# Eval("PredicateMarkTypeID") %>' />
                                    <input type="hidden" class="hdnCompetencyMarkTypeID" value='<%# Eval("CompetencyMarkTypeID") %>' />
                                    <input type="hidden" class="hdnTaskGCMarkType" value='<%# Eval("TaskGCMarkType") %>' />
                                    <input type="hidden" class="hdnFinalGCMarkType" value='<%# Eval("FinalGCMarkType") %>' />
                                    <input type="hidden" class="hdnPredicateGCMarkType" value='<%# Eval("PredicateGCMarkType") %>' />
                                    <input type="hidden" class="hdnIsAllowTask" value='<%# Eval("IsAllowTask") %>' />
                                    <input type="hidden" class="hdnGCCompetencyDescriptionType" value='<%# Eval("GCCompetencyDescriptionType") %>' />
                                    <asp:TextBox ID="txtFinalStudentMark" CssClass="txtFinalStudentMark number" Text="-" runat="server" Width="60px" />
                                    <dxe:ASPxComboBox ID="cboFinalStudentMarkOption" Width="80px" runat="server" />
                                    <asp:TextBox ID="txtFinalStudentMarkDescription" runat="server" CssClass="txtFinalStudentMarkDescription" Text="" Width="390px" />                         
                                </td>
                                <td align="center" id="tdPredicateStudentMark" runat="server">
                                    <dxe:ASPxComboBox ID="cboPredicateStudentMarkOption" Width="80px" runat="server" />                     
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptStudentMarkTypeDesc" runat="server" OnItemDataBound="rptStudentMarkTypeDesc_ItemDataBound">
                            <ItemTemplate> 
                                <td align="center"><dxe:ASPxComboBox ID="cboCompetencyMarkType" runat="server" Width="90%" /></td>
                                <td align="center">
                                    <input type="hidden" class="hdnListProgress" id="hdnListProgress" runat="server"/>
                                    <asp:TextBox ID="txtCompetencyDescription" CssClass="txtCompetencyDescription" runat="server" Width="90%" />
                                </td>
                            </ItemTemplate> 
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
            ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>