namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBaseCategories : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT dbo.Categories ON " +                
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(1, N'��� ���������', 'false','true');" +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(2, N'��� ���������', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(3, N'����� � ����', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(4, N'���� � ����', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(5, N'�������', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(6, N'������ ����', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(7, N'������� �����', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(8, N'���������� ����', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(20, N'��������', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(21, N'������', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(22, N'������ � ��������', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(23, N'��������', 'true','false')  " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(24, N'�����', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(25, N'����', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(26, N'���������', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(27, N'�����', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(28, N'��� � ��������', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(29, N'�����, ����', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(30, N'�����������', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(31, N'������', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(32, N'�����', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(40, N'�������', 'true','false'); "
                );
        }

        public override void Down()
        {
            Sql("DELETE FROM Categories");
        }
    }
}
