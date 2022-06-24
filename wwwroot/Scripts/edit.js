jQuery(document).ready(function () {
    jQuery("#editsubmit").click(function () {
        alert("getdata");
        var onCheckpoint = {};

        onCheckpoint.CheckpointName = jQuery("#point").val();

        onCheckpoint.BUnitID = jQuery("#abcd").val();
        jQuery("#aaaa").val(onCheckpoint.BUnitID);
    
        onCheckpoint.DeptId = jQuery("#efgh").val();
        jQuery("#dddd").val(onCheckpoint.DeptId);

        onCheckpoint.AssigneeId = jQuery("#asign").val();
        jQuery("#ssss").val(onCheckpoint.AssigneeId);

        onCheckpoint.Description = jQuery("#desc").val();

        jQuery.ajax({
            type: "POST",
            url: "/Boarding/ONEdit",
          
            data: postData,

            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                alert("sucess");

            }
        });

    });
});
