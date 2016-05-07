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
        console.log(newActor);
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
            data: actor
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) {
            callbackError(data.data)
        });
    }

    this.deleteActor = function (id, callbackSuccess, callbackError) {
        $http({
            method: "DELETE",
            url: "/api/actors/" + id
        }).then(function (data) {
            callbackSuccess(data.data)
        }, function (data) {
            callbackError(data.data)
        });
    }
});

app.controller('MoviesCtrl', function ($scope, MoviesService) {

    // Success and error callbacks.
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

    var succUpdDel = function () {
        MoviesService.getActors(succ, err);
    }

    $scope.addNewActor = function () {
        MoviesService.addNewActor($scope.newActor, succAdd, err);
    }

    $scope.updateActor = function () {
        MoviesService.updateActor($scope.newActor, succUpdDel, err);
        $scope.buttonState = $scope.addActorState;
    }

    $scope.addActorState = { text: 'Add new actor', action: $scope.addNewActor };
    $scope.updateActorState = { text: 'Edit actor', action: $scope.updateActor };

    $scope.buttonState = $scope.addActorState;
    $scope.actorsList = null;
    $scope.newActor = { Id: 0, Name: null, DateOfBirth: null, Revenue: null };
    $scope.actorNameSearch = '';
    
    $scope.submit = function () {
        //debug
        //console.log(typeof ($scope.buttonState.action));
        $scope.buttonState.action();
        $scope.buttonState = $scope.addActorState;
    }

    $scope.selectActor = function (actor) {
        //deep cloning
        $scope.newActor.Id = actor.Id;
        $scope.newActor.Name = actor.Name;
        $scope.newActor.DateOfBirth = actor.DateOfBirth;
        $scope.newActor.Revenue = actor.Revenue;
        $scope.buttonState = $scope.updateActorState;
    }

    $scope.delete = function (id) {
        MoviesService.deleteActor(id, succUpdDel, err);
    }

    MoviesService.getActors(succ, err);
    
});