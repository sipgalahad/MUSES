<%@ Page Language="C#" MasterPageFile="~/MasterPage/MPPatientPageList.master" AutoEventWireup="true" 
    CodeBehind="DentalChartList.aspx.cs" Inherits="QIS.Medinfras.Web.EMR.Program.DentalChartList" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=grdView.ClientID %> tr:gt(0):not(.trEmpty)').live('click', function () {
                $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnID.ClientID %>').val($(this).find('.keyField').html());
            });
            $('#<%=grdView.ClientID %> tr:eq(1)').click();
        });

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });
        });

        function onCbpViewEndCallback(s) {
            $('#containerImgLoadingView').hide();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion
    </script>
    <input type="hidden" value="" id="hdnID" runat="server" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />

    <style type="text/css">
        .tblTooth                           { margin: auto; }
        .tblTooth tr td                     { padding: 2px; font-size: 0.8em; }
        .tblTooth tr td img                 { cursor:pointer; }
        .tblTooth tr td img.selected        { border:2px solid #F4921B; }
        .tblTooth tr td img.notSelected     { border:2px solid transparent; }
        .tblToothHeader                     { width:100%;font-weight:bolder; }
        .tblToothHeader tr td               { background-color: #CCC; font-size: 0.9em;width: 50%; }
    </style>
    <table style="width:100%;font-size:14px">
        <colgroup style="width:50%" />
        <colgroup style="width:25%" />
        <colgroup />
        <tr>
            <td style="vertical-align:top">
                <input type="hidden" runat="server" id="hdnToothId" value="0" />
                <div style="text-align:center;" id="containerTableTooth" runat="server">
                </div>            
            </td>
            <td style="vertical-align:top;padding-left:15px">
                
            </td>
            <td style="vertical-align:top;padding-left:15px">
                
            </td>
        </tr>
    </table>


    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingView').show(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdPatientPage" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <div>
                                            <b>
                                                <span style="float:left;width:50px;">Date</span>
                                                <span style="float:left;width:50px;margin-left:80px">Time</span>
                                                <span style="margin-left:40px">Physician</span>
                                            </b>
                                        </div>
                                        <div>Chief Complaint</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <b>
                                                    <span style="float:left;width:50px;"><%# Eval("ObservationDateInString")%></span>
                                                    <span style="float:left;width:50px;margin-left:80px"><%# Eval("ObservationTime")%></span>
                                                    <span style="margin-left:40px"><%# Eval("ParamedicName")%></span>
                                                </b>
                                            </div>
                                            <div><%# Eval("ChiefComplaintText")%></div>
                                            <div>
                                                <table cellpadding="0" cellspacing="0">
                                                    <colgroup>
                                                        <col style="width:90px"/>
                                                        <col style="width:200px"/>
                                                        <col style="width:90px"/>
                                                        <col style="width:200px"/>
                                                        <col style="width:90px"/>
                                                        <col style="width:200px"/>
                                                    </colgroup>
                                                    <tr>
                                                        <td>Location</td>
                                                        <td>: <%# Eval("Location")%></td>
                                                        <td>Quality</td>
                                                        <td>: <%# Eval("DisplayQuality")%></td>
                                                        <td>Relieved By</td>
                                                        <td>: <%# Eval("DisplayRelieved")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Onset</td>
                                                        <td>: <%# Eval("DisplayOnset")%></td>
                                                        <td>Severity</td>
                                                        <td>: <%# Eval("DisplaySeverity")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Provocation</td>
                                                        <td>: <%# Eval("DisplayProvocation")%></td>
                                                        <td>Time</td>
                                                        <td>: <%# Eval("DisplayCourse")%></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data To Display
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
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>
</asp:Content>
