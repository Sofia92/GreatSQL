import { bootstrap } from 'angular2/platform/browser';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
import { Component } from 'angular2/core';

import LoginComponent from './login.component';
import AppComponent from './app.component';

@Component({
  selector: 'greatsql.gs-com',
  template: '<router-outlet></router-outlet>',
  directives: [ROUTER_DIRECTIVES],
  providers: [ROUTER_PROVIDERS]
})
@RouteConfig([
  {
    path: '/login',
    name: 'Login',
    component: LoginComponent,
    useAsDefault: true,
  },
  {
    path: '/great/...',
    name: 'App',
    component: AppComponent
  }
])
class GreatSqlComponent {}

bootstrap(GreatSqlComponent);
