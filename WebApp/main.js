"use strict";
function configure(aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();
    aurelia.start()
        .then(function (a) { return a.setRoot(); });
}
exports.configure = configure;
//# sourceMappingURL=main.js.map