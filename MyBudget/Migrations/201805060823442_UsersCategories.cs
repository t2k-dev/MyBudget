namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserCategories",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Category_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Category_Id);
            
            AddColumn("dbo.Categories", "IsSpendingCategory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ApplicationUserCategories", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCategories", new[] { "Category_Id" });
            DropIndex("dbo.ApplicationUserCategories", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Categories", "Icon");
            DropColumn("dbo.Categories", "IsSpendingCategory");
            DropTable("dbo.ApplicationUserCategories");
        }
    }
}
