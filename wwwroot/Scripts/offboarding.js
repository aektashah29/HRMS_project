jQuery(document).ready(function () {
    jQuery("#button").click(function () {

        var offcheckpoint = {};

        offcheckpoint.offcheckpointName = jQuery("#offcheck").val();


        offcheckpoint.Businessunit = jQuery("#offbunit").val();
        jQuery("#Bnid").val(offcheckpoint.Businessunit);

        offcheckpoint.DeptName = jQuery("#offdept").val();
        jQuery("#DEid").val(offcheckpoint.DeptName);

        offcheckpoint.AssigneeName = jQuery("#offasign").val();
        jQuery("#aId").val(offcheckpoint.AssigneeName);

        offcheckpoint.Description = jQuery("#offdesc").val();
  
        jQuery.ajax({
            type: "POST",
            url: "/OffBoarding/OffBoardingcheckpoint",
           

            data: JSON.stringify(offcheckpoint),


            dataType: "json",
            success: function () {
                alert("sucess");
            }
        });
    });
});