(function () {
    'use strict';

    describe('Errors service', function () {

        var ErrorsService;
        var $rootScope;
        var $timeout;

        beforeEach(module('common'));

        beforeEach(inject(function (_$rootScope_, _ErrorsService_, _$timeout_) {
            ErrorsService = _ErrorsService_;
            $rootScope = _$rootScope_;
            $timeout = _$timeout_;
        }));

        it('should fire the validationError event with proper arguments', function () {

            var errorResponse = {
                data: {
                    Courses: {
                        Errors: [
                            {
                                ErrorMessage: 'First error message'
                            },
                            {
                                ErrorMessage: 'Second error message'
                            }
                        ]
                    }
                }
            };

            spyOn($rootScope, '$broadcast').and.callThrough();

            ErrorsService.throwResponseErrors(errorResponse);

            $timeout.flush();

            expect($rootScope.$broadcast)
                .toHaveBeenCalledWith('validationError', ['First error message', 'Second error message']);
        });
    });
})();