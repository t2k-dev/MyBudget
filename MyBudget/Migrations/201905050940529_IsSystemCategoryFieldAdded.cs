namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSystemCategoryFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsSystem", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsSystem");
        }
    }
}
