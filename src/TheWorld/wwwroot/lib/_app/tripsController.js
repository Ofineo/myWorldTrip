//trips-Controller.js
(function () {

    "use strict";

    //getting teh existing module
    tripsController.$inject = ["$http"];
    angular.module("app-trips")
    .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
        .then(function (response) {
            //Success
            angular.copy(response.data, vm.trips);
            //console.log(vm.trips);
        }, function (error) {
            //Failure
            vm.errorMessage = "Failed to load data" + error;
        })
            //executes no matter the result of the http call
        .finally(function(){
            vm.isBusy = false;
        });

        vm.addTrip = function () {
            //how to add items to an existing collection it does not save it only on the fly
            //vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            //vm.newTrip={};
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    //Success
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                  
                }, function () {
                    //Failure
                    vm.errorMessage = "Failed to save new trip";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    };
    })();