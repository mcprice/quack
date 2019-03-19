jQuery(document).ready(function () {
    jQuery("#login-form").on("submit", function (e) {
        e.preventDefault();

        var newUser =
        {
            email: jQuery("#email").val(),
            password: jQuery("#password").val()
        };

        jQuery.ajax({
            url: "/User/Login",
            data: newUser,
            cache: false,
            type: 'POST',
            success: function (data) {
                if (data === "1") {
                    location.href = "/";
                }
            }
        });
    });
});