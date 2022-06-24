$(function () {
    if ($("#tbl_bunit tr").length > 2) {
        $("#tbl_bunit tr:eq(1)").remove();
    } else {
        var row = $("#tbl_bunit tr:last-child");
        row.find("#edit").hide();
        row.find("#trash").hide();
        row.find("span").html('&nbsp;');
    }
});


function AppendRow(row, Businessunit, Bunit_status) {
    $("#tbl_business", row).find("span").html(Businessunit);
    $("#tbl_business", row).find("input").val(Businessunit);

    $("#tbl_status", row).find("span").html(Bunit_status);
    $("#tbl_status", row).find("input").val(Bunit_status);

    row.find("#edit").show();
    row.find("#trash").show();
    $("#tbl_bunit").append(row);
};

$("body").on("click", "#tbl_bunit #edit", function () {
    var row = $(this).closest("tr");

    $("td", row).each(function () {
        if ($(this).find("input").length > 0) {
            $(this).find("input").show();
            $(this).find("span").hide();
        }
    });

    row.find("#none").show();
    row.find("#display").show();    
    row.find("#trash").hide();
    $(this).hide();
});

//Update event handler.
$("body").on("click", "#tbl_bunit #none", function () {
    var row = $(this).closest("tr");

    alert(row);
    
    $("td", row).each(function () {
        if ($(this).find("input").length > 0) {
            var span = $(this).find("span");
            var input = $(this).find("input");
            span.html(input.val());
            span.show();
            input.hide();
        }
    });
    row.find("#edit").show();
    row.find("#trash").show();
    row.find("#display").hide();
    $(this).hide();

    var Bunit = {};

    Bunit.Businessunit = row.find("#tbl_business").find("span").html();
    Bunit.Bunit_status = row.find("#tbl_status").find("span").html();

    $.ajax({
        type: "POST",
        url: "/Admin/UpdateBunit",
        data: '{Bunit:' + JSON.stringify(Bunit) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
});