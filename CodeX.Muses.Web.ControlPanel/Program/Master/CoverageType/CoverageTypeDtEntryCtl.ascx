<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoverageTypeDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.ControlPanel.Program.CoverageTypeDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#divTransactionAddPopup').click(function () {
            $('#<%=hdnEntryID.ClientID %>').val('');
            $('#<%=txtCoverageTypeDtName.ClientID %>').val('');
            $('#<%=txtRemarks.ClientID %>').val('');
            $('.txtNoOfPeriod').each(function () {
                $(this).val('0');
            });
            $('.txtDiscountAmount').each(function () {
                $(this).val('0').trigger('changeValue');
            });
            $('.txtCoverageAmount').each(function () {
                $(this).val('0').trigger('changeValue');
            });
            $('.chkIsDiscountInPercentage input').each(function () {
                $(this).prop('checked', false);
            });

            $('#<%=hdnLstClassTypeID.ClientID %>').val('');
            ddeClassType.SetText('');
            $('.chkClassType input:checked').each(function () {
                $(this).prop('checked', false);
            });

            $('#entryDetailContainerPopup').show();
        });

        $('#btnCancelPopup').click(function () {
            $('#entryDetailContainerPopup').hide();
        });

        $('#btnSavePopup').click(function (evt) {
            if (IsValid(evt, 'fsTrxPopup', 'mpTrxPopup')) {
                getSaveValue();
                cbpProcessPopup.PerformCallback('save');
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
            var coverageAmount = $tr.find('.txtCoverageAmount').attr('hiddenVal');
            var isCoverageInPercentage = $tr.find('.chkIsCoverageInPercentage input').is(':checked') ? '1' : '0';
            var noOfPeriod = $tr.find('.txtNoOfPeriod').val();
            lstSaveCompValue += admissionFeeCompID + ';' + discountAmount + ';' + isDiscountInPercentage + ';' + coverageAmount + ';' + isCoverageInPercentage + ';' + noOfPeriod;
        });
        $('#<%=hdnStudentFeeCompTypeSaveValue.ClientID %>').val(lstSaveCompValue);
    }

    $('#tblView .divDetailDelete').die('click');
    $('#tblView  .divDetailDelete').live('click', function () {
        $row = $(this).closest('tr');
        showToastConfirmation("Are You Sure Want To Delete This Data?", function (result) {
            if (result) {
                var entity = rowToObject($row);
                cboGrade.SetValue(entity.GCGrade);
                cbpProcessPopup.PerformCallback('delete');
            }
        });
    });

    $('#tblView  .divDetailEdit').die('click');
    $('#tblView  .divDetailEdit').live('click', function () {
        $row = $(this).closest('tr');
        var entity = rowToObject($row);
        $('#<%=hdnEntryID.ClientID %>').val(entity.CoverageTypeDtID);
        $('#<%=txtCoverageTypeDtName.ClientID %>').val(entity.CoverageTypeDtName);
        $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);

        $('.chkClassType input:checked').each(function () {
            $(this).prop('checked', false);
        });

        var lstClassTypeID = entity.ListClassTypeID.split(',');
        for (var i = 0; i < lstClassTypeID.length; ++i) {
            $('.chkClassType').each(function () {
                if ($(this).attr('classtypeid') == lstClassTypeID[i])
                    $(this).find('input').prop('checked', true);
            });
        }
        setDdeClassTypeText();

        $('.txtDiscountAmount').each(function () {
            $txt = $(this);
            $tr = $(this).closest('tr');
            var admissionFeeCompID = $tr.find('.hdnStudentFeeCompTypeID').val();

            $row.find('.tdCompDt').each(function () {
                var admissionFeeCompID1 = $(this).find('.hdnStudentFeeCompTypeID').val();
                if (admissionFeeCompID == admissionFeeCompID1) {
                    var discountAmount = $(this).find('.hdnDiscountAmount').val();
                    var isDiscountInPercentage = $(this).find('.hdnIsDiscountInPercentage').val() == 'True';
                    var coverageAmount = $(this).find('.hdnCoverageAmount').val();
                    var isCoverageInPercentage = $(this).find('.hdnIsCoverageInPercentage').val() == 'True';
                    var noOfPeriod = $(this).find('.hdnNoOfPeriod').val();
                    $txt.val(discountAmount).trigger('changeValue');
                    $tr.find('.chkIsDiscountInPercentage input').prop('checked', isDiscountInPercentage);
                    $tr.find('.txtCoverageAmount').val(coverageAmount).trigger('changeValue');
                    $tr.find('.chkIsCoverageInPercentage input').prop('checked', isCoverageInPercentage);
                    $tr.find('.txtNoOfPeriod').val(noOfPeriod);
                }
            });
        });
        $('#entryDetailContainerPopup').show();
    });

    function onCbpProcesPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                showToast('Save Failed', 'Error Message : ' + param[2]);
            else {
                $('#divTransactionAddPopup').click();
                cbpViewPopup.PerformCallback('refresh');
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                showToast('Delete Failed', 'Error Message : ' + param[2]);
            else
                cbpViewPopup.PerformCallback('refresh');
        }
    }

    $('.chkClassType input').change(function () {
        setDdeClassTypeText();
    });

    function setDdeClassTypeText() {
        var lstClassTypeID = '';
        var lstClassTypeName = '';
        $('.chkClassType input:checked').each(function () {
            if (lstClassTypeName != '') {
                lstClassTypeName += ', ';
                lstClassTypeID += ',';
            }
            lstClassTypeID += $(this).parent().attr('classtypeid');
            lstClassTypeName += $(this).parent().attr('classtypename');
        });
        $('#<%=hdnLstClassTypeID.ClientID %>').val(lstClassTypeID);
        ddeClassType.SetText(lstClassTypeName);
    }

    $(function () {
        addTableHeader();
    });

    function addTableHeader() {
        $('#tblView thead').html($('#tblView1 thead').html());
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnStudentFeeCompTypeSaveValue" runat="server" />
    <input type="hidden" id="hdnLstClassTypeID" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Nama")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>
                
    <div class="divTransactionEntry">   
        <span id="divTransactionAddPopup" class="divAdd"><%=GetLabel("Tambah Data")%></span><br />
        <div id="entryDetailContainerPopup" class="entryDetailContainer" style="display: none">
            <fieldset id="fsTrxPopup" style="margin:0"> 
                <input type="hidden" id="hdnEntryID" runat="server" value="" />
                <table>
                    <colgroup>
                        <col style="width: 160px" />
                        <col style="width: 150px" />
                        <col style="width: 50px" />
                        <col style="width: 150px" />
                        <col style="width: 50px" />
                        <col style="width: 110px" />
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Nama") %></label></td>
                        <td colspan="5"><asp:TextBox runat="server" ID="txtCoverageTypeDtName" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Tipe Kelas")%></label></td>
                        <td colspan="5">
                            <dxe:ASPxDropDownEdit ClientInstanceName="ddeClassType" ID="ddeClassType"
                                Width="300px" runat="server" EnableAnimation="False">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <asp:Repeater ID="rptClassType" runat="server" OnItemDataBound="rptClassType_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkClassType" CssClass="chkClassType" runat="server"  /> <%#Eval("ClassTypeName") %><br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </DropDownWindowTemplate>
                            </dxe:ASPxDropDownEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="center"><div class="lblComponent"><%=GetLabel("Total Diskon") %></div></td>
                        <td align="center"><div class="lblComponent">[%]</div></td>
                        <td align="center"><div class="lblComponent"><%=GetLabel("Total Tanggung") %></div></td>
                        <td align="center"><div class="lblComponent">[%]</div></td>
                        <td align="center"><div class="lblComponent"><%=GetLabel("Frek Bayar")%></div></td>
                    </tr>
                    <asp:Repeater ID="rptStudentFeeCompType" runat="server" OnItemDataBound="rptStudentFeeCompType_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td class="tdLabel">
                                    <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID")%>' />
                                    <label class="lblNormal"><%#Eval("StudentFeeCompTypeName")%></label>
                                </td>
                                <td><asp:TextBox ID="txtDiscountAmount" CssClass="txtCurrency txtDiscountAmount" Width="100%" runat="server" /></td>
                                <td align="center"><asp:CheckBox ID="chkIsDiscountInPercentage" CssClass="chkIsDiscountInPercentage" runat="server" /></td>
                                <td><asp:TextBox ID="txtCoverageAmount" CssClass="txtCurrency txtCoverageAmount" Width="100%" runat="server" /></td>
                                <td align="center"><asp:CheckBox ID="chkIsCoverageInPercentage" CssClass="chkIsCoverageInPercentage" runat="server" /></td>
                                <td><asp:TextBox ID="txtNoOfPeriod" CssClass="number txtNoOfPeriod" Width="100%" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Deskripsi") %></label></td>
                        <td colspan="5"><asp:TextBox runat="server" ID="txtRemarks" Width="400px" TextMode="MultiLine" Rows="3" /></td>
                    </tr>
                    <tr>
                        <td> 
                            <input type="button" id="btnSavePopup" class="btnWhite" value="Commit"/>
                            <input type="button" id="btnCancelPopup" class="btnWhite" value="Cancel"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>

    <table id="tblView1" rules="all" class="tblTransactionEntryResult grdBorder" style="display:none">
        <thead>
            <tr>
                <th style="width:220px" rowspan="3"><%=GetLabel("Tipe Kelas")%></th>     
                <th style="width:120px" rowspan="3"><%=GetLabel("Nama")%></th>     
                <th id="thFeeComp" runat="server" class="thCenter"><%=GetLabel("Komponen") %></th> 
                <th rowspan="3"><%=GetLabel("Keterangan")%></th>
                <th style="width:80px" rowspan="3"></th>
            </tr>
            <tr>
                <asp:Repeater ID="rptStudentFeeCompTypeView" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" colspan="3"><%#Eval("StudentFeeCompTypeName")%></th>
                    </ItemTemplate>
                </asp:Repeater>       
            </tr>
            <tr> 
                <asp:Repeater ID="rptStudentFeeCompTypeView2" runat="server">
                    <ItemTemplate>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Diskon") %></th>
                        <th class="thCenter" style="width:80px"><%=GetLabel("Tanggung") %></th>
                        <th class="thCenter" style="width:70px"><%=GetLabel("Frek Bayar") %></th>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </thead>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ addTableHeader(); hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:Repeater ID="rptView" runat="server" OnItemDataBound="rptView_ItemDataBound">
                        <HeaderTemplate>
                            <table id="tblView" rules="all" class="tblTransactionEntryResult grdBorder">
                                <thead>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="trDt">
                                <td><%#Eval("ListClassTypeName")%></td>
                                <td><%#Eval("CoverageTypeDtName")%></td>
                                <asp:Repeater ID="rptViewDt" runat="server">
                                    <ItemTemplate>
                                        <td class="thRight tdCompDt">
                                            <input type="hidden" class="hdnDiscountAmount" value='<%#Eval("DiscountAmount")%>' />
                                            <input type="hidden" class="hdnIsDiscountInPercentage" value='<%#Eval("IsDiscountInPercentage")%>' />
                                            <input type="hidden" class="hdnCoverageAmount" value='<%#Eval("CoverageAmount")%>' />
                                            <input type="hidden" class="hdnIsCoverageInPercentage" value='<%#Eval("IsCoverageInPercentage")%>' />
                                            <input type="hidden" class="hdnNoOfPeriod" value='<%#Eval("NoOfPeriod")%>' />
                                            <input type="hidden" class="hdnStudentFeeCompTypeID" value='<%#Eval("StudentFeeCompTypeID")%>' />
                                            <div><%#Eval("cfDiscountAmount") %></div>
                                        </td>
                                        <td class="thRight tdCompDt2">
                                            <div><%#Eval("cfCoverageAmount") %></div>
                                        </td>
                                        <td class="thRight tdCompDt3">
                                            <%#Eval("NoOfPeriod") %>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>                              
                                <td><%#Eval("Remarks")%></td>
                                <td align="center">
                                    <div style='float:right;' class="divDetailDelete"></div>
                                    <div style='float:right;margin-right:10px;' class="divDetailEdit"><%=GetLabel("Edit")%></div>
                                    <input type="hidden" value="<%#Eval("CoverageTypeDtID") %>" bindingfield="CoverageTypeDtID" />
                                    <input type="hidden" value="<%#Eval("CoverageTypeDtName") %>" bindingfield="CoverageTypeDtName" />
                                    <input type="hidden" value="<%#Eval("ListClassTypeID") %>" bindingfield="ListClassTypeID" />
                                    <input type="hidden" value="<%#Eval("ListClassTypeName") %>" bindingfield="ListClassTypeName" />
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
    <dxcp:ASPxCallbackPanel ID="cbpProcessPopup" runat="server" Width="100%" ClientInstanceName="cbpProcessPopup"
        ShowLoadingPanel="false" OnCallback="cbpProcessPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" EndCallback="function(s,e) { onCbpProcesPopupEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>

