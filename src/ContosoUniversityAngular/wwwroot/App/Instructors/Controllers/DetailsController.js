(function () {
    'use strict';

    instructorsApp.controller('DetailsController', ['$state', 'instructor',
        function ($state, instructor) {

        var vm = this;

        vm.instructor = instructor;
        vm.showEditForm = function () { $state.go('instructors.edit', { id: instructor.id }); };
    }]);

})();
