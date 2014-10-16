<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.GeneralCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>

<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.4.3.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery-1.7.min.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery.validate.min.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery.paginate.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery.colorPicker.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.core.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.widget2.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/Jquery/ui/jquery.ui.accordion.js") %>' type="text/javascript"></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.button.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.position.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.autocomplete.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.datepicker.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/TinyMce/tiny_mce.js")%>' type="text/javascript" ></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/Constant.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/Methods.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/dropdown/superfish.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery.tmpl.min.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/jquery.maphilight.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/CustomControl/QISClientIntellisenseTextBox.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/CustomControl/QISClientSearchTextBox.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/CustomControl/QISClientQuickEntry.js")%>' type='text/javascript'></script>
<script src='<%= ResolveUrl("~/Libs/Scripts/CustomControl/CodeXClientAutoComplete.js")%>' type='text/javascript'></script>
<script type="text/javascript">
    var baseUrl = '<%= ResolveUrl("~/") %>';
    window.ResolveUrl = function (url) {
        if (url.indexOf("~/") == 0) {
            url = baseUrl + url.substring(2);
        }
        return url;
    }

    if ($.browser.mozilla) {
        $(document).keypress(bodyKeyPressGeneral);
    } else {
        $(document).keydown(bodyKeyPressGeneral);
    }
    function bodyKeyPressGeneral(e) {
        if (e.target.tagName.toUpperCase() == 'INPUT' || e.target.tagName.toUpperCase() == 'TEXTAREA') return;
        var charCode = (e.which) ? e.which : e.keyCode;
        switch (charCode) {
            case 39: e.preventDefault(); break; //right
        }
    }

    //#region Num Entries
    function setNumEntriesText($elm, rowCount, page, rowCountPerPage) {
        var end = rowCountPerPage * page;
        var start = end - rowCountPerPage + 1;
        if (end > rowCount)
            end = rowCount;
        var text = 'Showing <b>${StartRow}</b> to <b>${EndRow}</b> of <b>${RowCount}</b> entries';
        var text = text.replace('${RowCount}', rowCount);
        var text = text.replace('${StartRow}', start);
        var text = text.replace('${EndRow}', end);
        $elm.html(text);
    }
    //#endregion

    //#region Collapse Expand
    function registerCollapseExpandHandler() {
        registerCollapseHandler();
        registerExpandHandler();
    }

    function registerCollapseHandler() {
        $('h4.h4collapsed').click(function () {
            $elm = $(this);
            $(this).next('div.containerTblEntryContent').slideDown('fast', function () {
                $elm.removeClass('h4collapsed');
                $elm.addClass('h4expanded');
                $elm.unbind('click');
                registerExpandHandler();
            });
        });
    }

    function registerExpandHandler() {
        $('h4.h4expanded').click(function () {
            $elm = $(this);
            $(this).next('div.containerTblEntryContent').slideUp('fast', function () {
                $elm.removeClass('h4expanded');
                $elm.addClass('h4collapsed');
                $elm.unbind('click');
                registerCollapseHandler();
            });
        });
    }
    //#endregion

    //#region Report Viewer
    function openReportViewer(reportCode, param) {
        var win = window.open("", reportCode.replace('-', ''), 'status=1,toolbar=0,menubar=0,resizable=1,location=0,scrollbars=1,width=1150,height=600');
        win.focus();

        var mapForm = document.createElement("form");
        mapForm.target = reportCode.replace('-', '');
        mapForm.method = "POST";
        mapForm.action = ResolveUrl('~/Libs/Program/ReportViewer.aspx?id=' + reportCode);

        var mapInput = document.createElement("input");
        mapInput.type = "hidden";
        mapInput.name = "param";
        mapInput.value = param;
        mapForm.appendChild(mapInput);

        document.body.appendChild(mapForm);

        mapForm.submit();

        $(mapForm).remove();
    }
    //#endregion

    //#region HTML Editor
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        editor_selector: "htmlEditor",
        encoding : "xml",
        plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,visualblocks",

        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: false,

        content_css: ResolveUrl("~/Libs/Styles/TinyMce/content.css"),

        content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/template_list.js"),
        content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/link_list.js"),
        content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/image_list.js"),
        content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/media_list.js"),

        style_formats: [
            { title: 'Bold text', inline: 'b' },
            { title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
            { title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
            { title: 'Example 1', inline: 'span', classes: 'example1' },
            { title: 'Example 2', inline: 'span', classes: 'example2' },
            { title: 'Table styles' },
            { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
        ],

        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        },

        onchange_callback: function (ed) {
            ed.save();
        }
    });

    function setHtmlEditor() {
        //#region HTML Editor
        tinyMCE.init({
            mode: "textareas",
            theme: "advanced",
            editor_selector: "htmlEditor",
            encoding: "xml",
            plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,visualblocks",

            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: false,

            content_css: ResolveUrl("~/Libs/Styles/TinyMce/content.css"),

            content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/template_list.js"),
            content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/link_list.js"),
            content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/image_list.js"),
            content_css: ResolveUrl("~/Libs/Scripts/TinyMce/lists/media_list.js"),

            style_formats: [
            { title: 'Bold text', inline: 'b' },
            { title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
            { title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
            { title: 'Example 1', inline: 'span', classes: 'example1' },
            { title: 'Example 2', inline: 'span', classes: 'example2' },
            { title: 'Table styles' },
            { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
        ],

            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            },

            onchange_callback: function (ed) {
                ed.save();
            }
        });
        //#endregion
    }
    //#endregion

    //#region Currency
    Number.prototype.formatMoney = function (c, d, t) {
        var n = this,
    c = isNaN(c = Math.abs(c)) ? 2 : c,
    d = d == undefined ? "." : d,
    t = t == undefined ? "," : t,
    s = n < 0 ? "-" : "",
    i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
    j = (j = i.length) > 3 ? j % 3 : 0;
        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    };

    $('.txtCurrency').live('blur', function () {
        if ($(this).attr('readonly') != 'readonly') {
            $(this).trigger('changeValue');
        }
    });

    $('.txtCurrency').live('focus', function () {
        if ($(this).attr('readonly') != 'readonly') {
            $value = $(this).attr('hiddenVal');
            if ($value != null)
                $(this).val($value);
        }
    });
    $('.txtCurrency').die('changeValue');
    $('.txtCurrency').live('changeValue', function () {
        $hiddenVal = $(this).val();
        if ($hiddenVal == '')
            $hiddenVal = '0';
        if ($hiddenVal.indexOf(',') > -1 && $hiddenVal.indexOf('.') > -1) {
        }
        else {
            //$hiddenVal = Math.floor(parseFloat($hiddenVal) * 100 / 100);
            $hiddenVal = parseFloat($hiddenVal).toFixed(2);
            $val = parseFloat($hiddenVal).formatMoney(2, '.', ',');
            $(this).val($val);
            //$(this).val(parseFloat($hiddenVal).toFixed(2));
            $(this).attr('hiddenVal', $hiddenVal);
        }
    })

    $(function () {
        $('.txtCurrency').each(function () {
            $(this).val($(this).val().replace('.00', ''));
            $(this).trigger('changeValue');
        });
    });
    //#endregion

    //#region Context Menu
    function showContextMenu($ctxMenu, e) {
        var top = 0;
        var left = 0;
        if (e.pageY < $(window).height() / 2) {
            top = e.pageY;
            left = e.pageX;
        }
        else {
            top = e.pageY - $ctxMenu.height();
            left = e.pageX;
        }

        if (left + $ctxMenu.width() > $(window).width())
            left = left - $ctxMenu.width();
        $ctxMenu.css({ top: top + "px", left: left + "px" }).show(100);
    }
    //#endregion

    //#region Search Dialog
    var grdSearchID = '<%=grdSearch.ClientID %>';
    var onClickRowSearchDialogHandler = null;
    var searchDialogSearchType = '';
    var searchDialogFilterExpression = '';
    var isInitSearchDialog = false;
    var lastSearchText = '';
    var lastIndex = '';
    $(function () {
        pcSearchDialog.SetHeaderText('<%= GetLabel("Search Dialog")%>');
        $imgClose = '<%= ResolveUrl("~/Libs/Images/close-icon.png")%>';

        $('.dxWeb_pcCloseButton').each(function () {
            $td = $(this).parent();
            $(this).remove();
            $td.append($("<img src='" + $imgClose + "' height='32'/>"));
        });
        $('#txtSearchResult').keydown(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13)
                highlightText();
        });

        $('#' + grdSearchID + ' tr:gt(0)').live('click', function () {
            if ($('#' + grdSearchID + ' td').length > 1) {
                //showLoadingPanel();
                onClickRowSearchDialogHandler($(this).find('td:eq(0)').html());
                pcSearchDialog.Hide();
                //hideLoadingPanel();
            }
        });

        /*$('#' + grdSearchID + ' th').live('click', function () {
        if ($('#' + grdSearchID + ' td').length > 1) {
        var sortedClassName = 'ASC';
        if ($(this).attr('class') != null && $(this).attr('class').indexOf('ASC') > -1)
        sortedClassName = 'DESC';
        $('#' + grdSearchID + ' th').each(function () {
        $(this).removeClass('ASC');
        $(this).removeClass('DESC');
        });
        $(this).addClass(sortedClassName);
        var idx = parseInt($(this).index()) - 1;
        cbpSearchDialog.PerformCallback('sort|' + idx + '|' + sortedClassName);
        }
        });*/

        function isTextSelected(input) {
            var startPos = input.selectionStart;
            var endPos = input.selectionEnd;
            var doc = document.selection;

            if (doc && doc.createRange().text.length != 0) {
                return true;
            } else if (!doc && input.value.substring(startPos, endPos).length != 0) {
                return true;
            }
            return false;

            /*if (typeof input.selectionStart == "number") {
            return input.selectionStart == 0 && input.selectionEnd == input.value.length;
            } else if (typeof document.selection != "undefined") {
            input.focus();
            return document.selection.createRange().text == input.value;
            }*/
        }

        $('.NoRM').live('keydown', function (e) {
            if (isTextSelected($(this)[0])) {
            }
            else {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code > 47 && code < 59 || code == 173 || code == 189) {
                    var val = $(this).val();
                    if (val.length == 11) {
                        e.preventDefault();
                    }
                    else {
                        if (val.length == 1 || val.length == 4 || val.length == 7) {
                            var c = String.fromCharCode(e.which);
                            val += c + '-';
                            e.preventDefault();
                        }
                        $(this).val(val);
                    }
                }
                else if (e.ctrlKey || code == 9 || code == 8 || code == 34 || code == 35 || code == 36 || code == 37 || code == 38 || code == 39 || code == 40) {
                }
                else
                    e.preventDefault();
            }
        });
    });

    function openSearchDialog(searchType, filterExpression, functionHandler) {
        searchDialogSearchType = searchType;
        searchDialogFilterExpression = filterExpression;
        onClickRowSearchDialogHandler = functionHandler;
        pcSearchDialog.Show();
    }

    function highlightText() {
        var text = $('#txtSearchResult').val();
        var inputText = document.getElementById("containerSearchResult");
        var innerHTML = inputText.innerHTML.replace(/\"/g, '').replace(/<span class=highlightText>([_A-Z0-9a-z-+\.]+)<\/span>/g, '$1').replace(/<SPAN class=highlightText>([_A-Z0-9a-z-+\.]+)<\/SPAN>/g, '$1'); ;

        var index = 0;
        if (lastSearchText != text) {
            index = innerHTML.toUpperCase().indexOf(text.toUpperCase());
        }
        else {
            index = innerHTML.toUpperCase().substr(lastIndex + 1).indexOf(text.toUpperCase());
            index = lastIndex + index + 1;
        }
        if (index >= 0 && lastIndex != index) {
            innerHTML = innerHTML.substring(0, index) + "<span class='highlightText'>" + innerHTML.substring(index, index + text.length) + "</span>" + innerHTML.substring(index + text.length);
            inputText.innerHTML = innerHTML;
            lastIndex = index;
            lastSearchText = text;
        }
        else {
            inputText.innerHTML = innerHTML;
            showToast('Warning', 'No further occurence of "' + text + '" were found');
            lastIndex = 0;
            lastSearchText = '';
        }
    }

    function onCbpSearchDialogEndCallback(s) {
        if (isInitSearchDialog) {
            var intellisenseHints = $.parseJSON('[' + s.cpIntellisenseHints + ']');
            txtQuickSearchDialogHelper.setIntellisenseHints(intellisenseHints);
            txtQuickSearchDialog.setIntellisenseHints(intellisenseHints);
            //txtQuickSearchDialog.SetFocus();
            isInitSearchDialog = false;
        }
        //$th = $('#' + grdSearchID + ' th:eq(' + (parseInt(s.cpSortedIndex) + 1) + ')');
        //$th.addClass(s.cpSortedType);

        $('#containerImgLoadingSearchDialog').hide();
    }
    //#endregion

    //#region Loading Panel
    function showLoadingPanel() {
        $('#loadingPanel').show();
    }

    function hideLoadingPanel() {
        $('#loadingPanel').hide();
    }

    function isLoadingPanelVisible() {
        return $('#loadingPanel').is(":visible");
    }
    //#endregion

    //#region Toast Panel
    var functionHandlerCloseToast = null;
    function showToast(title, message, fn) {
        functionHandlerCloseToast = fn;
        $('#toastPanel .messageHeader').html(title);
        $('#toastPanel .messageText').html(message);
        $('#toastPanel').show();
    }

    function hideToast() {
        $('#toastPanel').hide();
        if (functionHandlerCloseToast != null)
            functionHandlerCloseToast();
    }

    var functionHandlerCloseToast = null;
    function showToastConfirmation(message, fn) {
        functionHandlerCloseToast = fn;
        $('#toastConfirmationPanel .messageText').html(message);
        $('#toastConfirmationPanel').show();
    }

    function hideToastConfirmation(result) {
        $('#toastConfirmationPanel').hide();
        if (functionHandlerCloseToast != null)
            functionHandlerCloseToast(result);
    }
    //#endregion

    //#region Popup Content
    function openUserControlPopup(url, param, title, width, height, code) {
        code = typeof code !== 'undefined' ? code : '';
        $('#hdnRightPanelContentCode').val(code);
        $('#hdnRightPanelContentIsLoadContent').val('1');
        $('#hdnRightPanelContentUrl').val(url);
        $('#hdnRightPanelContentFirstTimeLoad').val('1');
        $('#hdnRightPanelContentParam').val(param);
        $('#hdnRightPanelContentTitle').val(title); 
        pcRightPanelContent.SetHeaderText(title);
        pcRightPanelContent.SetSize(width, height);
        pcRightPanelContent.Show();
    }

    function onCbpRightPanelContentBeginCallback() {
        showLoadingPanel();
    }

    function onCbpRightPanelContentEndCallback() {
        hideLoadingPanel();        
        $('#hdnRightPanelContentFirstTimeLoad').val('0');
    }

    function onPcRightPanelContentClosing() {
        $('#hdnRightPanelContentIsLoadContent').val('0');
        $('#hdnRightPanelContentUrl').val('');
        $('#<%=pnlRightPanelContentArea.ClientID %>').empty();
        if (typeof onAfterPopupControlClosing == 'function') {
            onAfterPopupControlClosing();
        }
    }
    //#endregion

    //#region Validate
    jQuery.extend(jQuery.validator.messages, {
        required: "",
        remote: "Please fix this field.",
        confirmpassword: "",
        confirmmobilepin: "",
        email: "",
        url: "Please enter a valid URL.",
        date: "Please enter a valid date.",
        dateISO: "Please enter a valid date (ISO).",
        number: "",
        digits: "",
        time: "",
        datepicker: "",
        creditcard: "Please enter a valid credit card number.",
        equalTo: "Please enter the same value again.",
        accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
        minlength: jQuery.validator.format("Please enter at least {0} characters."),
        rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        max: "",
        min: ""
    });

    window.IsValid = function (evt, fieldID, validationGroup) {
        var $group = $('#' + fieldID);
        var result = true;
        $group.find(':[validationgroup=' + validationGroup + ']').each(function (i, item) {
            if (typeof $(item).attr('readonly') == "undefined") {
                if (typeof $(item).attr('class') != "undefined") {
                    if ($(item).attr('class').indexOf("required") < 0 && $(item).val() == "") {
                    }
                    else if ($(item).is(':visible')) {
                        if (!$(item).valid())
                            result = false;
                    }
                }
            }
        });
        if (result) {
            if (typeof ASPxClientEdit != 'undefined') {
                result = ASPxClientEdit.ValidateGroup(validationGroup);
            }
        }
        return result;
    }

    function isValidDate(date) {
        var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(date);
        if (matches == null) return false;
        var d = matches[1];
        var m = matches[2] - 1;
        var y = matches[3];
        var composedDate = new Date(y, m, d);
        return composedDate.getDate() == d &&
            composedDate.getMonth() == m &&
            composedDate.getFullYear() == y;
    }

    $(function () {
        $('span.required input:radio').each(function () {
            $(this).addClass('required');
        });
        $.validator.addMethod("time", function (value, element) {
            return this.optional(element) || /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/i.test(value);
        }, "");
        $.validator.addMethod("datepicker", function (value, element) {
            return isValidDate(value);
        }, "");
    });
    //#endregion

    //#region Datepicker
    function setDatePicker(id) {
        $('#' + id).datepicker({
            defaultDate: "w",
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            showOn: "button", 
            //showOn: "both",
            buttonImage: ResolveUrl("~/Libs/Images/calendar.gif"),
            buttonImageOnly: true
        });
    }

    function setDatePickerElement($elm) {
        $elm.datepicker({
            defaultDate: "w",
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            showOn: "button",
            //showOn: "both",
            buttonImage: ResolveUrl("~/Libs/Images/calendar.gif"),
            buttonImageOnly: true
        });
    } 
    
    $(function () {
        $('.datepicker').each(function () {
            $(this).attr('placeholder', 'dd-MM-yyyy');
        });
    });
    /*$(function () {
        $('.datepicker').datepicker({
            defaultDate: "w",
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-M-yy",
            beforeShowDay: highlightDays
        });

        var dates = ['04/30/2013', '05/01/2013'];
        function highlightDays(date) {
            for (var i = 0; i < dates.length; i++) {
                if (new Date(dates[i]).toString() == date.toString()) {
                    return [true, 'holiday'];
                }
            }
            return [true, ''];

        }
    });*/
    //#endregion

    //#region Today Date
    var todayDateInString = '<%=TodayDate%>';
    window.todayDate = Methods.stringToDate(todayDateInString);
    //#endregion
    
    //#region AppSession
    var AppSession = new (function () {
        this.siteID = '<%=SiteID%>';
        this.userID = parseInt('<%=UserID%>');
    })();
    //#endregion

    //#region Paging
    function setPaging($elm, pageCount, onPageChanged, display, start) {
        if (display == null)
            display = 12;
        if (start == null)
            start = 1;
        if (pageCount > 0) {
            $elm.closest('.containerPaging').show();
            $elm.paginate({
                count: pageCount,
                start: start,
                display: display,
                border: false,
                text_color: '#a5a5a5',
                background_color: 'none',
                text_hover_color: '#a5a5a5',
                background_hover_color: 'none',
                images: false,
                mouse: 'press',
                onChange: function () {
                    var page = $elm.find('.jPag-current').html();
                    onPageChanged(page);
                }
            });
        }
        else
            $elm.closest('.containerPaging').hide();
    }
    //#endregion

    function openMatrixControl(type, param, headerText) {
        var url = ResolveUrl("~/Libs/Controls/MatrixCtl.ascx");
        openUserControlPopup(url, type + '|' + param, headerText, 1000, 500);
    }
    function openMatrixWithParameterControl(type, param, headerText) {
        var url = ResolveUrl("~/Libs/Controls/MatrixWithParameterCtl.ascx");
        openUserControlPopup(url, type + '|' + param, headerText, 1000, 500);
    }

    //#region Utility
    function setCheckBoxEnabled($item, isEnabled) {
        if (isEnabled)
            $item.removeAttr('disabled');
        else
            $item.attr('disabled', 'disabled');
    }

    function rowToObject($row) {
        var selectedObj = {};
        $row.find('input[type=hidden]').each(function () {
            if ($(this).attr('bindingfield') != null)
                selectedObj[$(this).attr('bindingfield')] = $(this).val();
        });
        return selectedObj;
    }
    //#endregion
</script>
<style type="text/css">
    #loadingPanel                                   { display: none; }
    #loadingPanel .divBlanket                       { background-color: #EEE; opacity: 0.65; -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=65)"; -moz-opacity: 0.65; -khtml-opacity: 0.65; position: fixed; z-index: 29001; top: 0px; left: 0px; width: 100%; height: 100%; }
    #loadingPanel .divLoading                       { position: fixed; top: 50%; left: 50%; width: 200px; height: 50px; margin-top: -15px; margin-left: -100px; z-index: 29002; text-align: center; vertical-align: middle; }
    #loadingPanel .imgLoading                       { float: left; margin-top: 3px; }
    
    /* #region Right Panel */
    #containerRightPanel                            { font-size: 0.9em; }
    .divOpenRightPanelContent                       { max-height: 105px; display: none; }
    #tdOpenRightPanel                               { opacity: 0.6; }
    #tdOpenRightPanel.open                          { opacity: 1; }
    .textRightPanel                                 { position: absolute; right: -27px; top: 33px; text-align: center; width: 80px; color: #FFFFFF; -webkit-transform: rotate(-90deg); -moz-transform: rotate(-90deg); filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3); }
    .divRightPanelBackground                        { position:relative; cursor: pointer; background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_mrotate.png")%>');height:70px;width:30px;margin: 0px auto;color:White; padding-top:10px; }
    .divRightPanelBackgroundTop                     { cursor: pointer; background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_t.png")%>');height: 13px; width: 30px; }
    .divRightPanelBackgroundBottom                  { cursor: pointer; background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_btm.png")%>'); height: 13px; width: 30px; }
    .divRightPanelBackground:hover .textRightPanel  { text-decoration:underline; }
    
    .divRightPanelBackground.selected               { background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_mrotate_sel.png")%>'); }
    .divRightPanelBackgroundTop.selected            { background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_t_sel.png")%>'); }
    .divRightPanelBackgroundBottom.selected         { background-image:url('<%=ResolveUrl("~/Libs/Images/Slide/tab_btm_sel.png")%>'); }
    .divRightPanelBackground.selected .textRightPanel { color: #000; }
    
    .containerRightPanelContent                     { display: none; }
    #divListRightPanel .rightPanelContent           { width: 100%; padding: 10px 2px; }
    #divListRightPanel .rightPanelContent div       { margin-right:20px; }
    #divListRightPanel .rightPanelContent div.qmtitle { font-size:1.2em !important; }
    #divListRightPanel .rightPanelContent div.qmdescription { font-size:1em; }
    #divListRightPanel .rightPanelContent a         { border: 0px; text-decoration: none; padding: 4px 10px; float: right; width: 20px; }
    #divListRightPanel .rightPanelContent a[enabled="false"] { /*background-color: #EAEAEA; color: #BEBEBE;*/background-color:#AAA; color: #979797; cursor: not-allowed; }
    .containerRightPanelContent.print               { font-size: 14px; }
    #headerRightPanel                               { border-bottom: 1px solid #FFF; padding-bottom: 5px; }
    #imgCloseRightPanel                             { cursor: pointer; }
    #headerRightPanelTitle                          { float: right; padding: 0 5px 0 0; font-size: 1.6em; }
    #imgRightPanelPrint                             { cursor: pointer; }
    #imgRightPanelPrint:hover                       { background-color: #F39200; }
    /* #endregion */
</style>
<!--[if IE]>
<style type="text/css">
    .textRightPanel                                 { right: -60px; top: 0;  }
</style>
<![endif]-->

<div style="position:absolute;">
    <dxpc:ASPxPopupControl ID="pcRightPanelContent" runat="server" ClientInstanceName="pcRightPanelContent" EnableHierarchyRecreation="True"
        FooterText="" HeaderText="" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="950px" Height="600px"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ClientSideEvents Shown="function (s,e) { cbpRightPanelContent.PerformCallback(); }" 
            Closing="function(s,e) { onPcRightPanelContentClosing(); }"/>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div style="text-align: left; width: 100%;">
                    <dxcp:ASPxCallbackPanel ID="cbpRightPanelContent" runat="server" Width="100%" ClientInstanceName="cbpRightPanelContent"
                        ShowLoadingPanel="false" OnCallback="cbpRightPanelContent_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ onCbpRightPanelContentBeginCallback(); }"
                            EndCallback="function(s,e){ onCbpRightPanelContentEndCallback(); }"  />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <asp:Panel runat="server" ID="pnlRightPanelContentArea" Style="width: 100%; margin-left: auto; margin-right: auto">
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </div>                
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</div>

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

<div id="toastPanel" class="toastPanel">
    <div class="tblToast">
        <div class="tblCellToast">
            <div class="toastBox">
                <div class="messageContainer">
                    <h2 class="messageHeader"><%=GetLabel("Warning")%></h2>
                    <div class="messageText"></div>                                
                </div>
                <input type="button" class="btnClose" style="width: 100px;" onclick="hideToast()" value='<%=GetLabel("Close")%>' />
            </div>
        </div>
    </div>
    <div class="blanket">
    </div>
</div>

<div id="toastConfirmationPanel" class="toastPanel">
    <div class="tblToast">
        <div class="tblCellToast">
            <div class="toastBox">
                <div class="messageContainer">
                    <h2 class="messageHeader"><%=GetLabel("Confirmation")%></h2>
                    <div class="messageText"></div>                                
                </div>
                <input type="button" class="btnOK" style="width: 100px;" onclick="hideToastConfirmation(true)" value='<%=GetLabel("Yes")%>' />
                <input type="button" class="btnClose" style="width: 100px;" onclick="hideToastConfirmation(false)" value='<%=GetLabel("No")%>' />
            </div>
        </div>
    </div>
    <div class="blanket">
    </div>
</div>
<div id="containerSearchDialogtest">
<dxpc:ASPxPopupControl ID="pcSearchDialog" runat="server" ClientInstanceName="pcSearchDialog" EnableHierarchyRecreation="True"
    FooterText="" HeaderText="" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="800px"
    PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
    <ClientSideEvents Shown="function (s,e) { isInitSearchDialog = true; cbpSearchDialog.PerformCallback('open|' + searchDialogSearchType + '|' + searchDialogFilterExpression); }" 
        Closing="function(s,e){ txtQuickSearchDialog.SetBlur(); txtQuickSearchDialog.SetText(''); $('#' + grdSearchID).empty(); }" />
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <table style="float:right;display:none;">
                <tr>
                    <td>
                        <input type="text" id="txtSearchResult" />
                    </td>
                    <td>
                        <input type="button" value="Find Next" onclick="highlightText();" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtQuickSearchDialog" ID="txtQuickSearchDialog" Width="300px" Watermark="Search">
                            <ClientSideEvents SearchClick="function(s){ s.SetBlur(); cbpSearchDialog.PerformCallback('refresh|' + s.GenerateFilterExpression()); }" />
                        </qis:QISIntellisenseTextBox>
                    </td>
                </tr>
            </table>
            <div style="clear:both"/>
            <div id="containerSearchResult" style="height:400px;overflow-y:scroll;position:relative">
                <dxcp:ASPxCallbackPanel ID="cbpSearchDialog" runat="server" Width="100%" ClientInstanceName="cbpSearchDialog"
                    ShowLoadingPanel="false" OnCallback="cbpSearchDialog_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingSearchDialog').show(); }"
                        EndCallback="function(s,e){ onCbpSearchDialogEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlQuickMenuArea" Style="width: 100%; margin-left: auto; margin-right: auto">
                                <asp:GridView ID="grdSearch" Width="100%" runat="server" CssClass="grdView" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <EmptyDataTemplate>
                                        <%=GetLabel("No Data To Display")%>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="imgLoadingGrdView" id="containerImgLoadingSearchDialog">
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
            </div>                
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>   
</div>
<input type="hidden" id="hdnRightPanelContentIsLoadContent" name="hdnRightPanelContentIsLoadContent" value="0" />
<input type="hidden" id="hdnRightPanelContentUrl" name="hdnRightPanelContentUrl" value="" />
<input type="hidden" id="hdnRightPanelContentFirstTimeLoad" name="hdnRightPanelContentFirstTimeLoad" value="0" />
<input type="hidden" id="hdnRightPanelContentParam" name="hdnRightPanelContentParam" value="" />
<input type="hidden" id="hdnRightPanelContentCode" name="hdnRightPanelContentCode" value="" />
<input type="hidden" id="hdnRightPanelContentTitle" name="hdnRightPanelContentTitle" value="" />