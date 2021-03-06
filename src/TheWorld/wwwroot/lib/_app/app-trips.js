//app-trips.js
(function () {

    "use strict";

    //creating the module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html"
            });

            $routeProvider.when("/editor/:tripName", {
                controller: "tripEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });
    }])

})();