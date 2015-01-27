<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPTrx.master" AutoEventWireup="true" 
    CodeBehind="ScholarshipEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.ScholarshipEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#divTransactionAdd').click(function (evt) {
                $('#<%=hdnEntryID.ClientID %>').val('');
                $('#<%=txtScholarshipName.ClientID %>').val('');
                $('#<%=txtRemarks.ClientID %>').val('');
                cboScholarshipType.SetValue('');

                $('.txtNoOfPeriod').each(function () {
                    $(this).val('0');
                });
                $('.txtDiscountAmount').each(function () {
                    $(this).val('0').trigger('changeValue');
                });
                $('.chkIsDiscountInPercentage input').each(function () {
                    $(this).prop('checked', false);
                });
                $('#entryDetailContainer').show();
            });

            $('#btnCancel').click(function () {
                $('#entryDetailContainer').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx')) {
                    getSaveValue();
                    cbpProcess.PerformCallback('save');
                }
            });
        });

        function getSaveValue() {
            var lstSaveCompValue = '';
            $('.txtDiscountAmount').each(function () {
                if (lstSaveCompValue != '')
                    lstSaveCompValue += '|';
                var discountAmount = $(this).attr('hiddenVal');
                $tr = $(this).closest('tr');
                var admissionFeeCompID = $tr.find('.hdnStudentFeeCompTypeID').val();
                var isDiscountInPercentage = $tr.find('.chkIsDiscountInPercentage input').is(':checked') ? '1' : '0';
                var noOfPeriod = $tr.find('.txtNoOfPeriod').val();
                lstSaveCompValue += admissionFeeCompID + ';' + discountAmount + ';' + isDiscountInPercentage + ';' + noOfPeriod;
            });
            $('#<%=hdnStudentFeeCompTypeSaveValue.ClientID %>').val(lstSaveCompValue);
        }

        //#region edit and delete
        $('#tblView .divDetailDelete').live('click', function () {
            $row = $(this).closest('tr');
            showToastConfirmation('Are You Sure Want To Delete?', function (result) {
                if (result) {
                    var entity = rowToObject($row);
                    $('#<%=hdnEntryID.ClientID %>').val(entity.ScholarshipID);
                    cbpProcess.PerformCallback('delete');
                }
            });
        });

        $('#tblView .divDetailEdit').live('click', function () {
            $row = $(this).closest('tr');
            var entity = rowToObject($row);

            $('#<%=hdnEntryID.ClientID %>').val(entity.ScholarshipID);
            $('#<%=txtScholarshipName.ClientID %>').val(entity.ScholarshipName);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            cboScholarshipType.SetValue(entity.GCScholarshipType);

            $('.txtDiscountAmount').each(function () {
                $txt = $(this);
                $tr = $(this).closest('tr');
                var admissionFeeCompID = $tr.find('.hdnStudentFeeCompTypeID').val();

                $row.find('.tdCompDt').each(function () {
                    var admissionFeeCompID1 = $(this).find('.hdnStudentFeeCompTypeID').val();
                    if (admissionFeeCompID == admissionFeeCompID1) {
                        var discountAmount = $(this).find('.hdnDiscountAmount').val();
                        var isDiscountInPercentage = $(this).find('.hdnIsDiscountInPercentage').val() == 'True';
                        var noOfPeriod = $(this).find('.hdnNoOfPeriod').val();
                        $txt.val(discountAmount).trigger('changeValue');
                        $tr.find('.chkIsDiscountInPercentage input').prop('checked', isDiscountInPercentage);
                        $tr.find('.txtNoOfPeriod').val(noOfPeriod);
                    }
                });
            });

            $('#entryDetailContainer').show();
        });

        //#endregion

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    showToast('Save Failed', 'Error Message : ' + param[2]);
                else {
                    $('#divTransactionAdd').click();
                    cbpView.PerformCallback('refresh');
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    showToast('Delete Failed', 'Error Message : ' + param[2]);
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        $(function () {
            addTableHeader();
        });

        function addTableHeader() {
            $('#tblView thead').html($('#tblView1 thead').html());
        }

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
            addTableHeader();
        }
    </script>
    <input type="hidden" id="hdnStudentFeeCompTypeSaveValue" runat="server" />
    
    <table>
        <colgroup>
            <col style="width:150px"/>
            <col style="width:300px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tahun Ajaran") %></label></td>
            <td colspan="2">
                <dxe:ASPxComboBox runat="server" ID="cboSchoolPeriod" ClientInstanceName="cboSchoolPeriod" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboSchoolPeriodValueChanged(s); }" />
                </dxe:ASPxComboBox> 
            </td>
        </tr>
    </table>
    <div class="divTransactionEntry">
        <span id="divTransactionAdd" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainer" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrx" style="margin: 0">
                <input type="hidden" value="" id="hdnEntryID" runat="server" />
                <table style="width: 100%">
                    <colgroup>
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <colgroup>
                                    <col style="width: 160px" />
                                    <col style="width: 150px" />
                                    <col style="width: 50px" />
                                    <col style="width: 110px" />
                                </colgroup>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama")%></label></td>
                                    <td colspan="3"><asp:TextBox ID="txtScholarshipName" runat="server" Width="100%" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Beasiswa")%></label></td>
                                    <td colspan="3"><dxe:ASPxComboBox ID="cboScholarshipType" ClientInstanceName="cboScholarshipType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Total Diskon") %></div></td>
                                    <td align="center"><div class="lblComponent">[%]</div></td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Frek Bayar")%></div></td>
                                </tr>
                                <asp:Repeater ID="rptStudentFeeCompType" runat="server" OnItemDataBound="rptStudentFeeCompType_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("StudentFeeCompTypeName")%></label></td>
                                            <td>
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID")%>' />
                                                <asp:TextBox ID="txtDiscountAmount" CssClass="txtCurrency txtDiscountAmount" Width="100%" runat="server" />
                                            </td>
                                            <td align="center"><asp:CheckBox ID="chkIsDiscountInPercentage" CssClass="chkIsDiscountInPercentage" runat="server" /></td>
                                            <td><asp:TextBox ID="txtNoOfPeriod" CssClass="number txtNoOfPeriod" Width="100%" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td class="tdLabel" style="vertical-align:top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Keterangan") %></label></td>
                                    <td colspan="3"><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" Width="100%" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSave" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancel" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <table id="tblView1" rules="all" class="tblTransactionEntryResult grdBorder" style="display:none">
            <thead>
                <tr>
                    <th style="width:220px" rowspan="3"><%=GetLabel("Nama")%></th>    
                    <th style="width:220px" rowspan="3"><%=GetLabel("Tipe")%></th>       
                    <th id="thFeeComp" runat="server" class="thCenter"><%=GetLabel("Komponen") %></th> 
                    <th rowspan="3"><%=GetLabel("Keterangan")%></th>
                    <th style="width:80px" rowspan="3"></th>
                </tr>
                <tr>
                    <asp:Repeater ID="rptStudentFeeCompTypeView" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" colspan="2"><%#Eval("StudentFeeCompTypeName")%></th>
                        </ItemTemplate>
                    </asp:Repeater>       
                </tr>
                <tr> 
                    <asp:Repeater ID="rptStudentFeeCompTypeView2" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" style="width:80px"><%=GetLabel("Diskon") %></th>
                            <th class="thCenter" style="width:70px"><%=GetLabel("Frek Bayar") %></th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </thead>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                EndCallback="function(s,e){ onCbpViewEndCallback(); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto;
                        position: relative; font-size: 0.95em;">
                        <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblView" rules="all" class="tblTransactionEntryResult grdBorder">
                                    <thead>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="trDt">
                                    <td><%#Eval("ScholarshipName")%></td>
                                    <td><%#Eval("ScholarshipType")%></td>
                                    <asp:Repeater ID="rptViewDt" runat="server">
                                        <ItemTemplate>
                                            <td class="thRight tdCompDt">
                                                <input type="hidden" class="hdnDiscountAmount" value='<%#Eval("DiscountAmount")%>' />
                                                <input type="hidden" class="hdnIsDiscountInPercentage" value='<%#Eval("IsDiscountInPercentage")%>' />
                                                <input type="hidden" class="hdnNoOfPeriod" value='<%#Eval("NoOfPeriod")%>' />
                                                <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID")%>' />
                                                <div><%#Eval("cfDiscountAmount") %></div>
                                            </td>
                                            <td class="thRight tdCompDt2">
                                                <%#Eval("NoOfPeriod") %>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>                              
                                    <td><%#Eval("Remarks")%></td>
                                    <td align="center">
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("ScholarshipID") %>" bindingfield="ScholarshipID" />
                                        <input type="hidden" value="<%#Eval("ScholarshipName") %>" bindingfield="ScholarshipName" />
                                        <input type="hidden" value="<%#Eval("GCScholarshipType") %>" bindingfield="GCScholarshipType" />
                                        <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>                                
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>