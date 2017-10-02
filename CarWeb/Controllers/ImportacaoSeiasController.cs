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
using System.Data.OleDb;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace CarWeb.Controllers
{
    public class ImportacaoSeiasController : Controller
    {
      
        // GET: ImportacaoSeias
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImportacaoSeias/Details/5
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ModelCarWeb"].ConnectionString);
                    string query = ("Insert into ImportacaoSeia values(@nomeimovel,@municipio,@statuscadastro,@requerente,@area,@dtprimeirafinalizacao)");
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (ds.Tables[0].Rows[i][1].ToString() != "")
                    {

                        cmd.Parameters.AddWithValue("@nomeimovel", ds.Tables[0].Rows[i][1].ToString());
                        cmd.Parameters.AddWithValue("@municipio", ds.Tables[0].Rows[i][2].ToString());
                        cmd.Parameters.AddWithValue("@statuscadastro", ds.Tables[0].Rows[i][3].ToString());
                        cmd.Parameters.AddWithValue("@requerente", ds.Tables[0].Rows[i][4].ToString());
                        decimal area = Convert.ToDecimal(ds.Tables[0].Rows[i][5].ToString());
                        cmd.Parameters.AddWithValue("@area", area);
                        cmd.Parameters.AddWithValue("@dtprimeirafinalizacao", SqlDbType.Date).Value = Convert.ToDateTime(ds.Tables[0].Rows[i][6]).ToString("yyyy/MM/dd");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }

            }
            return View();
        }

    }
}
