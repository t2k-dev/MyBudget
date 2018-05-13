$(document).ready(function () {
    jQuery('#sandbox-container').datepicker({
        format: "MM yyyy",
        minViewMode: 1,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    }).change(function () {
        var month = jQuery(this).datepicker("getDate").getMonth() + 1;
        var mnthStr = (month < 10) ? '0' + month.toString() : month.toString();
        var year = jQuery(this).datepicker("getDate").getFullYear().toString();
        var dt = mnthStr + year.toString();
        window.location.href = '/Transactions/MyBudget/' + dt;
    });

    $('.js-pay-goal').on("click", function () {
        $('#putOnId').val($(this).attr("data-goal-id"));
        $('#catType').val($(this).attr("data-catType"));        
    });

    $('.js-del-tr').on("click", function () {
        $('#transId').val($(this).attr("data-tr-id"));
        
    });    
});

jQuery('#sandbox-container').datepicker({
    format: "MM yyyy",
    minViewMode: 1,
    todayBtn: "linked",
    language: "ru",
    autoclose: true,
    defaultViewDate: { year: 2018, month: 01, day: 01 }
});

function setingDate(r) {
    var dt = jQuery('#sandbox-container').datepicker('getDate');
    var dt1 = new Date();

    if (r == 1) {
        dt1 = new Date(dt.setMonth(dt.getMonth() + 1));
    } else {
        dt1 = new Date(dt.setMonth(dt.getMonth() - 1));
    }

    jQuery('#sandbox-container').datepicker('update', dt1);
};