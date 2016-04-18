var app = angular.module('todo', []);

app.controller('todoCtrl', function ($scope) {
    //initialize empty list of todo items
    $scope.list = [];
    $scope.newItem;

    $scope.finishedTasks = [];

    $scope.addTaskEnter = function (event) {
        if (event.keyCode == 13) {
            $scope.addTask();
        }
    }

    //Itemul meu ar trebui sa fie un obiect, care are un nume si o valoare de "checked".
    $scope.addTask = function () {
        $scope.list[$scope.list.length] = $scope.newItem;
        $scope.newItem = '';
    }

    $scope.check = function (task) {
        $scope.finishedTasks[$scope.finishedTasks.length] = task;
    }
});