namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories VALUES(1,N'���')");
            Sql("INSERT INTO Categories VALUES(2,N'����')");
            Sql("INSERT INTO Categories VALUES(3,N'��������')");
            Sql("INSERT INTO Categories VALUES(4,N'�����')");
            Sql("INSERT INTO Categories VALUES(5,N'����� � �����������')");
            Sql("INSERT INTO Categories VALUES(6,N'�����')");
            Sql("INSERT INTO Categories VALUES(7,N'������')");
            Sql("INSERT INTO Categories VALUES(8,N'������')");
        }
        
        public override void Down()
        {
        }
    }
}
