namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(1,N'��������','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(2,N'������� �����','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(3,N'�������','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(4,N'������','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(5,N'�������','false')");


            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(6,N'��������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(7,N'�����','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(8,N'����','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(9,N'���������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(10,N'�����','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(11,N'��� � ��������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(12,N'���������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(13,N'�����������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(14,N'������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(15,N'������','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(16,N'������� �����','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(17,N'�����','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(18,N'����������','true')");            
        }

        public override void Down()
        {
        }
    }
}
