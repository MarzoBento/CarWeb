using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarWeb.Models;

namespace CarWeb.Controllers
{
    [Authorize]
    public class LotesController : Controller
    {
        private ModelCarWeb db = new ModelCarWeb();

        // GET: Lotes
        public async Task<ActionResult> Index()
        {
            return View(await db.Lotes.ToListAsync());
        }

        // GET: Lotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // GET: Lotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lotes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idlote,lote")] Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                db.Lotes.Add(lotes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lotes);
        }

        // GET: Lotes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // POST: Lotes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idlote,lote")] Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lotes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lotes);
        }

        // GET: Lotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = await db.Lotes.FindAsync(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lotes lotes = await db.Lotes.FindAsync(id);
            db.Lotes.Remove(lotes);
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

        [HttpPost]
        public ActionResult Index(string pesquisaLote)
        {

            List<Lotes> consultass;
            if (string.IsNullOrEmpty(pesquisaLote))
            {
                consultass = db.Lotes.ToList();
            }
            else
            {
                consultass = db.Lotes.Where(l => l.lote.StartsWith(pesquisaLote)).ToList();
            }
            return View(consultass);


        }
    }
}
