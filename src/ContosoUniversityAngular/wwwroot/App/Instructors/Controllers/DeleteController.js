(function () {
    'use strict';

    instructorsApp.controller('DeleteController', ['$state', '$stateParams', 'NotificationsService', 'InstructorsService',
        function ($state, $stateParams, NotificationsService, InstructorsService) {

            InstructorsService.deleteInstructor($stateParams.id)
                .then(function () {
                    NotificationsService.success("Successfuly deleted instructor!");
                    $state.go('instructors', {}, { reload: true });
                }, function () {
                    NotificationsService.error("Couldn't delete instructor.");
                });
        }]);

})();
