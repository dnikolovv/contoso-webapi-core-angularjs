(function () {
    'use strict';

    studentsApp.factory('StudentsService', ['$http', function ($http) {

        var studentsRoutePrefix = "/api/students";

        var getStudents = function () {

            var requestUrl = studentsRoutePrefix + "/all";

            return $http({method: "GET", url: requestUrl});
        };

        var getDetails = function (studentId) {

            var requestUrl = studentsRoutePrefix + "/details/" + studentId;

            return $http({method: "GET", url: requestUrl,
                transformResponse: function (data) {
                    data = angular.fromJson(data);

                    if(typeof data.enrollmentDate !== 'undefined') {
                        data.enrollmentDate = new Date(data.enrollmentDate);
                    }

                    return data;
                }});
        };

        var editStudent = function (data) {

            var requestUrl = studentsRoutePrefix + "/edit";

            return $http({
                method: "POST", url: requestUrl, data: data, headers: {'Content-Type': 'application/json'}});
        };

        var createStudent = function (data) {

            var requestUrl = studentsRoutePrefix + "/create";

            return $http({method: "POST", url: requestUrl, data: data, headers: {'Content-Type': 'application/json'}});
        };

        var deleteStudent = function (studentId) {

            var requestUrl = studentsRoutePrefix + "/delete/" + studentId;

            return $http({method: "DELETE", url: requestUrl, headers: {'Content-Type': 'application/json'}});
        };

        var getAvailableCourses = function () {

            var requestUrl = "/api/courses/all";

            return $http({method: "GET", url: requestUrl});
        };

        return {
            getStudents: getStudents,
            getDetails: getDetails,
            editStudent: editStudent,
            createStudent: createStudent,
            deleteStudent: deleteStudent,
            getAvailableCourses: getAvailableCourses
        };

    }]);
})();