(function () {
    'use strict';

    departmentsApp.controller('DeleteController', ['$state', '$stateParams', 'NotificationsService', 'DepartmentsService',
        function ($state, $stateParams, NotificationsService, DepartmentsService) {

            DepartmentsService.deleteDepartment($stateParams.id)
                .then(function () {
                    NotificationsService.success("Successfuly deleted department!");
                    $state.go('departments', {}, { reload: true });
                }, function () {
                    NotificationsService.error("Couldn't delete department.");
                });
        }]);
})();
