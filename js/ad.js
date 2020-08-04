jQuery(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready
    console.log("document is ready");


    jQuery('.btn[href^=#]').click(function (e) {
        e.preventDefault();
        var href = jQuery(this).attr('href');
        jQuery(href).modal('toggle');
    });



});







