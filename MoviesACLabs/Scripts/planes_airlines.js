//Note: similar functions for the planes CRUD operations

var app = angular.module('planesApp', []);

app.service("AirlinesService", function ($http) {

    this.getAirlines = function (callbackSuccess, callbackError) {
        return $http({
            method: "GET",
            url: "/api/Airlines"
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) { callbackError(data.data) });
    }

    this.addNewAirline = function (newAirline, callbackSuccess, callbackError) {
        console.log(newAirline);
        $http({
            method: "POST",
            url: "/api/Airlines",
            data: newAirline
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) { callbackError(data.data) });
    }

    this.updateAirline = function (airline, callbackSuccess, callbackError) {
        $http({
            method: "PUT",
            url: "/api/Airlines/" + airline.Id,
            data: airline
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) {
            callbackError(data.data)
        });
    }

    this.deleteAirline = function (id, callbackSuccess, callbackError) {
        $http({
            method: "DELETE",
            url: "/api/Airlines/" + id
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) {
            callbackError(data.data)
        });
    }
});

app.controller('AirlinesCtrl', function ($scope, AirlinesService) {

    // Success and error callbacks.
    var succ = function (dataResponse) {
        $scope.airlinesList = dataResponse;
    }

    var err = function (dataResponse) {
        console.log("Error on http request!");
    }

    var succAdd = function (dataResponse) {
        $scope.airlinesList[$scope.airlinesList.length] = dataResponse;
        $scope.newAirline = null;
    }

    var succUpdDel = function () {
        AirlinesService.getAirlines(succ, err);
    }

    $scope.addNewAirline = function () {
        AirlinesService.addNewAirline($scope.newAirline, succAdd, err);
    }

    $scope.updateAirline = function () {
        AirlinesService.updateAirline($scope.newAirline, succUpdDel, err);
        $scope.buttonState = $scope.addAirlineState;
    }

    $scope.addAirlineState = { text: 'Add new airline', action: $scope.addNewAirline };
    $scope.updateAirlineState = { text: 'Edit airline', action: $scope.updateAirline };

    $scope.buttonState = $scope.addAirlineState;
    $scope.airlinesList = null;
    $scope.newAirline = { Id: 0, Name: null, CountryOfOrigin: null};

    $scope.submit = function () {
        //debug
        //console.log(typeof ($scope.buttonState.action));
        $scope.buttonState.action();
        $scope.buttonState = $scope.addAirlineState;
    }

    $scope.selectAirline = function (airline) {
        //deep cloning
        $scope.newAirline.Id = airline.Id;
        $scope.newAirline.Name = airline.Name;
        $scope.newAirline.CountryOfOrigin = airline.CountryOfOrigin;
        $scope.buttonState = $scope.updateAirlineState;
    }

    $scope.delete = function (id) {
        AirlinesService.deleteAirline(id, succUpdDel, err);
    }

    AirlinesService.getAirlines(succ, err);

});