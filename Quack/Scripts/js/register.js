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
                if (data === "1") {
                    alert("User Created");
                    $('#registration-form')[0].reset();
                }
                else if (data === "2") {
                    alert("The account was not created because there is already an account associated with '" + jQuery("#email").val() + "'.");
                }
            }
        });
    });
});