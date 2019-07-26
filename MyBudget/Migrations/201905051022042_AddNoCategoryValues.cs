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
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(30,N'��� ���������','false','true')"
                );
            Sql(
                "SET IDENTITY_INSERT Categories ON " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(31,N'��� ���������','true','true')"
                );
        }
        
        public override void Down()
        {
        }
    }
}
