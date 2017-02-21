(function () {
    'use strict';

    instructorsApp.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

        $stateProvider
            .state('instructors', {
                url: '/instructors',
                resolve: {
                    instructors: ['$q', 'InstructorsService', function ($q, InstructorsService) {

                        var deferred = $q.defer();

                        InstructorsService.getInstructors().then(function (response) {
                            deferred.resolve(response.data.instructors);
                        });

                        return deferred.promise;
                    }]
                },
                views: {
                    '@': {
                        templateUrl: '/App/Instructors/Views/InstructorsListView.html',
                        controller: 'ListController',
                        controllerAs: 'model'
                    }
                }
            });

        $stateProvider
            .state('instructors.add', {
                url: '/add',
                resolve: {
                    newInstructor: function () {
                        return {};
                    }
                },
                views: {
                    'add@instructors': {
                        templateUrl: '/App/Instructors/Views/InstructorAddView.html',
                        controller: 'AddController',
                        controllerAs: 'model'
                    }
                }
            });

        $stateProvider
            .state('instructors.courses', {
                url: '/courses/:instructorId',
                resolve: {
                    courses: ['$q', '$stateParams', 'InstructorsService', function ($q, $stateParams, InstructorsService) {

                        var deferred = $q.defer();

                        InstructorsService.getDetails($stateParams.instructorId).then(function (response) {
                            deferred.resolve(response.data.courses);
                        });

                        return deferred.promise;
                    }]
                },
                views: {
                    'instructorsCourses@instructors': {
                        templateUrl: '/App/Instructors/Views/InstructorsCoursesView.html',
                        controller: 'InstructorsCoursesController',
                        controllerAs: 'model'
                    }
                }
            });

        $stateProvider
            .state('instructors.courses.students', {
                url: '/students/:courseId',
                resolve: {
                    enrollments: ['$q', '$stateParams', 'InstructorsService', function ($q, $stateParams, InstructorsService) {

                        var deferred = $q.defer();

                        InstructorsService.getCourseDetails($stateParams.courseId).then(function (response) {
                            deferred.resolve(response.data.enrollments);
                        });

                        return deferred.promise;
                    }]
                },
                views: {
                    'coursesStudents@instructors.courses': {
                        templateUrl: '/App/Instructors/Views/CoursesStudentsView.html',
                        controller: 'CoursesStudentsController',
                        controllerAs: 'model'
                    }
                }
            });


        $stateProvider
            .state('instructors.details', {
                url: '/details/:id',
                resolve: {
                    instructor: ['$q', 'InstructorsService', '$stateParams', function ($q, InstructorsService, $stateParams) {

                        var deferred = $q.defer();

                        InstructorsService.getDetails($stateParams.id).then(function (response) {
                            deferred.resolve(response.data);
                        });

                        return deferred.promise;
                    }]
                },
                views: {
                    '@': {
                        templateUrl: '/App/Instructors/Views/InstructorDetailsView.html',
                        controller: 'DetailsController',
                        controllerAs: 'model'
                    }
                }
            });


        $stateProvider
            .state('instructors.edit', {
                url: '/edit/:id',
                resolve: {
                    instructor: ['$q', 'InstructorsService', '$stateParams', function ($q, InstructorsService, $stateParams) {

                        var deferred = $q.defer();

                        InstructorsService.getDetails($stateParams.id).then(function (response) {
                            deferred.resolve(response.data);
                        });

                        return deferred.promise;
                    }],
                    availableCourses: ['$q', 'InstructorsService', function ($q, InstructorsService) {

                        var deferred = $q.defer();

                        InstructorsService.getAvailableCourses().then(function (response) {
                            deferred.resolve(response.data.courses);
                        });

                        return deferred.promise;
                    }]
                },
                views: {
                    '@': {
                        templateUrl: '/App/Instructors/Views/InstructorEditView.html',
                        controller: 'EditController',
                        controllerAs: 'model'
                    }
                }
            });

        $stateProvider
            .state('instructors.delete', {
                url: '/delete/:id',
                controller: 'DeleteController'
            });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }]);
})();