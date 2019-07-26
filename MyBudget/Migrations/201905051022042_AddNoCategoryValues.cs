namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoCategoryValues : DbMigration
    {
        public override void Up()
        {
            Sql(
                "SET IDENTITY_INSERT Categories ON " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(30,N'Без категории','false','true')"
                );
            Sql(
                "SET IDENTITY_INSERT Categories ON " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(31,N'Без категории','true','true')"
                );
        }
        
        public override void Down()
        {
        }
    }
}
