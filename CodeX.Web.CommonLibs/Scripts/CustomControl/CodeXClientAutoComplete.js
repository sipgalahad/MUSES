function CodeXClientAutoCompleteHelper() {
    var _self = this;
    this.clientID = '';
    this.idxSelectedRow = -1;
    this.numRows = 0;
    this.timer = null;
    this.xhr;
    this.searchFields = [];
    this.methodName = '';
    this.filterExpression = '';
    this.filterType = '';
    this.getFilterExpressionFunction = '';
    this.orderByExpression = '';
    this.isHover = false;

    this.onValueChanged = '';
    this.onBtnSearchClick = '';
    this.init = function (clientID, searchFields, methodName, filterExpression, getFilterExpressionFunction, orderByExpression, filterType) {
        _self.clientID = clientID;
        _self.searchFields = searchFields.split(',');
        _self.methodName = methodName;
        _self.filterExpression = filterExpression;
        _self.getFilterExpressionFunction = getFilterExpressionFunction;
        _self.orderByExpression = orderByExpression;
        _self.filterType = filterType;
    }
    this.setClientSideEvents = function (onValueChanged, onBtnSearchClick) {
        _self.onValueChanged = onValueChanged;
        _self.onBtnSearchClick = onBtnSearchClick;
    }

    this.initializeControl = function () {
        $("#" + _self.clientID + " .txtAutoComplete").keyup(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 9 || (code === 9 && e.shiftKey)) {
            }
            else {
                if (code == 40) { //down
                }
                else if (code == 38) { //up
                }
                else if (code == 13) { //enter
                }
                else if (code == 37 || code == 39) { // left right
                }
                else {
                    if (_self.xhr && _self.xhr.readystate != 4) {
                        _self.xhr.abort();
                    }
                    if (_self.timer)
                        clearTimeout(_self.timer);
                    if ($(this).val() == '' || $(this).attr('readonly') != null) {
                        $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
                        $("#" + _self.clientID + " .divListAutoCompleteResultBox").hide();
                    }
                    else
                        _self.timer = setTimeout(_self.getAutoComplete, 300);
                }
            }
        });

        $("#" + _self.clientID + " .btnAutoCompleteSearchMore").click(function () {
            if ($(this).attr('enabled') == null) {
                setTimeout(function () {
                    if (typeof _self.onBtnSearchClick == 'function')
                        _self.onBtnSearchClick();
                }, 0);
            }
        });

        $("#" + _self.clientID + " .divListAutoCompleteResult").hover(function () {
            _self.isHover = true;
        }, function () {
            _self.isHover = false;
        });

        $("#" + _self.clientID + " .txtAutoComplete").focus(function () {
            //if ($(this).attr('readonly') == null)
            //    _self.timer = setTimeout(_self.getAutoComplete, 500);
        });

        $("#" + _self.clientID + " .txtAutoComplete").blur(function () {
            if ($(this).attr('readonly') == null) {
                if ($("#" + _self.clientID + " .txtAutoComplete").val() == '') {
                    $("#" + _self.clientID + " .txtAutoComplete").val('');
                    $("#" + _self.clientID + " .hdnAutoCompleteText").val('');
                    $("#" + _self.clientID + " .hdnAutoCompleteValue").val('');
                    _self.idxSelectedRow = -1;
                    setTimeout(function () {
                        if (typeof _self.onValueChanged == 'function')
                            _self.onValueChanged($(this));
                    }, 0);
                    setTimeout(function () {
                        $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
                        $("#" + _self.clientID + " .divListAutoCompleteResultBox").hide();
                    }, 100);
                }
                else if (!_self.isHover) {
                    if (_self.xhr && _self.xhr.readystate != 4) {
                        _self.xhr.abort();
                    }
                    if (_self.timer)
                        clearTimeout(_self.timer);
                    _self.idxSelectedRow = -1;
                    $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
                    $("#" + _self.clientID + " .divListAutoCompleteResultBox").hide();
                    $("#" + _self.clientID + " .txtAutoComplete").val($("#" + _self.clientID + " .hdnAutoCompleteText").val());
                }
                //else {
                //    $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
                //    $("#" + _self.clientID + " .divListAutoCompleteResultBox").hide();
                //}
            }
        });

        $("#" + _self.clientID + " .txtAutoComplete").keydown(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 40) { //down
                if (_self.idxSelectedRow < _self.numRows - 1)
                    _self.changeIdxSelectedRow(1);
            }
            else if (code == 38) { //up
                if (_self.idxSelectedRow > 0)
                    _self.changeIdxSelectedRow(-1);
            }
            else if (code == 13 || code == 9) { //enter // tab
                if (_self.idxSelectedRow > -1) {
                    _self.selectRow();
                    if (code == 13)
                        $(this).blur();
                }
                else {
                    var val = $("#" + _self.clientID + " .txtAutoComplete").val();
                    if (val != '') {
                        var filterExpression = '';
                        for (var i = 0; i < _self.searchFields.length; ++i) {
                            if (filterExpression != '')
                                filterExpression += ' OR ';
                            if (_self.filterType == '0')
                                filterExpression += "(" + _self.searchFields[i] + " LIKE '" + val + "%')";
                            else
                                filterExpression += "(" + _self.searchFields[i] + " LIKE '%" + val + "%')";
                        }

                        var tempFilterExpression = '';
                        if (_self.getFilterExpressionFunction != '') {
                            var fn = window[_self.getFilterExpressionFunction];
                            if (typeof fn === 'function') {
                                tempFilterExpression = fn();
                            }
                            else if (_self.filterExpression != '')
                                tempFilterExpression = _self.filterExpression;
                        }
                        else
                            tempFilterExpression = _self.filterExpression;

                        var filterExpression1 = tempFilterExpression;
                        if (tempFilterExpression.slice(-1) != ';')
                            filterExpression1 += ' AND ';
                        filterExpression = filterExpression1 + '(' + filterExpression + ')';
                        setTimeout(function () {
                            _self.getListObject(_self.methodName, filterExpression, _self.orderByExpression, function (result) {
                                _self.numRows = result.length;
                                $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
                                $("#" + _self.clientID + " .tmpltAutoComplete").tmpl(result).appendTo("#" + _self.clientID + " .divListAutoCompleteResult");
                                $("#" + _self.clientID + " .divListAutoCompleteResultBox").show();
                                if (result.length > 0) {
                                    _self.idxSelectedRow = 0;
                                    $("#" + _self.clientID + " .divListAutoCompleteResult div:eq(0)").addClass('selected');
                                }
                                else
                                    _self.idxSelectedRow = -1;
                                _self.selectRow();
                                if (code == 13)
                                    $(this).blur();
                            });
                        }, 0);
                    }
                }
            }
        });

        $("#" + _self.clientID + " .divListAutoCompleteResult div").die('click');
        $("#" + _self.clientID + " .divListAutoCompleteResult div").live('click', function () {
            $("#" + _self.clientID + " .divListAutoCompleteResult div.selected").removeClass('selected');
            $(this).addClass('selected');
            _self.selectRow();
        });
    }
    this.selectRow = function () {
        $div = $("#" + _self.clientID + " .divListAutoCompleteResult div.selected");
        var text = $div.find('.hdnAutoCompleteRowText').val();
        $("#" + _self.clientID + " .hdnAutoCompleteText").val(text);
        $("#" + _self.clientID + " .txtAutoComplete").val(text);
        $("#" + _self.clientID + " .hdnAutoCompleteValue").val($div.find('.hdnAutoCompleteRowValue').val());
        $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
        $("#" + _self.clientID + " .divListAutoCompleteResultBox").hide();
        _self.idxSelectedRow = -1;
        setTimeout(function () {
            if (typeof _self.onValueChanged == 'function')
                _self.onValueChanged($("#" + _self.clientID + " .txtAutoComplete"));
        }, 0);
    }

    this.changeIdxSelectedRow = function (value) {
        _self.idxSelectedRow += value;
        $("#" + _self.clientID + " .divListAutoCompleteResult div.selected").removeClass('selected');
        $("#" + _self.clientID + " .divListAutoCompleteResult div:eq(" + _self.idxSelectedRow + ")").addClass('selected');
    }
    this.getAutoComplete = function () {
        //var re = new RegExp('{val}', 'g');
        //var filterExpression = _self.filterExpression.replace(re, $("#" + _self.clientID + " .txtAutoComplete").val());
        var val = $("#" + _self.clientID + " .txtAutoComplete").val();
        var filterExpression = '';
        for (var i = 0; i < _self.searchFields.length; ++i) {
            if (filterExpression != '')
                filterExpression += ' OR ';
            if (_self.filterType == '0')
                filterExpression += "(" + _self.searchFields[i] + " LIKE '" + val + "%')";
            else
                filterExpression += "(" + _self.searchFields[i] + " LIKE '%" + val + "%')";
        }

        var tempFilterExpression = '';
        if (_self.getFilterExpressionFunction != '') {
            var fn = window[_self.getFilterExpressionFunction];
            if (typeof fn === 'function') {
                tempFilterExpression = fn();
            }
            else if (_self.filterExpression != '')
                tempFilterExpression = _self.filterExpression;
        }
        else
            tempFilterExpression = _self.filterExpression;

        var filterExpression1 = tempFilterExpression;
        if (tempFilterExpression.slice(-1) != ';')
            filterExpression1 += ' AND ';
        filterExpression = filterExpression1 + '(' + filterExpression + ')'
        _self.getListObject(_self.methodName, filterExpression, _self.orderByExpression, function (result) {
            _self.numRows = result.length;
            $("#" + _self.clientID + " .divListAutoCompleteResult").empty();
            $("#" + _self.clientID + " .tmpltAutoComplete").tmpl(result).appendTo("#" + _self.clientID + " .divListAutoCompleteResult");
            $("#" + _self.clientID + " .divListAutoCompleteResultBox").show();
            if (result.length > 0) {
                _self.idxSelectedRow = 0;
                $("#" + _self.clientID + " .divListAutoCompleteResult div:eq(0)").addClass('selected');
            }
            else
                _self.idxSelectedRow = -1;
        });
    }


    this.getListObject = function (methodName, filterExpression, orderByExpression, functionHandler) {
        _self.xhr = $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: true,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetLimitListObject2'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "methodName" : "' + methodName + '", "filterExpression" : "' + filterExpression + '", "pageCount" : "5", "orderByExpression" : "' + orderByExpression + '"}',
            dataType: 'json',
            error: function (msg) {
                //alert(filterExpression);
            },
            success: function (msg) {
                functionHandler(msg.d);
            }
        });
    }

    this.setText = function (value) {
        $("#" + _self.clientID + " .hdnAutoCompleteText").val(value);
        $("#" + _self.clientID + " .txtAutoComplete").val(value);
    }
    this.setValue = function (value) {
        $("#" + _self.clientID + " .hdnAutoCompleteValue").val(value);
    }
    this.getText = function () {
        return $("#" + _self.clientID + " .txtAutoComplete").val();
    }
    this.getValue = function () {
        return $("#" + _self.clientID + " .hdnAutoCompleteValue").val();
    }
    this.setEnabled = function (isEnabled) {
        if (isEnabled) {
            $("#" + _self.clientID + " .txtAutoComplete").removeAttr('readonly');
            $("#" + _self.clientID + " .btnAutoCompleteSearchMore").removeAttr('enabled');
        }
        else {
            $("#" + _self.clientID + " .txtAutoComplete").attr('readonly', 'readonly');
            $("#" + _self.clientID + " .btnAutoCompleteSearchMore").attr('enabled', false);
        }
    }
    this.getEnabled = function () {
        if ($("#" + _self.clientID + " .txtAutoComplete").attr('readonly') != null)
            return true;
        return false;
    }
    this.setFocus = function () {
        $("#" + _self.clientID + " .txtAutoComplete").focus();
    }
    this.setFilterExpression = function (value) {
        _self.filterExpression = value;
    }
}

function CodeXClientAutoComplete() {
    var _self = this;
    this.autoCompleteHelper;
    this.init = function (autoCompleteHelper) {
        _self.autoCompleteHelper = autoCompleteHelper;
    }
    this.setValue = function (value) {
        _self.autoCompleteHelper.setValue(value);
    }
    this.setText = function (value) {
        _self.autoCompleteHelper.setText(value);
    }
    this.getValue = function (value) {
        return _self.autoCompleteHelper.getValue(value);
    }
    this.getText = function (value) {
        return _self.autoCompleteHelper.getText(value);
    }
    this.setEnabled = function (isEnabled) {
        _self.autoCompleteHelper.setEnabled(isEnabled);
    }
    this.getEnabled = function () {
        return _self.autoCompleteHelper.getEnabled();
    }
    this.setFocus = function () {
        return _self.autoCompleteHelper.setFocus();
    }
    this.setFilterExpression = function (value) {
        _self.setFilterExpression(value);
    }
}