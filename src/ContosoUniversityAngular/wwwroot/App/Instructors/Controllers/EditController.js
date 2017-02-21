(function () {
    'use strict';

    instructorsApp.controller('EditController', ['$state', 'instructor', 'availableCourses', 'InstructorsService', 'ErrorsService',
        function ($state, instructor, availableCourses, InstructorsService, ErrorsService) {

            var vm = this;

            vm.instructor = instructor;
            vm.availableCourses = checkTaughtCourses(availableCourses);
            vm.editInstructor = editInstructor;
            vm.hideEditForm = function () { $state.go('instructors.details', { id: instructor.id }) };
            vm.instructorTeachesCourse = instructorTeachesCourse;

            function editInstructor() {
                processCheckedCourses(vm.availableCourses);
                InstructorsService.editInstructor(vm.instructor)
                    .then(function () {
                        $state.go('instructors.details', { id: vm.instructor.id });
                    }, function (errorResponse) {
                        ErrorsService.throwResponseErrors(errorResponse);
                    });
            }

            function checkTaughtCourses(courses) {
                angular.forEach(courses, function (course) {
                    if (instructorTeachesCourse(course.id)) {
                        course.isSelected = true;
                    } else {
                        course.isSelected = false;
                    }
                });

                return courses;
            }

            function processCheckedCourses(courses) {
                var selectedCourses = [];

                angular.forEach(courses, function (course) {
                    if (course.isSelected) {
                        selectedCourses.push({ instructorId: vm.instructor.id, courseId: course.id });
                    }
                });

                vm.instructor.selectedCourses = selectedCourses;
            }

            function instructorTeachesCourse(courseId) {
                if (typeof vm.instructor.courses !== 'undefined') {
                    return vm.instructor.courses.some(function(c) {
                        return c.id == courseId;
                    });
                }
            }
        }]);
})();