var Methods = new (function () {
    //#region Get Object
    this.getListObject = function (methodName, filterExpression, functionHandler) {
        showLoadingPanel();
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetListObject'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "methodName" : "' + methodName + '", "filterExpression" : "' + filterExpression + '"}',
            dataType: 'json',
            error: function (msg) {
                hideLoadingPanel();
                alert(msg.responseText);
            },
            success: function (msg) {
                hideLoadingPanel();
                functionHandler(msg.d);
            }
        });     //end ajax
    };

    this.getObject = function (methodName, filterExpression, functionHandler) {
        showLoadingPanel();
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetObject'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "methodName" : "' + methodName + '", "filterExpression" : "' + filterExpression + '"}',
            dataType: 'json',
            error: function (msg) {
                hideLoadingPanel();
                alert(msg.responseText);
            },
            success: function (msg) {
                hideLoadingPanel();
                functionHandler(msg.d);
            }
        });     //end ajax
    };

    this.getValue = function (methodName, filterExpression, functionHandler) {
        showLoadingPanel();
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetValue'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "methodName" : "' + methodName + '", "filterExpression" : "' + filterExpression + '"}',
            dataType: 'json',
            error: function (msg) {
                hideLoadingPanel();
                alert(msg.responseText);
            },
            success: function (msg) {
                hideLoadingPanel();
                functionHandler(msg.d);
            }
        });     //end ajax
    };

    this.getObjectValue = function (methodName, filterExpression, returnField, functionHandler) {
        showLoadingPanel();
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetObjectValue'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "methodName" : "' + methodName + '", "filterExpression" : "' + filterExpression + '", "returnField" : "' + returnField + '"}',
            dataType: 'json',
            error: function (msg) {
                hideLoadingPanel();
                alert(msg.responseText);
            },
            success: function (msg) {
                hideLoadingPanel();
                functionHandler(msg.d);
            }
        });     //end ajax
    };

    this.getObjectValueFromSession = function (sessionName, filterBy, filterValue, returnField, functionHandler, $row) {
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetObjectValueFromSession'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "sessionName" : "' + sessionName + '", "filterBy" : "' + filterBy + '", "filterValue" : "' + filterValue + '", "returnField" : "' + returnField + '"}',
            dataType: 'json',
            error: function (msg) {
                alert(msg.responseText);
            },
            success: function (msg) {
                //alert(msg.d);
                if ($row != null)
                    window[functionHandler]($row, msg.d);
                else
                    window[functionHandler](msg.d);
            }
        });     //end ajax
    };

    this.getObjectFromSession = function (sessionName, filterBy, filterValue, functionHandler, $row) {
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetObjectFromSession'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "sessionName" : "' + sessionName + '", "filterBy" : "' + filterBy + '", "filterValue" : "' + filterValue + '"}',
            dataType: 'json',
            error: function (msg) {
                alert(msg.responseText);
            },
            success: function (msg) {
                //alert(msg.d);
                if ($row != null)
                    window[functionHandler]($row, msg.d);
                else
                    window[functionHandler](msg.d);
            }
        });     //end ajax
    };

    this.getSessionValue = function (sessionName, functionHandler) {
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetSessionValue'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "sessionName" : "' + sessionName + '"}',
            dataType: 'json',
            error: function (msg) {
                alert(msg.responseText);
            },
            success: function (msg) {
                functionHandler(msg.d);
            }
        });     //end ajax
    };
    //#endregion

    //#region Get Item Master Purchase
    this.getItemMasterPurchase = function (itemID, businessPartnerID, functionHandler) {
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetItemMasterPurchase'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "itemID" : "' + itemID + '", "businessPartnerID" : "' + businessPartnerID + '"}',
            dataType: 'json',
            error: function (msg) {
                alert(msg.responseText);
            },
            success: function (msg) {
                functionHandler(msg.d);
            }
        });     //end ajax
    };
    //#endregion

    //#region Get Item Qty On Order
    this.getItemQtyOnOrder = function (itemID, locationID, type, functionHandler) {
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetItemQtyOnOrder'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "itemID" : "' + itemID + '", "locationID" : "' + locationID + '", "type" : "' + type + '"}',
            dataType: 'json',
            error: function (msg) {
                alert(msg.responseText);
            },
            success: function (msg) {
                functionHandler(msg.d);
            }
        });     //end ajax
    };
    //#endregion

    this.getCustomObject = function (listParentValue, selectedColumnID, selectedColumnName, valueColumn, valueColumnType, filterExpression, orderByExpression, objectTypeName, functionHandler) {
        showLoadingPanel();
        $.ajax({
            // have to use synchronous here, else returns before data is fetched
            async: false,
            type: 'POST',
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetCustomObject'),
            contentType: 'application/json; charset=utf-8',
            data: '{ "listParentValue" : "' + listParentValue + '", "selectedColumnID" : "' + selectedColumnID + '", "selectedColumnName" : "' + selectedColumnName + '", "valueColumn" : "' + valueColumn + '", "valueColumnType" : "' + valueColumnType + '", "filterExpression" : "' + filterExpression + '", "orderByExpression" : "' + orderByExpression + '", "objectTypeName" : "' + objectTypeName + '"}',
            dataType: 'json',
            error: function (msg) {
                hideLoadingPanel();
                alert(msg.responseText);
            },
            success: function (msg) {
                hideLoadingPanel();
                functionHandler(msg.d);
            }
        });     //end ajax
    };

    this.getHtmlControl = function (controlLocation, queryString, functionHandler, functionOnErrorHandler) {
        showLoadingPanel();
        $.ajax({
            type: "POST",
            url: ResolveUrl('~/Libs/Service/MethodService.asmx/GetControlHtml'),
            data: "{ controlLocation:'" + controlLocation + "', queryString:'" + queryString + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                functionHandler(msg.d);
                hideLoadingPanel();
            },
            failure: function (msg) {
                alert('fail');
                alert(msg);
                functionOnErrorHandler(msg.d);
                hideLoadingPanel();
            }
        });
    };

    this.getDateFromJSON = function (jsonDate) {
        return new Date(parseInt(jsonDate.substr(6)));
    };

    this.dateToYMD = function (date) {
        var d = date.getDate();
        var m = date.getMonth() + 1;
        var y = date.getFullYear();
        return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
    }

    this.stringToDate = function (value) {
        if (value != '') {
            var YYYY = value.substring(0, 4);
            var MM = value.substring(4, 6);
            var DD = value.substring(6);
            var date = new Date(parseInt(YYYY, 10), parseInt(MM, 10) - 1, parseInt(DD, 10));
            return date;
        }
        return new Date();
    }
    this.stringToDateTime = function (value) {
        if (value != '') {
            var YYYY = value.substring(0, 4);
            var MM = value.substring(4, 6);
            var DD = value.substring(6, 8);
            var HH = value.substring(8, 10);
            var mm = value.substring(10, 12);
            var date = new Date(parseInt(YYYY, 10), parseInt(MM, 10) - 1, parseInt(DD, 10), parseInt(HH, 10), parseInt(mm, 10));
            return date;
        }
        return new Date();
    }
    this.getDatePickerDate = function (value) {
        if (value != '') {
            var DD = value.substring(0, 2);
            var MM = value.substring(3, 5);
            var YYYY = value.substring(6, 10);
            var date = new Date(parseInt(YYYY, 10), parseInt(MM, 10) - 1, parseInt(DD, 10), 0, 0);
            return date;
        }
        return new Date();
    }
    this.dateToString = function (value) {
        var dateStr = padStr(value.getFullYear()) +
                  padStr(1 + value.getMonth()) +
                  padStr(value.getDate());
        return dateStr;
    }

    function padStr(i) {
        return (i < 10) ? "0" + i : "" + i;
    }

    this.getJSONDateValue = function (jsonDate) {
        var date = new Date(parseInt(jsonDate.substr(6)));
        var dateStr = padStr(date.getDate()) + "-" +
            padStr(1 + date.getMonth()) + "-" +
            padStr(date.getFullYear());
        return dateStr;
    }

    this.ExecuteFunction = function (fn, s) {
        fn(s);
    }

    this.daysInMonth = function (month, year) {
        return new Date(year, month, 0).getDate();
    }

    this.getAgeFromDatePickerFormat = function (dob) {
        var date = Methods.getDatePickerDate(dob);
        var diffDate = Methods.calculateDateDifference(date, todayDate);
        return diffDate;
    }

    this.calculateDateDifference = function (d1, d2) {
        var years, months, days = 0;

        days = d2.getDate() - d1.getDate();
        months = d2.getMonth() - d1.getMonth();
        years = d2.getFullYear() - d1.getFullYear();
        if (d2.getMonth() < d1.getMonth()) {
            years--;
            months += 12;
        }
        if (d2.getDate() < d1.getDate()) {
            months--;
            if (d2.getMonth() > 0)
                days += new Date(d2.getFullYear(), d2.getMonth(), 0).getDate();
            else
                days += new Date(d2.getFullYear() - 1, 12, 0).getDate();
            if (months < 0) {
                years--;
                months += 12;
            }
        }
        return { "years": years, "months": months, "days": days };
    }

    this.checkImageError = function (className, type, classGender) {
        setTimeout(function () {
            if (type == 'patient') {
                var imgUrlM = ResolveUrl("~/Libs/Images/patient_male.png");
                var imgUrlF = ResolveUrl("~/Libs/Images/patient_female.png");

                $('.' + className).error(function () {
                    var gender = $(this).parent().find('.' + classGender).val();
                    if (gender == '0003^F')
                        this.src = imgUrlF;
                    else
                        this.src = imgUrlM;
                }).each(function () {
                    $(this).attr("src", $(this).attr("src"));
                });
            }
        }, 0);
    }

})();

(function ($) {
    $.fn.rptTemplate = function (idInputHdn, clickHandler) {
        var id = this[0].id;
        $('#' + idInputHdn).val('');
        $('#' + id + ' .repeaterDataItemTemplate').live('click', function () {
            $(this).parent().children().attr('class', 'repeaterDataItemTemplate notSelected');
            $(this).attr('class', 'repeaterDataItemTemplate selected');

            var idValue = $(this).find('.repeaterDataItemID').val();
            $('#' + idInputHdn).val(idValue);
            if (clickHandler != null) {
                window[clickHandler](idValue);
            }
        });
    };
})(jQuery);