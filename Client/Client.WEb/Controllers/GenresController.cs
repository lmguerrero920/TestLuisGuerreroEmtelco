using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Client.WEb.Models;

namespace Client.WEb.Controllers
{
    public class GenresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genres
        public async Task<ActionResult> Index()
        {
            return View(await db.Genres.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdGenre,NameGenre")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdGenre,NameGenre")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Genre genre = await db.Genres.FindAsync(id);
            db.Genres.Remove(genre);
            await db.SaveChangesAsync();
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
