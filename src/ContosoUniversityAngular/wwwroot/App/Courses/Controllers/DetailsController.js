(function () {
    'use strict';

    coursesApp.controller('DetailsController', ['$state', 'course',
        function ($state, course) {

            var vm = this;
            vm.course = course;
            vm.showEditForm = function () { $state.go('courses.edit', { id: course.id }) };
        }]);
})();