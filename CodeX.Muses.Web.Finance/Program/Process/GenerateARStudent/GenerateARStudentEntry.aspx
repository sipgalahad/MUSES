<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="GenerateARStudentEntry.aspx.cs" Inherits="CodeX.Muses.Web.Finance.Program.GenerateARStudentEntry" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnGenerate" runat="server" crudmode="R"><img src='<%=ResolveUrl("~/Libs/Images/Icon/set.png")%>' alt="" /><div><%=GetLabel("Generate")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            $('#btnRefresh').click(function () {
                onRefreshGridView();
                //cbpView.PerformCallback('refresh');
            });

            $('#<%=btnGenerate.ClientID %>').click(function () {
                getCheckedValue();
                if ($('#<%=hdnSelectedValue.ClientID %>').val() == "")
                    showToast('Warning', 'Silakan Pilih Siswa Terlebih Dahulu');
                else
                    onCustomButtonClick('save');
            });
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var rowCount = parseInt('<%=RowCount %>');
        var rowCountPerPage = parseInt('<%=RowCountPerPage %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setStudentImage();
            setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedValue();
                cbpView.PerformCallback('changepage|' + page);
                setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            setStudentImage();
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();
                else
                    $('#<%=hdnID.ClientID %>').val('');

                setNumEntriesText($('#informationNumEntries'), rowCount, currPage, rowCountPerPage);
                setPaging($("#paging"), pageCount, function (page) {
                    getCheckedValue();
                    cbpView.PerformCallback('changepage|' + page);
                    setNumEntriesText($('#informationNumEntries'), rowCount, page, rowCountPerPage);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function getCheckedValue() {
            var lstID = null;
            if ($('#<%=hdnSelectedValue.ClientID %>').val() != '')
                lstID = $('#<%=hdnSelectedValue.ClientID %>').val().split(',');
            else
                lstID = [];
            $('.chkIsSelected input').each(function () {
                if ($(this).is(':checked')) {
                    var id = $(this).closest('tr').find('.keyField').html();
                    if (lstID.indexOf(id) < 0)
                        lstID.push(id);
                }
                else {
                    var id = $(this).closest('tr').find('.keyField').html();
                    var idx = lstID.indexOf(id);
                    if (idx > -1)
                        lstID.splice(idx, 1);
                }
            });
            if (lstID.length == 0)
                $('#<%=hdnSelectedValue.ClientID %>').val('');
            else
                $('#<%=hdnSelectedValue.ClientID %>').val(lstID.join(','));
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        function setStudentImage() {
            setTimeout(function () {
                var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
                var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");

                $('.imgStudentImage').each(function () {
                    $divStudentImage = $(this).parent().find('.divStudentImage');
                    $divStudentImage.attr('style', "background-image:url('" + this.src + "')");
                    $(this).error(function () {
                        var gender = $(this).parent().find('.hdnStudentGender').val();
                        if (gender == '0003^F')
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlF + "')");
                        else
                            $(this).parent().find('.divStudentImage').attr('style', "background-image:url('" + imgUrlM + "')");
                    }).attr('src', this.src);
                });
            }, 0);
        }

        function onAfterCustomClickSuccess() {
            $('#<%=hdnSelectedValue.ClientID %>').val('');
            cbpView.PerformCallback('refresh');            
        }

        $('.lblDetail').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html() + '|' + cboMonth.GetValue() + '|' + cboYear.GetValue();
            var url = ResolveUrl("~/Program/Process/GenerateARStudent/GenerateARStudentDtCtl.ascx");
            openUserControlPopup(url, id, 'Detil Transaksi', 800, 550);
        });

        function onCboSchoolPeriodValueChanged(s) {
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
        }

        //#region School Period
        function onGetSchoolPeriodFilterExpression() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "'";
            return filterExpression;
        }

        function onTacSchoolPeriodButtonSearchClick() {
            openSearchDialog('schoolperiod', onGetSchoolPeriodFilterExpression(), function (value) {
                var filterExpression = onGetSchoolPeriodFilterExpression() + " AND SchoolPeriodCode = '" + value + "'";
                Methods.getObject('GetvSchoolPeriodList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolPeriod.setValue(result.SchoolPeriodID);
                        tacSchoolPeriod.setText(result.SchoolPeriodName);
                    }
                    else {
                        tacSchoolPeriod.setValue('');
                        tacSchoolPeriod.setText('');
                    }
                    onTacSchoolPeriodValueChanged();
                });
            });

        }

        function onTacSchoolPeriodValueChanged() {
            tacSchoolClass.setValue('');
            tacSchoolClass.setText('');
        }
        //#endregion

        //#region Class
        function onGetClassFilterExpression() {
            var filterExpression = "SchoolPeriodID = " + tacSchoolPeriod.getValue() + " AND GCClassStudyType = '<%=OnGetClassStudyTypeRegular() %>' AND IsDeleted = 0";
            return filterExpression;
        }

        function onTacClassButtonSearchClick() {
            openSearchDialog('schoolclass', onGetClassFilterExpression(), function (value) {
                var filterExpression = onGetClassFilterExpression() + " AND SchoolClassCode = '" + value + "'";
                Methods.getObject('GetvSchoolClassList', filterExpression, function (result) {
                    if (result != null) {
                        tacSchoolClass.setValue(result.SchoolClassID);
                        tacSchoolClass.setText(result.SchoolClassName);
                    }
                    else {
                        tacSchoolClass.setValue('');
                        tacSchoolClass.setText('');
                    }
                    onTacClassValueChanged();
                });
            });

        }

        function onTacClassValueChanged() {
        }
        //#endregion

        $('.chkIsSelected input').live('change', function () {
            $('.chkSelectAll input').prop('checked', false);
        });

        $('.chkSelectAll input').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkIsSelected').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
        });

        $('.lblStudent').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl('~/Program/ARInvoice/ARInvoiceStudent/StudentPageLauncher.aspx?id=' + id);
            openWindowPopup(url, 'Student', '1300', '650');
        });

        function onCboSiteValueChanged() {
            var filterExpression = "SiteID = '" + cboSite.GetValue() + "' AND <%=OnGetSchoolPeriodNowFilterExpression() %>";
            Methods.getObject('GetSchoolPeriodList', filterExpression, function (result) {
                if (result != null) {
                    tacSchoolPeriod.setValue(result.SchoolPeriodID);
                    tacSchoolPeriod.setText(result.SchoolPeriodName);
                }
                else {
                    tacSchoolPeriod.setValue('');
                    tacSchoolPeriod.setText('');
                }
                onTacSchoolPeriodValueChanged();
            });
        }

        function onRefreshGridView() {
            $('#<%=hdnFilterExpressionQuickSearch.ClientID %>').val(txtSearchView.GenerateFilterExpression());
            cbpView.PerformCallback('refresh');
        }

        function onTxtSearchViewSearchClick(s) {
            setTimeout(function () {
                s.SetBlur();
                onRefreshGridView();
                setTimeout(function () {
                    s.SetFocus();
                }, 0);
            }, 0);
        }
    </script>
    <style type="text/css">
        .gridCircle                         { display: block; width: 22px; height: 22px; margin: 0 auto; background-size: cover; background-repeat: no-repeat;
                                         background-position : center center; -webkit-border-radius: 99em; -moz-border-radius: 99em; border-radius: 99em; border: 1px solid #eee;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3); }
    </style>
    <input type="hidden" value="" id="hdnFilterExpressionQuickSearch" runat="server" />
    <input type="hidden" id="hdnSelectedValue" runat="server" />
    <table>
        <colgroup>
            <col style="width:150px"/>
        </colgroup>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Site") %></td>
            <td>
                <dxe:ASPxComboBox runat="server" ID="cboSite" ClientInstanceName="cboSite" Width="200px">
                    <ClientSideEvents Init="function(s,e){ onCboSiteValueChanged(); }"  ValueChanged="function(s,e){ onCboSiteValueChanged() }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel" style="width:100px;"><%=GetLabel("Tahun Ajaran") %></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolPeriod" ClientInstanceName="tacSchoolPeriod" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetSchoolPeriodFilterExpression"
                    SearchFields="SchoolPeriodName,SchoolPeriodCode" TextField="SchoolPeriodName" ValueField="SchoolPeriodID" SearchText="${SchoolPeriodName} (<b>${SchoolPeriodCode}</b>)" OrderByExpression="SchoolPeriodName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacSchoolPeriodButtonSearchClick(); }"
                        ValueChanged="function(){ onTacSchoolPeriodValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Kelas")%></label></td>
            <td>
                <cdx:CodeXAutoCompleteTextBox runat="server" Width="200px" ID="tacSchoolClass" ClientInstanceName="tacSchoolClass" MethodName="GetvSchoolClassList" GetFilterExpressionFunction="onGetClassFilterExpression"
                    SearchFields="SchoolClassName,SchoolClassCode" TextField="SchoolClassName" ValueField="SchoolClassID" SearchText="${SchoolClassName} (<b>${SchoolClassCode}</b>)" OrderByExpression="SchoolClassName">
                    <ClientSideEvents ButtonSearchClick="function(){ onTacClassButtonSearchClick(); }"
                        ValueChanged="function(){ onTacClassValueChanged(); }" />
                </cdx:CodeXAutoCompleteTextBox>   
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Search Filter")%></label></td>
            <td>
                <cdx:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchView" ID="txtSearchView"
	                Width="300px" Watermark="Search">
	                <ClientSideEvents SearchClick="function(s){ onTxtSearchViewSearchClick(s); }" />
	                <IntellisenseHints>
		                <cdx:QISIntellisenseHint Text="Nama" FieldName="Name" />
		                <cdx:QISIntellisenseHint Text="NIS" FieldName="StudentCode" />
	                </IntellisenseHints>
                </cdx:QISIntellisenseTextBox>
            </td>
        </tr>
        <tr>
            <td class="tdLabel"><%=GetLabel("Bulan") %></td>
            <td style="padding-right: 1px;">
                <table cellpadding="0" cellspacing="0" >
                    <colgroup>
                        <col width="120px" />
                        <col width="70px" />
                        <col width="120px" />
                    </colgroup>
                    <tr>
                        <td class="tdMonth"><dxe:ASPxComboBox ID="cboMonth" runat="server" ClientInstanceName="cboMonth" Width="120px" /></td>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tahun")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboYear" runat="server" ClientInstanceName="cboYear" Width="120px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><input type="button" id="btnRefresh" value="Refresh" /></td>
        </tr>
    </table>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="StudentID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-Width="40px" HeaderStyle-CssClass="thCenter" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsSelected" CssClass="chkIsSelected" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <img class="imgStudentImage" src='<%#Eval("StudentImageUrl") %>' alt="" height="25px" width="20px" style="float:left;margin-right: 10px; display:none" />
                                        <input type="hidden" value='<%# Eval("GCGender")%>' class="hdnStudentGender" />
                                        <div class="gridCircle divStudentImage"></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StudentCode" HeaderText="NIS" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="VirtualAccountNo" HeaderText="Nomor Bank Siswa" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Nama Siswa">
                                    <ItemTemplate>
                                        <label class="lblLink lblStudent"><%#Eval("StudentName") %></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-CssClass="thRight" HeaderText="Total Transaksi" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <label runat="server" id="lblStudentAmount" class="lblLink lblDetail"></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=GetLabel("Data Tidak Tersedia")%>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="divInformationNumEntries" id="informationNumEntries"></div>
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>
