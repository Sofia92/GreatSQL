import {Component} from 'angular2/core';
import {ROUTER_DIRECTIVES} from 'angular2/router';

import {User} from '../../models/models';
import {UserService} from '../../services/user.service';

@Component({
    selector: 'users.gs-com',
    templateUrl: 'user/list.html',
    providers: [UserService],
    directives: [ROUTER_DIRECTIVES]
})
export default class UserListComponent {
    users:User[];

    constructor(private userService: UserService) {
        this.users = userService.getUsers();
    }
}
