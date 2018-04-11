namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Set_GoalDate_Null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goals", "CompleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goals", "CompleteDate", c => c.DateTime(nullable: false));
        }
    }
}
