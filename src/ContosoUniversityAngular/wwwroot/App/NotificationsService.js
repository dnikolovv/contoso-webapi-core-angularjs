common.factory('NotificationsService', ['toastr', function (toastr) {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",//
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var success = function (message) {
        toastr.success(message);
    };

    var error = function (message) {
        toastr.error(message);
    };

    return {
        success: success,
        error: error
    }
}]);