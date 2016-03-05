namespace GreatSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rule = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SqlItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Record = c.Int(nullable: false),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false),
                        RunTime = c.DateTime(nullable: false),
                        ElapsedTime = c.Time(nullable: false, precision: 7),
                        Creater_ID = c.Int(),
                        Runner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Creater_ID)
                .ForeignKey("dbo.Users", t => t.Runner_ID)
                .Index(t => t.Creater_ID)
                .Index(t => t.Runner_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        RuleGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.RuleGroupID, cascadeDelete: true)
                .Index(t => t.RuleGroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SqlItems", "Runner_ID", "dbo.Users");
            DropForeignKey("dbo.SqlItems", "Creater_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "RuleGroupID", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "RuleGroupID" });
            DropIndex("dbo.SqlItems", new[] { "Runner_ID" });
            DropIndex("dbo.SqlItems", new[] { "Creater_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.SqlItems");
            DropTable("dbo.Groups");
        }
    }
}
