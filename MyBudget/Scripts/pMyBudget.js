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

    /*  Высота таблицы  */
    var heightTbl = $(window).height() - 295;
    if ($("#tbl-w").height() > heightTbl) {    
        $('#btn-tbl-exp').show();
    }
    $('#tbl-w').height(heightTbl + 'px');

    $('#btn-tbl-exp').on("click", function () {
        var span = $(this).find("span");
        if ($("#tbl-w").hasClass("tbl-max") == true) {            
            $('#tbl-w').removeClass("tbl-max", 1000);                        
            span.removeClass("glyphicon-chevron-up");
            span.addClass("glyphicon-chevron-down");
            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
        else {
            $('#tbl-w').addClass("tbl-max", 1000);
            span.removeClass("glyphicon-chevron-down");
            span.addClass("glyphicon-chevron-up");

        }
    });
    

    $('.js-pay-goal').on("click", function () {
        $('#putOnId').val($(this).attr("data-goal-id"));
        $('#catType').val($(this).attr("data-catType"));        
    });

    $('.js-del-tr').on("click", function () {
        $('#transId').val($(this).attr("data-tr-id"));
        
    });

    //Смена статуса транзакции
    $('.js-switch').on('click', function () {
        var switch_btn = $(this);
        $.ajax({
            url: "/api/transactions/SwitchPlaned/?Id=" + switch_btn.attr("data-transaction-id"),
            method: "PUT",
            success: function () {                
                if (switch_btn.hasClass("itm-opacity") == true)
                    switch_btn.removeClass("itm-opacity");                
                else 
                    switch_btn.addClass("itm-opacity");                                                    
            }

        })
    })
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