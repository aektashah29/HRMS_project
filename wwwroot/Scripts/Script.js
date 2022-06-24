jQuery(document).ready(function () {

    jQuery("#ContId").on("change", function () {

        jQuery("#stateid").empty();
        var id = jQuery(this).val();
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetStates",
            dataType: "json",
            data: { CountryId: id },
            success: function (states) {

                jQuery("#stateid").append("<option value=" + "Select State" +-1 + ">" + "Select State" + "</option>");
                jQuery.each(states, function (key, state) {
                    jQuery("#stateid").append("<option value=" + state.value + ">" + state.text + "</option>");
                });

            }

        });



    });





    jQuery("#stateid").on("change", function () {
        jQuery("#CityId").empty();
        var id = jQuery(this).val();
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetCities",
            dataType: "json",
            data: { StateId: id },
            success: function (cities) {
                jQuery("#CityId").append("<option value=" + "Select City" + ">" + "Select City" + "</option>");
                jQuery.each(cities, function (key, city) {
                    jQuery("#CityId").append("<option value=" + city.value + ">" + city.text + "</option>");
                });
            }
        });



    });





    jQuery("#CityId").on("change", function () {
        jQuery("#PCId").empty();
        var id = jQuery(this).val();
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetPostalCode",
            dataType: "json",
            data: { cityId: id },
            success: function (codes) {
                jQuery("#PCId").append("<option value=" + "Select postalCode" + ">" + "Select postalCode" + "</option>");
                jQuery.each(codes, function (key, PostalCode) {
                    jQuery("#PCId").append("<option value=" + PostalCode.value + ">" + PostalCode.text + "</option>");
                });
            }
        });



    });



    jQuery("#btnsubmit").click(function () {
      
        var register = {};
        register.UserName = jQuery("#UserName").val();
        register.Gender = jQuery("#Gender").val();
        register.Address = jQuery("#Address").val();

        register.Country = jQuery("#ContId").val();
        jQuery("#cntid").val(register.Country);

        register.State = jQuery("#stateid").val();
        jQuery("#stid").val(register.State);

        register.City = jQuery("#CityId").val();
        jQuery("#ctid").val(register.City);

        register.Postalcode = jQuery("#PCId").val();
        jQuery("#pid").val(register.Postalcode);

        register.Email = jQuery("#Email").val();
        register.Password = jQuery("#Password").val();
        register.ConfirmPassword = jQuery("#CPwd").val();


            jQuery.ajax({
            type: "POST",
            url: "/Account/Register",
          

            data: JSON.stringify(register),


            dataType: "json",
            success: function () {
                alert("sucess");
            }
        });

    });
});