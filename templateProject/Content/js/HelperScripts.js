$.ajaxSetup({ cache: false });

//#region Variables
var ntfExpense = "#ntfExpense";

var loadingOverlay = "#loadingOverlay";

var ntfWms = "#ntfWms";

var date = new Date(), y = date.getFullYear(), m = date.getMonth();
var firstDay = new Date(y, m, 1);
var lastDay = new Date(y, m + 1, 0);
//#endregion Variables

//#region Methods
function isUsingIE() {
    if (navigator.appName == 'Microsoft Internet Explorer') {
        return true;
    }
    else {
        return false;
    }
}

function ieVersion() {
    var ie = (function () {

        var undef,
            v = 3,
            div = document.createElement('div'),
            all = div.getElementsByTagName('i');

        while (
            div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->',
            all[0]
        );

        return v > 4 ? v : undef;

    }());

    return ie;
}

function alertErrors(errors, splitterChar) {
    var msg = "Errors:\n";

    var arr = errors.split(splitterChar);
    $.each(arr, function () {
        msg += this + '\n';
    });

    alert(msg);
}

function openNewWindowWithUrl(url) {
    window.open(url, '_parent');

    //_blank - URL is loaded into a new window. This is default
    //_parent - URL is loaded into the parent frame
    //_self - URL replaces the current page
    //_top - URL replaces any framesets that may be loaded

}

function closeWindow() {
    var win = window.open('', '_self');
    window.close();
    win.close(); return false;
}

function setFocusTo(id) {
    $(id).focus();
}

function onEnterPressed(e, doSomeFunction) {
    if (e.keyCode == 13) {
        doSomeFunction();
    }
}

//#region urlRelated
function redirectTo(url) {
    window.location.href = url;
}

function urlGetHost() {
    return $(location).attr('host');//www.test.com:8082
}

function urlGetHostName() {
    return $(location).attr('hostname');//www.test.com
}

function urlGetPort() {
    return $(location).attr('port');//8082
}

function urlGetProtocol() {
    return $(location).attr('protocol');//http
}

function urlGetPathName() {
    return $(location).attr('pathname');//index.php
}

function urlGetFullUrl() {
    return $(location).attr('href');//http://www.test.com:8082/index.php#tab2
}

function urlGetHash() {
    return $(location).attr('hash');//#tab2
}

function urlGetSearch() {
    return $(location).attr('search');//?foo=123
}
//#endregion urlRelated

//#region StyleRelated
function elmAddClass(elm, classes) {
    $(elm).addClass(classes);
}

function elmRemoveClass(elm, classes) {
    $(elm).removeClass(classes);
}
//#endregion StyleRelated

function executeAsynchronously(functions, timeout) {
    for (var i = 0; i < functions.length; i++) {
        setTimeout(functions[i], timeout);
    }
}

function executeDelay(funcToCall, delayInt) {
    setTimeout(funcToCall, delayInt);
}

function toggleLoading() {
    $("#loading").toggle();
}

function hideLoadingBlock() {
    hideElement(loadingOverlay);
}

function setLoadingPosition() {
    $('.loadingBar').css({
        //position: 'absolute',
        //left: ($(window).width() - $('.loadingBar').outerWidth()) / 2,
        //top: ($(window).height() - $('.loadingBar').outerHeight()) / 2

        position: 'absolute',
        top: Math.max(0, (($(window).height()) / 2)),
        left: '45%'
        //left: Math.max(0, (($(window).width() - $('.loadingBar').outerWidth()) / 2))
    });
}

function isContainCharacter(variable, char) {
    if (variable.toLowerCase().indexOf(char.toLowerCase()) >= 0) {
        return true;
    }
    else {
        return false;
    }
}

function showNotifInfo(title, message) {
    kendoNotification_Show(ntfExpense, title, message, "info");
}

function showNotifError(title, message) {
    kendoNotification_Show(ntfExpense, title, message, "error");
}

function showNotifSuccess(title, message) {
    kendoNotification_Show(ntfExpense, title, message, "success");
}

function getCharacterLength(elm) {
    return $(elm).val().length;
}

function allowOnlyNumeric(id) {
    $(id).keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}

function showElement(elm) {
    $(elm).show();
}

function hideElement(elm) {
    $(elm).hide();
}

function dateTime_todayDate() {
    var today = new Date();
    return today;
}

function dateTime_todayDay(date) {
    if (arguments.length == 0) {
        var today = new Date();
        return today.getDay();
    }
    else if (arguments.length == 1) {
        return date.getDay();
    }
}

function dateTime_todayMonth(date) {
    if (arguments.length == 0) {
        var today = new Date();
        return today.getMonth();
    }
    else if (arguments.length == 1) {
        return date.getMonth();
    }
}

function dateTime_todayYear(date) {
    if (arguments.length == 0) {
        var today = new Date();
        return today.getFullYear();
    }
    else if (arguments.length == 1) {
        return date.getFullYear();
    }
}

function scrollTo(identifier) {//identifier bisa id, bisa class
    if ($(identifier).length) {
        $('html, body').animate({
            scrollTop: $(identifier).offset().top
        }, 500);
    }
}

function getSelectedRadioButton(radioButtonName) {
    var selectedParam = radioButtonName + ':checked';
    var selected = $(selectedParam).val();
    return selected;
}

function setSelectedRadioButton(radioButtonName, val) {
    $(radioButtonName + '[value="' + val + '"]').prop('checked', true);
}

function unselectRadioGroup(radioButtonName) {
    $(radioButtonName).prop('checked', false);
}

function toggleKendoTab(tabStripId, index, isEnable) {
    var kTabStrip = $(tabStripId).kendoTabStrip().data("kendoTabStrip");
    kTabStrip.enable(kTabStrip.tabGroup.eq(index), isEnable);
}

function countJsonObject(data) {
    var count = 0;
    for (var k in data) { // only simple cross browser way to get the first property
        var obj = data[k];
        for (var key in obj) {
            count++;
        }
        break; // no need to go further, we have counted in "AA1 1AA" 
    }

    return count;
}

function setAsCurrency(id) {
    $(id).formatCurrency({ colorize: true, region: 'id-ID', symbol: 'Rp', positiveFormat: '%s%n', negativeFormat: '-%s%n', roundToDecimalPlace: 0 });
}

function getValueFromCurrency(value) {
    var removedRp = value.replace('Rp', '');
    var clean = removedRp.replace(/,/g, '');

    return clean;
}

function setTextToElement(id, value) {
    $(id).text(value);
}

function getTextFromElement(id) {
    return $(id).text();
}

function fillTextBox(id, value) {
    $(id).val(value);
}

function GetNumTextBox(id) {
    var numerictextbox = $(id).data("kendoNumericTextBox");
    return numerictextbox.value();
}

function fillNumTextBox(id, value) {
    var numerictextbox = $(id).data("kendoNumericTextBox");
    numerictextbox.value(value);
}

function NumTextBoxDisabled(id) {
    var numerictextbox = $(id).data("kendoNumericTextBox");
    numerictextbox.enable(false);
}

function NumTextBoxEnabled(id) {
    var numerictextbox = $(id).data("kendoNumericTextBox");
    numerictextbox.enable();
}

function NumTextBoxReadOnly(id) {
    var numerictextbox = $(id).data("kendoNumericTextBox");
    numerictextbox.readonly();
}

function getInputValue(id) {
    var result = $(id).val();

    return result;
}

function enableControl(id) {
    $(id).prop('disabled', false);
}

function disableControl(id) {
    $(id).prop('disabled', true);
}

function dateJsonToJsDate(val) {
    var result = new Date(parseInt(val.substr(6)));
    return result;
}

function dateConvertion(value, format) {
    return kendo.toString(value, format);//Date.parseExact(getInputValue(value),format);
}

function dateId_to_yyyyMMdd(value) {
    var part = value.split('/');
    var tgl = part[2] + part[1] + part[0];

    return tgl;
}

function dateId_toDate(value) {
    var part = value.split('/');
    var date = new Date(part[2], part[1], part[0]);
    return date;
}

function dateId_toMM_dd_yyyy(value) {
    var part = value.split('/');
    var tgl = part[1] + '/' + part[0] + '/' + part[2];
    return tgl;
}

function splitAndGetSplitedIndex(toBeSplit, splitChar, indexToGet) {
    var part = toBeSplit.split(splitChar);
    var result = part[indexToGet];

    if (result) {
        return result;
    }
    else {
        return '';
    }
}

//#region Kendo
//#region kendoDDL
function kendoDDL_SetValue(id, val) {
    var dropdown = $(id).data('kendoDropDownList');
    dropdown.value(val);
}

function kendoDDL_GetSelectedValue(id) {
    var dropdownVal = $(id).data("kendoDropDownList").value();
    return dropdownVal;
}

function kendoDDL_GetSelectedText(id) {
    var dropdown = $(id).data("kendoDropDownList");
    return dropdown.text();
}

function kendoDDL_GetSelectedIndex(eOnChange) {
    return this.selectedIndex;
}

function kendoDDL_GetSelectedData(eOnChange) {
    var dropdown = eOnChange.sender.dataItem();
    return dropdown;
}

function kendoDDL_Reload(id) {
    $(id).data("kendoDropDownList").dataSource.read();
}

function kendoDDL_ReadOnly(id) {
    $(id).data("kendoDropDownList").readonly();
}

function kendoDDL_Enable(id) {
    $(id).data("kendoDropDownList").enable();
}

function kendoDDL_Disable(id) {
    $(id).data("kendoDropDownList").enable(false);
}

function kendoDDL_ItemCount(id) {
    var dropDown = $(id).data("kendoDropDownList");
    var len = dropDown.dataSource.data().length;

    return len;
}
//#endregion kendoDDL

function kendoMultiDDL_GetValue(id) {
    var multiselect = $(id).data("kendoMultiSelect");
    return multiselect.value();
}

function kendoMultiDDL_GetItem(id) {
    var multiselect = $(id).data("kendoMultiSelect");
    return multiselect.dataItems();
}

function kendoMultiDDL_SetValue(id, val) {
    var data = $(id).data("kendoMultiSelect");
    data.value(val);
}

function kendoMultiDDL_Reload(id) {
    $(id).data('kendoMultiSelect').dataSource.read();
}

function kendoMultiDDL_Refresh(id) {
    var multiselect = $(id).data("kendoMultiSelect");
    multiselect.refresh();
}

//#region kendoCombobox
function kendoCombobox_SetValue(id, val) {
    var dropdown = $(id).data('kendoComboBox');
    dropdown.value(val);
}

function kendoCombobox_GetSelectedValue(id) {
    var dropdownVal = $(id).data("kendoComboBox").value();
    return dropdownVal;
}

function kendoCombobox_GetSelectedText(id) {
    var dropdown = $(id).data("kendoComboBox");
    return dropdown.text();
}

function kendoCombobox_GetSelectedIndex(eOnChange) {
    return this.selectedIndex;
}

function kendoCombobox_GetSelectedData(eOnChange) {
    var dropdown = eOnChange.sender.dataItem();
    return dropdown;
}

function kendoCombobox_PreventInvalidEntry(eOnChange) {
    if (this.value() && this.selectedIndex == -1) {
        var dt = this.dataSource._data[0];
        this.text('');
        this.value('');
    }
}

function kendoCombobox_Reload(id) {
    $(id).data("kendoComboBox").dataSource.read();
}

function kendoCombobox_ReadOnly(id) {
    $(id).data("kendoComboBox").readonly();
}

function kendoCombobox_Enable(id) {
    $(id).data("kendoComboBox").enable();
}

function kendoCombobox_Disable(id) {
    $(id).data("kendoComboBox").enable(false);
}

function kendoCombobox_ItemCount(id) {
    var dropDown = $(id).data("kendoComboBox");
    var len = dropDown.dataSource.data().length;

    return len;
}
//#endregion kendoCombobox

//#region kendoGrid
function kendoGrid_hideToolbar(id) {
    var toolbar = id + " .k-grid-toolbar";
    hideElement(toolbar);
}

function kendoGrid_showToolbar(id) {
    var toolbar = id + " .k-grid-toolbar";
    showElement(toolbar);
}

function kendoGrid_Reload(id) {
    $(id).data("kendoGrid").dataSource.read();
    //$(id).data("kendoGrid").dataSource.refresh();
}

function kendoGrid_ClearData(id) {
    $(id).data("kendoGrid").dataSource.data([]);
}

function kendoGrid_GetRowCount(id, isTotalRecord) {
    var grid = $(id).data("kendoGrid");
    var dataSource = grid.dataSource;

    if (isTotalRecord == true) {
        return dataSource.total();//all total data
    }
    else {
        return dataSource.view().length;//only total of current paging
    }
}

function kendoGrid_GetSpecificRow(id, rowIndex) {
    var grid = $(id).data("kendoGrid");
    var dataSource = grid.dataSource;
    var data = dataSource.data();

    return data[rowIndex];
}

function kendoGrid_GetData(id) {
    var grid = $(id).data("kendoGrid");
    var dataSource = grid.dataSource;
    var data = dataSource.data();

    return data;
}

function kendoGrid_SetData(gridId, propertyToSet, valueToSet) {
    var selectedItem = kendoGrid_GetSelectedItem(gridId);
    var changedItem = $(gridId).data().kendoGrid.dataSource.data()[0];
    changedItem.set(propertyToSet, valueToSet);
}

function kendoGrid_GetSelectedItems(id) {
    var rows = $(id).data("kendoGrid").select();

    var result = [];
    rows.each(
            function () {
                var record = $(this).data();
                result.push(record);
            }
        )

    return result;
}

function kendoGrid_GetSelectedItem(id) {
    var result = $(id).data("kendoGrid").dataItem($(id).find("tr.k-state-selected"));

    return result;
}

function kendoGrid_GetCustomButtonRowData(id, e) {
    var dataItem = $(id).data("kendoGrid").dataItem($(e.currentTarget).closest("tr"));

    return dataItem;
}

function kendoGrid_GetTemplateButtonRowData(id, e) {
    var dataItem = $(id).data("kendoGrid").dataItem($(e).closest("tr"));

    return dataItem;
}

function kendoGrid_GetParentData(idGridParent, e) {
    var parentData = $(idGridParent).data("kendoGrid").dataItem(e.sender.element.closest("tr").prev());

    return parentData;
}

function kendoGrid_GetPagingSize(id) {
    var result = $(id).data("kendoGrid").dataSource.pageSize();

    return result;
}

function kendoGrid_ErrorHandler(args, gridId) {
    if (args.errors) {
        var grid = $(gridId).data("kendoGrid");
        //var elm = grid.editable.element.find(".errors");

        grid.one("dataBinding", function (e) {
            e.preventDefault();

            $.each(args.errors, function (propertyName) {
                //var renderedTemplate = validationTemplate({ field: propertyName, messages: this.errors });

                //elm.append(renderedTemplate);

                kendoNotification_Show(ntfWms, propertyName, this.errors, "error")
            });
        });
    }
}

function kendoGrid_ErrorHandlerAlert(e, id) {
    if (e.errors) {
        var message = "";
        var title = "";
        var gridErr = $(id).data("kendoGrid");

        var typeRequest = typeof e.sender.type !== 'undefined' ? e.sender.type : "";
        if (typeRequest == "destroy") {
            gridErr.cancelChanges();
        }

        gridErr.one('dataBinding', function (f) {
            $.each(e.errors, function (key, value) {
                title = key;
                if ('errors' in value) {
                    $.each(value.errors, function () {
                        message += this + "<br>";
                    });
                }
            });
            f.preventDefault();

            showNotification('bg-red', title, message);
        });
    }
}

function kendoGrid_ErrorHandlerDefault(e) {
    if (e.errors) {
        var message = "Errors:\n";
        var gridErr = $(grd).data("kendoGrid");

        gridErr.one('dataBinding', function (f) {
            //gridErr.cancelChanges();

            $.each(e.errors, function (key, value) {
                if ('errors' in value) {
                    $.each(value.errors, function () {
                        message += this + "\n";
                    });
                }
            });
            f.preventDefault();
            gridErr.cancelChanges();
            alert(message);
        });

    }
}

function kendoGrid_HideColumn(id, indexColumn) {
    $(id).data("kendoGrid").hideColumn(indexColumn);
}

function kendoGrid_ShowColumn(id, indexColumn) {
    $(id).data("kendoGrid").showColumn(indexColumn);
}

function kendoGrid_ReadOnly(id) {
    //var grid = $(id).data("kendoGrid");
    //var len= $(id).data("kendoGrid tbody tr").length();
    //for(i=0;i<=len ; i++)
    //{
    //    var model = grid.datasource.at(i);
    //    if(model)
    //        model.fields["cell"].editable = false;
    //}
}
//#endregion kendoGrid

//#region kendoWindow
function kendoWindow_OpenCenter(id) {
    $(id).data("kendoWindow").center().open();
}

function kendoWindow_Close(id) {
    $(id).data("kendoWindow").close();
}

function kendoWindow_ChangeTitle(id, Title) {
    $(id).data("kendoWindow").title(Title);
}
//#endregion kendoWindow

//#region kendoDatePicker
function kendoDatePicker_DisableTyping(id) {
    $(id).attr("readonly", "readonly");//disable datepicker typing
}

function kendoDatePicker_GetValue(id) {
    var result = $(id).data("kendoDatePicker").value();

    return result;
}

function kendoDatePicker_SetValue(id, dateValue) {
    $(id).data("kendoDatePicker").value(dateValue);
}

function kendoDatePicker_SetMinValue(id, minValue) {
    $(id).data("kendoDatePicker").min(minValue);
}

function kendoDatePicker_SetMaxValue(id, maxValue) {
    $(id).data("kendoDatePicker").max(maxValue);
}

function kendoDatePicker_GetConvertedDate_yyyyMMdd(value) {
    var result = kendo.toString(value, "yyyyMMdd");

    return result;
}

function kendoDatePicker_Disabled(id) {
    var datepicker = $(id).data("kendoDatePicker");
    datepicker.enable(false);
}

function kendoDatePicker_Enable(id) {
    var datepicker = $(id).data("kendoDatePicker");
    datepicker.enable(true);
}
//#endregion kendoDatePicker

//#region kendoNotification
function kendoNotification_Show(id, notifTitle, notifMessage, notifTemplate) {
    var notification = $(id).data("kendoNotification");
    notification.show({
        title: notifTitle,
        message: notifMessage
    }, notifTemplate);
}

function kendoNotification_Hide(id) {
    var notification = $(id).data("kendoNotification");
    notification.hide();
}

function KendoErrorHandler(args) {
    if (args.errors) {
        var grid = $(grdProductShared).data("kendoGrid");
        var validationTemplate = kendo.template($("#ValidationMessageTemplate").html());
        var elm = grid.editable.element.find(".errors");

        grid.one("dataBinding", function (e) {
            e.preventDefault();

            $.each(args.errors, function (propertyName) {
                var renderedTemplate = validationTemplate({ field: propertyName, messages: this.errors });

                elm.append(renderedTemplate);
            });
        });
    }
}
//#endregion kendoNotification
var grd = "#grd";
function wmsErrorHandler(args) {
    kendoNotification_Hide(ntfWms);
    kendoGrid_ErrorHandler(args, grd);
    kendoGrid_Reload(grd);
}

function PSErrorHandler(args) {
    kendoNotification_Hide(ntfExpense);

    kendoGrid_ErrorHandler(args, grdProductShared);
}

function OPSErrorHandler(args) {
    kendoNotification_Hide(ntfExpense);

    kendoGrid_ErrorHandler(args, grdOutletProductShared);
}

function ItemErrorHandler(args) {
    //kendoNotification_Hide(ntfExpense);

    kendoGrid_ErrorHandler(args, grdItem);
}

//#endregion Kendo

//#endregion Methods




function showNotification(colorName, title, text, icon) {
    var content = {};

    content.message = text;
    content.title = title;
    if (!typeof icon === "undefined") {
        content.icon = icon;
    } else {
        content.icon = 'fas fa-info-circle';
    }

    $.notify(content, {
        type: colorName,
        placement: {
            from: "top",
            align: "right"
        },
        time: 2000,
        allow_dismiss: true,
        newest_on_top: true,
        z_index: 10011
    });
}

function AjaxForm_showNotification(Errors) {
    if (Errors) {
        var message = "";
        var title = 'Failed';

        $.each(Errors, function (key, value) {
            message += value + "<br>";
        });

        showNotification('danger', title, message);
    }
}

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
}

function SplitByUpperCase(input) {
    var result = input.match(/[A-Z][a-z]+|[0-9]+/g).join(" ")
    return result;
}

function elementAuth(type, el) {
    var allow = "True";
    switch (type) {
        case "create":
            allow = $("input[name='AuthAllowCreate']").val();
            break;
        case "read":
            allow = $("input[name='AuthAllowRead']").val();
            break;
        case "update":
            allow = $("input[name='AuthAllowUpdate']").val();
            break;
        case "delete":
            allow = $("input[name='AuthAllowDelete']").val();
            break;
        default:
            allow = "False";
            break;
    }

    if (allow == "False") {
        $(el).remove();
    }
}



function AjaxForm_ErrorHandlerAlertWithFocus(Errors) {
    if (Errors) {
        var message = "";
        var title = "";


        $.each(Errors, function (key, value) {
            var splitTitle = key.split("|");
            title = splitTitle[0];
            if (splitTitle[1]) {
                if (document.getElementById(splitTitle[1])) {
                    $('#' + splitTitle[1]).position().top;
                }
            }
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "<br>";
                });
            }
        });

        showNotification('bg-red', title, message);
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}