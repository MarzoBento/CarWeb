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
using System.IO;

namespace CarWeb.Controllers
{
    public class CadastrosController : Controller
    {
        private ModelCarWeb db = new ModelCarWeb();
        private static int idarq;

        // GET: Cadastros
        public ActionResult Index()
        {
            var query = (from c in db.Cadastros orderby c.propietario descending select c).Skip(20).Take(20);

            return View(query);
        }

        // GET: Cadastros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastros cadastros = await db.Cadastros.FindAsync(id);
            if (cadastros == null)
            {
                return HttpNotFound();
            }
            return View(cadastros);
        }

        // GET: Cadastros/Create
        public ActionResult Create()
        {

            ViewBag.Coordenadas = new SelectList(new[]
      {
              new { Valor = 1, Text = "COM COORDENADAS" },
              new { Valor = 2, Text = "SEM COORDENADAS" },
            }, "Text", "Text");

            ViewBag.Google = new SelectList(new[]
             {
              new { Valor = 1, Text = "ESTÁ NO GOOGLE DRIVE" },
              new { Valor = 2, Text = "NÃO ESTÁ NO GOOGLE DRIVE" },
            }, "Text", "Text");


            ViewBag.Servico = new SelectList(new[]
{
              new { Valor = 1, Text = "EQUIPE GOIÂNIA" },
              new { Valor = 2, Text = "SALA TÉCNICA" },
              new { Valor = 3, Text = "ERRO DE LOCALIZAÇÃO" },
              new { Valor = 4, Text = "SOBREPOSIÇÃO" },
              new { Valor = 5, Text = "TRIAGEM" },
              new { Valor = 6, Text = "ARQUIVO" },
              new { Valor = 7, Text = "FINALIZADO" },
            }, "Text", "Text");


            ViewBag.cadastrador = new SelectList(db.Cadastrador, "cadastrador1", "cadastrador1");

            ViewBag.Emissao = new SelectList(new[]
               {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");


            ViewBag.Recibo = new SelectList(new[]
              {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");

            ViewData["DropMunicipio"] = new SelectList(db.Municipios, "Idmunicipio", "municipio");

            ViewData["DropLote"] = new SelectList(db.Lotes, "idlote", "lote");

            return View();
        }

        // POST: Cadastros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idcadastro,ncadastro,idlote,idmunicipio,propietario,cpf,fonecontato,propriedade,comunidade,area,statuscoodenada,statusgoogle,statusservico,observacao,cadastrador,dtcadastro,statusemissao,numerocarba,statusimpressao,dtentrega")] Cadastros cadastros, Arquivos arq)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.Cadastros.Add(cadastros);


                    byte[] data = null;

                    string nomeArquivo = "";

                    foreach (var arquivo in arq.Arquivoss)
                    {

                        if (arquivo.ContentLength > 0)
                        {
                            nomeArquivo = Path.GetFileName(arquivo.FileName);
                            var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivo);
                            arquivo.SaveAs(caminho);
                            FileStream fStream = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                            long numBytes = caminho.Length;
                            BinaryReader br = new BinaryReader(fStream);
                            data = br.ReadBytes((int)numBytes);
                            arq.caminho = caminho;
                            arq.idcadastro = cadastros.idcadastro;
                            arq.idlote = cadastros.idlote;
                            arq.cadastrador = cadastros.cadastrador;
                            arq.nomearquivo = nomeArquivo;
                        }

                        db.Arquivos.Add(arq);
                        db.SaveChanges();
                    }



                }
                catch
                {
                    ViewBag.Coordenadas = new SelectList(new[]
   {
              new { Valor = 1, Text = "COM COORDENADAS" },
              new { Valor = 2, Text = "SEM COORDENADAS" },
            }, "Text", "Text");

                    ViewBag.Google = new SelectList(new[]
                     {
              new { Valor = 1, Text = "ESTÁ NO GOOGLE DRIVE" },
              new { Valor = 2, Text = "NÃO ESTÁ NO GOOGLE DRIVE" },
            }, "Text", "Text");


                    ViewBag.Servico = new SelectList(new[]
        {
              new { Valor = 1, Text = "EQUIPE GOIÂNIA" },
              new { Valor = 2, Text = "SALA TÉCNICA" },
              new { Valor = 3, Text = "ERRO DE LOCALIZAÇÃO" },
              new { Valor = 4, Text = "SOBREPOSIÇÃO" },
              new { Valor = 5, Text = "TRIAGEM" },
              new { Valor = 6, Text = "ARQUIVO" },
              new { Valor = 7, Text = "FINALIZADO" },
            }, "Text", "Text");

                    ViewBag.Emissao = new SelectList(new[]
                     {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");


                    ViewBag.Recibo = new SelectList(new[]
                      {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");

                    ViewData["cadastrador"] = new SelectList(db.Cadastrador, "cadastrador1", "cadastrador1");

                    ViewData["DropMunicipio"] = new SelectList(db.Municipios, "Idmunicipio", "municipio");
                    return View(cadastros);

                }
            }
            else
            {
                ViewBag.Coordenadas = new SelectList(new[]
   {
              new { Valor = 1, Text = "COM COORDENADAS" },
              new { Valor = 2, Text = "SEM COORDENADAS" },
            }, "Text", "Text");

                ViewBag.Google = new SelectList(new[]
                 {
              new { Valor = 1, Text = "ESTÁ NO GOOGLE DRIVE" },
              new { Valor = 2, Text = "NÃO ESTÁ NO GOOGLE DRIVE" },
            }, "Text", "Text");


                ViewBag.Servico = new SelectList(new[]
    {
              new { Valor = 1, Text = "EQUIPE GOIÂNIA" },
              new { Valor = 2, Text = "SALA TÉCNICA" },
              new { Valor = 3, Text = "ERRO DE LOCALIZAÇÃO" },
              new { Valor = 4, Text = "SOBREPOSIÇÃO" },
              new { Valor = 5, Text = "TRIAGEM" },
              new { Valor = 6, Text = "ARQUIVO" },
              new { Valor = 7, Text = "FINALIZADO" },
            }, "Text", "Text");

                ViewBag.Emissao = new SelectList(new[]
                 {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");


                ViewBag.Recibo = new SelectList(new[]
                  {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");

                ViewBag.cadastrador = new SelectList(db.Cadastrador, "cadastrador1", "cadastrador1");

                ViewData["DropMunicipio"] = new SelectList(db.Municipios, "Idmunicipio", "municipio");

                ViewData["DropLote"] = new SelectList(db.Lotes, "idlote", "lote");


                db.Cadastros.Add(cadastros);
                db.SaveChanges();

                byte[] datab = null;

                string nomeArquivob = "";

                foreach (var arquivo in arq.Arquivoss)
                {

                    if (arquivo.ContentLength > 0)
                    {
                        nomeArquivob = Path.GetFileName(arquivo.FileName);
                        var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivob);
                        arquivo.SaveAs(caminho);
                        FileStream fStream = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                        long numBytes = caminho.Length;
                        BinaryReader br = new BinaryReader(fStream);
                        datab = br.ReadBytes((int)numBytes);
                        arq.caminho = caminho;
                        arq.idcadastro = cadastros.idcadastro;
                        arq.idlote = cadastros.idlote;
                        arq.cadastrador = cadastros.cadastrador;
                        arq.nomearquivo = nomeArquivob;
                    }

                    db.Arquivos.Add(arq);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        // GET: Cadastros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastros cadastros = await db.Cadastros.FindAsync(id);
            if (cadastros == null)
            {
                return HttpNotFound();
            }
            idarq = Convert.ToInt32(id);



           ViewBag.idlote = new SelectList(db.Lotes, "idlote", "lote").OrderBy(l => l.Text);
            ViewBag.idmunicipio = new SelectList(db.Municipios, "idmunicipio", "municipio").OrderBy(m => m.Text);
            ViewBag.idpropietario = new SelectList(db.Propietario, "idpropietario", "propietario1").OrderBy(p => p.Text);
            ViewBag.cadastrador = new SelectList(db.Cadastrador, "cadastrador1", "cadastrador1").OrderBy(c => c.Text);

            ViewBag.Coordenadas = new SelectList(new[]
       {
              new { Valor = 1, Text = "COM COORDENADAS" },
              new { Valor = 2, Text = "SEM COORDENADAS" },
            }, "Text", "Text");

            ViewBag.Google = new SelectList(new[]
             {
              new { Valor = 1, Text = "ESTÁ NO GOOGLE DRIVE" },
              new { Valor = 2, Text = "NÃO ESTÁ NO GOOGLE DRIVE" },
            }, "Text", "Text");


            ViewBag.Servico = new SelectList(new[]
{
              new { Valor = 1, Text = "EQUIPE GOIÂNIA" },
              new { Valor = 2, Text = "SALA TÉCNICA" },
              new { Valor = 3, Text = "ERRO DE LOCALIZAÇÃO" },
              new { Valor = 4, Text = "SOBREPOSIÇÃO" },
              new { Valor = 5, Text = "TRIAGEM" },
              new { Valor = 6, Text = "ARQUIVO" },
              new { Valor = 7, Text = "FINALIZADO" },
            }, "Text", "Text");

            ViewBag.Emissao = new SelectList(new[]
             {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");


            ViewBag.Recibo = new SelectList(new[]
              {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");

            return View(cadastros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcadastro,ncadastro,idlote,idmunicipio,propietario,cpf,fonecontato,propriedade,comunidade,area,statuscoodenada,statusgoogle,statusservico,observacao,cadastrador,dtcadastro,statusemissao,numerocarba,statusimpressao,dtentrega")] Cadastros cadastros, Arquivos arq)
        {
          

            ViewBag.idlote = new SelectList(db.Lotes, "idlote", "lote").OrderBy(l => l.Text);
            ViewBag.idmunicipio = new SelectList(db.Municipios, "idmunicipio", "municipio").OrderBy(m => m.Text);
            ViewBag.idpropietario = new SelectList(db.Propietario, "idpropietario", "propietario1").OrderBy(p => p.Text);
            ViewBag.cadastrador = new SelectList(db.Cadastrador, "cadastrador1", "cadastrador1").OrderBy(c => c.Text);

            ViewBag.Coordenadas = new SelectList(new[]
       {
              new { Valor = 1, Text = "COM COORDENADAS" },
              new { Valor = 2, Text = "SEM COORDENADAS" },
            }, "Text", "Text");

            ViewBag.Google = new SelectList(new[]
             {
              new { Valor = 1, Text = "ESTÁ NO GOOGLE DRIVE" },
              new { Valor = 2, Text = "NÃO ESTÁ NO GOOGLE DRIVE" },
            }, "Text", "Text");


            ViewBag.Servico = new SelectList(new[]
{
              new { Valor = 1, Text = "EQUIPE GOIÂNIA" },
              new { Valor = 2, Text = "SALA TÉCNICA" },
              new { Valor = 3, Text = "ERRO DE LOCALIZAÇÃO" },
              new { Valor = 4, Text = "SOBREPOSIÇÃO" },
              new { Valor = 5, Text = "TRIAGEM" },
              new { Valor = 6, Text = "ARQUIVO" },
              new { Valor = 7, Text = "FINALIZADO" },
            }, "Text", "Text");

            ViewBag.Emissao = new SelectList(new[]
             {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");


            ViewBag.Recibo = new SelectList(new[]
              {
              new { Valor = 1, Text = "SIM" },
              new { Valor = 2, Text = "NÃO" },
            }, "Text", "Text");

          
                db.Entry(cadastros).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("Index");
            

           // byte[] datab = null;

            string nomeArquivob = "";

            foreach (var arquivo in arq.Arquivoss)
            {

                if (arquivo.ContentLength > 0)
                {
                    nomeArquivob = Path.GetFileName(arquivo.FileName);
                    var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivob);
                    arquivo.SaveAs(caminho);
                    FileStream fStream = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                    long numBytes = caminho.Length;
                    BinaryReader br = new BinaryReader(fStream);
                    //datab = br.ReadBytes((int)numBytes);
                    arq.caminho = caminho;
                    arq.idcadastro = cadastros.idcadastro;
                    arq.idlote = cadastros.idlote;
                    arq.cadastrador = cadastros.cadastrador;
                    arq.nomearquivo = nomeArquivob;
                }

                db.Arquivos.Add(arq);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }



        // GET: Cadastros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastros cadastros = await db.Cadastros.FindAsync(id);
            if (cadastros == null)
            {
                return HttpNotFound();
            }
            return View(cadastros);
        }

        // POST: Cadastros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cadastros cadastros = await db.Cadastros.FindAsync(id);
            db.Cadastros.Remove(cadastros);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteArquivo(int id)
        {
            Arquivos arquivos = db.Arquivos.Find(id);
            int idCadastro = arquivos.idcadastro;
            db.Arquivos.Remove(arquivos);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = idCadastro });
        }

        [HttpPost]
        public ActionResult Index(string pesquisaProp)
        {

            List<Cadastros> consultass;
            if (string.IsNullOrEmpty(pesquisaProp))
            {
                consultass = db.Cadastros.ToList();
            }
            else
            {
                consultass = db.Cadastros.Where(m => m.propietario.StartsWith(pesquisaProp)).ToList();
            }
            return View(consultass);


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
