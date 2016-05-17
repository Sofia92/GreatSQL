import {Component} from 'angular2/core';
import {RouteParams} from "angular2/router";

import {User, Group} from '../../models/models';
import {UserService} from '../../services/user.service';

@Component({
    selector: 'user.gs-com',
    templateUrl: 'user/detail.html',
    providers: [UserService]
})
export default class UserDetailComponent {
    user: User;
    groups: Group[];

    constructor(params: RouteParams, private userService: UserService) {
        let id = params.get("id");
        this.user = userService.getUser({id: parseInt(id)});
        this.groups = userService.getGroups();
    }
}
