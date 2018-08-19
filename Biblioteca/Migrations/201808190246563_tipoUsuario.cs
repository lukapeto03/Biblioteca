namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tipoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "tipo", c => c.Int());
            DropColumn("dbo.Usuarios", "TipoUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "TipoUsuario", c => c.String());
            DropColumn("dbo.Usuarios", "tipo");
        }
    }
}
