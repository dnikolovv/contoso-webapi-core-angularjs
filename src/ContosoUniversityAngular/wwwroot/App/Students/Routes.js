(function () {
    'use strict';

    studentsApp.config(['$stateProvider', '$locationProvider',
        function ($stateProvider, $locationProvider) {

            $stateProvider
                .state('students', {
                    url: '/students',
                    resolve: {
                        students: ['$q', 'StudentsService', function ($q, StudentsService) {

                            var deferred = $q.defer();

                            StudentsService.getStudents().then(function (response) {
                                deferred.resolve(response.data.students);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Students/Views/StudentsListView.html',
                            controller: 'ListController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('students.add', {
                    url: '/add',
                    resolve: {
                        newStudent: function () {
                            return {}
                        }
                    },
                    views: {
                        'add': {
                            templateUrl: '/App/Students/Views/StudentAddView.html',
                            controller: 'AddController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('students.details', {
                    url: "/details/:id",
                    resolve: {
                        student: ['$q', 'StudentsService', '$stateParams', function ($q, StudentsService, $stateParams) {

                            var deferred = $q.defer();

                            StudentsService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Students/Views/StudentDetailsView.html',
                            controller: 'DetailsController',
                            controllerAs: 'model'
                        }
                    }
                });


            $stateProvider
                .state('students.edit', {
                    url: "/edit/:id",
                    resolve: {
                        student: ['$q', 'StudentsService', '$stateParams', function ($q, StudentsService, $stateParams) {

                            var deferred = $q.defer();

                            StudentsService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }],
                        availableCourses: ['$q', 'StudentsService', function ($q, StudentsService) {

                            var deferred = $q.defer();

                            StudentsService.getAvailableCourses().then(function (response) {
                                deferred.resolve(response.data.courses);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Students/Views/StudentEditView.html',
                            controller: 'EditController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('students.delete', {
                    url: "/delete/:id",
                    controller: "DeleteController"
                });


            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        }]);
})();