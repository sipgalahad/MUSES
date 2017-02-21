<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JournalQuickPicksCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.Accounting.Program.JournalQuickPicksCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_drugslogisticsquickpicksctl">
    function addItemFilterRow() {
        $trHeader = $('#<%=grdView.ClientID %> tr:eq(0)');
        $trFilter = $("<tr><td></td><td></td></tr>");

        $input = $("<input type='text' id='txtFilterItem' style='width:100%;height:20px' />").val($('#<%=hdnFilterItem.ClientID %>').val());
        $trFilter.find('td').eq(1).append($input);
        $trFilter.insertAfter($trHeader);
    }

    $('#txtFilterItem').live('keypress', function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            getCheckedMember();
            $('#<%=hdnFilterItem.ClientID %>').val($(this).val());
            e.preventDefault();
            cbpPopup.PerformCallback('refresh');
        }
    });

    $(function () {
        hideLoadingPanel();
        addItemFilterRow();
    });

    function onBeforeSaveRecordPopup(errMessage) {
        if (IsValid(null, 'fsDrugsQuickPicks', 'mpDrugsQuickPicks')) {
            getCheckedMember();
            if ($('#<%=hdnSelectedMember.ClientID %>').val() != '')
                return true;
            else {
                errMessage.text = 'Please Select Item First';
                return false;
            }
        }
        return false;
    }

    function getCheckedMember() {
        var lstSelectedMember = [];
        var lstSelectedMemberName = [];
        var lstSelectedMemberQty = [];

        var result = '';
        $('#tblSelectedItem .trSelectedItem').each(function () {
            var key = $(this).find('.keyField').val();
            var name = $(this).find('.tdSiteName').html();
            var qty = $(this).find('.txtQPAmount').attr('hiddenVal');
            lstSelectedMember.push(key);
            lstSelectedMemberQty.push(qty);
            lstSelectedMemberName.push(name);
        });
        $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
        $('#<%=hdnSelectedMemberName.ClientID %>').val(lstSelectedMemberName.join(','));
        $('#<%=hdnSelectedMemberQty.ClientID %>').val(lstSelectedMemberQty.join(','));
    }

    //#region COA
    function onTacQPCOAButtonSearchClick() {
        openSearchDialog('chartofaccount', onGetCOAFilterExpression(), function (value) {
            var filterExpression = onGetCOAFilterExpression() + " AND GLAccountNo = '" + value + "'";
            Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                if (result != null) {
                    tacQPCOA.setValue(result.GLAccountID);
                    tacQPCOA.setText(result.GLAccountName);
                }
                else {
                    tacQPCOA.setValue('');
                    tacQPCOA.setText('');
                }
                entityToControlQPCoa(result);
            });
        });

    }

    function onTacQPCOAValueChanged() {
        var id = tacQPCOA.getValue();
        if (id != '') {
            var filterExpression = "GLAccountID = " + id;
            Methods.getObject('GetvChartOfAccountList', filterExpression, function (result) {
                entityToControlQPCoa(result);
            });
        }
    }

    function entityToControlQPCoa(result) {
        if (result != '') {
            $('#<%=rblPosition.ClientID %> input[value=' + result.Position + ']').attr("checked", "checked"); ;
        }
    }
    //#endregion

    //#region Paging
    var pageCount = parseInt('<%=PageCount %>');

    $(function () {
        setPaging($("#pagingPopup"), pageCount, function (page) {
            getCheckedMember();
            cbpPopup.PerformCallback('changepage|' + page);
        });
    });

    function onCbpPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedMember();
                cbpPopup.PerformCallback('changepage|' + page);
            });
        }
        addItemFilterRow();
    }
    //#endregion

    $('#<%=grdView.ClientID %> .chkIsSelected input').die('change');
    $('#<%=grdView.ClientID %> .chkIsSelected input').live('change', function () {
        if ($(this).is(':checked')) {
            $selectedTr = $(this).closest('tr');

            $newTr = $('#tmplSelectedTestItem').html();
            $newTr = $newTr.replace(/\$\{SiteName}/g, $selectedTr.find('.tdSiteName').html());
            $newTr = $newTr.replace(/\$\{SiteID}/g, $selectedTr.find('.keyField').html());
            $newTr = $($newTr);
            $newTr.insertBefore($('#trFooterPopup'));
            setSelectedItemAmount();
        }
        else {
            var id = $(this).closest('tr').find('.keyField').html();
            $('#tblSelectedItem tr').each(function () {
                if ($(this).find('.keyField').val() == id) {
                    $(this).remove();
                }
            });
            setSelectedItemAmount();
        }
    });

    $('#<%=txtTotalAmount.ClientID %>').change(function () {
        $(this).blur();
        setSelectedItemAmount();
    });

    function setSelectedItemAmount() {
        var amount = parseFloat($('#<%=txtTotalAmount.ClientID %>').attr('hiddenVal'));
        var count = $('#tblSelectedItem .trSelectedItem').length;
        var unitAmount = amount / count;
        $('#tblSelectedItem .trSelectedItem').each(function () {
            $(this).find('.txtQPAmount').val(unitAmount).trigger('changeValue');
        });
    }

    $('#tblSelectedItem .chkIsSelected2').die('change');
    $('#tblSelectedItem .chkIsSelected2').live('change', function () {
        if ($(this).is(':checked')) {
            $selectedTr = $(this).closest('tr');
            var id = $selectedTr.find('.keyField').val();
            var isFound = false;
            $('#<%=grdView.ClientID %> tr').each(function () {
                if (id == $(this).find('.keyField').html()) {
                    $(this).find('.chkIsSelected').find('input').prop('checked', false);
                    isFound = true;
                }
            });
            if (!isFound) {
                var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
                lstSelectedMember.splice(lstSelectedMember.indexOf(id), 1);
                $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
            }
            $(this).closest('tr').remove();
            setSelectedItemAmount();
        }
    });

    $('#<%=txtTotalAmount.ClientID %>').trigger('changeValue');

    $('#<%=btnCommit.ClientID %>').click(function () {
        if (tacQPCOA.getValue() == '')
            showToast('Warning', 'Harap Pilih Perkiraan Terlebih Dahulu');
        else {
            getCheckedMember();
            fillTransactionDt(tacQPCOA.getValue(), $('#<%=hdnSelectedMember.ClientID %>').val(), $('#<%=hdnSelectedMemberName.ClientID %>').val(), $('#<%=hdnSelectedMemberQty.ClientID %>').val(), $('#<%=rblPosition.ClientID %> input:checked').val());
            pcRightPanelContent.Hide();
        }
    });
</script>

<div style="padding:10px;">
    <script id="tmplSelectedTestItem" type="text/x-jquery-tmpl">
        <tr class="trSelectedItem">
            <td align="center">
                <input type="checkbox" class="chkIsSelected2" />
                <input type="hidden" class="keyField" value='${SiteID}' />
            </td>
            <td class="tdSiteName">${SiteName}</td>
            <td><input type="text" validationgroup="mpDrugsQuickPicks" class="txtCurrency txtQPAmount" style="width:120px" /></td>
        </tr>
    </script>
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMemberName" runat="server" value="" />
    <input type="hidden" id="hdnTransactionID" runat="server" value="" />
    <input type="hidden" id="hdnParam" runat="server" value="" />
    <input type="hidden" id="hdnFilterItem" runat="server" />
    <input type="hidden" id="hdnSelectedMemberQty" runat="server" value="" />
    
    <table>
        <colgroup>
            <col style="width:150px"/>
            <col style="width:400px"/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Perkiraan")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacQPCOA" ClientInstanceName="tacQPCOA" MethodName="GetvChartOfAccountList" GetFilterExpressionFunction="onGetCOAFilterExpression"
                    SearchFields="GLAccountName,GLAccountNo" TextField="GLAccountName" ValueField="GLAccountID" SearchText="${GLAccountName} (<b>${GLAccountNo}</b>)" OrderByExpression="GLAccountName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacQPCOAButtonSearchClick(); }"
                        ValueChanged="function(){ onTacQPCOAValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Posisi")%></label></td>
            <td>
                <asp:RadioButtonList ID="rblPosition" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="D" Value="D" />
                    <asp:ListItem Text="K" Value="K" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label><%=GetLabel("Total")%></label></td>
            <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="txtCurrency" Width="120px" /></td>
        </tr>
    </table>
    <table style="width:100%">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Tersedia")%></h4>
                <dxcp:ASPxCallbackPanel ID="cbpPopup" runat="server" Width="100%" ClientInstanceName="cbpPopup"
                    ShowLoadingPanel="false" OnCallback="cbpPopup_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel();}"
                        EndCallback="function(s,e){ onCbpPopupEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                OnRowDataBound="grdView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ReferenceID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField"/>
                                        <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsSelected" runat="server" CssClass="chkIsSelected" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SiteName" HeaderText="Unit" ItemStyle-CssClass="tdSiteName" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="pagingPopup"></div>
                    </div>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Dipilih")%></h4>
                <fieldset id="fsDrugsQuickPicks">
                    <table id="tblSelectedItem" class="grdView notAllowSelect" cellspacing="0" rules="all" >
                        <tr id="trHeader2">
                            <th style="width:40px">&nbsp;</th>
                            <th align="center"><%=GetLabel("Unit")%></th> 
                            <th align="center"style="width:60px"><%=GetLabel("Jumlah")%></th> 
                        </tr>
                        <tr id="trFooterPopup"></tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
        
    <div class="imgLoadingGrdView" id="containerImgLoadingView" >
        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
    </div> 

    <div style="text-align:right; padding-right: 10px;">
        <input type="button" runat="server" id="btnCommit" value="Commit" />
    </div>
</div>