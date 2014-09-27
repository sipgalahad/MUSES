<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" 
    CodeBehind="Test.aspx.cs" Inherits="CodeX.Muses.Web.ControlPanel.Test" %>

<%@ Register Assembly="CodeX.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="CodeX.Web.CustomControl" TagPrefix="qis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        function QISClientAutoCompleteHelper() {
            var _self = this;
            this.clientID = ''; 
            this.idxSelectedRow = -1;
            this.numRows = 0;
            this.timer = null;
            this.xhr;
            this.searchFields = [];
            this.methodName = '';
            this.filterExpression = '';
            this.orderByExpression = '';
            this.isHover = false;

            this.valueChanged = '';
            this.init = function (clientID, searchFields, methodName, filterExpression, orderByExpression) {
                _self.clientID = clientID;
                _self.searchFields = searchFields.split(',');
                _self.methodName = methodName;
                _self.filterExpression = filterExpression;
                _self.orderByExpression = orderByExpression;
            }
            this.setClientSideEvents = function (valueChanged) {
                _self.valueChanged = valueChanged;
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

                $("#" + _self.clientID + " .divListAutoCompleteResult").hover(function () {
                    _self.isHover = true;
                }, function () {
                    _self.isHover = false;
                });

                $("#" + _self.clientID + " .txtAutoComplete").blur(function () {
                    if ($("#" + _self.clientID + " .txtAutoComplete").val() == '') {
                        $("#" + _self.clientID + " .txtAutoComplete").val('');
                        $("#" + _self.clientID + " .hdnAutoCompleteText").val('');
                        $("#" + _self.clientID + " .hdnAutoCompleteValue").val('');
                        _self.idxSelectedRow = -1;
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
                        if (_self.idxSelectedRow > -1)
                            _self.selectRow();
                    }
                });

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
            }

            this.changeIdxSelectedRow = function(value) {
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
                    filterExpression += "(" + _self.searchFields[i] + " LIKE '%" + val + "%')";
                }
                if (_self.filterExpression != '')
                    filterExpression += ' AND ' + _self.filterExpression;
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
                if (isEnabled)
                    $("#" + _self.clientID + " .txtAutoComplete").removeAttr('readonly');
                else
                    $("#" + _self.clientID + " .txtAutoComplete").attr('readonly', 'readonly');
            }
            this.getEnabled = function () {
                if ($("#" + _self.clientID + " .txtAutoComplete").attr('readonly') != null)
                    return true;
                return false;
            }
        }

        function QISClientAutoComplete() {
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
        }
    </script>
    <style type="text/css">
        body                                                        { font-family: Segoe UI; }
        .containerAutoComplete                                      { position:relative; }
        .containerAutoComplete .divListAutoCompleteResultBox        { position:absolute; top:25px; left:0; display: none; background-color:#FFF; border:1px solid #CCCCCC; box-shadow: 0px 3px 3px #CCC; vertical-align:middle; font-size:12px; }
        .containerAutoComplete .divListAutoCompleteResult           { margin:2px 0px 0 0px; display:inline-block; }
        .containerAutoComplete .divListAutoCompleteResult div       { padding: 0 10px; }
        .containerAutoComplete .divListAutoCompleteResult div.selected,
        .containerAutoComplete .divListAutoCompleteResult div:hover { background-color: #F1F1F1; }
        .containerAutoComplete .btnAutoCompleteSearchMore           { margin-left: 3px; }
    </style>
    <qis:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacPatient" ClientInstanceName="tacPatient" MethodName="GetvPatientList" FilterExpression="IsDeleted = 0"
        SearchFields="PatientName,MedicalNo" TextField="PatientName" ValueField="MRN" SearchText="${PatientName} (<b>${MedicalNo}</b>)" OrderByExpression="PatientName" />

    <qis:CodeXAutoCompleteTextBox runat="server" Width="300px" ID="tacItem" ClientInstanceName="tacItem" MethodName="GetItemMasterList" FilterExpression="IsDeleted = 0"
        SearchFields="ItemName1,ItemCode" TextField="ItemName1" ValueField="ItemID" SearchText="${ItemName1} (<b>${ItemCode}</b>)" OrderByExpression="ItemName1" />
</asp:Content>
