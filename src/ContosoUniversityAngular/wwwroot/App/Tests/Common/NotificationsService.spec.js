(function () {
    'use strict';

    describe('Notifications service', function () {

        var NotificationsService;
        var toastr;

        beforeEach(module('common'));

        beforeEach(inject(function (_NotificationsService_, _toastr_) {
            NotificationsService = _NotificationsService_;
            toastr = _toastr_;
        }));

        it('should display a success message', function () {

            spyOn(toastr, 'success').and.callThrough();

            NotificationsService.success('A sample message');

            expect(toastr.success).toHaveBeenCalledWith('A sample message');
        });

        it('should display an error message', function () {

            spyOn(toastr, 'error').and.callThrough();

            NotificationsService.error('A sample message');

            expect(toastr.error).toHaveBeenCalledWith('A sample message');
        });
    });
})();