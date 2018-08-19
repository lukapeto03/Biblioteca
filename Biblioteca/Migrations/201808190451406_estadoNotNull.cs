namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estadoNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "estado", c => c.Int());
        }
    }
}
