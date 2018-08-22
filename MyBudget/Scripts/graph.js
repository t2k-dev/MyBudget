$(document).ready(function () {
    var graphTerm = $("#graphTerm");
    loadPie(graphTerm.val());
    
    graphTerm.on("change", function () {
        loadPie($(this).val());
    })


})

function loadPie(term) {    
    $.ajax({
        url: "/api/graphpie/?term=" + term,
        method: "GET",
        success: function (result) {
            $('#graphLegend tbody').remove();        

            config.data.datasets.splice(0, 1);
            var newDataset = {
                backgroundColor: [],
                data: [],
                label: 'New dataset',
            };
            config.data.labels.splice(0);            
            var DefCurrency = $("#DefCurrency").val();
            $.each(result, function (i, item) {                
                newDataset.data.push(item.Amount);
                newDataset.backgroundColor.push(item.Color);
                config.data.labels.push(item.Caption);
                
                var $tr = $('<tr>').append(
                    $('<td>').append($("<div class='leg-marker' style='background-color:" + item.Color + "'>")),
                    $('<td>').text(item.Caption),
                    $("<td class='text-right'>").text(item.Amount.toLocaleString("ru-Ru") + ' ' + DefCurrency)
                );

                $tr.appendTo('#graphLegend');
                
            })
            config.data.datasets.push(newDataset);
            window.myPie.update();
            
        }
    })

}