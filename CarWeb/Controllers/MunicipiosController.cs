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
    public class MunicipiosController : Controller
    {
        private ModelCarWeb db = new ModelCarWeb();

        // GET: Municipios
        public async Task<ActionResult> Index()
        {
            return View(await db.Municipios.ToListAsync());
        }

        // GET: Municipios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = await db.Municipios.FindAsync(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            ViewBag.uf = new SelectList(new[]
       {
              new { Valor = 1, Text = "BA" },
              new { Valor = 2, Text = "TO" },

            }, "Text", "Text");

            ViewData["DropLote"] = new SelectList(db.Lotes, "idlote", "lote");

            return View();
        }

        // POST: Municipios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idmunicipio,idlote,municipio,uf")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Municipios.Add(municipios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(municipios);
        }

        // GET: Municipios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = await db.Municipios.FindAsync(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }

            ViewBag.uf = new SelectList(new[]
      {
              new { Valor = 1, Text = "BA" },
              new { Valor = 2, Text = "TO" },

            }, "Text", "Text");

            ViewBag.idlote = new SelectList(db.Lotes, "idlote", "lote").OrderBy(l => l.Text);

            return View(municipios);
        }

        // POST: Municipios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idmunicipio,idlote,municipio,uf")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(municipios);
        }

        // GET: Municipios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = await db.Municipios.FindAsync(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Municipios municipios = await db.Municipios.FindAsync(id);
            db.Municipios.Remove(municipios);
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
        public ActionResult Index(string pesquisaMunicipio)
        {

            List<Municipios> consultass;
            if (string.IsNullOrEmpty(pesquisaMunicipio))
            {
                consultass = db.Municipios.ToList();
            }
            else
            {
                consultass = db.Municipios.Where(m => m.municipio.StartsWith(pesquisaMunicipio)).ToList();
            }
            return View(consultass);


        }
    }
}
