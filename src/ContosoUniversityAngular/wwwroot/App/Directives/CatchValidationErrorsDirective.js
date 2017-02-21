(function () {
    directivesModule.directive('catchValidationErrors', ['$rootScope', function ($rootScope) {

        function link(scope, element) {
            $rootScope.$on('validationError', function (event, errorMessages) {

                var validationErrorsList = document.getElementById('validationSummary');

                if (validationErrorsList !== null) {
                    element.empty();
                }

                validationErrorsList = document.createElement('ul');
                validationErrorsList.id = 'validationSummary';
                validationErrorsList.classList.add('alert', 'alert-danger');

                angular.forEach(errorMessages, function (message) {
                    var li = document.createElement("li");
                    li.textContent = message;
                    li.classList.add("error-li");

                    validationErrorsList.appendChild(li);
                });

                element.append(validationErrorsList);
            });
        }

        return{
            link: link
        };
    }]);
})();