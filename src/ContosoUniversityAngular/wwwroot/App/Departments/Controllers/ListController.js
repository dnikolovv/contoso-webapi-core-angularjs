(function () {
    'use strict';

    departmentsApp.controller('ListController', ['$state', 'departments',
        function ($state, departments) {

            var vm = this;

            vm.departments = departments;

            vm.showAddDepartmentBox = function () { $state.go('departments.add'); };
            vm.getDepartmentDetails = function (departmentId) { $state.go('departments.details', { id: departmentId }) };
            vm.deleteDepartment = function (departmentId) { $state.go('departments.delete', { id: departmentId }) };
        }]);
})();