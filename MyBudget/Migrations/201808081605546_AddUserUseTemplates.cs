namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserUseTemplates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UseTemplates", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "UpdateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UpdateDate");
            DropColumn("dbo.AspNetUsers", "UseTemplates");
        }
    }
}
