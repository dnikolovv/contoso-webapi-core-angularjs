(function () {
    'use strict';

    studentsApp.controller('DetailsController', ['$state', 'student',
        function ($state, student) {

            var vm = this;

            vm.student = student;
            vm.showEditForm = function () { $state.go('students.edit', { id: student.id }) };
        }]);
})();