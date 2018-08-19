namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class defaultEstado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "estado", c => c.Int(defaultValue: 0));
        }
        
        public override void Down()
        {
        }
    }
}
