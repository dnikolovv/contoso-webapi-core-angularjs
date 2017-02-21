(function () {
    'use strict';

    coursesApp.config(['$stateProvider', '$locationProvider',
        function ($stateProvider, $locationProvider) {

            $stateProvider
                .state('courses', {
                    url: '/courses',
                    resolve: {
                        courses: ['$q', 'CoursesService', function ($q, CoursesService) {

                            var deferred = $q.defer();

                            CoursesService.getCourses().then(function (response) {
                                deferred.resolve(response.data.courses);
                            });

                            return deferred.promise;
                        }]
                    },
                    templateUrl: '/App/Courses/Views/CoursesListView.html',
                    controller: 'ListController',
                    controllerAs: 'model'
                });

            $stateProvider
                .state('courses.add', {
                    url: '/add',
                    resolve: {
                        departments: ['$q', 'CoursesService', function ($q, CoursesService) {

                            var deferred = $q.defer();

                            CoursesService.getAvailableDepartments().then(function (response) {
                                deferred.resolve(response.data.departments);
                            });

                            return deferred.promise;
                        }],
                        newCourse: function () {
                            return {}
                        }
                    },
                    views: {
                        'add': {
                            templateUrl: '/App/Courses/Views/CourseAddView.html',
                            controller: 'AddController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('courses.details', {
                    url: '/details/:id',
                    resolve: {
                        course: ['$q', '$stateParams', 'CoursesService', function ($q, $stateParams, CoursesService) {

                            var deferred = $q.defer();

                            CoursesService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Courses/Views/CourseDetailsView.html',
                            controller: 'DetailsController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('courses.edit', {
                    url: '/edit/:id',
                    resolve: {
                        course: ['$q', '$stateParams', 'CoursesService', function ($q, $stateParams, CoursesService) {

                            var deferred = $q.defer();

                            CoursesService.getDetails($stateParams.id).then(function (response) {
                                deferred.resolve(response.data);
                            });

                            return deferred.promise;
                        }],
                        departments: ['$q', 'CoursesService', function ($q, CoursesService) {

                            var deferred = $q.defer();

                            CoursesService.getAvailableDepartments().then(function (response) {
                                deferred.resolve(response.data.departments);
                            });

                            return deferred.promise;
                        }]
                    },
                    views: {
                        '@': {
                            templateUrl: '/App/Courses/Views/CourseEditView.html',
                            controller: 'EditController',
                            controllerAs: 'model'
                        }
                    }
                });

            $stateProvider
                .state('courses.delete', {
                    url: '/delete/:id',
                    controller: 'DeleteController'
                });

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        }]);
})();