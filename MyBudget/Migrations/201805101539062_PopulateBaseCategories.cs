namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBaseCategories : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT dbo.Categories ON " +                
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(1, N'Без категории', 'false','true');" +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(2, N'Без категории', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(3, N'Взять в долг', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(4, N'Дать в долг', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(5, N'Остаток', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(6, N'Отдать долг', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(7, N'Выплата долга', 'false','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(8, N'Пополнение цели', 'true','true'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(20, N'Зарплата', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(21, N'Бизнес', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(22, N'Снятие с депозита', 'false','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(23, N'Здоровье', 'true','false')  " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(24, N'Жилье', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(25, N'Авто', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(26, N'Транспорт', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(27, N'Спорт', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(28, N'Еда и продукты', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(29, N'Клубы, бары', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(30, N'Развлечения', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(31, N'Одежда', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(32, N'Связь', 'true','false'); " +
                "INSERT INTO Categories(Id,Name,IsSpendingCategory,IsSystem) VALUES(40, N'Подарки', 'true','false'); "
                );
        }

        public override void Down()
        {
            Sql("DELETE FROM Categories");
        }
    }
}
