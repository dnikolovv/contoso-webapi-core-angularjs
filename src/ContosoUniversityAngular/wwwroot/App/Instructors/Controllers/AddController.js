(function () {
    'use strict';

    instructorsApp.controller('AddController', ['$state', 'newInstructor', 'InstructorsService', 'ErrorsService', 'NotificationsService',
        function ($state, newInstructor, InstructorsService, ErrorsService, NotificationsService) {

        var vm = this;

        vm.newInstructor = newInstructor;
        vm.addInstructor = addInstructor;
        vm.hideAddInstructorBox = function () { $state.go('instructors') };

        function addInstructor(instructor) {
            InstructorsService.createInstructor(instructor)
                .then(function (response) {
                    NotificationsService.success("Instructor added successfuly!");
                    $state.go('instructors', {}, { reload: true });
                }, function (errorResponse) {
                    ErrorsService.throwResponseErrors(errorResponse);
                });
        }
    }]);
})();