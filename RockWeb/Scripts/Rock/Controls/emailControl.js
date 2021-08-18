(function ($) {
    'use strict';
    window.Rock = window.Rock || {};
    Rock.controls = Rock.controls || {};

    Rock.controls.emailControl = (function () {
        var exports = {
            initialize: function (options) {
                if (!options.id) {
                    throw 'id is required';
                }
            },
            clientValidate: function (validator, args) {
                var $emailControl = $(validator).closest('.js-emailControl');
                var isValid = true;

                var primaryEmail = $emailControl.find('.js-primary input').val().trim().toLowerCase();
                var confirmEmail = $emailControl.find('.js-confirm input').val().trim().toLowerCase();

                if (primaryEmail.length > 0 && primaryEmail !== confirmEmail) {
                    isValid = false;
                    validator.errormessage = "Email and confirmation do not match.";
                    $emailControl.find('.js-confirm').closest('.form-group').addClass('has-error');
                }
                else {
                    $emailControl.find('.js-confirm').closest('.form-group').removeClass('has-error');
                }

                args.IsValid = isValid;
            }
        };

        return exports;
    }());
}(jQuery));
