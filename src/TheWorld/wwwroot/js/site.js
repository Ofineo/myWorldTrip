//site.js
(function () {

    //var ele = $("#username");
    //ele.text("jordi Melendez");

    //var main = $("#main");

    //main.mouseenter(function () {
    //    main.css("background-color", "#888");
    //});
    //main.mouseleave(function () {
    //    main.css("background-color", "");
    //});

    var $sideAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function ()
    //if it does not work clear cache before executing
    {
        $sideAndWrapper.toggleClass("hide-sidebar");
        if ($sideAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa fa-shield fa-rotate-90");
            $icon.addClass("fa fa-shield fa-rotate-270");
        } else {
            $icon.removeClass("fa fa-shield fa-rotate-270");
            $icon.addClass("fa fa-shield fa-rotate-90");
        };
    });

})();
