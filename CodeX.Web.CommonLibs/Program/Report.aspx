<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" 
    Inherits="CodeX.CodeX.Web.CommonLibs.Program.Report" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#dropDownMenu').superfish({
                //useClick: true
            });

            //$('#dropDownMenu a').click(function () {
                //showLoadingPanel();
                //document.location = window.location.href.split('?')[0] + "?id=" + $(this).attr('module');
            //});
        });
    </script>

    <input type="hidden" runat="server" id="hdnLoginData" />
    <input type="hidden" runat="server" id="hdnModuleID" />

    <div class="applicationTopBar" style="height: 33px;">
        <div class="hospitalName" id="hospitalName" style="float:left;width:120px; margin-left: 15px;"><%=GetLabel("Laporan")%></div>
        <div id="page-wrap" style="padding-top:7px;">
            <asp:Repeater ID="rptMenu" runat="server">
                <HeaderTemplate>
                    <ul class="sf-menu" id="dropDownMenu">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="liHeader"><a href="<%# GetResolveUrl((string)Eval("MenuUrl"))%>"><%# GetLabel((string)Eval("MenuCaption"))%></a>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div style="position: relative;width:100%;">
        <div style="position:absolute;width:100%;z-index:1;">
            <script type="text/javascript">
                $(function () {
                    $('.treeNode').click(function () {
                        var param = $(this).attr('href').split('|');
                        var id = parseInt(param[0]);
                        if (!isNaN(id)) {
                            $('#<%=hdnReportID.ClientID %>').val(param[0]);
                            $('#<%=hdnReportCode.ClientID %>').val(param[1]);
                            $('#divReportTitle').html($(this).html());
                            cbpReportParameter.PerformCallback();
                            return false;
                        }
                    });
                });

                $(function () {
                    $('#<%=btnReportPageExport.ClientID %>').click(function (evt) {
                        if (IsValid(evt, 'fsMPEntry', 'mpReport')) {
                            showLoadingPanel();
                            $('#<%=btnExport.ClientID%>').click();

                            setTimeout(function () {
                                hideLoadingPanel();
                            }, 1000);
                        }
                    });

                    $('#<%=btnReportPagePrint.ClientID %>').click(function (evt) {
                        if (IsValid(evt, 'fsMPEntry', 'mpReport'))
                            cbpReportProcess.PerformCallback();
                    });
                });

                //#region Search Dialog
                function getSDFilterExpression(baseFilterExpression) {
                    if (baseFilterExpression.indexOf('[') > -1 && baseFilterExpression.indexOf(']') > -1) {
                        var idx1 = baseFilterExpression.indexOf('[') + 1;
                        var cboClientInstanceName = baseFilterExpression.substr(idx1, baseFilterExpression.indexOf(']') - idx1);
                        var val = eval(cboClientInstanceName + '.GetValue()');
                        baseFilterExpression = baseFilterExpression.replace('[' + cboClientInstanceName + ']', val);
                    }
                    return baseFilterExpression;
                }

                $('label.lblLink').live('click', function () {
                    $tr = $(this).closest('tr');
                    $txtCode = $tr.find('.txtSdCode');
                    $txtName = $tr.find('.txtSdText');
                    $hdnID = $tr.find('.hdnSdID');

                    var searchDialogType = $tr.find('.hdnSearchDialogType').val();
                    var baseFilterExpression = $tr.find('.hdnSearchDialogFilterExpression').val();
                    var searchDialogCodeField = $tr.find('.hdnSearchDialogCodeField').val();
                    var searchDialogMethodName = $tr.find('.hdnSearchDialogMethodName').val();
                    var searchDialogIDField = $tr.find('.hdnSearchDialogIDField').val();
                    var searchDialogNameField = $tr.find('.hdnSearchDialogNameField').val();
                    baseFilterExpression = getSDFilterExpression(baseFilterExpression);

                    openSearchDialog(searchDialogType, baseFilterExpression, function (value) {
                        $txtCode.val(value);
                        $txtCode.focus();
                        var codeField = searchDialogCodeField;
                        var filterExpression = codeField + " = '" + value + "'";
                        if (baseFilterExpression != "")
                            filterExpression += " AND " + baseFilterExpression;
                        Methods.getObject(searchDialogMethodName, filterExpression, function (result) {
                            if (result != null) {
                                $hdnID.val(result[searchDialogIDField]);
                                $txtName.val(result[searchDialogNameField]);
                            }
                            else {
                                $txtCode.val('');
                                $hdnID.val('');
                                $txtName.val('');
                            }
                        });
                    });
                });

                $('input.txtSdCode').live('change', function () {
                    var value = $(this).val();
                    $tr = $(this).closest('tr').parent().closest('tr');
                    $txtCode = $tr.find('.txtSdCode');
                    $txtName = $tr.find('.txtSdText');
                    $hdnID = $tr.find('.hdnSdID');

                    var searchDialogType = $tr.find('.hdnSearchDialogType').val();
                    var baseFilterExpression = $tr.find('.hdnSearchDialogFilterExpression').val();
                    var searchDialogCodeField = $tr.find('.hdnSearchDialogCodeField').val();
                    var searchDialogMethodName = $tr.find('.hdnSearchDialogMethodName').val();
                    var searchDialogIDField = $tr.find('.hdnSearchDialogIDField').val();
                    var searchDialogNameField = $tr.find('.hdnSearchDialogNameField').val();
                    baseFilterExpression = getSDFilterExpression(baseFilterExpression);

                    var codeField = searchDialogCodeField;
                    var filterExpression = codeField + " = '" + value + "'";
                    if (baseFilterExpression != "")
                        filterExpression += " AND " + baseFilterExpression;

                    Methods.getObject(searchDialogMethodName, filterExpression, function (result) {
                        if (result != null) {
                            $hdnID.val(result[searchDialogIDField]);
                            $txtName.val(result[searchDialogNameField]);
                        }
                        else {
                            $txtCode.val('');
                            $hdnID.val('');
                            $txtName.val('');
                        }
                    });
                });
                //#endregion

                function onCboValueChanged(s) {
                    $tr = $(s.GetInputElement()).closest('tr').parent().closest('tr');
                    var isNumberVisible = false;
                    var isCustomPeriodVisible = false;
                    var isMonthYearVisible = false;
                    if ((s.GetValue() > 'X106^049' && s.GetValue() < 'X106^054')
                        || (s.GetValue() > 'X106^009' && s.GetValue() < 'X106^014')) {
                        isNumberVisible = true;
                        isCustomPeriodVisible = false;
                        isMonthYearVisible = false;
                    }
                    else if (s.GetValue() == 'X106^090') {
                        isNumberVisible = false;
                        isCustomPeriodVisible = true;
                        isMonthYearVisible = false;
                    }
                    else if (s.GetValue() == 'X106^089') {
                        isNumberVisible = false;
                        isCustomPeriodVisible = false;
                        isMonthYearVisible = true;
                    }
                    else {
                        isNumberVisible = false;
                        isCustomPeriodVisible = false;
                        isMonthYearVisible = false;
                    }

                    $tdDateMonth = $tr.find('.tdDateMonth');
                    $tdValueNum = $tr.find('.tdValueNum');
                    $tdCustomDate = $tr.find('.tdCustomDate');
                    if (isNumberVisible)
                        $tdValueNum.show();
                    else
                        $tdValueNum.hide();

                    if (isMonthYearVisible) {
                        if ($tr.find('.txtYear').val() == '')
                            $tr.find('.txtYear').val($tr.find('.hdnYear').val());
                        $tdDateMonth.show();
                    }
                    else {
                        $tdDateMonth.hide();
                    }

                    if (isCustomPeriodVisible) {
                        $tdCustomDate.show();
                        setDatePickerElement($tr.find('.txtValueDateFrom'));
                        setDatePickerElement($tr.find('.txtValueDateTo'));
                    }
                    else
                        $tdCustomDate.hide();
                }

                $('.txtYear').live('change', function () {
                    $tr = $(this).closest('tr');
                    $tr.find('.hdnYear').val($(this).val());
                });

                function onCbpReportProcessEndCallback(s) {
                    var param = s.cpResult.split('|');
                    if (param[0] == 'fail')
                        showToast('Print Failed', 'Error Message : ' + param[1]);
                    else
                        openReportViewer($('#<%=hdnReportCode.ClientID %>').val(), s.cpParam);
                    hideLoadingPanel();
                }

                function onCbpReportParameterEndCallback() {
                    $('.datepicker:visible').each(function () {
                        setDatePickerElement($(this));
                    });
                    if ($('#<%=hdnIsAllowExportExcel.ClientID %>').val() == '1')
                        $('#<%=btnReportPageExport.ClientID %>').show();
                    else
                        $('#<%=btnReportPageExport.ClientID %>').hide();

                    if ($('#<%=hdnIsAllowPrint.ClientID %>').val() == '1')
                        $('#<%=btnReportPagePrint.ClientID %>').show();
                    else
                        $('#<%=btnReportPagePrint.ClientID %>').hide();

                    hideLoadingPanel();
                }
            </script>
            <style type="text/css">
                .treeNode           { text-decoration: none; }
                .treeNode:hover     { text-decoration: underline; }
            </style>
            <input type="hidden" id="hdnReportID" runat="server" value="" />
            <input type="hidden" id="hdnReportCode" runat="server" value="" />
            <div style="display:none;">
                <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
                <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
            </div>
            <table style="width:100%">
                <colgroup>
                    <col style="width:35%"/>
                </colgroup>
                <tr>
                    <td valign="top" style="border-right: 2px solid #AAA; padding-right: 3px;">
                        <h4 style="min-height:28px;"><%=GetLabel("Daftar Laporan")%></h4>
                        <table class="tblContentArea" style="height:480px">
                            <tr>
                                <td valign="top">
                                    <asp:TreeView ID="tvwView" runat="server" ShowLines="true" ShowExpandCollapse="true"
                                        ExpandDepth="-1" Height="100%" Width="100%" OnTreeNodePopulate="tvwView_TreeNodePopulate">
                                        <NodeStyle ForeColor="Black" CssClass="treeNode" />
                                    </asp:TreeView>    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <h4 id="divReportTitle" style="min-height:28px;"></h4>
                        <fieldset id="fsMPEntry" style="margin:0"> 
                            <table class="tblContentArea" style="height:480px">
                                <tr>
                                    <td valign="top">
                                        <dxcp:ASPxCallbackPanel ID="cbpReportParameter" runat="server" Width="100%" ClientInstanceName="cbpReportParameter"
                                            ShowLoadingPanel="false" OnCallback="cbpReportParameter_Callback">  
                                            <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel();}"
                                                EndCallback="function(s,e){ onCbpReportParameterEndCallback(); }" />
                                            <PanelCollection>
                                                <dx:PanelContent ID="PanelContent2" runat="server">
                                                    <input type="hidden" id="hdnIsAllowExportExcel" value="0" runat="server" />
                                                    <input type="hidden" id="hdnIsAllowPrint" value="0" runat="server" />
                                                    <asp:Panel runat="server" ID="Panel1" >
                                                        <asp:Repeater ID="rptReportParameter" runat="server" OnItemDataBound="rptReportParameter_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <colgroup>
                                                                        <col style="width:150px"/>
                                                                        <col />
                                                                    </colgroup>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr id="trReportParameter" runat="server">
                                                                    <td class="tdLabel">
                                                                        <input type="hidden" id="hdnGCFilterParameterType" runat="server" value='<%#Eval("GCFilterParameterType") %>' />
                                                                        <label id="lblColumn" runat="server" dbid="<%# Container.ItemIndex %>" ><%#Eval("FilterParameterCaption")%></label>
                                                                    </td>
                                                                    <td>
                                                                        <div id="divTxt" runat="server" visible="false">
                                                                            <asp:TextBox ID="txtValue" Width="100px" runat="server" />
                                                                        </div>
                                                                        <div id="divDte" runat="server" visible="false">
                                                                            <asp:TextBox ID="txtDteValue" CssClass="datepicker" Width="120px" runat="server" dteid="<%# Container.ItemIndex %>" />
                                                                        </div>
                                                                        <div id="divCbo" runat="server" visible="false">
                                                                            <table cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="width: 160px">
                                                                                        <dxe:ASPxComboBox ID="cboValue" Width="250px" runat="server">
                                                                                            <ClientSideEvents ValueChanged="function(s,e){ onCboValueChanged(s); }" />
                                                                                        </dxe:ASPxComboBox>    
                                                                                    </td>
                                                                                    <td class="tdDateMonth" style="display:none">
                                                                                        <table cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td><dxe:ASPxComboBox ID="cboMonth" runat="server" Width="120px" /></td>
                                                                                                <td style="width:5px"></td>
                                                                                                <td>
                                                                                                    <input type="number" class="txtYear number" style="width:60px" />
                                                                                                    <input type="hidden" id="hdnYear" class="hdnYear" value='' runat="server"/>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="tdValueNum" style="display:none">
                                                                                        <asp:TextBox ID="txtValueNum" CssClass="txtValueNum number" runat="server" Width="80px" Text="1" />
                                                                                    </td>
                                                                                    <td class="tdCustomDate" style="display:none">
                                                                                        <asp:TextBox ID="txtValueDateFrom" CssClass="txtValueDateFrom datepicker" runat="server" Width="120px" />
                                                                                        -
                                                                                        <asp:TextBox ID="txtValueDateTo" CssClass="txtValueDateTo datepicker" runat="server" Width="120px" />
                                                                                    </td>
                                                                                </tr>                                                                        
                                                                            </table>                                                                    
                                                                        </div>
                                                                        <div id="divSd" runat="server" visible="false">
                                                                            <input type="hidden" runat="server" id="hdnSdNewID" class="hdnSdID" />
                                                                            <input type="hidden" class="hdnSearchDialogType" value='<%#Eval("SearchDialogType") %>' />
                                                                            <input type="hidden" id="hdnSearchDialogFilterExpression" runat="server" class="hdnSearchDialogFilterExpression" />
                                                                            <input type="hidden" class="hdnSearchDialogCodeField" value='<%#Eval("SearchDialogCodeField") %>' />
                                                                            <input type="hidden" class="hdnSearchDialogMethodName" value='<%#Eval("SearchDialogMethodName") %>' />
                                                                            <input type="hidden" class="hdnSearchDialogIDField" value='<%#Eval("SearchDialogIDField") %>' />
                                                                            <input type="hidden" class="hdnSearchDialogNameField" value='<%#Eval("SearchDialogNameField") %>' />
                                                                            <table cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td><asp:TextBox ID="txtSdNewCode" dbid="<%# Container.ItemIndex %>" CssClass="txtSdCode" Width="120px" runat="server" /></td>
                                                                                    <td style="width:5px">&nbsp;</td>
                                                                                    <td><asp:TextBox ID="txtSdNewText" disabled="disabled" CssClass="txtSdText" ReadOnly="true" Width="250px" runat="server" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </asp:Panel>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <dxcp:ASPxCallbackPanel ID="cbpReportProcess" runat="server" Width="100%" ClientInstanceName="cbpReportProcess"
                ShowLoadingPanel="false" OnCallback="cbpReportProcess_Callback">  
                <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel();}"
                    EndCallback="function(s,e){ onCbpReportProcessEndCallback(s); }" />
            </dxcp:ASPxCallbackPanel>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {
            var isHoverToolbarArea = false;
            $('#toolbarArea').hover(function () {
                isHoverToolbarArea = true;
            }, function () {
                isHoverToolbarArea = false;
            });

            $('html').click(function () {
                if (!isHoverToolbarArea)
                    $('#toolbarArea').slideUp('fast');
            });

            $('#openBottomBar').hover(function () {
                $('#toolbarArea').slideDown('fast');
            });
            $('#toolbarArea').slideDown('fast');
        });

        $(document).live("contextmenu", function (e) {
            if (e.button === 2) {
                e.preventDefault();
                $('#toolbarArea').slideDown('fast');
            }
        });
    </script>

    <div id="openBottomBar"></div>        
    <div class="toolbarArea" id="toolbarArea">
        <ul id="ulMPTrxToolbar" runat="server">
            <li id="btnReportPagePrint" CRUDMode="R" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/print.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Print")%></div></li>
            <li id="btnReportPageExport" CRUDMode="R" style="display:none" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/export.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Export")%></div></li>
        </ul>
    </div>
</asp:Content>
