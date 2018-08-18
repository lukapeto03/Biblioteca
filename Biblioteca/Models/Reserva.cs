using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Reserva
    {
        [Key]
        public int cod_reserva { get; set; }
        public int cod_libro { get; set; }
        public int cod_usuario { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int DiasPrestamo { get; set; }

        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
    public class ReservaDBContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }

        public System.Data.Entity.DbSet<Biblioteca.Models.Libro> Libroes { get; set; }

        public System.Data.Entity.DbSet<Biblioteca.Models.Usuario> Usuarios { get; set; }
    }
}