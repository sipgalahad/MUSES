<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProspectiveStudentDtEntryCtl.ascx.cs" 
    Inherits="CodeX.Muses.Web.StudentManagement.Program.ProspectiveStudentDtEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunitsiteentryctl">
    $(function () {
        $('#ulProspectiveStudent li a').click(function () {
            if (!$(this).hasClass('disabled')) {
                if (!$(this).hasClass('selected')) {
                    clickUlProspectiveStudent($(this));
                }
            }
        });

        setTimeout(function () {
            showLoadingPanel();
            setTimeout(function () {
                hideLoadingPanel();
            }, 1000);
        }, 50);

        if ($('#<%=hdnID.ClientID %>').val() != '') {
            $('#ulProspectiveStudent li a').each(function () {
                $(this).removeClass('disabled');
            });
        }

        clickUlProspectiveStudent($('#ulProspectiveStudent li:eq(0) a'));
    });

    function clickUlProspectiveStudent($a) {
        $('#ulProspectiveStudent li a.selected').removeClass('selected');
        $a.addClass('selected');
        $a.removeClass('disabled');

        var id = $('#<%=hdnID.ClientID %>').val();
        var src = $a.attr('url');
        if (id != '')
            src += "?id=" + id;
        $('#frmProspectiveStudent').attr('src', src);
        showLoadingPanel();

        setTimeout(function () {
            hideLoadingPanel();
        }, 1000);
    }

    window.OnFinishButtonClick = function () {
        pcRightPanelContent.Hide();
    }

    window.OnNextButtonClick = function () {
        var idx = $('#ulProspectiveStudent li a.selected').index("#ulProspectiveStudent li a");
        idx++;
        $a = $('#ulProspectiveStudent li:eq(' + idx + ') a');
        clickUlProspectiveStudent($a);
    }

    window.OnPrevButtonClick = function () {
        var idx = $('#ulProspectiveStudent li a.selected').index("#ulProspectiveStudent li a");
        idx--;
        $a = $('#ulProspectiveStudent li:eq(' + idx + ') a');
        clickUlProspectiveStudent($a);
    }

    window.OnSetHdnID = function (id) {
        $('#<%=hdnID.ClientID %>').val(id);
    }
</script>

<style type="text/css">
	.stepmenu               { list-style: none; overflow: hidden; font: 18px Helvetica, Arial, Sans-Serif; margin: 0; padding: 0; margin-left: 50px; }
	.stepmenu li            { float: left; }
	.stepmenu li a          { color: white; text-decoration: none; padding: 10px 0 10px 55px; background: brown; background: hsla(34,85%,35%,1); position: relative; display: block; float: left; width: 150px; }
	.stepmenu li a:after    { content: " "; display: block; width: 0; height: 0; border-top: 50px solid transparent; border-bottom: 50px solid transparent; border-left: 30px solid hsla(34,85%,35%,1); position: absolute; top: 50%; margin-top: -50px; left: 100%; z-index: 2; }	
	.stepmenu li a:before   { content: " "; display: block; width: 0; height: 0; border-top: 50px solid transparent; border-bottom: 50px solid transparent; border-left: 30px solid white; position: absolute; top: 50%; margin-top: -50px; margin-left: 1px; left: 100%; z-index: 1; }	
	.stepmenu li:first-child a { padding-left: 10px; }
	.stepmenu li a       { background:        #01CB37; }
	.stepmenu li a:after { border-left-color: #01CB37; }
	.stepmenu li a.selected       { background:        #FC7201; cursor: default; }
	.stepmenu li a.selected:after { border-left-color: #FC7201; cursor: default; }
	.stepmenu li a.disabled       { background:        #D0D0D0; cursor: default; }
	.stepmenu li a.disabled:after { border-left-color: #D0D0D0; cursor: default; }
	.stepmenu li a:not(.disabled):hover { background: #FF1901; }
	.stepmenu li a:not(.disabled):hover:after { border-left-color: #FF1901 !important; }
</style>

<input type="hidden" runat="server" id="hdnID" value="" />
<center>
    <div id="page-wrap" style="width:100%; text-align: center;">
	    <ul class="stepmenu" id="ulProspectiveStudent">
		    <li><a href="#" url="ProspectiveStudentDtEntry.aspx" class="disabled"><%=GetLabel("Data Pribadi")%></a></li>
		    <li><a href="#" url="ProspectiveStudentParentDtEntry.aspx" class="disabled"><%=GetLabel("Data Orangtua / Wali")%></a></li>
		    <li><a href="#" url="ProspectiveStudentFamilyDtEntry.aspx" class="disabled"><%=GetLabel("Data Saudara Kandung")%></a></li>
		    <li><a href="#" url="ProspectiveStudentRemarksDtEntry.aspx" class="disabled"><%=GetLabel("Keterangan Lain")%></a></li>
		    <li><a href="#" url="ProspectiveStudentSurveyDtEntry.aspx" class="disabled"><%=GetLabel("Lembar Survei")%></a></li>
	    </ul>
    </div>

    <iframe style="width:100%;border:0;min-height:470px;max-height:470px; overflow:hidden;" id="frmProspectiveStudent" />
</center>