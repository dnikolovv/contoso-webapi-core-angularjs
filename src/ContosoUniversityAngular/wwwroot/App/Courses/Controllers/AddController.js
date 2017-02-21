(function () {
    'use strict';

    coursesApp.controller('AddController', ['$state', 'CoursesService', 'ErrorsService', 'NotificationsService', 'departments', 'newCourse',
        function ($state, CoursesService, ErrorsService, NotificationsService, departments, newCourse) {

        var vm = this;

        vm.departments = departments;
        vm.newCourse = newCourse;
        vm.addCourse = addCourse;
        vm.hideAddCourseBox = function () { $state.go('courses') };

        function addCourse(course) {
            CoursesService.createCourse(course)
                .then(function () {
                    NotificationsService.success("Course added successfuly!");
                    $state.go('courses',  {}, { reload: true });
                }, function (errorResponse) {
                    ErrorsService.throwResponseErrors(errorResponse);
                });
        }
    }]);
})();