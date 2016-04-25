var app = angular.module('moviesApp', []);

app.service("MoviesService", function ($http) {
    this.getActors = function(callbackSuccess, callbackError){
        return $http({
            method: "GET",
            url: "/api/actors"
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) { callbackError(data.data) });
    }

    this.addNewActor = function (newActor, callbackSuccess, callbackError) {
        $http({
            method: "POST",
            url: "/api/actors",
            data: newActor
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) { callbackError(data.data) });
    }

    this.updateActor = function (actor, callbackSuccess, callbackError) {
        $http({
            method: "PUT",
            url: "/api/actors/" + actor.Id,
            data: id, actor
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) { callbackError(data.data) });
    }
});

app.controller('MoviesCtrl', function ($scope, MoviesService) {
    $scope.addActorState = { text: 'Add new actor', action: $scope.addNewActor };
    $scope.updateActorState = { text: 'Edit actor', action: $scope.updateActor };

    $scope.actorsList = null;
    $scope.newActor = null;
    $scope.buttonState = $scope.addActorState;

    var succ = function (dataResponse) {
        $scope.actorsList = dataResponse;
    }

    var err = function (dataResponse) {
        console.log("Error on http request!");
    }

    var succAdd = function (dataResponse) {
        $scope.actorsList[$scope.actorsList.length] = dataResponse;
        $scope.newActor = null;
    }

    var succUpd = function () {
        MoviesService.getActors(succ, err);
    }
    
    $scope.addNewActor = function () {
        MoviesService.addNewActor($scope.newActor, succAdd, err);
    }

    $scope.updateActor = function () {
        MoviesService.updateActor($scope.newActor, succUpd, err);
        $scope.buttonState = $scope.addActorState;
    }

    $scope.submit = function () {
        console.log(typeof ($scope.buttonState.action));
        $scope.buttonState.action();
    }

    $scope.selectActor = function (actor) {
        $scope.newActor = actor;
        $scope.buttonState = $scope.updateActorState;
    }

    MoviesService.getActors(succ, err);
    
});