(function () {
    'use strict';

    studentsApp.controller('EditController', ['$state', 'student', 'availableCourses', 'StudentsService', 'ErrorsService',
        function ($state, student, availableCourses, StudentsService, ErrorsService) {

            var vm = this;

            vm.student = student;
            vm.availableCourses = checkEnrolledCourses(availableCourses);
            vm.hideEditForm = function () { $state.go('students.details', { id: student.id })};
            vm.editStudent = editStudent;

            function editStudent() {
                processCheckedCourses(vm.availableCourses);
                StudentsService.editStudent(vm.student)
                    .then(function () {
                        $state.go('students.details', { id: vm.student.id });
                    }, function (errorResponse) {
                        ErrorsService.throwResponseErrors(errorResponse);
                    });
            }

            function processCheckedCourses(courses) {
                var selectedCourses = [];

                angular.forEach(courses, function (course) {
                    if (course.isSelected) {
                        selectedCourses.push({ studentId: vm.student.id, courseId: course.id });
                    }
                });

                vm.student.enrollments = selectedCourses;
            }

            function checkEnrolledCourses(courses) {
                angular.forEach(courses, function (course) {
                    if (studentIsEnrolledInCourse(course.id)) {
                        course.isSelected = true;
                    } else {
                        course.isSelected = false;
                    }
                });

                return courses;
            }

            function studentIsEnrolledInCourse(courseId) {
                if (typeof vm.student.enrollments !== 'undefined') {
                    return vm.student.enrollments.some(function(e) {
                        return e.courseId == courseId;
                    });
                }
            }
        }]);
})();