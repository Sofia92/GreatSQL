namespace GreatSQL.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    using GreatSQL.Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<GreatSQL.Models.GreatSQLContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GreatSQL.Models.GreatSQLContext context)
        {
            context.Groups.AddOrUpdate(
                g => g.ID,
                new Group() { ID = 1, Name = "Admin", Rule = (int)(Rule.User) },
                new Group() { ID = 2, Name = "DBA", Rule = (int)(Rule.CreateSql | Rule.ReadAllLog | Rule.RunSql) },
                new Group() { ID = 3, Name = "Default", Rule = (int)(Rule.CreateSql | Rule.ReadLog) }
            );

            context.Users.AddOrUpdate(
                u => u.ID,
                new User() { ID = 1, Email = "admin@voyageone.cn", Name = "admin", Password = "admin", RuleGroupID = 1 },
                new User() { ID = 2, Email = "neil@voyageone.cn", Name = "neil", Password = "1", RuleGroupID = 2 },
                new User() { ID = 3, Email = "james@voyageone.cn", Name = "james", Password = "2", RuleGroupID = 3 }
            );
        }
    }
}
