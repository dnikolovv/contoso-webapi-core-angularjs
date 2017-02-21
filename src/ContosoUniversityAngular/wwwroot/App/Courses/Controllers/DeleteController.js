(function () {
    'use strict';

    coursesApp.controller('DeleteController', ['$state', '$stateParams', 'NotificationsService', 'CoursesService',
        function ($state, $stateParams, NotificationsService, CoursesService) {

        CoursesService.deleteCourse($stateParams.id)
                 .then(function () {
                     NotificationsService.success("Successfuly deleted course!");
                     $state.go('courses', {}, { reload: true });
                 }, function () {
                     NotificationsService.error("Couldn't delete course.");
                 });
    }]);

})();