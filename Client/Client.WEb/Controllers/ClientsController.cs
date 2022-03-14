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
using Newtonsoft.Json.Linq;

namespace Client.WEb.Controllers
{
    public class ClientsController : Controller
    {
        private ClientContext db = new ClientContext();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            //Test();
            var client = db.Client.Include(c => c.Genre1);
            return View(await client.ToListAsync());

        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client.WEb.Models.Client client = await db.Client.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
 

        // GET: Clients/Create
        public ActionResult Create()
        {

            ViewBag.Genre = new SelectList(db.Genre, "IdGenre", "NameGenre");
            return View();
        }

        // POST: Clients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdClient,DocumentClient,FullName,Email,Phone,Genre")] Client.WEb.Models.Client client)
        {
            if (ModelState.IsValid)
            {
                
                ServicePointManager.Expect100Continue = true;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string json = (new WebClient()).DownloadString("https://gorest.co.in/public/v2/users");

                var root = JToken.Parse(json);
                var filter = root.Where(x => (int?)x["id"] == Convert.ToInt16(client.DocumentClient)).ToList();


                bool exist = db.Client.Any(x=> x.DocumentClient == client.DocumentClient);

                if (!exist || filter.Count < 1)
                {

                    db.Client.Add(client);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                

            }

            ViewBag.Genre = new SelectList(db.Genre, "IdGenre", "NameGenre", client.Genre);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client.WEb.Models.Client client = await db.Client.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genre = new SelectList(db.Genre, "IdGenre", "NameGenre", client.Genre);
            return View(client);
        }

        // POST: Clients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdClient,DocumentClient,FullName,Email,Phone,Genre")] Client.WEb.Models.Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Genre = new SelectList(db.Genre, "IdGenre", "NameGenre", client.Genre);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client.WEb.Models.Client client = await db.Client.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client.WEb.Models.Client client = await db.Client.FindAsync(id);
            db.Client.Remove(client);
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
