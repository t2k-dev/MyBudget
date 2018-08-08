$(document).ready(function () {
    //$("#DefCur").val($("#hDefCurr").val());

    $("#DefCur").on("change", function () {
        var curr = $(this).val();        

        $.ajax({
            url: "../api/manage/setdefaultcurrency/" + curr,
            method: "PUT",
            success: function () {  }
        })


    })
});
