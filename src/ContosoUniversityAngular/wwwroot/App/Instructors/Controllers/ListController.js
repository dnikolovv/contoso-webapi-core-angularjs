(function () {
    'use strict';

    instructorsApp.controller('ListController', ['$state', 'instructors',
        function ($state, instructors) {

        var vm = this;

        vm.instructors = instructors;
        vm.selectedInstructorId = null;
        vm.selectInstructor = function (instructorId) {
                $state.go('instructors.courses', { instructorId: instructorId });
                vm.selectedInstructorId = instructorId;
            };

        vm.getInstructorDetails = function (instructorId) { $state.go('instructors.details', { id: instructorId })};
        vm.deleteInstructor = function (instructorId) { $state.go('instructors.delete', { id: instructorId })};
        vm.showAddInstructorBox = function () { $state.go('instructors.add') };
    }]);
})();