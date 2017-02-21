(function () {
    'use strict';

    coursesApp.factory('CoursesService', ['$http', function ($http) {

        var coursesRoutePrefix = "/api/courses";

        var getCourses = function (selectedDepartmentName) {

            var requestUrl = coursesRoutePrefix + "/all";

            if (typeof selectedDepartmentName !== "undefined") {
                requestUrl += "?selectedDepartmentName=" + selectedDepartmentName;
            }

            return $http({ method: "GET", url: requestUrl });
        };

        var createCourse = function (course) {

            var requestUrl = coursesRoutePrefix + "/create";

            return $http({ method: "POST", url: requestUrl, data: course, headers: { 'Content-Type': 'application/json' }});
        };

        var getDetails = function (courseId) {

            var requestUrl = coursesRoutePrefix + "/details/" + courseId;

            return $http({ method: "GET", url: requestUrl, headers: { 'Content-Type': 'application/json' } });
        };

        var deleteCourse = function (courseId) {

            var requestUrl = coursesRoutePrefix + "/delete/" + courseId;

            return $http({ method: "DELETE", url: requestUrl, headers: { 'Content-Type': 'application/json' } });
        };

        var editCourse = function (data) {

            var requestUrl = coursesRoutePrefix + "/edit";

            return $http({ method: "POST", data: data, url: requestUrl, headers: { 'Content-Type': 'application/json' }});
        };

        var getAvailableDepartments = function () {

            var requestUrl = "/api/departments/all";

            return $http({ method: "GET", url: requestUrl });
        };

        return {
            getCourses: getCourses,
            createCourse: createCourse,
            getDetails: getDetails,
            deleteCourse: deleteCourse,
            editCourse: editCourse,
            getAvailableDepartments: getAvailableDepartments
        };
    }]);
})();