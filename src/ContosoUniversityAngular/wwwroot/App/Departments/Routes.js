(function () {
    'use strict';

    departmentsApp.config(['$stateProvider', '$locationProvider',
        function ($stateProvider, $locationProvider) {

            $stateProvider
                .state('departments', {
                    url: '/departments',
                    resolve: {
                        departments: ['$q', 'DepartmentsService', function ($q, DepartmentsService) {

                            var deferred = $q.defer();

                            DepartmentsService.getDepartments().then(function (response) {
                                deferred.resolve(response.data.departments);
                            });

                            return deferred.promise;
                        }]
                    },
                    templateUrl: '/App/Departments/Views/DepartmentsListView.html',
                    controller: 'ListController',
                    controllerAs: 'model'
                });

            $stateProvider
                .state('departments.add', {
                    url: '/add',
                    resolve: {
                        instructors: ['$q', 'DepartmentsService', function ($q, DepartmentsService) {

                            var deferred = $q.defer();

                            DepartmentsService.getAvailableInstructors().then(function (response) {
                                deferred.resolve(response.data.instructors);
                            });

                            return deferred.promise;
                        }],
                        newDepartment: function () {
                            return {}
                        }
                    },
                    views: {
                        'add': {
                            templateUrl: '/App/Departments/Views/DepartmentAddView.html',
                            controller: 'AddController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('departments.details', {
                    url: '/details/:id',
                    resolve: {
                        department: ['$q', 'DepartmentsService', '$stateParams', function ($q, DepartmentsService, $stateParams) {

                            var deferred = $q.defer();

                            DepartmentsService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Departments/Views/DepartmentDetailsView.html',
                            controller: 'DetailsController',
                            controllerAs: 'model'
                        }
                    }
                });


            $stateProvider
                .state('departments.edit', {
                    url: '/edit/:id',
                    resolve: {
                        department: ['$q', 'DepartmentsService', '$stateParams', function ($q, DepartmentsService, $stateParams) {

                            var deferred = $q.defer();

                            DepartmentsService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }],
                        instructors: ['$q', 'DepartmentsService', function ($q, DepartmentsService) {

                            var deferred = $q.defer();

                            DepartmentsService.getAvailableInstructors().then(function (response) {
                                deferred.resolve(response.data.instructors)
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Departments/Views/DepartmentEditView.html',
                            controller: 'EditController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('departments.delete', {
                    url: '/delete/:id',
                    controller: 'DeleteController'
                });


            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });

        }]);
})();