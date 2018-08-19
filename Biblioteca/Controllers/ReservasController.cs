using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class ReservasController : Controller
    {
        private ReservaDBContext db = new ReservaDBContext();

        // GET: Reservas
        /*public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Libro).Include(r => r.Usuario);
            return View(reservas.ToList());
        }*/

        public ActionResult Index(String busquedaReservas)
        {
            var reserva = from m in db.Reservas select m;
            if (!string.IsNullOrEmpty(busquedaReservas))
            {
                reserva = reserva.Where(s => (s.cod_reserva + "").Contains(busquedaReservas));
            }
            return View(reserva.Include(r => r.Libro).Include(r => r.Usuario));
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {   
            //0 significa que el libro es de tipo Normal por lo tanto se puede prestar
            //1 significa que el libro es de tipo reserva por lo tanto no se puede prestar porque debe reposar siempre dentro de la Biblioteca         
            ViewBag.cod_libro = new SelectList(db.Libroes.Where(l => l.Stock > 0 && l.Tipo == 0), "cod_libro", "Nombre");
            ViewBag.cod_usuario = new SelectList(db.Usuarios.Where(u => u.estado == EstadoUsuario.Activo), "cod_usuario", "NombreCompleto");
            return View();
        }

        public ActionResult Cerrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }

            String mensaje = "La devolución se ha registrado exitosamente";

            Libro libro = db.Libroes.Find(reserva.cod_libro);

            libro.Stock = libro.Stock + 1;
            db.SaveChanges();

            reserva.FechaDevolucion = DateTime.Today;
            db.SaveChanges();

            if (DateTime.Today > (reserva.FechaReserva.AddDays(reserva.DiasPrestamo)) )
            {
                Usuario usuario = db.Usuarios.Find(reserva.cod_usuario);
                usuario.estado = EstadoUsuario.Inactivo;
                mensaje = "El usuario ha quedado en estado inactivo";
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { mensaje = mensaje });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_reserva,cod_libro,cod_usuario,FechaReserva,FechaDevolucion,DiasPrestamo")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                Libro libro = db.Libroes.Find(reserva.cod_libro);
                if ( libro == null || libro.Stock < 1)
                {
                    return RedirectToAction("Create", new { mensaje = "El libro no tiene stock" });
                }
                    
                db.Reservas.Add(reserva);
                db.SaveChanges();
                libro.Stock = libro.Stock - 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_libro = new SelectList(db.Libroes, "cod_libro", "Nombre", reserva.cod_libro);
            ViewBag.cod_usuario = new SelectList(db.Usuarios, "cod_usuario", "NombreCompleto", reserva.cod_usuario);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_libro = new SelectList(db.Libroes, "cod_libro", "Nombre", reserva.cod_libro);
            ViewBag.cod_usuario = new SelectList(db.Usuarios, "cod_usuario", "NombreCompleto", reserva.cod_usuario);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_reserva,cod_libro,cod_usuario,FechaReserva,FechaDevolucion,DiasPrestamo")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_libro = new SelectList(db.Libroes, "cod_libro", "Nombre", reserva.cod_libro);
            ViewBag.cod_usuario = new SelectList(db.Usuarios, "cod_usuario", "NombreCompleto", reserva.cod_usuario);
            return View(reserva);
        }


        // GET: Reservas/Imprimir/5
        public ActionResult Imprimir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index", new { ticketImpreso = id });
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
