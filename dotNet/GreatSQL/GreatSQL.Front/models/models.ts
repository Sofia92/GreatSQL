export class User {
  ID: Number;
  Email: String;
  Name: String;
  Password: String;
  RuleGroupID: Number;
  RuleGroup: Group;
}

export class Group {
  ID: Number;
  Name: String;
  Rule: Number;
}

export class Sql {
  ID: Number;
  Body: String;
  Record: Number;
  Message: String;
  Creater_ID: Number;
  Runner_ID: Number;
  Created: Date;
  RunTime: Date;
  ElapsedTime: String;
  Runner: User;
  Creater: User;
}

export enum Rule {
  CreateSql = 1,
  ReadLog = 2,
  RunSql = 4,
  User = 8,
  ReadAllLog = 16,
  UpdateSql = 32
}
