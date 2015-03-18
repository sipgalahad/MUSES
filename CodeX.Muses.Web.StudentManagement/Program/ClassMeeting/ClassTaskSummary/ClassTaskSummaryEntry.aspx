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
                $('.trDetail').each(function () {
                    var tempResult = '';
                    $(this).find('.txtStudentMarkTheory').each(function () {
                        var value = '';
                        var positiontag = $(this).attr('positiontag');
                        switch (GCSubjectMarkType) {
                            case '<%=OnGetSubjectMarkTypeNumber() %>': value = $(this).val(); break;
                            case '<%=OnGetSubjectMarkTypeOption() %>':
                                var cboStudentMarkOption = eval('cboStudentMarkOptionTheory' + positiontag);
                                if (cboStudentMarkOption.GetValue() != null)
                                    value = cboStudentMarkOption.GetValue(); break;
                            case '<%=OnGetSubjectMarkTypeText() %>': value = $(this).parent().find('.txtStudentMarkTheoryDescription').val(); break;
                        }

                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += value;
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
                    if (result != '')
                        result += '|';
                    result += $(this).find('.keyField').html() + '*' + $(this).find('.txtFinalStudentMarkTheory').val() + '*' + $(this).find('.txtFinalStudentMarkPractice').val() + '*' + $(this).find('.txtAffectiveMark').val() + '*' + $(this).find('.txtAffectiveDescription').val() + '*' + $(this).find('.txtProgressDescription').val() + '*' + tempResult;
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

            setTotalPercentageTheory();
            setTotalPercentagePractice();
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
            var total = 0;
            $('.txtFinalMarkPercentageTheory').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentageTheory.push(value);
                total += value;
            });
            $('#txtTotalFinalMarkPercentageTheory').val(total);
            $('.trDetail').each(function () {
                setStudentFinalMarkTheory($(this));
            });
        }

        $('.txtFinalMarkPercentageTheory').live('change', function () {
            var idx = $(this).index('.txtFinalMarkPercentageTheory');
            lstFinalMarkPercentageTheory[idx] = parseFloat($(this).val());
            setTotalPercentageTheory();
        });

        $('.txtStudentMarkTheory').live('change', function () {
            setStudentFinalMarkTheory($(this).closest('.trDetail'));
        });

        function setStudentFinalMarkTheory($tr) {
            var ctr = 0;
            var total = 0;

            $tr.find('.txtStudentMarkTheory').each(function () {
                var value = 0;
                if ($(this).val() != "-")
                    value = parseFloat($(this).val());
                total += value * lstFinalMarkPercentageTheory[ctr] / 100;
                ctr++;
            });
            $tr.find('.txtTotalStudentMarkTheory').val(total);
            //$tr.find('.txtFinalStudentMarkTheory').val(total);

        }
        //#endregion

        //#region Practice
        var lstFinalMarkPercentagePractice = [];
        function setTotalPercentagePractice() {
            var total = 0;
            $('.txtFinalMarkPercentagePractice').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentagePractice.push(value);
                total += value;
            });
            $('#txtTotalFinalMarkPercentagePractice').val(total);
            $('.trDetail').each(function () {
                setStudentFinalMarkPractice($(this));
            });
        }

        $('.txtFinalMarkPercentagePractice').live('change', function () {
            var idx = $(this).index('.txtFinalMarkPercentagePractice');
            lstFinalMarkPercentagePractice[idx] = parseFloat($(this).val());
            setTotalPercentagePractice();
        });

        $('.txtStudentMarkPractice').live('change', function () {
            setStudentFinalMarkPractice($(this).closest('.trDetail'));
        });

        function setStudentFinalMarkPractice($tr) {
            var ctr = 0;
            var total = 0;

            $tr.find('.txtStudentMarkPractice').each(function () {
                var value = 0;
                if ($(this).val() != "-")
                    value = parseFloat($(this).val());
                total += value * lstFinalMarkPercentagePractice[ctr] / 100;
                ctr++;
            });
            $tr.find('.txtTotalStudentMarkPractice').val(total);
            //$tr.find('.txtFinalStudentMarkPractice').val(total);

        }
        //#endregion
    </script>
    <style type="text/css">
        .bIsRemedial                { cursor: pointer; }
        .bIsRemedial:hover          { text-decoration: underline; }
    </style>
    <input type="hidden" id="hdnListSaveHeaderValue" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnIsMainTeacher" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnGCSubjectMarkType" runat="server" />
    <input type="hidden" id="hdnGCTransactionStatus" runat="server" />
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
                <th id="thTheory" runat="server" class="thCenter"><%=GetLabel("TEORI") %></th>
                <th id="thPractice" runat="server" class="thCenter"><%=GetLabel("PRAKTEK") %></th>
                <th colspan="2" class="thCenter"><%=GetLabel("Afektif") %></th>
                <th rowspan="3" style="width:200px" class="thCenter"><%=GetLabel("Deskripsi Kemajuan Bljr") %></th>
            </tr>
            <tr> 
                <th id="thMarkTheory" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                <th id="thFinalReadonlyMarkTheory" runat="server" rowspan="2" style="width:90px" class="thCenter">
                    <%=GetLabel("Total") %><br />
                    <input type="text" id="txtTotalFinalMarkPercentageTheory" readonly="readonly" style="width:30px" class="number" />[%]
                </th>
                <th id="thFinalMarkTheory" runat="server" rowspan="2" style="width:90px" class="thCenter"><%=GetLabel("Nilai Akhir") %></th>
                <th id="thMarkPractice" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                <th id="thFinalReadonlyMarkPractice" runat="server" rowspan="2" style="width:90px" class="thCenter">
                    <%=GetLabel("Total") %><br />
                    <input type="text" id="txtTotalFinalMarkPercentagePractice" readonly="readonly" style="width:30px" class="number" />[%]
                </th>
                <th id="thFinalMarkPractice" runat="server" rowspan="2" style="width:90px" class="thCenter"><%=GetLabel("Nilai Akhir") %></th>
                <th class="thCenter" rowspan="2" style="width:40px"><%=GetLabel("Nilai") %></th>
                <th class="thCenter" rowspan="2" style="width:200px"><%=GetLabel("Deskripsi") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeaderTheory" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:90px">
                            <%#Eval("ClassTaskCode")%><br />
                            <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                            <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" class="number txtFinalMarkPercentageTheory" />[%]
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptHeaderPractice" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:90px">
                            <%#Eval("ClassTaskCode")%><br />
                            <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                            <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" class="number txtFinalMarkPercentagePractice" />[%]
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
                        <asp:Repeater ID="rptStudentMarkTheory" runat="server" OnItemDataBound="rptStudentMarkTheory_ItemDataBound">
                            <ItemTemplate>
                                <td align="center">
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                    <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMarkTheory" Text="" Width="60px" />&nbsp;<b id="bIsRemedial" class="bIsRemedial" runat="server" style="color:Red;">R*</b>
                                    <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                    <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkTheoryDescription" Text="" Width="390px" />                         
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center" id="tdTotalStudentMarkTheory" runat="server"><input class="txtTotalStudentMarkTheory number" readonly="readonly" style="width:90%" /></td>
                        <td align="center" id="tdFinalStudentMarkTheory" runat="server"><asp:TextBox ID="txtFinalStudentMarkTheory" CssClass="txtFinalStudentMarkTheory number" Text="-" runat="server" Width="90%" /></td>
                        
                        <asp:Repeater ID="rptStudentMarkPractice" runat="server" OnItemDataBound="rptStudentMarkPractice_ItemDataBound">
                            <ItemTemplate>
                                <td align="center">
                                    <input type="hidden" class="hdnItemIndex" value='<%# Container.ItemIndex %>' />
                                    <asp:TextBox ID="txtStudentMark" runat="server" CssClass="number txtStudentMarkPractice" Text="" Width="80px" />
                                    <dxe:ASPxComboBox ID="cboStudentMarkOption" Width="80px" runat="server" />
                                    <asp:TextBox ID="txtStudentMarkDescription" runat="server" CssClass="txtStudentMarkPracticeDescription" Text="" Width="390px" />                         
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center" id="tdTotalStudentMarkPractice" runat="server"><input class="txtTotalStudentMarkPractice number" readonly="readonly" style="width:90%" /></td>
                        <td align="center" id="tdFinalStudentMarkPractice" runat="server"><asp:TextBox ID="txtFinalStudentMarkPractice" CssClass="txtFinalStudentMarkPractice number" Text="-" runat="server" Width="90%" /></td>
                        
                        <td align="center"><asp:TextBox ID="txtAffectiveMark" CssClass="txtAffectiveMark" runat="server" Width="90%" /></td>
                        <td align="center"><asp:TextBox ID="txtAffectiveDescription" CssClass="txtAffectiveDescription" runat="server" Width="90%" /></td>
                        <td align="center"><asp:TextBox ID="txtProgressDescription" CssClass="txtProgressDescription" runat="server" Width="90%" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>