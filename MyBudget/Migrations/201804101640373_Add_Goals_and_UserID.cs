namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Goals_and_UserID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalName = c.String(nullable: false),
                        Type = c.Byte(nullable: false),
                        Amount = c.Double(nullable: false),
                        CurAmount = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CompleteDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Transactions", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Transactions", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Transactions", "UserId");
            AddForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Goals", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropIndex("dbo.Goals", new[] { "UserId" });
            AlterColumn("dbo.Transactions", "Name", c => c.String());
            DropColumn("dbo.Transactions", "UserId");
            DropTable("dbo.Goals");
        }
    }
}
