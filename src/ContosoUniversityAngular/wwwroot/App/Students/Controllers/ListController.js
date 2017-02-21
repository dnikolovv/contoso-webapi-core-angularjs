(function () {
    'use strict';

    studentsApp.controller('ListController', ['$state', 'students',
        function ($state, students) {

            var vm = this;
            vm.students = students;
            vm.getStudentDetails = function (studentId) { $state.go('students.details', { id: studentId }) };
            vm.deleteStudent = function (studentId) { $state.go('students.delete', { id: studentId }) };
            vm.showAddStudentBox = function () { $state.go('students.add') };
        }]);
})();