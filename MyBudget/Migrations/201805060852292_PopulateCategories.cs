namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(1,N'Зарплата','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(2,N'Возврат долга','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(3,N'Депозит','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(4,N'Бизнес','false')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(5,N'Остаток','false')");


            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(6,N'Здоровье','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(7,N'Жилье','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(8,N'Авто','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(9,N'Транспорт','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(10,N'Спорт','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(11,N'Еда и продукты','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(12,N'Заведения','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(13,N'Развлечения','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(14,N'Одежда','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(15,N'Кредит','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(16,N'Выплата долга','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(17,N'Связь','true')");
            Sql("INSERT INTO Categories(Id,Name,IsSpendingCategory) VALUES(18,N'Накопление','true')");            
        }

        public override void Down()
        {
        }
    }
}
