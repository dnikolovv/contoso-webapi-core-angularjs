common.factory('ErrorsService', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    var throwResponseErrors = function (errorResponse) {
        var errorMessages = extractErrorsFromResponse(errorResponse);
        $timeout(function () {
            $rootScope.$broadcast('validationError', errorMessages);
        });
    };

    function extractErrorsFromResponse(response) {
        var errorMessages = [];

        // Cycle through all the nodes that the response contains
        angular.forEach(response.data, function (value, key) {
            // For each node, find the "Errors" node inside and extract the errors
            angular.forEach(value, function (value, key) {
                // If you find the "Errors" node, cycle through it and insert all error messages into the errorMessages collection
                if (key === "Errors") {
                    angular.forEach(value, function (value) {
                        errorMessages.push(value.ErrorMessage);
                    });
                }
            });
        });

        return errorMessages;
    }

    return {
        throwResponseErrors: throwResponseErrors
    };
}]);