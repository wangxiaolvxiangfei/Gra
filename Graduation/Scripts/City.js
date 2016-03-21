
function GetCity() {
    $("#originList").empty();
    $.getJSON(
    "/FillBaseInfo/GetCity",
    { content: $("#originPro").val() },
    function (data) {
        $.each(data, function (i, item) {
            $("<option></option>").val(item["code"]).text(item["name"]).appendTo($("#originList"));
        })
    });
}


function GetCityFamily() {
    $("#familyList").empty();
    $.getJSON(
    "/FillBaseInfo/GetCity",
    { content: $("#familyPro").val() },
    function (data) {
        $.each(data, function (i, item) {
            $("<option></option>").val(item["code"]).text(item["name"]).appendTo($("#familyList"));
        })
    });
}