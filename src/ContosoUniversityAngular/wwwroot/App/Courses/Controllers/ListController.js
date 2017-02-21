(function () {
    'use strict';

    coursesApp.controller('ListController', ['$state', 'courses',
        function ($state, courses) {

            var vm = this;
            vm.courses = courses;

            vm.showAddCourseBox = function () { $state.go('courses.add'); };
            vm.getCourseDetails = function (courseId) { $state.go('courses.details', { id: courseId }); };
            vm.deleteCourse = function (courseId) { $state.go('courses.delete', { id: courseId }) };
        }]);
})();