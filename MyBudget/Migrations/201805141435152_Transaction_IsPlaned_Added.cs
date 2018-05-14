namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction_IsPlaned_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "IsPlaned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "IsPlaned");
        }
    }
}
