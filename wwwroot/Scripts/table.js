$(document).ready(function () {
   
    var rowCount = $("#secondtable tr").length;
    $("#totalrow").val(rowCount);

    
});



jQuery(document).ready(function () {
    jQuery("#rolebtn").click(function () {
        alert("getdata");

        var roleViewModel = {};

        roleViewModel.UserName = jQuery("#user").val();
        roleViewModel.RoleName = jQuery("#role").val();

       
        jQuery.ajax({
            type: "POST",
            url: "/Account/CreateRole",


            data: JSON.stringify(roleViewModel),


            dataType: "json",
            success: function () {
                alert("sucess");
            }
        });


   });

});

 

