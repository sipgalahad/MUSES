<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="APInvoiceSupplierProcessCreditNoteCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Finance.Program.APInvoiceSupplierProcessCreditNoteCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('.txtCreditNoteDate').each(function () {
        setDatePickerElement($(this));
    });

    $('#containerPopup .txtCurrency').each(function () {
        $(this).trigger('changeValue');
    });

    function onBeforeSaveRecordPopup(errMessage) {
        var result = '';
        $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
            if (result != '')
                result += '|';
            var creditNoteID = $(this).find('.keyField').html();
            var creditNoteDate = $(this).find('.txtCreditNoteDate').val();
            var GCCreditNoteType = $(this).find('.ddlCreditNoteType').val();
            var CNAmount = $(this).find('.txtCNAmount').attr('hiddenVal');
            result += creditNoteID + ';' + creditNoteDate + ';' + GCCreditNoteType + ';' + CNAmount;
        });
        $('#<%=hdnLstSaveValue.ClientID %>').val(result);
        return true;
    }
</script>
<input type="hidden" id="hdnID" runat="server" />
<input type="hidden" id="hdnLstSaveValue" runat="server" />
<input type="hidden" id="hdnVATPercentage" runat="server" />

<div style="max-height: 440px; overflow-y: auto" id="containerPopup">
    <table style="width:100%">
        <tr>
            <td style="padding: 5px; vertical-align: top">
                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="CreditNoteID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                        <asp:BoundField DataField="CreditNoteNo" HeaderText="No Nota Kredit" HeaderStyle-Width="150px" />
                        <asp:TemplateField HeaderText="Tanggal Nota Kredit" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCreditNoteDate" runat="server" CssClass="txtCreditNoteDate datepicker" Width="90px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PurchaseReturnNo" HeaderText="No Retur" />
                        <asp:TemplateField HeaderText="Tipe Nota Kredit" HeaderStyle-Width="140px" >
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCreditNoteType" CssClass="ddlCreditNoteType" runat="server" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsIncludeVAT" HeaderText="PPN" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter" HeaderStyle-Width="70px" />                                
                        <asp:BoundField DataField="TotalNetTransactionAmount" HeaderText="Nilai Retur" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="100px" DataFormatString="{0:N}" />
                        <asp:TemplateField HeaderText="Total (Setelah PPN)" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thRight" HeaderStyle-Width="120px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCNAmount" runat="server" CssClass="txtCNAmount txtCurrency" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <%=GetLabel("No Data To Display")%>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>        
        </tr>
    </table>
</div>