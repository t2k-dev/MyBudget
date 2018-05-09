namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBaseCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Зарплата','false','glyphicon glyphicon-calendar')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Возврат долга','false','glyphicon glyphicon-cloud-upload')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Депозит','false','glyphicon glyphicon-piggy-bank')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Бизнес','false','glyphicon glyphicon-knight')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Остаток','false','glyphicon glyphicon-share')");


            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Здоровье','true','glyphicon glyphicon-heart')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Жилье','true','glyphicon glyphicon-home')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Авто','true','glyphicon glyphicon-font')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Транспорт','true','glyphicon glyphicon-road')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Спорт','true','glyphicon glyphicon-flag')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Еда и продукты','true','glyphicon glyphicon-apple')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Заведения','true','glyphicon glyphicon-cutlery')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Развлечения','true','glyphicon glyphicon-glass')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Одежда','true','glyphicon glyphicon-sunglasses')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Кредит','true','glyphicon glyphicon-piggy-bank')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Выплата долга','true','glyphicon glyphicon-save-file')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Одолжить','true','glyphicon glyphicon-cloud-download')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Связь','true','glyphicon glyphicon-phone-alt')");
            Sql("INSERT INTO Categories(Name,IsSpendingCategory,Icon) VALUES(N'Накопление','true','glyphicon glyphicon-briefcase')");

        }

        public override void Down()
        {
        }
    }
}
