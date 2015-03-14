<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GenerateSchoolClassExtracurricularEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.GenerateSchoolClassExtracurricularEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
    });

    //#region Room
    $('.divDeleteRoom').die('click');
    $('.divDeleteRoom').live('click', function () {
        $td = $(this).parent();
        $td.find('.hdnRoomID').val('0');
        $td.find('.lblRoom').html('Pilih Ruangan');
    });

    function onGetRoomFilterExpression() {
        var filterExpression = "<%=OnGetRoomFilterExpression() %>";
        return filterExpression;
    }

    $td = null;
    $('.lblRoom.lblLink').die('click');
    $('.lblRoom.lblLink').live('click', function () {
        $td = $(this).parent();
        openSearchDialog('room', onGetRoomFilterExpression(), function (value) {
            onTxtRoomChanged(value);
        });
    });

    function onTxtRoomChanged(value) {
        var filterExpression = onGetRoomFilterExpression() + " AND RoomCode = '" + value + "'";
        Methods.getObject('GetRoomList', filterExpression, function (result) {
            if (result != null) {
                $td.find('.hdnRoomID').val(result.RoomID);
                $td.find('.lblRoom').html(result.RoomName);
            }
            else {
                $td.find('.hdnRoomID').val('0');
                $td.find('.lblRoom').html('Pilih Ruangan');
            }
        });
    }
    //#endregion

    function onBeforeSaveRecord(errMessage) {
        var isRoomAllowSave = true;
        var isTeacherAllowSave = true;

        var lstSchoolClassCode = [];
        var lstSchoolClassName = [];
        var lstRoomID = [];
        var lstMaxStudent = [];
        $('#<%=grdView.ClientID %> tr:gt(0)').each(function () {
            $tr = $(this);
            var schoolClassCode = $tr.find('.txtSchoolClassCode').val();
            var schoolClassName = $tr.find('.txtSchoolClassName').val();
            var roomID = $tr.find('.hdnRoomID').val();
            var maxStudent = $tr.find('.txtMaxStudent').val();

            lstSchoolClassCode.push(schoolClassCode);
            lstSchoolClassName.push(schoolClassName);
            lstRoomID.push(roomID);
            lstMaxStudent.push(maxStudent);
        });
        $('#<%=hdnListSchoolClassCode.ClientID %>').val(lstSchoolClassCode.join(','));
        $('#<%=hdnListSchoolClassName.ClientID %>').val(lstSchoolClassName.join(','));
        $('#<%=hdnListRoomID.ClientID %>').val(lstRoomID.join(','));
        $('#<%=hdnListMaxStudent.ClientID %>').val(lstMaxStudent.join(','));

        return true;
    } 
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnMaxStudent" value="" runat="server" />
    <input type="hidden" id="hdnClassTypeCode" value="" runat="server" />
    <input type="hidden" id="hdnClassTypeName" value="" runat="server" />
    <input type="hidden" id="hdnNoOfClass" value="" runat="server" />
    <input type="hidden" id="hdnID" value="" runat="server" />
    <input type="hidden" id="hdnListSchoolClassCode" value="" runat="server" />
    <input type="hidden" id="hdnListSchoolClassName" value="" runat="server" />
    <input type="hidden" id="hdnListRoomID" value="" runat="server" />
    <input type="hidden" id="hdnListTeacherID" value="" runat="server" />
    <input type="hidden" id="hdnListMaxStudent" value="" runat="server" />
    
    <table class="tblEntryContent" style="width:70%">
        <colgroup>
            <col style="width:160px"/>
            <col/>
        </colgroup>
        <tr>
            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Tipe Kelas")%></label></td>
            <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
        </tr> 
    </table>

    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ hideLoadingPanel(); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect grdBorder" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:TemplateField HeaderText="Kode" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                <ItemTemplate>
                                    <input type="text" class="txtSchoolClassCode required" style="width:99%" validationgroup="mpEntryPopup" value='<%#Eval("Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                <ItemTemplate>
                                    <input type="text" class="txtSchoolClassName required" style="width:99%" validationgroup="mpEntryPopup" value='<%#Eval("Value") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ruangan" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                <ItemTemplate>
                                    <div style='float:right;' class="divDeleteRoom divDetailDelete"></div>
                                    <input type="hidden" value="0" class="hdnRoomID" />
                                    <label class="lblLink lblRoom"><%=GetLabel("Pilih Ruangan") %></label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kapasitas Siswa" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thCenter">
                                <ItemTemplate>
                                    <input type="text" class="txtMaxStudent number required" style="width:99%" validationgroup="mpEntryPopup" value='<%=hdnMaxStudent.Value%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
</div>