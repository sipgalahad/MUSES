<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="SchoolGradeEntry.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Program.SchoolGradeEntry" %>

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
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Grade")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboGrade" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top"><label class="lblMandatory"><%=GetLabel("Display Order")%></label></td>
                            <td><asp:TextBox ID="txtDisplayOrder" CssClass="number" Width="120px" runat="server" /></td>
                        </tr>
                    </table>    
                </div>    
            </td>
        </tr>
    </table>
</asp:Content>
