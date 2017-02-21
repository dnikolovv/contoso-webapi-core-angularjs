(function () {
    'use strict';

    instructorsApp.controller('InstructorsCoursesController', ['$state', 'courses',
        function ($state, courses) {

            var vm = this;
            vm.courses = courses;
            vm.selectedCourseId = null;
            vm.selectCourse = function (courseId) {
                $state.go('instructors.courses.students', { courseId: courseId })
                vm.selectedCourseId = courseId;
            };
        }]);

})();