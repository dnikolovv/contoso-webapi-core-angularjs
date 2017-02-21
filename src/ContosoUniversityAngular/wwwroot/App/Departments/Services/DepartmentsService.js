departmentsApp.factory('DepartmentsService', ['$http', function ($http) {

    var departmentsRoutePrefix = "/api/departments";

    var getDepartments = function () {

        var requestUrl = departmentsRoutePrefix + "/all";

        return $http({ url: requestUrl, method: "GET"});
    };

    var addDepartment = function (department) {

        var requestUrl = departmentsRoutePrefix + "/create";

        return $http({url: requestUrl, data: department, method: "POST", headers: { 'Content-Type': 'application/json' }});
    };

    var deleteDepartment = function (departmentId) {

        var requestUrl = departmentsRoutePrefix + "/delete/" + departmentId;

        return $http({url: requestUrl, method: "DELETE", headers: { 'Content-Type': 'application/json' }});
    };

    var getDetails = function (departmentId) {

        var requestUrl = departmentsRoutePrefix + "/details/" + departmentId;

        return $http({url: requestUrl, method: "GET"});
    };

    var editDepartment = function (data) {

        var requestUrl = departmentsRoutePrefix + "/edit";

        return $http({ method: "POST", data: data, url: requestUrl, headers: { 'Content-Type': 'application/json' }});
    };

    var getAvailableInstructors = function () {

        var requestUrl = "/api/instructors/all";

        return $http({url: requestUrl, method: "GET"});
    };

    return {
        getDepartments: getDepartments,
        addDepartment: addDepartment,
        deleteDepartment: deleteDepartment,
        getDetails: getDetails,
        getAvailableInstructors: getAvailableInstructors,
        editDepartment: editDepartment
    };
}]);