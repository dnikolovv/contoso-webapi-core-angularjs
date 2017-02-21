var common = angular.module('common', ['toastr', 'directives', 'ui.router']);

var app = angular.module('app', ['common'])
    .config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true, requireBase: false
        });
    }])
    .controller('IndexController', ['$scope', function ($scope) {
        $scope.author = 'Dobromir Nikolov'
    }]);