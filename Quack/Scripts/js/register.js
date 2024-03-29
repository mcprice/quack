﻿jQuery(document).ready(function () {
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
                    alert("Your user account has been created. You may now login.");
                    $('#registration-form')[0].reset();
                }
                else if (data === "2") {
                    alert("The account was not created because there is already an account associated with '" + jQuery("#email").val() + "'.");
                }
                else {
                    alert("Something went wrong - your account may not have been created. Please try again or email mcprice0@gmail.com");
                }
            }
        });
    });
});