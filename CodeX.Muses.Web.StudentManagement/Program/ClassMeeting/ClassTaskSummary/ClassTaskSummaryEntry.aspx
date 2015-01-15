<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPClassSubjectPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="ClassTaskSummaryEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ClassTaskSummaryEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            setStudentImage();

            var width = parseInt('<%=OnGetTableViewWidth() %>');
            //if (width < 1250)
            //width = 1250;
            $('#tblView').width(width);

            setFinalMark();
        });

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
                var value = parseFloat($(this).val());
                total += value * lstFinalMarkPercentage[ctr] / 100;
                ctr++;
            });
            $tr.find('.txtTotalStudentMark').val(total);
            $tr.find('.txtFinalStudentMark').val(total);

        }
    </script>
    <input type="hidden" id="hdnListSaveValue" runat="server" />
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
            </tr>
            <tr>
                <asp:Repeater ID="rptHeader" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:90px">
                            <%#Eval("ClassTaskCode")%><br />
                            <input type="text" value='<%#Eval("FinalMarkPercentage")%>' style="width:30px" class="number txtFinalMarkPercentage" />[%]
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
                                        <%#Eval("StudentName") %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <asp:Repeater ID="rptStudentAttendance" runat="server" OnItemDataBound="rptStudentAttendance_ItemDataBound">
                            <ItemTemplate>
                                <td align="center">
                                    <asp:TextBox ID="txtStudentMark" Text="0" runat="server" CssClass="txtStudentMark number" Width="90%" />                                
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td align="center"><input class="txtTotalStudentMark number" readonly="readonly" style="width:90%" /></td>
                        <td align="center"><input class="txtFinalStudentMark number" style="width:90%" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>