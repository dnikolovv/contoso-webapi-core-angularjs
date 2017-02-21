(function () {
    'use strict';

    describe('catch-validation-errors directive', function () {

        var ErrorsService;
        var $rootScope;
        var $compile;
        var $timeout;

        beforeEach(module('directives'));
        beforeEach(module('common'));

        beforeEach(inject(function (_ErrorsService_, _$rootScope_, _$compile_, _$timeout_) {
            ErrorsService = _ErrorsService_;
            $rootScope = _$rootScope_;
            $compile = _$compile_;
            $timeout = _$timeout_;
        }));

        it('should integrate properly with ErrorsService and insert errors into parent', function () {
            var element = $compile('<div catch-validation-errors></div>')($rootScope);

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

            ErrorsService.throwResponseErrors(errorResponse);

            $timeout.flush();

            element = element[0];

            expect(element.childNodes.length).toEqual(1);

            var validationList = element.childNodes[0];

            expect(validationList.childNodes.length).toEqual(2);
            expect(validationList.childNodes[0].innerHTML).toEqual('First error message');
            expect(validationList.childNodes[1].innerHTML).toEqual('Second error message');
        });
    });
})();