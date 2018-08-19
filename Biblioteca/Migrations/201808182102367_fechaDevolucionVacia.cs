namespace Biblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class fechaDevolucionVacia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                            "dbo.Reservas",
                            c => new
                            {
                                cod_reserva = c.Int(nullable: false, identity: true),
                                cod_libro = c.Int(nullable: false),
                                cod_usuario = c.Int(nullable: false),
                                FechaReserva = c.DateTime(nullable: false),
                                FechaDevolucion = c.DateTime(),
                                DiasPrestamo = c.Int(nullable: false),
                            })
                            .PrimaryKey(t => t.cod_reserva)
                            .ForeignKey("dbo.Libroes", t => t.cod_libro, cascadeDelete: true)
                            .ForeignKey("dbo.Usuarios", t => t.cod_usuario, cascadeDelete: true)
                            .Index(t => t.cod_libro)
                            .Index(t => t.cod_usuario);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "cod_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Reservas", "cod_libro", "dbo.Libroes");
            DropIndex("dbo.Reservas", new[] { "cod_usuario" });
            DropIndex("dbo.Reservas", new[] { "cod_libro" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Reservas");
            DropTable("dbo.Libroes");
        }
    }
}
