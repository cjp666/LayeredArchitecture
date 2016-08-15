"use strict";
var App = (function () {
    function App() {
    }
    App.prototype.configureRouter = function (config, router) {
        this.router = router;
        config.map([
            { route: ['', 'welcome'], moduleId: 'welcome', title: 'Welcome', nav: true, name: 'welcome' },
            { route: 'about', moduleId: 'about/about', title: 'About', nav: true }
        ]);
    };
    return App;
}());
exports.App = App;
//# sourceMappingURL=app.js.map