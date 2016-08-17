"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var aurelia_http_client_1 = require('aurelia-http-client');
var aurelia_framework_1 = require('aurelia-framework');
var Authentication = (function () {
    function Authentication(httpClient) {
        this._httpClient = httpClient;
        this._isAuthenticated = false;
    }
    Object.defineProperty(Authentication.prototype, "isAuthenticated", {
        get: function () {
            return this._isAuthenticated;
        },
        enumerable: true,
        configurable: true
    });
    Authentication.prototype.login = function (username, password) {
        this._isAuthenticated = true;
        var p = new Promise(function (resolve, reject) {
            resolve(true);
        });
        return p;
    };
    Authentication.prototype.logout = function () {
        this._isAuthenticated = false;
        var p = new Promise(function (resolve, reject) {
            resolve(true);
        });
        return p;
    };
    Authentication = __decorate([
        aurelia_framework_1.inject(aurelia_http_client_1.HttpClient), 
        __metadata('design:paramtypes', [Object])
    ], Authentication);
    return Authentication;
}());
exports.Authentication = Authentication;
//# sourceMappingURL=authentication.js.map