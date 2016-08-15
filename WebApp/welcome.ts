/// <reference path="typings/toastr.d.ts" />

import {autoinject} from 'aurelia-framework';

@autoinject
export class Welcome {
    activate() {
        toastr.info('Loading...');
    }
}