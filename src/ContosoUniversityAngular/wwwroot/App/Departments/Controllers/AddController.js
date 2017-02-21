(function () {
    'use strict';

    departmentsApp.controller('AddController', ['$state', 'DepartmentsService', 'ErrorsService', 'NotificationsService', 'newDepartment', 'instructors',
        function ($state, DepartmentsService, ErrorsService, NotificationsService, newDepartment, instructors) {

            var vm = this;

            vm.instructors = instructors;
            vm.newDepartment = newDepartment;
            vm.addDepartment = addDepartment;
            vm.hideAddDepartmentBox = function () { $state.go('departments', {}, { reload: true }) };

            function addDepartment(department) {
                DepartmentsService.addDepartment(department)
                    .then(function (response) {
                        NotificationsService.success("Department added successfuly!");
                        $state.go('departments', {}, { reload: true });
                    }, function (errorResponse) {
                        ErrorsService.throwResponseErrors(errorResponse);
                    });
            }
        }]);
})();