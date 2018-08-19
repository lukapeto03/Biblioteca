namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estadoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "estado", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "estado");
        }
    }
}
