(function () {
    'use strict';

    instructorsApp.factory('InstructorsService', ['$http', function ($http) {

        var instructorsRoutePrefix = "/api/instructors";

        var getInstructors = function () {

            var requestUrl = instructorsRoutePrefix + "/all";

            return $http({ method: "GET", url: requestUrl });
        };

        var createInstructor = function (instructor) {

            var requestUrl = instructorsRoutePrefix + "/create";

            return $http({
                method: "POST", url: requestUrl, data: instructor, headers: { 'Content-Type': 'application/json' }});
        };

        var getDetails = function (instructorId) {

            var requestUrl = instructorsRoutePrefix + "/details/" + instructorId;

            return $http({ method: "GET", url: requestUrl, headers: { 'Content-Type': 'application/json' },
                transformResponse: function (data) {
                    data = angular.fromJson(data);

                    if (typeof data.hireDate !== 'undefined') {
                        data.hireDate = new Date(data.hireDate);
                    }

                    return data;
                }});
        };

        var deleteInstructor = function (instructorId) {

            var requestUrl = instructorsRoutePrefix + "/delete/" + instructorId;

            return $http({ method: "DELETE", url: requestUrl, headers: { 'Content-Type': 'application/json' } });
        };

        var editInstructor = function (data) {

            var requestUrl = instructorsRoutePrefix + "/edit";

            return $http({ method: "POST", data: data, url: requestUrl, headers: { 'Content-Type': 'application/json' }});
        };

        var getAvailableCourses = function () {

            var requestUrl = "/api/courses/all";

            return $http({ method: "GET", url: requestUrl });
        };

        // This is here because you can ask for a course that is taught by an instructor, and it is not worth coupling the
        // courses module with the instructors just for that query
        var getCourseDetails = function (courseId) {

            var requestUrl = "/api/courses/details/" + courseId;

            return $http({ method: "GET", url: requestUrl, headers : { 'Content-Type': 'application/json' }});
        };

        return {
            getInstructors: getInstructors,
            createInstructor: createInstructor,
            getDetails: getDetails,
            deleteInstructor: deleteInstructor,
            editInstructor: editInstructor,
            getAvailableCourses: getAvailableCourses,
            getCourseDetails: getCourseDetails
        };
    }]);
})();