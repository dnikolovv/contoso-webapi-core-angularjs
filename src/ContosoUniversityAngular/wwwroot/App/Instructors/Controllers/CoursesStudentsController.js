(function () {
    'use strict';

    instructorsApp.controller('CoursesStudentsController', ['$state', 'enrollments',
        function ($state, enrollments) {

            var vm = this;
            vm.enrollments = enrollments;
        }]);
})();
