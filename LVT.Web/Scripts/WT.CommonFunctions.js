var WT = WT || {};

WT.CommonFunctions = {
    responseStatus: {
        SUCCESS: "SUCCESS",
        ERROR: "ERROR",
        SESSIONEXPIRED: "SESSIONEXPIRED"
    },
    GoToLogin: function () {
        window.location.href = WT.Common.rootURL + 'Default.aspx';
    },
    FillDropdown: function (url, method, byId, dropdownList, selectedValue, isSearch) {
        $.blockUI();
        $.post(url,
            {
                method: method,
                Id: byId
            },
            function (response) {
                switch (response.Status) {
                    case WT.CommonFunctions.responseStatus.SUCCESS:
                        $.unblockUI();
                        var dataList = response.Records;
                        $(dropdownList).empty();
                        var options = '';
                        if (isSearch) {
                            options = '<option value="0">All</option>';
                        }
                        else {
                            options = '<option value="">---Select---</option>';
                        }
                        if (dataList.length > 0) {
                            for (var i = 0; i < dataList.length; i++) {
                                var data = dataList[i];
                                options += '<option value="' + data.Id + '">' + data.Name + '</option>';
                            }
                        }
                        $(dropdownList).append(options);
                        if (selectedValue != "") {
                            $(dropdownList).val(selectedValue);
                        }
                        WT.CommonFunctions.ChosenListUpdated(dropdownList);
                        break;
                    case WT.CommonFunctions.responseStatus.ERROR:
                        $.unblockUI();
                        WT.CommonFunctions.ErrorMessage("Fill Drop Down", response.Message);
                        break;
                    case WT.CommonFunctions.responseStatus.SESSIONEXPIRED:
                        $.unblockUI();
                        WT.CommonFunctions.GoToLogin();
                    default:
                        break;
                }
            });
    },
    DropdownEmpty: function (dropdownList) {
        $(dropdownList).empty();
        $(dropdownList).trigger("liszt:updated");
    },
    SetDropdown: function (dropdownList, dataList, isSearch) {
        WT.CommonFunctions.DropdownEmpty(dropdownList)
        options = '';
        if (dataList.length > 0) {
            if (isSearch)
            {
                options += '<option value="0">All</option>';
            }
            else
            {
                options += '<option value="">---Select---</option>';
            }
            for (var i = 0; i < dataList.length; i++) {
                var data = dataList[i];
                options += '<option value="' + data.Id + '">' + data.Name + '</option>';
            }
            $(dropdownList).append(options);
            WT.CommonFunctions.ChosenListUpdated(dropdownList);
        }
    },
    notifyMessage: function (message) {
        alert(message);
    },
    SuccessMessage: function (title, message) {
        $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
            _title: function (title) {
                var $title = this.options.title || '&nbsp;'
                if (("title_html" in this.options) && this.options.title_html == true)
                    title.html($title);
                else title.text($title);
            }
        }));

        $("#dialog-message").html("");
        $("#dialog-message").append("<p>" + message + "</p>");
        var dialog = $("#dialog-message").removeClass('hide').dialog({
            modal: true,
            title:
            "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-check'></i> " + title + "</h4></div>",
            title_html: true,
            buttons: [
                {
                    text: "OK",
                    "class": "btn btn-primary btn-minier",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    },
    ConfirmMessage: function (title, message, deleteButtonText, callBackfunction) {
        $("#dialog-confirm").html("");
        $("#dialog-confirm").append("<div class='space-6'></div>" +
            "<p class='bigger-110 bolder center grey'>" +
            "<i class='ace-icon fa fa-hand-o-right blue bigger-120'></i>" + message + "</p>");
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            width: '320',
            modal: true,
            title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i>" + title + "</h4></div>",
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; '" + deleteButtonText + "'",
                    "class": "btn btn-danger btn-minier",
                    click: function () {
                        var callBack = callBackfunction;
                        $(this).dialog("close");
                    }
                },
                {
                    html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancel",
                    "class": "btn btn-minier",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    },
    ErrorMessage: function (title, message) {
        $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
            _title: function (title) {
                var $title = this.options.title || '&nbsp;'
                if (("title_html" in this.options) && this.options.title_html == true)
                    title.html($title);
                else title.text($title);
            }
        }));

        $("#dialog-message").html("");
        $("#dialog-message").append("<p>" + message + "</p>");
        var dialog = $("#dialog-message").removeClass('hide').dialog({
            modal: true,
            title:
            "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i> " + title + "</h4></div>",
            title_html: true,
            buttons: [
                {
                    text: "OK",
                    "class": "btn btn-primary btn-minier",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    },
    GritterSuccessMsg: function (title, message) {
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: title,
            // (string | mandatory) the text inside the notification
            text: message,
            class_name: 'gritter-success'
        });
    },
    GritterErrorMsg: function (title, message) {
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: title,
            // (string | mandatory) the text inside the notification
            text: message,
            class_name: 'gritter-error'
        });
    },
    NumericValuesOnly: function (txtControlId) {
        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode == 46)) {

            if (String(document.getElementById(txtControlId).value).indexOf(".", 1) > 0 && event.keyCode == 46) {
                event.returnValue = false;
            } else {
                event.returnValue = true;
            }
        } else {
            event.returnValue = false;
        }
    },
    Set_Numeric: function (txtControlId, decimalPoints) {
        var strAmount;
        strAmount = document.getElementById(txtControlId).value;
        strAmount = String(strAmount).replace(/,/gi, '');
        if (strAmount == "") {
            strAmount = "0";
        } else if (WT.CommonFunctions.FormatNumber(Number(strAmount).toFixed(Number(decimalPoints))) === "NaN") {
            strAmount = "0";
        }
        document.getElementById(txtControlId).value = WT.CommonFunctions.FormatNumber(Number(strAmount).toFixed(Number(decimalPoints)));
    },
    OnPaste: function (value) {
        return value;
    },
    GetTodayDate: function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        today = mm + '/' + dd + '/' + yyyy;
        return today;
    },
    InitDatePickers: function (inputName) {
        var myDate = new Date("Jan 01, 2010");
        $('#' + inputName).datepicker("destroy");
        $('#' + inputName).datepicker({
            dateFormat: 'mm-dd-yy',
            changeMonth: true,
            changeYear: true,
            //yearRange: '-0:+10',
            defaultDate: myDate
        });
        $('#' + inputName).datepicker("refresh");
        WT.CommonFunctions.SetDate(inputName);
    },
    InitDatePickersStartEnd: function (inputName) {
        var myDate = new Date("Jan 01, 2010");
        $('#' + inputName).datepicker("destroy");
        $('#' + inputName).datepicker({
            dateFormat: 'dd-M-yy',
            changeMonth: true,
            changeYear: true,
            yearRange: '-2:+10',
            defaultDate: myDate
        });
        $('#' + inputName).datepicker("refresh");
        WT.CommonFunctions.SetDate(inputName);
    },
    SetDate: function (inputName) {
        $('#' + inputName).datepicker("refresh");
        $('#' + inputName).datepicker("setDate", new Date());
    },
    GetParameterByName: function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    GetDateFromJSON: function (jsonDate) {
        var dateString = (jsonDate).substr(6);
        var date = new Date(parseInt(dateString));
        //date = (date.getDate().padLeft(2) + '/' + (date.getMonth() + 1).padLeft(2) + '/' + date.getFullYear());
        date = (date.getMonth() + 1).padLeft(2) + '-' + (date.getDate().padLeft(2) + '-' + date.getFullYear());
        return date;
    },
    IsValidEmailAddress: function (emailAddress) {
        var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(emailAddress);
    },
    IsValidDate: function (inputDate) {
        var pattern = /^(0[1-9]|[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$/;
        var result = pattern.test(inputDate);
        return pattern.test(inputDate);
    },
    InputIntValuesOnly: function (evt, elementId) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(evt.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (evt.keyCode == 65 && (evt.ctrlKey === true || evt.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (evt.keyCode >= 35 && evt.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((evt.shiftKey || (evt.keyCode < 48 || evt.keyCode > 57)) && (evt.keyCode < 96 || evt.keyCode > 105)) {
            evt.preventDefault();
        }
    },
    InputFloatValuesOnly: function (evt, elementId) {
        //getting key code of pressed key
        var keycode = (evt.which) ? evt.which : evt.keyCode;
        //comparing pressed keycodes
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            evt.preventDefault();
        }
        else {
            var parts = $('#' + elementId).val().split('.');
            if (parts.length > 1 && keycode == 46)
                evt.preventDefault();
            return;
        }
    },
    FormatFloatValuesOnly: function (elementId, decimalPoints) {
        var strValue;
        strValue = $('#' + elementId).val();
        strValue = String(strValue).replace(/,/gi, '');
        if (strValue == "") {
            strValue = "0";
        } else if (WT.CommonFunctions.FormatNumber(Number(strValue).toFixed(Number(decimalPoints))) === "NaN") {
            strValue = "0";
        }
        $('#' + elementId).val(WT.CommonFunctions.FormatNumber(Number(strValue).toFixed(Number(decimalPoints))));
    },
    FormatIntValuesOnly: function (elementId) {
        var strValue;
        strValue = $('#' + elementId).val();
        strValue = String(strValue).replace(/,/gi, '');
        if (strValue == "") {
            strValue = "0";
        }
        $('#' + elementId).val(WT.CommonFunctions.FormatNumber(Number(strValue)));
    },
    FormatNumber: function (nStr) {
        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    },
    IsValidateImage: function (fileupload) {
        if (!/(\.(gif|jpg|jpeg|bmp|png))$/i.test(fileupload.val().toLowerCase())) {
            return false;
        }
        return true;
    },
    IsValidateImageName: function (fileName) {
        if (!/(\.(gif|jpg|jpeg|bmp|png))$/i.test(fileName.toLowerCase())) {
            return false;
        }
        return true;
    },
    ValidateLength: function (sender, args) {
        var day = $('#ddlDay').val();
        if (day != 'Day' || $('#ddlMonth').val() != 'Month' || $('#ddlYear').val() != 'Year') {
            if (day != 'Day' && $('#ddlMonth').val() != 'Month' && $('#ddlYear').val() != 'Year') {
                var dDate = $('#ddlDay').val();
                var dMonth = $('#ddlMonth').val();
                var dYear = $('#ddlYear').val();
                var dateofBirth = dMonth + '/' + dDate + '/' + dYear;
                if (!isDate(dateofBirth)) {
                    return args.IsValid = false;
                } else {
                    return args.IsValid = true;
                }
            }
            else {
                var dobvalid = true;
                switch ($(sender).attr('id')) {
                    case "CustomDay":
                        dobvalid = (day == "Day") ? false : true;
                        break;
                    case "CustomMonth":
                        dobvalid = ($('#ddlMonth').val() == 'Month') ? false : true;
                        break;
                    case "CustomYear":
                        dobvalid = ($('#ddlYear').val() == 'Year') ? false : true;
                        break;
                }
                return args.IsValid = dobvalid;
            }
        }
        else {
            return args.IsValid = true;
        }
    },
    IsDate: function (txtDate) {
        var currVal = txtDate;
        if (currVal == '')
            return false;

        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?

        if (dtArray == null)
            return false;

        //Checks for mm/dd/yyyy format.
        var dtMonth = dtArray[1];
        var dtDay = dtArray[3];
        var dtYear = dtArray[5];

        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay > 31)
            return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
            return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap))
                return false;
        }
        return true;
    },
    ResponseMessage: function (response, msg, alertdiv, sucessdiv, failurediv) {
        $('#' + sucessdiv).html('');
        $('#' + failurediv).html('');
        if (response == 'Success') {
            $('#' + sucessdiv).html(msg);
            $('#' + sucessdiv).show();
            $('#' + failurediv).hide();
            $('#' + alertdiv).fadeIn(1000);
            window.setTimeout(function () {
                $('#' + alertdiv).fadeOut(1000);
            }, 10000);
        } else {
            $('#' + sucessdiv).hide();
            $('#' + failurediv).html(msg);
            $('#' + failurediv).show();
            $('#' + alertdiv).fadeIn(1000);
            window.setTimeout(function () {
                $('#' + alertdiv).fadeOut(1000);
            }, 10000);
        }
    },
    Responsefun: function (response, msg) {
        $('#success').html('');
        $('#failure').html('');
        if (response == 'Success') {
            $('#success').html(msg);
            $('#success').show();
            $('#alertdiv').fadeIn(1000);
            window.setTimeout(function () {
                $('#alertdiv').fadeOut(1000);
            }, 10000);
            $('#failure').hide();
        } else {
            $('#success').hide();
            $('#failure').html(msg);
            $('#failure').show();
            $('#alertdiv').fadeIn(1000);
            window.setTimeout(function () {
                $('#alertdiv').fadeOut(1000);
            }, 10000);
        }
    },
    ResponsefunTime: function (response, msg, time) {
        $('#success').html('');
        $('#failure').html('');
        if (response == 'Success') {
            $('#success').html(msg);
            $('#success').show();
            $('#alertdiv').fadeIn(1000);
            window.setTimeout(function () {
                $('#alertdiv').fadeOut(1000);
            }, time);
            $('#failure').hide();
        } else {
            $('#success').hide();
            $('#failure').html(msg);
            $('#failure').show();
            $('#alertdiv').fadeIn(1000);
            window.setTimeout(function () {
                $('#alertdiv').fadeOut(1000);
            }, time);
        }
    },
    // Validates that the input string is a valid date formatted as "mm/dd/yyyy"
    IsValidDate_MMDDYYYY: function (dateString) {
        // First check for the pattern
        if (!/^(\d{1,2})[-\/](\d{1,2})[-\/](\d{4})$/.test(dateString))
            return false;

        // Parse the date parts to integers
        var parts = dateString.split("-");
        var day = parseInt(parts[1], 10);
        var month = parseInt(parts[0], 10);
        var year = parseInt(parts[2], 10);

        // Check the ranges of month and year
        if (year < 1000 || year > 3000 || month == 0 || month > 12)
            return false;

        var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

        // Adjust for leap years
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            monthLength[1] = 29;

        // Check the range of the day
        return day > 0 && day <= monthLength[month - 1];
    },
    GetPageName: function () {
        var index = window.location.href.lastIndexOf("/") + 1,
            filenameWithExtension = window.location.href.substr(index),
            filename = filenameWithExtension.split(".")[0];
        return filename;
    },
    IsImageFound: function (imgSrc) {
        var isImagefound = true;
        var imageTest = $("<img>");
        imageTest.attr('src', imgSrc).load(function () {
            isImagefound = true;
        }).error(function () {
            isImagefound = false;
        });
        return isImagefound;
    },
    ConvertStringifyToJson: function (jsonStringify) {
        var jsonObject = JSON.parse(jsonStringify);
        return jsonObject;
    },
    ChosenListUpdated: function (ddlSrch) {
        $(ddlSrch).trigger("liszt:updated");
    },
    ReplaceStatus: function (status)
    {
        var replaced = status;
        if (status == "Pending for Recommendation")
        {
            replaced = status.replace("Pending", '<b><u>Pending</u></b>');
        }
        else if (status == "Recommended (Pending for Approval)") {
            replaced = status.replace("Recommended", '<b><u>Recommended</u></b>');
        }
        else if (status == "Rejected by Recommeded Person" || status == "Rejected by Approval Person" || status == "Rejected by Issuing Person") {
            replaced = status.replace("Rejected", '<b><u>Rejected</u></b>');
        }
        else if (status == "Approved (Pending for Issuing)") {
            replaced = status.replace("Approved", '<b><u>Approved</u></b>');
        }
        else if (status == "Issued by Issuing Person") {
            replaced = status.replace("Issued", '<b><u>Issued</u></b>');
        }
        return replaced;
    }
};