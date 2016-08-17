
import {Router} from 'aurelia-router';

export class App {
    router: Router;

    configureRouter(config, router: Router) {
        this.router = router;

        config.map([
            { route: ['', 'welcome'], moduleId: 'welcome', title: 'Welcome', nav: true, name: 'welcome' },
            { route: 'about', moduleId: 'about/about', title: 'About', nav: true }
            // { route: 'details/:id', moduleId: 'movies/details', title: 'Details', nav: false, name: 'details' },
            // { route: 'edit/:id', moduleId: 'movies/edit', title: 'Edit', nav: false, name: 'edit' },
            // { route: 'create', moduleId: 'movies/edit', title: 'Create', nav: false, name: 'create' }
        ]);
    }
}
