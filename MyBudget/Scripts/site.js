$(document).ready(function () {       
    $("#sandbox-container").change(function () {
        //window.location.href = '/Transactions/MyBudget/';

        var sDate = $('#sandbox-container').val();
        alert(sDate.toString('MMyyyy'));
        //var areaId = $('#ddlLocation').val();

        /*
        $.ajax({
            type: "POST",
            url: 'Home/Contact',
            //contentType: "application/json; charset=utf-8",
            //data: { date: sDate, area: areaId },
            //dataType: "json",
            success: function (result) {
                //alert('Yay! It worked!');
            },
            error: function (result) {
                //alert('Oh no :(');
            }
        });
        */
    });
    


});