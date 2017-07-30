//tripEditorController.js
(function () {

    "use strict";
    tripEditorController.$inject = ["$routeParams", "$http"];
    angular.module("app-trips")
    .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {

        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};
        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
        .then(function (response) {
            //success
            angular.copy(response.data, vm.stops);
            _showmap(vm.stops);
        }, function (err) {
            //failure
            vm.errorMessage = "Failed to load stops";
        })
        .finally(function(){
            vm.isBusy = false;
        });

        vm.addStop = function () {
            vm.isBusy = true;
            $http.post(url, vm.newStop)
            .then(function (response) {
                //success
                vm.stop.push(response.data);
                _showmap(vm.stops);
                vm.newStop = {};
            }, function () {
                //failure
                vm.errorMessage="failed to add new stop";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };
    }

    //underscore is to show that the function is a private function that is only going ot be used in this file.
    function _showmap(stops) {

        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function(item){
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
            };
        });

            //show map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoomo: 3
            });
        }

    }
})();