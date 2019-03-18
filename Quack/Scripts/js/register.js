jQuery(document).ready(function () {
    jQuery("#registration-form").on("submit", function (e) {
        e.preventDefault();

        var newUser =
        {
            email: jQuery("#email").val(),
            name: jQuery("#first-name").val(),
            password: jQuery("#password").val()
        };
      
        jQuery.ajax({
            url: "/User/Create",
            data: newUser,
            cache: false,
            type: 'POST',
            success: function (data) {
                console.log(data);
            }
        });
    });
});