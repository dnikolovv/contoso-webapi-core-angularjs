(function () {
    'use strict';

    departmentsApp.controller('DetailsController', ['$state', 'department',
        function ($state, department) {

            var vm = this;
            vm.department = department;
            vm.showEditForm = function () { $state.go('departments.edit', { id: department.id }) }
        }]);
})();