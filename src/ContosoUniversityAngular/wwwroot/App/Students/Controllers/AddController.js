(function () {
    'use strict';

    studentsApp.controller('AddController', ['$state', 'newStudent',  'StudentsService', 'ErrorsService', 'NotificationsService',
        function ($state, newStudent, StudentsService, ErrorsService, NotificationsService) {

            var vm = this;

            vm.newStudent = newStudent;
            vm.addStudent = addStudent;
            vm.hideAddStudentBox = function () { $state.go('students') };

            function addStudent(student) {
                StudentsService.createStudent(student).then(function () {
                    NotificationsService.success('Student added successfully!');
                    $state.go('students', {}, { reload: true });
                }, function (errorResponse) {
                    ErrorsService.throwResponseErrors(errorResponse);
                });
            }
        }]);
})();