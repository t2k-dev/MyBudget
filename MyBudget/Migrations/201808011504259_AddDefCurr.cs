namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefCurr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DefCurrency", c => c.String(maxLength: 3));
            AddColumn("dbo.AspNetUsers", "CarryoverRests", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CarryoverRests");
            DropColumn("dbo.AspNetUsers", "DefCurrency");
        }
    }
}
