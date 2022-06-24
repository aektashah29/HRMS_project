jQuery(document).ready(function () {

    jQuery("#dept").on("change", function () {
       
        jQuery("#sdept").empty();
        var id = jQuery(this).val();
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetSDepts",
            dataType: "json",
            data: { DeptID: id },
            success: function (sdepts) {

                jQuery("#sdept").append("<option value=" + "Select SubDepartment" + ">" + "Select SubDepartment" + "</option>");
                jQuery.each(sdepts, function (key, SubDepartment) {
                    jQuery("#sdept").append("<option value=" + SubDepartment.value + ">" + SubDepartment.text + "</option>");
                });

            }

        });



    });

});
jQuery(function () {
    jQuery('.datepicker').datepicker();
});
jQuery(document).ready(function () {
    jQuery("#Buttton").click(function () {

        var RequestModel = {};

        RequestModel.Firstname = jQuery("#Fname").val();

        RequestModel.Lastname = jQuery("#Lname").val();
        RequestModel.Email = jQuery("#Emaildb").val();
        RequestModel.Mobile = jQuery("#Mobile").val();
        RequestModel.Doj = jQuery("#Doj").val();
        RequestModel.BUnitID = jQuery("#bunit").val();
        jQuery("#Bnid").val(RequestModel.BUnitID);

        RequestModel.DeptID = jQuery("#dept").val();
        jQuery("#DEid").val(RequestModel.DeptID );

        RequestModel.SDeptID = jQuery("#sdept").val();
        jQuery("#SDid").val(RequestModel.SDeptID);

        RequestModel.Designation = jQuery("#designation").val();
        jQuery("#Design").val(RequestModel.Designation);

        RequestModel.RMID = jQuery("#reporting").val();
        jQuery("#Rid").val(RequestModel.RMID);

        

        jQuery.ajax({
            type: "POST",
            url: "/Account/OnBoardRequest",
            
            data: JSON.stringify(RequestModel),


            dataType: "json",
            success: function () {
                alert("sucess");
            }
        });

    });
});




    