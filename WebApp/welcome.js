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
var authentication_1 = require('./authentication');
var aurelia_framework_1 = require('aurelia-framework');
var Welcome = (function () {
    function Welcome(authentication, httpClient) {
        this.authentication = authentication;
    }
    Welcome.prototype.activate = function () {
        this.loginUsername = '';
        this.loginPassword = '';
        this.registerUsername = '';
        this.registerPassword = '';
    };
    Welcome.prototype.performLogin = function () {
        if (this.loginUsername === '' || this.loginPassword === '') {
            toastr.error('Username and password are required');
            return;
        }
        toastr.info('logging in...');
        this.authentication
            .login(this.loginUsername, this.loginPassword)
            .then(this.loggedIn);
    };
    Welcome.prototype.loggedIn = function () {
        toastr.success('logged in...');
    };
    Welcome.prototype.performRegister = function () {
        if (this.loginUsername === '' || this.loginPassword === '') {
            toastr.error('Enter username and password to register');
            return;
        }
        toastr.info('registering...');
    };
    Welcome.prototype.performLogout = function () {
        toastr.info('logging out...');
        this.authentication
            .logout()
            .then(this.loggedOut);
    };
    Welcome.prototype.loggedOut = function () {
        toastr.success('logged out');
    };
    Welcome = __decorate([
        aurelia_framework_1.inject(authentication_1.Authentication), 
        __metadata('design:paramtypes', [Object, Object])
    ], Welcome);
    return Welcome;
}());
exports.Welcome = Welcome;
//# sourceMappingURL=welcome.js.map