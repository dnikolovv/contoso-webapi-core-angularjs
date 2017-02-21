(function () {
    'use strict';

    departmentsApp.controller('EditController', ['$state', 'department', 'instructors', 'DepartmentsService', 'ErrorsService',
        function ($state, department, instructors, DepartmentsService, ErrorsService) {

            var vm = this;

            vm.department = department;
            vm.instructors = instructors;
            vm.editDepartment = editDepartment;
            vm.hideEditForm = function () { $state.go('departments.details', { id: department.id })};

            function editDepartment(department) {
                DepartmentsService.editDepartment(department)
                    .then(function () {
                        $state.go('departments.details', { id: department.id });
                    }, function (errorResponse) {
                        ErrorsService.throwResponseErrors(errorResponse);
                    });
            }
        }]);
})();