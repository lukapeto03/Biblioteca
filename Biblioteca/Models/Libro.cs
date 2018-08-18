﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public enum Tipo
    {
        Normal, Reserva
    }
    public class Libro
    {
        [Key]
        public int cod_libro { get; set; }
        public string Nombre { get; set; }
        public string Autor { get; set; }
        public string Ubicacion { get; set; }
        public string Editorial { get; set; }
        public int Año { get; set; }
        public Tipo? Tipo { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
    public class LibroDBContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
    }
}