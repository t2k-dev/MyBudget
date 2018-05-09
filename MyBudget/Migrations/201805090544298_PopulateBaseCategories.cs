namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBaseCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'��������','false','glyphicon glyphicon-calendar')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'������� �����','false','glyphicon glyphicon-cloud-upload')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�������','false','glyphicon glyphicon-piggy-bank')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'������','false','glyphicon glyphicon-knight')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�������','false','glyphicon glyphicon-share')");


            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'��������','true','glyphicon glyphicon-heart')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�����','true','glyphicon glyphicon-home')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'����','true','glyphicon glyphicon-font')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'���������','true','glyphicon glyphicon-road')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�����','true','glyphicon glyphicon-flag')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'��� � ��������','true','glyphicon glyphicon-apple')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'���������','true','glyphicon glyphicon-cutlery')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�����������','true','glyphicon glyphicon-glass')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'������','true','glyphicon glyphicon-sunglasses')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'������','true','glyphicon glyphicon-piggy-bank')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'������� �����','true','glyphicon glyphicon-save-file')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'��������','true','glyphicon glyphicon-cloud-download')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'�����','true','glyphicon glyphicon-phone-alt')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'����������','true','glyphicon glyphicon-briefcase')");

        }

        public override void Down()
        {
        }
    }
}
