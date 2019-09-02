namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemplateAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Double(nullable: false),
                        Day = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsSpending = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Templates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Templates", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Templates", new[] { "UserId" });
            DropIndex("dbo.Templates", new[] { "CategoryId" });
            DropTable("dbo.Templates");
        }
    }
}
