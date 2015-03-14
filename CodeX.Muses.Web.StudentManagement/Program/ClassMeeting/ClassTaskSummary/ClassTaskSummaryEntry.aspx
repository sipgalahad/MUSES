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
                $('.trDetail').each(function () {
                    var tempResult = '';
                    $(this).find('.txtStudentMark').each(function () {
                        var value = $(this).val();
                        if (tempResult != '')
                            tempResult += ',';
                        tempResult += value;
                    });
                    if (result != '')
                        result += '|';
                    result += $(this).find('.keyField').html() + '^' + $(this).find('.txtFinalStudentMark').val() + '^' + $(this).find('.txtAffectiveMark').val() + '^' + $(this).find('.txtAffectiveDescription').val() + '^' + $(this).find('.txtProgressDescription').val() + '^' + tempResult;
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

            setFinalMark();
        });

        $('.lblStudent').live('click', function () {
            var id = $(this).closest('table').parent().closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/ClassMeeting/ClassTaskSummary/StudentSummaryViewDtCtl.ascx");
            openUserControlPopup(url, id, 'Riwayat Siswa', 800, 450);
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

        var lstFinalMarkPercentage = [];
        function setFinalMark() {
            setTotalPercentage();
        }

        function setTotalPercentage() {
            var total = 0;
            $('.txtFinalMarkPercentage').each(function () {
                var value = parseFloat($(this).val());
                lstFinalMarkPercentage.push(value);
                total += value;
            });
            $('#txtTotalFinalMarkPercentage').val(total);
            $('.trDetail').each(function () {
                setStudentFinalMark($(this));
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

        function setStudentFinalMark($tr) {
            var ctr = 0;
            var total = 0;

            $tr.find('.txtStudentMark').each(function () {
                var value = 0;
                if ($(this).val() != "-")
                    value = parseFloat($(this).val());
                total += value * lstFinalMarkPercentage[ctr] / 100;
                ctr++;
            });
            $tr.find('.txtTotalStudentMark').val(total);
            //$tr.find('.txtFinalStudentMark').val(total);

        }
    </script>
    <input type="hidden" id="hdnListSaveHeaderValue" runat="server" />
    <input type="hidden" id="hdnListSaveValue" runat="server" />
    <input type="hidden" id="hdnIsMainTeacher" runat="server" />
    <input type="hidden" id="hdnParentClassSubjectID" runat="server" />
    <input type="hidden" id="hdnGCTransactionStatus" runat="server" />
    <div style="width:1250px; overflow-x: auto;">
        <table rules="all" cellspacing="0" class="grdBorder grdSelected grdStudent" id="tblView">
            <tr>
                <th rowspan="2" style="width:300px"><%=GetLabel("Siswa") %></th>
                <th id="thMark" runat="server" class="thCenter"><%=GetLabel("NILAI") %></th>
                <th rowspan="2" style="width:90px" class="thCenter">
                    <%=GetLabel("Total") %><br />
                    <input type="text" id="txtTotalFinalMarkPercentage" readonly="readonly" style="width:30px" class="number" />[%]
                </th>
                <th rowspan="2" style="width:90px" class="thCenter"><%=GetLabel("Nilai Akhir") %></th>
                <th colspan="2" class="thCenter"><%=GetLabel("Afektif") %></th>
                <th rowspan="2" style="width:200px" class="thCenter"><%=GetLabel("Deskripsi Kemajuan Bljr") %></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:90px">
                            <%#Eval("ClassTaskCode")%><br />
                            <input type="hidden" value='<%#Eval("ClassSubjectTaskID")%>' class="hdnClassSubjectTaskID" />
                            <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" class="number txtFinalMarkPercentage" />[%]
                        </th>
                    </ItemTemplate>
                </asp:Repeater>
                <th class="thCenter" style="width:40px"><%=GetLabel("Nilai") %></th>
                <th class="thCenter" style="width:200px"><%=GetLabel("Deskripsi") %></th>
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
                        <asp:Repeater ID="rptStudentMark" runat="server" OnItemDataBound="rptStudentMark_ItemDataBound">
                            <ItemTemplate>
                                <td align="center">
                                    <asp:TextBox ID="txtStudentMark" Text="-" runat="server" CssClass="txtStudentMark number" Width="90%" />                                
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center"><input class="txtTotalStudentMark number" readonly="readonly" style="width:90%" /></td>
                        <td align="center"><asp:TextBox ID="txtFinalStudentMark" CssClass="txtFinalStudentMark number" Text="-" runat="server" Width="90%" /></td>
                        <td align="center"><asp:TextBox ID="txtAffectiveMark" CssClass="txtAffectiveMark" runat="server" Width="90%" /></td>
                        <td align="center"><asp:TextBox ID="txtAffectiveDescription" CssClass="txtAffectiveDescription" runat="server" Width="90%" /></td>
                        <td align="center"><asp:TextBox ID="txtProgressDescription" CssClass="txtProgressDescription" runat="server" Width="90%" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>