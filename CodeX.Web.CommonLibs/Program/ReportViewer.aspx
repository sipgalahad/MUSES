<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" 
    Inherits="CodeX.Web.CommonLibs.Program.ReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="ttlTitle" runat="server"></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body>
    <form id="myForm" runat="server">
        <input type="hidden" value="" id="hdnReportFileName" runat="server" />
        <input type="hidden" value="" id="hdnID" runat="server" />
        <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
        <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.4.3.js")%>' type='text/javascript'></script>
        <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.7.min.js")%>' type='text/javascript'></script>
        <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-barcode.js")%>' type='text/javascript'></script>
        <script src='<%= ResolveUrl("~/Libs/Scripts/PDF/jspdf.min.js")%>' type='text/javascript'></script>
        <script src='<%= ResolveUrl("~/Libs/Scripts/PDF/html2canvas.min.js?1")%>' type='text/javascript'></script>
        <script type="text/javascript">
            function generateBarcode($elm) {
                var value = $elm.html();
                var btype = 'code128';
                var renderer = 'bmp';

                var settings = {
                    output: renderer,
                    bgColor: '#FFFFFF',
                    color: '#000000',
                    barWidth: 1,
                    barHeight: 40,
                    moduleSize: 5,
                    posX: 1,
                    posY: 20,
                    addQuietZone: 1
                };
                $elm.html("").show().barcode(value, btype, settings);
            }

            $(function () {
                //localStorage.setItem("isReadLetterSpacingFromSetting", "1");
                // Retrieve
                if (localStorage.isReadLetterSpacingFromSetting) {
                    if (localStorage.isReadLetterSpacingFromSetting == '1') {
                        $('#myStyle').html($('#myStyle').html() + '@media print { *   { letter-spacing: <%=letterSpacingPrint %>; } }');
                    }
                }
                else
                    $('#myStyle').html($('#myStyle').html() + '@media print { *   { letter-spacing: <%=letterSpacingPrint %>; } }');

                var isUseLinux = false;
                if (localStorage.isUseLinux) {
                    if (localStorage.isUseLinux == '1')
                        isUseLinux = true;
                }
                if (!isUseLinux)
                    $('#myStyle').html($('#myStyle').html() + '@media print{@page {margin:0; }}');

                $('.barcode').each(function () {
                    generateBarcode($(this));
                });

                var height = $(window).height() - $('#toolbarArea').height() - 20;
                $('#pageArea').height(height);

                $('.trGroup1').each(function () {
                    if (!$(this).prev().hasClass('trGroup0')) {
                        $(this).addClass('trGroup1Child');
                    }
                });
                $('.trGroup2').each(function () {
                    if (!$(this).prev().hasClass('trGroup1')) {
                        $(this).addClass('trGroup2Child');
                    }
                });

                $.browser.chrome = /chrom(e|ium)/.test(navigator.userAgent.toLowerCase());
                $div = $('<div></div>');
                var html = '<style type="text/css">' + $('#divStyleExcel').html() + '</style>';
                html += '<div class="pageContent">' + $('#page1').html() + '</div>';
                $div.html(html);
                $div.find('.siteInformation').remove();
                $div.find('.tblReport').attr('border', '1');
                $div.find('.pageFooter').remove();

                $('#<%=hdnExportExcel.ClientID %>').val($div.html());

                showLoadingPanel();
                processOverflowDiv1(1);

                var noOfPrintCopy = parseInt('<%=noOfPrintCopy %>');
                for (var i = 0; i < noOfPrintCopy; ++i) {
                    var idx = $('.page').length;
                    $('.page').each(function () {
                        var p1 = $(this).find('.divcontent');

                        var containerp2 = $("<div class='page'><div id='page" + (idx + 1) + "' class='pageContent'><div class='divPrintCopy'><span>COPY</span></div>" + p1.html() + "</div></div>");
                        containerp2.insertAfter(p1.parent().parent());

                        idx++;
                    });
                }
            });

            function processOverflowDiv1(idx) {
                var cont1 = $('#page' + idx);
                var cont1Height = cont1.height();

                var p1 = $('#page' + idx + ' .divcontent');
                var p1Height = p1.height();

                $elm = p1.find('.divOverflow');
                if ($elm != null && $elm[0] != null) {
                    if ($elm[0].scrollHeight > $elm.innerHeight()) {
                        var newText = [];
                        var text = $elm.html();
                        var oldText = text.split(" ");
                        while (true) {
                            for (var i = 0; i < 1; ++i) {
                                var temp = oldText.pop();
                                newText.unshift(temp);
                            }
                            $elm.html(oldText.join(' '));
                            if ($elm[0].scrollHeight <= $elm.innerHeight()) {
                                var isFindEnd = false;
                                for (var i = oldText.length - 1; i >= 0; --i) {
                                    if (oldText[i].indexOf('>') > -1 && oldText[i].indexOf('><') < 0 && oldText[i].indexOf('>\n<') < 0) {
                                        isFindEnd = true;
                                    }
                                    if (oldText[i].indexOf('<') > -1) {
                                        if (!isFindEnd) {
                                            for (var j = oldText.length - 1; j >= i; --j) {
                                                var temp = oldText.pop();
                                                newText.unshift(temp);
                                            }
                                        }
                                        break;
                                    }
                                }

                                $elm.html(oldText.join(' '));
                                break;
                            }
                        }

                        var text = newText.join(" ").replace(/&(lt|gt|quot);/g, function (m, p) {
                            return (p == "lt") ? "<" : (p == "gt") ? ">" : "'";
                        });

                        newText = text.split(' ');
                        var newText2 = text.split(' ');

                        var isFindP = false;
                        var isFindDiv = false;
                        var isFindSpan = false;
                        for (var i = 0; i < newText.length; ++i) {
                            if (newText[i].indexOf('<p') > -1)
                                isFindP = true;
                            if (newText[i].indexOf('<div') > -1)
                                isFindDiv = true;
                            if (newText[i].indexOf('<span') > -1)
                                isFindSpan = true;

                            if (newText[i].indexOf('</p>') > -1) {
                                if (!isFindP)
                                    newText2.unshift(findElement('p', oldText));
                                isFindP = false;
                            }
                            if (newText[i].indexOf('</div>') > -1) {
                                if (!isFindDiv)
                                    newText2.unshift(findElement('div', oldText));
                                isFindDiv = false;
                            }
                            if (newText[i].indexOf('</span>') > -1) {
                                if (!isFindSpan)
                                    newText2.unshift(findElement('span', oldText));
                                isFindSpan = false;
                            }
                        }
                        text = newText2.join(" ");

                        var containerp2 = $("<div class='page'><div id='page" + (idx + 1) + "' class='pageContent'><div class='divcontent'><div class='pageFooter'></div><div class='pageHeader'></div></div></div></div>");
                        var containerp1 = p1.parent();

                        containerp2.find('.pageHeader').html(containerp1.find('.pageHeader').html());
                        containerp2.find('.pageFooter').html(containerp1.find('.pageFooter').html());
                        if (!containerp1.find('.pageFooter').is(':visible'))
                            containerp2.find('.pageFooter').hide();
                        containerp2.find('.divPageNumber').html("Page " + (idx + 1) + " of [TotalPageCount]");
                        var p2 = containerp2.find('.divcontent');

                        p2.append('<table class="tblReport" style="width:100%" cellpadding="0" cellspacing="0"><thead></thead><tbody class="reportBody"></tbody></table><div class="divContainerReportItem"></div><div class="divContainerReportFooter"></div>');
                        p2.find('.tblReport').find('thead').html(containerp1.find('.tblReport').find('thead').html());
                        p2.find('.divContainerReportFooter').html(containerp1.find('.divContainerReportFooter').html());
                        containerp1.find('.divContainerReportFooter').remove();

                        var p1text = p1.text();
                        p1text = p1text.split('');

                        $tbody = p1.find('.reportBody');
                        $tbody2 = p2.find('.reportBody');
                        $tbody2.find('.trReportBody').each(function () {
                            $(this).remove();
                        });
                        containerp2.insertAfter(p1.parent().parent());
                        var cont2 = $('#page' + (idx + 1));
                        var cont2Height = cont2.height();

                        p2.find('.divContainerReportItem').html(text);
                    }
                }

                if (p1Height > cont1Height) {
                    var containerp2 = $("<div class='page'><div id='page" + (idx + 1) + "' class='pageContent'><div class='divcontent'><div class='pageFooter'></div><div class='pageHeader'></div></div></div></div>");
                    var containerp1 = p1.parent();

                    containerp2.find('.pageHeader').html(containerp1.find('.pageHeader').html());
                    containerp2.find('.pageFooter').html(containerp1.find('.pageFooter').html());
                    if (!containerp1.find('.pageFooter').is(':visible'))
                        containerp2.find('.pageFooter').hide();
                    containerp2.find('.divPageNumber').html("Page " + (idx + 1) + " of [TotalPageCount]");
                    var p2 = containerp2.find('.divcontent');

                    p2.append('<table class="tblReport" cellpadding="0" cellspacing="0"><thead></thead><tbody class="reportBody"></tbody></table><div class="divContainerReportItem"></div><div class="divContainerReportFooter"></div>');
                    p2.find('.tblReport').find('thead').html(containerp1.find('.tblReport').find('thead').html());
                    p2.find('.divContainerReportFooter').html(containerp1.find('.divContainerReportFooter').html());
                    containerp1.find('.divContainerReportFooter').remove();

                    var p1text = p1.text();
                    p1text = p1text.split('');

                    $tbody = p1.find('.reportBody');
                    $tbody2 = p2.find('.reportBody');
                    $tbody2.find('.trReportBody').each(function () {
                        $(this).remove();
                    });
                    containerp2.insertAfter(p1.parent().parent());
                    var cont2 = $('#page' + (idx + 1));
                    var cont2Height = cont2.height();

                    if ($.browser.chrome) {
                        while (p1Height + 10 > cont1Height) {
                            $elm = $tbody.find('tr.trReportBody').last();
                            $tbody2.prepend($elm);

                            //re-evaluate height
                            p1Height = p1.height();
                            //loop
                        }
                    }
                    else {
                        var p2Height = 0;
                        var ctr = 0;
                        while (true) {
                            $elm = $tbody.find('tr.trReportBody').first();
                            /*if ($elm.attr('id') == 'trTest1') {
                            alert($elm.height() + ';' + p2Height + ';' + ($elm.height() + p2Height) + ';' + cont2Height);
                            }*/
                            if ($elm.height() + p2Height < cont2Height - 15) {
                                $tbody2.append($elm);

                                //re-evaluate height
                                p2Height = p2.height();
                                //loop
                                ctr++;
                            }
                            else
                                break;
                        }

                        $tempBody = $tbody2.html();
                        $tbody2.html($tbody.html());
                        $tbody.html($tempBody);
                    }

                    setTimeout(function () {
                        processOverflowDiv1(idx + 1);
                    }, 0);
                }
                else {
                    if (p1Height + $('.divCustomFooter').height() > cont1Height) {
                        var containerp2 = $("<div class='page'><div id='page" + (idx + 1) + "' class='pageContent'><div class='divcontent'><div class='pageFooter'></div><div class='pageHeader'></div></div></div></div>");
                        var containerp1 = p1.parent();

                        containerp2.find('.pageHeader').html(containerp1.find('.pageHeader').html());
                        containerp2.find('.pageFooter').html(containerp1.find('.pageFooter').html());
                        if (!containerp1.find('.pageFooter').is(':visible'))
                            containerp2.find('.pageFooter').hide();
                        containerp2.find('.divPageNumber').html("Page " + (idx + 1) + " of [TotalPageCount]");
                        var p2 = containerp2.find('.divcontent');

                        p2.append('<table class="tblReport" cellpadding="0" cellspacing="0"><thead></thead><tbody class="reportBody"></tbody></table><div class="divContainerReportItem"></div><div class="divContainerReportFooter"></div>');
                        p2.find('.tblReport').find('thead').html(containerp1.find('.tblReport').find('thead').html());
                        p2.find('.divContainerReportFooter').html(containerp1.find('.divContainerReportFooter').html());
                        containerp1.find('.divContainerReportFooter').remove();

                        var p1text = p1.text();
                        p1text = p1text.split('');

                        containerp2.insertAfter(p1.parent().parent());
                    }

                    var pageCount = $('.pageFooter').length;
                    $('.pageFooter').each(function () {
                        $(this).html($(this).html().replace('[TotalPageCount]', pageCount));
                    });

                    for (var i = 1; i <= pageCount; ++i) {
                        $('#cboJumpPage').append($("<option></option>").text(i));
                    }
                    $('#txtTotalPage').val(pageCount);

                    hideLoadingPanel();
                }
            }

            function findElement(tag, oldText) {
                var isFindElm = false;

                for (var i = oldText.length - 1; i >= 0; --i) {
                    var temp = oldText[i].split('\n');
                    for (var j = temp.length - 1; j >= 0; --j) {
                        if (temp[j].indexOf('</' + tag + '>') > -1)
                            isFindElm = true;

                        if (temp[j].indexOf('<' + tag) > -1) {
                            if (!isFindElm) {
                                var result = temp[j];
                                for (var k = i + 1; k < oldText.length; ++k) {

                                    if (oldText[k].indexOf('>') > -1) {
                                        result += ' ' + oldText[k].substr(0, oldText[k].indexOf('>')) + '>';
                                        return result;
                                    }
                                    else
                                        result += ' ' + oldText[k];
                                }
                            }
                            isFindElm = false;
                        }
                    }
                }
            }

            $(function () {
                $('#imgExport').click(function () {
                    if ($('#<%=ddlExportType.ClientID %>').val() == 'pdf') {
                        /*var ctr = 0;
                        var count = $('.page').length;
                        showLoadingPanel();
                        var lstImage = [];
                        for (var ctrImage = 0; ctrImage < count; ++ctrImage) {
                        lstImage.push('');
                        }
                        $('.page').each(function () {
                        var scaleBy = 5;
                        var div = $(this);
                        var w = $(this).outerWidth();
                        var h = $(this).outerHeight();
                        var canvas = document.createElement('canvas');
                        canvas.width = w * scaleBy;
                        canvas.height = h * scaleBy;
                        canvas.style.width = w + 'px';
                        canvas.style.height = h + 'px';
                        var context = canvas.getContext('2d');
                        context.scale(scaleBy, scaleBy);

                        $('#pageArea').attr('style', 'overflow-y:inherit');

                        html2canvas(div, {}).then(function (canvas) {
                        //$('#pageArea').attr('style', 'overflow-y:scroll');
                        var pageNumber = div.find('.pageContent').attr('id').substring(4);

                        ctr++;
                        var imgData = canvas.toDataURL("image/png");
                        lstImage[pageNumber - 1] = imgData;

                        if (ctr == count) {
                        $('#pageArea').attr('style', 'overflow-y:scroll');

                        var paperPortraitLandscape = "<%=paperPortraitLandscape %>".substr(0, 1);
                        var paperWidth = parseFloat("<%=paperWidth%>");
                        var paperHeight = parseFloat("<%=paperHeight%>");
                        var pdf = new jsPDF(paperPortraitLandscape, "mm", "<%=paperSize %>");

                        for (ctrImage = 0; ctrImage < count; ++ctrImage) {
                        if (ctrImage > 0)
                        pdf.addPage();
                        pdf.addImage(lstImage[ctrImage], 'JPEG', 0, 0, paperWidth, paperHeight);  // w h
                        }
                        pdf.save($('#<%=hdnReportFileName.ClientID %>').val() + '.pdf');
                        hideLoadingPanel();
                        }
                        });
                        });*/


                        var ctr = 0;
                        var count = $('.page').length;
                        showLoadingPanel();
                        var lstImage = [];
                        for (var ctrImage = 0; ctrImage < count; ++ctrImage) {
                            lstImage.push('');
                        }
                        $('.page').each(function () {
                            var bigCanvas = $("<div>").appendTo('body');
                            var scaledElement = $(this).clone()
                        .css({
                            'transform': 'scale(1,1)',
                            'transform-origin': '0 0'
                        }).appendTo(bigCanvas);
                            var oldWidth = scaledElement.outerWidth();
                            var oldHeight = scaledElement.outerHeight();

                            var newWidth = oldWidth * 2;
                            var newHeight = oldHeight * 2;

                            bigCanvas.css({
                                'width': newWidth,
                                'height': newHeight,
                                'margin': '0px',
                                'padding': '0px'
                            })
                            var page = bigCanvas.find('.page');
                            page.css({
                                'margin': '0px'
                            });


                            html2canvas(bigCanvas, {
                                onrendered: function (canvasq) {
                                    //$('#pageArea').attr('style', 'overflow-y:scroll');
                                    var pageNumber = bigCanvas.find('.pageContent').attr('id').substring(4);

                                    bigCanvas.remove();

                                    var resizeCanvas = document.createElement("canvas");
                                    resizeCanvas.height = canvasq.height / 2;
                                    resizeCanvas.width = canvasq.width / 2;

                                    var resizeCtx = resizeCanvas.getContext('2d');
                                    // Put original canvas contents to the resizing canvas
                                    resizeCtx.drawImage(canvasq, 0, 0, resizeCanvas.width, resizeCanvas.height, 0, 0, resizeCanvas.width, resizeCanvas.height);

                                    ctr++;
                                    var imgData = resizeCanvas.toDataURL("image/png");
                                    lstImage[pageNumber - 1] = imgData;

                                    if (ctr == count) {
                                        var paperPortraitLandscape = "<%=paperPortraitLandscape %>".substr(0, 1);
                                        var paperWidth = parseFloat("<%=paperWidth%>");
                                        var paperHeight = parseFloat("<%=paperHeight%>");
                                        var pdf = new jsPDF(paperPortraitLandscape, "mm", "<%=paperSize %>", true);

                                        for (ctrImage = 0; ctrImage < count; ++ctrImage) {
                                            if (ctrImage > 0)
                                                pdf.addPage();
                                            pdf.addImage(lstImage[ctrImage], 'JPEG', 0, 0, paperWidth, paperHeight, '', 'FAST');  // w h
                                        }
                                        pdf.save($('#<%=hdnReportFileName.ClientID %>').val() + '.pdf');
                                        hideLoadingPanel();
                                    }
                                }
                            });
                        });
                    }
                    else
                        $('#<%=btnExport.ClientID%>').click();
                });
                $('#imgPrint').click(function () {
                    window.print();
                });

                var totalPage = parseInt($('#txtTotalPage').val());
                $('#imgMoveNext').click(function () {
                    var page = parseInt($('#cboJumpPage').val());
                    if (page < totalPage)
                        page++;
                    goToByScroll('page' + page);
                    $('#cboJumpPage').val(page);
                });
                $('#imgMovePrev').click(function () {
                    var page = parseInt($('#cboJumpPage').val());
                    if (page > 1)
                        page--;
                    goToByScroll('page' + page);
                    $('#cboJumpPage').val(page);
                });
                $('#imgMoveFirst').click(function () {
                    var page = 1;
                    goToByScroll('page' + page);
                    $('#cboJumpPage').val(page);
                });
                $('#imgMoveLast').click(function () {
                    var page = totalPage;
                    goToByScroll('page' + page);
                    $('#cboJumpPage').val(page);
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
            function showLoadingPanel() {
                $('#loadingPanel').show();
            }

            function hideLoadingPanel() {
                $('#loadingPanel').hide();
            }
        </script>
        <div style="display:none;" id="divStyleExcel">
            * {
                box-sizing: border-box;
                -moz-box-sizing: border-box;
                font: <%=fontSize%> "<%=fontFamily%>";
            }
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
            .tdSignatureSmall   { padding-top:1cm; }
    /*border-collapse: separate;    margin-top: 0.1em;    border-spacing: 0;*/
            .divContainerReportHeader *     { font-weight: normal; }
            .divContainerReportHeader b     { font-weight: bold !important; }
            .divContainerReportHeader       { margin-bottom: 0.5cm; }
            .divContainerReportFooter       { margin-top: 0.1cm; }
            
            .tdReportTotal *                { font-weight: bold; font-size: 8pt; }
            
            h1 { font-weight: bold; font-size: 12pt; margin-bottom: 0.5cm }
            h2 { font-weight: bold; font-size: 10pt; margin-bottom: 0.5cm; margin-top: -0.5cm; }
        </div>

        <style type="text/css" id="myStyle">
             #loadingPanel                                   { display: none; }
            #loadingPanel .divBlanket                       { background-color: #EEE; opacity: 0.65; -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=65)"; -moz-opacity: 0.65; -khtml-opacity: 0.65; position: fixed; z-index: 29001; top: 0px; left: 0px; width: 100%; height: 100%; }
            #loadingPanel .divLoading                       { position: fixed; top: 50%; left: 50%; width: 200px; height: 50px; margin-top: -15px; margin-left: -100px; z-index: 29002; text-align: center; vertical-align: middle; }
            #loadingPanel .imgLoading                       { float: left; margin-top: 3px; }
    
            
            .imgLink        { cursor: pointer; }
            
            body {
                margin: 0;
                padding: 0;
                background-color: #FAFAFA;
            }
            @font-face {
                font-family: "TahomaLoad";
                src: url(../Styles/fonts/Tahoma/Tahoma.ttf);
            }
            @font-face {
                font-family: "TahomaLoad";
                font-weight: bold;
                src: url(../Styles/fonts/Tahoma/TahomaBd.ttf);
            }
            * {
                box-sizing: border-box;
                -moz-box-sizing: border-box;
                font: <%=fontSize%> <%=fontFamily%>;
            }
            .siteInformation *, .pageFooter * {
                font: 8pt <%=fontFamily%>;
            }
            @media screen 
            {
                #pageArea { width: 100%; overflow-y: scroll; }
                .page {
                    border: 0.1mm #D3D3D3 solid;
                    border-radius: 0.5mm;
                    background: white;
                    box-shadow: 0 0 0.5mm rgba(0, 0, 0, 0.1);
                    padding: 0.2cm 0.7cm;
                    margin: 0 auto 0.5cm auto;
                    width: <%=paperWidth%>mm;
                    height: <%=paperHeight%>mm;
                }
                .pageFooter         { border-top: 1px solid; position: absolute; bottom: 0.5cm; left: 0.7cm; right: 0.7cm; font-size: 8pt; }
                .divCustomFooter    { position: absolute; left: 0.7cm; right: 0.7cm; }
            }
            .page   { position: relative; }
            .pageContent        { height:<%=paperPageContent %>; overflow-y: hidden; }
            .divMargin          { height:<%=pageContentPaddingTop %>cm; }
            #toolbarArea { background-color: #E0E0E0;  border: 1px #ADADAD solid; border-radius: 3px; width: 440px; position: relative; padding: 5px 5px; }
            #toolbarArea, #toolbarArea *  { font-family: Segoe UI; font-size: 9pt; }
            p
            { page-break-before: always
            }

            @page {
                size: <%=paperSize %>;
                /*margin: 0;*/
            }
            @media print{@page {size: <%=paperPortraitLandscape%>; /*margin:0;*/ }}
            @media print {
                .page {
                    margin: 0;
                    border: initial;
                    border-radius: initial;
                    width: initial;
                    padding: <%=pagePaperPadding %>;
                    min-height: initial;
                    box-shadow: initial;
                    box-sizing: border-box;
                    background: initial;
                    page-break-after: always;
                    page-break-inside:avoid; 
                    width: auto;
                    height: auto;
                }
                .pageFooter         { border-top: 1px solid; position: absolute; bottom: 0.5cm; left: <%=leftRightPosition%>; right: <%=leftRightPosition%>; font-size: 8pt; }
                #toolbarArea        { display: none; }
                .pageContent        { height:<%=paperPrintPageContent %> !important; margin: 0; }
                #pageArea           { max-height: <%=paperPrintPageContent %>; }
                .divCustomFooter    { position: absolute; left: <%=leftRightPosition%>; right: <%=leftRightPosition%>; }
            }
            
            .divcontent         { margin: <%=customMargin %>; }
            
            .tblReport                          { table-layout:fixed; word-wrap:break-word; width: 100% !important;  }
            .tblReport .tdDetail, .tblHeader th { padding-right:0.1cm; padding-left:0.1cm; word-wrap:break-word !important; }
            .tblReport .tdDetail                { border-bottom: <%=borderBottomDetail%>; }
            .tdDetail           { vertical-align: top; }
            thead th            { font-weight: <%=fontWeight%> }
            .tblHeader th            { border-bottom: 1px solid; }
            .tblHeader:nth-child(1) th            { border-top: 1px solid; }
            .tblBorder th:last-child            { border-right: 1px solid; }
            .tblBorder th            { border-left: 1px solid; border-collapse:collapse; }
            thead { display:table-row-group; }
            .tdGroupName, .tdSubTotal, .tdGrandTotal        { font-weight: bold; }
            .tdGrandTotal, .tdSubTotal                 { text-align: right; }
            .tdSubTotalDetail           { border-top: 1px dotted; padding: 0.5mm 0; white-space: nowrap; }
            .reportBody tr.trGroup0:not(:first-child) > td { padding-top: 20px; }
            .reportBody tr.trGroup0:not(:first-child) > td > tr.trGroup1:not(:first-child) > td { padding-top: 20px; }
            .reportBody .trGroup1Child > td, .reportBody .trGroup2Child > td { padding-top: 20px; }
            .tdAutoNumber       { padding-right:4px !important; }
            .borderTop          { border-top: 1px dotted; }
            .tdSignature        { padding-top:1.7cm; }
            .tdSignatureSmall   { padding-top:1cm; }
    /*border-collapse: separate;    margin-top: 0.1em;    border-spacing: 0;*/
            .divContainerReportHeader *     { font-weight: normal; }
            .divContainerReportHeader b     { font-weight: <%=fontWeight%> !important; }
            .divContainerReportHeader       { margin-bottom: 0.5cm; }
            .divContainerReportFooter       { margin-top: 0.1cm; }
            
            .tdReportTotal *                { font-weight: <%=fontWeight%>; font-size: 8pt; }
            
            .separator                      { color :#ADADAD; margin: 0 10px; }
            h1 { font-weight: <%=fontWeight%>; font-size: <%=h1FontSize%>; margin-bottom: 0.5cm }
            h2 { font-weight: <%=fontWeight%>; font-size: 10pt; margin-bottom: 0.5cm; margin-top: -0.5cm; }
            .tblReportParameterDt td:nth-child(1)          { width: 150px; font-weight: <%=fontWeight%>; }
            .tblReportParameterDt td:nth-child(2)          { width: 10px; }
            
            .divCircle          { border-radius: 50%;width: 22px; height: 22px;background: #0099CC; padding: 3px 0 0 0px; }
            
            .divPageNumber               { <%=divPageNumberStyle%> }
            
            .tdEntityHeader                 { font-weight: bold !important; }
            
            .divPrintCopy                   { position: absolute; top:0; bottom:0; left:0; right:0; width: 80px; height: 40px; margin:auto;-webkit-transform: rotate(-10deg); -moz-transform: rotate(-10deg); filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2); }
            .divPrintCopy span              { border:1px solid #F00; color: #F00; padding: 5px; font-size:24pt; }
        </style>
    </head>
    <body>
        <div id="divTest"></div>
        <input type="hidden" id="hdnParam" runat="server" />
        <input type="hidden" id="hdnLang" runat="server" />
        <input type="hidden" id="hdnFacility" runat="server" />
        <input type="hidden" id="hdnServiceUnit" runat="server" />
        <input type="hidden" id="hdnPosition" runat="server" />
        <center>
            <div id="toolbarArea">
                <div style="display:none;">
                    <asp:Button ID="btnTemp" Visible="true" runat="server" OnClientClick="return false" Text="Export" />
                    <asp:Button ID="btnExport" Visible="true" runat="server" OnClick="btnExport_Click" Text="Export" />
                </div>
                <input type="hidden" id="hdnExportExcel" runat="server" />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/print.png") %>' title="Print" id="imgPrint" class="imgLink" height="16px" /></div></td>
                        <td><span class="separator">|</span></td>
                        <td style="width:26px;" align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/movefirst.png") %>' title="First" id="imgMoveFirst" class="imgLink" height="16px" /></div></td>
                        <td style="width:26px;" align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/moveprev.png") %>' title="Prev" id="imgMovePrev" class="imgLink" height="16px" /></div></td>
                        <td style="width:140px;" align="center">
                            Page 
                            <select id="cboJumpPage">
                            </select>
                            of <input type="text" id="txtTotalPage" value="1" style="width: 30px; text-align: right" readonly="readonly" class="number"/>
                        </td>
                        <td style="width:26px;" align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/movenext.png") %>' title="Next" id="imgMoveNext" class="imgLink" height="16px" /></div></td>
                        <td style="width:26px;" align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/movelast.png") %>' title="Last" id="imgMoveLast" class="imgLink" height="16px" /></div></td>
                        <td><span class="separator">|</span></td>
                        <td style="width:26px;" align="center"><div class="divCircle"><img src='<%=ResolveUrl("~/Libs/Images/Report/export.png") %>' title="Export" id="imgExport" class="imgLink" height="16px" /></div></td>
                        <td align="center" style="padding-left:10px;">
                            <asp:DropDownList ID="ddlExportType" runat="server" Width="50px">
                                <asp:ListItem Value="exc" Text="Excel" />
                                <asp:ListItem Value="pdf" Text="PDF" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        <div id="pageArea">
            <div class="page">
                <div id="page1" class="pageContent">
                    <div class="divcontent">
                        <div class="pageFooter" id="divContainerPageFooter" runat="server">
                            <div style="float: right;" class="divPageNumber">Page 1 of [TotalPageCount]</div>
                            <div id="divReportProperties" runat="server"></div>
                        </div>
                        <div class="divMargin" style="width:100%;">
                        
                        </div>
                        <div class="pageHeader">
                            <div class="siteInformation" id="divPageHeader" runat="server">
                                <div style="float: right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="font-weight: normal;"><div id="divPrintDateTime" runat="server"></div></td>
                                        </tr>
                                    </table>
                                </div>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="tdImageLogo" runat="server" style="padding-right: 10px;"><img src='<%=GetImageLogo() %>' width="50px" /></td>
                                        <td>
                                            <div id="divSiteName" runat="server" style="font-weight:bold; font-size:1.1em"></div>
                                            <div id="divAddressLine1" runat="server" style="font-weight: normal;"></div>
                                            <div id="divAddressLine2" runat="server" style="font-weight: normal;"></div>
                                            <div id="divPhoneFaxNo" runat="server">Phone/Fax : 0717422605/0717422605</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <center><h1 id="headerText" runat="server" style="display: none"></h1></center><br style="display: none" />
                            <center><h2 id="subHeaderText" runat="server" style="display: none"></h2></center><br style="display: none" />
                            <div id="divContainerReportHeader" class="divContainerReportHeader" runat="server" style="display:none"></div>
                            <div id="divContainerReportParameter" class="divContainerReportParameter" runat="server"></div>
                        </div>
                        <div id="divContainerReportBody" runat="server">
                            <asp:Repeater ID="rptReport" runat="server">
                            </asp:Repeater>
                        </div>
                        <div id="divContainerReportItem" class="divContainerReportItem" runat="server"></div>
                        
                        <div id="divContainerReportFooter" class="divContainerReportFooter" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>
        <img id="imgPreview" style="width:100%" />
    </form>
    <div id="loadingPanel">
        <div class="divBlanket">
        </div>
        <div class="divLoading">
            <table style="margin-left: auto; margin-right: auto;">
                <tr>
                    <td>
                        <img class="imgLoading" src="<%=ResolveUrl("~/Libs/Images/Loading.gif")%>" alt="0" />
                    </td>
                    <td style="padding-left: 5px">
                        <div class="txtLoading"></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
