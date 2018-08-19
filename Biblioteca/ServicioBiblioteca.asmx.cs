using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Biblioteca
{
    /// <summary>
    /// Descripción breve de ServicioBiblioteca
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioBiblioteca : System.Web.Services.WebService
    {
        private LibroDBContext db = new LibroDBContext();
       
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public List<Libro> buscarLibros(String busquedalibros, string tipo, string area, string autor)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var libros = from m in db.Libros select m;
            if (!string.IsNullOrEmpty(busquedalibros))
            {
                libros = libros.Where(s => s.Nombre.Contains(busquedalibros));
            }
            if (!string.IsNullOrEmpty(tipo))
            {
                var tipoI = (Int32.Parse(tipo));

                libros = libros.Where(s => ((int)s.Tipo) == tipoI);
            }
            if (!string.IsNullOrEmpty(area))
            {
                libros = libros.Where(s => s.Area.Contains(area));
            }
            if (!string.IsNullOrEmpty(autor))
            {
                libros = libros.Where(s => s.Autor.Contains(autor));
            }

            return new List<Libro>(libros);
        }
    }
}
