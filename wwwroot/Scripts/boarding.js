jQuery(document).ready(function () {
    jQuery("#Buttton").click(function () {
      
        var onCheckpoint = {};

        onCheckpoint.CheckpointName = jQuery("#check").val();
      
        onCheckpoint.BUnitID = jQuery("#abcd").val();
        jQuery("#Bnid").val(onCheckpoint.BUnitID);

        onCheckpoint.DeptId = jQuery("#efgh").val();
        jQuery("#DEid").val(onCheckpoint.DeptId);

        onCheckpoint.AssigneeId = jQuery("#asign").val();
        jQuery("#aId").val(onCheckpoint.AssigneeId);

        onCheckpoint.Description = jQuery("#desc").val();


        console.log(onCheckpoint);
        jQuery.ajax({
            type: "POST",
            url: "/Boarding/ONBOARDINGCHECKPOINT",
       
            data: JSON.stringify(onCheckpoint),

            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
             
                alert("sucess");
                
            }
        });

    });
});

