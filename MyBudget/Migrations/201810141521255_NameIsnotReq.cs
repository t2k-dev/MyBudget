namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameIsnotReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Name", c => c.String(nullable: false));
        }
    }
}
