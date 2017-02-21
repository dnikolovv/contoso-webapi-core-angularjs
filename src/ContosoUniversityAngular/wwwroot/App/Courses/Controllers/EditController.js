(function () {
    'use strict';

    coursesApp.controller('EditController', ['$state', 'CoursesService', 'ErrorsService', 'course', 'departments',
        function ($state, CoursesService, ErrorsService, course, departments) {

            var vm = this;

            vm.course = course;
            vm.departments = departments;
            vm.editCourse = editCourse;
            vm.hideEditForm = hideEditForm;

            function editCourse(course) {
                CoursesService.editCourse(course)
                    .then(function () {
                        $state.go('courses.details', { id: course.id });
                    }, function (errorResponse) {
                        ErrorsService.throwResponseErrors(errorResponse);
                    });
            }

            function hideEditForm() {
                $state.go('courses.details', { id: course.id });
            }
        }]);
})();