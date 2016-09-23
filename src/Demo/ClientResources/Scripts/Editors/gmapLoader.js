
define(['dojo/_base/kernel'], function (kernel) {
    'use strict';

    var w = kernel.global,
        cb = '_googleApiLoadCallback';

    return {
        load: function (param, req, loadCallback) {
            if (!cb) {
                return;
            }
            w[cb] = function () {
                delete w[cb];
                cb = null;
                loadCallback();
            };
            require([param + "&callback=" + cb]);
        }
    };
});