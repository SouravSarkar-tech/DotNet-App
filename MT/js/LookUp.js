var plantCodeGlobal = "";
var materialNoGlobal = "";

function AutoCompleteLookUpHeader() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpHeader",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data != null)
                        response($.map(data.d, function (item) {
                            return {
                                value: item,
                                result: item
                            }
                        }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });

}

function CheckLookupHeader(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpHeaderValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpHeaderC() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpCustomer",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupHeaderC(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpCustomerValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpReceipe() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpReceipe",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookUpReceipe(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpReceipeValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpVendor() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpVendor",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupVendor(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpVendorValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpBOM() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpBOM",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupBOM(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpBOMValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpResource() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpResource",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupResource(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpResourceValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpPrice() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpPrice",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupPrice(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpPriceValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteLookUpExcise() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteLookUpExcise",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","controlId": "' + textboxRealId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckLookupExcise(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckLookUpExciseValue",
        data: '{ "lookUpValue": "' + lookupValue + '","controlId": "' + controlReadId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}

function AutoCompleteBankIFSC() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteBankIFSC",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '","CountryId": "' + CountryId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}


function AutoCompleteVendorName() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../Service.svc/AutoCompleteVendorName",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}

function CheckBankValue(controlId, controlReadId, btnNextId) {
    var lookupValue = $('#' + controlId).val();
    $('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../../Service.svc/CheckBankValue",
        data: '{ "lookUpValue": "' + lookupValue + '","CountryId": "' + CountryId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                $('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                $('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}

function CheckLookupUser(controlId) {
    var lookupValue = $('#' + controlId).val();
    //$('#' + textboxId).attr('class', 'textboxbussy')
    $('#' + btnNextId).attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "../Service.svc/CheckLookUpUserName",
        data: '{ "lookUpValue": "' + lookupValue + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                //$('#' + btnNextId).removeAttr('disabled');
            }
            else {
                alert('Invalid Input.');
                $('#' + controlId).val('');
                $('#' + controlId).focus();
                //$('#' + btnNextId).removeAttr('disabled');
            }
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        },
        error: function (a) {
            alert("Error Occurred");
            $('#' + controlId).attr('class', 'textboxAutocomplete');
        }
    });
}


function AutoCompleteUserName() {
    var str = "";
    var ret = "";
    var str1 = "";
    $("#" + textboxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../Service.svc/AutoCompleteUserName",
                data: '{ "lookUpValue": "' + $.trim(request.term) + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    response($.map(data.d, function (item) {
                        return {
                            value: item,
                            result: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
        },
        focus: function (event, ui) {
            event.preventDefault();
            str = $.trim(ui.item.value)
            ret = str.split("-");
            str1 = ret[0];
            $(this).val(str1);
            return false;
        },
        minLength: 0
    }).focus(function () {
        $("#" + textboxId).trigger('keydown.autocomplete');
    });
}
