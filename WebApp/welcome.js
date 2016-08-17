"use strict";
var Welcome = (function () {
    function Welcome() {
    }
    Welcome.prototype.activate = function () {
        console.log('welcome loading...');
        toastr.info('welcome Loading...');
    };
    return Welcome;
}());
exports.Welcome = Welcome;
//# sourceMappingURL=welcome.js.map