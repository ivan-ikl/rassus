(function () {
    var app = angular.module('andacolApp', []);

    app.controller('questionController', function ($scope) {

        $scope.questionType = QuestionType || 'ScoreQuestion';

        $scope.options = [];

        $scope.newOption = "";

        $scope.deleteOption = function (index) {
            $scope.options.splice(index, 1);
        };

        $scope.addOption = function () {
            if ($scope.isUnique()) {
                $scope.options.push($scope.newOption);
                $scope.newOption = "";
            }
        };

        $scope.isUnique = function () {
            console.log($scope.options.indexOf($scope.newOption));
            return ($scope.options.indexOf($scope.newOption) < 0);
        };

    });

})();