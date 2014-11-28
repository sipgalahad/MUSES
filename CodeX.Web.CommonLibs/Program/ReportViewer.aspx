<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" 
    Inherits="CodeX.Web.CommonLibs.Program.ReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Medinfras</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body>
    <form id="myForm" runat="server">
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.4.3.js")%>' type='text/javascript'></script>
        <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.7.min.js")%>' type='text/javascript'></script>
        <script type="text/javascript">
            $(function () {
                var height = $(window).height() - $('#toolbarArea').height() - 20;
                $('#pageArea').height(height);

                $.browser.chrome = /chrom(e|ium)/.test(navigator.userAgent.toLowerCase());
                $div = $('<div></div>');
                var html = '<style type="text/css">' + $('#divStyleExcel').html() + '</style>';
                html += '<div class="pageContent">' + $('#page1').html() + '</div>';
                $div.html(html);
                $div.find('.siteInformation').remove();
                $div.find('.tblReport').attr('border', '1');
                $div.find('.pageFooter').remove();

                $('#<%=hdnExportExcel.ClientID %>').val($div.html());

                processOverflowDiv1(1);

                var pageCount = $('.pageFooter').length;
                $('.pageFooter').each(function () {
                    $(this).html($(this).html().replace('[TotalPageCount]', pageCount));
                });

                for (var i = 1; i <= pageCount; ++i) {
                    $('#cboJumpPage').append($("<option></option>").text(i));
                }
                $('#txtTotalPage').val(pageCount);
            });

            function processOverflowDiv1(idx) {
                var cont1 = $('#page' + idx);
                var cont1Height = cont1.height();

                var p1 = $('#page' + idx + ' .divcontent');
                var p1Height = p1.height();
                if (p1Height > cont1Height) {
                    var containerp2 = $("<div class='page'><div id='page" + (idx + 1) + "' class='pageContent'><div class='divcontent'><div class='pageFooter'></div><div class='pageHeader'></div></div></div></div>");
                    var containerp1 = p1.parent();

                    containerp2.find('.pageHeader').html(containerp1.find('.pageHeader').html());
                    containerp2.find('.pageFooter').html(containerp1.find('.pageFooter').html());
                    if (!containerp1.find('.pageFooter').is(':visible'))
                        containerp2.find('.pageFooter').hide();
                    containerp2.find('.divPageNumber').html("Page " + (idx + 1) + " of [TotalPageCount]");
                    var p2 = containerp2.find('.divcontent');

                    p2.append('<table class="tblReport" style="width:100%" cellpadding="0" cellspacing="0"><thead></thead><tbody class="reportBody"></tbody></table><div class="divContainerReportFooter"></div>');
                    p2.find('.tblReport').find('thead').html(containerp1.find('.tblReport').find('thead').html());
                    p2.find('.divContainerReportFooter').html(containerp1.find('.divContainerReportFooter').html());
                    containerp1.find('.divContainerReportFooter').remove();

                    var p1text = p1.text();
                    p1text = p1text.split('');

                    $tbody = p1.find('.reportBody');
                    $tbody2 = p2.find('.reportBody');

                    if ($.browser.chrome) {
                        while (p1Height + 10 > cont1Height) {
                            $elm = $tbody.find('tr').last();
                            $tbody2.prepend($elm);

                            //re-evaluate height
                            p1Height = p1.height();
                            //loop
                        }
                    }
                    else {
                        while (p1Height > cont1Height) {
                            $elm = $tbody.find('tr').last();
                            $tbody2.prepend($elm);

                            //re-evaluate height
                            p1Height = p1.height();
                            //loop
                        }
                    }
                    containerp2.insertAfter(p1.parent().parent());
                    processOverflowDiv1(idx + 1);
                }
            }

            $(function () {
                $('#btnExportExcel').click(function () {
                    $('#<%=btnExport.ClientID%>').click();
                });
                $('#btnPrint').click(function () {
                    window.print();
                });

                $('#cboJumpPage').change(function () {
                    goToByScroll('page' + $(this).val());
                });
            });
            function goToByScroll(id) {
                $('#pageArea').animate({
                    scrollTop: $('#pageArea').scrollTop() + $("#" + id).offset().top - 45
                });
            }
        </script>
        <div style="display:none;" id="divStyleExcel">
            .tblReport .tdDetail, .tblHeader td { padding-right:0.1cm; padding-left:0.1cm;  }
            thead td            { font-weight: bold; }
            .tblHeader td            { border-bottom: 1px solid; }
            .tblHeader:nth-child(1) td            { border-top: 1px solid; }
            .tblBorder td:last-child            { border-right: 1px solid; }
            .tblBorder td            { border-left: 1px solid; border-collapse:collapse; }
            thead { display:table-row-group; }
            .tdGroupName, .tdSubTotal, .tdGrandTotal        { font-weight: bold; }
            .tdGrandTotal, .tdSubTotal                 { text-align: right; }
            .tdSubTotalDetail           { border-top: 1px dotted; padding: 0.5mm 0; }
            .reportBody tr.trGroup0:not(:first-child) > td { padding-top: 20px; }
            .reportBody tr.trGroup0:not(:first-child) > td > tr.trGroup1:not(:first-child) > td { padding-top: 20px; }
            .pageFooter         { border-top: 1px solid; position: absolute; bottom: 0.5cm; left: 0.7cm; right: 0.7cm; font-size: 8pt; }
            .tdAutoNumber       { padding-right:4px !important; }
            .borderTop          { border-top: 1px dotted; }
            .tdSignature        { padding-top:1.7cm; }
    /*border-collapse: separate;    margin-top: 0.1em;    border-spacing: 0;*/
            .divContainerReportHeader *     { font-weight: normal; }
            .divContainerReportHeader b     { font-weight: bold !important; }
            .divContainerReportHeader       { margin-bottom: 0.5cm; }
            .divContainerReportFooter       { margin-top: 0.1cm; }
            
            .tdReportTotal *                { font-weight: bold; font-size: 8pt; }
            
            h1 { font-weight: bold; font-size: 12pt; margin-bottom: 0.5cm }
        </div>

        <style type="text/css">
            body {
                margin: 0;
                padding: 0;
                background-color: #FAFAFA;
            }
            * {
                box-sizing: border-box;
                -moz-box-sizing: border-box;
                font: <%=fontSize%> "Tahoma";
            }
            .siteInformation *, .pageFooter * {
                font: 8pt "Tahoma";
            }
            
            #pageArea
            {
                width: 100%;            
                overflow-y: scroll;
            }
            .page {
                width: <%=paperWidth %>mm;
                height: <%=paperHeight%>mm;
                padding: 0.2cm 0.7cm;
                margin: 0 auto 0.5cm auto;
                border: 1px #D3D3D3 solid;
                border-radius: 5px;
                background: white;
                box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
                position: relative;
            }
            #toolbarArea { background-color: #E0E0E0;  border: 1px #ADADAD solid; border-radius: 3px; width: 400px; position: relative; padding: 5px 5px; }
            p
            { page-break-before: always
            }

            @page {
                size: <%=paperSize %>;
                margin: 0;
            }
            @media print{@page {size: <%=paperPortraitLandscape%>;}}
            @media print {
                .page {
                    margin: 0;
                    border: initial;
                    border-radius: initial;
                    width: initial;
                    min-height: initial;
                    box-shadow: initial;
                    background: initial;
                    page-break-after: always;
                    page-break-inside:avoid; 
                    width: <%=paperPrintWidth %>mm;
                    height: <%=paperPrintHeight %>mm;
                }
                #pageArea           { height: 100%; overflow: auto; }
                #toolbarArea        { display: none; }
                .pageContent        { min-height:<%=paperPrintPageContent %> !important; }
            }
            .tblReport .tdDetail, .tblHeader th { padding-right:0.1cm; padding-left:0.1cm;  }
            .tdDetail           { vertical-align: top; }
            thead th            { font-weight: bold; }
            .tblHeader th            { border-bottom: 1px solid; }
            .tblHeader:nth-child(1) th            { border-top: 1px solid; }
            .tblBorder th:last-child            { border-right: 1px solid; }
            .tblBorder th            { border-left: 1px solid; border-collapse:collapse; }
            thead { display:table-row-group; }
            .tdGroupName, .tdSubTotal, .tdGrandTotal        { font-weight: bold; }
            .tdGrandTotal, .tdSubTotal                 { text-align: right; }
            .tdSubTotalDetail           { border-top: 1px dotted; padding: 0.5mm 0; }
            .reportBody tr.trGroup0:not(:first-child) > td { padding-top: 20px; }
            .reportBody tr.trGroup0:not(:first-child) > td > tr.trGroup1:not(:first-child) > td { padding-top: 20px; }
            .pageFooter         { border-top: 1px solid; position: absolute; bottom: 0.5cm; left: 0.7cm; right: 0.7cm; font-size: 8pt; }
            .pageContent        { height:<%=paperPageContent %>; overflow-y: hidden; white-space: nowrap; }
            .tdAutoNumber       { padding-right:4px !important; }
            .borderTop          { border-top: 1px dotted; }
            .tdSignature        { padding-top:1.7cm; }
    /*border-collapse: separate;    margin-top: 0.1em;    border-spacing: 0;*/
            .divContainerReportHeader *     { font-weight: normal; }
            .divContainerReportHeader b     { font-weight: bold !important; }
            .divContainerReportHeader       { margin-bottom: 0.5cm; }
            .divContainerReportFooter       { margin-top: 0.1cm; }
            
            .tdReportTotal *                { font-weight: bold; font-size: 8pt; }
            
            .separator                      { color :#ADADAD; margin: 0 10px; }
            h1 { font-weight: bold; font-size: 12pt; margin-bottom: 0.5cm }
            h2 { font-weight: bold; font-size: 10pt; margin-bottom: 0.5cm; margin-top: -0.5cm; }
        </style>
    </head>
    <body>
        <input type="hidden" id="hdnParam" runat="server" />
        <center>
            <div id="toolbarArea">
                <div style="display:none;">
                    <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
                    <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
                </div>
                <input type="hidden" id="hdnExportExcel" runat="server" />
                <input type="button" id="btnPrint" value="Print" />
                <span class="separator">|</span>
                Page 
                <select id="cboJumpPage">
                </select>
                of <input type="text" id="txtTotalPage" value="1" style="width: 30px; text-align: right" readonly="readonly" class="number"/>
                <span class="separator">|</span>
                
                <input type="button" id="btnExportExcel" value="export" />
            </div>
        </center>
        <div id="pageArea">
            <div class="page">
                <div id="page1" class="pageContent">
                    <div class="divcontent">
                        <div class="pageFooter" id="divPageFooter" runat="server">
                            <div style="float: right;" class="divPageNumber">Page 1 of [TotalPageCount]</div>
                            <div id="divReportProperties" runat="server"></div>
                        </div>
                        <div class="pageHeader">
                            <div class="siteInformation" id="divPageHeader" runat="server">
                                <div style="float: right">
                                    <table>
                                        <tr>
                                            <td style="font-weight: normal;"><div id="divPhoneFaxNo" runat="server">Phone/Fax : 0717422605/0717422605</div></td>
                                        </tr>
                                    </table>
                                </div>
                                <table>
                                    <tr>
                                        <td><img src='<%=ResolveUrl("~/Libs/Images/logo.png") %>' width="50px" /></td>
                                        <td>
                                            <div id="divSiteName" runat="server"></div>
                                            <div id="divAddressLine1" runat="server" style="font-weight: normal;"></div>
                                            <div id="divAddressLine2" runat="server" style="font-weight: normal;"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <center><h1 id="headerText" runat="server" style="display: none"></h1></center><br style="display: none" />
                            <center><h2 id="subHeaderText" runat="server" style="display: none"></h2></center><br style="display: none" />
                            <div id="divContainerReportHeader" class="divContainerReportHeader" runat="server" style="display:none"></div>
                        </div>
                        <table class="tblReport" style="width:100%" cellpadding="0" cellspacing="0">
                            <thead>
                            <asp:Repeater ID="rptReport" runat="server">
                            </asp:Repeater>
                        </table>
                        <div id="divContainerReportFooter" class="divContainerReportFooter" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
