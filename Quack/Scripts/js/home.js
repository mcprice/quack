window.onscroll = function (e) {
    // called when the window is scrolled.  
    var scrollPosition = jQuery("body, html").scrollTop();

    if (scrollPosition !== 0) {
        jQuery(".navbar").removeClass("bg-transparent");
        jQuery(".navbar").addClass("bg-light");
    } else {
        jQuery(".navbar").addClass("bg-transparent");
        jQuery(".navbar").removClass("bg-light");
    }
};