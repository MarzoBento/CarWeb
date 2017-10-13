using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CarWeb.Models;
using System.IO.Compression;
using System.IO;

namespace CarWeb.Controllers
{
    public class DownloadController : Controller
    {
        private ModelCarWeb db = new ModelCarWeb();

        // GET: Download
        public ActionResult Index()
        {
            return View();

        }

        // GET: Download/Lote/003
        [HttpPost]
        public async Task<ActionResult> Index(string pesquisaLote)
        {
           // List<Lotes> lotes;

            if (string.IsNullOrEmpty(pesquisaLote))
            {
              
                return View();

                
            }

            var lote = await db.Lotes.Where(l => l.lote.ToString().ToLower().Contains(pesquisaLote))
                          .Include(a => a.Arquivoss).SingleOrDefaultAsync();

            //string[] files = new string[lote.Arquivoss.Count];

            List<string> downloads = new List<string>();

            foreach (var item in lote.Arquivoss)
            {
                //string file = Server.MapPath("~/Arquivos/") + item.nomearquivo;
                // string file = Path.GetFileName(Server.MapPath("~/Arquivos/") + item.nomearquivo);
                Uri uri = new Uri(Request.Url, Url.Content("~/Arquivos/" + item.nomearquivo));
                downloads.Add(uri.ToString());
            }


            return View(downloads);
        }

        public FileResult Baixar(int id)
        {
            Arquivos arquivo = db.Arquivos.Find(id);
           

            if (arquivo == null)
            {
                return null;
            }
            else
            {

                var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Arquivos"));
                System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
                List<string> selectedfiles = new List<string>();

                foreach (var file in fileNames)
                {
                    selectedfiles.Add(file.Name);
                }

                // var path = Path.Combine(Server.MapPath("~/Upload"), arquivo.Nome);

                return File( arquivo.caminho, System.Net.Mime.MediaTypeNames.Application.Octet, arquivo.caminho);

            }

        }

        //public FileResult Downloads(List<Object> id)
        //{
        //    Arquivos arquivo = db.Arquivos.Find(id);

        //    List<string> selectedfiles = new List<string>();

        //    if (arquivo == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {



        //        var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Arquivos"));
        //        System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");


        //        foreach (var file in fileNames)
        //        {
        //            selectedfiles.Add(file.Name);
        //        }
        //    }

        //    return File(arquivo.caminho, System.Net.Mime.MediaTypeNames.Application.Octet, arquivo.caminho);
        //}


        [HttpPost]
        public ActionResult Downloads(Lotes lote)
        {
            //if (System.IO.File.Exists(Server.MapPath
            //                  ("~/zipfiles/bundle.zip")))
            //{
            //    System.IO.File.Delete(Server.MapPath
            //                  ("~/zipfiles/bundle.zip"));
            //}

            //ZipArchive zip = ZipFile.Open(Server.MapPath
            //         ("~/zipfiles/bundle.zip"), ZipArchiveMode.Create);
            //foreach (string file in selectedfiles)
            //{
            //    zip.CreateEntryFromFile(Server.MapPath
            //         ("~/images/" + file), file);
            //}

            //zip.Dispose();
            //return File(Server.MapPath("~/zipfiles/bundle.zip"),
            //          "application/zip", "bundle.zip");
            return null;
        }


        // GET: Download/Details/5
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

        // GET: Download/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Download/Create
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

        // GET: Download/Edit/5
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

        // POST: Download/Edit/5
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

        // GET: Download/Delete/5
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

        // POST: Download/Delete/5
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
    }
}
