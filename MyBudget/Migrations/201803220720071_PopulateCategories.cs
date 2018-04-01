namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories VALUES(1,N'Еда')");
            Sql("INSERT INTO Categories VALUES(2,N'Авто')");
            Sql("INSERT INTO Categories VALUES(3,N'Здоровье')");
            Sql("INSERT INTO Categories VALUES(4,N'Спорт')");
            Sql("INSERT INTO Categories VALUES(5,N'Отдых и развлечения')");
            Sql("INSERT INTO Categories VALUES(6,N'Жильё')");
            Sql("INSERT INTO Categories VALUES(7,N'Одежда')");
            Sql("INSERT INTO Categories VALUES(8,N'Прочее')");
        }
        
        public override void Down()
        {
        }
    }
}
