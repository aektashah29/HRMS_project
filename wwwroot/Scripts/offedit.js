jQuery(document).ready(function () {
    jQuery("#offeditsubmit").click(function () {
        alert("getdata");
        var offcheckpoint = {};

        offcheckpoint.offcheckpointName = jQuery("#ofpoint").val();

        offcheckpoint.BUnitID = jQuery("#offbunit").val();
        jQuery("#aaaa").val(onCheckpoint.BUnitID);

        offcheckpoint.DeptId = jQuery("#offDept").val();
        jQuery("#dddd").val(onCheckpoint.DeptId);

        offcheckpoint.AssigneeId = jQuery("#offasign").val();
        jQuery("#ssss").val(onCheckpoint.AssigneeId);

        offcheckpoint.Description = jQuery("#offdisc").val();

        jQuery.ajax({
            type: "POST",
            url: "/OffBoarding/ONEdit",
           
            data: postData,

            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                alert("sucess");

            }
        });

    });
});
