import {HttpClient} from 'aurelia-http-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export class Authentication {
    // http://localhost:9021/

    private _isAuthenticated: boolean;

    private _httpClient: any;

    constructor(httpClient) {
        this._httpClient = httpClient;
        this._isAuthenticated = false;
    }

    get isAuthenticated() {
        return this._isAuthenticated;
    }

    login(username: string, password: string): Promise<boolean> {
        this._isAuthenticated = true;
        let p = new Promise<boolean>(function (resolve, reject) {
            // TODO call the actual WebAPI login method 
            resolve(true);
        });
        return p;
    }

    logout(): Promise<boolean> {
        // TODO
        this._isAuthenticated = false;
         let p = new Promise<boolean>(function (resolve, reject) {
            // TODO call the actual WebAPI login method 
            resolve(true);
        });
        return p;
   }
}
