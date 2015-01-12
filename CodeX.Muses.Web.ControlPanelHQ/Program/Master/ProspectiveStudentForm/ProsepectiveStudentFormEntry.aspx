<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="ProsepectiveStudentFormEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanelHQ.Program.ProsepectiveStudentFormEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="cdx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        
    </script>
    <input type="hidden" id="hdnIsExist" runat="server" value="" />
    <input type="hidden" id="hdnID" runat="server" value="" />
    <table>
        <colgroup>
            <col style="width:50%"/>
            <col />
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Data Formulir")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Form Code")%></label></td>
                            <td><asp:TextBox ID="txtFormCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Form Name")%></label></td>
                            <td><asp:TextBox ID="txtFormName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top"><label class="lblMandatory"><%=GetLabel("Remarks")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="300px" runat="server" /></td>
                        </tr>
                    </table>    
                </div>    
            </td>
        </tr>
    </table>
</asp:Content>