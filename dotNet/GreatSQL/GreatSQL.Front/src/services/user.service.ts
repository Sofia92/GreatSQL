import {Injectable} from 'angular2/core';

import {User, Group, Rule} from '../models/models';

@Injectable()
export class UserService {
    getUsers():User[] {
        let neil = new User();
        neil.ID = 1;
        neil.Name = 'shen yun';
        neil.Email = 'neil.shen@voyageone.cn';
        neil.Password = 'neil';
        neil.RuleGroupID = 1;
        neil.RuleGroup = this.getGroup({id: neil.RuleGroupID});

        let james = new User();
        james.ID = 2;
        james.Name = 'li jun';
        james.Email = 'james.li@voyageone.cn';
        james.Password = 'james';
        james.RuleGroupID = 2;
        james.RuleGroup = this.getGroup({id: james.RuleGroupID});

        return [neil, james];
    }

    getGroups():Group[] {
        let admins = new Group();
        admins.ID = 1;
        admins.Name = "Administrator";
        admins.Rule = Rule.User | Rule.ReadAllLog;

        let developers = new Group();
        developers.ID = 2;
        developers.Name = "Developer";
        developers.Rule = Rule.CreateSql | Rule.ReadLog;

        return [admins, developers];
    }

    getGroup({id}):Group {
        return this.getGroups().find(g => g.ID == id);
    }

    getUser({id}:{id:Number}):User {
        return this.getUsers().find(u => u.ID === id);
    }
}