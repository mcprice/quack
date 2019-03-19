jQuery(document).ready(function () {
   
    jQuery("#feed-form").on("submit", function (e) {
        e.preventDefault();

        var newFeed =
        {
            location: jQuery("#location").val(),
            feedTime: jQuery("#feedTime").val(),
            numberFed: jQuery("#numberFed").val(),
            feedType: jQuery("#feedType").val(),
            gramsFed: jQuery("#gramsFed").val()
        };

        jQuery.ajax({
            url: "/Report/Feed",
            data: newFeed,
            cache: false,
            type: 'POST',
            success: function (data) {
                if (data === "1") {
                    alert("Your feeding has been recorded. Thank you!");
                }
                else {
                    alert("Something went wrong while trying to save your feeding event. Please try again.");
                }
            }
        });
    });
});