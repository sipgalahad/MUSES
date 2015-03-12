<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="StudentFeeCompEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.StudentFeeCompEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            onCboAdmissionPaymentPeriodChanged();
        })
        function onGetAdmissionPaymentPeriodMonth() {
            return "<%=GetAdmissionPaymentPeriodMonth() %>";
        }

        function onGetAdmissionPaymentPeriodYear() {
            return "<%=GetAdmissionPaymentPeriodYear() %>";
        }

        function onCboAdmissionPaymentPeriodChanged() {
            var paymentPeriod = cboAdmissionPaymentPeriod.GetValue();
            if (paymentPeriod == onGetAdmissionPaymentPeriodMonth())
            {
                $('.tdDay').show();
                cboMonth.SetValue('');
                $('.tdMonth').hide();
            }
            else if (paymentPeriod == onGetAdmissionPaymentPeriodYear()){
                $('.tdDay').show();
                $('.tdMonth').show();
            }
            else {
                $('#<%=txtDay.ClientID %>').val('');
                cboMonth.SetValue('');
                $('.tdMonth').hide();
                $('.tdDay').hide();
            }
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:160px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                        <td><asp:TextBox ID="txtStudentFeeCompTypeName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama Singkat")%></label></td>
                        <td><asp:TextBox ID="txtShortName" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Periode Pembayaran")%></label></td>
                        <td>
                            <dxe:ASPxComboBox id="cboAdmissionPaymentPeriod" ClientInstanceName="cboAdmissionPaymentPeriod" runat="server">
                                <ClientSideEvents ValueChanged="function(s,e){onCboAdmissionPaymentPeriodChanged()}" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel tdDay"><label class="lblNormal"><%=GetLabel("Tanggal")%></label></td>
                        <td class="tdDay">
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col />
                                    <col width="70px"/>
                                </colgroup>
                                <tr>
                                    <td class="tdDay"><asp:TextBox ID="txtDay" Width="50px" runat="server" CssClass="txtNumeric" /></td>
                                    <td class="tdLabel tdMonth"><label class="lblNormal"><%=GetLabel("Bulan")%></label></td>
                                    <td class="tdMonth"><dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Denda [%]")%></label></td>
                        <td><asp:TextBox ID="txtPenaltyPercentage" Width="100px" CssClass="number" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
