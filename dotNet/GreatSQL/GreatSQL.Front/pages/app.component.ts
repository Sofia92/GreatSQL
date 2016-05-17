import {Component, ViewEncapsulation} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES} from 'angular2/router';

import SqlListComponent from './sql/sql.list.component';
import SqlDetailComponent from './sql/sql.detail.component';
import UserListComponent from './user/user.list.component';
import UserDetailComponent from './user/user.detail.component';

@Component({
    selector: 'app.gs-com.container-fluid',
    templateUrl: 'app.html',
    styleUrls: ['../styles/app.css'],
    encapsulation: ViewEncapsulation.None,
    directives: [ROUTER_DIRECTIVES]
})
@RouteConfig([
    {
        path: '/sqls',
        name: 'SqlList',
        component: SqlListComponent,
        useAsDefault: true,
    },
    {
        path: '/sql/:id',
        name: 'SqlDetail',
        component: SqlDetailComponent,
    },
    {
        path: '/users',
        name: 'UserList',
        component: UserListComponent,
    },
    {
        path: '/user/:id',
        name: 'UserDetail',
        component: UserDetailComponent,
    }
])
export default class AppComponent {
}
