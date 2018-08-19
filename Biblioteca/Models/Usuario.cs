using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public enum Tipo
    {
        Estudiante, Egresado, Docente, Funcionario
    }

    public class Usuario
    {
        [Key]
        public int cod_usuario { get; set; }
        public int Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public Tipo? tipo { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
    public class UsuarioDBContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}