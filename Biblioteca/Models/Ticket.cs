using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public enum Estado
    {
        Abierto, Cerrado
    }

    public class Ticket
    {
        [Key]
        public int cod_ticket { get; set; }
        public int cod_reserva { get; set; }
        public Estado? estado { get; set; }


    }
    public class TicketDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
}