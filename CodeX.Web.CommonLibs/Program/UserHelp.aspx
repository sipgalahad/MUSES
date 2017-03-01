<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.master" AutoEventWireup="true" 
    CodeBehind="UserHelp.aspx.cs" Inherits="CodeX.Web.CommonLibs.Program.UserHelp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.divFile').click(function () {
                $('#frmPreview').attr('src', $(this).attr('url'));
            });

            $('.treeNode').click(function () {
                var param = $(this).attr('href');
                //alert($(this).html());
                //alert(param);
                $('#frmPreview').attr('src', param);
                return false;
            });
        });
    </script>

    <table style="width:100%;">
        <colgroup>
            <col style="width: 220px"/>
        </colgroup>
        <tr>
            <td valign="top" style="border-right: 2px solid #AAA; padding-right: 3px;">
                <h4><%=GetLabel("Daftar User Guide")%></h4>
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
                <iframe id="frmPreview" src="" width="100%" height="640px"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>
