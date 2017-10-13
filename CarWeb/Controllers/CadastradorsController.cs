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
    public class CadastradorsController : Controller
    {
        private ModelCarWeb db = new ModelCarWeb();

        // GET: Cadastradors
        public async Task<ActionResult> Index()
        {
            return View(await db.Cadastrador.ToListAsync());
        }

        // GET: Cadastradors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastrador cadastrador = await db.Cadastrador.FindAsync(id);
            if (cadastrador == null)
            {
                return HttpNotFound();
            }
            return View(cadastrador);
        }

        // GET: Cadastradors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cadastradors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idcadastrador,cadastrador1")] Cadastrador cadastrador)
        {
            if (ModelState.IsValid)
            {
                db.Cadastrador.Add(cadastrador);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cadastrador);
        }

        // GET: Cadastradors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastrador cadastrador = await db.Cadastrador.FindAsync(id);
            if (cadastrador == null)
            {
                return HttpNotFound();
            }
            return View(cadastrador);
        }

        // POST: Cadastradors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idcadastrador,cadastrador1")] Cadastrador cadastrador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadastrador).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cadastrador);
        }

        // GET: Cadastradors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastrador cadastrador = await db.Cadastrador.FindAsync(id);
            if (cadastrador == null)
            {
                return HttpNotFound();
            }
            return View(cadastrador);
        }

        // POST: Cadastradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cadastrador cadastrador = await db.Cadastrador.FindAsync(id);
            db.Cadastrador.Remove(cadastrador);
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
        public ActionResult Index(string pesquisaCadastrador)
        {

            List<Cadastrador> consultass;
            if (string.IsNullOrEmpty(pesquisaCadastrador))
            {
                consultass = db.Cadastrador.ToList();
            }
            else
            {
                consultass = db.Cadastrador.Where(c => c.cadastrador1.StartsWith(pesquisaCadastrador)).ToList();
            }
            return View(consultass);


        }
    }
}
