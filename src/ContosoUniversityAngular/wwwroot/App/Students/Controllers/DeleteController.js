(function () {
    'use strict';

    studentsApp.controller('DeleteController', ['$state', '$stateParams', 'StudentsService', 'NotificationsService',
        function ($state, $stateParams, StudentsService, NotificationsService) {

            StudentsService.deleteStudent($stateParams.id).then(function () {
                NotificationsService.success('Successfully deleted student!');
                $state.go('students', {}, { reload: true });
            }, function () {
                NotificationsService.error('There was an error deleting the student.');
            });
        }]);
})();