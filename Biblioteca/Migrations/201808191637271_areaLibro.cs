namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class areaLibro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Libroes", "Area", c => c.String());
            CreateIndex("dbo.Usuarios", "Cedula", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuarios", new[] { "Cedula" });
            DropColumn("dbo.Libroes", "Area");
        }
    }
}
