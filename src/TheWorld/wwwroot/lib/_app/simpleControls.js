//simpleControls.js
(function () {
    "use strict";
    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {

        return {
            scope: {
                //scope name is what is visible on the template waitcursor.html and the name is what is going to be used in the 
                //consumer of the directive Trips.cshtml
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl:"/views/waitCursor.html"
        }
    };


})();