import {Authentication} from './authentication'
import {inject} from 'aurelia-framework';

@inject(Authentication)
export class Welcome {
    loginUsername: string;
    loginPassword: string;

    registerUsername: string;
    registerPassword: string;

    authentication: any;

    constructor(authentication, httpClient) {
        this.authentication = authentication;
    }

    activate() {
        this.loginUsername = '';
        this.loginPassword = '';
        this.registerUsername = '';
        this.registerPassword = '';
    }

    performLogin() {
        if (this.loginUsername === '' || this.loginPassword === '') {
            toastr.error('Username and password are required');
            return;
        }
        toastr.info('logging in...');
        this.authentication
            .login(this.loginUsername, this.loginPassword)
            .then(this.loggedIn);
    }

    loggedIn() {
        toastr.success('logged in...');
    }

    performRegister() {
        if (this.loginUsername === '' || this.loginPassword === '') {
            toastr.error('Enter username and password to register');
            return;
        }
        toastr.info('registering...');
    }

    performLogout() {
        toastr.info('logging out...');
        this.authentication
            .logout()
            .then(this.loggedOut);
    }

    loggedOut() {
        toastr.success('logged out');
    }
}
