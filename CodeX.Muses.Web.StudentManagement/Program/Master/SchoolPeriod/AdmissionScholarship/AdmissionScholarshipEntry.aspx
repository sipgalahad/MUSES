<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPSchoolPeriodPageTrxVisit.master" AutoEventWireup="true" 
    CodeBehind="AdmissionScholarshipEntry.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.AdmissionScholarshipEntry" %>

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
                cboFromSchoolType.SetValue('');

                $('.chkPeriodAdmissionID input').each(function () {
                    $(this).prop('checked', false);
                });
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
            var lstSavePeriodAdmission = '';
            $('.chkPeriodAdmissionID input').each(function () {
                if ($(this).is(':checked')) {
                    if (lstSavePeriodAdmission != '')
                        lstSavePeriodAdmission += '|';
                    lstSavePeriodAdmission += $(this).closest('tr').find('.hdnPeriodAdmissionID').val();
                }
            });
            $('#<%=hdnPeriodAdmissionSaveValue.ClientID %>').val(lstSavePeriodAdmission);

            var lstSaveCompValue = '';
            $('.txtDiscountAmount').each(function () {
                if (lstSaveCompValue != '')
                    lstSaveCompValue += '|';
                var discountAmount = $(this).attr('hiddenVal');
                $tr = $(this).closest('tr');
                var admissionFeeCompID = $tr.find('.hdnAdmissionFeeCompID').val();
                var isDiscountInPercentage = $tr.find('.chkIsDiscountInPercentage input').is(':checked') ? '1' : '0';
                var noOfPeriod = $tr.find('.txtNoOfPeriod').val();
                lstSaveCompValue += admissionFeeCompID + ';' + discountAmount + ';' + isDiscountInPercentage + ';' + noOfPeriod;
            });
            $('#<%=hdnAdmissionFeeCompSaveValue.ClientID %>').val(lstSaveCompValue);
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
            cboFromSchoolType.SetValue(entity.GCFromSchoolType);

            $('.txtDiscountAmount').each(function () {
                $txt = $(this);
                $tr = $(this).closest('tr');
                var admissionFeeCompID = $tr.find('.hdnAdmissionFeeCompID').val();

                $row.find('.tdCompDt').each(function () {
                    var admissionFeeCompID1 = $(this).find('.hdnAdmissionFeeCompID').val();
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

            $('.chkPeriodAdmissionID input').each(function () {
                $(this).prop('checked', false);
            });
            $row.find('.chkPeriodAdmissionView input').each(function () {
                if ($(this).is(':checked')) {
                    var periodAdmissionID = $(this).closest('td').find('.hdnPeriodAdmissionID').val();
                    $('.chkPeriodAdmissionID input').each(function () {
                        var periodAdmissionID1 = $(this).closest('tr').find('.hdnPeriodAdmissionID').val();
                        if (periodAdmissionID == periodAdmissionID1)
                            $(this).prop('checked', true);
                    });
                }
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
    <input type="hidden" id="hdnPeriodAdmissionSaveValue" runat="server" />
    <input type="hidden" id="hdnAdmissionFeeCompSaveValue" runat="server" />
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
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Asal Sekolah")%></label></td>
                                    <td colspan="3"><dxe:ASPxComboBox ID="cboFromSchoolType" ClientInstanceName="cboFromSchoolType" runat="server" Width="200px" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Total Diskon") %></div></td>
                                    <td align="center"><div class="lblComponent">[%]</div></td>
                                    <td align="center"><div class="lblComponent"><%=GetLabel("Frek Bayar")%></div></td>
                                </tr>
                                <asp:Repeater ID="rptAdmissionFeeComp" runat="server" OnItemDataBound="rptAdmissionFeeComp_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("AdmissionFeeCompType")%></label></td>
                                            <td>
                                                <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID")%>' />
                                                <asp:TextBox ID="txtDiscountAmount" CssClass="txtCurrency txtDiscountAmount" Width="100%" runat="server" />
                                            </td>
                                            <td align="center"><asp:CheckBox ID="chkIsDiscountInPercentage" CssClass="chkIsDiscountInPercentage" runat="server" /></td>
                                            <td><asp:TextBox ID="txtNoOfPeriod" CssClass="number txtNoOfPeriod" Width="100%" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="3"><div class="lblComponent"><%=GetLabel("Gelombang Pendaftaran") %></div></td>
                                </tr>
                                <asp:Repeater ID="rptPeriodAdmission" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%#Eval("PeriodAdmissionName")%></label></td>
                                            <td colspan="3" style="height:25px;">
                                                <asp:CheckBox ID="chkPeriodAdmissionID" CssClass="chkPeriodAdmissionID" runat="server" />
                                                <input type="hidden" class="hdnPeriodAdmissionID" value='<%#Eval("PeriodAdmissionID")%>' />
                                            </td>
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
                    <th id="thFeeComp" runat="server" class="thCenter"><%=GetLabel("Komponen") %></th> 
                    <th id="thPeriodAdmission" runat="server" class="thCenter"><%=GetLabel("Gelombang") %></th> 
                    <th rowspan="3"><%=GetLabel("Keterangan")%></th>
                    <th style="width:80px" rowspan="3"></th>
                </tr>
                <tr>
                    <asp:Repeater ID="rptAdmissionFeeCompView" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" colspan="2"><%#Eval("AdmissionFeeCompType")%></th>
                        </ItemTemplate>
                    </asp:Repeater>       
                    <asp:Repeater ID="rptPeriodAdmissionView" runat="server">
                        <ItemTemplate>
                            <th class="thCenter" style="width:100px" rowspan="2"><%#Eval("PeriodAdmissionName")%></th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <tr> 
                    <asp:Repeater ID="rptAdmissionFeeCompView2" runat="server">
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
                                    <asp:Repeater ID="rptViewDt" runat="server">
                                        <ItemTemplate>
                                            <td class="thRight tdCompDt">
                                                <input type="hidden" class="hdnDiscountAmount" value='<%#Eval("DiscountAmount")%>' />
                                                <input type="hidden" class="hdnIsDiscountInPercentage" value='<%#Eval("IsDiscountInPercentage")%>' />
                                                <input type="hidden" class="hdnNoOfPeriod" value='<%#Eval("NoOfPeriod")%>' />
                                                <input type="hidden" class="hdnAdmissionFeeCompID" value='<%#Eval("AdmissionFeeCompID")%>' />
                                                <div><%#Eval("cfDiscountAmount") %></div>
                                            </td>
                                            <td class="thRight tdCompDt2">
                                                <%#Eval("NoOfPeriod") %>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>     
                                    <asp:Repeater ID="rptViewDtAdmission" runat="server" OnItemDataBound="rptViewDtAdmission_ItemDataBound">
                                        <ItemTemplate>
                                            <td class="thCenter tdCompDtAdmission">
                                                <input type="hidden" class="hdnPeriodAdmissionID" value='<%#Eval("PeriodAdmissionID")%>' />
                                                <asp:CheckBox ID="chkPeriodAdmissionView" Enabled="false" CssClass="chkPeriodAdmissionView" runat="server" />
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>                                    
                                    <td><%#Eval("Remarks")%></td>
                                    <td align="center">
                                        <div style='float:right;' class="divDetailDelete"></div>
                                        <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                        <input type="hidden" value="<%#Eval("ScholarshipID") %>" bindingfield="ScholarshipID" />
                                        <input type="hidden" value="<%#Eval("ScholarshipName") %>" bindingfield="ScholarshipName" />
                                        <input type="hidden" value="<%#Eval("GCFromSchoolType") %>" bindingfield="GCFromSchoolType" />
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